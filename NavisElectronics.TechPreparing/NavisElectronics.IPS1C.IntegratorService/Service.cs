using System.Threading;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using NavisElectronics.IPS1C.IntegratorService.Services;

namespace NavisElectronics.IPS1C.IntegratorService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Entities;
    using Exceptions;
    using Intermech.Interfaces;
    using ListOfCooperation.Entities;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;
    using NavisElectronics.TechPreparation.Helpers;
    using NavisElectronics.TechPreparation.Services;

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

        public void NotifyIM(long ipsObjectId, long code1C)
        {
            //TODO: здесь должен быть механизм сохранения кода 1с в объект базы IPS
            //using (SessionKeeper keeper = new SessionKeeper())
            //{
            //    IDBObject myObject = keeper.Session.GetObjectByID(ipsObjectId, false);
            //    IDBAttribute attNavis = myObject.GetAttributeByName("Navis1C");
            //}
        }



        /// <summary>
        /// Получить полный состав указанной версии объекта
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<ProductTreeNode> GetProductComposition(long id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод позволяет получить данные о наименовании, обозначении и типе по указанному Id
        /// </summary>
        /// <param name="id">
        /// Идентификатор версии объекта
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// Массив строк
        /// </returns>
        /// <exception cref="ObjectNotFoundException">
        /// Если объект не найден, вываливаем исключение из IPS
        /// </exception>
        public ProductTreeNode GetProductInfo(long objectId)
        {
            ProductTreeNode treeNode = new ProductTreeNode();
            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                // нашли первый попавшийся объект с таким номером
                IDBObject myObject = keeper.Session.GetObjectByID(objectId, true);

                // хочу попробовать получить здесь набор элементов с данным ID объекта
                IDBObjectCollection objectCollection = null;
                DataTable table = null;

                objectCollection = keeper.Session.GetObjectCollection(myObject.TypeID);
                treeNode.Type1 = myObject.TypeID.ToString();
                ConditionStructure[] conditions = new ConditionStructure[1];

                ConditionStructure condition = new ConditionStructure(-3, RelationalOperators.Equal,
                    objectId, LogicalOperators.NONE, 0, false);
                conditions[0] = condition;

                // номер изменения 1035
                // PartNumber 17784
                // Id версии -2
                // 9 - обозначение
                // 10 - наименование
                DBRecordSetParams pars = new DBRecordSetParams(conditions, new object[] { -2, 9, 10, 17784, 1035 }, null,null);
                table = objectCollection.Select(pars);


                IDBAttribute measureUnitAttribute = myObject.GetAttributeByID(1254);
                if (measureUnitAttribute != null)
                {
                    // надо теперь выцепить по атрибуту ед. измерения сам объект единиц измерения
                    IDBObject unitsObject = keeper.Session.GetObject(measureUnitAttribute.AsInteger, false);
                    if (unitsObject != null)
                    {
                        IDBAttribute shortNameUnit = unitsObject.GetAttributeByID(13);
                        treeNode.MeasureUnits1 = shortNameUnit.AsString;
                    }
                }
                else
                {
                    treeNode.MeasureUnits1 = "шт";
                }

                foreach (DataRow row in table.Rows)
                {
                    LifeCycleSteps currentStep = LifeCycleSteps.Developing;
                    ProductTreeNode versionNode = new ProductTreeNode();
                    versionNode.ObjectId1 = objectId.ToString();
                    versionNode.Type1 = myObject.TypeID.ToString();
                    versionNode.Id1 = Convert.ToString(row[0]);
                    if (row[1] != DBNull.Value)
                    {
                        versionNode.Designation1 = (string)row[1];
                    }
                    else
                    {
                        versionNode.Designation1 = string.Empty;
                    }
                    if (row[2] != DBNull.Value)
                    {
                        versionNode.Name1 = (string) row[2];
                    }
                    else
                    {
                        versionNode.Name1 = string.Empty;
                    }

                    if (row[3] != DBNull.Value)
                    {
                        versionNode.PartNumber1 = (string)row[3];
                    }
                    else
                    {
                        versionNode.PartNumber1 = string.Empty;
                    }

                    if (row[4] != DBNull.Value)
                    {
                        versionNode.LastVersion1 = (string)row[4];
                    }
                    else
                    {
                        versionNode.LastVersion1 = string.Empty;
                    }

                    versionNode.MeasureUnits1 = treeNode.MeasureUnits1;

                    treeNode.Name1 = versionNode.Name1;
                    treeNode.Designation1 = versionNode.Designation1;

                    treeNode.Add(versionNode);

                    int treeNodeType = myObject.TypeID;

                    // если объект может содержать другие объекты, то смотрим его состав документации. По спецификации определяем номер изменения
                    if (myObject.TypeID == 1078 || treeNodeType == 1074 || treeNodeType == 1052 || treeNodeType == 1097)
                    {

                        IDBObject objectVersion = keeper.Session.GetObject(long.Parse(versionNode.Id1));
                        
                        // Необходимые колонки
                        ColumnDescriptor[] specificationColumns =
                        {
                            new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор объекта
                            new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                            new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер изменения
                        };

                        // получаем историю жизненного цикла версии объекта
                        DataTable lcHistoryDataTable = objectVersion.GetLCHistory(false);

                        // Находим строку с датой когда версия сборочной единицы была переведена на Шаг ЖЦ "Хранение"
                        DateTime date = DateTime.MinValue;
                        foreach (DataRow dataRow in lcHistoryDataTable.Rows)
                        {
                            if ((int) dataRow["F_LC_STEP"] == (int)LifeCycleSteps.Keeping)
                            {
                                // получаем дату
                                date = DateTime.Parse(dataRow["F_START_DATE"].ToString());
                                currentStep = LifeCycleSteps.Keeping;
                                break;
                            }
                        }

                        // по дате надо найти ту версию входящего документа, которая была актуальна на эту дату, то есть базовая версия на эту дату, но это при условии, что версия узла находится на хранении. Если на производстве, но можно оставить так, как есть
                        DataTable docComposition = compositionService.LoadComposition(keeper.Session.SessionGUID, objectVersion.ObjectID, 1004, new List<ColumnDescriptor>(specificationColumns), string.Empty, 1259, 1682);

                        if (docComposition.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in docComposition.Rows)
                            {
                                int type = Convert.ToInt32(dataRow[1]);
                                switch (type)
                                {
                                    // нашли спецификацию. Ее версию подбирать не будем, потому что действует жестая конкретизация
                                    case 1259:
                                        if (dataRow[2] != DBNull.Value)
                                        {
                                            versionNode.LastVersion1 = Convert.ToString(dataRow[2]);
                                        }

                                        break;
                                    
                                    // нашли проект AD. Для него стоило бы подобрать версию
                                    case 1682:
                                        if (treeNodeType == 1052)
                                        {
                                            versionNode.IsPCB = "True";
                                        }
                                        else
                                        {
                                            // если в сборке или комплекте есть Э3, то это не печатная плата. Здесь просто проверяем, ничего не надо подбирать
                                            DataTable schemes = compositionService.LoadComposition(keeper.Session.SessionGUID, Convert.ToInt64(versionNode.Id1), 1004, new List<ColumnDescriptor>(specificationColumns), string.Empty, 1307);
                                            if (schemes.Rows.Count == 0)
                                            {
                                                versionNode.IsPCB = "True";
                                            }
                                        }

                                        // забираем номер версии печатной платы с файла. Файл надо подобрать правильно, так что заходим 
                                        if (versionNode.IsPCB == "True")
                                        {
                                            // это базовая версия
                                            IDBObject pcbProject = keeper.Session.GetObject(Convert.ToInt64(dataRow[0]));

                                            // Если шаг ЖЦ - Хранение, то надо подобрать нужную версию
                                            if (currentStep == LifeCycleSteps.Keeping)
                                            {
                                                IList<long> allVersions = keeper.Session.GetAllObjectVersionsList(pcbProject.ID, true, false, false);
                                                
                                                DataTable historyTable = new DataTable();
                                                DataColumn versionIdColumn = new DataColumn("F_OBJECT_ID", typeof(long));
                                                DataColumn stepColumn = new DataColumn("F_LC_STEP", typeof(int));
                                                DataColumn dateColumn = new DataColumn("F_START_DATE", typeof(DateTime));
                                                DataColumn numberVersionColumn = new DataColumn("F_VERSION_ID", typeof(int));

                                                historyTable.Columns.AddRange(new DataColumn[] {versionIdColumn,stepColumn,dateColumn,numberVersionColumn});

                                                foreach (long version in allVersions)
                                                {
                                                    IDBObject versionPcbObject = keeper.Session.GetObject(version);
                                                    DataTable sinlgeVersionHistoryDataTable = versionPcbObject.GetLCHistory();
                                                    foreach (DataRow myRow in sinlgeVersionHistoryDataTable.Rows)
                                                    {
                                                        DataRow historyRow = historyTable.NewRow();
                                                        historyRow["F_OBJECT_ID"] = versionPcbObject.ObjectID;
                                                        historyRow["F_LC_STEP"] = myRow["F_LC_STEP"];
                                                        historyRow["F_START_DATE"] = myRow["F_START_DATE"];
                                                        historyRow["F_VERSION_ID"] = versionPcbObject.VersionID;
                                                        historyTable.Rows.Add(historyRow);
                                                    }
                                                }

                                                long versionId = SelectVersionOnDate(historyTable, date);
                                                pcbProject = keeper.Session.GetObject(versionId);
                                            }

                                            IDBAttribute fileAttribute = pcbProject.GetAttributeByID(1002);
                                            string lastDigitsOfDesignation = GetLastNumbersOfDesignation(treeNode.Designation1);
                                            if (fileAttribute != null)
                                            {
                                                object[] data = fileAttribute.Values;
                                                CheckVersion(data, lastDigitsOfDesignation, versionNode, treeNode);
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return treeNode;
        }

        /// <summary>
        /// Получает актуальную версию входящих документов, находящихся на производстве, на дату с учетом того родитель на хранении
        /// </summary>
        /// <param name="history">История ЖЦ всех версий</param>
        /// <param name="date">Дата постановки на хранение</param>
        /// <returns>Возвращает актуальную версию на дату</returns>
        internal long SelectVersionOnDate(DataTable history, DateTime date)
        {
            IList<long> versionsOnManufactoring = new List<long>();
            
            // Нужен для того, чтобы не регистрировать лишние постановки на хранение
            IDictionary<int, DataRow> dictionary = new Dictionary<int, DataRow>(history.Rows.Count);
            IList<DateTime> datesOfSetKeepingLfStep = new List<DateTime>();

            foreach (DataRow historyRow in history.Rows)
            {
                if (Convert.ToInt32(historyRow["F_LC_STEP"]) == 1018)
                {
                    DateTime stepDateTime = DateTime.Parse(historyRow["F_START_DATE"].ToString());

                    int compareResult = DateTime.Compare(
                        new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second),
                        new DateTime(stepDateTime.Year, stepDateTime.Month, stepDateTime.Day, stepDateTime.Hour, stepDateTime.Minute, stepDateTime.Second));

                    if (compareResult == 0 || compareResult == 1)
                    {
                        if (!dictionary.ContainsKey((int)historyRow["F_VERSION_ID"]))
                        {
                            dictionary.Add((int)historyRow["F_VERSION_ID"],historyRow);
                            datesOfSetKeepingLfStep.Add(stepDateTime);
                        }
                    }
                }
            }

            // если этапов "Хранение" было больше, чем 1, то надо выбрать этап производство между текущим и предыдущим
            if (datesOfSetKeepingLfStep.Count > 1)
            {
                int compareResult = DateTime.Compare(datesOfSetKeepingLfStep[datesOfSetKeepingLfStep.Count - 1], date);

                // если последняя постановка на хранение документа совпадает с постановкой на хранение сборочной единицы,
                // то надо выбрать между этим и предыдущим хранением
                DateTime previousKeepingStep = DateTime.MinValue;

                // если даты на хранении равны,то надо взять еще раньшую
                if (compareResult == 0)
                {
                    previousKeepingStep = datesOfSetKeepingLfStep[datesOfSetKeepingLfStep.Count - 2];
                }

                // если дата хранения оказалась меньше даты постановки на хранение сборочной единицы, то можно взять последний элемент 
                if (compareResult == -1)
                {
                    previousKeepingStep = datesOfSetKeepingLfStep[datesOfSetKeepingLfStep.Count - 1];
                }

                DateTime currentKeepingStep = date;

                // то, что нам нужно, лежит между подобранными датами: текущей и предыдущей
                foreach (DataRow historyRow in history.Rows)
                {
                    if ((int)historyRow["F_LC_STEP"] == 1015)
                    {
                        DateTime stepDateTime = DateTime.Parse(historyRow["F_START_DATE"].ToString());

                        int compareResultWithPrevious = DateTime.Compare(stepDateTime, previousKeepingStep);
                        int compareResultWithCurrent = DateTime.Compare(stepDateTime, currentKeepingStep);

                        if (compareResultWithPrevious >= 0 && compareResultWithCurrent <= 0)
                        {
                            versionsOnManufactoring.Add((long)historyRow["F_OBJECT_ID"]);
                        }
                    }
                }
            }
            else
            {
                // Если шага "Хранение" вообще еще не было, то возьмем единственную версию на производстве
                if (datesOfSetKeepingLfStep.Count == 0)
                {
                    foreach (DataRow historyRow in history.Rows)
                    {
                        if ((int)historyRow["F_LC_STEP"] == 1015)
                        {
                            versionsOnManufactoring.Add((long)historyRow["F_OBJECT_ID"]);
                            break;
                        }
                    }
                }
                else
                {
                    // если был только один шаг Хранения
                    // он может быть равным текущей дате, а также меньше неё
                    DateTime dateOfOnlyOneKeepingStep = datesOfSetKeepingLfStep[0];


                    int compareResult = DateTime.Compare(date, dateOfOnlyOneKeepingStep);

                    switch (compareResult)
                    {
                        // Даты равны
                        case 0:

                            // когда даты равны, то надо забрать версию на производстве ту, которая меньше текущей
                            foreach (DataRow historyRow in history.Rows)
                            {
                                if ((int)historyRow["F_LC_STEP"] == 1015)
                                {
                                    DateTime stepDateTime = DateTime.Parse(historyRow["F_START_DATE"].ToString());
                                    if (DateTime.Compare(stepDateTime, date) < 0)
                                    {
                                        versionsOnManufactoring.Add((long)historyRow["F_OBJECT_ID"]);
                                        break;
                                    }
                                }
                            }

                            break;

                        // текущая дата постановки на хранение сборочной единицы больше, чем дата постановки на хранение документа
                        case 1:
                            // надо подобрать такую дату, которая будет лежать между текущей датой и датой постановки документа на хранение
                            foreach (DataRow historyRow in history.Rows)
                            {
                                if ((int)historyRow["F_LC_STEP"] == 1015)
                                {
                                    DateTime stepDateTime = DateTime.Parse(historyRow["F_START_DATE"].ToString());

                                    if (DateTime.Compare(stepDateTime, date) < 0 && DateTime.Compare(stepDateTime, dateOfOnlyOneKeepingStep) >= 0)
                                    { 
                                        versionsOnManufactoring.Add((long)historyRow["F_OBJECT_ID"]);
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            long versionToChoose = -1;

            if (versionsOnManufactoring.Count == 1)
            {
                versionToChoose = versionsOnManufactoring[0];
            }

            return versionToChoose;

        }


        internal string CheckVersion(object[] data, string lastDigitsOfDesignation, ProductTreeNode versionNode, ProductTreeNode treeNode)
        {
            foreach (object o in data)
            {
                string str = (string)o;
                if (str.Contains("PcbDoc"))
                {
                    if (str.Contains(lastDigitsOfDesignation))
                    {
                        int versionPcb = ExtractPcbVersion(str);
                        versionNode.PcbVersion = versionPcb.ToString();
                        treeNode.IsPCB = versionNode.IsPCB;
                        break;
                    }
                }
            }

            return versionNode.PcbVersion;
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

        private int ExtractPcbVersion(string str)
        {
            int version = 0;
            
            // Разрежем по символу '.'
            string[] lines = str.Split(new char[] { '.' });
            if (lines.Length > 0)
            {
                int length = 0;
                for (int i = lines[lines.Length-2].Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(lines[lines.Length-2][i]))
                    {
                        length++;
                    }
                    else
                    {
                        break;
                    }
                }

                // нас интересует вторая часть строки
                string value = lines[lines.Length - 2].Substring(lines[lines.Length - 2].Length - length, length);
                version = int.Parse(value);
            }

            return version;
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
            DataSet myDataset = reader.GetDataset(versionId, ConstHelper.BinaryDataOfOrder);
            TreeBuilderService treeBuilderService = new TreeBuilderService();
            techDataOrderElement = treeBuilderService.Build(myDataset, CancellationToken.None);
            ProductTreeNodeMapper mapper = new ProductTreeNodeMapper();
            ProductTreeNode root = mapper.Map(techDataOrderElement);

            Changes changesDefiner = new Changes();

            ProductTreeNode updatedTreeElement = changesDefiner.Filter(root, agentFilter);

            return updatedTreeElement;
        }
    }
}
