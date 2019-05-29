namespace NavisElectronics.ListOfCooperation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using Entities;
    using EventArguments;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Client;
    using Intermech.Navigator;
    using IO;
    using Reports;
    using Services;
    using ViewInterfaces;
    using ViewModels;
    using Views;

    /// <summary>
    /// Класс-посредник между логикой и представлением для ведомости кооперации
    /// </summary>
    public class CooperationPresenter
    {
        /// <summary>
        /// Интерфейс для окна ведомости кооперации
        /// </summary>
        private readonly ICooperationView _view;

        /// <summary>
        /// Главный элемент дерева (по сути заказ)
        /// </summary>
        private readonly IntermechTreeElement _element;

        /// <summary>
        /// Ссылка на сервис собирания Dataset из элемента дерева
        /// </summary>
        private readonly DataSetGatheringService _gatheringService;

        /// <summary>
        /// Ссылка на сервис для указания параметров элементов, таких как объем выборки и тех. запас
        /// </summary>
        private readonly SetParametersService _parametersService;

        /// <summary>
        /// Модель для представления. Умеет строить дерево. 
        /// </summary>
        private readonly CooperationViewModel _model;

        /// <summary>
        /// Это уже элемент старого дерева, неотфильтрованного по изготовителю
        /// </summary>
        private IntermechTreeElement _oldElement;


        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationPresenter"/> class.
        /// </summary>
        /// <param name="view">
        /// Интерфейс окна
        /// </param>
        /// <param name="element">
        /// Главный элемент дерева. Отфильтрованный по изготовителю
        /// </param>
        /// <param name="oldElement">
        /// Главный элемент самого полного дерева
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        public CooperationPresenter(ICooperationView view, IntermechTreeElement element, IntermechTreeElement oldElement, CooperationViewModel model)
        {
            _model = model;
            _view = view;
            _element = element;
            _oldElement = oldElement;
            _view.Load += _view_Load;
            _view.SaveClick += _view_SaveClick;
            _gatheringService = new DataSetGatheringService();
            _view.SetCooperationClick += _view_SetCooperationClick;
            _view.DeleteCooperationClick += _view_DeleteCooperationClick;
            _view.SetTechProcessReferenceClick += _view_SetTechProcessReferenceClick;
            _view.DeleteTechProcessReferenceClick += _view_DeleteTechProcessReferenceClick;
            _view.SetNoteClick += _view_SetNoteClick;
            _view.DeleteNoteClick += _view_DeleteNoteClick;
            _view.CheckListOfCooperation += _view_CheckListOfCooperation;
            _view.SetParametersClick += _view_SetParametersClick;
            _view.PutDownCooperation += _view_PutDownCooperation;
            _view.SearchInArchiveClick += _view_SearchInArchiveClick;
            _parametersService = new SetParametersService(element);
            _view.FindInTreeClick += _view_FindInTreeClick;
            _view.GlobalSearchClick += _view_GlobalSearchClick;
            _view.CreateCooperationClick += _view_CreateCooperationClick;
            _view.SetTechTaskClick += _view_SetTechTaskClick;
            _view.CreateCompleteListClick += _view_CreateCompleteListClick;
            _view.SetPcbClick += _view_SetPcbClick;
        }

        private void _view_SetPcbClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (CooperationNode myNode in e.SelectedNodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }
            foreach (IntermechTreeElement element in rows)
            {
                element.IsPCB = true;
                ICollection<IntermechTreeElement> elementsToCheckPcb = _element.Find(element.Id);
                foreach (IntermechTreeElement temp in elementsToCheckPcb)
                {
                    temp.IsPCB = true;
                }
            }
            foreach (CooperationNode myNode in e.SelectedNodes)
            {
                myNode.IsPcb = true;
            }
        }

        private void _view_CreateCompleteListClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            if (e.SelectedNodes == null)
            {
                throw new ArgumentNullException("e.SelectedNodes");
            }
            ReportService reportService = new ReportService();
            foreach (CooperationNode cooperationNode in e.SelectedNodes)
            {
                reportService.CreateReport(cooperationNode, cooperationNode.Name, ReportType.CompleteList, DocumentType.Intermech, null);
            }
        }

        private void _view_SetTechTaskClick(object sender, MultipleNodesSelectedEventArgs e)
        {

            string text = string.Empty;

            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();

            foreach (CooperationNode myNode in e.SelectedNodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            if (rows.Count == 1)
            {
                text = rows[0].Note;
            }

            using (AddNoteForm noteForm = new AddNoteForm(text))
            {
                AddNotePresenter notePresenter = new AddNotePresenter(noteForm);

                if (notePresenter.RunDialog() == DialogResult.OK)
                {

                    foreach (IntermechTreeElement element in rows)
                    {
                        element.TechTask = notePresenter.GetNote();
                    }

                    foreach (CooperationNode myNode in e.SelectedNodes)
                    {
                        myNode.TechTask = notePresenter.GetNote();
                    }

                }
            }


        }

        private void _view_CreateCooperationClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            if (e.SelectedNodes == null)
            {
                throw new ArgumentNullException("e.SelectedNodes");
            }
            ReportService reportService = new ReportService();
            foreach (CooperationNode cooperationNode in e.SelectedNodes)
            {
                reportService.CreateReport(cooperationNode, cooperationNode.Name, ReportType.ListOfCooperation, DocumentType.Intermech, null);
            }

        }

        private FindNodePresenter _presenter;
        private void _view_GlobalSearchClick(object sender, EventArgs e)
        {
            FindNodeView findNodeView = new FindNodeView();
            _presenter = new FindNodePresenter(findNodeView, _view.GetTreeView());
            _presenter.Run();
            _presenter.SearchInitClick += Presenter_SearchInitClick;
            _presenter.ViewClosing += Presenter_ViewClosing;
        }

        private void Presenter_ViewClosing(object sender, EventArgs e)
        {
            _presenter.SearchInitClick -= Presenter_SearchInitClick;
            _presenter.ViewClosing -= Presenter_ViewClosing;
        }

        private void _view_FindInTreeClick(object sender, EventArgs e)
        {

        }

        private void Presenter_SearchInitClick(object sender, TreeNodeAdv e)
        {
            _view.JumpToNode(e);
        }

        private void _view_SearchInArchiveClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            IList<CooperationNode> nodes = new List<CooperationNode>(e.SelectedNodes);
            ShowFileManager manager = new ShowFileManager();
            manager.Show(nodes[0].Id, nodes[0].Type);
        }

        private void _view_PutDownCooperation(object sender, System.EventArgs e)
        {
            //CooperationNode mainElement = _view.GetMainNode();
            //CooperationSetter setter = new CooperationSetter();
            //setter.SetCooperation(mainElement);
            throw new NotImplementedException();
        }

        private void _view_SetParametersClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            using (ParametersView parametersView = new ParametersView())
            {
                ParametersViewPresenter parametersPresenter = new ParametersViewPresenter(parametersView);

                if (parametersPresenter.Run() == DialogResult.OK)
                {
                    SetParameters(e.SelectedNodes, parametersPresenter.GetStockRate(), parametersPresenter.GetSampleSize());
                }
            }
        }

        private void _view_CheckListOfCooperation(object sender, System.EventArgs e)
        {
            _view.ClearOutput();
            _model.CheckReady(_view.GetMainNode());
        }

        private void _view_DeleteNoteClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            ICollection<IntermechTreeElement> rows = new List<IntermechTreeElement>();

            foreach (CooperationNode myNode in e.SelectedNodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
                myNode.Note = string.Empty;
            }

            foreach (IntermechTreeElement element in rows)
            {
                element.Note = string.Empty;
            }
        }

        private void _view_SetNoteClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            string text = string.Empty;

            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();

            foreach (CooperationNode myNode in e.SelectedNodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            if (rows.Count == 1)
            {
                text = rows[0].Note;
            }

            using (AddNoteForm noteForm = new AddNoteForm(text))
            {
                AddNotePresenter notePresenter = new AddNotePresenter(noteForm);

                if (notePresenter.RunDialog() == DialogResult.OK)
                {

                    foreach (IntermechTreeElement element in rows)
                    {
                        element.Note = notePresenter.GetNote();
                    }

                    foreach (CooperationNode myNode in e.SelectedNodes)
                    {
                        myNode.Note = notePresenter.GetNote();
                    }

                }
            }

        }

        private void _view_DeleteTechProcessReferenceClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            SetTechProcess(e.SelectedNodes, TechProcess.Empty);
        }

        private void _view_SetTechProcessReferenceClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            long[] result = SelectionWindow.SelectObjects("Выбор типового техпроцесса", "Выберите требуемый ТП", 1255, SelectionOptions.SelectObjects);
            if (result != null)
            {
                TechProcess tp = new TechProcess();

                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject techProcessObject = keeper.Session.GetObject(result[0]);
                    tp.Id = result[0];
                    tp.Name = techProcessObject.Caption;
                }

                SetTechProcess(e.SelectedNodes, tp);
            }
        }

        private void SetCooperation(bool cooperationFlag, ICollection<CooperationNode> nodes)
        {
            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (CooperationNode myNode in nodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            _parametersService.SetCooperationValue(rows, cooperationFlag);

            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            queue.Enqueue(_view.GetMainNode());
            while (queue.Count > 0)
            {
                CooperationNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement intermechElement = nodeFromQueue.Tag as IntermechTreeElement;
                nodeFromQueue.CooperationFlag = intermechElement.CooperationFlag;

                if (nodeFromQueue.Nodes.Count > 0)
                {
                    foreach (Node coopNode in nodeFromQueue.Nodes)
                    {
                        queue.Enqueue((CooperationNode)coopNode);
                    }
                }
            }
        }

        private void SetParameters(ICollection<CooperationNode> nodes, double stockRate, string sampleSize)
        {
            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (CooperationNode myNode in nodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            _parametersService.SetParameters(rows,stockRate,sampleSize);



            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            queue.Enqueue(_view.GetMainNode());
            while (queue.Count > 0)
            {
                CooperationNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement intermechElement = nodeFromQueue.Tag as IntermechTreeElement;

                nodeFromQueue.StockRate = intermechElement.StockRate.ToString("F3");
                nodeFromQueue.SampleSize = intermechElement.SampleSize;
                nodeFromQueue.TotalAmount = intermechElement.TotalAmount.ToString("F3");

                if (nodeFromQueue.Nodes.Count > 0)
                {
                    foreach (Node coopNode in nodeFromQueue.Nodes)
                    {
                        queue.Enqueue((CooperationNode)coopNode);
                    }
                }
            }

        }

        private void SetTechProcess(ICollection<CooperationNode> nodes, TechProcess techProcess)
        {
            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (CooperationNode myNode in nodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }
            _parametersService.SetTechProcess(rows, techProcess);

            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            queue.Enqueue(_view.GetMainNode());
            while (queue.Count > 0)
            {
                CooperationNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement intermechElement = nodeFromQueue.Tag as IntermechTreeElement;
                nodeFromQueue.TechProcessReference = intermechElement.TechProcessReference;

                if (nodeFromQueue.Nodes.Count > 0)
                {
                    foreach (Node coopNode in nodeFromQueue.Nodes)
                    {
                        queue.Enqueue((CooperationNode)coopNode);
                    }
                }
            }
        }


        private void _view_DeleteCooperationClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            SetCooperation(false, e.SelectedNodes);
        }

        private void _view_SetCooperationClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            SetCooperation(true, e.SelectedNodes);
        }

        private void _view_SaveClick(object sender, System.EventArgs e)
        {
            ChangesDefiner changesDefiner = new ChangesDefiner();
            IntermechTreeElement newTree = changesDefiner.GetChanges(_oldElement, _element);
            _oldElement = newTree;
            System.Data.DataSet ds = _gatheringService.Gather(newTree);
            IntermechWriter writer = new IntermechWriter();
            writer.WriteDataSet(newTree.Id, ds);
        }

        private void _view_Load(object sender, System.EventArgs e)
        {
            _view.FillTree(_model.GetModel(_element));
        }

        /// <summary>
        /// Запуск формы
        /// </summary>
        public void Run()
        {
            _view.Show();
        }
    }
}