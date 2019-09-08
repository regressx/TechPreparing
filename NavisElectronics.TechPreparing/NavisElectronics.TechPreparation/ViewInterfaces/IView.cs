namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    /// <summary>
    /// Общий интерфейс для всех представлений
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Показать форму
        /// </summary>
        void Show();

        /// <summary>
        /// Закрыть форму
        /// </summary>
        void Close();
    }
}