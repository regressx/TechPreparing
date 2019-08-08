using System.Threading.Tasks;

namespace NavisElectronics.TechPreparing.Data
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Helpers;
    using Intermech;
    using Intermech.Interfaces;
    using Intermech.Interfaces.BlobStream;
    using TechPreparation.Entities;
    using TechPreparation.Interfaces;

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
        /// <param name="element">
        /// Элемент для записи
        /// </param>
        public void WriteFileAttribute(long orderId, IntermechTreeElement element)
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
                binaryFormatter.Serialize(ms, element);
                bytes = ms.ToArray();
            }

            // если есть, что писать, пишем в базу
            if (bytes.Length > 0)
            {
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject orderObject = keeper.Session.GetObject(orderId);
                    IDBAttribute fileAttribute = orderObject.GetAttributeByID(ConstHelper.FileAttribute);

                    if (fileAttribute != null)
                    {
                        fileAttribute.Delete(0L);
                    }


                    fileAttribute = orderObject.Attributes.AddAttribute(ConstHelper.FileAttribute, false);

                    string fileName = string.Format("{0}_{1}_изм_{2}", element.Name, element.Id, orderObject.VersionID);

                    BlobInformation info = new BlobInformation(bytes.Length, 0, DateTime.Now, fileName, ArcMethods.ZLibPacked,  string.Format("Сериализованный IntermechTreeElement от {0}", DateTime.Now));
                    using (BlobWriterStream bws =
                        new BlobWriterStream(fileAttribute, bytes.Length, info, keeper.Session))
                    {
                        bws.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        /// <summary>
        /// The write file attribute async.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task WriteFileAttributeAsync(long orderId, IntermechTreeElement element)
        {
            return Task.Run(() =>
            {
                WriteFileAttribute(orderId, element);
            });
        }
    }
}