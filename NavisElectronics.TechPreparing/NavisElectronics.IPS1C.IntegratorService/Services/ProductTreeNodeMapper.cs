using System;
using NavisElectronics.IPS1C.IntegratorService.Entities;
using NavisElectronics.TechPreparation.Entities;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.IPS1C.IntegratorService.Services
{
    public class ProductTreeNodeMapper
    {
        public ProductTreeNode Map(DataBaseProduct product)
        {
            ProductTreeNode root = CreateProductTreeNode(product);
            MapRecursive(root, product);
            return root;
        }

        public ProductTreeNode Map(IntermechTreeElement product)
        {
            ProductTreeNode root = CreateProductTreeNode(product);
            MapRecursive(root, product);
            return root;
        }


        private void MapRecursive(ProductTreeNode root, IntermechTreeElement databaseProduct)
        {
            if (databaseProduct.Children.Count > 0)
            {
                foreach (IntermechTreeElement product in databaseProduct.Children)
                {
                    ProductTreeNode childTreeNode = CreateProductTreeNode(product);
                    root.Add(childTreeNode);
                    MapRecursive(childTreeNode, product);
                }
            }
        }


        private void MapRecursive(ProductTreeNode root, DataBaseProduct databaseProduct)
        {
            if (databaseProduct.Products.Count > 0)
            {
                foreach (DataBaseProduct product in databaseProduct.Products)
                {
                    ProductTreeNode childTreeNode = CreateProductTreeNode(product);
                    root.Add(childTreeNode);
                    MapRecursive(childTreeNode, product);
                }
            }
        }

        private ProductTreeNode CreateProductTreeNode(DataBaseProduct product)
        {
            ProductTreeNode root = new ProductTreeNode();
            root.VersionId = product.Id.ToString();
            root.ObjectId = product.ObjectId.ToString();
            root.Amount = product.AmountAsString;
            root.Designation = product.Designation;
            root.Name = product.Name;
            root.SubstituteGroup = product.SubstituteGroupNumber.ToString();
            root.NumberInSubstituteGroup = product.SubstituteNumberInGroup.ToString();
            root.PositionDesignation = product.PositionDesignation;
            root.PositionInSpecification = product.PositionInSpecification;
            root.PartNumber = product.PartNumber;
            root.Supplier = product.Supplier;
            root.Class = product.Class;
            root.LastVersion = product.LastVersion;
            root.MeasureUnits = product.MeasureUnits;
            root.Case = product.Case;
            return root;
        }

        private ProductTreeNode CreateProductTreeNode(IntermechTreeElement product)
        {
            ProductTreeNode root = new ProductTreeNode();
            try
            {
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
                root.IsComplectNodeComponent = product.IsToComplect.ToString();
                root.IsPCB = product.IsPCB.ToString();
                root.PcbVersion = product.PcbVersion.ToString();
                root.TechTaskOnPCB = product.TechTask;
                root.TypeOfWithDrawal = product.TypeOfWithDrawal;
                root.Case = product.Case;
            }
            catch (NullReferenceException ex)
            {
                throw new Exception("Где-то нулевая ссылка. Какая-то ошибка с " + product.Designation + " " + product.Name, ex);
            }

            return root;
        }

    }
}