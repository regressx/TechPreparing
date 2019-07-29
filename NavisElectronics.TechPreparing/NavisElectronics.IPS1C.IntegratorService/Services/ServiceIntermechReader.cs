using NavisElectronics.TechPreparation.Enums;

namespace NavisElectronics.IPS1C.IntegratorService.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using Exceptions;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Kernel.Search;

    /// <summary>
    /// The service intermech reader.
    /// </summary>
    public class ServiceIntermechReader
    {        
        public Task<DataBaseProduct> GetFullOrderAsync(long versionId, string agentFilter, CancellationToken token)
        {
            return Task.Run(() => GetFullOrder(versionId, agentFilter));
        }

        public DataBaseProduct GetFullOrder(long versionId, string agentFilter)
        {

            if (agentFilter == null)
            {
                throw new ArgumentNullException("agentFilter");
            }

            if (versionId < 0)
            {
                throw new OrderInfoException("Идентификатор заказа должен быть положительным. Если Вы видите эту ошибку, значит на стороне IPS ведутся работы над заказом, и Вы не получите самых актуальных данных");
            }

            IDictionary<long, DataBaseProduct> downloadedParts = new Dictionary<long, DataBaseProduct>();
            DataBaseProduct orderElement = new DataBaseProduct();

            IDBObject orderObject = null;
            string name = string.Empty;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                orderObject = keeper.Session.GetObject(versionId);
                name = orderObject.Caption;
                orderElement.Id = versionId;
                orderElement.ObjectId = orderObject.ID;
                orderElement.Name = name;
            }

            orderElement.Amount = 1;
            Queue<DataBaseProduct> queue = new Queue<DataBaseProduct>();
            queue.Enqueue(orderElement);

            while (queue.Count > 0)
            {
                DataBaseProduct elementFromQueue = queue.Dequeue();
                ICollection<DataBaseProduct> allElements = null;

                if (!downloadedParts.ContainsKey(elementFromQueue.Id))
                {
                    int typeId = elementFromQueue.Type;

                    if (typeId == 1019 || typeId == 1078 || typeId == 1097 ||
                        typeId == 1074 || typeId == 0)
                    {
                        // получаем всё, что сидит в заказе
                        allElements = Read(elementFromQueue.Id);
                        IDictionary<long, DataBaseProduct> uniqElementsDictionary = new Dictionary<long, DataBaseProduct>();
                        foreach (DataBaseProduct element in allElements)
                        {
                            if (uniqElementsDictionary.ContainsKey(element.Id))
                            {
                                DataBaseProduct uniqElement = uniqElementsDictionary[element.Id];
                                uniqElement.Amount += element.Amount;
                                uniqElement.PositionDesignation +=
                                    string.Format(", {0}", elementFromQueue.PositionDesignation);
                                uniqElement.Position +=
                                    string.Format(", {0}", elementFromQueue.PositionInSpecification);
                            }
                            else
                            {
                                uniqElementsDictionary.Add(element.Id, element);
                            }
                        }
                            
                        // Регистрируем, что скачали
                        downloadedParts.Add(elementFromQueue.Id, elementFromQueue);
                        foreach (DataBaseProduct element in uniqElementsDictionary.Values)
                        {
                            if (downloadedParts.ContainsKey(element.Id))
                            {
                                elementFromQueue.Add(downloadedParts[element.Id]);
                            }
                            else
                            {
                                elementFromQueue.Add(element);
                            }
                        }

                        if (elementFromQueue.Products.Count > 0)
                        {
                            foreach (DataBaseProduct child in elementFromQueue.Products)
                            {
                                queue.Enqueue(child);
                            }
                        }
                    }
                }
                else
                {
                    DataBaseProduct alreadyDownloadedElement = downloadedParts[elementFromQueue.Id];
                    alreadyDownloadedElement.Amount = elementFromQueue.Amount;
                    DataBaseProduct parent = elementFromQueue.Parent;
                    parent.Remove(elementFromQueue);
                    parent.Add(alreadyDownloadedElement);
                }
            }

            return orderElement;
        }

        private ICollection<DataBaseProduct> Read(long versionId)
        {
            ICollection<DataBaseProduct> elements = new List<DataBaseProduct>();
            using (SessionKeeper keeper = new SessionKeeper())
            {
                ICompositionLoadService compositionService = (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));
                
                // Получим конструкторский состав на сборку
                // Необходимые колонки
                ColumnDescriptor[] columns = {
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(9, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0), // обозначение
                    new ColumnDescriptor(10, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // наименование
                    new ColumnDescriptor(1129, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Количество
                    new ColumnDescriptor(17784, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // PartNumber
                    new ColumnDescriptor(17840, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // По кооперации 
                    new ColumnDescriptor(1130, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер группы заменителей
                    new ColumnDescriptor(1131, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер в группе заменителей
                    new ColumnDescriptor(1132, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Позиция в спецификации
                    new ColumnDescriptor(4100, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Позицинное обозначение
                    new ColumnDescriptor(11, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Примечание
                    new ColumnDescriptor(12691, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Поставщик
                    new ColumnDescriptor(1098, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Класс
                    new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер последнего изменения
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор объекта
                };

                // Поиск состава по связи "состав документации"
                DataTable articlesCmposition = compositionService.LoadComposition(keeper.Session.SessionGUID, Convert.ToInt64(versionId), 1, new List<ColumnDescriptor>(columns), string.Empty, 1138, 1074, 1078, 1052, 1105, 1128); // прочие изделия, сборочные ед.,

                // Поиск состава по связи "Изделие-заготовка"
                DataTable detailBlank = compositionService.LoadComposition(keeper.Session.SessionGUID, Convert.ToInt64(versionId), 1036, new List<ColumnDescriptor>(columns), string.Empty, 1138, 1074, 1078, 1052, 1105, 1128);

                // Если состава нет - выходим
                if (articlesCmposition.Rows.Count == 0)
                {
                    return new List<DataBaseProduct>();
                }


                // если есть изделия-заготовки, то не будем их добавлять в состав. Надо найти деталь, заготовкой которой они являются и перенести в него количество
                IList<DataBaseProduct> elementsForDetails = new List<DataBaseProduct>();
                if (detailBlank.Rows.Count > 0)
                {
                    foreach (DataRow row in detailBlank.Rows)
                    {
                        DataBaseProduct element = CreateNewDatabaseProduct(row, keeper.Session, elementsForDetails);
                        elementsForDetails.Add(element);
                    }    
                }

                foreach (DataRow row in articlesCmposition.Rows)
                {
                    DataBaseProduct element = CreateNewDatabaseProduct(row, keeper.Session, elementsForDetails);
                    elements.Add(element);

                    if (element.Type == 1078 || element.Type == 1074 || element.Type == 1097)
                    {
                        // надо забрать с этих сложных объектов спецификацию. Именно по ней мы однозначно определим номер последнего изменения
                        ColumnDescriptor[] specificationColumns =
                        {
                            new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор объекта
                            new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                            new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Номер последнего изменения
                        };

                        DataTable docComposition = compositionService.LoadComposition(keeper.Session.SessionGUID, Convert.ToInt64(element.Id), 1004, new List<ColumnDescriptor>(specificationColumns), string.Empty, 1259);

                        if (docComposition.Rows.Count > 0)
                        {
                            if (docComposition.Rows[0][2] != DBNull.Value)
                            {
                                // присваиваем номер последнего изменения
                                element.LastVersion = Convert.ToString(docComposition.Rows[0][2]);
                            }
                        }
                    }

                }

            }
            return elements;
        }

        private DataBaseProduct CreateNewDatabaseProduct(DataRow row, IUserSession session, IList<DataBaseProduct> elementsForDetails)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            if (elementsForDetails == null)
            {
                throw new ArgumentNullException("elementsForDetails");
            }

            DataBaseProduct product = new DataBaseProduct();

            product.Id = Convert.ToInt64(row[0]);
            product.ObjectId = Convert.ToInt64(row[15]);
            product.Type = Convert.ToInt32(row[1]);

            if (row[2] != DBNull.Value)
            {
                product.Designation = Convert.ToString(row[2]);
            }

            if (row[3] != DBNull.Value)
            {
                product.Name  = Convert.ToString(row[3]);
            }

            if (row[4] != DBNull.Value)
            {
                product.AmountAsString = Convert.ToString(row[4]);
            }

            if (row[5] != DBNull.Value)
            {
                product.PartNumber = Convert.ToString(row[5]);
            }

            if (row[6] != DBNull.Value)
            {
                product.CooperationFlag = Convert.ToBoolean(row[6]);
            }

            if (row[7] != DBNull.Value)
            {
                product.SubstituteGroupNumber = Convert.ToInt32(row[7]);
            }

            if (row[8] != DBNull.Value)
            {
                product.SubstituteNumberInGroup = Convert.ToInt32(row[8]);
            }

            if (row[9] != DBNull.Value)
            {
                product.PositionInSpecification = Convert.ToString(row[9]);
            }

            if (row[10] != DBNull.Value)
            {
                product.PositionDesignation = Convert.ToString(row[10]);
            }

            if (row[11] != DBNull.Value)
            {
                product.Note = Convert.ToString(row[11]);
            }

            if (row[12] != DBNull.Value)
            {
                product.Supplier = Convert.ToString(row[12]);
            }

            if (row[13] != DBNull.Value)
            {
                product.Class = Convert.ToString(row[13]);
            }

            if (row[14] != DBNull.Value)
            {
                product.LastVersion = Convert.ToString(row[14]);
            }


            if (product.Type == 1052 || product.Type == 1159)
            {
                IDBObject detailObject = session.GetObject(product.Id);
                IDBAttribute materialAttribute = detailObject.GetAttributeByID(1181);
                if (materialAttribute != null)
                {
                    long materialId = materialAttribute.AsInteger;
                            
                    if (materialId != (int)MaterialTypes.Zero && materialId != (int)MaterialTypes.NotDefined) 
                    {
                        IDBObject materialObject = session.GetObject(materialId);

                        if (materialObject != null)
                        {
                            // забрать единицы измерения
                            IDBAttribute unitsAttribute = materialObject.GetAttributeByID(1254);
                            DataBaseProduct detailMaterialNode = new DataBaseProduct()
                            {
                                Id = materialObject.ObjectID,
                                ObjectId = materialObject.ID,
                                Name = materialObject.Caption,
                                Type = materialObject.TypeID,
                            };

                            // есть совпадение с изделием-заготовкой
                            foreach (DataBaseProduct elementForDetail in elementsForDetails)
                            {
                                if (detailMaterialNode.Id == elementForDetail.Id)
                                {
                                    detailMaterialNode.Amount = elementForDetail.Amount;
                                    break;
                                }
                            }

                            if (unitsAttribute != null)
                            {
                                detailMaterialNode.MeasureUnits = unitsAttribute.AsString;
                            }

                            product.Add(detailMaterialNode);
                        }
                    }
                }
            }
            return product;
        }

    }
}