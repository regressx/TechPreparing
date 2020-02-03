using NavisElectronics.TechPreparation.Interfaces.Collections;
using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.Interfaces.Entities
{
    public class RateCatalog
    {
        SortedList<string, RateCatalogNode> _nodes;


        public RateCatalog()
        {
            _nodes = new SortedList<string, RateCatalogNode>();
        }

        public void AddMaterial(RateCatalogNode material)
        {
            _nodes.Add(material.Name, material);
        }


        public RateCatalogNode FindMaterial(string material)
        {
            return _nodes[material];
        }



    }
}