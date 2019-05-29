using System;

namespace NavisElectronics.ListOfCooperation.Exceptions
{
    public class DataSetIsEmptyException:Exception
    {
        public DataSetIsEmptyException(string message):base (message)
        {
            
        }

        public DataSetIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

    }
}