// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntermechReader.cs" company="NavisElectronics">
//   Cherkashin I.V.
// </copyright>
// <summary>
//   Реализует чтение составов отдельных элементов, целого заказа
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Interfaces.Helpers;

namespace NavisElectronics.TechPreparation.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using Exceptions;
    using ICSharpCode.SharpZipLib.Zip.Compression;
    using Interfaces;
    using Interfaces.Entities;
    using Interfaces.Services;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Kernel.Search;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;
    using Substitutes;

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
            return Task.Run(() => GetFullOrder(versionId, token));
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
        /// Метод получения полного заказа
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии заказа
        /// </param>
        /// <returns>
        /// The <see cref="IntermechTreeElement"/>.
        /// </returns>
        public IntermechTreeElement GetFullOrder(long versionId)
        {
            return GetFullOrder(versionId, CancellationToken.None);
        }

        /// <summary>
        /// Метод чтения данных с IPS по идентификатору весии объекта
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection{IntermechTreeElement}"/>.
        /// </returns>
        public ICollection<IntermechTreeElement> Read(long id)
        {
            return Read(id, CancellationToken.None);
        }

        /// <summary>
        /// Метод получения заказа
        /// </summary>
        /// <param name="versionId">
        /// Id версии заказа
        /// </param>
        /// <param name="token">
        /// Токен отмены
        /// </param>
        /// <returns>
        /// The <see cref="IntermechTreeElement"/>.
        /// </returns>
        private IntermechTreeElement GetFullOrder(long versionId, CancellationToken token)
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
            orderElement.UseAmount = 1;
            orderElement.AmountWithUse = orderElement.Amount * orderElement.UseAmount;
            orderElement.StockRate = 1;


            orderElement.TotalAmount = orderElement.StockRate * orderElement.AmountWithUse;

            // загрузка всего остального дерева
            FetchNodeRecursive(orderElement, downloadedParts, token);

            RecountAmountInTree(orderElement);

            return orderElement;
        }



        /// <summary>
        /// Метод получения состава изделия по его идентификатору версии объекта
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="token">
        /// Токен отмены
        /// </param>
        /// <returns>
        /// The <see cref="ICollection{IntermechTreeElement}"/>.
        /// </returns>
        private ICollection<IntermechTreeElement> Read(long id, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            ICollection<IntermechTreeElement> elements = new List<IntermechTreeElement>();
            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));


                ColumnDescriptor[] columnsForDocuments =
                {
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(-20, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0), // Id связи 
                    new ColumnDescriptor(9, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // обозначение
                    new ColumnDescriptor(10, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // наименование
                    new ColumnDescriptor(1129, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // количество
                    new ColumnDescriptor(1177, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // первичная применяемость
                    new ColumnDescriptor(1145, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // литера
                    new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // номер изменения
                    new ColumnDescriptor(11, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание к самому объекту
                    new ColumnDescriptor(11, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание по связи
                    new ColumnDescriptor(17947, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание к составу
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Идентификатор объекта
                    new ColumnDescriptor(-15, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // код извещения
                };

                // Поиск состава
                DataTable documents = compositionService.LoadComposition(keeper.Session, id, 1004, new List<ColumnDescriptor>(columnsForDocuments), string.Empty); // забрать всю документацию

                foreach (DataRow row in documents.Rows)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 9; i <= 10; i++)
                    {
                        if (row[i] != DBNull.Value)
                        {
                            string convertedString = Convert.ToString(row[i]);
                            if (convertedString != string.Empty)
                            {
                                sb.Append(convertedString + " ");
                            }
                        }
                    }

                    IntermechTreeElement document = new IntermechTreeElementBuilder()
                        .SetId(row[0])
                        .SetType(row[1])
                        .SetRelationId(row[2])
                        .SetDesignation(row[3])
                        .SetName(row[4])
                        .SetAmount(row[5])
                        .SetFirstUse(row[6])
                        .SetLetter(row[7])
                        .SetChangeNumber(row[8])
                        .SetNote(row[9])
                        .SetRelationNote(sb.ToString().TrimEnd()).SetObjectId(row[12])
                        .SetChangeDocument(row[13]);
                    document.RelationName = "Документ";



                    elements.Add(document);
                }


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
                    new ColumnDescriptor(17887, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип монтажа компонента
                    new ColumnDescriptor(11, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание
                    new ColumnDescriptor(11, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание по связи "Состав изделия"
                    new ColumnDescriptor(-15, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // код извещения
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
                        IntermechTreeElement element = CreateNewElement(row, keeper.Session, elementsForDetails,id);
                        element.MeasureUnits = "шт";
                        elementsForDetails.Add(element);
                    }    
                }

                ColumnDescriptor[] columnsForPickedRelation =
                {
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(-20, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0), // Id связи 
                    new ColumnDescriptor(9, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // обозначение
                    new ColumnDescriptor(10, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // наименование
                    new ColumnDescriptor(1473, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // количество на регулировку
                    new ColumnDescriptor(18028, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // подбор для позиционного обозначения
                    new ColumnDescriptor(11, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание по связи
                    new ColumnDescriptor(17995, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание ПЭ
                    new ColumnDescriptor(17765, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Тип корпуса
                    new ColumnDescriptor(1098, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Класс
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Идентификатор объекта
                };

                // по связи Подборной компонент
                DataTable pickedElements = compositionService.LoadComposition(keeper.Session.SessionGUID, id, 1056, new List<ColumnDescriptor>(columnsForPickedRelation), string.Empty, 1074, 1078, 1052, 1128, 1138, 1105, 1097);

                foreach (DataRow row in pickedElements.Rows)
                {
                    IntermechTreeElement pickedElement = new IntermechTreeElementBuilder()
                        .SetId(row[0])
                        .SetType(row[1])
                        .SetRelationId(row[2])
                        .SetDesignation(row[3])
                        .SetName(row[4])
                        .SetAmount(row[5])
                        .SetPositionDesignation(row[6])
                        .SetNote(row[7])
                        .SetRelationNote(row[8])
                        .SetCase(row[9])
                        .SetClass(row[10])
                        .SetObjectId(row[11]);
                    pickedElement.RelationName = "Подборной элемент";
                    elements.Add(pickedElement);
                }


                foreach (DataRow row in articlesCmposition.Rows)
                {
                    IntermechTreeElement element = CreateNewElement(row, keeper.Session, elementsForDetails, id);

                    // если сборка или комплект, то смотрим их состав документации. По спецификации определяем номер изменения
                    if (element.Type == 1078 || element.Type == 1074 || element.Type == 1052 || element.Type == 1097)
                    {
                        // Необходимые колонки
                        ColumnDescriptor[] specificationColumns =
                        {
                            new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор объекта
                            new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                            new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер последнего изменения
                            new ColumnDescriptor(-15, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // код извещения
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
                                            IntermechTreeElement tempElement = new IntermechTreeElement() { Id = Convert.ToInt64(dataRow[0]) };
                                            SetChangeDocumentName(tempElement, dataRow[3]);
                                            element.ChangeDocument = tempElement.ChangeDocument;
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
        /// Получить Dataset
        /// </summary>
        /// <param name="versionId">Идентификатор версии объекта</param>
        /// <param name="fileAttributeId">файловый атрибут</param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// Dataset
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

            // расчет применяемостей
            RecountAmountInTree(root);
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
                                0), // код цеха

                            new ColumnDescriptor(1194,
                                AttributeSourceTypes.Object,
                                ColumnContents.ID,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // код участка
                            new ColumnDescriptor(11101,
                                AttributeSourceTypes.Object,
                                ColumnContents.Text,
                                ColumnNameMapping.Index,
                                SortOrders.NONE,
                                0), // производитель

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

                            node.ManufacturerId = row[8] == DBNull.Value ? 0 : Convert.ToInt64(row[8]);

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

        public Task<IntermechTreeElement> GetElementDataAsync(long versionId)
        {
            Func<IntermechTreeElement> func = () =>
            {
                IntermechTreeElement elementToReturn = null;
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject objectToGet = keeper.Session.GetObject(versionId,true);

                    elementToReturn = new IntermechTreeElement();

                    IDBAttribute nameAttribute = objectToGet.GetAttributeByID(10);
                    elementToReturn.Name = nameAttribute.AsString;

                    IDBAttribute noteAttribute = objectToGet.GetAttributeByID(11);
                    elementToReturn.Note = noteAttribute.AsString;

                    IDBAttribute pcbAttribute = objectToGet.GetAttributeByID(18079);
                    if (pcbAttribute != null)
                    {
                        elementToReturn.IsPcb = pcbAttribute.AsInteger == 1;
                    }

                    if (elementToReturn.IsPcb)
                    {
                        IDBAttribute pcbVersionAttribute = objectToGet.GetAttributeByID(17965);
                        if (pcbVersionAttribute != null)
                        {
                            elementToReturn.PcbVersion = (byte)pcbVersionAttribute.AsInteger;
                        }

                        IDBAttribute pcbTechTaskAttribute = objectToGet.GetAttributeByID(18086);
                        if (pcbTechTaskAttribute != null)
                        {
                            char[] textBytes = null;
                            IMemoReader memoReader = pcbTechTaskAttribute as IMemoReader;
                            memoReader.OpenMemo(0);
                            textBytes = memoReader.ReadDataBlock();
                            memoReader.CloseMemo();
                            elementToReturn.TechTask = new string(textBytes);
                        }
                    }
                }

                return elementToReturn;
            };

            return Task.Run(func);
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
        private void FetchNodeRecursive(IntermechTreeElement elementToFetch, IDictionary<long, IntermechTreeElement> fetchedElements, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            
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
            elements = Read(elementToFetch.Id, token);
                
            IDictionary<string, IntermechTreeElement> uniqueElements = new Dictionary<string, IntermechTreeElement>();

            // сжимаем повторяющиеся элементы
            foreach (IntermechTreeElement element in elements)
            {
                string key = element.Id.ToString() + element.RelationName;
                if (uniqueElements.ContainsKey(key))
                {
                    IntermechTreeElement registeredElement = uniqueElements[key];
                    registeredElement.Amount += element.Amount;
                    registeredElement.Position += ", " + element.Position;
                    registeredElement.PositionDesignation += ", " + element.PositionDesignation;
                }
                else
                {
                    uniqueElements.Add(key, element);
                }
            }

            elements = uniqueElements.Values;
                
            // зарегистрировали, что скачали элемент
            fetchedElements.Add(elementToFetch.Id, elementToFetch);


            // проходим по элементам
            foreach (IntermechTreeElement element in elements)
            {
                // здесь пропускаем все элементы, которые стоят на ЖЦ Аннулировано
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject currentObject = keeper.Session.GetObject(element.Id);

                    IDBLifecycleStep lifeCycleStep = keeper.Session.GetLifecycleStep(currentObject.LCStep, element.Type);
                    element.LifeCycleStep = lifeCycleStep.LCName;

                    if (element.LifeCycleStep == "Аннулировано")
                    {
                        continue;
                    }
                }

                elementToFetch.Add(element);

                // если у объекта есть состав, то спускаемся рекурсивно
                if (element.Type == 1019 || element.Type == 1078 || element.Type == 1074 || element.Type == 1097)
                {
                    FetchNodeRecursive(element, fetchedElements, token);
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
        private IntermechTreeElement CreateNewElement(DataRow row, IUserSession session, IList<IntermechTreeElement> elementsForDetails, long parentId)
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
                ChangeNumber = Convert.ToString(row[9]),
                RelationName = "Состав изделия"
            };

            if (row[5] != DBNull.Value)
            {
                IDBRelation relation = session.GetRelation((long)row[2]);
                IDBAttribute amountAttribute = relation.GetAttributeByID(1129);
                if (amountAttribute == null)
                {
                    throw new FormatException(string.Format("Нет количеств у объекта {0} в составе объекта c идентификатором версии объекта",element.Name, parentId));
                }


                MeasuredValue currentValue = (MeasuredValue)amountAttribute.Value;
                element.Amount = (float)currentValue.Value;
                MeasureDescriptor measureDescriptor = MeasureHelper.FindDescriptor(currentValue.MeasureID);
                element.MeasureUnits = measureDescriptor.ShortName;

                // если мы получили единицу измерения в мм, то надо ее в метры перевести
                if (currentValue.MeasureID == 2806)
                {
                    element.Amount /= 1000;
                    element.MeasureUnits = "м";
                }
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
                element.IsPcb = Convert.ToByte(row[16]) == 1;
            }

            if (row[17] != DBNull.Value)
            {
                element.PcbVersion = Convert.ToByte(row[17]);
            }

            if (row[18] != DBNull.Value)
            {
                element.MountingType = Convert.ToString(row[18]);
            }

            if (row[19] != DBNull.Value)
            {
                element.Note = Convert.ToString(row[19]);
            }

            if (row[20] != DBNull.Value)
            {
                element.RelationNote = Convert.ToString(row[20]);
            }

            // если это печатная плата, то надо забрать тех. задание
            if (element.IsPcb)
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

            // если деталь или Б/Ч деталь
            if (element.Type == 1052 || element.Type == 1159)
            {
                IDBObject detailObject = session.GetObject(element.Id);

                SetChangeDocumentName(element, row[21]);

                IDBAttribute materialAttribute = detailObject.GetAttributeByID(1181);
                if (materialAttribute != null)
                {
                    long materialId = materialAttribute.AsInteger;
                     
                    // если это не пустой материал или не неопределенный, то заходим
                    if (materialId != ConstHelper.MaterialZero && materialId != (int)ConstHelper.MaterialNotDefined) 
                    {
                        IDBObject materialObject = session.GetObject(materialId);

                        if (materialObject != null)
                        {
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
                                    // всегда должна быть единица, всегда!
                                    detailMaterialNode.Amount = 1;
                                    detailMaterialNode.MeasureUnits = elementForDetail.MeasureUnits;
                                    detailMaterialNode.RelationName = "Изделие-заготовка";
                                    break;
                                }
                            }

                            // Количество материала на единицу изделия
                            IDBAttribute amountOnOneUnitOfProduct = detailObject.GetAttributeByID(18087);
                            if (amountOnOneUnitOfProduct != null)
                            {
                                MeasuredValue value = (MeasuredValue)amountOnOneUnitOfProduct.Value;
                                MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(value.MeasureID);
                                detailMaterialNode.Amount = (float)amountOnOneUnitOfProduct.AsDouble;
                                detailMaterialNode.MeasureUnits = descriptor.ShortName;
                            }

                            element.Add(detailMaterialNode);
                        }
                    }
                }
            }

            return element;
        }


        internal void RecountAmountInTree(IntermechTreeElement node)
        {
            // расчет применяемостей
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                IntermechTreeElement parent = elementFromQueue.Parent;
                if (parent != null)
                {
                    elementFromQueue.UseAmount = (int)Math.Round(parent.AmountWithUse, MidpointRounding.ToEven);
                    elementFromQueue.AmountWithUse = elementFromQueue.UseAmount * elementFromQueue.Amount;
                    elementFromQueue.TotalAmount = elementFromQueue.AmountWithUse * elementFromQueue.StockRate;
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private void SetChangeDocumentName(IntermechTreeElement element, object changeDocument)
        {
            long changeDocumentId = changeDocument == DBNull.Value ? 0 : Convert.ToInt64(changeDocument);
            if (changeDocumentId == 0)
            {
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject documentObject = keeper.Session.GetObject(element.Id);
                    IDBAttribute documentChangeName = documentObject.GetAttributeByID(17921);
                    if (documentChangeName != null)
                    {
                        element.ChangeDocument = documentChangeName.AsString;
                    }
                }
            }
            else
            {
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject documentObject = keeper.Session.GetObject(changeDocumentId);
                    element.ChangeDocument = documentObject.Caption;
                }
            }
        }
    }
}
