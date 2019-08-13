using Aga.Controls.Tree;

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
        public event EventHandler<TreeNodeMouseClickEventArgs> NodeMouseClick;
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

        public void FillAgent(string agent)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].HeaderCell.Value = "Кооперация";
            string[] agents = agent.Split(';');

            foreach (string s in agents)
            {
                for (int i = 0; i<dataGridView1.ColumnCount; i++)
                {
                    if (dataGridView1.Columns[i].Name == s)
                    {
                        dataGridView1[i, 0].Value = "+";
                    }
                }
            }
        }

        public TreeNode GetMainTreeElement()
        {
            //if (treeView1.Nodes.Count == 0)
            //{
            //    throw new IndexOutOfRangeException("У Вас нет в дереве ни одного элемента");
            //}
            //return treeView1.Nodes[0];
            throw new NotImplementedException();
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

        public void FillGridColumns(ICollection<Agent> agents)
        {
            foreach (Agent agent in agents)
            {
                dataGridView1.Columns.Add(agent.Id.ToString(), agent.Name);
            }

            foreach (DataGridViewColumn dataGridViewColumn in dataGridView1.Columns)
            {
                dataGridViewColumn.DefaultCellStyle.Font = new Font("TimesNewRoman", 14, FontStyle.Bold);
                dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].HeaderCell.Value = "Кооперация";

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

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (NodeMouseClick != null)
            {
                NodeMouseClick(sender, e);
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
            //if (e.RowIndex == 0)
            //{
            //    if (CellValueChanged != null)
            //    {
            //        IntermechTreeElement element = treeView1.SelectedNode.Tag as IntermechTreeElement;
            //        CellValueChanged(sender, new TreeNodeAgentValueEventArgs(element, dataGridView1.Columns[e.ColumnIndex].Name));
            //    }
            //}

            throw new NotImplementedException();
        }



        private void ClearCooperationButton_Click(object sender, EventArgs e)
        {
            if (ClearCooperationClick != null)
            {
                ClearCooperationClick(sender, new BoundTreeElementEventArgs(dataGridView1.SelectedCells[0].OwningRow.DataBoundItem as IntermechTreeElement,dataGridView1.SelectedCells[0].OwningRow.Index));
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


    }
}
