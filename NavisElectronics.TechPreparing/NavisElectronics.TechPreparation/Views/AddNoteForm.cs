namespace NavisElectronics.TechPreparation.Views
{
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.ViewInterfaces;

    /// <summary>
    /// Форма для добавления данных примечания
    /// </summary>
    public partial class AddNoteForm : Form, IAddNoteView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddNoteForm"/> class. 
        /// Конструктор формы
        /// </summary>
        /// <param name="text">
        /// Текст из предыдущего примечания
        /// </param>
        public AddNoteForm(string text)
        {
            InitializeComponent();
            textBox1.Text = text;
            textBox1.Select();
        }

        /// <summary>
        /// Метод получения данных с формы
        /// </summary>
        /// <returns></returns>
        public string GetNote()
        {
            return textBox1.Text;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Enter)
                {
                   DialogResult = DialogResult.OK;
                   Close();
                }
            }
        }
    }
}
