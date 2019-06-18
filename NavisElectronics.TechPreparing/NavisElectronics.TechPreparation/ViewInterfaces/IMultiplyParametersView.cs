namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.Entities;

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