namespace NavisElectronics.TechPreparation.ViewModels.TreeNodes
{
    using Aga.Controls.Tree;

    /// <summary>
    /// Представление узлов дерева для главного окна
    /// </summary>
    public class ViewNode : Node
    {
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
        public double Amount { get; set; }

        /// <summary>
        /// Количество c применяемостью
        /// </summary>
        public double AmountWithUse { get; set; }

        /// <summary>
        /// Тип связи
        /// </summary>
        public string RelationName { get; set; }
        
        /// <summary>
        /// Примечание по связи
        /// </summary>
        public string RelationNote { get; set; }

    }
}