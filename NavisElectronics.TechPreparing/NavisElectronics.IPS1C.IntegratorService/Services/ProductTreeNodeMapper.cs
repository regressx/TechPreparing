using System;
using NavisElectronics.IPS1C.IntegratorService.Entities;

namespace NavisElectronics.IPS1C.IntegratorService.Services
{
    using NavisElectronics.TechPreparation.Entities;

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
            root.Id1 = product.Id.ToString();
            root.ObjectId1 = product.ObjectId.ToString();
            root.Amount1 = product.AmountAsString;
            root.Designation1 = product.Designation;
            root.Name1 = product.Name;
            root.SubstituteGroup1 = product.SubstituteGroupNumber.ToString();
            root.NumberInSubstituteGroup1 = product.SubstituteNumberInGroup.ToString();
            root.PositionDesignation1 = product.PositionDesignation;
            root.PositionInSpecification1 = product.PositionInSpecification;
            root.PartNumber1 = product.PartNumber;
            root.Supplier1 = product.Supplier;
            root.Class1 = product.Class;
            root.LastVersion1 = product.LastVersion;
            root.MeasureUnits1 = product.MeasureUnits;
            root.Case = product.Case;
            return root;
        }

        private ProductTreeNode CreateProductTreeNode(IntermechTreeElement product)
        {
            ProductTreeNode root = new ProductTreeNode();
            try
            {
                root.Id1 = product.Id.ToString();
                root.ObjectId1 = product.ObjectId.ToString();
                root.Type1 = product.Type.ToString();
                root.Designation1 = product.Designation;
                root.Name1 = product.Name;
                root.Amount1 = product.Amount.ToString("F6");
                root.PartNumber1 = product.PartNumber;
                root.CooperationFlag1 = product.CooperationFlag.ToString();
                root.SubstituteGroup1 = product.SubstituteGroupNumber.ToString();
                root.NumberInSubstituteGroup1 = product.SubstituteNumberInGroup.ToString();
                root.SubstituteInfo1 = product.SubstituteInfo;
                root.PositionInSpecification1 = product.Position;
                root.PositionDesignation1 = product.PositionDesignation;
                root.Note1 = string.Format("{0} {1}", product.Note, product.RouteNote).Trim();
                root.Supplier1 = product.Supplier;
                root.MeasureUnits1 = product.MeasureUnits;
                root.Class1 = product.Class;
                root.LastVersion1 = product.ChangeNumber;
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