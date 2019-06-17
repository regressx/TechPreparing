namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.ViewInterfaces;

    public partial class FindNodeView : Form, IFindNodeView
    {
        public FindNodeView()
        {
            InitializeComponent();
        }

        public event EventHandler FindButtonClick;
        public event EventHandler<TreeNodeAdv> NodeClick;

        public string Designation
        {
            get { return textBox1.Text; }
        }
        public IList<TreeNodeAdv> Nodes { get; set; }

        public void FillListBox(IList<TreeNodeAdv> list)
        {
            listBox1.Items.Clear();
            foreach (TreeNodeAdv advNode in list)
            {
                listBox1.Items.Add(advNode);
            }
        }

        private void FindAllButton_Click(object sender, System.EventArgs e)
        {
            if (FindButtonClick != null)
            {
                FindButtonClick(sender, e);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (NodeClick != null)
                {
                    NodeClick(sender, listBox1.SelectedItem as TreeNodeAdv);
                }
            }
        }
    }
}
