using System;

namespace NavisElectronics.TechPreparation.Exceptions
{
    public class OrganizationStructNotDownloadedException : Exception
    {
        public OrganizationStructNotDownloadedException(string message) : base(message)
        {
            
        }

        public OrganizationStructNotDownloadedException(string message, Exception ex) : base(message, ex)
        {
            
        }
    }
}