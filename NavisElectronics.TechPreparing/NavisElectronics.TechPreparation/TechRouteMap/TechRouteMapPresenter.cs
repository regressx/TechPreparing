// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechRouteMapPresenter.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Класс-посредник, представитель для формы регистрации маршрутов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Main;

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
    internal class TechRouteMapPresenter : IPresenter<Parameter<object>, TechRouteNode, IDictionary<long, Agent>>
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

        // пихаем в словарь для быстрого поиска
        private IDictionary<long, TechRouteNode> _organizationDictionary;

        /// <summary>
        /// Поле служит на передачи параметров
        /// </summary>
        private Parameter<object> _parameter;

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
            _view.DeleteRouteClick += View_DeleteRouteClick;
            _view.Load += _view_Load;
            _view.EditNoteClick += View_EditNoteClick;
            _view.ShowClick += _view_ShowClick;
            _view.GoToOldArchive += _view_GoToOldArchive;
            _view.CreateReportClick += View_CreateReportClick;
            _view.CreateDevideList += View_CreateDevideList;
            _view.SetInnerCooperation += View_SetInnerCooperation;
            _view.RemoveInnerCooperation += View_RemoveInnerCooperation;
            _view.CreateCooperationList += _view_CreateCooperationList;
            _view.DownloadInfoFromIPS += OnDownloadFromIPS;
            _view.EditMassTechRouteClick += View_EditMassTechRouteClick;
            _view.UpdateNodeFromIps += View_UpdateNodeFromIps;
            _model = model;
            _presentationFactory = presentationFactory;
        }

        private async void View_UpdateNodeFromIps(object sender, EventArgs e)
        {
            string productionTypeDialogResult = string.Empty;
            using (ProductionTypeDialog productionTypeDialog = new ProductionTypeDialog())
            {
                if (productionTypeDialog.ShowDialog() == DialogResult.OK)
                {
                    productionTypeDialogResult = productionTypeDialog.ProductionTypeValue;
                }
            }

            if (productionTypeDialogResult == string.Empty)
            {
                return;
            }

            ICollection<MyNode> selectedRows = _view.GetSelectedRows();

            foreach (MyNode node in selectedRows)
            {
               await _model.UpdateNodeFromIPS(node, _organizationDictionary, _organizationStruct.Name, productionTypeDialogResult);
            }
        }

        private void View_EditMassTechRouteClick(object sender, EditTechRouteEventArgs e)
        {
            throw new NotImplementedException("Обработчик события отсутствует!");
        }

        private async void OnDownloadFromIPS(object sender, EventArgs e)
        {
           await _model.DownloadTechInfoFromIPS(_view.GetMainNode());
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

        /// <summary>
        /// Передача параметров, запуск формы
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Run(Parameter<object> parameter, TechRouteNode techRouteNode, IDictionary<long, Agent> agents)
        {
            _agents = agents;
            _parameter = parameter;
            _organizationStruct = techRouteNode;

            // пихаем в словарь для быстрого поиска
            _organizationDictionary = new Dictionary<long, TechRouteNode>();
            Queue<TechRouteNode> queue = new Queue<TechRouteNode>();
            queue.Enqueue(_organizationStruct);
            while (queue.Count > 0)
            {
                TechRouteNode nodeFromQueue = queue.Dequeue();
                if (!_organizationDictionary.ContainsKey(nodeFromQueue.Id))
                {
                    _organizationDictionary.Add(nodeFromQueue.Id, nodeFromQueue);
                }
                foreach (TechRouteNode child in nodeFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }

            _root = (IntermechTreeElement)parameter.GetParameter(0);
            _view.MdiParent = (Form)(IMainView)parameter.GetParameter(1);
            _view.Show();
        }
    }
}