using System;
using System.Collections.Generic;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.EventArguments;

namespace NavisElectronics.ListOfCooperation.ViewInterfaces
{
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