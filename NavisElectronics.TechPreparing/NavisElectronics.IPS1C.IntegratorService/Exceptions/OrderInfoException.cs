using System;

namespace NavisElectronics.IPS1C.IntegratorService.Exceptions
{
    /// <summary>
    /// Исключение для обработки данных заказа.
    /// </summary>
    public class OrderInfoException : Exception
    {
        public OrderInfoException(string message):base(message)
        {
            
        }

        public OrderInfoException(string message, Exception innterException): base(message,innterException)
        {

        }
    }
}