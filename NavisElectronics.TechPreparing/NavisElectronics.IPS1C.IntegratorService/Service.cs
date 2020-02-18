using NavisElectronics.TechPreparation.Interfaces;

namespace NavisElectronics.IPS1C.IntegratorService
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Security.Authentication;
    using System.Threading.Tasks;
    using Entities;
    using Exceptions;
    using Intermech;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Interfaces.Server;
    using Intermech.Kernel.Search;
    using Services;
    using TechPreparation.Data;
    using TechPreparation.Interfaces.Entities;
    using TechPreparation.Interfaces.Helpers;
    using TechPreparation.Interfaces.Services;

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

            IDBTimedEvents timedEvents = ServerServices.GetService(typeof(IDBTimedEvents)) as IDBTimedEvents;

            IUserSession session = timedEvents.GetSystemSessionTemporaryClone();

            try
            {
                objectCollection[0] = session.GetObjectCollection(1138);
                objectCollection[1] = session.GetObjectCollection(1105);
                objectCollection[2] = session.GetObjectCollection(1128);

                // -2 идентификатор версии объекта, -3 - индентификатор объекта
                DBRecordSetParams pars = new DBRecordSetParams(null, new object[] { -2, -3 }, null, null);

                for (int i = 0; i < objectCollection.Length; i++)
                {
                    tables[i] = objectCollection[i].Select(pars);
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
            }
            finally
            {
                session.Logout();
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

            IDBTimedEvents timedEvents = ServerServices.GetService(typeof(IDBTimedEvents)) as IDBTimedEvents;

            IUserSession session = timedEvents.GetSystemSessionTemporaryClone();

            try
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)session.GetCustomService(typeof(ICompositionLoadService));

                // нашли первый попавшийся объект с таким номером
                IDBObject myObject;

                try
                {
                    myObject = session.GetObjectByID(tempId, true);
                }
                catch (Exception)
                {
                    // перехватываем все исключения

                    // это поиск версии
                    myObject = session.GetObject(tempId, true);
                    // из этой версии получаем опять первую попавшуюся
                    myObject = session.GetObjectByID(myObject.ID, true);
                    // Сохрагняем во временной переменной идентификатор объекта. По нему будем искать все существующие версии
                    tempId = myObject.ID;
                }

                // выбрать из всех версий объектов базовую
                List<long> allObjectVersionsList = session.GetAllObjectVersionsList(tempId, true, false, false);
                foreach (long versionId in allObjectVersionsList)
                {
                    IDBObject versionObject = session.GetObject(versionId);
                    if (versionObject.IsBaseVersion)
                    {
                        myObject = versionObject;
                        break;
                    }
                }

                // забираем атрибут Единицы измерения, причем его краткую часть
                IDBAttribute measureUnitAttribute = myObject.GetAttributeByID(1254);
                if (measureUnitAttribute != null)
                {
                    // надо теперь выцепить по атрибуту ед. измерения сам объект единиц измерения
                    IDBObject unitsObject = session.GetObject(measureUnitAttribute.AsInteger, false);
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

                // номер изменения 1035
                // PartNumber 17784
                // Id версии -2
                // 9 - обозначение
                // 10 - наименование
                // 17965 - Версия печатной платы
                // 18079 - флаг печатной платы

                ProductTreeNode versionNode = new ProductTreeNode();

                versionNode.VersionId = myObject.ObjectID.ToString();
                long mainObjectVersion = myObject.ObjectID;

                versionNode.ObjectId = myObject.ID.ToString();
                versionNode.Type = myObject.TypeID.ToString();

                IDBAttribute designationAttribute = myObject.GetAttributeByID(9);
                if (designationAttribute != null)
                {
                    versionNode.Designation = designationAttribute.Value != DBNull.Value ? (string)designationAttribute.Value : string.Empty;
                }


                IDBAttribute nameAttribute = myObject.GetAttributeByID(10);
                if (nameAttribute != null)
                {
                    versionNode.Name = nameAttribute.Value != DBNull.Value ? (string)nameAttribute.Value : string.Empty;
                }


                IDBAttribute partNumberAttribute = myObject.GetAttributeByID(17784);
                if (partNumberAttribute != null)
                {
                    versionNode.PartNumber = partNumberAttribute.Value != DBNull.Value ? (string)partNumberAttribute.Value : string.Empty;
                }


                IDBAttribute currentNumberOfChangeAttribute = myObject.GetAttributeByID(1035);
                if (currentNumberOfChangeAttribute != null)
                {
                    versionNode.LastVersion = currentNumberOfChangeAttribute.Value != DBNull.Value ? (string)currentNumberOfChangeAttribute.Value : string.Empty;
                }


                IDBAttribute pcbFlagAttribute = myObject.GetAttributeByID(18079);
                if (pcbFlagAttribute != null)
                {
                    versionNode.IsPCB = pcbFlagAttribute.Value != DBNull.Value ? (Convert.ToBoolean(pcbFlagAttribute.Value)).ToString() : "False";
                }


                IDBAttribute pcbVersionAttribute = myObject.GetAttributeByID(17965);
                if (pcbVersionAttribute != null)
                {
                    versionNode.PcbVersion = pcbVersionAttribute.Value != DBNull.Value ? (Convert.ToInt32(pcbVersionAttribute.Value)).ToString() : string.Empty;
                }

                versionNode.MeasureUnits = treeNode.MeasureUnits;


                treeNode.ObjectId = versionNode.ObjectId;
                treeNode.Name = versionNode.Name;
                treeNode.Designation = versionNode.Designation;
                treeNode.Add(versionNode);
                int treeNodeType = myObject.TypeID;

                PickVersion(versionNode, treeNodeType, mainObjectVersion, session, compositionService);
            }
            finally
            {
                session.Logout();
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
            IntermechReader reader = new IntermechReader(new RecountService());
            
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
            IntermechReader reader = new IntermechReader(new RecountService());
            TechRouteNode organizationStruct = null;
            Task<TechRouteNode> task = Task.Run(async () => await reader.GetDataFromBinaryAttributeAsync<TechRouteNode>(orderVersionId, ConstHelper.OrganizationStructAttribute, new DeserializeStrategyBson<TechRouteNode>()));
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
