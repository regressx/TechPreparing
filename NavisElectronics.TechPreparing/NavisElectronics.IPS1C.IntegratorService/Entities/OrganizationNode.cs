namespace NavisElectronics.IPS1C.IntegratorService.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Единица структуры организации
    /// </summary>
    [DataContract]
    [Serializable]
    public class OrganizationNode
    {
        /// <summary>
        /// Потомки
        /// </summary>
        private IList<OrganizationNode> _children;

        public OrganizationNode()
        {
            _children = new List<OrganizationNode>();
        }

        /// <summary>
        /// Идентификатор тех. узла
        /// </summary>
        [DataMember]
        public string Id { get;set; }

        /// <summary>
        /// Тип
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Участок
        /// </summary>
        [DataMember]
        public string PartitionName { get; set; }

        /// <summary>
        /// Цех
        /// </summary>
        [DataMember]
        public string WorkshopName { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор участка
        /// </summary>
        [DataMember]
        public string PartitionId { get; set; }

        /// <summary>
        /// Идентификатор цеха
        /// </summary>
        [DataMember]
        public string WorkshopId { get; set; }

        /// <summary>
        /// Потомки
        /// </summary>
        [DataMember]
        public IList<OrganizationNode> Children
        {
            get
            {
                return new ReadOnlyCollection<OrganizationNode>(_children);
            }
        }

        /// <summary>
        /// Добавить потомка
        /// </summary>
        /// <param name="childTreeNode">
        /// The child tree node.
        /// </param>
        public void Add(OrganizationNode childTreeNode)
        {
            _children.Add(childTreeNode);
        }
    }
}