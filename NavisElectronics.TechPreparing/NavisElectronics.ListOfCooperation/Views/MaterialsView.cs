namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;

    using NavisElectronics.ListOfCooperation.Entities;
    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.ViewInterfaces;

    using TenTec.Windows.iGridLib;

    public partial class MaterialsView : Form, IMaterialsView
    {
        public MaterialsView()
        {
            InitializeComponent();
        }

        public event EventHandler SaveClick;

        public event EventHandler<ExtractedObjectEventArgs> IntermechObjectClick;

        public void FillDataGrid(IList<ExtractedObject> materials)
        {
            iGrid1.BeginUpdate();
            try
            {
                iGrid1.Rows.Clear();
                iGrid1.Rows.AddRange(materials.Count);
                for (int i = 0; i < materials.Count; i++)
                {
                    iGrid1.Rows[i].Cells[0].Value = materials[i].Id;
                    iGrid1.Rows[i].Cells[1].Value = materials[i].Name;
                    iGrid1.Rows[i].Cells[2].Value = materials[i].MeasureUnits;
                    iGrid1.Rows[i].Cells[3].Value = materials[i].Total;
                    iGrid1.Rows[i].Tag = materials[i];
                }
            }
            finally
            {
                iGrid1.EndUpdate();
            }
        }

        public void RedrawRow(int index)
        {
            ExtractedObject element = iGrid1.Rows[index].Tag as ExtractedObject;
            iGrid1.Rows[index].Cells[3].Value = element.Total;
        }

        private void MaterialsView_Load(object sender, EventArgs e)
        {
            Text = "Электронная ведомость материалов";
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref SaveClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void iGrid1_CellDoubleClick(object sender, iGCellDoubleClickEventArgs e)
        {
            if (IntermechObjectClick != null)
            {
                ExtractedObject selectedElement = iGrid1.Rows[e.RowIndex].Tag as ExtractedObject;
                IntermechObjectClick(sender, new ExtractedObjectEventArgs(selectedElement, e.RowIndex));
            }
        }
    }
}
