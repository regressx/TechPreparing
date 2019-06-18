// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeComparerPresenter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the TreeComparerPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;
    using NavisElectronics.TechPreparation.ViewInterfaces;
    using NavisElectronics.TechPreparation.ViewModels;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;
    using NavisElectronics.TechPreparation.Views;

    public class TreeComparerPresenter : IPresenter
    {
        /// <summary>
        /// Представление
        /// </summary>
        private readonly ITreeComparerView _view;

        /// <summary>
        /// Модель
        /// </summary>
        private readonly TreeComparerViewModel _model;

        /// <summary>
        /// Текущее состояние дерева
        /// </summary>
        private readonly IntermechTreeElement _oldElement;

        /// <summary>
        /// Набор агентов. Нужен для того, чтобы правильно создавать других представителей
        /// </summary>
        private readonly IDictionary<long, Agent> _agents;

        /// <summary>
        /// Новое дерево из базы данных
        /// </summary>
        private IntermechTreeElement _newElement;

        public TreeComparerPresenter(ITreeComparerView view, TreeComparerViewModel model, IntermechTreeElement oldElement, IDictionary<long, Agent> agents)
        {
            _view = view;
            _model = model;
            _oldElement = oldElement;
            _agents = agents;
            _view.Load += _view_Load;
            _view.Download += _view_Download;
            _view.Compare += _view_Compare;
            _view.Upload += _view_Upload;
            _view.PushChanges += _view_PushChanges;
            _view.EditCooperationClick += _view_EditCooperationClick;
            _view.EditTechRoutesClick += _view_EditTechRoutesClick;
            _view.JumpInit += _view_JumpInit;
            _view.EditAmount += _view_EditAmount;
            _view.FindInOldArchive += _view_FindInOldArchive;
        }

        private void _view_FindInOldArchive(object sender, ComparerNode e)
        {
            _model.OpenFolder(e.Designation);
        }

        private void _view_EditAmount(object sender, ComparerNode e)
        {
            using (MultiplyParametersView view = new MultiplyParametersView())
            {
                MultiplyParametersViewPresenter presenter = new MultiplyParametersViewPresenter(view, new MultiplyParametersViewModel());

                // меняем параметры выделенных строк
                if (presenter.Run() == DialogResult.OK)
                {
                    IList<Parameter> parameters = presenter.GetParameters();
                    double amount = Convert.ToDouble(parameters[0].Value);
                    double stockRate = Convert.ToDouble(parameters[1].Value);
                    IntermechTreeElement elementFromTag = e.Tag as IntermechTreeElement;
                    elementFromTag.Amount = (float)amount;
                    elementFromTag.StockRate = stockRate;
                    e.Amount = amount;

                    ComparerNode mainNode = _view.GetNewNode();

                    IList<IntermechTreeElement> elementsToUpdate = _newElement.Find(elementFromTag.Id);
                    foreach (IntermechTreeElement element in elementsToUpdate)
                    {
                        element.Amount = (float)amount;
                        element.StockRate = stockRate;
                        ComparerNode node = FindNodeWithTag(mainNode, element);
                        node.Amount = amount;
                    }
                } 
            }
        }

        private ComparerNode FindNodeWithTag(ComparerNode mainNode, IntermechTreeElement tag)
        {
            Queue<ComparerNode> queue = new Queue<ComparerNode>();
            ComparerNode nodeToFind = null;
            queue.Enqueue(mainNode);
            while (queue.Count > 0)
            {
                ComparerNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement elementFromTag = nodeFromQueue.Tag as IntermechTreeElement;
                if (elementFromTag.Equals(tag))
                {
                    nodeToFind = nodeFromQueue;
                    queue.Clear();
                    break;
                }

                if (nodeFromQueue.Nodes.Count > 0)
                {
                    foreach (Node child in nodeFromQueue.Nodes)
                    {
                        queue.Enqueue((ComparerNode)child);
                    }
                }
            }

            return nodeToFind;
        }

        private void _view_JumpInit(object sender, ComparerNode e)
        {
            long[] path = _model.GetFullPath(e);
            ComparerNode nodeToFind = null;
            ComparerNode nodeWhereFind = null;
            TreeViewAdv treeWhereToFind = null;
            if (((TreeViewAdv)sender).Name == "treeViewAdv1")
            {
                nodeWhereFind = _view.GetNewNode();
                treeWhereToFind = _view.GetNewTree();
            }
            else
            {
                nodeWhereFind = _view.GetOldNode();
                treeWhereToFind = _view.GetOldTree();
            }

            nodeToFind = _model.Find(nodeWhereFind, path);
            _view.JumpToNode(treeWhereToFind, nodeToFind);
        }

        private void _view_EditTechRoutesClick(object sender, IntermechTreeElement e)
        {
            using (SelectManufacturerView manufacturerView = new SelectManufacturerView(_agents.Values))
            {
                if (manufacturerView.ShowDialog() == DialogResult.OK)
                {
                    string filter = manufacturerView.SelectedAgentId;
                    Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                    queue.Enqueue(e);
                    while (queue.Count > 0)
                    {
                        IntermechTreeElement elementFromQueue = queue.Dequeue();
                        elementFromQueue.Agent = filter;
                        if (elementFromQueue.Children.Count > 0)
                        {
                            foreach (IntermechTreeElement child in elementFromQueue.Children)
                            {
                                queue.Enqueue(child);
                            }
                        }
                    }

                    TechRoutesMap view = new TechRoutesMap(manufacturerView.SelectedAgentName, false);
                    TechRouteMapPresenter presenter = new TechRouteMapPresenter(view, e,filter, _agents);
                    presenter.Run();
                }
            }
        }

        private void _view_EditCooperationClick(object sender, IntermechTreeElement e)
        {
            //CooperationView cooperationView = new CooperationView(false);
            //CooperationPresenter presenter = new CooperationPresenter(cooperationView, e, null);
            //presenter.Run();
        }

        private void _view_PushChanges(object sender, IntermechTreeElement e)
        {
            _model.Upload(_oldElement,_newElement, e);
            _view.FillOldTree(_model.GetModel(_oldElement));
            _view.FillNewTree(_model.GetModel(_newElement));
        }

        private void _view_Upload(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _view_Compare(object sender, System.EventArgs e)
        {
            _model.Compare(_oldElement, _newElement);

            Queue<ComparerNode> queueForOld = new Queue<ComparerNode>();
            queueForOld.Enqueue(_view.GetOldNode());
            while (queueForOld.Count > 0)
            {
                ComparerNode nodeFromQueue = queueForOld.Dequeue();
                IntermechTreeElement element = nodeFromQueue.Tag as IntermechTreeElement;
                switch (element.NodeState)
                {
                    case NodeStates.Deleted:
                        nodeFromQueue.NodeState = NodeStates.Deleted;
                        break;
                }

                if (nodeFromQueue.Nodes.Count > 0)
                {
                    foreach (Node node in nodeFromQueue.Nodes)
                    {
                        queueForOld.Enqueue((ComparerNode)node);
                    }
                }
            }

            Queue<ComparerNode> queueForNew = new Queue<ComparerNode>();
            queueForNew.Enqueue(_view.GetNewNode());
            while (queueForNew.Count > 0)
            {
                ComparerNode nodeFromQueue = queueForNew.Dequeue();
                IntermechTreeElement element = nodeFromQueue.Tag as IntermechTreeElement;
                switch (element.NodeState)
                {
                    case NodeStates.Added:
                        nodeFromQueue.NodeState = NodeStates.Added;
                        break;

                    case NodeStates.Modified:
                        nodeFromQueue.NodeState = NodeStates.Modified;
                        break;
                }

                if (nodeFromQueue.Nodes.Count > 0)
                {
                    foreach (Node node in nodeFromQueue.Nodes)
                    {
                        queueForNew.Enqueue((ComparerNode)node);
                    }
                }
            }

            _view.ExpandFirstTree();
            _view.ExpandSecondTree();
        }

        private async void _view_Download(object sender, System.EventArgs e)
        {
            TreeModel model = new TreeModel();
            ComparerNode node = new ComparerNode();
            node.Name = "Пожалуйста, подождите! Идет загрузка данных из IPS";
            model.Nodes.Add(node);
            _view.FillNewTree(model);
            _view.LockButtons();
            IntermechTreeElement order =
                await _model.GetFullOrderFromDatabaseAsync(_oldElement.Id, CancellationToken.None);
            _newElement = order;
            _view.FillNewTree(_model.GetModel(order));
            _view.UnlockButtons();
        }

        private void _view_Load(object sender, EventArgs e)
        {
            _view.FillOldTree(_model.GetModel(_oldElement));
        }

        public DialogResult RunWithDialog()
        {
            return _view.ShowDialog();
        }

        public void Run()
        {
            _view.Show();
        }
    }
}