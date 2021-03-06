﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Agent.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Класс, описывающий изготовителя
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Interfaces.Entities
{
    /// <summary>
    /// Класс, описывающий изготовителя
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Идентификатор версии из IPS
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Имя изготовителя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Переопределенный метод ToString, возвращающий имя производителя
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}