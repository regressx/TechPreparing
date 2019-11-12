// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   Модель для обслуживания запросов формы MainView
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Interfaces.Services;

namespace NavisElectronics.TechPreparation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Aga.Controls.Tree;
    using Entities;
    using Interfaces;
    using Intermech.Interfaces;
    using Services;
    using TechPreparing.Data.Helpers;
    using TreeNodes;

    /// <summary>
    /// Модель для обслуживания запросов формы MainView
    /// </summary>
    public class MainViewModel
    {
        public event EventHandler<SaveServiceEventArgs> Saving; 

        /// <summary>
        /// Репозиторий с деревом заказа
        /// </summary>
        private readonly IDataRepository _reader;
        
        /// <summary>
        /// Сервис, умеющий писать Dataset в базу данных
        /// </summary>
        private readonly SaveService _saveService;


        private readonly ITechPreparingSelector<IdOrPath> _selector;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="reader">
        /// Репозиторий с деревом заказа
        /// </param>
        /// <param name="writer">
        /// Интерфейс для записи в базу данных
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        public MainViewModel(IDataRepository reader, SaveService saveService, ITechPreparingSelector<IdOrPath> selector)
        {
            _reader = reader;
            _saveService = saveService;
            _selector = selector;
            _saveService.Saving += _saveService_Saving;
        }

        private void _saveService_Saving(object sender, SaveServiceEventArgs e)
        {
            if (Saving != null)
            {
                Saving(sender, e);
            }
        }

        public IList<IdOrPath> Select()
        {
            return _selector.Select();
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

        /// <summary>
        /// Пересчитать данные о входимости, а также с учетом тех. запаса
        /// </summary>
        /// <param name="mainElement">
        /// Элемент дерева
        /// </param>
        public void RecountAmount(IntermechTreeElement mainElement)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            foreach (IntermechTreeElement child in mainElement.Children)
            {
                queue.Enqueue(child);
            }

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.AmountWithUse = elementFromQueue.Amount * elementFromQueue.Parent.AmountWithUse;
                elementFromQueue.TotalAmount = elementFromQueue.AmountWithUse * elementFromQueue.StockRate;
                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        public async Task<WithdrawalType> GetWithdrawalTypesAsync()
        {
            return await _reader.GetWithdrawalTypesAsync();
        }

        public async Task<IntermechTreeElement> GetTreeFromFileAsync(long orderVersionId)
        {
            return await _reader.GetDataFromFileAsync(orderVersionId, ConstHelper.FileAttribute);
        }

        public async Task<ICollection<Agent>> GetAllAgentsAsync()
        {
            return await _reader.GetAllAgentsAsync();
        }

        /// <summary>
        /// Метод пишет
        /// </summary>
        /// <param name="orderVersionId">
        /// The order version id.
        /// </param>
        /// <param name="mainTreeElement">
        /// The main tree element.
        /// </param>
        public Task WriteIntoFileAttributeAsync(long orderVersionId, IntermechTreeElement mainTreeElement)
        {
            return _saveService.SaveAsync(orderVersionId, mainTreeElement);
        }

        /// <summary>
        /// Рекусивно строит дерево
        /// </summary>
        /// <param name="mainNode">
        /// Узел, который надо заполнить
        /// </param>
        /// <param name="mainElement">
        /// Главный элемент, из которого строим дерево
        /// </param>
        private void BuildTreeRecursive(ViewNode mainNode, IntermechTreeElement mainElement)
        {
            IEnumerable<IntermechTreeElement> nodes = mainElement.Children.Cast<IntermechTreeElement>();

            foreach (IntermechTreeElement node in nodes)
            {
                ViewNode viewNode = new ViewNode();
                viewNode.Name = node.Name;
                viewNode.Designation = node.Designation;
                viewNode.Amount = node.Amount;
                viewNode.AmountWithUse = node.AmountWithUse;
                viewNode.RelationName = node.RelationName;
                viewNode.Tag = node;
                mainNode.Nodes.Add(viewNode);
                if (node.Children.Count > 0)
                {
                    BuildTreeRecursive(viewNode, node);
                }
            }
        }

        public TreeModel GetTreeModel(IntermechTreeElement rootElement)
        {
            TreeModel model = new TreeModel();
            ViewNode root = new ViewNode();
            root.Name = rootElement.Name;
            root.Designation = rootElement.Designation;
            root.RelationName = rootElement.RelationName;
            root.Tag = rootElement;
            model.Nodes.Add(root);
            BuildTreeRecursive(root, rootElement);
            return model;
        }

        /// <summary>
        /// Асинхронно получает структуру предприятия
        /// </summary>
        /// <returns></returns>
        public async Task<TechRouteNode> GetWorkShopsAsync()
        {
            return await _reader.GetWorkshopsAsync();
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

        public Task WriteBlobAttributeAsync<T>(long rootVersionId, T element, int attrubuteId, string comment)
        {
            return _saveService.SaveIntoBlobAttributeAsync(rootVersionId, element, attrubuteId, comment);
        }

        public Task<T> ReadDataFromBlobAttribute<T>(long rootVersionId, int attrubuteId) where T:class
        {
            return _reader.GetDataFromBinaryAttributeAsync<T>(rootVersionId, attrubuteId);
        }

        /// <summary>
        /// Метод копирования одной тех. подготовки в другую
        /// </summary>
        /// <param name="elementWithTechPrep">
        /// Элемент с технологической подготовкой
        /// </param>
        /// <param name="elementWithout">
        /// Элемент без технологической подготовки
        /// </param>
        public void CopyTechPreparation(IntermechTreeElement elementWithTechPrep, IntermechTreeElement elementWithout)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(elementWithout);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                // ищем все-все узлы из дерева c тех. подготовкой, которые совпадают с узлом без тех. подготовки
                ICollection<IntermechTreeElement> elementsToSetTechPreparation = elementWithTechPrep.Find(elementFromQueue.ObjectId);

                foreach (IntermechTreeElement element in elementsToSetTechPreparation)
                {
                    CopyTechPrepSingle(element, elementFromQueue);
                    break;
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        /// <summary>
        /// The copy tech prep single.
        /// </summary>
        /// <param name="elementWithTechPrep">
        /// The element with tech prep.
        /// </param>
        /// <param name="elementWithout">
        /// The element without.
        /// </param>
        private void CopyTechPrepSingle(IntermechTreeElement elementWithTechPrep, IntermechTreeElement elementWithout)
        {
            elementWithout.CooperationFlag = elementWithTechPrep.CooperationFlag;
            elementWithout.Agent = elementWithTechPrep.Agent;
            elementWithout.StockRate = elementWithTechPrep.StockRate;
            elementWithout.SampleSize = elementWithTechPrep.SampleSize;
            elementWithout.TechProcessReference = elementWithTechPrep.TechProcessReference;
            elementWithout.Note = elementWithTechPrep.Note;
            elementWithout.RouteNote = elementWithTechPrep.RouteNote;
            elementWithout.TechRoute = elementWithTechPrep.TechRoute;
            elementWithout.ContainsInnerCooperation = elementWithTechPrep.ContainsInnerCooperation;
            elementWithout.InnerCooperation = elementWithTechPrep.InnerCooperation;
        }
    }
}