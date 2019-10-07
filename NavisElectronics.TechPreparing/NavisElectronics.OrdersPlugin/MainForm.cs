using System;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace NavisElectronics.Orders
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AbortLoading != null)
            {
                AbortLoading(sender, e);
            }
        }

        public event EventHandler StartChecking;
        public event EventHandler AbortLoading;


        public void UpdateTreeModel(TreeModel treeModel)
        {
            treeViewAdv.Model = null;
            treeViewAdv.Model = treeModel;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (StartChecking != null)
            {
                StartChecking(sender, e);
            }
        }
    }
}
