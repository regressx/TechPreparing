namespace NavisElectronics.ListOfCooperation.Services
{
    using Entities;
    using Enums;
    using IO;

    public class SaveTreeService
    {
        private readonly IntermechTreeElement _rootElement;

        public SaveTreeService(IntermechTreeElement rootElement)
        {
            _rootElement = rootElement;
        }

        public void Save()
        {
            DataSetGatheringService gatheringService = new DataSetGatheringService();
            System.Data.DataSet ds = gatheringService.Gather(_rootElement);
            IntermechWriter writer = new IntermechWriter();
            writer.WriteDataSet(_rootElement.Id, ds);
        }
    }
}