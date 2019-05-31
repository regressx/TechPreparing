using System;
using NavisElectronics.ListOfCooperation.Logic;

namespace NavisElectronics.ListOfCooperation.ViewModels
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Entities;
    using IO;
    using Services;

    /// <summary>
    /// Модель для обслуживания запросов формы MainView
    /// </summary>
    public class MainViewModel
    {
        /// <summary>
        /// Репозиторий с деревом заказа
        /// </summary>
        private readonly IDataRepository _reader;

        /// <summary>
        /// Сервис, умеющий собирать Dataset из заказа
        /// </summary>
        private readonly DataSetGatheringService _gatheringService;

        /// <summary>
        /// Сервис, умеющий писать Dataset в базу данных
        /// </summary>
        private readonly IDatabaseWriter _writer;

        private readonly TreeBuilderService _treeBuilderService;
        private readonly ITechPreparingSelector<IdOrPath> _selector;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="reader">
        /// Репозиторий с деревом заказа
        /// </param>
        /// <param name="gatheringService">
        /// Сервис, умеющий собирать Dataset из заказа
        /// </param>
        /// <param name="writer">
        /// Интерфейс для записи в базу данных
        /// </param>
        /// <param name="treeBuilderService">
        /// Сервис построения дерева из Dataset
        /// </param>
        public MainViewModel(IDataRepository reader, DataSetGatheringService gatheringService, IDatabaseWriter writer, TreeBuilderService treeBuilderService, ITechPreparingSelector<IdOrPath> selector)
        {
            _reader = reader;
            _gatheringService = gatheringService;
            _writer = writer;
            _treeBuilderService = treeBuilderService;
            _selector = selector;
        }

        /// <summary>
        /// Построение представления дерева из состава заказа
        /// </summary>
        /// <param name="mainElement">
        /// Дерево заказа
        /// </param>
        /// <returns>
        /// The <see cref="TreeNode"/>.
        /// </returns>
        public TreeNode BuildTree(IntermechTreeElement mainElement)
        {
            TreeNode mainNode = new TreeNode(mainElement.Name);
            mainNode.Tag = mainElement;
            BuildTreeRecursive(mainNode, mainElement);
            return mainNode;
        }


        public IList<IdOrPath> Select()
        {
            return _selector.Select();
        }

        /// <summary>
        /// Построение представления дерева из Dataset
        /// </summary>
        /// <param name="dataset">
        /// Набор данных из заказа
        /// </param>
        /// <returns>
        /// The <see cref="TreeNode"/>.
        /// </returns>
        public TreeNode BuildTree(DataSet dataset)
        {
            IntermechTreeElement mainElement = _treeBuilderService.Build(dataset, CancellationToken.None);
            return BuildTree(mainElement);
        }


        /// <summary>
        /// Создание представления дерева асинхронно
        /// </summary>
        /// <param name="dataset">
        /// Набор данных
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<TreeNode> BuildTreeAsync(DataSet dataset)
        {
            return BuildTreeAsync(dataset, CancellationToken.None);
        }

        /// <summary>
        /// Создание представления дерева асинхронно
        /// </summary>
        /// <param name="dataset">
        /// Набор данных
        /// </param>
        /// <param name="token">Токен отмены</param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<TreeNode> BuildTreeAsync(DataSet dataset, CancellationToken token)
        {
            IntermechTreeElement mainElement = await _treeBuilderService.BuildAsync(dataset, token);
            Func<TreeNode> func = () => { return BuildTree(mainElement); };

            return await Task.Run(func, token);
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
        /// Асинхронно получает заказ
        /// </summary>
        /// <param name="dataSet">
        /// Dataset
        /// </param>
        /// <param name="token">
        /// Токен отмемы
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IntermechTreeElement> GetFullOrderAsync(DataSet dataSet, CancellationToken token)
        {
            return await _treeBuilderService.BuildAsync(dataSet, token);
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

        public async Task<DataSet> GetDataSetAsync(long orderVersionId)
        {
            return await _reader.GetDatasetAsync(orderVersionId, Helpers.ConstHelper.BinaryDataOfOrder);
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
        public void WriteDatasetIntoDatabase(long orderVersionId, IntermechTreeElement mainTreeElement)
        {
            DataSet ds = _gatheringService.Gather(mainTreeElement);
            _writer.WriteDataSet(orderVersionId, ds);
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
        private void BuildTreeRecursive(TreeNode mainNode, IntermechTreeElement mainElement)
        {
            ICollection<IntermechTreeElement> nodes = mainElement.Children;

            foreach (IntermechTreeElement node in nodes)
            {
                string description = node.SubstituteInfo;

                TreeNode childNode = new TreeNode
                {
                    Text = string.Format("{0} {1} {2:F2} {3} ", node.Name, node.Designation, node.Amount,
                        description).Trim(),
                };

                childNode.Tag = node;

                mainNode.Nodes.Add(childNode);

                if (node.Children.Count > 0)
                {
                    BuildTreeRecursive(childNode, node);
                }
            }
        }
    }
}