// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechRouteMapPresenter.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Класс-посредник, представитель для формы регистрации маршрутов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;
    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.IO;
    using NavisElectronics.TechPreparation.Reports;
    using NavisElectronics.TechPreparation.Services;
    using NavisElectronics.TechPreparation.ViewInterfaces;
    using NavisElectronics.TechPreparation.ViewModels;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;
    using NavisElectronics.TechPreparation.Views;

    /// <summary>
    /// Класс-посредник, представитель для формы регистрации маршрутов 
    /// </summary>
    public class TechRouteMapPresenter : IPresenter<Parameter<IntermechTreeElement>>
    {
        /// <summary>
        /// Интерфейс представления
        /// </summary>
        private readonly ITechRouteMap _view;

        /// <summary>
        /// Главный элемент дерева
        /// </summary>
        private readonly IntermechTreeElement _element;

        /// <summary>
        /// Строка-фильтр для агентов
        /// </summary>
        private readonly string _agentFilter;

        /// <summary>
        /// Словарик, чтобы быстро выгребать данные по агентам
        /// </summary>
        private readonly IDictionary<long, Agent> _agents;

        /// <summary>
        /// Класс-логика для событий представления
        /// </summary>
        private readonly TechRoutesMapModel _model;

        /// <summary>
        /// Класс, позволяющий по номеру объекта показать его файлы
        /// </summary>
        private readonly ShowFileManager _showFileManager;

        /// <summary>
        /// Представляет собой узел из Imbase, в котором представлена структура предприятия
        /// </summary>
        private TechRouteNode _techRouteNode;

        /// <summary>
        /// Поле служит на передачи параметров
        /// </summary>
        private Parameter<IntermechTreeElement> _parameter;

        /// <summary>
        /// Переменная, от чьего лица мы смотрим ведомость тех. маршрутов
        /// </summary>
        private Agent _mainManufacturer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRouteMapPresenter"/> class. 
        /// Конструктор посредника для ведомости маршрутов
        /// </summary>
        /// <param name="view">
        /// Окно ведомости маршрутов
        /// </param>
        /// <param name="element">
        /// Главный элемент дерева
        /// </param>
        /// <param name="agentFilter">
        /// Фильтр по изготовителю
        /// </param>
        /// <param name="agents">
        /// Существующие контрагенты
        /// </param>
        public TechRouteMapPresenter( ITechRouteMap view,  IntermechTreeElement element,  string agentFilter,  IDictionary<long, Agent> agents)
        {
            _view = view;
            _element = element;
            _agentFilter = agentFilter;
            _agents = agents;
            _view.EditTechRouteClick += _view_EditTechRouteClick;
            _view.Load += _view_Load;
            _view.SaveClick += _view_SaveClick;
            _view.EditClick += _view_EditClick;
            _view.CopyClick += _view_CopyClick;
            _view.PasteClick += _view_PasteClick;
            _view.ShowClick += _view_ShowClick;
            _view.CreateReportClick += View_CreateReportClick;
            _view.CreateDevideList += View_CreateDevideList;
            _view.SetInnerCooperation += View_SetInnerCooperation;
            _view.RemoveInnerCooperation += View_RemoveInnerCooperation;
            _view.SetNodesToComplectClick += _view_SetNodesToComplectClick;
            _view.CreateCooperationList += _view_CreateCooperationList;
            _model = new TechRoutesMapModel();
            _showFileManager = new ShowFileManager();

        }

        private void _view_CreateCooperationList(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService();
            ICollection<MyNode> elements = _view.GetSelectedRows();
            foreach (MyNode node in elements)
            {
                reportService.CreateReport(node, node.Name, ReportType.ListOfCooperation, DocumentType.Intermech, _mainManufacturer);
            }
        }

        private void _view_SetNodesToComplectClick(object sender, EventArgs e)
        {
            MyNode mainNode = _view.GetMainNode();

            switch (_agentFilter)
            {
                case "1372599":
                    Queue<MyNode> queue = new Queue<MyNode>();
                    queue.Enqueue(mainNode);
                    while (queue.Count > 0)
                    {
                        MyNode nodeFromQueue = queue.Dequeue();
                        IntermechTreeElement taggedElement = nodeFromQueue.Tag as IntermechTreeElement;
                        if (nodeFromQueue.Route != null)
                        {
                            if (nodeFromQueue.Route.Contains("773") || nodeFromQueue.Route.Contains("774") || nodeFromQueue.Route.Contains("103"))
                            {
                                nodeFromQueue.IsToComplect = true;
                                taggedElement.IsToComplect = true;
                            }

                        }
                        foreach (Node child in nodeFromQueue.Nodes)
                        {
                            queue.Enqueue((MyNode)child);
                        }
                    }

                    break;
                case "1299782":
                    Queue<MyNode> anotherQueue = new Queue<MyNode>();
                    anotherQueue.Enqueue(mainNode);
                    while (anotherQueue.Count > 0)
                    {
                        MyNode nodeFromQueue = anotherQueue.Dequeue();
                        IntermechTreeElement taggedElement = nodeFromQueue.Tag as IntermechTreeElement;
                        if (nodeFromQueue.Route != null)
                        {
                            if (nodeFromQueue.Route.Contains("756") || nodeFromQueue.Route.Contains("106"))
                            {
                                nodeFromQueue.IsToComplect = true;
                                taggedElement.IsToComplect = true;
                            }
                        }

                        foreach (Node child in nodeFromQueue.Nodes)
                        {
                            anotherQueue.Enqueue((MyNode)child);
                        }
                    }
                    break;
            }

        }

        /// <summary>
        /// Обработчик события удаления внутрипроизводственной кооперации
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void View_RemoveInnerCooperation(object sender, ClipboardEventArgs e)
        {
            //SetParametersService parametersService = new SetParametersService(_element);

            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (MyNode myNode in e.Nodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            //parametersService.SetInnerCooperationValue(rows, false);

            Queue<MyNode> queue = new Queue<MyNode>();
            queue.Enqueue(_view.GetMainNode());
            while (queue.Count > 0)
            {
                MyNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement intermechElement = nodeFromQueue.Tag as IntermechTreeElement;
                nodeFromQueue.InnerCooperation = intermechElement.InnerCooperation;

                if (nodeFromQueue.Nodes.Count > 0)
                {
                    foreach (Node coopNode in nodeFromQueue.Nodes)
                    {
                        queue.Enqueue((MyNode)coopNode);
                    }
                }
            }
        }

        private void View_SetInnerCooperation(object sender, ClipboardEventArgs e)
        {
            //SetParametersService parametersService = new SetParametersService(_element);

            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (MyNode myNode in e.Nodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            //parametersService.SetInnerCooperationValue(rows, true);

            // проходим по дереву и расставляем галки внутрипроизводственной кооперации уже на объекты View
            Queue<MyNode> queue = new Queue<MyNode>();
            queue.Enqueue(_view.GetMainNode());
            while (queue.Count > 0)
            {
                MyNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement intermechElement = nodeFromQueue.Tag as IntermechTreeElement;
                nodeFromQueue.InnerCooperation = intermechElement.InnerCooperation;

                if (nodeFromQueue.Nodes.Count > 0)
                {
                    foreach (Node coopNode in nodeFromQueue.Nodes)
                    {
                        queue.Enqueue((MyNode)coopNode);
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки по созданию разделительной ведомости
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void View_CreateDevideList(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService();
            ICollection<MyNode> elements = _view.GetSelectedRows();
            foreach (MyNode node in elements)
            {
                reportService.CreateReport(node, node.Name, ReportType.DividingList, DocumentType.Intermech, _mainManufacturer);
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки по созданию ведомости тех. маршрутов
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void View_CreateReportClick(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService();
            ICollection<MyNode> elements = _view.GetSelectedRows();
            foreach (MyNode node in elements)
            {
                reportService.CreateReport(node, node.Name, ReportType.ListOfTechRoutes, DocumentType.Intermech, _mainManufacturer);
            }

        }

        private void _view_ShowClick(object sender, SaveClickEventArgs e)
        {
            IntermechTreeElement element = e.Node.Tag as IntermechTreeElement;
            _showFileManager.Show(element.Id, element.Type);
        }

        private void _view_PasteClick(object sender, ClipboardEventArgs e)
        {
            _model.Paste(e.Nodes, _agentFilter);
        }

        private void _view_CopyClick(object sender, ClipboardEventArgs e)
        {
            _model.Copy(e.Nodes, _agentFilter);
        }

        private void _view_EditClick(object sender, SaveClickEventArgs e)
        {

            IList<MyNode> elements = _view.GetSelectedRows().ToList();
            string note = string.Empty;
            TechAgentDataExtractor dataExtractor = new TechAgentDataExtractor();

            if (elements.Count == 1)
            {
                IntermechTreeElement element = elements[0].Tag as IntermechTreeElement;

                note = dataExtractor.ExtractData(element.RouteNote,_agentFilter);
            }

            using (AddNoteForm noteForm = new AddNoteForm(note))
            {

                AddNotePresenter notePresenter = new AddNotePresenter(noteForm);
                if (notePresenter.RunDialog() == DialogResult.OK)
                {
                    foreach (MyNode myNode in elements)
                    {
                        IntermechTreeElement intermechNode = myNode.Tag as IntermechTreeElement;
                        string str = notePresenter.GetNote();

                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("<{0}:{1}/>", _agentFilter, str);

                        string temp = string.Empty;

                        if (intermechNode.RouteNote != null)
                        {
                            temp = dataExtractor.RemoveData(intermechNode.RouteNote, _agentFilter);
                            if (!temp.Contains('<'))
                            {
                                temp = string.Empty;
                            }
                        }
                        intermechNode.RouteNote = string.Format("{0}{1}", temp, sb);
                        myNode.Note = str;
                    }
                }
            }
        }

        private void _view_SaveClick(object sender, SaveClickEventArgs e)
        {
            IntermechTreeElement rootElement = e.Node.Tag as IntermechTreeElement;
            DataSetGatheringService gatheringService = new DataSetGatheringService();
            System.Data.DataSet ds = gatheringService.Gather(rootElement);
            IntermechWriter writer = new IntermechWriter();
            writer.WriteDataSet(rootElement.Id, ds);
        }

        private async void _view_Load(object sender, System.EventArgs e)
        {
            TreeModel treeModel = new TreeModel();
            _techRouteNode = await _model.GetWorkShops();
            if (_agentFilter == ((int)AgentsId.NavisElectronics).ToString())
            {
                _techRouteNode.Children.RemoveAt(0);
                _mainManufacturer = _agents[(long)AgentsId.NavisElectronics];
            }
            else
            {
                _techRouteNode.Children.RemoveAt(1);
                _mainManufacturer = _agents[(long)AgentsId.Kb];
            }
            MyNode mainNode = _model.BuildTree(_element, _techRouteNode, _agentFilter, _agents);
            treeModel.Nodes.Add(mainNode);
            _view.SetTreeModel(treeModel);

        }

        public void Run()
        {
            _view.Show();
        }

        private void _view_EditTechRouteClick(object sender, EditTechRouteEventArgs e)
        {
            using (TechRouteEditView techRouteView = new TechRouteEditView("Маршрут элемента", _techRouteNode))
            {
                TechRouteModel techRouteModel = new TechRouteModel();
                TechRoutePresenter presenter = new TechRoutePresenter(techRouteView, techRouteModel, _techRouteNode);
                if (presenter.Run() == DialogResult.OK)
                {

                    ICollection<MyNode> elements = _view.GetSelectedRows();
                    TechAgentDataExtractor dataExtractor = new TechAgentDataExtractor();

                    foreach (MyNode element in elements)
                    {
                        IntermechTreeElement treeElement = element.Tag as IntermechTreeElement;
                        StringBuilder stringId = new StringBuilder();
                        StringBuilder caption = new StringBuilder();
                        IList<TechRouteNode> nodes = techRouteModel.GetTechRouteNodes();

                        if (e.Append)
                        {
                            if (nodes.Count > 0)
                            {
                                stringId.AppendFormat("|| {0}", nodes[0].Id.ToString());
                                caption.AppendFormat(" \\ {0}", nodes[0].GetCaption());
                            }

                            for (int i = 1; i < nodes.Count; i++)
                            {
                                stringId.AppendFormat(";{0}", nodes[i].Id.ToString());
                                caption.AppendFormat("-{0}", nodes[i].GetCaption());
                            }

                            string oldTechRouteCodes = string.Format("<{0}:{1}/>", _agentFilter, dataExtractor.ExtractData(treeElement.TechRoute, _agentFilter));
                            int index = oldTechRouteCodes.IndexOf("/>", StringComparison.Ordinal);
                            if (index > 0)
                            {
                                string newTechRouteCodes = oldTechRouteCodes.Insert(index, stringId.ToString());

                                string temp = string.Empty;
                                if (treeElement.TechRoute != null)
                                {
                                    temp = dataExtractor.RemoveData(treeElement.TechRoute, _agentFilter);
                                }
                                treeElement.TechRoute = string.Format("{0}{1}", temp, newTechRouteCodes);
                            }

                            string oldCaption = element.Route;

                            element.Route = oldCaption + caption;
                        }
                        else
                        {
                            if (nodes.Count > 0)
                            {
                                stringId.AppendFormat("<{0}:{1}", _agentFilter, nodes[0].Id.ToString());
                                caption.AppendFormat(nodes[0].GetCaption());
                            }

                            for (int i = 1; i < nodes.Count; i++)
                            {
                                stringId.AppendFormat(";{0}", nodes[i].Id.ToString());
                                caption.AppendFormat("-{0}", nodes[i].GetCaption());
                            }

                            stringId.AppendFormat("/>");
                            element.Route = caption.ToString();
                            string temp = string.Empty;
                            if (treeElement.TechRoute != null)
                            {
                                temp = dataExtractor.RemoveData(treeElement.TechRoute, _agentFilter);
                            }

                            treeElement.TechRoute = string.Format("{0}{1}", temp, stringId);
                        }


                    }
                }
            }
        }

        public void Run(Parameter<IntermechTreeElement> parameter)
        {
            _parameter = parameter;
            _view.Show();
        }
    }
}