using System;
using System.ComponentModel.Design.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace DataTests
{
    [TestClass]
    public class RecountTests
    {
        [TestMethod]
        public void RecountTest()
        {
            IntermechReader reader = new IntermechReader();

            IntermechTreeElement root = new IntermechTreeElement();
            root.Amount = 1;
            root.UseAmount = 1;
            root.AmountWithUse = root.Amount * root.UseAmount;

            IntermechTreeElement node1 = new IntermechTreeElement();
            node1.Amount = 2;
            root.Add(node1);

            IntermechTreeElement node5 = new IntermechTreeElement();
            node5.Amount = 10;
            node1.Add(node5);

            IntermechTreeElement node6 = new IntermechTreeElement();
            node6.Amount = 7;
            node5.Add(node6);


            IntermechTreeElement node2 = new IntermechTreeElement();
            node2.Amount = 3;
            root.Add(node2);


            IntermechTreeElement node3 = new IntermechTreeElement();
            node3.Amount = 3;
            node2.Add(node3);

            IntermechTreeElement node4 = new IntermechTreeElement();
            node4.Amount = 6;
            node3.Add(node4);

            reader.RecountAmountInTree(root);

            Assert.AreEqual("2,000",root[0].AmountWithUse.ToString("F3"));
            Assert.AreEqual("20,000",root[0].Children[0].AmountWithUse.ToString("F3"));
            Assert.AreEqual("140,000",root[0].Children[0].Children[0].AmountWithUse.ToString("F3"));

            Assert.AreEqual("3,000",root[1].AmountWithUse.ToString("F3"));
            Assert.AreEqual("9,000",root[1].Children[0].AmountWithUse.ToString("F3"));
            Assert.AreEqual("54,000",root[1].Children[0].Children[0].AmountWithUse.ToString("F3"));

        }
    }
}
