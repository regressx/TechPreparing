using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Threading.Tasks;
    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// The temp reader.
    /// </summary>
    public class TempReader : IDataRepository
    {
        public Task<IntermechTreeElement> GetFullOrderAsync(long versionId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IntermechTreeElement> GetFullOrderAsync(long versionId)
        {
            throw new NotImplementedException();
        }

        public IntermechTreeElement GetFullOrder(long versionId)
        {
            throw new NotImplementedException();
        }

        public ICollection<IntermechTreeElement> Read(long versionId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Agent>> GetAllAgentsAsync()
        {
            throw new NotImplementedException();
        }

        public ICollection<Agent> GetAgents()
        {
            throw new NotImplementedException();
        }

        public IntermechTreeElement GetDataFromFile(long versionId, int fileAttributeId)
        {
            throw new NotImplementedException();
        }

        public Task<IntermechTreeElement> GetDataFromFileAsync(long versionId, int dataAttributeId)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetDataFromBinaryAttributeAsync<T>(long versionId, int dataAttributeId) where T : class
        {
            throw new NotImplementedException();
        }

        public T GetDataFromBinaryAttribute<T>(long versionId, int dataAttributeId) where T : class
        {
            throw new NotImplementedException();
        }

        public IntermechTreeElement GetDataFromFile(long versionId, int fileAttributeId, long organization)
        {
            throw new NotImplementedException();
        }

        public Task<IntermechTreeElement> GetDataFromFileAsync(long versionId, int dataAttributeId, long organization)
        {
            throw new NotImplementedException();
        }

        public WithdrawalType GetWithdrawalTypes()
        {
            throw new NotImplementedException();
        }

        public Task GetWithdrawalTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetWorkshopsAsync()
        {
            throw new NotImplementedException();
        }

        public ICollection<Document> GetDocuments(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IntermechTreeElement> GetElementDataAsync(long versionId)
        {
            throw new NotImplementedException();
        }

        Task<WithdrawalType> IDataRepository.GetWithdrawalTypesAsync()
        {
            throw new NotImplementedException();
        }

        Task<TechRouteNode> IDataRepository.GetWorkshopsAsync()
        {
            throw new NotImplementedException();
        }
    }
}