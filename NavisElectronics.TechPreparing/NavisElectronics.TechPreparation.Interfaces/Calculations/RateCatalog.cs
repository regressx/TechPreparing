using NavisElectronics.TechPreparation.Interfaces.Collections;
using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.Interfaces.Entities
{
    public class RateCatalog
    {
        readonly SortedList<string, RateCatalogNode> _nodes;


        public RateCatalog()
        {
            _nodes = new SortedList<string, RateCatalogNode>();
        }

        public void AddMaterial(RateCatalogNode material)
        {
            _nodes.Add(material.Name, material);
        }

        public IEnumerable<RateCatalogNode> Materials
        {
            get
            {
                return _nodes.Values ;
            }
        }


        public RateCatalogNode FindMaterial(string material)
        {
            return _nodes[material];
        }



    }
}