namespace NavisElectronics.TechPreparation.Views
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewInterfaces;

    public partial class MultiplyParametersView : Form, IMultiplyParametersView
    {
        public MultiplyParametersView()
        {
            InitializeComponent();
        }

        public void FillGrid(IList<Parameter> parameters)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = parameters;
            int i = 0;
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {
                dataGridViewRow.HeaderCell.Value = parameters[i].Name;
                i++;
            }

            dataGridView1.RowHeadersWidth = 200;

        }
    }
}
