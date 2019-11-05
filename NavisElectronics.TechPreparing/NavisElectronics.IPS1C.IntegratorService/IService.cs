namespace NavisElectronics.IPS1C.IntegratorService
{
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


        /// <summary>
        /// The get all products.
        /// </summary>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// </returns>
        [OperationContract]
        ProductTreeNode GetAllProducts();

        /// <summary>
        /// Выдает заголовок изделия и тип по его ID
        /// </summary>
        /// <param name="objectId">
        /// Id объекта
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// </returns>
        [OperationContract]
        ProductTreeNode GetProductInfo(long objectId);


        /// <summary>
        /// Получает дерево заказа
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии объекта
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// Возвращает узел главный узел дерева
        /// </returns>
        [OperationContract]
        ProductTreeNode GetOrder(long versionId);


        /// <summary>
        /// Получает дерево организации, используемой в заказе
        /// </summary>
        /// <param name="orderVersionId">
        /// Идентификатор версии объекта заказа
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// Возвращает дерево организации
        /// </returns>
        [OperationContract]
        OrganizationNode GetOrganizationStruct(long orderVersionId);

        /// <summary>
        /// Получить хэши файла заказа
        /// </summary>
        /// <param name="orderVersionId">
        /// The order version id.
        /// </param>
        /// <returns>
        /// The <see cref="HashAlgorithmNode"/>.
        /// </returns>
        [OperationContract]
        HashAlgorithmNode GetOrderFileHash(long orderVersionId);


        /// <summary>
        /// Получить количество с учетом тех. отхода
        /// </summary>
        /// <param name="objectVersionId">
        /// The order version id.
        /// </param>
        /// <returns>
        /// The <see cref="HashAlgorithmNode"/>.
        /// </returns>
        [OperationContract]
        string GetTechDisposal(long objectVersionId, double totalAmount, int year);
    }
}
