namespace NavisElectronics.IPS1C.IntegratorService.Services
{
    using System;
    using System.Reflection;
    using Entities;
    using TechPreparation.Interfaces.Entities;

    /// <summary>
    /// Маппер для узла структуры предприятия
    /// </summary>
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
                    OrganizationNode childTreeNode = CreateOrganizationNode(node);
                    root.Add(childTreeNode);
                    MapRecursive(childTreeNode, node);
                }
            }
        }

        private OrganizationNode CreateOrganizationNode(TechRouteNode product)
        {
            OrganizationNode root = new OrganizationNode();
            root.Id = product.Id.ToString();
            root.Type = product.Type.ToString();
            root.Name = product.Name;
            root.PartitionId = product.PartitionId.ToString();
            root.PartitionName = product.PartitionName;
            root.WorkshopId = product.WorkshopId.ToString();
            root.WorkshopName = product.WorkshopName;

            // для всех остальных свойств, которые строковые и с нулевым указателем проставить значение string.Empty
            Type type = typeof(ProductTreeNode);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string) && property.GetValue(root) == null)
                {
                    property.SetValue(root, string.Empty);
                }
            }

            return root;
        }
    }
}