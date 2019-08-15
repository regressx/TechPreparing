using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.IPS1C.IntegratorService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using Entities;
    using Exceptions;
    using Intermech;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Kernel.Search;
    using Services;
    using TechPreparation.Entities;
    using TechPreparation.Enums;
    using TechPreparation.Services;
    using TechPreparing.Data;
    using Changes = Logic.Changes;


    /// <summary>
    /// Реализация интерфейса IService
    /// </summary>
    public class Service : IService
    {

        /// <summary>
        /// Возвращает введенную строку через сервис
        /// </summary>
        /// <param name="message">
        /// Строка
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMessage(string message)
        {
            return message;
        }

        public ProductTreeNode GetAllProducts()
        {
            ProductTreeNode resultNode = new ProductTreeNode();
            resultNode.Designation = "Результат";
            return resultNode;
        }

        public ProductTreeNode GetUsedTypes()
        {
            ProductTreeNode resultNode = new ProductTreeNode();
            return resultNode;
        }

        /// <summary>
        /// Метод позволяет получить данные о наименовании, обозначении и типе по указанному Id
        /// </summary>
        /// <param name="objectId">Идентификатор объекта</param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// Массив строк
        /// </returns>
        /// <exception cref="ObjectNotFoundException">
        /// Если объект не найден, вываливаем исключение из IPS
        /// </exception>
        public ProductTreeNode GetProductInfo(long objectId)
        {
            long tempId = objectId;
            ProductTreeNode treeNode = new ProductTreeNode();
            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                bool isVersion = false;

                // нашли первый попавшийся объект с таким номером
                IDBObject myObject;
                try
                {
                    myObject = keeper.Session.GetObjectByID(tempId, true);
                }
                catch (Exception) 
                {
                    // перехватываем все исключения
                    
                    // это поиск версии
                    myObject = keeper.Session.GetObject(tempId, true);

                    // из этой версии получаем опять первую попавшуюся, чтобы не напрягаться переписыванием кода
                    myObject = keeper.Session.GetObjectByID(myObject.ID, true);
                    tempId = myObject.ID;

                    isVersion = true;
                }

                // забираем атрибут Единицы измерения, причем его краткую часть
                IDBAttribute measureUnitAttribute = myObject.GetAttributeByID(1254);
                if (measureUnitAttribute != null)
                {
                    // надо теперь выцепить по атрибуту ед. измерения сам объект единиц измерения
                    IDBObject unitsObject = keeper.Session.GetObject(measureUnitAttribute.AsInteger, false);
                    if (unitsObject != null)
                    {
                        IDBAttribute shortNameUnit = unitsObject.GetAttributeByID(13);
                        treeNode.MeasureUnits = shortNameUnit.AsString;
                    }
                }
                else
                {
                    treeNode.MeasureUnits = "шт";
                }


                // если искали по коду объекта, то надо найти все версии
                IDBObjectCollection objectCollection = null;
                DataTable table = null;
                objectCollection = keeper.Session.GetObjectCollection(myObject.TypeID);
                treeNode.Type = myObject.TypeID.ToString();
                ConditionStructure[] conditions = new ConditionStructure[1];

                ConditionStructure condition = new ConditionStructure(-3, RelationalOperators.Equal, myObject.ID, LogicalOperators.NONE, 0, false);
                conditions[0] = condition;

                // номер изменения 1035
                // PartNumber 17784
                // Id версии -2
                // 9 - обозначение
                // 10 - наименование
                // 17965 - Версия печатной платы
                // 18079 - флаг печатной платы

                DBRecordSetParams pars = new DBRecordSetParams(conditions, new object[] { -2, 9, 10, 17784, 1035, 18079, 17965 }, null, null);
                table = objectCollection.Select(pars);
                foreach (DataRow row in table.Rows)
                {
                    long mainObjectVersion = Convert.ToInt64(row[0]);
                    
                    // если мы искали по версии, то пропустить все несовпадающие по номеру объекты
                    if (isVersion)
                    {
                        if (mainObjectVersion != objectId)
                        {
                            continue;
                        }
                    }

                    ProductTreeNode versionNode = new ProductTreeNode();
                    versionNode.VersionId = mainObjectVersion.ToString();

                    versionNode.ObjectId = tempId.ToString();
                    versionNode.Type = myObject.TypeID.ToString();

                    versionNode.Designation = row[1] != DBNull.Value ? (string)row[1] : string.Empty;
                    versionNode.Name = row[2] != DBNull.Value ? (string)row[2] : string.Empty;
                    versionNode.PartNumber = row[3] != DBNull.Value ? (string)row[3] : string.Empty;
                    versionNode.LastVersion = row[4] != DBNull.Value ? (string)row[4] : string.Empty;
                    versionNode.IsPCB = row[5] != DBNull.Value ? (Convert.ToBoolean(row[5])).ToString() : "False";
                    versionNode.PcbVersion = row[6] != DBNull.Value ? (Convert.ToInt32(row[6])).ToString() : string.Empty;

                    versionNode.MeasureUnits = treeNode.MeasureUnits;
                    treeNode.ObjectId = tempId.ToString();
                    treeNode.Name = versionNode.Name;
                    treeNode.Designation = versionNode.Designation;
                    treeNode.Add(versionNode);
                    int treeNodeType = myObject.TypeID;

                    PickVersion(versionNode, treeNodeType, mainObjectVersion, keeper.Session, compositionService);
                }

            }
            return treeNode;
        }

        private void PickVersion(ProductTreeNode versionNode, int treeNodeType, long mainObjectVersion, IUserSession session, ICompositionLoadService compositionService)
        {
            // если объект может содержать другие объекты, то смотрим его состав документации. По спецификации определяем номер изменения
            if (treeNodeType == 1078 || treeNodeType == 1074 || treeNodeType == 1097)
            {
                IDBObject objectVersion = session.GetObject(mainObjectVersion);
                        
                // Необходимые колонки
                ColumnDescriptor[] specificationColumns =
                {
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор объекта
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер изменения
                };

                // поиск состава по связи "Документация на изделие"
                DataTable docComposition = compositionService.LoadComposition(session.SessionGUID, objectVersion.ObjectID, 1004, new List<ColumnDescriptor>(specificationColumns), string.Empty, 1259);

                if (docComposition.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in docComposition.Rows)
                    {
                        int type = Convert.ToInt32(dataRow[1]);
                        switch (type)
                        {
                            // нашли спецификацию
                            case 1259:
                                if (dataRow[2] != DBNull.Value)
                                {
                                    versionNode.LastVersion = Convert.ToString(dataRow[2]);
                                }
                                break;
                        }
                    }
                }
            }
        }

        internal string GetLastNumbersOfDesignation(string str)
        {
            string resultString = string.Empty;

            if (str != string.Empty)
            {
                int lastIndex = str.LastIndexOf('.');

                resultString = str.Substring(lastIndex + 1);

                if (resultString.Contains("-"))
                {
                    resultString = resultString.Remove(resultString.IndexOf('-'));
                }

            }
            return resultString;
        }

        /// <summary>
        /// Получает отфильтрованный заказ для КБ НАВИС
        /// </summary>
        /// <param name="versionId">
        /// The version id.
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// </returns>
        public ProductTreeNode GetFilteredOrderForKB(long versionId)
        {
            return GetFilteredOrder(versionId, ((int)AgentsId.Kb).ToString());
        }

        /// <summary>
        /// Получает отфильтрованный заказ для НЭ
        /// </summary>
        /// <param name="versionId">
        /// The version id.
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// </returns>
        public ProductTreeNode GetFilteredOrderForElectronics(long versionId)
        {
            return GetFilteredOrder(versionId, ((int)AgentsId.NavisElectronics).ToString());
        }

        /// <summary>
        /// Получает отфильтрованный в соответствии с технологическими данными готовый состав заказа
        /// </summary>
        /// <param name="versionId">
        /// The id.
        /// </param>
        /// <param name="agentFilter">
        /// фильтр по агенту
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// Возвращает узел главный узел дерева
        /// </returns>
        private ProductTreeNode GetFilteredOrder(long versionId, string agentFilter)
        {
            if (versionId < 0)
            {
                throw new OrderInfoException("На данный момент заказ находится на редактировании. Скорее всего, он еще не готов для передачи. Для уточнения информации о статусе заказа свяжитесь с технологами");
            }
           
            // технологические данные заказа: сведения о кооперации, тех. маршруты
            IntermechTreeElement techDataOrderElement = null;

            IntermechReader reader = new IntermechReader();
            long organization = long.Parse(agentFilter);

            // Асинхронно получаем файлик
            reader.GetDataFromFileAsync(versionId, 1006)
                .ContinueWith(t =>
                {
                    // здесь получаем результат
                    techDataOrderElement = t.Result;
                });

            ProductTreeNodeMapper mapper = new ProductTreeNodeMapper();
            ProductTreeNode root = mapper.Map(techDataOrderElement);

            Changes changesDefiner = new Changes();

            ProductTreeNode updatedTreeElement = changesDefiner.Filter(root, agentFilter);

            return updatedTreeElement;
        }
    }
}
