using System;

namespace NavisElectronics.TechPreparation.Interfaces.Exceptions
{
    public class DeleteAttempFoundException : Exception
    {
        public DeleteAttempFoundException(string message) : base (message)
        {
            
        }

        public DeleteAttempFoundException(string message, Exception ex) : base(message,ex)
        {
            
        }

    }
}