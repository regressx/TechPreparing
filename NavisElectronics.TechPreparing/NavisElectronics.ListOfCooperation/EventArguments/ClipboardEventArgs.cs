namespace NavisElectronics.ListOfCooperation.EventArguments
{
    using System;
    using System.Collections.Generic;
    using ViewModels;

    /// <summary>
    /// Аргумент для события копирования узлов в буфер
    /// </summary>
    public class ClipboardEventArgs : EventArgs
    {
        /// <summary>
        /// Узлы, которые были скопированы
        /// </summary>
        private readonly ICollection<MyNode> _nodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClipboardEventArgs"/> class.
        /// </summary>
        /// <param name="nodes">
        /// Узлы, скопированные в буфер
        /// </param>
        public ClipboardEventArgs(ICollection<MyNode> nodes)
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