// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationPresenter.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Класс-посредник между логикой и представлением для ведомости кооперации
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Presenters
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
    using ViewInterfaces;
    using ViewModels;
    using ViewModels.TreeNodes;
    using Views;

    /// <summary>
    /// Класс-посредник между логикой и представлением для ведомости кооперации
    /// </summary>
    public class CooperationPresenter : IPresenter<Parameter<IntermechTreeElement>>
    {
        /// <summary>
        /// Интерфейс для окна ведомости кооперации
        /// </summary>
        private readonly ICooperationView _view;

        /// <summary>
        /// Модель для представления. Умеет строить дерево. 
        /// </summary>
        private readonly CooperationViewModel _model;

        /// <summary>
        /// Переданный параметр
        /// </summary>
        private Parameter<IntermechTreeElement> _parameter;

        /// <summary>
        /// Главный элемент дерева (по сути заказ)
        /// </summary>
        private IntermechTreeElement _root;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationPresenter"/> class.
        /// </summary>
        /// <param name="view">
        /// Интерфейс окна
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        public CooperationPresenter(ICooperationView view, CooperationViewModel model)
        {
            _model = model;
            _view = view;
            _view.Load += _view_Load;
            _view.SetCooperationClick += _view_SetCooperationClick;
            _view.DeleteCooperationClick += _view_DeleteCooperationClick;
            _view.SetTechProcessReferenceClick += _view_SetTechProcessReferenceClick;
            _view.DeleteTechProcessReferenceClick += _view_DeleteTechProcessReferenceClick;
            _view.SetNoteClick += _view_SetNoteClick;
            _view.DeleteNoteClick += _view_DeleteNoteClick;
            _view.CheckListOfCooperation += _view_CheckListOfCooperation;
            _view.SetParametersClick += _view_SetParametersClick;
            _view.SearchInArchiveClick += _view_SearchInArchiveClick;
            _view.GlobalSearchClick += _view_GlobalSearchClick;
            _view.SyncObjectsWithIPS += View_SyncWithIPS;
            _view.SetCooperationToPcb += View_SetCooperationToPcb;
        }

        /// <summary>
        /// The view_ set cooperation to pcb.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void View_SetCooperationToPcb(object sender, EventArgs e)
        {
            _model.SetCooperationToPcb(_root);

            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            foreach (CooperationNode node in _view.GetMainNodes())
            {
                queue.Enqueue(node);
            }
            while (queue.Count > 0)
            {
                CooperationNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement intermechElement = (IntermechTreeElement)nodeFromQueue.Tag;

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


        /// <summary>
        /// The view_ set tech task click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private async void View_SyncWithIPS(object sender, MultipleNodesSelectedEventArgs e)
        {
            foreach (CooperationNode myNode in e.SelectedNodes)
            {
                IntermechTreeElement synchronizedElement = (IntermechTreeElement)myNode.Tag;
                await _model.UpdateElementDataFromDatabase(synchronizedElement.Id, synchronizedElement);
                myNode.TechTask = synchronizedElement.TechTask;
                myNode.PcbVersion = synchronizedElement.PcbVersion;
                myNode.IsPcb = synchronizedElement.IsPcb;
                myNode.Name = synchronizedElement.Name;
            }

        }

        private void _view_GlobalSearchClick(object sender, EventArgs e)
        {
            FindNodeView findNodeView = new FindNodeView(_view.GetTreeView());
            findNodeView.Show();
        }

        private void _view_SearchInArchiveClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            List<CooperationNode> nodes = new List<CooperationNode>(e.SelectedNodes);
            CooperationNode selectedNode = nodes[0];
            _model.OpenInOldArchive(selectedNode.Designation);

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
            //_model.CheckReady(_root);
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

        /// <summary>
        /// The set cooperation.
        /// </summary>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        private void SetCooperation(ICollection<CooperationNode> nodes)
        {
            IList<CooperationNode> rows = new List<CooperationNode>();
            foreach (CooperationNode myNode in nodes)
            {
                rows.Add(myNode);
            }

            Func<CooperationNode, CooperationNode> func = (cooperationNode) =>
            {
                CooperationNode node = cooperationNode;
                while (node.Type != 0)
                {
                    node = (CooperationNode)node.Parent;
                }
                return node;
            };

            CooperationDialog dialog = new CooperationDialog(func(rows[0]), rows[0]);
            dialog.ShowDialog();
            _view.GetTreeView().Refresh();
        }

        private void SetParameters(ICollection<CooperationNode> nodes, double stockRate, string sampleSize)
        {
            IList<IntermechTreeElement> rows = new List<IntermechTreeElement>();
            foreach (CooperationNode myNode in nodes)
            {
                rows.Add(myNode.Tag as IntermechTreeElement);
            }

            _model.SetParameters(_root, rows, stockRate, sampleSize);


            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            foreach (CooperationNode node in _view.GetMainNodes())
            {
                queue.Enqueue(node);
            }
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
            _model.SetTechProcess(_root, rows, techProcess);

            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            foreach (CooperationNode node in _view.GetMainNodes())
            {
                queue.Enqueue(node);
            }
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
            SetCooperation(e.SelectedNodes);
        }

        private void _view_SetCooperationClick(object sender, MultipleNodesSelectedEventArgs e)
        {
            SetCooperation(e.SelectedNodes);
        }

        private void _view_Load(object sender, System.EventArgs e)
        {
            _view.FillTree(_model.GetModel(new IntermechTreeElement()
            {
                Designation = "Пожалуйста, подождите! Идет фильтрация состава"
            }, string.Empty));

            _view.FillTree(_model.GetModel(_root, _root.Agent));
        }

        /// <summary>
        /// Отвечает за передачу параметров и запуск формы
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Run(Parameter<IntermechTreeElement> parameter)
        {
            _parameter = parameter;
            
            // получить заказ
            _root = parameter.GetParameter(0);
            _view.Show();
        }
    }
}