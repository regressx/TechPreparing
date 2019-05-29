// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechProcess.cs" company="NavisElectronics">
//   Черкашин И.В.
// </copyright>
// <summary>
//   Класс, описывающий некоторые данные тех. процесса, такие как Id и наименование
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.ListOfCooperation.Entities
{
    using System;

    /// <summary>
    /// Класс, описывающий некоторые данные тех. процесса, такие как Id и наименование
    /// </summary>
    [Serializable]
    public class TechProcess
    {
        /// <summary>
        /// Возвращает пустой тех. процесс
        /// </summary>
        public static TechProcess Empty
        {
            get
            {
                return new TechProcess();
            }
        }

        /// <summary>
        /// Версия объекта ТП
        /// </summary>
        public long Id { get;set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }



        /// <summary>
        /// Переопределяем, чтобы отображалось наименование тех. процесса
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