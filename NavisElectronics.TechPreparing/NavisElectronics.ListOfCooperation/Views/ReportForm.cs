using System.Windows.Forms;

namespace NavisElectronics.ListOfCooperation.Views
{
    public partial class ReportForm : Form
    {
        public ReportForm(string str)
        {
            InitializeComponent();
            textBox1.Text = str;
        }
    }
}
