using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace NavisElectronics.IPS1C.IntegratorService.Entities
{
    /// <summary>
    /// Узел для хранения хэша
    /// </summary>
    [DataContract]
    [Serializable]
    public class HashAlgorithmNode
    {
        /// <summary>
        /// набор потомков
        /// </summary>
        private IList<HashAlgorithmNode> _children;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashAlgorithmNode"/> class.
        /// </summary>
        public HashAlgorithmNode()
        {
            _children = new List<HashAlgorithmNode>();
        }

        /// <summary>
        /// Имя алгоритма хэширования
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение хэша
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Набор дочерних элементов
        /// </summary>
        public IList<HashAlgorithmNode> Children
        {
            get
            {
                return new ReadOnlyCollection<HashAlgorithmNode>(_children);
            }
        }

        public void Add(HashAlgorithmNode node)
        {
            _children.Add(node);
        }

    }
}