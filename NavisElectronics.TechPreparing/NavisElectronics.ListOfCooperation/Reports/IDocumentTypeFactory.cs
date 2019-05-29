using Aga.Controls.Tree;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.ViewModels;

namespace NavisElectronics.ListOfCooperation.Reports
{
    public interface IDocumentTypeFactory
    {
        void Create(Node mainElement, string name, Agent currentManufacturer);
    }
}