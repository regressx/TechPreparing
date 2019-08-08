namespace NavisElectronics.TechPreparation.ViewModels.TreeNodes
{
    using Aga.Controls.Tree;
    using Entities;

    /// <summary>
    /// Узел дерева для кооперации
    /// </summary>
    public class CooperationNode : Node
    {
        /// <summary>
        /// Идентификатор версии объекта
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Тип узла
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
        /// <summary>
        /// Флаг кооперации
        /// </summary>
        public bool CooperationFlag { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// Количество с применяемостью
        /// </summary>
        public string AmountWithUse { get; set; }
        /// <summary>
        /// Всего с учетом коэффициента запаса
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// Коэффициент запаса
        /// </summary>
        public string StockRate { get; set; }
        /// <summary>
        /// Объем выборки
        /// </summary>
        public string SampleSize { get; set; }
        /// <summary>
        /// Ссылка на тех. процесс
        /// </summary>
        public TechProcess TechProcessReference { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Информация о заменах
        /// </summary>
        public string SubstituteInfo { get; set; }
        /// <summary>
        /// Флаг служит для проверки простановки кооперации
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Является ли печатной платой
        /// </summary>
        public bool IsPcb { get; set; }

        /// <summary>
        /// Версия печатной платы
        /// </summary>
        public int PcbVersion { get; set; }

        /// <summary>
        /// Тех. задание
        /// </summary>
        public string TechTask { get; set; }

        /// <summary>
        /// Переопределяем ToString на отображение обозначения и наименования
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format($"{Designation} {Name}").Trim();

        }
    }
}