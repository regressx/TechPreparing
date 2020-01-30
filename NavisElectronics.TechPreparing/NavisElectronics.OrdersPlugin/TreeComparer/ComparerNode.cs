namespace NavisElectronics.Orders.TreeComparer
{
    using Aga.Controls.Tree;
    using TechPreparation.Interfaces.Enums;

    /// <summary>
    /// Узел для отображения в Aga.Controls.Tree. Служит для построения модели данных для сравнения деревьев
    /// </summary>
    public class ComparerNode : Node
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
        /// Номер изменения
        /// </summary>
        public string ChangeNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether cooperation flag.
        /// </summary>
        public bool CooperationFlag { get; set; }

        /// <summary>
        /// Идентификатор версии объекта
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Идентификатор объекта (заменяет по сути наименование и обозначение)
        /// </summary>
        public long ObjectId { get; set; }
        
        /// <summary>
        /// Текущее состояние узла
        /// </summary>
        public NodeStates NodeState { get; set; }


        public string RelationType { get; set; }
    }
}