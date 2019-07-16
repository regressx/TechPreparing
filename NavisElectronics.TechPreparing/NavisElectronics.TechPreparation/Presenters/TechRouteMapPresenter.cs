﻿// --------------------------------------------------------------------------------------------------------------------
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
    using Entities;
    using Enums;
    using EventArguments;
    using IO;
    using Reports;
    using Services;
    using ViewInterfaces;
    using ViewModels;
    using ViewModels.TreeNodes;
    using Views;

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
        private IntermechTreeElement _root;

        /// <summary>
        /// Строка-фильтр для агентов
        /// </summary>
        private string _agentFilter;

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
        private string _mainManufacturer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRouteMapPresenter"/> class. 
        /// Конструктор посредника для ведомости маршрутов
        /// </summary>
        /// <param name="view">
        /// Окно ведомости маршрутов
        /// </param>
        /// <param name="model">
        ///  модель для окна
        /// </param>
        public TechRouteMapPresenter(ITechRouteMap view, TechRoutesMapModel model)
        {
            _view = view;
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
            _model = model;
        }

        /// <summary>
        /// Метод создания ведомости кооперации
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _view_CreateCooperationList(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService();
            ICollection<MyNode> elements = _view.GetSelectedRows();
            foreach (MyNode node in elements)
            {
                reportService.CreateReport(node, node.Name, ReportType.ListOfCooperation, DocumentType.Intermech);
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
                reportService.CreateReport(node, node.Name, ReportType.DividingList, DocumentType.Intermech);
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
                reportService.CreateReport(node, node.Name, ReportType.ListOfTechRoutes, DocumentType.Intermech);
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
            IDictionary<long, Agent> agents = await _model.GetAgents();
            if (_agentFilter == ((int)AgentsId.NavisElectronics).ToString())
            {
                _techRouteNode.Children.RemoveAt(0);
            }
            else
            {
                _techRouteNode.Children.RemoveAt(1);
            }
            TreeModel model = _model.GetTreeModel(_root, _mainManufacturer, _agentFilter, _techRouteNode, agents);
            _view.SetTreeModel(model);

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

        /// <summary>
        /// Передача параметров, запуск формы
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Run(Parameter<IntermechTreeElement> parameter)
        {
            _root = parameter.GetParameter(0);
            _mainManufacturer = parameter.GetParameter(1).Agent;
            _agentFilter = parameter.GetParameter(2).Agent;
            _view.Show();
        }
    }
}