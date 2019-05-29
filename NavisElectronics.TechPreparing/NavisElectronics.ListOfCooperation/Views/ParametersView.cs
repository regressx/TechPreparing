using System.Windows.Forms;
using NavisElectronics.ListOfCooperation.ViewInterfaces;

namespace NavisElectronics.ListOfCooperation
{
    public partial class ParametersView : Form, IParametersView
    {
        public ParametersView()
        {
            InitializeComponent();
            textBox1.Select();
        }

        public double GetStockRate()
        {
            string str = textBox1.Text.Replace('.', ',');
            return double.Parse(str);
        }

        public string GetSampleSize()
        {
            return string.Format("{0} %", textBox2.Text);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == (char)Keys.Back)
                {
                    e.Handled = false;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == ',' || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
