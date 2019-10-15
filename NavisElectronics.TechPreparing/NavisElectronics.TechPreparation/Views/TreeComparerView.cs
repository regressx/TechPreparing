using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;
    using NavisElectronics.TechPreparation.ViewInterfaces;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    public partial class TreeComparerView : Form, ITreeComparerView
    {
        public TreeComparerView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие слияния нового дерева со старым
        /// </summary>
        public event EventHandler Upload;
        public event EventHandler Download;

        /// <summary>
        /// Сравнение нового дерева со старым
        /// </summary>
        public event EventHandler Compare;

        public event EventHandler CompareListsClick;

        public event EventHandler<IntermechTreeElement> PushChanges;
        public event EventHandler<ComparerNode> DeleteNodeClick;

        public event EventHandler<IntermechTreeElement> EditCooperationClick;

        public event EventHandler<IntermechTreeElement> EditTechRoutesClick;

        public event EventHandler<ComparerNode> EditAmount;

        /// <summary>
        /// Событие для того, чтобы по узлу в одном дереве прыгнуть на этот же узел во втором дереве
        /// </summary>
        public event EventHandler<ComparerNode> JumpInit;

        public event EventHandler<ComparerNode> FindInOldArchive;
        public event EventHandler<CompareTwoNodesEventArgs> CompareTwoNodesClick;


        public void FillOldTree(TreeModel model)
        {
            treeViewAdv1.Model = null;
            treeViewAdv1.Model = model;
        }

        public void FillNewTree(TreeModel model)
        {
            treeViewAdv2.Model = null;
            treeViewAdv2.Model = model;
        }

        /// <summary>
        /// Разблокировать кнопки
        /// </summary>
        public void UnlockButtons()
        {
            foreach (ToolStripItem button in toolStrip.Items)
            {
                button.Enabled = true;
            }
        }

        /// <summary>
        /// Заблокировать кнопки
        /// </summary>
        public void LockButtons()
        {
            foreach (ToolStripItem button in toolStrip.Items)
            {
                button.Enabled = false;
            }
        }

        /// <summary>
        /// Получить старое дерево
        /// </summary>
        /// <returns>
        /// The <see cref="ComparerNode"/>.
        /// </returns>
        public ComparerNode GetOldNode()
        {
            ComparerNode myOldNode = treeViewAdv1.Root.Children[0].Tag as ComparerNode;
            return myOldNode;
        }

        /// <summary>
        /// Получить новое дерево
        /// </summary>
        /// <returns>
        /// The <see cref="ComparerNode"/>.
        /// </returns>
        public ComparerNode GetNewNode()
        {
            ComparerNode myNewNode = treeViewAdv2.Root.Children[0].Tag as ComparerNode;
            return myNewNode;
        }

        /// <summary>
        /// Раскрыть старое дерево
        /// </summary>
        public void ExpandFirstTree()
        {
            treeViewAdv1.ExpandAll();
        }

        /// <summary>
        /// Раскрыть новое дерево
        /// </summary>
        public void ExpandSecondTree()
        {
            treeViewAdv2.ExpandAll();
        }

        /// <summary>
        /// Прыгнуть на выбранный в одном дереве узел на тот же узел, но уже в другом дереве
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="nodeToFind">
        /// The node to find.
        /// </param>
        public void JumpToNode(object sender, ComparerNode nodeToFind)
        {
            TreeViewAdv selectedTree = sender as TreeViewAdv;

            Queue<TreeNodeAdv> queue = new Queue<TreeNodeAdv>();
            queue.Enqueue(selectedTree.Root);
            while (queue.Count > 0)
            {
                TreeNodeAdv nodeFromQueue = queue.Dequeue();
                ComparerNode taggedNode = nodeFromQueue.Tag as ComparerNode;
                if (taggedNode == nodeToFind)
                {
                    selectedTree.SelectedNode = nodeFromQueue;
                    queue.Clear();
                    break;
                }

                if (nodeFromQueue.Children.Count > 0)
                {
                    foreach (TreeNodeAdv child in nodeFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        public TreeViewAdv GetNewTree()
        {
            return treeViewAdv2;
        }

        public TreeViewAdv GetOldTree()
        {
            return treeViewAdv1;
        }

        private void uploadChangesButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void downloadTreeButton_Click(object sender, System.EventArgs e)
        {
            if (Download != null)
            {
                Download(sender, e);
            }
        }

        private void compareTreeButton_Click(object sender, System.EventArgs e)
        {
            if (Compare != null)
            {
                Compare(sender, e);
            }
        }

        private void treeViewAdv1_RowDraw(object sender, TreeViewRowDrawEventArgs e)
        {
            ComparerNode node = e.Node.Tag as ComparerNode;
            if (node.NodeState == NodeStates.Deleted)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 192, 192)), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

            IntermechTreeElement taggedElement = node.Tag as IntermechTreeElement;
            if (taggedElement.CooperationFlag)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

        }

        private void treeViewAdv2_RowDraw(object sender, TreeViewRowDrawEventArgs e)
        {
            ComparerNode node = e.Node.Tag as ComparerNode;

            IntermechTreeElement taggedElement = node.Tag as IntermechTreeElement;
            if (taggedElement != null)
            {
                if (taggedElement.CooperationFlag)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
                }
            }

            if (node.NodeState == NodeStates.Added)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255)), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

            if (node.NodeState == NodeStates.Modified)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 192)), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

        }

        private void pushChangesMenuItem_Click(object sender, EventArgs e)
        {
            if (PushChanges != null)
            {
                ComparerNode selectedNode = treeViewAdv2.SelectedNode.Tag as ComparerNode;
                IntermechTreeElement selectedIntermechElement = selectedNode.Tag as IntermechTreeElement;
                PushChanges(sender, selectedIntermechElement);
            }
        }

        private void editCooperationMenuItem_Click(object sender, EventArgs e)
        {
            if (EditCooperationClick != null)
            {
                ComparerNode selectedNode = treeViewAdv2.SelectedNode.Tag as ComparerNode;
                IntermechTreeElement selectedIntermechElement = selectedNode.Tag as IntermechTreeElement;
                EditCooperationClick(sender, selectedIntermechElement);
            }
        }

        private void editRouteMenuItem_Click(object sender, EventArgs e)
        {
            if (EditTechRoutesClick != null)
            {
                ComparerNode selectedNode = treeViewAdv2.SelectedNode.Tag as ComparerNode;
                IntermechTreeElement selectedIntermechElement = selectedNode.Tag as IntermechTreeElement;
                EditTechRoutesClick(sender, selectedIntermechElement);
            }
        }

        private void treeViewAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (JumpInit != null)
                {
                    ComparerNode selectedNode = ((TreeViewAdv)sender).SelectedNode.Tag as ComparerNode;
                    JumpInit(sender, selectedNode);
                }
            }
        }

        private void editAmountMenuItem_Click(object sender, EventArgs e)
        {
            if (EditAmount != null)
            {
                ComparerNode selectedNode = treeViewAdv2.SelectedNode.Tag as ComparerNode;
                EditAmount(sender, selectedNode);
            }
        }

        private void findInOldArchiveMenuItem_Click(object sender, EventArgs e)
        {
            if (FindInOldArchive != null)
            {
                ComparerNode selectedNode = treeViewAdv2.SelectedNode.Tag as ComparerNode;
                FindInOldArchive(sender, selectedNode);
            }
        }

        private void TreeComparerView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                if (treeViewAdv1.SelectedNode != null && treeViewAdv2.SelectedNode != null)
                {
                    if (CompareTwoNodesClick != null)
                    {
                        IntermechTreeElement leftElement =
                            (IntermechTreeElement)((ComparerNode)treeViewAdv1.SelectedNode.Tag).Tag;

                        IntermechTreeElement rightElement =
                            (IntermechTreeElement)((ComparerNode)treeViewAdv2.SelectedNode.Tag).Tag;


                        CompareTwoNodesClick(sender, new CompareTwoNodesEventArgs(leftElement, rightElement));
                    }
                }
            }
        }

        private void deleteNodeMenuItem_Click(object sender, EventArgs e)
        {
            if (DeleteNodeClick != null)
            {
                ComparerNode selectedElement =
                     (ComparerNode)treeViewAdv1.SelectedNode.Tag;

                DeleteNodeClick(sender, selectedElement);
            }
            
        }
    }
}
