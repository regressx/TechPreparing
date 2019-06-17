namespace NavisElectronics.TechPreparation.Reports
{
    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;

    public interface IDocumentTypeFactory
    {
        void Create(Node mainElement, string name, Agent currentManufacturer);
    }
}