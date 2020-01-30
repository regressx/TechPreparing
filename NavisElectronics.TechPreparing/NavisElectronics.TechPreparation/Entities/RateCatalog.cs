using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.Interfaces.Entities
{
    public class RateCatalog
    {
        private IList<RateCatalogNode> _nodes;

        public RateCatalog()
        {
            _nodes = new List<RateCatalogNode>();
        }

        public void AddMaterial(RateCatalogNode material)
        {
            _nodes.Add(material);
        }


        public RateCatalogNode FindMaterial(string materialName)
        {
            throw new System.NotImplementedException();
        }
    }
}