using System.Security.Authentication;
using Intermech;
using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Helpers;


namespace NavisElectronics.IPS1C.IntegratorService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Entities;
    using Exceptions;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Interfaces.Server;
    using Intermech.Kernel.Search;
    using Services;
    using TechPreparation.Interfaces.Entities;

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

        /// <summary>
        /// Получает набор материалов, прочих изделий и стандартных
        /// </summary>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// </returns>
        public ProductTreeNode GetAllProducts()
        {
            ProductTreeNode resultNode = new ProductTreeNode();
            resultNode.Designation = "Результат";

            int countOfitems = 3;

            IDBObjectCollection[] objectCollection = new IDBObjectCollection[countOfitems];

            DataTable[] tables = new DataTable[countOfitems];


            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
                objectCollection[0] = sessionKeeper.Session.GetObjectCollection(1138);
                objectCollection[1] = sessionKeeper.Session.GetObjectCollection(1105);
                objectCollection[2] = sessionKeeper.Session.GetObjectCollection(1128);

                // -2 идентификатор версии объекта, -3 - индентификатор объекта
                DBRecordSetParams pars = new DBRecordSetParams(null, new object[] { -2, -3 }, null, null);

                for (int i = 0; i < objectCollection.Length; i++)
                {
                    tables[i] = objectCollection[i].Select(pars);
                }
            }

            foreach (DataTable table in tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    ProductTreeNode node = new ProductTreeNode()
                    {
                        VersionId = Convert.ToInt64(row[0]).ToString(),
                        ObjectId = Convert.ToInt64(row[1]).ToString()
                    };
                    resultNode.Add(node);
                }
            }

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

        /// <summary>
        /// Возвращает дерево заказа
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии заказа.
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// Дерево заказа
        /// </returns>
        public ProductTreeNode GetOrder(long versionId)
        {
            IDBObject orderObject = null;
            ProductTreeNode root = null;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                orderObject = keeper.Session.GetObject(versionId, true);

                // если шаг ЖЦ не производство, то выбрасываем исключение
                if (orderObject.LCStep != (int)OrderLifeCycleSteps.Manufacturing && orderObject.LCStep != (int)OrderLifeCycleSteps.Keeping)
                {
                    throw new OrderInfoException("Шаг жизненного цикла указанной версии заказа отличается от Производство и эксплуатация или от Хранения" );
                }
            }

            // технологические данные заказа: сведения о кооперации, тех. маршруты
            IntermechTreeElement techDataOrderElement = null;
            IntermechReader reader = new IntermechReader();
            
            Task<IntermechTreeElement> myTask = Task.Run(async () => await reader.GetDataFromFileAsync(versionId, ConstHelper.FileAttribute));
            techDataOrderElement = myTask.Result;


            if (techDataOrderElement == null)
            {
                throw new OrderInfoException("При обработке заказа модуль не смог получить данные о дереве заказа. Возможно, файл заказа отсутствует, или в нем испорчены данные");
            }

            ProductTreeNodeMapper mapper = new ProductTreeNodeMapper();
            root = mapper.Map(techDataOrderElement);

            return root;
        }

        /// <summary>
        /// Получить структуру организации
        /// </summary>
        /// <param name="orderVersionId">
        /// The order version id.
        /// </param>
        /// <returns>
        /// The <see cref="OrganizationNode"/>.
        /// </returns>
        /// <exception cref="OrderInfoException">
        /// Если что-то не так, выбросить исключение о неправильной информации в заказе
        /// </exception>
        public OrganizationNode GetOrganizationStruct(long orderVersionId)
        {
            IntermechReader reader = new IntermechReader();
            TechRouteNode organizationStruct = null;
            Task<TechRouteNode> task = Task.Run(async () => await reader.GetDataFromBinaryAttributeAsync<TechRouteNode>(orderVersionId, ConstHelper.OrganizationStructAttribute));
            organizationStruct = task.Result;
            if (organizationStruct == null)
            {
                throw new OrderInfoException("При обработке заказа модуль не смог получить данные о структуре предприятия на указанный заказ. Возможно, данные не прикреплены к заказу");
            }

            OrganizationNodeMapper mapper = new OrganizationNodeMapper();
            OrganizationNode root = mapper.Map(organizationStruct);
            return root;

        }

        /// <summary>
        /// Получить хэш заказа
        /// </summary>
        /// <param name="orderVersionId">
        /// The order version id.
        /// </param>
        /// <returns>
        /// The <see cref="HashAlgorithmNode"/>.
        /// </returns>
        public HashAlgorithmNode GetOrderFileHash(long orderVersionId)
        {
            byte[] bytes = null;
            IDBTimedEvents timedEvents = ServerServices.GetService(typeof(IDBTimedEvents)) as IDBTimedEvents;
            IUserSession session = timedEvents.GetSystemSessionTemporaryClone();
            try
            {
                IDBObject orderObject = session.GetObject(orderVersionId);
                IDBAttribute fileAttribute = orderObject.GetAttributeByID(ConstHelper.FileAttribute);

                if (fileAttribute != null)
                {
                    IBlobReader blobReader = fileAttribute as IBlobReader;
                    blobReader.OpenBlob(0);
                    bytes = blobReader.ReadDataBlock();
                    blobReader.CloseBlob();
                }
            }
            finally
            {
                session.Logout();
            }

            CheckSumService checkSumService = new CheckSumService();
            HashAlgorithmNode hashResultNode = new HashAlgorithmNode();

            if (bytes != null)
            {
                HashAlgorithmNode md5Node = new HashAlgorithmNode();
                md5Node.Name = "MD5";
                md5Node.Value = checkSumService.ComputeHash(new byte[0], HashAlgorithmType.Md5);

                HashAlgorithmNode sha1Node = new HashAlgorithmNode();
                sha1Node.Name = "SHA1";
                sha1Node.Value = checkSumService.ComputeHash(new byte[0], HashAlgorithmType.Md5);
                hashResultNode.Add(md5Node);
                hashResultNode.Add(sha1Node);
            }

            return hashResultNode;
        }

        /// <summary>
        /// The get tech disposal.
        /// </summary>
        /// <param name="objectId">
        /// The object version id.
        /// </param>
        /// <param name="totalAmount">
        /// The total amount.
        /// </param>
        /// <param name="year">
        /// The year.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTechDisposal(long objectId, double totalAmount, int year)
        {
            int caseAttributeId = 17765;
            IDBTimedEvents timedEvents = ServerServices.GetService(typeof(IDBTimedEvents)) as IDBTimedEvents;
            
            IUserSession session = timedEvents.GetSystemSessionTemporaryClone();

            string packageType = string.Empty;
            try
            {
                IDBObject orderObject = session.GetObjectByID(objectId, true);
                IDBAttribute caseAttribute = orderObject.GetAttributeByID(caseAttributeId);
                if (caseAttribute != null)
                {
                    packageType = caseAttribute.AsString;
                }
            }
            finally
            {
                session.Logout();
            }

            return GetTechDisposalInternal(packageType, totalAmount, year);
        }

        /// <summary>
        /// The get tech disposal internal.
        /// </summary>
        /// <param name="packageType">
        /// The package type.
        /// </param>
        /// <param name="totalAmount">
        /// The total amount.
        /// </param>
        /// <param name="year">
        /// The year.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal string GetTechDisposalInternal(string packageType, double totalAmount, int year)
        {
            if (packageType == string.Empty)
            {
                return "Попытка подать на вход пустой корпус или упаковку";
            }

            int koef = 1;
            int index = 0;
            int[] array = new int[2];

            if (totalAmount < 1000)
            {
                index = 1;
            }

            switch (packageType)
            {
                case "01005":
                    array[0] = 10;
                    array[1] = 100;
                    break;

                case "0201":
                    array[0] = 10;
                    array[1] = 100;
                    break;

                case "0402":
                    array[0] = 10;
                    array[1] = 100;
                    break;

                case "0603":
                    array[0] = 5;
                    array[1] = 50;
                    break;

                case "0805":
                    array[0] = 5;
                    array[1] = 50;
                    break;

                case "1206":
                    array[0] = 5;
                    array[1] = 50;
                    break;

                default:
                    return "Для такого типа объектов тип тех. отхода временно отсутствует";
            }

            // в процентах
            if (index == 0)
            {
                return ((koef + ((double)array[index] / 100)) * totalAmount).ToString("F6");
            }

            return (totalAmount + array[index]).ToString("F6");

        }

        /// <summary>
        /// The get last numbers of designation.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
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
        /// The pick version.
        /// </summary>
        /// <param name="versionNode">
        /// The version node.
        /// </param>
        /// <param name="treeNodeType">
        /// The tree node type.
        /// </param>
        /// <param name="mainObjectVersion">
        /// The main object version.
        /// </param>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="compositionService">
        /// The composition service.
        /// </param>
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
    }
}
