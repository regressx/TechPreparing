namespace NavisElectronics.TechPreparation.Entities
{
    using System.Collections.Generic;
    using Interfaces;
    using Interfaces.Entities;

    /// <summary>
    /// The intermech tree element adapter.
    /// </summary>
    public class IntermechTreeElementAdapter : IStructElement
    {
        private IntermechTreeElement _root;
        private IList<IStructElement> _children;
        public IntermechTreeElementAdapter(IntermechTreeElement root)
        {
            _root = root;
            _children = new List<IStructElement>();
        }

        public long Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }

        public IList<IStructElement> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public IntermechTreeElement Root => _root;

        public void Add(IStructElement element)
        {
            _children.Add(element);
        }

        public IntermechTreeElementAdapter Wrap(IntermechTreeElement nodeToWrap)
        {
            IntermechTreeElementAdapter wrapperRoot = new IntermechTreeElementAdapter(nodeToWrap);
            wrapperRoot.Name = nodeToWrap.Name;
            WrapRecursive(nodeToWrap, wrapperRoot);

            return wrapperRoot;
        }

        private void WrapRecursive(IntermechTreeElement node, IntermechTreeElementAdapter wrapperNode)
        {
            foreach (IntermechTreeElement child in node.Children)
            {
                IntermechTreeElementAdapter wrap = new IntermechTreeElementAdapter(child);
                wrap.Name = string.Format("{0} {1}",child.Designation, child.Name).TrimEnd();
                wrapperNode.Add(wrap);
                WrapRecursive(child, wrap);
            }
        }

    }
}