// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeComparerPresenter.cs" company="NavisElectronics">
//   Cherkashin I.V.
// </copyright>
// <summary>
//   Defines the TreeComparerPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Exceptions;

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Aga.Controls.Tree;
    using Enums;
    using Interfaces.Entities;
    using ViewInterfaces;
    using ViewModels;
    using ViewModels.TreeNodes;

    /// <summary>
    /// The tree comparer presenter.
    /// </summary>
    public class TreeComparerPresenter : IPresenter<IntermechTreeElement>
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
        private IntermechTreeElement _oldElement;

        /// <summary>
        /// Новое дерево из базы данных
        /// </summary>
        private IntermechTreeElement _newElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeComparerPresenter"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        public TreeComparerPresenter(ITreeComparerView view, TreeComparerViewModel model)
        {
            _view = view;
            _model = model;
            _view.Load += _view_Load;
            _view.Download += _view_Download;
            _view.Compare += _view_Compare;
            _view.PushChanges += _view_PushChanges;
            _view.EditCooperationClick += _view_EditCooperationClick;
            _view.EditTechRoutesClick += _view_EditTechRoutesClick;
            _view.JumpInit += _view_JumpInit;
            _view.FindInOldArchive += _view_FindInOldArchive;
            _view.CompareTwoNodesClick += View_CompareTwoNodesClick;
            _view.DeleteNodeClick += View_DeleteNodeClick;
        }

        private void View_DeleteNodeClick(object sender, ComparerNode e)
        {
            if (e.NodeState != NodeStates.Deleted)
            {
                throw new DeleteAttempFoundException("Обнаружена попытка удаления компонента, который нельзя удалять");
            }

            IntermechTreeElement selectedNode = (IntermechTreeElement)e.Tag;
            IntermechTreeElement parent = selectedNode.Parent;
            
            int i = 0;
            
            // я не могу гарантировать 100%, что в составе не будет повторяющихся элементов, во всяком случае на 15.10.2019г, поэтому
            // будем искать линейно первый попавшийся, который удовлетворяет условию, что совпадает его идентификатор,  и элемент отмечен на удаление
            foreach (IntermechTreeElement child in parent.Children)
            {
                // это тот элемент, что там нужен
                if (e.ObjectId == child.ObjectId && child.NodeState == NodeStates.Deleted)
                {
                    parent.RemoveAt(i);
                    break;
                }

                i++;
            }

            _view.FillOldTree(_model.GetModel(_oldElement));

            TreeViewAdv treeViewAdv = _view.GetOldTree();

            // найти родительский узел
            ComparerNode parentComparerNode = FindNodeWithTag(_view.GetOldNode(), parent);

            TreeNodeAdv nodeToFind = treeViewAdv.FindNodeByTag(parentComparerNode);
            treeViewAdv.SelectedNode = nodeToFind;
            treeViewAdv.EnsureVisible(nodeToFind);

        }

        /// <summary>
        /// Обработчик события сравнения двух узлов
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void View_CompareTwoNodesClick(object sender, CompareTwoNodesEventArgs e)
        {

        }

        /// <summary>
        /// Обработчик события поиска узла в архиве предприятия
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _view_FindInOldArchive(object sender, ComparerNode e)
        {
            _model.OpenFolder(e.Designation);
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
            // using (SelectManufacturerView manufacturerView = new SelectManufacturerView(_agents.Values))
            // {
            // if (manufacturerView.ShowDialog() == DialogResult.OK)
            // {
            // string filter = manufacturerView.SelectedAgentId;
            // Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            // queue.Enqueue(e);
            // while (queue.Count > 0)
            // {
            // IntermechTreeElement elementFromQueue = queue.Dequeue();
            // elementFromQueue.Agent = filter;
            // if (elementFromQueue.Children.Count > 0)
            // {
            // foreach (IntermechTreeElement child in elementFromQueue.Children)
            // {
            // queue.Enqueue(child);
            // }
            // }
            // }

            // TechRoutesMap view = new TechRoutesMap(manufacturerView.SelectedAgentName, false);
            // TechRouteMapPresenter presenter = new TechRouteMapPresenter(view, e,filter, _agents);
            // presenter.Run();
            // }
            // }
        }

        private void _view_EditCooperationClick(object sender, IntermechTreeElement e)
        {
            // CooperationView cooperationView = new CooperationView(false);
            // CooperationPresenter presenter = new CooperationPresenter(cooperationView, e, null);
            // presenter.Run();
        }

        private void _view_PushChanges(object sender, EventArgs e)
        {
            ICollection<ComparerNode> collection = _view.GetSelectedNodesInRightTree();
            if (collection.Count > 0)
            {
                IntermechTreeElement lastElementToJump = null;
                foreach (ComparerNode comparerNode in collection)
                {
                    lastElementToJump = (IntermechTreeElement)comparerNode.Tag;
                    _model.Upload(_oldElement, _newElement, lastElementToJump);
                }

                _view.FillOldTree(_model.GetModel(_oldElement));
                _view.FillNewTree(_model.GetModel(_newElement));

                TreeViewAdv treeViewAdv = _view.GetNewTree();

                // найти родительский узел
                ComparerNode parentComparerNode = FindNodeWithTag(_view.GetNewNode(), lastElementToJump);

                TreeNodeAdv nodeToFind = treeViewAdv.FindNodeByTag(parentComparerNode);
                treeViewAdv.SelectedNode = nodeToFind;
                treeViewAdv.EnsureVisible(nodeToFind);
            }

        }

        private void _view_Compare(object sender, EventArgs e)
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

        private async void _view_Download(object sender, EventArgs e)
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

        public void Run(IntermechTreeElement root)
        {
            _oldElement = root;
            _view.Show();
        }
    }
}