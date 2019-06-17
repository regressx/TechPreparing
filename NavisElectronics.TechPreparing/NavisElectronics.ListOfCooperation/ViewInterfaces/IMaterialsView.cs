namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;

    using NavisElectronics.ListOfCooperation.Entities;
    using NavisElectronics.TechPreparation.EventArguments;

    public interface IMaterialsView
    {
        event EventHandler Load;
        event EventHandler SaveClick;

        event EventHandler<ExtractedObjectEventArgs> IntermechObjectClick;
        //event EventHandler<MaterialEventArgs> SetStockRateClick;
        void Show();

        void FillDataGrid(IList<ExtractedObject> materials);
        //ICollection<Material> GetMaterials();
        void RedrawRow(int index);
    }
}