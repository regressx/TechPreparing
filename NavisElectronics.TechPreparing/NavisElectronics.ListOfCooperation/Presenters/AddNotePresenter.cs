namespace NavisElectronics.ListOfCooperation.Presenters
{
    using System.Windows.Forms;
    using ViewInterfaces;

    /// <summary>
    /// Посредник между окном ввода примечания и некоторой логикой
    /// </summary>
    public class AddNotePresenter
    {
        /// <summary>
        /// Интерфейс представления
        /// </summary>
        private IAddNoteView _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNotePresenter"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        public AddNotePresenter(IAddNoteView view)
        {
            _view = view;
        }

        /// <summary>
        /// Метод запуска посредника. Окно будет запущено в модальном режиме
        /// </summary>
        /// <returns>
        /// The <see cref="DialogResult"/>.
        /// </returns>
        public DialogResult RunDialog()
        {
            return _view.ShowDialog();
        }

        /// <summary>
        /// Метод возвращает то, что было введено в текстовое поле представления
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNote()
        {
            return _view.GetNote();
        }
    }
}