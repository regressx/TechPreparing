﻿namespace NavisElectronics.Orders
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using EventArguments;
    using TechPreparation.Interfaces.Entities;

    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
            treeViewAdv.RowDraw += TreeViewAdv_RowDraw;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AbortLoading != null)
            {
                AbortLoading(sender, e);
            }
        }

        public event EventHandler DownloadAndUpdate;
        public event EventHandler Save;
        public event EventHandler StartChecking;
        public event EventHandler AbortLoading;
        public event EventHandler<ProduceEventArgs> DoNotProduceClick;


        public void UpdateTreeModel(TreeModel treeModel)
        {
            treeViewAdv.Model = null;
            treeViewAdv.Model = treeModel;
        }

        public void UpdateSaveLabel(string message)
        {
            saveInfoLabel.Text = message;
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            if (StartChecking != null)
            {
                StartChecking(sender, e);
            }
        }

        private void ToolStripMenuItem3_Click(object sender, System.EventArgs e)
        {
            if (DoNotProduceClick != null)
            {
                IntermechTreeElement selectedElement =
                    (IntermechTreeElement)((OrderNode)treeViewAdv.SelectedNode.Tag).Tag;

                DoNotProduceClick(sender, new ProduceEventArgs(selectedElement,true));
                treeViewAdv.Invalidate();
            }

        }

        private void TreeViewAdv_RowDraw(object sender, TreeViewRowDrawEventArgs e)
        {
            IntermechTreeElement node = ((IntermechTreeElement)((OrderNode)e.Node.Tag).Tag);
            if (node.CooperationFlag)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

            if (node.ProduseSign)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkKhaki), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
                ((OrderNode) e.Node.Tag).Note = node.RelationNote;
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

        private void SaveOrderButton_Click(object sender, EventArgs e)
        {
            if (Save != null)
            {
                Save(this, EventArgs.Empty);
            }
        }

        private void LoadAndUpdateButtonStrip_Click(object sender, EventArgs e)
        {
            if (DownloadAndUpdate != null)
            {
                DownloadAndUpdate(sender, e);
            }
        }
    }
}
