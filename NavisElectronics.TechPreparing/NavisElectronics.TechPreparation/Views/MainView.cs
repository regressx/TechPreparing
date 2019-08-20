using Aga.Controls.Tree;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Presenters;
using NavisElectronics.TechPreparation.ViewModels.TreeNodes;
using TenTec.Windows.iGridLib;

namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.ViewInterfaces;

    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            cooperationButton.Click += CooperationButton_Click;

            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.if_stock_new_meeting_21476);
            iGrid.ImageList = imageList;

        }

        private void CooperationButton_Click(object sender, EventArgs e)
        {
            if (CooperationClick != null)
            {
                CooperationClick(sender, e);
            }
        }


        #region События

        public event EventHandler<TreeNodeAgentValueEventArgs> CellValueChanged;
        public event EventHandler CooperationClick;
        public event EventHandler<TreeNodeClickEventArgs> NodeMouseClick;
        public event EventHandler ApplyButtonClick;
        public event EventHandler ClearCooperationClick;
        public event EventHandler EditTechRoutesClick;
        public event EventHandler UpdateClick;
        public event EventHandler EditMainMaterialsClick;
        public event EventHandler EditStandartDetailsClick;
        public event EventHandler LoadPreparationClick;
        public event EventHandler EditWithdrawalTypeClick;
        public event EventHandler RefreshClick;
        public event EventHandler CheckAllReadyClick;
        public void UpdateLabelText(string message)
        {
            toolStripLabel1.Text = message;
        }

        public void UpdateProgressBar(int progressReportPercent)
        {
            toolStripProgressBar1.Value = progressReportPercent;
        }

        #endregion

        #region Реализация интерфеса

        public void UpdateAgent(long agentId)
        {
            foreach (iGRow row in iGrid.Rows)
            {
                row.Cells[1].Value = null;
            }

            // организаций мало - пойдет и обычный проход за линейное время
            foreach (iGRow row in iGrid.Rows)
            {
                Agent currentAgent = (Agent)row.Tag;
                if (currentAgent.Id == agentId)
                {
                    row.Cells[1].ImageIndex = 0;
                    break;
                }
            }
        }

        public string GetNote()
        {
            return textBoxNote.Text;
        }

        public void FillNote(string orderElementNote)
        {
            textBoxNote.Text = orderElementNote;
        }

        public void UnLockButtons()
        {
            foreach (ToolStripItem button in toolStrip.Items)
            {
                button.Enabled = true;
            }
        }

        public void LockButtons()
        {
            foreach (ToolStripItem button in toolStrip.Items)
            {
                button.Enabled = false;
            }
        }

        public void UpdateCaptionText(string orderName)
        {
            this.Text = orderName;
        }

        public void FillTree(TreeModel model)
        {
            treeViewAdv.Model = null;
            treeViewAdv.Model = model;
        }

        public void FillGrid(ICollection<Agent> agents)
        {
            iGrid.BeginUpdate();
            try
            {
                iGrid.Rows.Clear();
                iGrid.Rows.AddRange(agents.Count);
                int i = 0;
                foreach (Agent agent in agents)
                {
                    iGrid.Rows[i].Cells[0].Value = agent.Name;
                    iGrid.Rows[i].Tag = agent;
                    i++;
                }
                iGrid.Cols[0].AutoWidth();
            }
            finally
            {
                iGrid.EndUpdate();
            }
        }

        #endregion

        #region Обработчики событий

        private void LoadTechPreparationButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref LoadPreparationClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void MainMaterialsButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref EditMainMaterialsClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void standartsButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref EditStandartDetailsClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }


        private void UpdateButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref UpdateClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (ApplyButtonClick != null)
            {
                ApplyButtonClick(sender, e);
            }
        }


        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            throw new NotImplementedException();
        }



        private void ClearCooperationButton_Click(object sender, EventArgs e)
        {
            if (ClearCooperationClick != null)
            {
                ClearCooperationClick(sender, new BoundTreeElementEventArgs(iGrid.SelectedCells[0].Row.Tag as IntermechTreeElement, iGrid.SelectedCells[0].RowIndex));
            }
        }

        private void TechRoutesEditButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref EditTechRoutesClick);
            if (temp != null)
            {
                temp(sender, e);
            }

        }

        private void SetTechWithdrawalButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref EditWithdrawalTypeClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void refreshTreeButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref RefreshClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void CheckReadyButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref CheckAllReadyClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }


        #endregion

        private void MainView_Load(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ApplyButtonClick != null)
            {
                ApplyButtonClick(sender, e);
            }
        }

        private void iGrid_CellDoubleClick(object sender, iGCellDoubleClickEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                if (CellValueChanged != null)
                {
                    ViewNode selectedTreeNode = (ViewNode)treeViewAdv.SelectedNode.Tag;
                    //CellValueChanged(sender, new TreeNodeAgentValueEventArgs((IntermechTreeElement)selectedTreeNode.Tag, dataGridView1.Columns[e.ColumnIndex].Name));
                }
            }
        }

        private void treeViewAdv_NodeMouseClick(object sender, TreeNodeAdvMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (NodeMouseClick != null)
                {
                    TreeNodeAdv currentNodeView = e.Node;
                    ViewNode selectedNode = (ViewNode)currentNodeView.Tag;
                    IntermechTreeElement element = (IntermechTreeElement) selectedNode.Tag;
                    NodeMouseClick(sender, new TreeNodeClickEventArgs(element));
                }
            }
        }
    }
}
