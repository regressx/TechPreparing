using NavisElectronics.Orders.Enums;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders.EventArguments
{
    public class ProduceEventArgs
    {
        private readonly IntermechTreeElement _element;
        private readonly bool _produceSign;
        private readonly ProduceIn _whereToSet;

        public ProduceEventArgs(IntermechTreeElement element, bool produceSign, ProduceIn whereToSet)
        {
            _element = element;
            _produceSign = produceSign;
            _whereToSet = whereToSet;
        }

        public IntermechTreeElement Element
        {
            get { return _element; }
        }

        public bool ProduceSign
        {
            get { return _produceSign; }
        }

        public ProduceIn WhereToSet => _whereToSet;
    }
}