using System;

namespace NavisElectronics.ListOfCooperation.ViewInterfaces
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ViewModels;

    /// <summary>
    /// Интерфейс для окна с динамическими параметрами
    /// </summary>
    public interface IMultiplyParametersView
    {
        /// <summary>
        /// Метод для заполнения таблицы
        /// </summary>
        /// <param name="parameters">
        /// набор параметров для объекта
        /// </param>
        void FillGrid(IList<Parameter> parameters);

        /// <summary>
        /// Показывает оконный диалог
        /// </summary>
        /// <returns>Возвращает диалог</returns>
        DialogResult ShowDialog();

        event EventHandler Load;
    }
}