// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Agent.cs" company="">
//   
// </copyright>
// <summary>
//   Класс, описывающий изготовителя
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Entities
{
    /// <summary>
    /// Класс, описывающий изготовителя
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Идентификатор из IPS
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