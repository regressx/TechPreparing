using System.Collections.ObjectModel;

namespace NavisElectronics.TechPreparation.Entities
{
    using System.Collections.Generic;
    using Interfaces;
    using Interfaces.Entities;

    /// <summary>
    /// 
    /// </summary>
    public class IntermechTreeElementWrapper : IStructElement
    {
        private IntermechTreeElement _root;
        private IList<IStructElement> _children;
        public IntermechTreeElementWrapper(IntermechTreeElement root)
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

        public IntermechTreeElementWrapper Wrap(IntermechTreeElement nodeToWrap)
        {
            IntermechTreeElementWrapper wrapperRoot = new IntermechTreeElementWrapper(nodeToWrap);
            wrapperRoot.Name = nodeToWrap.Name;
            WrapRecursive(nodeToWrap, wrapperRoot);

            return wrapperRoot;
        }

        private void WrapRecursive(IntermechTreeElement node, IntermechTreeElementWrapper wrapperNode)
        {
            foreach (IntermechTreeElement child in node.Children)
            {
                IntermechTreeElementWrapper wrap = new IntermechTreeElementWrapper(child);
                wrap.Name = string.Format("{0} {1}",child.Designation, child.Name).TrimEnd();
                wrapperNode.Add(wrap);
                WrapRecursive(child, wrap);
            }
        }

    }
}