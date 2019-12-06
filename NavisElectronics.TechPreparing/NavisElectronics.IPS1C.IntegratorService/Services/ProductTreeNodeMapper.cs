namespace NavisElectronics.IPS1C.IntegratorService.Services
{
    using System;
    using System.Reflection;
    using Entities;
    using TechPreparation.Interfaces.Entities;

    /// <summary>
    /// Automapper нормально не ставится на этот фреймворк и даже на 4.6.1. Нужна новая версия студии, что бы переключиться на 4.7..
    /// Даже не рассчитывайте найти здесь крутую рефлексию или что-либо еще. Здесь тупое копирование одних полей в другие 
    /// </summary>
    public class ProductTreeNodeMapper
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
        public ProductTreeNode Map(IntermechTreeElement product)
        {
            ProductTreeNode root = CreateProductTreeNode(product);
            MapRecursive(root, product);
            return root;
        }

        /// <summary>
        /// The map recursive.
        /// </summary>
        /// <param name="root">
        /// The root.
        /// </param>
        /// <param name="databaseProduct">
        /// The database product.
        /// </param>
        private void MapRecursive(ProductTreeNode root, IntermechTreeElement databaseProduct)
        {
            if (databaseProduct.Children.Count > 0)
            {
                foreach (IntermechTreeElement product in databaseProduct.Children)
                {
                    if (product.RelationName == "Документ")
                    {
                        continue;
                    }

                    ProductTreeNode childTreeNode = CreateProductTreeNode(product);
                    root.Add(childTreeNode);
                    MapRecursive(childTreeNode, product);
                }
            }
        }

        /// <summary>
        /// Маппинг одного элемента дерева в другой. И здесь есть чуть-чуть рефлексии
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see cref="ProductTreeNode"/>.
        /// </returns>
        private ProductTreeNode CreateProductTreeNode(IntermechTreeElement product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product","На стороне сервиса была попытка подать пустой параметр в метод. Обратитесь к разработчику сервиса");
            }

            ProductTreeNode root = new ProductTreeNode();
            root.VersionId = product.Id.ToString();
            root.ObjectId = product.ObjectId.ToString();
            root.Type = product.Type.ToString();
            root.Designation = product.Designation;
            root.Name = product.Name;
            root.Amount = product.Amount.ToString("F6");
            root.PartNumber = product.PartNumber;
            root.CooperationFlag = product.CooperationFlag.ToString();
            root.SubstituteGroup = product.SubstituteGroupNumber.ToString();
            root.NumberInSubstituteGroup = product.SubstituteNumberInGroup.ToString();
            root.SubstituteInfo = product.SubstituteInfo;
            root.PositionInSpecification = product.Position;
            root.PositionDesignation = product.PositionDesignation;
            root.Note = string.Format("{0} {1}", product.Note, product.RouteNote).Trim();
            root.Supplier = product.Supplier;
            root.MeasureUnits = product.MeasureUnits;
            root.Class = product.Class;
            root.LastVersion = product.ChangeNumber;
            root.Agent = product.Agent;
            root.StockRate = product.StockRate.ToString("F6");
            root.SampleSize = product.SampleSize;
            root.IsPCB = product.IsPcb.ToString();
            root.PcbVersion = product.PcbVersion.ToString();
            root.TechTaskOnPCB = product.TechTask;
            root.Case = product.Case;
            root.MountingType = product.MountingType;
            root.ProduseSign = product.ProduseSign.ToString();
            root.TechRoute = product.TechRoute;
            root.RelationName = product.RelationName;
            root.ChangeDocumentName = product.ChangeDocument;
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