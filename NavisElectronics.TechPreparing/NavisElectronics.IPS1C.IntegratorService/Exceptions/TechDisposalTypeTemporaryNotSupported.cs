using System;

namespace NavisElectronics.IPS1C.IntegratorService.Exceptions
{
    public class TechDisposalTypeTemporaryNotSupportedException : Exception 
    {
        public TechDisposalTypeTemporaryNotSupportedException(string message) : base (message)
        {
            
        }
    }
}