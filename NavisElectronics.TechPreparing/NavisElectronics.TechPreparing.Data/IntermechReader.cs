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
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using ICSharpCode.SharpZipLib.Zip.Compression;
    using Interfaces;
    using Interfaces.Entities;
    using Interfaces.Exceptions;
    using Interfaces.Helpers;
    using Interfaces.Services;
    using Intermech;
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

        private RecountService _recountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntermechReader"/> class.
        /// </summary>
        public IntermechReader(RecountService recountService)
        {
            _substituteDecriptorService = new SubsituteDecryptorService(new ActualSubsitutesDecryptor(), new SubGroupsDecryptor(new SubGroupDecryptor()));
            _recountService = recountService;
        }


        /// <summary>
        /// Количество в изделии заготовке
        /// </summary>
        /// <param name="id">
        /// идентификатор версии сборочной единицы
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<ICollection<TechRouteNode>> GetTechRouteAsync(IntermechTreeElement element, IDictionary<long, TechRouteNode> dictionary, string organizationStructName, string productionType)
        {
            Func<ICollection<TechRouteNode>> func = () => GetTechRouteInternal(element, dictionary, organizationStructName, productionType);

            return Task.Run(func);
        }

        private ICollection<TechRouteNode> GetTechRouteInternal(IntermechTreeElement element, IDictionary<long, TechRouteNode> dictionary, string organizationStructName, string productionType)
        {
            ICollection<TechRouteNode> existedRoutes = new List<TechRouteNode>();
            DataTable techRoutes = null;
            IDictionary<long, IntermechTreeElement> materials = new Dictionary<long, IntermechTreeElement>();
            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                ColumnDescriptor[] columnsForTechRoutes =
                {
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(1032, AttributeSourceTypes.Relation,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0), // сортировка
                    new ColumnDescriptor(14819, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // код организации разработчика
                    new ColumnDescriptor(10, AttributeSourceTypes.Object,
                    ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // наименование
                    new ColumnDescriptor(1065, AttributeSourceTypes.Object,
                    ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0) // тип производства (микроэлектроника, цех нап и тд и тп)

                };

                // Поиск состава
                techRoutes = compositionService.LoadComposition(keeper.Session, element.Id, 1002, new List<ColumnDescriptor>(columnsForTechRoutes), string.Empty, 1037); // забрать всё по связи тех. состав

                if (techRoutes != null)
                {
                    // есть маршруты обработки
                    if (techRoutes.Rows.Count != 0)
                    {
                        foreach (DataRow techRoutesRow in techRoutes.Rows)
                        {
                            long routeVersionId = (long)techRoutesRow[0];
                            
                            // создать маршрут обработки
                            TechRouteNode techRoute = new TechRouteNode();

                            // добавляем в коллекцию
                            existedRoutes.Add(techRoute);

                            techRoute.Id = routeVersionId;
                            techRoute.Name = techRoutesRow[4] != DBNull.Value
                                ? (string)techRoutesRow[4]
                                : string.Empty;

                            // смотрим тех. процессы в нём
                            DataTable techRouteItems = compositionService.LoadComposition(
                                keeper.Session,
                                routeVersionId,
                                1002,
                                new List<ColumnDescriptor>(columnsForTechRoutes),
                                string.Empty,
                                1090, // только изделия-заготовки и единичные тех. процессы
                                1237); 

                            if (techRouteItems != null)
                            {
                                if (techRouteItems.Rows.Count != 0)
                                {
                                    foreach (DataRow row in techRouteItems.Rows)
                                    {
                                        int type = (int)row[1];

                                        switch (type)
                                        {
                                            // заготовки
                                            case 1090:
                                                long blankVersionId = (long)row[0];

                                                IDBObject blankObject = keeper.Session.GetObject(blankVersionId);

                                                if (element.IsPcb)
                                                {
                                                    IDBAttribute techTaskOnPcbAttribute = blankObject.GetAttributeByID(18086);
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
                                                else
                                                {
                                                    // если детали и б/ч детали, то их материалу надо присвоить значение
                                                    if (element.Type == 1052 || element.Type == 1159)
                                                    {
                                                        IDBAttribute amountAttribute = blankObject.GetAttributeByID(1223);

                                                        if (amountAttribute.Value != DBNull.Value)
                                                        {
                                                            MeasuredValue value = (MeasuredValue)amountAttribute.Value;

                                                            if (element.RelationName != "Изделие-заготовка")
                                                            {
                                                                // присвоить материалу внутри детали полученное значение
                                                                element.Children[0].Amount = (float)value.Value;

                                                                MeasureDescriptor descriptor =
                                                                    MeasureHelper.Measures.FirstOrDefault(measureDescriptor =>
                                                                        measureDescriptor.MeasureID == value.MeasureID);
                                                                element.Children[0].MeasureUnits = descriptor.ShortName;
                                                            }
                                                        }
                                                    }
                                                }

                                                IDBAttribute stockKoefAttribute = blankObject.GetAttributeByID(14410);
                                                if (stockKoefAttribute != null)
                                                {
                                                    element.StockRate = stockKoefAttribute.AsDouble;
                                                }

                                                IDBAttribute sampleSizeAttribute = blankObject.GetAttributeByID(17912);
                                                if (sampleSizeAttribute != null)
                                                {
                                                    element.SampleSize = sampleSizeAttribute.AsString;
                                                }
                                                else
                                                {
                                                    element.SampleSize = "100%";
                                                }


                                                break; 
                                        
                                            // тех процессы
                                            case 1237:
                                                long techProcessId = (long)row[0];

                                                string developer = row[3] != DBNull.Value
                                                    ? Convert.ToString(row[3])
                                                    : string.Empty;

                                                string productTypeOfSingleTechProcess =
                                                    row[5] == DBNull.Value
                                                        ? string.Empty
                                                        : (string)row[5];

                                                if (developer.ToUpper() != organizationStructName.ToUpper() || productionType != productTypeOfSingleTechProcess)
                                                {
                                                    continue;
                                                }

                                                TechRouteNode techProcessNode = new TechRouteNode();
                                                techRoute.Add(techProcessNode);

                                                DataTable techProcessItems = compositionService.LoadComposition(
                                                    keeper.Session,
                                                    techProcessId,
                                                    1002,
                                                    new List<ColumnDescriptor>(columnsForTechRoutes),
                                                    string.Empty,
                                                    1110); // цехозаходы 



                                                foreach (DataRow item in techProcessItems.Rows)
                                                {
                                                    IDBObject workshop = keeper.Session.GetObject((long)item[0]);
                                                
                                                    // ссылка на объект Imbase
                                                    IDBAttribute imbaseReferenceAttribute = workshop.GetAttributeByID(1092);

                                                    if (imbaseReferenceAttribute == null)
                                                    {
                                                        throw new NullReferenceException("В тех. процессе есть цехозаход, у которого отсутствует привязка к справочнику!");
                                                    }

                                                    TechRouteNode workshopNode = null;
                                                    
                                                    try
                                                    {
                                                        workshopNode = dictionary[imbaseReferenceAttribute.AsInteger];
                                                    }
                                                    catch (KeyNotFoundException ex)
                                                    {
                                                        throw new KeyNotFoundException("Нет маршрута обработки для запрошенного узла " + element.Designation + " " + element.Name, ex);
                                                    }


                                                    ColumnDescriptor[] columnsForOperations =
                                                    {
                                                        new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                                                            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                                                    };

                                                    DataTable operations = compositionService.LoadComposition(
                                                        keeper.Session,
                                                        workshop.ObjectID,
                                                        1002,
                                                        new List<ColumnDescriptor>(columnsForOperations),
                                                        string.Empty,
                                                        1075); // материалы 



                                                    ColumnDescriptor[] columnsForMaterials =
                                                    {
                                                        new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                                                            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                                                        new ColumnDescriptor(10, AttributeSourceTypes.Object,
                                                            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0), // наименование
                                                        new ColumnDescriptor(1129, AttributeSourceTypes.Relation,
                                                            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0), // количество
                                                        new ColumnDescriptor(-20, AttributeSourceTypes.Relation,
                                                            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0) // идентификатор связи
                                                    };

                                                    foreach (DataRow operation in operations.Rows)
                                                    {
                                                        DataTable materialsItems = compositionService.LoadComposition(
                                                            keeper.Session,
                                                            (long)operation[0],
                                                            1002,
                                                            new List<ColumnDescriptor>(columnsForMaterials),
                                                            string.Empty,
                                                            1128); // материалы 

                                                        foreach (DataRow materialRow in materialsItems.Rows)
                                                        {
                                                            IDBObject materialObject =
                                                                keeper.Session.GetObject((long)materialRow[0]);

                                                            if (materials.ContainsKey(materialObject.ID))
                                                            {
                                                                IntermechTreeElement materialFromDictionary =
                                                                    materials[materialObject.ID];

                                                                IDBRelation relation =
                                                                    keeper.Session.GetRelation((long) materialRow[3]);
                                                                IDBAttribute amountAttribute =
                                                                    relation.GetAttributeByID(1129);

                                                                materialFromDictionary.Amount +=
                                                                    (float)((MeasuredValue)amountAttribute.Value).Value;
                                                            }
                                                            else
                                                            {
                                                                IntermechTreeElement materialElement =
                                                                    new IntermechTreeElementBuilder()
                                                                        .SetId(materialObject.ObjectID)
                                                                        .SetObjectId(materialObject.ID)
                                                                        .SetType(1128)
                                                                        .SetName(materialRow[1])
                                                                        .SetRelationId((long) materialRow[3])
                                                                        .SetAmount(materialRow[2]);
                                                                materialElement.RelationName = "Технологический состав";
                                                                materials.Add(materialElement.ObjectId, materialElement);
                                                            }
                                                        }
                                                    }

                                                    // если первый элемент в списке 102 или 101, то это автоматически кооперация и для всех дочерних узлов ественно
                                                    if (techProcessNode.Children.Count == 0)
                                                    {
                                                        if (workshopNode.PartitionName == "102" ||
                                                            workshopNode.PartitionName == "101")
                                                        {
                                                            // поиск по связи групповой тп
                                                            IDBRelationCollection collectionOfRelations =
                                                                keeper.Session.GetRelationCollection(1006);

                                                            // получить все групповые тех. процессы
                                                            DBRecordSetParams pars = new DBRecordSetParams(null, new object[] { -2 }, null, null);

                                                            DataTable groupTechProcesses = collectionOfRelations.EntersInVersion(pars, techProcessId);;

                                                            // выбрать один, который совпадает по коду организации и виду производства
                                                            foreach (DataRow groupTechProcess in groupTechProcesses.Rows)
                                                            {
                                                                IDBObject groupTechProcessDBobject =
                                                                    keeper.Session.GetObject((long)groupTechProcess[0]);

                                                                IDBAttribute groupDesAttribute =
                                                                    groupTechProcessDBobject.GetAttributeByID(9);
                                                                IDBAttribute groupTechProcessDeveloperAttribute =
                                                                    groupTechProcessDBobject.GetAttributeByID(14819);

                                                                IDBAttribute productionTypeOfGroupTechProcess =
                                                                    groupTechProcessDBobject.GetAttributeByID(1065);


                                                                string groupTechProcessDeveloper = groupTechProcessDeveloperAttribute != null
                                                                    ? groupTechProcessDeveloperAttribute.AsString
                                                                    : string.Empty;

                                                                string productionTypeOfGroupTechPRocess =
                                                                    productionTypeOfGroupTechProcess == null
                                                                        ? string.Empty
                                                                        : productionTypeOfGroupTechProcess.AsString;

                                                                // если не сходится разработчик или тип производства, пропускаем
                                                                if (developer.ToUpper() != groupTechProcessDeveloper.ToUpper() || productionTypeOfGroupTechPRocess != productionType)
                                                                {
                                                                    continue;
                                                                }
                                                                
                                                                element.TechProcessReference = new TechProcess()
                                                                {
                                                                    Id = groupTechProcessDBobject.ObjectID,
                                                                    Name = groupDesAttribute.AsString
                                                                };

                                                                // всегда 100
                                                                element.SampleSize = "100%";
                                                            }

                                                            Queue<IntermechTreeElement> elementQueue = new Queue<IntermechTreeElement>();
                                                            elementQueue.Enqueue(element);
                                                            while (elementQueue.Count > 0)
                                                            {
                                                                IntermechTreeElement elementFromQueue = elementQueue.Dequeue();
                                                                elementFromQueue.CooperationFlag = true;
                                                                foreach (IntermechTreeElement child in elementFromQueue.Children)
                                                                {
                                                                    elementQueue.Enqueue(child);
                                                                }
                                                            }
                                                        }
                                                    }

                                                    techProcessNode.Add(workshopNode);
                                                }

                                                // Удалить элементы по связи тех. состав из элемента
                                                IEnumerable<IntermechTreeElement> children = element.Children.Where((e) => e.RelationName != "Технологический состав");
                                                element.Children = children.ToList();

                                                // добавить элементы по связи тех. состав в элемент
                                                foreach (IntermechTreeElement techMaterial in materials.Values)
                                                {
                                                    element.Add(techMaterial);
                                                }

                                                break;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            return existedRoutes;
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
        public ICollection<IntermechTreeElement> Read(long id, string caption)
        {
            return Read(id, CancellationToken.None, caption);
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

            _recountService.RecountAmount(orderElement);

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
        private ICollection<IntermechTreeElement> Read(long id, CancellationToken token, string caption)
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
                DataTable documents = compositionService.LoadComposition(keeper.Session, id, 1004, new List<ColumnDescriptor>(columnsForDocuments), SystemGUIDs.filtrationBaseVersions); // забрать всю документацию

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
                DataTable articlesCmposition = compositionService.LoadComposition(keeper.Session.SessionGUID, id, 1, new List<ColumnDescriptor>(columns), SystemGUIDs.filtrationBaseVersions, 1074, 1078, 1052, 1128, 1138, 1105, 1097);

                // по связи деталь-заготовка
                DataTable detailBlank = compositionService.LoadComposition(keeper.Session.SessionGUID, id, 1038, new List<ColumnDescriptor>(columns), SystemGUIDs.filtrationBaseVersions, 1074, 1078, 1052, 1128, 1138, 1105, 1097);

                // если есть изделия-заготовки, то не будем их добавлять в состав. Надо найти деталь, заготовкой которой они являются и перенести в него количество
                IList<IntermechTreeElement> elementsForDetails = new List<IntermechTreeElement>();
                if (detailBlank.Rows.Count > 0)
                {
                    foreach (DataRow row in detailBlank.Rows)
                    {
                        IntermechTreeElement element = CreateNewElement(row, keeper.Session, elementsForDetails, caption);
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
                    new ColumnDescriptor(18028, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // подбор для позиционного обозначения
                    new ColumnDescriptor(11, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание по связи
                    new ColumnDescriptor(17995, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // примечание ПЭ
                    new ColumnDescriptor(17765, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Тип корпуса
                    new ColumnDescriptor(1098, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Класс
                    new ColumnDescriptor((int)ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // Идентификатор объекта
                };

                // по связи Подборной компонент
                DataTable pickedElements = compositionService.LoadComposition(keeper.Session.SessionGUID, id, 1056, new List<ColumnDescriptor>(columnsForPickedRelation), SystemGUIDs.filtrationBaseVersions, 1074, 1078, 1052, 1128, 1138, 1105, 1097);

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
                    IntermechTreeElement element = CreateNewElement(row, keeper.Session, elementsForDetails, caption);

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

                        DataTable docComposition = compositionService.LoadComposition(keeper.Session.SessionGUID, Convert.ToInt64(element.Id), 1004, new List<ColumnDescriptor>(specificationColumns), SystemGUIDs.filtrationBaseVersions, 1259, 1682);

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
                                        if (dataRow[3] != DBNull.Value)
                                        {
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
                // десериализуем
                JsonSerializer jsonSerializer = new JsonSerializer();
                using (JsonReader jsonReader = new BsonReader(ms))
                {
                    root = (IntermechTreeElement)jsonSerializer.Deserialize(jsonReader, typeof(IntermechTreeElement));
                }
            }

            // расчет применяемостей
            _recountService.RecountAmount(root);
            return root;
        }


        public Task<IntermechTreeElement> GetDataFromFileAsync(long versionId, int dataAttributeId)
        {
            return Task.Run<IntermechTreeElement>(() =>
            {
                return GetDataFromFile(versionId, dataAttributeId);
            });
        }

        public Task<T> GetDataFromBinaryAttributeAsync<T>(long versionId, int dataAttributeId, IDeserializeStrategy<T> deserializeStrategy) where T:class
        {
            return Task.Run<T>(() => { return GetDataFromBinaryAttribute<T>(versionId, dataAttributeId,deserializeStrategy); });
        }

        public T GetDataFromBinaryAttribute<T>(long versionId, int dataAttributeId, IDeserializeStrategy<T> deserializeStrategy) where T:class
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


            T deserializedObject = deserializeStrategy.Deserialize(unpackedBytes);

            return deserializedObject;
        }

        /// <summary>
        /// Асинхронно получает цеха из IMBase
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<TechRouteNode> GetWorkshopsAsync()
        {
            Func<TechRouteNode> funcDelegate = GetWorkshopInternal;
            return Task.Run(funcDelegate);
        }

        private TechRouteNode GetWorkshopInternal()
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
            elements = Read(elementToFetch.Id, token, elementToFetch.Designation);
                
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
        private IntermechTreeElement CreateNewElement(DataRow row, IUserSession session, IList<IntermechTreeElement> elementsForDetails, string parentDesignation)
        {
            //TODO Этот метод можно попозже заменить на уже существующий класс IntermechTreeElementBulder, в нём тоже не всё замечательно, но тогда мы снимаем лишнюю обязанность с текущего класса

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
                    throw new FormatException(string.Format("Нет количеств у объекта {0} в составе объекта {1}", element.Name, parentDesignation));
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

            // если деталь или Б/Ч деталь
            if (element.Type == 1052 || element.Type == 1159)
            {
                IDBObject detailObject = session.GetObject(element.Id);

                SetChangeDocumentName(element, row[21]);

                IDBAttribute materialAttribute = detailObject.GetAttributeByID(1181);

                if (materialAttribute == null)
                {
                    string message = string.Format($"У детали {element.Designation} отсутствует материал");
                    throw new TreeNodeNotFoundException(message);
                }


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
                            StockRate = 1,
                            RelationName = "Состав изделия"

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
                        element.Add(detailMaterialNode);
                    }
                }
            }

            return element;
        }

        /// <summary>
        /// Присвоить документу децимальный номер извещения
        /// </summary>
        /// <param name="element">
        /// узел дерева
        /// </param>
        /// <param name="changeDocument">
        /// извещение
        /// </param>
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
                    try
                    {
                        IDBObject documentObject = keeper.Session.GetObject(changeDocumentId);
                        element.ChangeDocument = documentObject.Caption;
                    }
                    catch (Exception)
                    {
                        element.ChangeDocument = string.Empty;
                    }


                } 
            }
        }
    }
}
