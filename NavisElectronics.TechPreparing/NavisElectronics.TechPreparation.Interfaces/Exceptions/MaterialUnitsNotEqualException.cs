using System;

namespace NavisElectronics.TechPreparation.Exceptions
{
    /// <summary>
    /// The material units not equal exception.
    /// </summary>
    public class MaterialUnitsNotEqualException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialUnitsNotEqualException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public MaterialUnitsNotEqualException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialUnitsNotEqualException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public MaterialUnitsNotEqualException(string message, Exception innerException) : base(message,innerException)
        {
            
        }
    }
}