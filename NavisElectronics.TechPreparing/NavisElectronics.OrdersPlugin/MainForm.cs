using System;
using System.Drawing;
using System.Windows.Forms;
using Aga.Controls.Tree;
using NavisElectronics.Orders.EventArguments;
using NavisElectronics.TechPreparation.Interfaces.Entities;

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
        public event EventHandler<ProduceEventArgs> DoNotProduceClick;


        public void UpdateTreeModel(TreeModel treeModel)
        {
            treeViewAdv.Model = null;
            treeViewAdv.Model = treeModel;
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            if (StartChecking != null)
            {
                StartChecking(sender, e);
            }
        }

        private void toolStripMenuItem3_Click(object sender, System.EventArgs e)
        {
            if (DoNotProduceClick != null)
            {
                IntermechTreeElement selectedElement =
                    (IntermechTreeElement)((OrderNode)treeViewAdv.SelectedNode.Tag).Tag;

                DoNotProduceClick(sender, new ProduceEventArgs(selectedElement,true));
                treeViewAdv.Invalidate();
            }

        }

        private void treeViewAdv_RowDraw(object sender, TreeViewRowDrawEventArgs e)
        {
            IntermechTreeElement node = ((IntermechTreeElement)((OrderNode)e.Node.Tag).Tag);
            if (node.CooperationFlag)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

            if (node.ProduseSign)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.IndianRed), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }
        }

        private void produceMenuStrip_Click(object sender, System.EventArgs e)
        {
            if (DoNotProduceClick != null)
            {
                IntermechTreeElement selectedElement =
                    (IntermechTreeElement)((OrderNode)treeViewAdv.SelectedNode.Tag).Tag;

                DoNotProduceClick(sender, new ProduceEventArgs(selectedElement,false));
                treeViewAdv.Invalidate();
            }
        }
    }
}
