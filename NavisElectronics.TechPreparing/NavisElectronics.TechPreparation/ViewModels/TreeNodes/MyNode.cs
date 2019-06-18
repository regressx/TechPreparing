namespace NavisElectronics.TechPreparation.ViewModels.TreeNodes
{
    using Aga.Controls.Tree;

    /// <summary>
    /// Наследник Node для компонента Aga.Controls.Tree. Наделяем его всякими свойствами для отображения, а затем строим с его помощью модель
    /// </summary>
    public class  MyNode:Node
    {
        public long Id { get; set; }
        public bool IsPcb { get; set; }
        public int PcbVersion { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="str"></param>
        public MyNode(string str):base(str)
        {
            
        }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Обозначение
        /// </summary>
        public string Designation { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// Строка маршрута
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// Примечание к маршруту
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// Флаг кооперации
        /// </summary>
        public bool CooperationFlag { get; set; }
        /// <summary>
        /// Информация о заменах
        /// </summary>
        public string SubInfo { get; set; }
        /// <summary>
        /// Изготовитель
        /// </summary>
        public string Agent { get; set; }
        /// <summary>
        /// Номер по порядку
        /// </summary>
        public string NumberInOrder { get;set; }
        /// <summary>
        /// Тип изделия
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Уровень в дереве
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Номер узла на уровне
        /// </summary>
        public int NumberOnLevel { get; set; }
        /// <summary>
        /// Количество с применяемостью
        /// </summary>
        public int AmountWithUse { get; set; }

        /// <summary>
        /// True, если узел изготавливает несколько предприятий
        /// </summary>
        public bool IsMultipleAgents { get; set; }

        /// <summary>
        /// True, если изготовитель узла отличается от выбранного по фильтру
        /// </summary>
        public bool AnotherAgent { get; set; }

        /// <summary>
        /// Наличие внутрипроизводственной кооперации у узла
        /// </summary>
        public bool InnerCooperation { get; set; }

        /// <summary>
        /// Наличие внутрипроизводственной кооперации у узла
        /// </summary>
        public bool ContainsInnerCooperation { get; set; }

        /// <summary>
        /// Является ли узлом для комплектования
        /// </summary>
        public bool IsToComplect { get; set; }

        public string TechPreparing { get; set; }

        public string TechProcessReference { get; set; }

        public double StockRate { get; set; }

        public string SampleSize { get; set; }
    }
}