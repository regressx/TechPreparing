namespace NavisElectronics.TechPreparation.Views
{
    using System.Windows.Forms;

    public partial class ReportForm : Form
    {
        public ReportForm(string str)
        {
            InitializeComponent();
            textBox1.Text = str;
        }
    }
}
