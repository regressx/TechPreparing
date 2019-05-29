namespace NavisElectronics.ListOfCooperation.IO
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Enums;
    using Intermech;
    using Intermech.Interfaces;
    using Intermech.Interfaces.BlobStream;

    /// <summary>
    /// Класс для записи технологического атрибута в базу IPS
    /// </summary>
    public class IntermechWriter : IDatabaseWriter
    {
        /// <summary>
        /// Метод записи атрибута в базу данных.
        /// </summary>
        /// <param name="orderId">
        /// Идентификатор версии объекта заказа, к которому будем прикреплять бинарный атрибут 
        /// </param>
        /// <param name="ds">
        /// Dataset
        /// </param>
        public void WriteDataSet(long orderId, System.Data.DataSet ds)
        {
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject orderObject = keeper.Session.GetObject(orderId);
                if (orderObject.CheckoutBy  != keeper.Session.UserID)
                {
                    throw new Exception("Объект взят на редактирование не Вами. Вы не можете сохранять данные, пока сами не возьмете на редактирование");
                }
            }

            // сериализуем
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, ds);
                bytes = ms.ToArray();
            }

            // если есть, что писать, пишем в базу
            if (bytes.Length > 0)
            {
                BlobInformation info = new BlobInformation(bytes.Length, 0, DateTime.Now,string.Empty, ArcMethods.ZLibPacked,  string.Format("Dataset от {0}", DateTime.Now));

                using (SessionKeeper keeper = new SessionKeeper())
                {
                    if (orderId > 0)
                    {
                        orderId = -orderId;
                    }
                    IDBObject orderObject = keeper.Session.GetObject(orderId);
                    IDBAttribute binaryAttribute = orderObject.GetAttributeByID(Helpers.ConstHelper.ShortNameAttribute);

                    if (binaryAttribute != null)
                    {
                        binaryAttribute.Delete(0L);
                    }

                    IDBAttribute newBinaryAttribute = orderObject.Attributes.AddAttribute(Helpers.ConstHelper.ShortNameAttribute, false);

                    using (BlobWriterStream bws =
                        new BlobWriterStream(newBinaryAttribute, bytes.Length, info, keeper.Session))
                    {
                        bws.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }
    }
}