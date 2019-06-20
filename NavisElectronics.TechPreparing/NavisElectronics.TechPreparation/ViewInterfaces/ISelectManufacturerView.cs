// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISelectManufacturerView.cs" company="NavisElectronics">
//   ----
// </copyright>
// <summary>
//   Defines the ISelectManufacturerView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;

    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// Интерфейс окна выбора агентов
    /// </summary>
    public interface ISelectManufacturerView: IView
    {
        /// <summary>
        /// Событие загрузки формы
        /// </summary>
        event EventHandler Load;

        /// <summary>
        /// СОбытие выбора агента
        /// </summary>
        event EventHandler<Agent> SelectAgent;
        
        /// <summary>
        /// Метод заполнения агентами списка
        /// </summary>
        /// <param name="agents"></param>
        void FillAgents(ICollection<Agent> agents);
    }
}