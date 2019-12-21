using Intermech.Interfaces;
using NavisElectronics.TechPreparation.Entities;

namespace NavisElectronics.TechPreparation.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;

    /// <summary>
    /// Основной интерфейс получения данных
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        /// Асинхронное получение заказа
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии объекта
        /// </param>
        /// <param name="token">
        /// Токен отмены
        /// </param>
        /// <returns>
        /// The <see cref="Task{IntermechTreeElement}"/>.
        /// </returns>
        Task<IntermechTreeElement> GetFullOrderAsync(long versionId, CancellationToken token);

        /// <summary>
        /// Асинхронное получение заказа
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии объекта
        /// </param>
        /// <returns>
        /// The <see cref="Task{IntermechTreeElement}"/>.
        /// </returns>
        Task<IntermechTreeElement> GetFullOrderAsync(long versionId);

        /// <summary>
        /// Получение заказа
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии объекта
        /// </param>
        /// <returns>
        /// The <see cref="IntermechTreeElement"/>.
        /// </returns>
        IntermechTreeElement GetFullOrder(long versionId);

        /// <summary>
        /// Получение состава изделия
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии объекта
        /// </param>
        /// <returns>
        /// The <see cref="ICollection{IntermechTreeElement}"/>.
        /// </returns>
        ICollection<IntermechTreeElement> Read(long versionId, string caption);


        /// <summary>
        /// Асинхронное получение всех организаций
        /// </summary>
        /// <returns>
        /// The <see cref="Task{Agent}"/>.
        /// </returns>
        Task<ICollection<Agent>> GetAllAgentsAsync();

        /// <summary>
        /// Получение всех организаций
        /// </summary>
        /// <returns>
        /// The <see cref="ICollection{Agent}"/>.
        /// </returns>
        ICollection<Agent> GetAgents();

        /// <summary>
        /// Получение содержимого атрибута Файл
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии заказа.
        /// </param>
        /// <param name="fileAttributeId">
        /// The file attribute id.
        /// </param>
        /// <param name="organization">
        /// Изготовитель
        /// </param>
        /// <returns>
        /// The <see cref="IList{IntermechTreeElement}"/>.
        /// </returns>
        IntermechTreeElement GetDataFromFile(long versionId, int fileAttributeId);

        /// <summary>
        /// Получение содержимого атрибута "Файл" асинхронно
        /// </summary>
        /// <param name="versionId">
        /// Идентификатор версии заказа
        /// </param>
        /// <param name="dataAttributeId">
        /// идентификатор атрибута файл
        /// </param>
        /// <param name="organization">
        /// Изготовитель
        /// </param>
        /// <returns>
        /// The <see cref="Task{IntermechTreeElement}"/>.
        /// </returns>
        Task<IntermechTreeElement> GetDataFromFileAsync(long versionId, int dataAttributeId);

        Task<T> GetDataFromBinaryAttributeAsync<T>(long versionId, int dataAttributeId) where T : class;

        T GetDataFromBinaryAttribute<T>(long versionId, int dataAttributeId) where T : class;

        /// <summary>
        /// Получение типов тех. отхода
        /// </summary>
        /// <returns>
        /// The <see cref="WithdrawalType"/>.
        /// </returns>
        WithdrawalType GetWithdrawalTypes();

        /// <summary>
        /// Асинхронное получение типов тех. отхода 
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<WithdrawalType> GetWithdrawalTypesAsync();

        /// <summary>
        /// Асинхронно получает цеха из IMBase
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TechRouteNode> GetWorkshopsAsync();

        /// <summary>
        /// Метод получает коллекцию документов на изделие, которые затем можно просмотреть
        /// </summary>
        /// <param name="id">Идентификатор изделия</param>
        /// <returns>Возвращает коллекцию прикрепленных документов</returns>
        ICollection<Document> GetDocuments(long id);


        /// <summary>
        /// Получает тех. маршрут у элемента
        /// </summary>
        /// <param name="element">
        /// собственно элемент, для которого идет поиск маршрута
        /// </param>
        /// <param name="dictionary">
        /// словарь со структурой предприятия
        /// </param>
        /// <param name="organizationStructName">
        /// наименование организации
        /// </param>
        /// <param name="productionType">
        /// тип производства
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<ICollection<TechRouteNode>> GetTechRouteAsync(IntermechTreeElement element,
            IDictionary<long, TechRouteNode> dictionary, string organizationStructName, string productionType);


        /// <summary>
        /// Получает тех. маршрут
        /// </summary>
        /// <param name="id">
        /// идентификатор версии сборочной единицы
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        //Task<ICollection<Production>> GetTechRouteAsync(IntermechTreeElement element,
        //    IDictionary<long, TechRouteNode> dictionary, string organizationStructName);

    }
}