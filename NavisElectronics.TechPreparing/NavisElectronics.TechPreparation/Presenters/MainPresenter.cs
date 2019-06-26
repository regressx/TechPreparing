// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPresenter.cs" company="NavisElectronics">
// ---
// </copyright>
// <summary>
//   Посредник между главной формой и ее моделью
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;
    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.Exceptions;
    using NavisElectronics.TechPreparation.Services;
    using NavisElectronics.TechPreparation.ViewInterfaces;
    using NavisElectronics.TechPreparation.ViewModels;
    using NavisElectronics.TechPreparation.Views;

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
        /// Модель для этого представления
        /// </summary>
        private readonly MainViewModel _model;

        /// <summary>
        /// Фабрика представителей. Умеет выдавать представителя по указке
        /// </summary>
        private readonly IPresentationFactory _presentationFactory;

        /// <summary>
        /// Номер версии объекта, для которого запрашивается состав или атрибут
        /// </summary>
        private long _rootVersionId;

        /// <summary>
        /// Для простановки агентов
        /// </summary>
        private IntermechTreeElement _globalTreeElement;

        /// <summary>
        /// Корень
        /// </summary>
        private IntermechTreeElement _rootElement;


        /// <summary>
        /// Нужен для того, чтобы понять, откуда копировать тех. подготовку
        /// </summary>
        private IntermechTreeElement _elementToCopy;

        /// <summary>
        /// Словарик агентов
        /// </summary>
        private IDictionary<long, Agent> _agents;



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
            _mainView.CooperationClick += _mainView_CooperationClick;
            _mainView.ClearCooperationClick += _mainView_ClearCooperationClick;
            _mainView.EditTechRoutesClick += _mainView_EditTechRoutesClick;
            _mainView.UpdateClick += _mainView_UpdateClick;
            _mainView.EditMainMaterialsClick += _mainView_EditMainMaterialsClick;
            _mainView.EditStandartDetailsClick += _mainView_EditStandartDetailsClick;
            _mainView.LoadPreparationClick += MainViewLoadPreparationClick;
            _mainView.EditWithdrawalTypeClick += _mainView_EditWithdrawalTypeClick;
            _mainView.RefreshClick += _mainView_RefreshClick;
            _mainView.CheckAllReadyClick += MainViewCheckAllReadyClick;
        }

        private void MainViewCheckAllReadyClick(object sender, EventArgs e)
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
            ICollection<ExtractedObject> collection = extractor.ExctractObjects(_mainView.GetMainTreeElement().Tag as IntermechTreeElement, IntermechObjectTypes.Other);
            WithdrawalTypeView view = new WithdrawalTypeView(year, _mainView.GetMainTreeElement().Tag as IntermechTreeElement, collection);
            view.Show();
        }

        /// <summary>
        /// Обработчик события загрузки формы выбора заказа с созданной тех. подготовкой
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private async void MainViewLoadPreparationClick(object sender, EventArgs e)
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

            Parameter<IntermechTreeElement> myParameter = new Parameter<IntermechTreeElement>();
            myParameter.AddParameter(oldPreparation);
            _elementToCopy = new IntermechTreeElement();
            myParameter.AddParameter(_elementToCopy);
            IPresenter<Parameter<IntermechTreeElement>> treeNodeDialogPresenter = _presentationFactory.GetPresenter<TreeNodeDialogPresenter, Parameter<IntermechTreeElement>>();
            treeNodeDialogPresenter.Run(myParameter);
            _elementToCopy = myParameter.GetParameter(1);

            if (_elementToCopy.Id == 0)
            {
                return;
            }

            // не хочу заморачиваться с сохранением уже заполненных узлов. Пройдем в ширину по указанному в предложенном окне дереву, 
            // каждый из узлов в очереди будем искать в основном дереве и проставлять нужные свойства
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(_elementToCopy);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                // ищем все-все узлы из главного дерева, которые совпадают с узлом из очереди
                ICollection<IntermechTreeElement> elementsToSetTechPreparation = _rootElement.Find(elementFromQueue.ObjectId);

                foreach (IntermechTreeElement elementToSetTechPreparation in elementsToSetTechPreparation)
                {
                    elementToSetTechPreparation.CooperationFlag = elementFromQueue.CooperationFlag;
                    elementToSetTechPreparation.Agent = elementFromQueue.Agent;
                    elementToSetTechPreparation.StockRate = elementFromQueue.StockRate;
                    elementToSetTechPreparation.Note = elementFromQueue.Note;
                    elementToSetTechPreparation.RouteNote = elementFromQueue.RouteNote;
                    elementToSetTechPreparation.SampleSize = elementFromQueue.SampleSize;
                    elementToSetTechPreparation.TechProcessReference = elementFromQueue.TechProcessReference;
                    elementToSetTechPreparation.TechRoute = elementFromQueue.TechRoute;
                    elementToSetTechPreparation.ContainsInnerCooperation = elementFromQueue.ContainsInnerCooperation;
                    elementToSetTechPreparation.InnerCooperation = elementFromQueue.InnerCooperation;
                    elementToSetTechPreparation.IsPCB = elementFromQueue.IsPCB;
                    elementToSetTechPreparation.IsToComplect = elementFromQueue.IsToComplect;
                    elementToSetTechPreparation.TechTask = elementFromQueue.TechTask;
                }

                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

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

            Parameter<Agent> agentParameter = new Parameter<Agent>();

            foreach (Agent agent in _agents.Values)
            {
                agentParameter.AddParameter(agent);
            }

            Agent filterAgent = new Agent();
            agentParameter.AddParameter(filterAgent);

            IPresenter<Parameter<Agent>> agentDialogPresenter = _presentationFactory.GetPresenter<SelectManufacturerPresenter, Parameter<Agent>>(); 
            agentDialogPresenter.Run(agentParameter);

            if (filterAgent.Id == 0)
            {
                return;
            }

            IPresenter<Parameter<IntermechTreeElement>> presenter = _presentationFactory.GetPresenter<TechRouteMapPresenter, Parameter<IntermechTreeElement>>();
            Parameter<IntermechTreeElement> parameter = new Parameter<IntermechTreeElement>();

            // корень
            parameter.AddParameter(_rootElement);
            
            // кто главный
            parameter.AddParameter(new IntermechTreeElement()
                                       {
                                           Agent = ((int)AgentsId.Kb).ToString()
                                       });
            
            // по кому фильтруем
            parameter.AddParameter(new IntermechTreeElement()
                                       {
                                           Name = filterAgent.Name,
                                           Agent = filterAgent.Id.ToString()
                                       });
            presenter.Run(parameter);
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

        private void _mainView_CooperationClick(object sender, EventArgs e)
        {
            Parameter<Agent> agentParameter = new Parameter<Agent>();

            foreach (Agent agent in _agents.Values)
            {
                agentParameter.AddParameter(agent);
            }

            Agent filterAgent = new Agent();
            agentParameter.AddParameter(filterAgent);

            IPresenter<Parameter<Agent>> agentDialogPresenter = _presentationFactory.GetPresenter<SelectManufacturerPresenter, Parameter<Agent>>(); 
            agentDialogPresenter.Run(agentParameter);

            if (filterAgent.Id == 0)
            {
                return;
            }

            IPresenter<Parameter<IntermechTreeElement>> presenter = _presentationFactory.GetPresenter<CooperationPresenter, Parameter<IntermechTreeElement>>();
            Parameter<IntermechTreeElement> parameter = new Parameter<IntermechTreeElement>();
            parameter.AddParameter(_rootElement);
            parameter.AddParameter(new IntermechTreeElement()
                                       {
                                           Agent = ((int)AgentsId.Kb).ToString()
                                       });
            parameter.AddParameter(new IntermechTreeElement()
                                       {
                                           Name = filterAgent.Name,
                                           Agent = filterAgent.Id.ToString()
                                       });
            presenter.Run(parameter);
        }


        private void _mainView_CellValueChanged(object sender, TreeNodeAgentValueEventArgs e)
        {
            string agentsValues = GatherAgents(e);
            _globalTreeElement.Agent = agentsValues;

            // Раздаем кооперацию потомкам
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
                if (parent == _rootElement)
                {
                    return;
                }
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

        private async void _view_Load(object sender, EventArgs e)
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

            }
            else
            {         
                // получаем всё, что сидит в заказе
                orderElement = await _model.GetFullOrderAsync(_rootVersionId, CancellationToken.None);
            }

            _mainView.FillNote(orderElement.Note);

            _rootElement = orderElement;
            _rootElement.Agent = ((int)AgentsId.Kb).ToString();
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
                    Text = string.Format("{0} {1} {2} {3} ", node.Name, node.Designation, node.IsPCB, description).Trim(),
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