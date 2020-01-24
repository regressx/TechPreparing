using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.IPS1C.IntegratorService.Entities;
using NavisElectronics.IPS1C.IntegratorService.Services;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace ServiceTests
{
    [TestClass]
    public class MappingTests
    {
        [TestMethod]
        //проверка, что ChangeNumber не перетирается
        public void MapIntermechTreeElementToProductTreeNode()
        {
            IntermechTreeElement elementToMap = new IntermechTreeElement()
            {
                Id = 0,
                ObjectId = 0,
                Type = 0,
                Name = null,
                Designation = null,
                ChangeNumber = "0",
                CooperationFlag = false,
                InnerCooperation = false,
                ContainsInnerCooperation = false,
                Amount = 0f,
                AmountWithUse = 0f,
                TotalAmount = 0f,
                StockRate = 0f,
                SampleSize = null,
                TechProcessReference = null,
                Note = null,
                SubstituteGroupNumber = 0,
                SubstituteNumberInGroup = 0,
                SubstituteInfo = null,
                Position =null,
                RelationId = 0,
                RouteNote = null,
                PositionDesignation = null,
                IsPcb = false,
                PcbVersion =0,
                TechTask = null,
                IsToComplect = false,
                Parent = null,
                Agent = null,
                TechRoute  = null,
                MeasureUnits = null,
                Class = null,
                Supplier = null,
                PartNumber = null,
                Case = null,
                ProduseSign = false,
                MountingType = null
            };

            ProductTreeNodeMapper mapper = new ProductTreeNodeMapper();
            ProductTreeNode node = mapper.Map(elementToMap);
            Assert.AreEqual("0", node.LastVersion);
        }

        [TestMethod]
        //проверка, что строковые null поля перетираются пустой строкой
        public void MapIntermechTreeElementToProductTreeNodeWithNullValues()
        {
            IntermechTreeElement elementToMap = new IntermechTreeElement()
            {
                Id = 0,
                ObjectId = 0,
                Type = 0,
                Name = null,
                Designation = null,
                ChangeNumber = "0",
                CooperationFlag = false,
                InnerCooperation = false,
                ContainsInnerCooperation = false,
                Amount = 0f,
                AmountWithUse = 0f,
                TotalAmount = 0f,
                StockRate = 0f,
                SampleSize = null,
                TechProcessReference = null,
                Note = null,
                SubstituteGroupNumber = 0,
                SubstituteNumberInGroup = 0,
                SubstituteInfo = null,
                Position =null,
                RelationId = 0,
                RouteNote = null,
                PositionDesignation = null,
                IsPcb = false,
                PcbVersion =0,
                TechTask = null,
                IsToComplect = false,
                Parent = null,
                Agent = null,
                TechRoute  = null,
                MeasureUnits = null,
                Class = null,
                Supplier = null,
                PartNumber = null,
                Case= null,
                ProduseSign = false,
                MountingType = null
            };

            ProductTreeNodeMapper mapper = new ProductTreeNodeMapper();
            ProductTreeNode node = mapper.Map(elementToMap);
            Assert.AreEqual(String.Empty, node.Supplier);
        }



    }
}
