using System.Collections.Generic;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Entities
{
    public class WithdrawalTypeWrapper:IStructElement
    {

        private IList<IStructElement> _children;

        public WithdrawalTypeWrapper(WithdrawalType withdrawalType)
        {
            WithdrawalType = withdrawalType;
            _children = new List<IStructElement>();
        }

        public long Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public IList<IStructElement> Children
        {
            get
            {
                return _children;

            }
            set { _children = value; }

        }
        public void Add(IStructElement element)
        {
            _children.Add(element);
        }

        public WithdrawalTypeWrapper Wrap(WithdrawalType withdrawalType)
        {
            WithdrawalTypeWrapper wrapperRoot = new WithdrawalTypeWrapper(withdrawalType);
            wrapperRoot.Name = withdrawalType.Value;
            WrapRecursive(withdrawalType, wrapperRoot);

            return wrapperRoot;
        }

        private void WrapRecursive(WithdrawalType node, WithdrawalTypeWrapper wrapperNode)
        {
            foreach (WithdrawalType child in node.Children)
            {
                WithdrawalTypeWrapper wrap = new WithdrawalTypeWrapper(child);
                wrap.Name = child.Value;
                wrapperNode.Add(wrap);
                WrapRecursive(child, wrap);
            }
        }


        public WithdrawalType WithdrawalType { get; set; }

    }
}