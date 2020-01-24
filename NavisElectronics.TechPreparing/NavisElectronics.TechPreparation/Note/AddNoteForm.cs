namespace NavisElectronics.TechPreparation.Views
{
    using System.Windows.Forms;

    using ViewInterfaces;

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
            noteTextBox.Text = text;
            noteTextBox.Select();
        }

        /// <summary>
        /// Метод получения данных с формы
        /// </summary>
        /// <returns></returns>
        public string GetNote()
        {
            return noteTextBox.Text;
        }

        private void noteTextBox_KeyDown(object sender, KeyEventArgs e)
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
