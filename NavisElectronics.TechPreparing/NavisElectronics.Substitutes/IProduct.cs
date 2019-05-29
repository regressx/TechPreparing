namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// Описание интерфейса для изделия. Используется в модуле допустимых замен
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// ID версии объекта
        /// </summary>
        long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get;}

        /// <summary>
        /// Количество
        /// </summary>
        float Amount { get; set; }

        /// <summary>
        /// Обозначение
        /// </summary>
        string Designation { get; set; }

        /// <summary>
        /// Номер группы допустимых замен
        /// </summary>
        int SubstituteGroupNumber { get; set; }

        /// <summary>
        /// Номер в группе допустимых замен
        /// </summary>
        int SubstituteNumberInGroup { get; set; }

        /// <summary>
        /// Информация о допустимых заменах
        /// </summary>
        string SubstituteInfo { get; set; }

        /// <summary>
        /// Позиция в спецификации
        /// </summary>
        string Position { get; set; }

        /// <summary>
        /// Тип изделия
        /// </summary>
        int Type { get; }

    }
}
