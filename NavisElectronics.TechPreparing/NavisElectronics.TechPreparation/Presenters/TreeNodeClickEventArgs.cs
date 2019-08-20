using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Presenters
{
    public class TreeNodeClickEventArgs
    {
        private IntermechTreeElement _element;

        public TreeNodeClickEventArgs(IntermechTreeElement element)
        {
            _element = element;
        }

        public IntermechTreeElement Element
        {
            get { return _element; }
        }
    }
}