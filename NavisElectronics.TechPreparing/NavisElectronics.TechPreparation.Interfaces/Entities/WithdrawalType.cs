// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WithdrawalType.cs" company="">
//   
// </copyright>
// <summary>
//   Класс, представляющий тип тех. отхода
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace NavisElectronics.TechPreparation.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Класс, представляющий тип тех. отхода
    /// </summary>
    [Serializable]
    public class WithdrawalType
    {
        /// <summary>
        /// Набор типов
        /// </summary>
        private ICollection<WithdrawalType> _types;

        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawalType"/> class.
        /// </summary>
        public WithdrawalType()
        {
            _types = new List<WithdrawalType>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор типа
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Описание типа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Значение типа
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Год, на который действует приказ
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Коллекция типов тех. отхода
        /// </summary>
        public ICollection<WithdrawalType> Types
        {
            get { return _types; }
        }

        /// <summary>
        /// Добавление узла в коллекцию тех. отходов
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public void Add(WithdrawalType type)
        {
            _types.Add(type);
        }
    }
}