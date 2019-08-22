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
        public event EventHandler<BoundTreeElementEventArgs> ClearManufacturerClick;
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

        public void UpdateAgent(string agentsInfo)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[0].Style.BackColor = Color.White;
                row.Cells[1].Style.BackColor = Color.White;
                row.Cells[1].Value = string.Empty;
            }
            // ничего не делать, если информация об агентах пуста
            if (agentsInfo == string.Empty)
            {
                return;
            }

            string[] agents = agentsInfo.Split(';');

            // организаций мало - пойдет и обычный проход за квадратичное время
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Agent currentAgent = (Agent)row.Tag;

                foreach (string agent in agents)
                {
                    long agentId = long.Parse(agent);
                    if (currentAgent.Id == agentId)
                    {
                        row.Cells[0].Style.BackColor = Color.LightGray;
                        row.Cells[1].Style.BackColor = Color.LightGray;
                        row.Cells[1].Value = "+";
                        break;
                    }
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
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(agents.Count);
            int i = 0;
            foreach (Agent agent in agents)
            {
                dataGridView1.Rows[i].Cells[0].Value = agent.Name;
                dataGridView1.Rows[i].Tag = agent;
                i++;
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


        private void ClearManufacturerButton_Click(object sender, EventArgs e)
        {
            if (ClearManufacturerClick != null)
            {
                ViewNode selectedTreeAdvNode = (ViewNode)treeViewAdv.SelectedNode.Tag;
                IntermechTreeElement selectedTreeElement = (IntermechTreeElement)selectedTreeAdvNode.Tag;
                ClearManufacturerClick(sender, new BoundTreeElementEventArgs(selectedTreeElement, dataGridView1.SelectedCells[0].RowIndex));
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

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (CellValueChanged != null)
                {
                    ViewNode selectedTreeNode = (ViewNode)treeViewAdv.SelectedNode.Tag;
                    Agent agent = (Agent)dataGridView1.Rows[e.RowIndex].Tag;
                    CellValueChanged(sender, new TreeNodeAgentValueEventArgs((IntermechTreeElement)selectedTreeNode.Tag, agent.Id.ToString()));
                }
            }
        }
    }
}
