using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Intermech.Interfaces;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.Enums;

namespace NavisElectronics.ListOfCooperation.IO
{
    public interface IDataRepository
    {
        Task<IntermechTreeElement> GetFullOrderAsync(long versionId, CancellationToken token);
        IntermechTreeElement GetFullOrder(long versionId);
        ICollection<IntermechTreeElement> Read(long id);
        Task<ICollection<Agent>> GetAllAgentsAsync();
        DataSet GetDataset(long orderId, int dataAttributeId);
        Task<DataSet> GetDatasetAsync(long orderId, int dataAttributeId);
        WithdrawalType GetWithdrawalTypes();
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
    }
}