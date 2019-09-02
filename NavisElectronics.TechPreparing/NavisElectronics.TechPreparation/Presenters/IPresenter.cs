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


    /// <summary>
    /// The Presenter interface.
    /// </summary>
    /// <typeparam name="TParameter">
    /// Входной параметр для запуска представителя
    /// </typeparam>
    /// <typeparam name="T1Parameter">
    /// Еще один параметр для представителя
    /// </typeparam>
    public interface IPresenter<in TParameter, in T1Parameter>
    {
        /// <summary>
        /// Запускает прикрепленное представление с параметром
        /// </summary>
        /// <param name="parameter">
        /// Входной параметр
        /// </param>
        void Run(TParameter parameter, T1Parameter parameterT1);
    }


    /// <summary>
    /// The Presenter interface.
    /// </summary>
    /// <typeparam name="TParameter">
    /// Входной параметр для запуска представителя
    /// </typeparam>
    /// <typeparam name="T1Parameter">
    /// Еще один параметр для представителя
    /// </typeparam>
    public interface IPresenter<in TParameter, in T1Parameter, in T2Parameter>
    {
        /// <summary>
        /// Запускает прикрепленное представление с параметром
        /// </summary>
        /// <param name="parameter">
        /// Входной параметр
        /// </param>
        void Run(TParameter parameter, T1Parameter parameterT1, T2Parameter parameter2);
    }

}