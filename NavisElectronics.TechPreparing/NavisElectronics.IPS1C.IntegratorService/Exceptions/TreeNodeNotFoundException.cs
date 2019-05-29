using System;

namespace NavisElectronics.IPS1C.IntegratorService.Exceptions
{
    public class TreeNodeNotFoundException:Exception
    {
        public TreeNodeNotFoundException(string str):base(str)
        {
        }

        public TreeNodeNotFoundException(string str, Exception innerException):base(str,innerException)
        {

        }
    }
}