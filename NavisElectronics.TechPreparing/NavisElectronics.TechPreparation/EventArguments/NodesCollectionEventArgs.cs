namespace NavisElectronics.TechPreparation.EventArguments
{
    using System;
    using System.Collections.Generic;

    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Аргумент для события копирования узлов в буфер
    /// </summary>
    public class NodesCollectionEventArgs : EventArgs
    {
        /// <summary>
        /// Узлы, которые были скопированы
        /// </summary>
        private readonly ICollection<MyNode> _nodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodesCollectionEventArgs"/> class.
        /// </summary>
        /// <param name="nodes">
        /// Узлы, скопированные в буфер
        /// </param>
        public NodesCollectionEventArgs(ICollection<MyNode> nodes)
        {
            _nodes = nodes;
        }

        /// <summary>
        /// Узлы, находящиеся в буфере обмена
        /// </summary>
        public ICollection<MyNode> Nodes
        {
            get { return _nodes; }
        }
    }
}