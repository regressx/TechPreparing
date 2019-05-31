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

    /// <summary>
    /// Абстрактный класс для всех представителей
    /// </summary>
    /// <typeparam name="TParameter">
    /// Входной параметр для запуска
    /// </typeparam>
    public abstract class BasePresenter<T1,T2> : IPresenter<T1,T2>
    {
        /// <summary>
        /// Запуск представителя
        /// </summary>
        /// <param name="parameter">
        /// Параметр, с которым надо запускать окно
        /// </param>
        public abstract void Run(T1 parameter1, T2 parameter2);
    }


}