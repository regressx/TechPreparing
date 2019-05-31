namespace NavisElectronics.ListOfCooperation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using Entities;
    using Enums;
    using EventArguments;
    using Exceptions;
    using Services;
    using ViewInterfaces;
    using ViewModels;
    using Views;

    /// <summary>
    /// Посредник между главной формой и ее моделью
    /// </summary>
    public class MainPresenter : BasePresenter<long>
    {
        /// <summary>
        /// Представление
        /// </summary>
        private readonly IMainView _mainView;

        /// <summary>
        /// Фабрика представителей. Умеет выдавать представителя по указке
        /// </summary>
        private readonly IPresentationFactory _presentationFactory;

        /// <summary>
        /// Номер версии объекта, для которого запрашивается состав или атрибут
        /// </summary>
        private long _rootVersionId;

        private IntermechTreeElement _globalTreeElement;
        private IntermechTreeElement _elementToCopy;


        private IDictionary<long, Agent> _agents;

        /// <summary>
        /// Модель для этого представления
        /// </summary>
        private readonly MainViewModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPresenter"/> class. 
        /// Конструктор
        /// </summary>
        /// <param name="mainView">
        /// Интерфейс главного окна
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="presentationFactory">
        /// The presentation Factory.
        /// </param>
        public MainPresenter(IMainView mainView, MainViewModel model, IPresentationFactory presentationFactory)
        {
            _model = model;
            _mainView = mainView;
            _presentationFactory = presentationFactory;
            _mainView.Load += _view_Load;
            _mainView.NodeMouseClick += _mainView_NodeMouseClick;
            _mainView.ApplyButtonClick += _mainView_ApplyButtonClick;
            _mainView.CellValueChanged += _mainView_CellValueChanged;
            _mainView.KbNavisFilterClick += _mainView_KbNavisFilterClick;
            _mainView.NavisElectronicsFilterClick += _mainView_NavisElectronicsFilterClick;
            _mainView.ClearCooperationClick += _mainView_ClearCooperationClick;
            _mainView.EditTechRoutesClick += _mainView_EditTechRoutesClick;
            _mainView.UpdateClick += _mainView_UpdateClick;
            _mainView.EditMainMaterialsClick += _mainView_EditMainMaterialsClick;
            _mainView.EditStandartDetailsClick += _mainView_EditStandartDetailsClick;
            _mainView.LoadPreparationClick += _mainView_LoadPreparationClick;
            _mainView.EditWithdrawalTypeClick += _mainView_EditWithdrawalTypeClick;
            _mainView.RefreshClick += _mainView_RefreshClick;
            _mainView.CheckAllReadyClick += _mainView_CheckAllReadyClick;
        }

        private void _mainView_CheckAllReadyClick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            IntermechTreeElement mainElement = _mainView.GetMainTreeElement().Tag as IntermechTreeElement;
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(mainElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                
                // если по кооперации, то пропускаем
                if (elementFromQueue.CooperationFlag)
                {
                    continue;
                }
                if (elementFromQueue.Amount < 0.000001)
                {
                    string str = string.Format("В узле {0} {1} из состава {2} {3} не указано количество",
                        elementFromQueue.Designation,
                        elementFromQueue.Name,
                        elementFromQueue.Parent.Designation,
                        elementFromQueue.Parent.Name);

                    sb.Append(str + Environment.NewLine);
                }

                if (elementFromQueue.MeasureUnits == string.Empty)
                {
                    string str = string.Format("В узле {0} {1} из состава {2} {3} не указана единица измерения",
                        elementFromQueue.Designation,
                        elementFromQueue.Name,
                        elementFromQueue.Parent.Designation,
                        elementFromQueue.Parent.Name);

                    sb.Append(str + Environment.NewLine);
                }

                if (elementFromQueue.Agent == string.Empty)
                {
                    string str = string.Format("В узле {0} {1} из состава {2} {3} не указан контрагент",
                        elementFromQueue.Designation,
                        elementFromQueue.Name,
                        elementFromQueue.Parent.Designation,
                        elementFromQueue.Parent.Name);

                    sb.Append(str + Environment.NewLine);
                }

                if (elementFromQueue.Type == 1159 || elementFromQueue.Type == 1052)
                {
                    if (elementFromQueue.Children.Count == 0)
                    {
                        string str = string.Format("В узле {0} {1} из состава {2} {3} не указан материал",
                            elementFromQueue.Designation,
                            elementFromQueue.Name,
                            elementFromQueue.Parent.Designation,
                            elementFromQueue.Parent.Name);

                        sb.Append(str + Environment.NewLine);
                    }
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }

            ReportForm rf = new ReportForm(sb.ToString());
            rf.Show();
        }

        private void _mainView_RefreshClick(object sender, EventArgs e)
        {
            IntermechTreeElement mainElement = _mainView.GetMainTreeElement().Tag as IntermechTreeElement;
            _model.RecountAmount(mainElement);
            TreeNode mainNode = _model.BuildTree(mainElement);
            _mainView.FillTree(mainNode);
        }

        private async void _mainView_EditWithdrawalTypeClick(object sender, EventArgs e)
        {
            WithdrawalType year = await _model.GetWithdrawalTypesAsync();

            IntermechObjectExtractor extractor = new IntermechObjectExtractor();
            ICollection<ExtractedObject> collection = extractor.ExctractObjects(_mainView.GetMainTreeElement().Tag as IntermechTreeElement,
                IntermechObjectTypes.Other);
            WithdrawalTypeView view = new WithdrawalTypeView(year, _mainView.GetMainTreeElement().Tag as IntermechTreeElement,collection);
            view.Show();
        }

        private async void _mainView_LoadPreparationClick(object sender, EventArgs e)
        {
            // загрузить список существующих заказов
            IList<IdOrPath> result = _model.Select();
            if (result.Count == 0)
            {
                return;
            }

            DataSet ds;
            try
            {
                ds = await _model.GetDataSetAsync(result[0].Id);
            }
            catch (DataSetIsEmptyException)
            {
                MessageBox.Show("На выбранный Вами заказ нет никакой тех. подготовки");
                return;
            }

            // строим дерево из полученной тех. подготовки
            IntermechTreeElement oldPreparation = await _model.GetFullOrderAsync(ds, CancellationToken.None);

            IPresenter<IntermechTreeElement> treeNodeDialogPresenter = _presentationFactory.GetPresenter<TreeNodeDialogPresenter, IntermechTreeElement>();

            IntermechTreeElement elementToCopy = new IntermechTreeElement(); // создаем пустышку
            treeNodeDialogPresenter.Run(oldPreparation);
            UpdateTechPreparinNode(elementToCopy);
        }

        private void UpdateTechPreparinNode(IntermechTreeElement elementToCopy)
        {
            _elementToCopy = elementToCopy;
        }

        private void _mainView_EditStandartDetailsClick(object sender, EventArgs e)
        {
            IntermechTreeElement mainElement = _mainView.GetMainTreeElement().Tag as IntermechTreeElement;
            MaterialsView view = new MaterialsView();
            MaterialsViewPresenter presenter = new MaterialsViewPresenter(view, mainElement, IntermechObjectTypes.StandartDetails);
            presenter.Run();
        }


        private void _mainView_EditMainMaterialsClick(object sender, EventArgs e)
        {
            IntermechTreeElement mainElement = _mainView.GetMainTreeElement().Tag as IntermechTreeElement;

            MaterialsView view = new MaterialsView();
            MaterialsViewPresenter presenter = new MaterialsViewPresenter(view, mainElement, IntermechObjectTypes.Material);
            presenter.Run();
        }

        private void _mainView_UpdateClick(object sender, EventArgs e)
        {
            IntermechTreeElement mainElement = _mainView.GetMainTreeElement().Tag as IntermechTreeElement;

            //TreeComparerView view = new TreeComparerView();
            //TreeComparerViewModel model = new TreeComparerViewModel();
            //TreeComparerPresenter presenter = new TreeComparerPresenter(view, model, mainElement, _agents);
            //presenter.Run();

            IPresenter presenter = _presentationFactory.GetPresenter<TreeComparerPresenter>();
            presenter.Run();


        }

        private void _mainView_EditTechRoutesClick(object sender, EventArgs e)
        {
            using (SelectManufacturerView manufacturerView = new SelectManufacturerView(_agents.Values))
            {
                if (manufacturerView.ShowDialog() == DialogResult.OK)
                {
                    string filter = manufacturerView.SelectedAgentId;
                    TechRoutesMap view = new TechRoutesMap(manufacturerView.SelectedAgentName, true);
                    TechRouteMapPresenter presenter = new TechRouteMapPresenter(view, _mainView.GetMainTreeElement().Tag as IntermechTreeElement, filter, _agents);
                    presenter.Run();
                }
            }

        }

        private void _mainView_ClearCooperationClick(object sender, EventArgs e)
        {
            _globalTreeElement.Agent = string.Empty;
            if (_globalTreeElement.Children.Count > 0)
            {
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                foreach (IntermechTreeElement child in _globalTreeElement.Children)
                {
                    queue.Enqueue(child);
                }

                while (queue.Count > 0)
                {
                    IntermechTreeElement child = queue.Dequeue();
                    child.Agent = string.Empty;
                    if (child.Children.Count > 0)
                    {
                        foreach (IntermechTreeElement childNodes in child.Children)
                        {
                            childNodes.Agent = string.Empty;
                            queue.Enqueue(childNodes);
                        }
                    }
                }
            }
            _mainView.FillAgent(_globalTreeElement.Agent);
        }

        private void _mainView_NavisElectronicsFilterClick(object sender, EventArgs e)
        {
            //CooperationView cooperationView = new CooperationView(true);
            //cooperationView.Text = "Фильтр по НАВИС-Электронике";
            //TreeBuilderService treeBuilderService = new TreeBuilderService();
            //IntermechTreeElement mainElement = _mainView.GetMainTreeElement().Tag as IntermechTreeElement;
            //IntermechTreeElement element =
            //    treeBuilderService.BuildTreeWithoutCoop(mainElement, ((int)AgentsId.NavisElectronics).ToString());
            //CooperationPresenter coopPresenter = new CooperationPresenter(cooperationView, element, mainElement);
            //coopPresenter.Run();
        }

        private void _mainView_KbNavisFilterClick(object sender, EventArgs e)
        {
            //CooperationView cooperationView = new CooperationView(true);
            //cooperationView.Text = "Фильтр по КБ НАВИС";
            //TreeBuilderService treeBuilderService = new TreeBuilderService();
            //IntermechTreeElement mainElement = _mainView.GetMainTreeElement().Tag as IntermechTreeElement;
            //IntermechTreeElement element =
            //    treeBuilderService.BuildTreeWithoutCoop(mainElement, ((int)AgentsId.Kb).ToString());
            //CooperationPresenter coopPresenter = new CooperationPresenter(cooperationView, element, mainElement);
            //coopPresenter.Run();
        }

        private void _mainView_CellValueChanged(object sender, TreeNodeAgentValueEventArgs e)
        {

            string agentsValues = GatherAgents(e);

            _globalTreeElement.Agent = agentsValues;

            // Раздаем кооперацию детям

            if (_globalTreeElement.Children.Count > 0)
            {
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                foreach (IntermechTreeElement child in _globalTreeElement.Children)
                {
                    queue.Enqueue(child);
                }

                while (queue.Count > 0)
                {
                    IntermechTreeElement child = queue.Dequeue();
                    child.Agent = agentsValues;
                    if (child.Children.Count > 0)
                    {
                        foreach (IntermechTreeElement childNodes in child.Children)
                        {
                            childNodes.Agent = agentsValues;
                            queue.Enqueue(childNodes);
                        }
                    }
                }

            }

            // раздаем кооперацию родителям
            SetParentCooperationRecursive(_globalTreeElement, e.Key);

            _mainView.FillAgent(_globalTreeElement.Agent);
        }

        private void SetParentCooperationRecursive(IntermechTreeElement globalTreeElement, string key)
        {
            if (globalTreeElement.Parent != null)
            {
                IntermechTreeElement parent = globalTreeElement.Parent;
                string agents = GatherAgents(new TreeNodeAgentValueEventArgs(parent, key));
                parent.Agent = agents;
                SetParentCooperationRecursive(parent, key);
            }
        }


        private string GatherAgents(TreeNodeAgentValueEventArgs element)
        {
            IDictionary<long, string> agents = new Dictionary<long, string>();
            
            // собрать старых агентов
            string oldAgents = element.TreeElement.Agent;

            if (oldAgents == null)
            {
                oldAgents = string.Empty;
            }


            if (oldAgents != string.Empty)
            {
                ICollection<string> oldAgentsCollection = oldAgents.Split(';');

                foreach (string s in oldAgentsCollection)
                {
                    long key = long.Parse(s);
                    if (!agents.ContainsKey(key))
                    {
                        agents.Add(long.Parse(s), s);
                    }
                }
            }

            if (!agents.ContainsKey(long.Parse(element.Key)))
            {
                agents.Add(long.Parse(element.Key), element.Key);
            }


            ICollection<string> agentsValues = agents.Values;

            StringBuilder sb = new StringBuilder();
            foreach (string agentsValue in agentsValues)
            {
                sb.AppendFormat("{0};", agentsValue);
            }

            string value = sb.ToString().TrimEnd(';');

            return value;
        }


        private void _mainView_ApplyButtonClick(object sender, EventArgs e)
        {
            IntermechTreeElement mainTreeElement = (IntermechTreeElement)_mainView.GetMainTreeElement().Tag;
            mainTreeElement.Note = _mainView.GetNote();
            _model.WriteDatasetIntoDatabase(_rootVersionId, mainTreeElement);
        }

        private void _mainView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // TODO Здесь должна появиться проверка на изменение предыдущего узла, если было какое-либо изменение
            IntermechTreeElement treeElement = e.Node.Tag as IntermechTreeElement;

            _globalTreeElement = treeElement;

            if (treeElement != null)
            {
                if (treeElement.Agent != null)
                {
                    _mainView.FillAgent(treeElement.Agent);
                }
            }
        }

        private async void _view_Load(object sender, System.EventArgs e)
        {
            _mainView.FillTree(new TreeNode("Пожалуйста, подождите. Идет загрузка данных"));
            _mainView.LockButtons();
            bool fromDataset = false;
            IntermechTreeElement orderElement = null;
            DataSet dataset = null;
            try
            {
                dataset = await _model.GetDataSetAsync(_rootVersionId);
                fromDataset = true;
            }
            catch (DataSetIsEmptyException)
            {
                // если атрибут пуст, то набор данных будет неоткуда загрузить
            }

            if (fromDataset)
            {
                orderElement = await _model.GetFullOrderAsync(dataset, CancellationToken.None);
                _mainView.FillNote(orderElement.Note);
            }
            else
            {         
                // получаем всё, что сидит в заказе
                orderElement = await _model.GetFullOrderAsync(_rootVersionId, CancellationToken.None);
            }

            ICollection<Agent> agents = await _model.GetAllAgentsAsync();
            _agents = new Dictionary<long, Agent>();
            foreach (Agent agent in agents)
            {
                _agents.Add(agent.Id, agent);
            }

            _mainView.FillGridColumns(agents);

            TreeNode mainNode = new TreeNode(orderElement.Name);
            mainNode.Tag = orderElement;
            FillTreeRecursive(mainNode, orderElement);
            _mainView.FillTree(mainNode);
            _mainView.UnLockButtons();
        }

        private void FillTreeRecursive(TreeNode mainNode, IntermechTreeElement element)
        {
            ICollection<IntermechTreeElement> nodes = element.Children;

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
                    FillTreeRecursive(childNode, node);
                }
            }
        }

        /// <summary>
        /// Запуск главной формы
        /// </summary>
        public override void Run(long rootVersionId)
        {
            _rootVersionId = rootVersionId;
            _mainView.Show();
        }
    }
}