using System;

namespace NavisElectronics.TechPreparation.Interfaces.Exceptions
{
    /// <summary>
    /// Исключение для выброса в случае, когда не был найден узел дерева
    /// </summary>
    public class TreeNodeNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNodeNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public TreeNodeNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNodeNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public TreeNodeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

    }
}