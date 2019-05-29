using System;

namespace NavisElectronics.IPS1C.IntegratorService.Exceptions
{
    public class WrongTypeCallException: Exception
    {
        public WrongTypeCallException(string message) : base(message)
        {
            
        }

        public WrongTypeCallException(string message, Exception innerException):base(message,innerException)
        {
            
        }
    }
}