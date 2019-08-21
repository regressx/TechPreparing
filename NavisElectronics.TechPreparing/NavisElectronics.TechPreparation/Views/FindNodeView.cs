namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using ViewModels.TreeNodes;

    /// <summary>
    /// Окно для поиска объектов в дереве
    /// </summary>
    public partial class FindNodeView : Form
    {
        /// <summary>
        /// Контрол, в котором будет осуществляться поиск
        /// </summary>
        private readonly TreeViewAdv _treeViewAdv;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="treeViewAdv"></param>
        public FindNodeView(TreeViewAdv treeViewAdv)
        {
            _treeViewAdv = treeViewAdv;
            InitializeComponent();
        }

        /// <summary>
        /// Найти все вхождения узла с указанным обозначением
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void FindAllButton_Click(object sender, System.EventArgs e)
        {
            listBox1.Items.Clear();

            string textToSearch = textBox1.Text.ToUpper();

            foreach (TreeNodeAdv node in _treeViewAdv.AllNodes)
            {
                CooperationNode tagNode = node.Tag as CooperationNode;
                if (tagNode.Designation != null)
                {
                    if (tagNode.Designation.Contains(textToSearch))
                    {
                        listBox1.Items.Add((CooperationNode)node.Tag);
                    }
                }
            }
        }

        /// <summary>
        /// Перейти к указанному узлу
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                TreeNodeAdv nodeToFind = _treeViewAdv.FindNodeByTag((CooperationNode)listBox1.SelectedItem);
                _treeViewAdv.SelectedNode = nodeToFind;
                _treeViewAdv.EnsureVisible(nodeToFind);

            }
        }

        /// <summary>
        /// По нажатию на Enter осуществляем поиск в дереве
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                FindAllButton_Click(sender, EventArgs.Empty);
            }
        }
    }
}
