namespace NavisElectronics.Orders.Views
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using TechPreparation.Interfaces.Entities;
    using TechPreparation.Interfaces.Enums;
    using TreeComparer;

    public partial class TreeComparerView : Form, ITreeComparerView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeComparerView"/> class.
        /// </summary>
        public TreeComparerView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие загрузки данных
        /// </summary>
        public event EventHandler Download;

        /// <summary>
        /// Сравнение нового дерева со старым
        /// </summary>
        public event EventHandler Compare;

        /// <summary>
        /// СОбытие отправки изменений в старое дерево
        /// </summary>
        public event EventHandler PushChanges;

        /// <summary>
        /// Удаление узла из старого дерева
        /// </summary>
        public event EventHandler<ComparerNode> DeleteNodeClick;

        /// <summary>
        /// Событие для того, чтобы по узлу в одном дереве прыгнуть на этот же узел во втором дереве
        /// </summary>
        public event EventHandler<ComparerNode> JumpInit;

        /// <summary>
        /// Сравнение двух узлов
        /// </summary>
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

        public ICollection<ComparerNode> GetSelectedNodesInRightTree()
        {
            ICollection<ComparerNode> selectedNodesCollection = new List<ComparerNode>();
            foreach (TreeNodeAdv adv in treeViewAdv2.SelectedNodes)
            {
                selectedNodesCollection.Add((ComparerNode)adv.Tag);
            }

            return selectedNodesCollection;
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
            if (node.CooperationFlag)
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

        private void PushChangesMenuItem_Click(object sender, EventArgs e)
        {
            if (PushChanges != null)
            {
                PushChanges(sender, EventArgs.Empty);
            }
        }

        private void TreeViewAdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (JumpInit != null)
                {
                    ComparerNode selectedNode = ((TreeViewAdv)sender).SelectedNode.Tag as ComparerNode;
                    JumpInit(sender, selectedNode);
                }
            }

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


        private void DeleteNodeMenuItem_Click(object sender, EventArgs e)
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
