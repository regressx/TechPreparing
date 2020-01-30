using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Intermech;
using Intermech.Interfaces;
using Intermech.Interfaces.BlobStream;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Interfaces.Enums;
using NavisElectronics.TechPreparation.Interfaces.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace NavisElectronics.TechPreparation.Data
{
    /// <summary>
    /// Класс для записи технологического атрибута в базу IPS
    /// </summary>
    public class IntermechWriter : IDatabaseWriter
    {

        public void WriteFileAttribute(long orderId, IntermechTreeElement element)
        {
            WriteFileAttribute(orderId, element, new Progress<ProgressReport>());
        }

        /// <summary>
        /// The write organization struct attribute.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void WriteBlobAttribute<T>(long orderId, T element, int blobAttributeId, string comment, ISerializeStrategy<T> serializeStrategy)
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
            byte[] bytes = serializeStrategy.Serialize(element);


            // если есть, что писать, пишем в базу
            if (bytes.Length > 0)
            {
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject orderObject = keeper.Session.GetObject(orderId);
                    IDBAttribute blobAttribute = orderObject.GetAttributeByID(blobAttributeId);

                    if (blobAttribute != null)
                    {
                        blobAttribute.Delete(0L);
                    }

                    blobAttribute = orderObject.Attributes.AddAttribute(blobAttributeId, false);

                    BlobInformation info = new BlobInformation(bytes.Length, 0, DateTime.Now, comment, ArcMethods.ZLibPacked,  comment);
                    using (BlobWriterStream bws =
                        new BlobWriterStream(blobAttribute, bytes.Length, info, keeper.Session))
                    {
                        bws.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }


        /// <summary>
        /// Метод записи атрибута в базу данных.
        /// </summary>
        /// <param name="orderId">
        /// Идентификатор версии объекта заказа, к которому будем прикреплять бинарный атрибут 
        /// </param>
        /// <param name="element">
        /// Элемент для записи
        /// </param>
        public void WriteFileAttribute(long orderId, IntermechTreeElement element, IProgress<ProgressReport> progress)
        {
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject orderObject = keeper.Session.GetObject(orderId);
                if (orderObject.CheckoutBy  != keeper.Session.UserID)
                {
                    throw new Exception("Объект взят на редактирование не Вами. Вы не можете сохранять данные, пока сами не возьмете на редактирование");
                }
            }
            ProgressReport progressReport = new ProgressReport();
            progressReport.Percent = 0;
            progressReport.Message = "Начинаю сериализацию";
            progress.Report(progressReport);

            IntermechTreeElement elementToSave = (IntermechTreeElement)element.Clone();

            byte[] bytes = SerializeTreeElement(elementToSave);

            // если есть, что писать, пишем в базу
            if (bytes.Length > 0)
            {
                progressReport.Percent = 50;
                progressReport.Message = "Записываю в базу данных";
                progress.Report(progressReport);

                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject orderObject = keeper.Session.GetObject(orderId);
                    IDBAttribute fileAttribute = orderObject.GetAttributeByID(ConstHelper.FileAttribute);

                    if (fileAttribute != null)
                    {
                        fileAttribute.Delete(0L);
                    }

                    fileAttribute = orderObject.Attributes.AddAttribute(ConstHelper.FileAttribute, false);

                    string fileName = string.Format("{0}_{1}_изм_{2}.bson", element.Name, Math.Abs(element.Id), orderObject.VersionID);

                    BlobInformation info = new BlobInformation(bytes.Length, 0, DateTime.Now, fileName, ArcMethods.ZLibPacked,  string.Format("Сериализованный в BSON IntermechTreeElement от {0}", DateTime.Now));
                    using (BlobWriterStream bws =
                        new BlobWriterStream(fileAttribute, bytes.Length, info, keeper.Session))
                    {
                        bws.Write(bytes, 0, bytes.Length);
                    }
                }
                progressReport.Percent = 100;
                progressReport.Message = "Сохранено в " + DateTime.Now;
                progress.Report(progressReport);
            }
        }

        private byte[] SerializeTreeElement(IntermechTreeElement elementToSave)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();

            while (queue.Count != 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.UseAmount = 0;
                elementFromQueue.AmountWithUse = 0;
                elementFromQueue.TotalAmount = 0;
                elementFromQueue.NodeState = NodeStates.Default;
                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }

            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (JsonWriter jsonWriter = new BsonWriter(ms))
                {
                    jsonSerializer.Serialize(jsonWriter, elementToSave);
                }

                bytes = ms.ToArray();
            }

            return bytes;
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

        /// <summary>
        /// The write organization struct attribute async.
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
        public Task WriteBlobAttributeAsync<T>(long orderId, T element, int blobAttributeId, string comment, ISerializeStrategy<T> serializeStrategy)
        {
            return Task.Run(() =>
            {
                WriteBlobAttribute<T>(orderId, element, blobAttributeId, comment, serializeStrategy);
            });
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
        /// <param name="progress">
        /// The progress.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task WriteFileAttributeAsync(long orderId, IntermechTreeElement element, IProgress<ProgressReport> progress)
        {
            return Task.Run(() =>
            {
                WriteFileAttribute(orderId, element, progress);
            });
        }

    }
}