﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaterialsViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the MaterialsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.ViewModels
{
    using System.Collections.Generic;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;
    using NavisElectronics.TechPreparation.IO;
    using NavisElectronics.TechPreparation.Services;

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