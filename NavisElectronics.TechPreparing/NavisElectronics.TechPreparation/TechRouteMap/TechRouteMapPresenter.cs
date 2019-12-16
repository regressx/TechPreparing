﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechRouteMapPresenter.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Класс-посредник, представитель для формы регистрации маршрутов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.TechRouteMap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using Entities;
    using EventArguments;
    using Interfaces.Entities;
    using Presenters;
    using Reports;
    using Services;
    using ViewInterfaces;
    using ViewModels;
    using ViewModels.TreeNodes;
    using Views;

    /// <summary>
    /// Класс-посредник, представитель для формы регистрации маршрутов 
    /// </summary>
    internal class TechRouteMapPresenter : IPresenter<Parameter<IntermechTreeElement>, TechRouteNode, IDictionary<long, Agent>>
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
        /// Класс-логика для событий представления
        /// </summary>
        private readonly TechRoutesMapModel _model;

        private readonly IPresentationFactory _presentationFactory;

        /// <summary>
        /// Класс, позволяющий по номеру объекта показать его файлы
        /// </summary>
        private readonly ShowFileManager _showFileManager;

        /// <summary>
        /// Представляет собой узел из Imbase, в котором представлена структура предприятия
        /// </summary>
        private TechRouteNode _organizationStruct;

        /// <summary>
        /// Поле служит на передачи параметров
        /// </summary>
        private Parameter<IntermechTreeElement> _parameter;

        private IDictionary<long, Agent> _agents;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRouteMapPresenter"/> class. 
        /// Конструктор посредника для ведомости маршрутов
        /// </summary>
        /// <param name="view">
        /// Окно ведомости маршрутов
        /// </param>
        /// <param name="model">
        /// модель для окна
        /// </param>
        /// <param name="presentationFactory">
        /// The presentation Factory.
        /// </param>
        public TechRouteMapPresenter(ITechRouteMap view, TechRoutesMapModel model, IPresentationFactory presentationFactory)
        {
            _view = view;
            _view.EditTechRouteClick += _view_EditTechRouteClick;
            _view.DeleteRouteClick += View_DeleteRouteClick;
            _view.Load += _view_Load;
            _view.EditNoteClick += View_EditNoteClick;
            _view.CopyClick += View_CopyClick;
            _view.PasteClick += View_PasteClick;
            _view.ShowClick += _view_ShowClick;
            _view.GoToOldArchive += _view_GoToOldArchive;
            _view.CreateReportClick += View_CreateReportClick;
            _view.CreateDevideList += View_CreateDevideList;
            _view.SetInnerCooperation += View_SetInnerCooperation;
            _view.RemoveInnerCooperation += View_RemoveInnerCooperation;
            _view.CreateCooperationList += _view_CreateCooperationList;
            _view.DownloadInfoFromIPS += OnDownloadFromIPS;
            _view.EditMassTechRouteClick += View_EditMassTechRouteClick;
            _view.RefreshTree += View_RefreshTree;
            _view.UpdateNodeFromIps += View_UpdateNodeFromIps;
            _model = model;
            _presentationFactory = presentationFactory;
        }

        private async void View_UpdateNodeFromIps(object sender, EventArgs e)
        {
            ICollection<MyNode> selectedRows = _view.GetSelectedRows();
            foreach (MyNode node in selectedRows)
            {
               await _model.UpdateNodeFromIPS(node, _organizationStruct);
            }
        }

        private void View_RefreshTree(object sender, EventArgs e)
        {
            TreeModel model = _model.GetTreeModel(_root, _root.Agent, _organizationStruct, _agents);
            _view.SetTreeModel(model);
        }

        private void View_EditMassTechRouteClick(object sender, EditTechRouteEventArgs e)
        {
            IList<MyNode> selectedRows = _view.GetSelectedRows().ToList();
            TechRouteDialog dialog = new TechRouteDialog(_view.GetMainNode(), selectedRows[0], _presentationFactory, _organizationStruct);
            dialog.ShowDialog();
            _view.GetTreeView().Refresh();
        }

        private async void OnDownloadFromIPS(object sender, EventArgs e)
        {
            _model.DownloadTechInfoFromIPS(_view.GetMainNode());
        }

        private void View_DeleteRouteClick(object sender, ClipboardEventArgs e)
        {
            ICollection<MyNode> nodes = e.Nodes;
            foreach (MyNode node in nodes)
            {
                node.Route = string.Empty;
                ((IntermechTreeElement)node.Tag).TechRoute = string.Empty;
            }
        }

        /// <summary>
        /// Событие перехода к старому архиву предприятия
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _view_GoToOldArchive(object sender, SaveClickEventArgs e)
        {
            _model.GoToOldArchive(e.Node.Designation);
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

            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (MyNode myNode in e.Nodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            foreach (IntermechTreeElement element in rows)
            {
                _model.SetInnerCooperation(element, false);
            }


            // проходим по дереву и расставляем галки внутрипроизводственной кооперации уже на объекты View
            Queue<MyNode> queue = new Queue<MyNode>();
            queue.Enqueue(_view.GetMainNode());
            while (queue.Count > 0)
            {
                MyNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement intermechElement = (IntermechTreeElement)nodeFromQueue.Tag;
                nodeFromQueue.InnerCooperation = intermechElement.InnerCooperation;
                nodeFromQueue.ContainsInnerCooperation = intermechElement.ContainsInnerCooperation;
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
            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (MyNode myNode in e.Nodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            foreach (IntermechTreeElement element in rows)
            {
                _model.SetInnerCooperation(element, true);
            }

            // проходим по дереву и расставляем галки внутрипроизводственной кооперации уже на объекты View
            Queue<MyNode> queue = new Queue<MyNode>();
            queue.Enqueue(_view.GetMainNode());
            while (queue.Count > 0)
            {
                MyNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement intermechElement = (IntermechTreeElement)nodeFromQueue.Tag;
                nodeFromQueue.InnerCooperation = intermechElement.InnerCooperation;
                nodeFromQueue.ContainsInnerCooperation = intermechElement.ContainsInnerCooperation;
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
            _model.ShowProductCard(element);
        }

        private void View_PasteClick(object sender, ClipboardEventArgs e)
        {
            _model.Paste(e.Nodes);
        }

        private void View_CopyClick(object sender, ClipboardEventArgs e)
        {
            _model.Copy(e.Nodes);
        }

        private void View_EditNoteClick(object sender, SaveClickEventArgs e)
        {
            IList<MyNode> elements = _view.GetSelectedRows().ToList();
            string note = string.Empty;

            if (elements.Count == 1)
            {
                IntermechTreeElement element = (IntermechTreeElement)elements[0].Tag;
                note = element.RouteNote;
            }

            using (AddNoteForm noteForm = new AddNoteForm(note))
            {
                AddNotePresenter notePresenter = new AddNotePresenter(noteForm);
                if (notePresenter.RunDialog() == DialogResult.OK)
                {
                    foreach (MyNode myNode in elements)
                    {
                        IntermechTreeElement intermechNode = (IntermechTreeElement)myNode.Tag;
                        string str = notePresenter.GetNote();
                        intermechNode.RouteNote = str;
                        myNode.Note = str;
                    }
                }
            }
        }

        private void _view_Load(object sender, EventArgs e)
        {
            TreeModel model = _model.GetTreeModel(_root, _root.Agent, _organizationStruct, _agents);
            _view.SetTreeModel(model);
        }

        private void _view_EditTechRouteClick(object sender, EditTechRouteEventArgs e)
        {
            IPresenter<TechRouteNode, IList<TechRouteNode>> presenter = _presentationFactory.GetPresenter<TechRoutePresenter, TechRouteNode, IList<TechRouteNode>>();
            IList<TechRouteNode> resultNodesList = new List<TechRouteNode>();

            presenter.Run(_organizationStruct, resultNodesList);

            if (resultNodesList.Count == 0)
            {
                return;
            }

            ICollection<MyNode> elements = _view.GetSelectedRows();

            foreach (MyNode element in elements)
            {
                IntermechTreeElement treeElement = (IntermechTreeElement)element.Tag;
                IList<TechRouteNode> nodes = resultNodesList;
                StringBuilder stringId = new StringBuilder();
                StringBuilder caption = new StringBuilder();
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

                    string oldTechRouteCodes = treeElement.TechRoute;
                    string newTechRouteCodes = string.Format("{0}{1}", oldTechRouteCodes, stringId.ToString());
                    treeElement.TechRoute = newTechRouteCodes;

                    string oldCaption = element.Route;
                    element.Route = oldCaption + caption;
                }
                else
                {
                    if (nodes.Count > 0)
                    {
                        stringId.Append(nodes[0].Id.ToString());
                        caption.Append(nodes[0].GetCaption());
                    }

                    for (int i = 1; i < nodes.Count; i++)
                    {
                        stringId.Append(";" + nodes[i].Id.ToString());
                        caption.Append("-" + nodes[i].GetCaption());
                    }

                    element.Route = caption.ToString();
                    treeElement.TechRoute = stringId.ToString();
                }
            }

        }

        /// <summary>
        /// Передача параметров, запуск формы
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Run(Parameter<IntermechTreeElement> parameter, TechRouteNode techRouteNode, IDictionary<long, Agent> agents)
        {
            _agents = agents;
            _parameter = parameter;
            _organizationStruct = techRouteNode;
            _root = parameter.GetParameter(0);
            _view.Show();
        }
    }
}