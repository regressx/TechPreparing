using System.Windows.Forms;

namespace NavisElectronics.ListOfCooperation.Presenters
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