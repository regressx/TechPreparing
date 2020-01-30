namespace NavisElectronics.Orders.TreeComparer
{
    using System;
    using TechPreparation.Interfaces.Entities;

    /// <summary>
    /// Аргументы для события сравнения двух единичных узлов
    /// </summary>
    public class CompareTwoNodesEventArgs : EventArgs
    {
        /// <summary>
        /// Элемент в левом дереве
        /// </summary>
        private readonly IntermechTreeElement _leftElement;
        
        /// <summary>
        /// Элемент в правом дереве
        /// </summary>
        private readonly IntermechTreeElement _rightElement;


        /// <summary>
        /// Initializes a new instance of the <see cref="CompareTwoNodesEventArgs"/> class.
        /// </summary>
        /// <param name="leftElement">
        /// Элемент в левом дереве
        /// </param>
        /// <param name="rightElement">
        /// Элемент в правом дереве
        /// </param>
        public CompareTwoNodesEventArgs(IntermechTreeElement leftElement, IntermechTreeElement rightElement)
        {
            _leftElement = leftElement;
            _rightElement = rightElement;
        }

        /// <summary>
        /// Элемент в левом дереве
        /// </summary>
        public IntermechTreeElement LeftElement
        {
            get { return _leftElement; }
        }

        /// <summary>
        /// Элемент в правом дереве
        /// </summary>
        public IntermechTreeElement RightElement
        {
            get { return _rightElement; }
        }
    }
}