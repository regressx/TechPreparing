namespace NavisElectronics.TechPreparation.Exceptions
{
    using System;

    public class FileAttributeIsEmptyException:Exception
    {
        public FileAttributeIsEmptyException(string message):base (message)
        {
            
        }

        public FileAttributeIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

    }
}