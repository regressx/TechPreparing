namespace NavisElectronics.TechPreparation.Entities
{
    using System.Collections.Generic;
    using Interfaces;

    /// <summary>
    /// The withdrawal type adapter.
    /// </summary>
    public class WithdrawalTypeAdapter : IStructElement
    {
        private IList<IStructElement> _children;

        public WithdrawalTypeAdapter(WithdrawalType withdrawalType)
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

        public WithdrawalTypeAdapter Wrap(WithdrawalType withdrawalType)
        {
            WithdrawalTypeAdapter wrapperRoot = new WithdrawalTypeAdapter(withdrawalType);
            wrapperRoot.Name = withdrawalType.Name;
            WrapRecursive(withdrawalType, wrapperRoot);

            return wrapperRoot;
        }

        private void WrapRecursive(WithdrawalType node, WithdrawalTypeAdapter wrapperNode)
        {
            foreach (WithdrawalType child in node.Children)
            {
                WithdrawalTypeAdapter wrap = new WithdrawalTypeAdapter(child);
                wrap.Name = child.Name;
                wrapperNode.Add(wrap);
                WrapRecursive(child, wrap);
            }
        }


        public WithdrawalType WithdrawalType { get; set; }

    }
}