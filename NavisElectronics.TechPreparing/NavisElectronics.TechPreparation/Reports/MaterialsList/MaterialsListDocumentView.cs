using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Reports.MaterialsList
{
    using Aga.Controls.Tree;

    public class MaterialsListDocumentView : IDocumentTypeFactory
    {
        private const long TemplateId = 1987754;

        private MaterialsListDocumentModel _model;

        public MaterialsListDocumentView(MaterialsListDocumentModel model)
        {
            _model = model;
        }

        public void Create(Node mainElement, string name)
        {
            IntermechTreeElement element = (IntermechTreeElement)mainElement.Tag;
            _model.GenerateFrom(element);
        }


    }
}