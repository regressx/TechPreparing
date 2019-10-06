using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Aga.Controls.Tree;

namespace NavisElectronics.Orders
{
    public partial class MainForm : Form, IMainView
    {
        CancellationTokenSource _tokenSource;

        public MainForm(CancellationTokenSource tokenSourceToken)
        {
            InitializeComponent();
            _tokenSource = tokenSourceToken;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tokenSource.Cancel();
        }

        public void UpdateTreeModel(TreeModel treeModel)
        {
            treeViewAdv.Model = null;
            treeViewAdv.Model = treeModel;

        }
    }
}
