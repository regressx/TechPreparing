using System.Collections.Generic;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Entities
{
    public class TechRouteNodeWrapper : IStructElement
    {
        private IList<IStructElement> _children;

        public TechRouteNodeWrapper(TechRouteNode techRouteNode)
        {
            TechRouteNode = techRouteNode;
            _children = new List<IStructElement>();
        }

        public TechRouteNodeWrapper Wrap(TechRouteNode nodeToWrap)
        {
            TechRouteNodeWrapper wrapperRoot = new TechRouteNodeWrapper(nodeToWrap);
            Name = nodeToWrap.Name;
            WrapRecursive(nodeToWrap, wrapperRoot);

            return wrapperRoot;
        }

        private void WrapRecursive(TechRouteNode node, TechRouteNodeWrapper wrapperNode)
        {
            foreach (TechRouteNode child in node.Children)
            {
                TechRouteNodeWrapper wrap = new TechRouteNodeWrapper(child);
                wrapperNode.Add(wrap);
                WrapRecursive(child, wrap);
            }
        }

        public TechRouteNode TechRouteNode
        { get; set; }

        public long Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }

        public IList<IStructElement> Children
        {
            get { return _children; }
            set { _children = value; }
        }


        public void Add(IStructElement node)
        {
            _children.Add(node);
        }
    }
}