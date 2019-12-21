using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavisElectronics.TechPreparation.TechRouteMap
{
    public partial class ProductionTypeDialog : Form
    {
        public ProductionTypeDialog()
        {
            InitializeComponent();
        }

        public string ProductionTypeValue { get; set; }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (listBox1.SelectedItem != null)
                {
                    DialogResult = DialogResult.OK;
                    ProductionTypeValue = (string) listBox1.SelectedItem;
                    this.Close();
                }
            }
        }
    }
}
