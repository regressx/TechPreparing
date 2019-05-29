namespace NavisElectronics.ListOfCooperation.Presenters
{
    /// <summary>
    /// Абстрактный класс для всех представителей
    /// </summary>
    /// <typeparam name="TParameter">
    /// Входной параметр для запуска
    /// </typeparam>
    public abstract class BasePresenter<TParameter> : IPresenter<TParameter>
    {
        /// <summary>
        /// Запуск представителя
        /// </summary>
        /// <param name="parameter">
        /// Параметр, с которым надо запускать окно
        /// </param>
        public abstract void Run(TParameter parameter);
    }
}