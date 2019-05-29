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
        public MainViewModel(IDataRepository reader, DataSetGatheringService gatheringService,IDatabaseWriter writer)
        {
            _reader = reader;
            _gatheringService = gatheringService;
            _writer = writer;
        }

        public TreeNode BuildTree(IntermechTreeElement mainElement)
        {
            TreeNode mainNode = new TreeNode(mainElement.Name);
            mainNode.Tag = mainElement;
            BuildTreeRecursive(mainNode, mainElement);
            return mainNode;
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