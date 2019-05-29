namespace NavisElectronics.IPS1C.IntegratorService
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities;

    /// <summary>
    /// Главный интерфейс сервиса
    /// </summary>
    [ServiceContract]
    public interface IService
    {
        /// <summary>
        /// Тестовый метод проброса сообщения через сервис
        /// </summary>
        /// <param name="message">
        /// Сообщение для проброса
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// Возращает сообщение, поданное на вход
        /// </returns>
        [OperationContract]
        string GetMessage(string message);

        [OperationContract]
        void NotifyIM(long ipsObjectId, long code1C);

        /// <summary>
        /// Получает состав объекта по идентификатору его версии
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии объекта
        /// </param>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// Возвращает коллекцию объектов, входящих в указанный
        /// </returns>
        [OperationContract]
        ICollection<ProductTreeNode> GetProductComposition(long versionId);


        /// <summary>
        /// Выдает заголовок изделия и тип по его ID
        /// </summary>
        /// <param name="id">
        /// Идентификатор 
        /// </param>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// Массив строк с соответствующим содержимым
        /// </returns>
        [OperationContract]
        ProductTreeNode GetProductInfo(long id);


        /// <summary>
        /// Получает отфильтрованный в соответствии с технологическими данными готовый состав заказа для КБ
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии объекта
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// Возвращает узел главный узел дерева
        /// </returns>
        [OperationContract]
        ProductTreeNode GetFilteredOrderForKB(long versionId);

        /// <summary>
        /// Получает отфильтрованный в соответствии с технологическими данными готовый состав заказа для Навис-Электроники
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии объекта
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// Возвращает узел главный узел дерева
        /// </returns>
        [OperationContract]
        ProductTreeNode GetFilteredOrderForElectronics(long versionId);


    }
}
