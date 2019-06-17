namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms.Design;

    using NavisElectronics.ListOfCooperation.Entities;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.IO;

    public class TempReader:IDataRepository
    {
        public Task<IntermechTreeElement> GetFullOrderAsync(long versionId, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public IntermechTreeElement GetFullOrder(long versionId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<IntermechTreeElement> Read(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ICollection<Agent>> GetAllAgentsAsync()
        {
            Func<ICollection<Agent>> func = () =>
                {
                    Agent agent1 = new Agent();
                    agent1.Id = 1372599;
                    agent1.Name = "КБ НАВИС";

                    Agent agent2 = new Agent();
                    agent2.Id = 1299782;
                    agent2.Name = "НАВИС-Электроника";
                    List<Agent> agents  = new List<Agent>(2);

                    agents.Add(agent1);
                    agents.Add(agent2);

                    return agents;
                };
            return Task<ICollection<Agent>>.Run(func);
        }

        public DataSet GetDataset(long orderId, int dataAttributeId)
        {
            DataSet ds = null;
            using (FileStream fs = new FileStream("dataset.blb", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                ds = (DataSet)bf.Deserialize(fs);
            }

            return ds;
        }

        public Task<DataSet> GetDatasetAsync(long orderId, int dataAttributeId)
        {
            Func<DataSet> func = () => { return GetDataset(orderId, dataAttributeId); };
            return Task.Run(func);
        }

        public WithdrawalType GetWithdrawalTypes()
        {
            throw new System.NotImplementedException();
        }

        public Task<WithdrawalType> GetWithdrawalTypesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<TechRouteNode> GetWorkshopsAsync()
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Document> GetDocuments(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}