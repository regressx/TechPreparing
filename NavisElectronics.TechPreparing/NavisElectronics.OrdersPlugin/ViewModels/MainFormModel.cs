namespace NavisElectronics.Orders.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Aga.Controls.Tree;
    using DataAccessLayer;
    using Enums;
    using Intermech.Interfaces;
    using Reports;
    using TechPreparation.Interfaces;
    using TechPreparation.Interfaces.Entities;
    using TechPreparation.Interfaces.Services;

    /// <summary>
    /// Фасад
    /// </summary>
    public class MainFormModel
    {
        /// <summary>
        /// Репозиторий с деревом заказа
        /// </summary>
        private readonly IDataRepository _reader;

        /// <summary>
        /// Сервис сохранения данных
        /// </summary>
        private readonly SaveService _saveService;

        /// <summary>
        /// Репозиторий, их которого получаем типы документов.
        /// </summary>
        private readonly SupportingRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainFormModel"/> class.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="saveService">
        /// The save service.
        /// </param>
        /// <param name="repository">
        /// Репозиторий типов документов
        /// </param>
        public MainFormModel(IDataRepository reader, SaveService saveService, SupportingRepository repository)
        {
            _reader = reader;
            _saveService = saveService;
            _repository = repository;
        }


        /// <summary>
        /// Асинхронно получает заказ
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии заказа
        /// </param>
        /// <param name="token">
        /// Токен отмемы
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IntermechTreeElement> GetFullOrderAsync(long versionId, CancellationToken token)
        {
            return await _reader.GetFullOrderAsync(versionId, token);
        }

        public Task WriteBlobAttributeAsync<T>(long rootVersionId, T element, int attrubuteId, string comment,  ISerializeStrategy<T> serializeStrategy)
        {
            return _saveService.SaveIntoBlobAttributeAsync(rootVersionId, element, attrubuteId, comment, serializeStrategy);
        }

        public Task<T> ReadDataFromBlobAttribute<T>(long rootVersionId, int attrubuteId) where T:class
        {
            return _reader.GetDataFromBinaryAttributeAsync<T>(rootVersionId, attrubuteId, new DeserializeStrategyBson<T>());
        }

        public Task<bool> AttributeExist(long versionId, int attributeId)
        {
            Func<bool> func = () =>
            {
                bool result = false;
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject orderObject = keeper.Session.GetObject(versionId);
                    IDBAttribute attribute =
                        orderObject.GetAttributeByID(attributeId);
                    if (attribute != null)
                    {
                        result = true;
                    }
                }

                return result;
            };

            return Task.Run(func);

        }

        public TreeModel GetTreeModel(IntermechTreeElement elementToView)
        {
            OrderNode root = new OrderNode();
            root.Amount = elementToView.Amount;
            root.AmountWithUse = elementToView.AmountWithUse;
            root.Name = elementToView.Name;
            root.Designation = elementToView.Designation;
            root.Tag = elementToView;
            GetOrderNodeRecursive(root, elementToView);
            TreeModel model = new TreeModel();
            model.Nodes.Add(root);
            return model;
        }

        public void CreateReport(string reportName, OrderNode element, ReportStyle reportStyle)
        {
            IReportFactory factory = null;
            switch (reportStyle)
            {
                case ReportStyle.Excel:

                    ICollection<LevelColor> colors = _repository.GetHexStringColorsCollection();
                    factory = new ExcelReportFactory(reportName, new MapTreeOnListService<ReportNode>(), colors);

                    ReportNode root = new ReportNode(element);
                    GetReportNodeRecursive(element, root);
                    factory.Create(root);
                    break;
            }
        }

        private void GetReportNodeRecursive(OrderNode orderNode, ReportNode reportNode)
        {
            int index = 0;
            foreach (OrderNode child in orderNode.Nodes)
            {
                ReportNode node = new ReportNode(child);
                if (node.TypeId == 1052 || node.TypeId == 1074 || node.TypeId == 1078 || node.TypeId == 1097 ||
                    node.TypeId == 1159)
                {
                    index++;
                }

                node.Index = index;
                reportNode.Add(node);
                GetReportNodeRecursive(child, node);
            }
        }


        private void GetOrderNodeRecursive(OrderNode root, IntermechTreeElement elementToView)
        {
            foreach (IntermechTreeElement child in elementToView.Children)
            {
                OrderNode node = new OrderNode();
                node.DoNotProduce = child.ProduseSign;
                node.Type = child.Type;
                node.Designation = child.Designation;
                node.Name = child.Name;
                node.FirstUse = child.FirstUse;
                node.Status = child.LifeCycleStep;
                node.Amount = child.Amount;
                node.AmountWithUse = child.AmountWithUse;
                node.Letter = child.Letter;
                node.ChangeNumber = child.ChangeNumber;
                node.ChangeDocument = child.ChangeDocument;
                node.Note = child.RelationNote;
                node.RelationType = child.RelationName;
                node.Tag = child;
                root.Nodes.Add(node);
                GetOrderNodeRecursive(node, child);
            }
        }


        public void DecryptDocuments(IntermechTreeElement root)
        {
            ICollection<DocumentType> documentTypes = _repository.GetDocumentTypes();
            
            IDictionary<string, string> codesDictionary = new Dictionary<string, string>();
            foreach (DocumentType documentType in documentTypes)
            {
                if (!codesDictionary.ContainsKey(documentType.ShortName))
                {
                    codesDictionary.Add(documentType.ShortName, documentType.Name);
                }
            }


            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                if (elementFromQueue.RelationName == "Документ")
                {
                    int spaceIndex = elementFromQueue.Designation.IndexOf(" ", StringComparison.Ordinal);
                    if (spaceIndex < 0)
                    {
                        elementFromQueue.Name = "ОШИБКА! Такой тип документа отсутствует в справочнике типов документов";

                    }
                    else
                    {
                        string documentCode = elementFromQueue.Designation.Substring(spaceIndex + 1).Trim();
                        try
                        {
                            elementFromQueue.Name = codesDictionary[documentCode];
                        }
                        catch (KeyNotFoundException)
                        {
                            elementFromQueue.Name = "ОШИБКА! Такой тип документа отсутствует в справочнике типов документов";
                        }
                    }

                    if (elementFromQueue.Type == 1259)
                    {
                        elementFromQueue.Name = "Спецификация";
                    }
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
    }
}