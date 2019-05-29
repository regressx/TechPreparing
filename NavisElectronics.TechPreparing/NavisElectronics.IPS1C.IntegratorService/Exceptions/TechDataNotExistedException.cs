using System;

namespace NavisElectronics.IPS1C.IntegratorService.Exceptions
{
    public class TechDataNotExistedException: Exception
    {
        public TechDataNotExistedException(string message): base(message)
        {
            
        }

        public TechDataNotExistedException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}