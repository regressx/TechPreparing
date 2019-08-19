using System;
using System.Threading.Tasks;

namespace NavisElectronics.TechPreparation.Interfaces
{
    using Entities;

    /// <summary>
    /// Интерфейс записи в базу данных
    /// </summary>
    public interface IDatabaseWriter
    {
        /// <summary>
        /// Метод записи в атрибут файл
        /// </summary>
        /// <param name="orderId">
        /// Идентификатор заказа, в который будет производиться запись
        /// </param>
        /// <param name="element">
        ///  Элемент для сериализации, который будем писать в файл
        /// </param>
        void WriteFileAttribute(long orderId, IntermechTreeElement element);

        /// <summary>
        /// Асинхронная запись в атрибут Файл
        /// </summary>
        /// <param name="orderId">
        /// Идентификатор версии объекта
        /// </param>
        /// <param name="element">
        /// Элемент для записи
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task WriteFileAttributeAsync(long orderId, IntermechTreeElement element);


        /// <summary>
        /// The write organization struct attribute async.
        /// </summary>
        /// <param name="orderId">
        ///     The order id.
        /// </param>
        /// <param name="element">
        ///     The element.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task WriteBlobAttributeAsync<T>(long orderId, T element, int blobAttributeId, string comment);


        /// <summary>
        /// The write organization struct attribute.
        /// </summary>
        /// <param name="orderId">
        /// The order id.
        /// </param>
        /// <param name="element">
        /// The element.
        /// </param>
        void WriteBlobAttribute<T>(long orderId, T element, int blobAttributeId, string comment);


        /// <summary>
        /// Асинхронная запись в атрибут Файл
        /// </summary>
        /// <param name="orderId">
        /// Идентификатор версии объекта
        /// </param>
        /// <param name="element">
        /// Элемент для записи
        /// </param>
        /// <param name="progress">
        /// The progress.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task WriteFileAttributeAsync(long orderId, IntermechTreeElement element, IProgress<ProgressReport> progress);

    }
}