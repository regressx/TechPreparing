// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntermechReader.cs" company="NavisElectronics">
//   Cherkashin I.V.
// </copyright>
// <summary>
//   Реализует чтение составов отдельных элементов, целого заказа
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Entities;
    using Exceptions;
    using ICSharpCode.SharpZipLib.Zip.Compression;
    using Interfaces;
    using Interfaces.Entities;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Kernel.Search;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;
    using Substitutes;
    using TechPreparing.Data.Helpers;

    /// <summary>
    /// Реализует чтение составов отдельных элементов, целого заказа
    /// </summary>
    public class IntermechReader : IDataRepository
    {
        /// <summary>
        /// Cервис расшифровки доп. замен
        /// </summary>
        private readonly SubsituteDecryptorService _substituteDecriptorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntermechReader"/> class.
        /// </summary>
        public IntermechReader()
        {
            _substituteDecriptorService = new SubsituteDecryptorService(new ActualSubsitutesDecryptor(), new SubGroupsDecryptor(new SubGroupDecryptor()));
        }

        /// <summary>
        /// Асинхронный метод получения полного заказа
        /// </summary>
        /// <param name="versionId">
        /// Id версии заказа
        /// </param>
        /// <param name="token">
        /// Токен отмены
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<IntermechTreeElement> GetFullOrderAsync(long versionId, CancellationToken token)
        {
            return Task.Run(() => GetFullOrder(versionId));
        }

        /// <summary>
        /// Асинхронный метод получения полного заказа
        /// </summary>
        /// <param name="versionId">
        /// The version id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<IntermechTreeElement> GetFullOrderAsync(long versionId)
        {
            return GetFullOrderAsync(versionId, CancellationToken.None);
        }

        /// <summary>
        /// Метод получения заказа
        /// </summary>
        /// <param name="versionId">
        /// Id версии заказа
        /// </param>
        /// <returns>
        /// The <see cref="IntermechTreeElement"/>.
        /// </returns>
        public IntermechTreeElement GetFullOrder(long versionId)
        {
            IDictionary<long, IntermechTreeElement> downloadedParts = new Dictionary<long, IntermechTreeElement>();
            IntermechTreeElement orderElement = new IntermechTreeElement();
            string name;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                var orderObject = keeper.Session.GetObject(versionId);
                name = orderObject.Caption;

                orderElement.Id = versionId;
                orderElement.ObjectId = orderObject.ID;
                orderElement.Name = name;
            }

            orderElement.Amount = 1;
            orderElement.AmountWithUse = 1;
            orderElement.StockRate = 1;
            orderElement.TotalAmount = orderElement.StockRate * orderElement.AmountWithUse;

            // загрузка всего остального дерева
            FetchNodeRecursive(orderElement, downloadedParts);

            // расчет применяемостей
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(orderElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                IntermechTreeElement parent = elementFromQueue.Parent;
                if (parent != null)
                {
                    elementFromQueue.AmountWithUse = parent.AmountWithUse * elementFromQueue.Amount;
                    elementFromQueue.TotalAmount = elementFromQueue.AmountWithUse * elementFromQueue.StockRate;
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return orderElement;
        }

        /// <summary>
        /// Метод получения состава изделия по его идентификатору версии объекта
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection{IntermechTreeElement}"/>.
        /// </returns>
        public ICollection<IntermechTreeElement> Read(long id)
        {
            ICollection<IntermechTreeElement> elements = new List<IntermechTreeElement>();
            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                // Получим конструкторский состав на сборку
                // Необходимые колонки
                ColumnDescriptor[] columns =
                {
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(-20, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0), // Id связи 
                    new ColumnDescriptor(9, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // обозначение
                    new ColumnDescriptor(10, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // наименование
                    new ColumnDescriptor(1129, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // количество
                    new ColumnDescriptor(1130, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер группы заменителей
                    new ColumnDescriptor(1131, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер в группе заменителей
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Идентификатор объекта
                    new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // номер изменения
                    new ColumnDescriptor(4100, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // позиционное обозначение
                    new ColumnDescriptor(1132, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // позиция
                    new ColumnDescriptor(12691, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Поставщик
                    new ColumnDescriptor(1098, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Класс
                    new ColumnDescriptor(17784, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // PartNumber
                    new ColumnDescriptor(17765, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Тип корпуса
                    new ColumnDescriptor(18079, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Флаг печатной платы
                    new ColumnDescriptor(17965, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Версия печатной платы
                };

                // Поиск состава
                // сборки, комплекты, детали
                DataTable articlesCmposition = compositionService.LoadComposition(keeper.Session.SessionGUID, id, 1, new List<ColumnDescriptor>(columns), string.Empty, 1074, 1078, 1052, 1128, 1138, 1105, 1097);

                // по связи деталь-заготовка
                DataTable detailBlank = compositionService.LoadComposition(keeper.Session.SessionGUID, id, 1038, new List<ColumnDescriptor>(columns), string.Empty, 1074, 1078, 1052, 1128, 1138, 1105, 1097);

                // если есть изделия-заготовки, то не будем их добавлять в состав. Надо найти деталь, заготовкой которой они являются и перенести в него количество
                IList<IntermechTreeElement> elementsForDetails = new List<IntermechTreeElement>();
                if (detailBlank.Rows.Count > 0)
                {
                    foreach (DataRow row in detailBlank.Rows)
                    {
                        IntermechTreeElement element = CreateNewElement(row, keeper.Session, elementsForDetails);
                        element.MeasureUnits = "шт";
                        elementsForDetails.Add(element);
                    }    
                }

                foreach (DataRow row in articlesCmposition.Rows)
                {
                    IntermechTreeElement element = CreateNewElement(row, keeper.Session, elementsForDetails);

                    // если сборка или комплект, то смотрим их состав документации. По спецификации определяем номер изменения
                    if (element.Type == 1078 || element.Type == 1074 || element.Type == 1052 || element.Type == 1097)
                    {
                        // Необходимые колонки
                        ColumnDescriptor[] specificationColumns =
                        {
                            new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор объекта
                            new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                            new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер последнего изменения
                        };

                        DataTable docComposition = compositionService.LoadComposition(keeper.Session.SessionGUID, Convert.ToInt64(element.Id), 1004, new List<ColumnDescriptor>(specificationColumns), string.Empty, 1259, 1682);

                        if (docComposition.Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in docComposition.Rows)
                            {
                                int type = Convert.ToInt32(dataRow[1]);
                                switch (type)
                                {
                                    case 1259:
                                        if (dataRow[2] != DBNull.Value)
                                        {
                                            element.ChangeNumber = Convert.ToString(dataRow[2]);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    elements.Add(element);
                }

                ISubsituteGroupGrabber grabber = new SubsituteGroupGrabber(elements);                    
                grabber.GetGroups();                    
                ICollection<SubstituteGroup> groups = grabber.SubGroups;             
                   
                foreach (SubstituteGroup group in groups)                   
                {
                    _substituteDecriptorService.DecriptSub(group);                 
                } 
            }

            return elements;
        }


        /// <summary>
        /// Асинхронное получение контрагентов
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Agent>> GetAllAgentsAsync()
        {
            ICollection<Agent> agents = new List<Agent>();
            Func<ICollection<Agent>> myFuncDelegate = () =>
            {
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObjectCollection collection = keeper.Session.GetObjectCollection(1604);
                    DBRecordSetParams pars = new DBRecordSetParams(null, new object[] { -2, 10 });
                    DataTable workshopsTable = collection.Select(pars);

                    foreach (DataRow row in workshopsTable.Rows)
                    {
                        long id = Convert.ToInt64(row[0]);
                        string name = Convert.ToString(row[1]);
                        Agent agent = new Agent
                        {
                            Id = id,
                            Name = name
                        };
                        agents.Add(agent);
                    }
                }
                return agents;
            };
            return await Task.Run(myFuncDelegate).ConfigureAwait(false);
        }

        public ICollection<Agent> GetAgents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get dataset.
        /// </summary>
        /// <param name="versionId"></param>
        /// <param name="fileAttributeId"></param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        public IntermechTreeElement GetDataFromFile(long versionId, int fileAttributeId)
        {
            // получаем из базы
            byte[] bytes = null;
            bool isDataset = false;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject orderObject = keeper.Session.GetObject(versionId);
                IDBAttribute fileAttribute = orderObject.GetAttributeByID(fileAttributeId);

                if (fileAttribute != null)
                {
                    IBlobReader blobReader = fileAttribute as IBlobReader;
                    blobReader.OpenBlob(0);
                    bytes = blobReader.ReadDataBlock();
                    blobReader.CloseBlob();

                    if (((string)fileAttribute.Value).Contains(".blb"))
                    {
                        isDataset = true;
                    }
                }
            }

            if (bytes == null)
            {
                throw new FileAttributeIsEmptyException("К заказу еще не прикреплен атрибут Файл");
            }

            // распаковываем
            Inflater inflater = new Inflater();
            byte[] temp = new byte[1024];
            byte[] unpackedBytes = null;
            using (MemoryStream memory = new MemoryStream())
            {
                inflater.SetInput(bytes);
                while (!inflater.IsFinished)
                {
                    int extracted = inflater.Inflate(temp);
                    if (extracted > 0)
                    {
                        memory.Write(temp, 0, extracted);
                    }
                    else
                    {
                        break;
                    }
                }
                unpackedBytes = memory.ToArray();
            }


            IntermechTreeElement root = null;
            using (MemoryStream ms = new MemoryStream(unpackedBytes))
            {
                if (!isDataset)
                {
                    // десериализуем
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    using (JsonReader jsonReader = new BsonReader(ms))
                    {
                        root = (IntermechTreeElement)jsonSerializer.Deserialize(jsonReader, typeof(IntermechTreeElement));
                    }
                }
                else
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    DataSet ds = (DataSet)binaryFormatter.Deserialize(ms);
                    TreeBuilderService treeBuilderService = new TreeBuilderService();
                    root = treeBuilderService.Build(ds);
                }
            }
            return root;
        }

        public DataSet GetDataset(long orderId, int dataAttributeId)
        {
            // получаем из базы
            byte[] bytes = null;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject orderObject = keeper.Session.GetObject(orderId);
                IDBAttribute binaryAtt = orderObject.GetAttributeByID((int)dataAttributeId);

                if (binaryAtt != null)
                {
                    IBlobReader blobReader = binaryAtt as IBlobReader;
                    blobReader.OpenBlob(0);
                    bytes = blobReader.ReadDataBlock();
                    blobReader.CloseBlob();
                }
            }

            if (bytes == null)
            {
                throw new FileAttributeIsEmptyException("Атрибута нет, поэтому нет и данных");
            }

            // распаковываем
            Inflater inflater = new Inflater();
            byte[] temp = new byte[1024];
            byte[] unpackedBytes = null;
            using (MemoryStream memory = new MemoryStream())
            {
                inflater.SetInput(bytes);
                while (!inflater.IsFinished)
                {
                    int extracted = inflater.Inflate(temp);
                    if (extracted > 0)
                    {
                        memory.Write(temp, 0, extracted);
                    }
                    else
                    {
                        break;
                    }
                }

                unpackedBytes = memory.ToArray();
            }

            // пакуем в Dataset
            BinaryFormatter binFormatter = new BinaryFormatter();
            DataSet dataSet;
            using (MemoryStream ms = new MemoryStream(unpackedBytes))
            {
                dataSet = (DataSet)binFormatter.Deserialize(ms);
            }
            return dataSet;
        }

        public Task<DataSet> GetDatasetAsync(long orderId, int dataAttributeId)
        {
            return Task.Run<DataSet>(() =>
            {
                return GetDataset(orderId, dataAttributeId);
            });
        }

        public Task<IntermechTreeElement> GetDataFromFileAsync(long versionId, int dataAttributeId)
        {
            return Task.Run<IntermechTreeElement>(() =>
            {
                return GetDataFromFile(versionId, dataAttributeId);
            });
        }

        public Task<T> GetDataFromBinaryAttributeAsync<T>(long versionId, int dataAttributeId) where T:class
        {
            return Task.Run<T>(() => { return GetDataFromBinaryAttribute<T>(versionId, dataAttributeId); });
        }

        public T GetDataFromBinaryAttribute<T>(long versionId, int dataAttributeId) where T:class
        {
            // получаем из базы
            byte[] bytes = null;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject orderObject = keeper.Session.GetObject(versionId);
                IDBAttribute binaryAtt = orderObject.GetAttributeByID(dataAttributeId);

                if (binaryAtt != null)
                {
                    IBlobReader blobReader = binaryAtt as IBlobReader;
                    blobReader.OpenBlob(0);
                    bytes = blobReader.ReadDataBlock();
                    blobReader.CloseBlob();
                }
            }

            // распаковываем
            Inflater inflater = new Inflater();
            byte[] temp = new byte[1024];
            byte[] unpackedBytes = null;
            using (MemoryStream memory = new MemoryStream())
            {
                inflater.SetInput(bytes);
                while (!inflater.IsFinished)
                {
                    int extracted = inflater.Inflate(temp);
                    if (extracted > 0)
                    {
                        memory.Write(temp, 0, extracted);
                    }
                    else
                    {
                        break;
                    }
                }

                unpackedBytes = memory.ToArray();
            }

            // пакуем в Dataset
            BinaryFormatter binFormatter = new BinaryFormatter();
            T deserializedObject;
            using (MemoryStream ms = new MemoryStream(unpackedBytes))
            {
                deserializedObject = (T)binFormatter.Deserialize(ms);
            }
            return deserializedObject;
        }


        /// <summary>
        /// Получает из IPS данные по запрошенному тех. отходу
        /// </summary>
        /// <returns>
        /// The <see cref="WithdrawalType"/>.
        /// </returns>
        public WithdrawalType GetWithdrawalTypes()
        {
            WithdrawalType mainNode = new WithdrawalType();
            mainNode.Id = 1440207;
            mainNode.Description = "Типы тех. отхода";
            Queue<WithdrawalType> queue = new Queue<WithdrawalType>();
            queue.Enqueue(mainNode);

            while (queue.Count > 0)
            {
                WithdrawalType nodeFromQueue = queue.Dequeue();
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject myObject = keeper.Session.GetObject(nodeFromQueue.Id);

                    // Сервис для получения составов
                    ICompositionLoadService compositionService =
                        (ICompositionLoadService)keeper.Session.GetCustomService(
                            typeof(ICompositionLoadService));

                    // Получим состав по связи Простая связь с сортировкой
                    // Необходимые колонки
                    ColumnDescriptor[] columns =
                    {
                        new ColumnDescriptor(
                            (int)ObligatoryObjectAttributes.F_OBJECT_ID,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // идентификатор версии объекта
                        new ColumnDescriptor(
                            (int)ObligatoryObjectAttributes.F_OBJECT_TYPE,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // тип объекта
                        new ColumnDescriptor(10,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // наименование
                        new ColumnDescriptor(11,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // примечание
                        new ColumnDescriptor(17727,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // значение-величина
                        new ColumnDescriptor(1317,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // идентификатор  tag

                    };

                    // забрать содержимое папки Imbase
                    DataTable composition = compositionService.LoadComposition(
                        keeper.Session.SessionGUID,
                        myObject.ObjectID,
                        1003,
                        new List<ColumnDescriptor>(columns),
                        string.Empty,
                        1095);


                    foreach (DataRow row in composition.Rows)
                    {
                        WithdrawalType withdrawalType = new WithdrawalType();
                        withdrawalType.Id = Convert.ToInt64(row[0]);
                        if (row[5] != DBNull.Value)
                        {
                            withdrawalType.Type = Convert.ToByte(row[5]);
                        }

                        if (row[2] != DBNull.Value)
                        {
                            withdrawalType.Name= Convert.ToString(row[2]);
                        }

                        if (row[3] != DBNull.Value)
                        {
                            withdrawalType.Description = (string)row[3];
                        }

                        if (row[4] != DBNull.Value)
                        {
                            withdrawalType.Value = (string)row[4];
                        }

                        nodeFromQueue.Add(withdrawalType);
                    }

                    foreach (WithdrawalType child in nodeFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            return mainNode;

        }


        /// <summary>
        /// Асинхронно получает из IPS данные по запрошенному тех. отходу
        /// </summary>
        /// <returns>
        /// Набор типов тех. отходов
        /// </returns>
        public Task<WithdrawalType> GetWithdrawalTypesAsync()
        {
            Func<WithdrawalType> funcDelegate = () => { return GetWithdrawalTypes(); };
            return Task.Run(funcDelegate);
        }

        /// <summary>
        /// Асинхронно получает цеха из IMBase
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<TechRouteNode> GetWorkshopsAsync()
        {
            Func<TechRouteNode> funcDelegate = () =>
            {
                TechRouteNode mainNode = new TechRouteNode();
                mainNode.Id = 2512;
                mainNode.Name = "Цеха";
                Queue<TechRouteNode> queue = new Queue<TechRouteNode>();
                queue.Enqueue(mainNode);

                while (queue.Count > 0)
                {
                    TechRouteNode nodeFromQueue = queue.Dequeue();

                    using (SessionKeeper keeper = new SessionKeeper())
                    {
                        IDBObject myObject = keeper.Session.GetObject(nodeFromQueue.Id);

                        // Сервис для получения составов
                        ICompositionLoadService compositionService =
                            (ICompositionLoadService)keeper.Session.GetCustomService(
                                typeof(ICompositionLoadService));

                        // Получим состав по связи Простая связь с сортировкой
                        // Необходимые колонки
                        ColumnDescriptor[] columns =
                        {
                            new ColumnDescriptor(
                                (int)ObligatoryObjectAttributes.F_OBJECT_ID,
                                AttributeSourceTypes.Object,
                                ColumnContents.Text,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // идентификатор версии объекта
                            new ColumnDescriptor(
                                (int)ObligatoryObjectAttributes.F_OBJECT_TYPE,
                                AttributeSourceTypes.Object,
                                ColumnContents.Text,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // тип объекта
                            new ColumnDescriptor(9,
                                AttributeSourceTypes.Object,
                                ColumnContents.Text,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // обозначение
                            new ColumnDescriptor(10,
                                AttributeSourceTypes.Object,
                                ColumnContents.Text,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // наименование
                            new ColumnDescriptor(1190,
                                AttributeSourceTypes.Object,
                                ColumnContents.Text,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // цех
                            new ColumnDescriptor(1194,
                                AttributeSourceTypes.Object,
                                ColumnContents.Text,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // участок

                            new ColumnDescriptor(1190,
                                AttributeSourceTypes.Object,
                                ColumnContents.ID,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // цех

                            new ColumnDescriptor(1194,
                                AttributeSourceTypes.Object,
                                ColumnContents.ID,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // участок

                        };


                        DataTable articlesComposition = compositionService.LoadComposition(
                            keeper.Session.SessionGUID,
                            myObject.ObjectID,
                            1003,
                            new List<ColumnDescriptor>(columns),
                            string.Empty,
                            1095);

                        foreach (DataRow row in articlesComposition.Rows)
                        {
                            TechRouteNode node = new TechRouteNode();
                            node.Id = (long)row[0];
                            node.Type = (int)row[1];
                            node.Name = (string)row[3];
                            if (row[4] == DBNull.Value)
                            {
                                node.WorkshopName = string.Empty;
                            }
                            else
                            {
                                node.WorkshopName = (string)row[4];
                            }

                            if (row[5] == DBNull.Value)
                            {
                                node.PartitionName = string.Empty;
                            }
                            else
                            {
                                node.PartitionName = (string)row[5];
                            }

                            node.WorkshopId = row[6] == DBNull.Value ? 0 : (long)row[6];
                            node.PartitionId = row[7] == DBNull.Value ? 0 : (long)row[7];

                            nodeFromQueue.Add(node);
                        }

                        foreach (TechRouteNode child in nodeFromQueue.Children)
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
                return mainNode;
            };

            return await Task.Run(funcDelegate).ConfigureAwait(false);
        }



 
        /// <summary>
        /// Метод получает коллекцию документов на изделие, которые затем можно просмотреть
        /// </summary>
        /// <param name="id">Идентификатор изделия</param>
        /// <returns>Возвращает коллекцию прикрепленных документов</returns>
        public ICollection<Document> GetDocuments(long id)
        {
            ICollection<Document> elements = new List<Document>();

            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                // Получим конструкторский состав на сборку
                // Необходимые колонки
                ColumnDescriptor[] columns =
                {
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор объекта
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(9, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index,
                        SortOrders.NONE, 0), // 
                    new ColumnDescriptor(10, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index,
                        SortOrders.NONE, 0), //
                };


                //DataTable docs = compositionService.LoadComposition(keeper.Session.SessionGUID,
                //    id, 1004, new List<ColumnDescriptor>(columns), string.Empty, 1160, 1247);


                // Поиск состава
                // сборки, комплекты, детали
                DataTable docs = compositionService.LoadComposition(keeper.Session.SessionGUID,
                    id, 1004, new List<ColumnDescriptor>(columns), string.Empty, 1247);

                foreach (DataRow row in docs.Rows)
                {
                    Document doc = new Document();
                    doc.Id = Convert.ToInt64(row[0]);
                    doc.Type = Convert.ToInt32(row[1]);
                    if (row[2] != DBNull.Value)
                    {
                        doc.Designation = Convert.ToString(row[2]);
                    }

                    if (row[3] != DBNull.Value)
                    {
                        doc.Name = Convert.ToString(row[3]);
                    }

                    elements.Add(doc);
                }


            }

            return elements;
        }

        /// <summary>
        /// Рекурсивная загрузка заказа
        /// </summary>
        /// <param name="elementToFetch">
        /// Элемент для загрузки
        /// </param>
        /// <param name="fetchedElements">
        /// Зарегистрированные уже загруженные элементы
        /// </param>
        private void FetchNodeRecursive(IntermechTreeElement elementToFetch, IDictionary<long, IntermechTreeElement> fetchedElements)
        {
            // читаем состав
            IEnumerable<IntermechTreeElement> elements = null;
            if (fetchedElements.ContainsKey(elementToFetch.Id))
            {
                IntermechTreeElement alreadyDownloadedElement = (IntermechTreeElement)fetchedElements[elementToFetch.Id].Clone();
                elements = alreadyDownloadedElement.Children.Cast<IntermechTreeElement>();
                
                // проходим по элементам
                foreach (IntermechTreeElement element in elements)
                {
                    elementToFetch.Add(element);
                }

                return;
            }

            // читаем состав
            elements = Read(elementToFetch.Id);
                
            IDictionary<long, IntermechTreeElement> uniqueElements = new Dictionary<long, IntermechTreeElement>();

            // сжимаем повторяющиеся элементы
            foreach (IntermechTreeElement element in elements)
            {
                if (uniqueElements.ContainsKey(element.Id))
                {
                    IntermechTreeElement registeredElement = uniqueElements[element.Id];
                    registeredElement.Amount += element.Amount;
                    registeredElement.Position += ", " + element.Position;
                    registeredElement.PositionDesignation += ", " + element.PositionDesignation;
                }
                else
                {
                    uniqueElements.Add(element.Id, element);
                }
            }

            elements = uniqueElements.Values;
                
            // зарегистрировали, что скачали элемент
            fetchedElements.Add(elementToFetch.Id, elementToFetch);


            // проходим по элементам
            foreach (IntermechTreeElement element in elements)
            {
                elementToFetch.Add(element);

                // если у объекта есть состав, то спускаемся рекурсивно
                if (element.Type == 1019 || element.Type == 1078 || element.Type == 1074 || element.Type == 1097)
                {
                    FetchNodeRecursive(element, fetchedElements);
                }
            }
        }

        /// <summary>
        /// Создание нового элемента из загруженной из IPS таблицы
        /// </summary>
        /// <param name="row">
        /// Строка таблицы
        /// </param>
        /// <param name="session">
        /// Текущая сессия
        /// </param>
        /// <param name="elementsForDetails">
        /// Набор элементов деталей-заготовок
        /// </param>
        /// <returns>
        /// The <see cref="IntermechTreeElement"/>.
        /// </returns>
        private IntermechTreeElement CreateNewElement(DataRow row, IUserSession session, IList<IntermechTreeElement> elementsForDetails)
        {
            IntermechTreeElement element = new IntermechTreeElement()
            {
                Id = Convert.ToInt64(row[0]),
                ObjectId = Convert.ToInt64(row[8]),
                Type = Convert.ToInt32(row[1]),
                RelationId = Convert.ToInt64(row[2]),
                Designation = Convert.ToString(row[3]),
                Name = Convert.ToString(row[4]),
                StockRate = 1,
                ChangeNumber = Convert.ToString(row[9])

            };

            if (row[5] != DBNull.Value)
            {
                string amount = Convert.ToString(row[5]);
                string[] amountSplit = amount.Split(' ');
                element.Amount = Convert.ToSingle(amountSplit[0]);
                element.MeasureUnits = amountSplit[1];
            }
            else
            {
                element.Amount = 0;
            }

            if (row[6] != DBNull.Value)
            {
                element.SubstituteGroupNumber = Convert.ToInt32(row[6]);
            }

            if (row[7] != DBNull.Value)
            {
                element.SubstituteNumberInGroup = Convert.ToInt32(row[7]);
            }

            if (row[10] != DBNull.Value)
            {
                element.PositionDesignation = Convert.ToString(row[10]);
            }

            if (row[11] != DBNull.Value)
            {
                element.Position = Convert.ToString(row[11]);
            }

            if (row[12] != DBNull.Value)
            {
                element.Supplier = Convert.ToString(row[12]);
            }

            if (row[13] != DBNull.Value)
            {
                element.Class = Convert.ToString(row[13]);
            }

            if (row[14] != DBNull.Value)
            {
                element.PartNumber = Convert.ToString(row[14]);
            }

            if (row[15] != DBNull.Value)
            {
                element.Case = Convert.ToString(row[15]);
            }

            if (row[16] != DBNull.Value)
            {
                element.IsPCB = Convert.ToByte(row[16]) == 1;
            }

            if (row[17] != DBNull.Value)
            {
                element.PcbVersion = Convert.ToByte(row[17]);
            }

            if (element.IsPCB)
            {
                IDBObject currentObject = session.GetObject(element.Id);
                IDBAttribute techTaskOnPcbAttribute = currentObject.GetAttributeByID(18086);
                if (techTaskOnPcbAttribute != null)
                {
                    char[] textBytes = null;
                    IMemoReader memoReader = techTaskOnPcbAttribute as IMemoReader;
                    memoReader.OpenMemo(0);
                    textBytes = memoReader.ReadDataBlock();
                    memoReader.CloseMemo();
                    element.TechTask = new string(textBytes);
                }
            }


            if (element.Type == 1052 || element.Type == 1159)
            {
                IDBObject detailObject = session.GetObject(element.Id);
                IDBAttribute materialAttribute = detailObject.GetAttributeByID(1181);
                if (materialAttribute != null)
                {
                    long materialId = materialAttribute.AsInteger;
                            
                    if (materialId != ConstHelper.MaterialZero && materialId != (int)ConstHelper.MaterialNotDefined) 
                    {
                        IDBObject materialObject = session.GetObject(materialId);

                        if (materialObject != null)
                        {
                            // забрать единицы измерения
                            IDBAttribute unitsAttribute = materialObject.GetAttributeByID(1254);
                            IntermechTreeElement detailMaterialNode = new IntermechTreeElement()
                            {
                                Id = materialObject.ObjectID,
                                ObjectId = materialObject.ID,
                                Name = materialObject.Caption,
                                Type = materialObject.TypeID,
                                StockRate = 1
                            };

                            // есть совпадение с изделием-заготовкой
                            foreach (IntermechTreeElement elementForDetail in elementsForDetails)
                            {
                                if (detailMaterialNode.Id == elementForDetail.Id)
                                {
                                    detailMaterialNode.Amount = elementForDetail.Amount;
                                    detailMaterialNode.MeasureUnits = elementForDetail.MeasureUnits;
                                    break;
                                }
                            }

                            string units = string.Empty;
                            if (unitsAttribute != null)
                            {
                                // надо теперь выцепить по атрибуту ед. измерения сам объект единиц измерения
                                IDBObject unitsObject = session.GetObject(unitsAttribute.AsInteger, false);
                                if (unitsObject != null)
                                {
                                    IDBAttribute shortNameUnit = unitsObject.GetAttributeByID(13);
                                    detailMaterialNode.MeasureUnits = shortNameUnit.AsString;
                                    units = shortNameUnit.AsString;
                                }
                            }

                            // Количество материала на единицу изделия
                            IDBAttribute amountOnOneUnitOfProduct = detailObject.GetAttributeByID(18087);
                            if (amountOnOneUnitOfProduct != null)
                            {
                                if (amountOnOneUnitOfProduct.AsString.Contains(units))
                                {
                                    detailMaterialNode.Amount = (float)amountOnOneUnitOfProduct.AsDouble;
                                }
                                else
                                {
                                    throw new MaterialUnitsNotEqualException(string.Format("В детали {0} единицы измерения, указанные в атрибуте Количество на единицу изделия, не совпадают с единицами измерения материала по умолчанию", element.Designation));
                                }
                            }

                            element.Add(detailMaterialNode);
                        }
                    }
                }
            }

            return element;
        }

    }
}
