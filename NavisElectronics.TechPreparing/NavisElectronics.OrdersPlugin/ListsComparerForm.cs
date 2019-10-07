using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NavisElectronics.Orders.Entities;
using NavisElectronics.Orders.Presenters;

namespace NavisElectronics.Orders
{
    public partial class ListsComparerForm : Form, IListsComparerView
    {
        public ListsComparerForm()
        {
            InitializeComponent();
        }

        public void FillFirstGrid(IList<ReportElement> firstDictionaryValues)
        {
            iGrid1.BeginUpdate();
            try
            {
                iGrid1.Rows.Clear();
                iGrid1.Rows.Count = firstDictionaryValues.Count;
                for (int i = 0; i < firstDictionaryValues.Count; i++)
                {
                    iGrid1.Rows[i].Cells[0].Value = firstDictionaryValues[i].ObjectId;
                    iGrid1.Rows[i].Cells[0].BackColor = firstDictionaryValues[i].Color;
                    iGrid1.Rows[i].Cells[1].Value = firstDictionaryValues[i].Caption;
                    iGrid1.Rows[i].Cells[1].BackColor = firstDictionaryValues[i].Color;
                    iGrid1.Rows[i].Cells[2].Value = firstDictionaryValues[i].AmountWithUse;
                    iGrid1.Rows[i].Cells[2].BackColor = firstDictionaryValues[i].Color;
                    iGrid1.Rows[i].Cells[3].Value = firstDictionaryValues[i].MeasureUnits;
                    iGrid1.Rows[i].Cells[3].BackColor = firstDictionaryValues[i].Color;
                    iGrid1.Rows[i].Tag = firstDictionaryValues[i];
                }
            }
            finally
            {
                iGrid1.EndUpdate();
            }
        }

        public void FillSecondGrid(IList<ReportElement> secondDictionaryValues)
        {
            iGrid2.BeginUpdate();
            try
            {
                iGrid2.Rows.Clear();
                iGrid2.Rows.Count = secondDictionaryValues.Count;
                for (int i = 0; i < secondDictionaryValues.Count; i++)
                {
                    iGrid2.Rows[i].Cells[0].Value = secondDictionaryValues[i].ObjectId;
                    iGrid2.Rows[i].Cells[0].BackColor = secondDictionaryValues[i].Color;
                    iGrid2.Rows[i].Cells[1].Value = secondDictionaryValues[i].Caption;
                    iGrid2.Rows[i].Cells[1].BackColor = secondDictionaryValues[i].Color;
                    iGrid2.Rows[i].Cells[2].Value = secondDictionaryValues[i].AmountWithUse;
                    iGrid2.Rows[i].Cells[2].BackColor = secondDictionaryValues[i].Color;
                    iGrid2.Rows[i].Cells[3].Value = secondDictionaryValues[i].MeasureUnits;
                    iGrid2.Rows[i].Cells[3].BackColor = secondDictionaryValues[i].Color;
                    iGrid2.Rows[i].Tag = secondDictionaryValues[i];
                }
            }
            finally
            {
                iGrid2.EndUpdate();
            }
        }


        public event EventHandler LoadCsv;
        public event EventHandler StartComparing;

        private void LoadCsvMenuItem_Click(object sender, System.EventArgs e)
        {
            if (LoadCsv != null)
            {
                LoadCsv(sender, e);
            }

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (StartComparing != null)
            {
                StartComparing(sender, e);
            }
            
        }
    }
}
