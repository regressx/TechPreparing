using System.Collections.Generic;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Entities
{
    /// <summary>
    /// The tech route node adapter.
    /// </summary>
    public class TechRouteNodeAdapter : IStructElement
    {
        private IList<IStructElement> _children;

        public TechRouteNodeAdapter(TechRouteNode techRouteNode)
        {
            TechRouteNode = techRouteNode;
            _children = new List<IStructElement>();
        }

        public TechRouteNodeAdapter Wrap(TechRouteNode nodeToWrap)
        {
            TechRouteNodeAdapter wrapperRoot = new TechRouteNodeAdapter(nodeToWrap);
            wrapperRoot.Name = nodeToWrap.Name;
            WrapRecursive(nodeToWrap, wrapperRoot);

            return wrapperRoot;
        }

        private void WrapRecursive(TechRouteNode node, TechRouteNodeAdapter wrapperNode)
        {
            foreach (TechRouteNode child in node.Children)
            {
                TechRouteNodeAdapter wrap = new TechRouteNodeAdapter(child);
                wrap.Name = child.Name;
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