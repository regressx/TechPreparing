using System.Collections;
using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.Interfaces.Entities
{
    public class RateCatalogNode
    {
        private SortedList<string, RateCatalogNode> _nodes;
        public RateCatalogNode()
        {
            _nodes = new SortedList<string, RateCatalogNode>();
        }

        public string Name { get; set; }

        public RateCatalogNode Parent { get; set; }

        public SortedList<string, RateCatalogNode> Nodes => _nodes;

        public void Add(RateCatalogNode nodeToAdd)
        {
            _nodes.Add(nodeToAdd.Name, nodeToAdd);
        }

    }
}