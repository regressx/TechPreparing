﻿namespace NavisElectronics.TechPreparation.ViewModels
{
    using System.Collections.Generic;

    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// Моделька для окна параметров
    /// </summary>
    public interface IMultiplyParametersViewModel
    {
        /// <summary>
        /// Метод получения зарегистрированных параметров
        /// </summary>
        /// <returns>Возвращает набор параметров для передачи в представление</returns>
        IList<Parameter> GetRegisteredParameters();
        
        /// <summary>
        /// Метод для регистрации параметра
        /// </summary>
        /// <param name="parameter">Регистрируемый параметр</param>
        void RegisterParameter(Parameter parameter);
    }
}