// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPresenter.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Интерфейс представителя
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Presenters
{
    /// <summary>
    /// Интерфейс представителя
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Запускает прикрепленное представление
        /// </summary>
        void Run();
    }

    /// <summary>
    /// The Presenter interface.
    /// </summary>
    /// <typeparam name="TParameter">
    /// Входной параметр для запуска представителя
    /// </typeparam>
    public interface IPresenter<in TParameter>
    {
        /// <summary>
        /// Запускает прикрепленное представление с параметром
        /// </summary>
        /// <param name="parameter">
        /// Входной параметр
        /// </param>
        void Run(TParameter parameter);
    }

}