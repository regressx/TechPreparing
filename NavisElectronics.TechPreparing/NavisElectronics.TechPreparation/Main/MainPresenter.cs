// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPresenter.cs" company="NavisElectronics">
// ---
// </copyright>
// <summary>
//   Посредник между главной формой и ее моделью
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces.Helpers;
using NavisElectronics.TechPreparation.TechRouteMap;

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using Entities;
    using Enums;
    using EventArguments;
    using Exceptions;
    using Interfaces.Entities;
    using Interfaces.Services;
    using Services;
    using ViewInterfaces;
    using ViewModels;
    using ViewModels.TreeNodes;
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
        /// Корень
        /// </summary>
        private IntermechTreeElement _rootElement;

        /// <summary>
        /// Словарик агентов
        /// </summary>
        private IDictionary<long, Agent> _agents;

        /// <summary>
        /// Структура предприятия
        /// </summary>
        private TechRouteNode _organizationStruct = null;

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
            _model.Saving += _model_Saving;
            _mainView = mainView;
            _presentationFactory = presentationFactory;
            _mainView.Load += _view_Load;
            _mainView.NodeMouseClick += _mainView_NodeMouseClick;
            _mainView.ApplyButtonClick += _mainView_ApplyButtonClick;
            _mainView.CellValueChanged += _mainView_CellValueChanged;
            _mainView.ClearManufacturerClick += _mainView_ClearManufacturerClick;
            _mainView.EditTechRoutesClick += _mainView_EditTechRoutesClick;
            _mainView.UpdateClick += _mainView_UpdateClick;
            _mainView.EditMainMaterialsClick += _mainView_EditMainMaterialsClick;
            _mainView.EditStandartDetailsClick += _mainView_EditStandartDetailsClick;
            _mainView.LoadPreparationClick += MainViewLoadPreparationClick;
            _mainView.EditWithdrawalTypeClick += _mainView_EditWithdrawalTypeClick;
            _mainView.RefreshClick += _mainView_RefreshClick;
            _mainView.CheckAllReadyClick += MainViewCheckAllReadyClick;
        }

        /// <summary>
        /// Оповещаем представителя о том, что идет сохранение
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _model_Saving(object sender, SaveServiceEventArgs e)
        {
            _mainView.UpdateLabelText(e.Message);
        }

        /// <summary>
        /// Обработчик нажатия кнопки проверки, что всё готово
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainViewCheckAllReadyClick(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            IntermechTreeElement mainElement = _rootElement as IntermechTreeElement;
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(mainElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                
                // если по кооперации, то пропускаем
                if (elementFromQueue.CooperationFlag || elementFromQueue.RelationName == "Документ" || elementFromQueue.ProduseSign)
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
                    string str = string.Format("В узле {0} {1} из состава {2} {3} не указан изготовитель",
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
            //IntermechTreeElement mainElement = _mainView.GetMainTreeElement().Tag as IntermechTreeElement;
            //_model.RecountAmount(mainElement);
            //TreeNode mainNode = _model.BuildTree(mainElement);
            //_mainView.FillTree(mainNode);
            //TODO перестроить дерево и пересчитать количества
            throw new NotImplementedException();
        }

        private void _mainView_EditWithdrawalTypeClick(object sender, EventArgs e)
        {
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

            IntermechTreeElement oldPreparation;
            try
            {
                oldPreparation = await _model.GetTreeFromFileAsync(result[0].Id);
            }
            catch (FileAttributeIsEmptyException)
            {
                MessageBox.Show("На выбранный Вами заказ нет никакой тех. подготовки");
                return;
            }


            IntermechTreeElementAdapter intermechTreeElementWrapper = new IntermechTreeElementAdapter(oldPreparation).Wrap(oldPreparation);

            TreeViewSettings settings = new TreeViewSettings();
            settings.AddColumn(new TreeColumn("Обозначение + наименование", 250), "Name");
            settings.ElementToBuild = intermechTreeElementWrapper;
                
            StructDialogViewPresenter<ViewNode, IntermechTreeElementAdapter> presenter = new StructDialogViewPresenter<ViewNode, IntermechTreeElementAdapter>(new StructDialogView(), new StructDialogViewModel<ViewNode, IntermechTreeElementAdapter>());
            presenter.Run(settings);

            if (settings.Result == null)
            {
                return;

            }

            IntermechTreeElementAdapter resultNode = (IntermechTreeElementAdapter)settings.Result;

            _model.CopyTechPreparation(resultNode.Root, _rootElement);
        }





        private void _mainView_EditStandartDetailsClick(object sender, EventArgs e)
        {
            //IntermechTreeElement mainElement = _rootElement as IntermechTreeElement;
            //MaterialsView view = new MaterialsView();
            //MaterialsViewPresenter presenter = new MaterialsViewPresenter(view, mainElement, IntermechObjectTypes.StandartDetails);
            //presenter.Run();
        }


        private void _mainView_EditMainMaterialsClick(object sender, EventArgs e)
        {
            //IntermechTreeElement mainElement = _rootElement;
            //MaterialsView view = new MaterialsView();
            //MaterialsViewPresenter presenter = new MaterialsViewPresenter(view, mainElement, IntermechObjectTypes.Material);
            //presenter.Run();
        }

        private void _mainView_UpdateClick(object sender, EventArgs e)
        {
            IPresenter<IntermechTreeElement> presenter =
                _presentationFactory.GetPresenter<TreeComparerPresenter, IntermechTreeElement>();
            presenter.Run(_rootElement);
        }

        private void _mainView_EditTechRoutesClick(object sender, EventArgs e)
        {
            IPresenter<Parameter<IntermechTreeElement>, TechRouteNode, IDictionary<long, Agent>> presenter = _presentationFactory.GetPresenter<TechRouteMapPresenter, Parameter<IntermechTreeElement>, TechRouteNode, IDictionary<long, Agent>>();
            Parameter<IntermechTreeElement> parameter = new Parameter<IntermechTreeElement>();
            parameter.AddParameter(_rootElement);
            presenter.Run(parameter, _organizationStruct, _agents);
        }

        private void _mainView_ClearManufacturerClick(object sender, BoundTreeElementEventArgs e)
        {
            IntermechTreeElement selectedElement = e.Element;
            
            // проходом в ширину очистить всем входящим изготовителя
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(selectedElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.Agent = string.Empty;
                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    child.Agent = string.Empty;
                    queue.Enqueue(child);
                }
            }
            _mainView.UpdateAgent(string.Empty);

            // TODO по идее надо еще и вверх подняться, чтобы убрать 
        }

        private void _mainView_CellValueChanged(object sender, TreeNodeAgentValueEventArgs e)
        {
            string agentsValues = GatherAgents(e);
            e.TreeElement.Agent = agentsValues;

            // Раздаем кооперацию потомкам
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            foreach (IntermechTreeElement child in e.TreeElement.Children)
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

            //раздаем изготовителя родителям
            SetParentCooperationRecursive(e.TreeElement, e.Key);
            _mainView.UpdateAgent(e.TreeElement.Agent);

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


        private async void _mainView_ApplyButtonClick(object sender, EventArgs e)
        {
            IntermechTreeElement mainTreeElement = _rootElement;
            mainTreeElement.Note = _mainView.GetNote();
            await _model.WriteIntoFileAttributeAsync(_rootVersionId, mainTreeElement);
        }

        private void _mainView_NodeMouseClick(object sender, TreeNodeClickEventArgs e)
        {
            // TODO Здесь должна появиться проверка на изменение предыдущего узла, если было какое-либо изменение


            if (e.Element.Agent != null)
            {
                _mainView.UpdateAgent(e.Element.Agent);
            }

        }

        /// <summary>
        /// The _view_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private async void _view_Load(object sender, EventArgs e)
        {
            TreeModel treeModel = new TreeModel();
            ViewNode waitingNode = new ViewNode();
            waitingNode.Name = "Пожалуйста, подождите. Идет загрузка данных";
            treeModel.Nodes.Add(waitingNode);
            _mainView.FillTree(treeModel);
            _mainView.LockButtons();

            // загрузить производителей
            ICollection<Agent> agents = await _model.GetAllAgentsAsync();
            _agents = new Dictionary<long, Agent>();
            foreach (Agent agent in agents)
            {
                _agents.Add(agent.Id, agent);
            }
            
            // проверка наличия файла со структурой предприятия
            bool fileStructEmpty = await _model.AttributeExist(_rootVersionId, ConstHelper.OrganizationStructAttribute);
            if (!fileStructEmpty)
            {
                // загрузить структуру предприятия из справочника Imbase
                _organizationStruct = await _model.GetWorkShopsAsync();

                TechRouteNodeAdapter organizationStructWrapper = new TechRouteNodeAdapter(_organizationStruct).Wrap(_organizationStruct);

                TreeViewSettings settings = new TreeViewSettings();
                settings.AddColumn(new TreeColumn("Наименование", 250), "Name");
                settings.ElementToBuild = organizationStructWrapper;
                
                StructDialogViewPresenter<TechRouteNodeView, TechRouteNodeAdapter> presenter = new StructDialogViewPresenter<TechRouteNodeView, TechRouteNodeAdapter>(new StructDialogView(), new StructDialogViewModel<TechRouteNodeView, TechRouteNodeAdapter>());
                presenter.Run(settings);

                if (settings.Result != null)
                {
                    TechRouteNodeAdapter resultNode = (TechRouteNodeAdapter)settings.Result;
                    _organizationStruct = resultNode.TechRouteNode;
                    await _model.WriteBlobAttributeAsync<TechRouteNode>(_rootVersionId, resultNode.TechRouteNode, ConstHelper.OrganizationStructAttribute, "Структура предприятия " + resultNode.Name);
                }
                else
                {
                    MessageBox.Show("Не указана структура предприятия: закройте окно тех. подготовки и откройте заново");
                    return;
                }
            }
            else
            {
                _organizationStruct = await _model.ReadDataFromBlobAttribute<TechRouteNode>(_rootVersionId, ConstHelper.OrganizationStructAttribute);
            }


            // если при загрузке производитель не выбран, то ничего не будем грузить
            if (_organizationStruct.ManufacturerId == 0)
            {
                throw new OrganizationStructNotDownloadedException(
                    "У корневого элемента дерева организации не указан код организации-производителя");
            }

            // пробуем загрузить из файла
            bool fromFile = false;

            try
            {
                _rootElement = await _model.GetTreeFromFileAsync(_rootVersionId);
                if (_rootElement.Id != _rootVersionId)
                {
                    _rootElement.Id = _rootVersionId;
                }
                fromFile = true;
            }
            catch (FileAttributeIsEmptyException)
            {
                fromFile = false;
            }

            // Если не из файла, то надо загрузить из атрибута "Двоичные данные заказа"
            if (!fromFile)
            {
                // проверить атрибут
                if (!await _model.AttributeExist(_rootVersionId,17964))
                {
                    throw new FileAttributeIsEmptyException("У заказа отсутствует заполненный атрибут Двоичные данные заказа. Обратитесь к сотруднику, составлявшему заказ!");
                }

                _rootElement = await _model.ReadDataFromBlobAttribute<IntermechTreeElement>(_rootVersionId, 17964);
                _rootElement.Agent = _organizationStruct.ManufacturerId.ToString();

                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(_rootElement);
                while (queue.Count > 0)
                {
                    IntermechTreeElement currentElement = queue.Dequeue();
                    currentElement.Agent = _rootElement.Agent;
                    foreach (IntermechTreeElement child in currentElement.Children)
                    {
                        queue.Enqueue(child);
                    }
                }

            }

            _mainView.FillNote(_rootElement.Note);
            _mainView.FillGrid(agents);
            treeModel = _model.GetTreeModel(_rootElement);
            _mainView.FillTree(treeModel);
            string orderName = string.Format($"{_rootElement.Name} {_agents[long.Parse(_rootElement.Agent)]}. Тех. подготовка");
            _mainView.UpdateCaptionText(orderName);
            _mainView.UnLockButtons();
        }




        /// <summary>
        /// Запуск главной формы
        /// </summary>
        /// <param name="rootVersionId">
        /// идентификатор версии объекта заказа
        /// </param>
        public override void Run(long rootVersionId)
        {
            _rootVersionId = rootVersionId;
            _mainView.Show();
        }
    }
}