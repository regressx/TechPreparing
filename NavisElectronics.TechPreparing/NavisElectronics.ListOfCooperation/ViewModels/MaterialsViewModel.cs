using System.Collections.Generic;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.Enums;
using NavisElectronics.ListOfCooperation.IO;
using NavisElectronics.ListOfCooperation.Services;

namespace NavisElectronics.ListOfCooperation.ViewModels
{
    public class MaterialsViewModel
    {

        public IList<ExtractedObject> GetObjects(IntermechTreeElement mainElement, IntermechObjectTypes type)
        {
            IntermechObjectExtractor extractor = new IntermechObjectExtractor();
            ICollection<ExtractedObject> answerCollection = null;
            answerCollection = extractor.ExctractObjects(mainElement, type);
            return new List<ExtractedObject>(answerCollection);
        }

        public void Save(IntermechTreeElement mainElement)
        {
            DataSetGatheringService gatheringService = new DataSetGatheringService();
            System.Data.DataSet ds = gatheringService.Gather(mainElement);
            IntermechWriter writer = new IntermechWriter();
            writer.WriteDataSet(mainElement.Id, ds);
        }
    }
}