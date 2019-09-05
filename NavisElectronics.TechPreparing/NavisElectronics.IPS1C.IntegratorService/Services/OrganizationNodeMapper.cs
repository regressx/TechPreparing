using NavisElectronics.IPS1C.IntegratorService.Entities;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.IPS1C.IntegratorService.Services
{
    public class OrganizationNodeMapper
    {

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// </returns>
        public OrganizationNode Map(TechRouteNode product)
        {
            OrganizationNode root = CreateOrganizationNode(product);
            MapRecursive(root, product);
            return root;
        }

        private void MapRecursive(OrganizationNode root, TechRouteNode product)
        {
            if (product.Children.Count > 0)
            {
                foreach (TechRouteNode node in product.Children)
                {
                    OrganizationNode childTreeNode = CreateOrganizationNode(product);
                    root.Add(childTreeNode);
                    MapRecursive(childTreeNode, product);
                }
            }
        }

        private OrganizationNode CreateOrganizationNode(TechRouteNode product)
        {
            throw new System.NotImplementedException();
        }
    }
}