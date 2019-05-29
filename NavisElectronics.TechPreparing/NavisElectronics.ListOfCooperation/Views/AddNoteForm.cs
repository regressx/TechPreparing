using NavisElectronics.ListOfCooperation.ViewInterfaces;

namespace NavisElectronics.ListOfCooperation
{
    using System.Windows.Forms;
    using Presenters;

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
    }
}
