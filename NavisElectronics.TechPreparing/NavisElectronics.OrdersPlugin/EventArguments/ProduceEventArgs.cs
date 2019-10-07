using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders.EventArguments
{
    public class ProduceEventArgs
    {
        private readonly IntermechTreeElement _element;
        private readonly bool _produceSign;

        public ProduceEventArgs(IntermechTreeElement element, bool produceSign)
        {
            _element = element;
            _produceSign = produceSign;
        }

        public IntermechTreeElement Element
        {
            get { return _element; }
        }

        public bool ProduceSign
        {
            get { return _produceSign; }
        }
    }
}