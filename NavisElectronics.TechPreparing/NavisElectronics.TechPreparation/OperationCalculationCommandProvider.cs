using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using NavisElectronics.TechPreparation.Presenters;
using Intermech.Expert.User;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Expert;

namespace NavisElectronics.TechPreparation
{
    public class OperationCalculationCommandProvider : ICommandsProvider
    {
        // встроенный класс
        public class Element
        {
            public int AmountOfContacts {get;set;}
            public int Amount {get;set;}
            public string Diameter {get;set;}
        }



        public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            // ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе даных 
            // для того, чтобы проверить, можно ли отображать команду меню или нет!

            // Список добавленных или перекрытых команд контекстного меню
            CommandsInfo commandsInfo = new CommandsInfo();

            // Есть один выделенный элемент
            if (items != null && items.Count == 1)
            {
                commandsInfo.Add("CalculateOperation",
                    new CommandInfo(TriggerPriority.ItemCategory, OnClickCalculateOperation));
            }

            // Вернём список
            return commandsInfo;
        }

        private void OnClickCalculateOperation(ISelectedItems items, IServiceProvider viewservices, object additionalinfo)
        {
            INodeID nodeId = items.GetItemID(0);

            // идентификатор версии операции
            long id = nodeId.GetObjVerID();

            ColumnDescriptor[] columnsForTechRoutes =
            {
                new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                    ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                    ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0) // тип объекта
            };

            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                int type = -1;
                long routeId = id;

                // крутим, пока не найдем маршрут обработки
                while (type != 1037)
                {
                    DataTable table = compositionService.LoadCompositionApplicability(keeper.Session, routeId, 1002, columnsForTechRoutes,
                        Intermech.SystemGUIDs.filtrationBaseVersions, 1117, 1037,1110);

                    foreach (DataRow row in table.Rows)
                    {
                        routeId = (long) row[0];
                        type = (int)row[1];
                    }

                    if (table.Rows.Count != 1)
                    {
                        break;
                    }
                }

                if (type != 1037)
                {
                    return;
                }

                ColumnDescriptor[] columnsForBlank =
                {
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(1220, AttributeSourceTypes.Object,
                    ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0) // вид изделия
                };


                // Поиск состава
                DataTable blankTable = compositionService.LoadComposition(keeper.Session, routeId, 1002, new List<ColumnDescriptor>(columnsForBlank), Intermech.SystemGUIDs.filtrationBaseVersions, 1090); // забрать заготовку

                // проверим и оповестим пользователя, что отсутствует заготовка
                if (blankTable.Rows.Count == 0)
                {
                    MessageBox.Show("У Вас отсутствует объект типа Заготовка в составе маршрута обработки");
                    return;
                }

                ColumnDescriptor[] columnsForAssemblies =
                {
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0) // тип объекта
                };

                DataTable assembliesTable = compositionService.LoadCompositionApplicability(keeper.Session, routeId, 1002, columnsForAssemblies,
                    Intermech.SystemGUIDs.filtrationBaseVersions, 1074, 1078, 1097);

                if (assembliesTable.Rows.Count != 1)
                {
                    MessageBox.Show("Маршрут обработки обязательно должен быть прикреплен к изделию, причем только к одному!");
                    return;
                }

                long assemblyId = (long)assembliesTable.Rows[0][0];

                ColumnDescriptor[] columns =
                {
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),  // тип объекта
                    new ColumnDescriptor(17887, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип монтажа компонента
                    new ColumnDescriptor(10176, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // диаметр отверстия
                    new ColumnDescriptor(18080, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)  // количество контактов
                };

                DataTable composition = compositionService.LoadComposition(keeper.Session,assemblyId,1,columns,Intermech.SystemGUIDs.filtrationBaseVersions,1074,1138);

                // получить все элементы ручного монтажа
                IDictionary<string, Element> statisticDictionary = new Dictionary<string, Element>();
                foreach(DataRow row in composition.Rows)
                {
                    string mountType = row[2] !=DBNull.Value ?  (string)row[2]: string.Empty;
                    string key = row[3] !=DBNull.Value ?  (string)row[3]: string.Empty;
                    int amountOfContacts = row[4] == DBNull.Value? 0 : Convert.ToInt32(row[4]);
                    if (mountType != string.Empty && mountType == "Ручной монтаж")
                    {
                        if (statisticDictionary.ContainsKey(key))
                        {
                            Element elementFromDictionary = statisticDictionary[key];
                            elementFromDictionary.AmountOfContacts += amountOfContacts;
                        }
                        else
                        {
                            statisticDictionary.Add(key, new Element 
                            {
                                Amount = 1,
                                AmountOfContacts = amountOfContacts,
                                Diameter = key
                            });
                        }
                    }
                }

                ColumnDescriptor[] columnsForMaterialsInOperation =
                {
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(-20, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0), // Id связи 
                };

                DataTable materialsInOperation = compositionService.LoadComposition(keeper.Session,id,1002,columnsForMaterialsInOperation,Intermech.SystemGUIDs.filtrationBaseVersions,1128);
                


                object value = null;

                foreach (DataRow row in materialsInOperation.Rows)
                {
                    long materialId = (long)row[0];
                    double sum = 0;

                    IDBObject materialObject = keeper.Session.GetObject(materialId);
                    IDBRelation relationId = keeper.Session.GetRelation((long)row[2]);
                    IDBAttribute holeDiameterAttribute = materialObject.GetAttributeByID(10176);
                    IDBAttribute normsAtrribute = relationId.GetAttributeByID(1223);

                    if (holeDiameterAttribute != null)
                    {
                        long measureId = 0;
                        long holeMeasureId = 0;
                        foreach (KeyValuePair<string, Element> pair in statisticDictionary)
                        {
                            MeasuredValue measureValue = MeasureHelper.ConvertToMeasuredValue(pair.Key);
                            holeDiameterAttribute.Value = measureValue;
                            holeMeasureId = measureValue.MeasureID;

                            IExpertUser expertUser = new ExpertUser();

                            IExpertTask expertTask = expertUser.GetExpertTask();

                            ExpertResult result = expertTask.Calculate(1223, materialId, out value);
                            if (result == ExpertResult.OK)
                            {
                                normsAtrribute.Value = value;

                                measureId = ((MeasuredValue)value).MeasureID;
                                sum += ((MeasuredValue)value).Value * pair.Value.AmountOfContacts * pair.Value.Amount;
                            }
                        }

                        IDBAttribute amountAttribute = relationId.GetAttributeByID(1129);
                        if (amountAttribute == null)
                        {
                            relationId.Attributes.AddAttribute(1129, false);
                        }
                        amountAttribute = relationId.GetAttributeByID(1129);
                        amountAttribute.Value = new MeasuredValue(sum, measureId);

                        holeDiameterAttribute.Value = new MeasuredValue(0, holeMeasureId);
                        normsAtrribute.Value = new MeasuredValue(0, measureId);
                    }
                }

            }

        }

        private void ExpertTask_EndCalculate(object sender, EndCalculateEventArgs e)
        {
            MessageBox.Show(e.Value.ToString());
        }

        public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            // ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе даных 
            // для того, чтобы проверить, можно ли отображать команду меню или нет!

            // Список добавленных или перекрытых команд контекстного меню
            CommandsInfo commandsInfo = new CommandsInfo();

            // Вернём список
            return commandsInfo;
        }
    }
}