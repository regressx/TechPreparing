﻿namespace NavisElectronics.ListOfCooperation.Entities
{
    /// <summary>
    /// Сущность для документа
    /// </summary>
    public class Document : IObjectView
    {
        /// <summary>
        /// Идентификатор из IPS
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Обозначение
        /// </summary>
        public string Designation { get; set; }
    }
}