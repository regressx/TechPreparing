using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Services;

namespace ListOfCooperationTests
{
    [TestClass]
    public class TechRouteStringBuilderTests
    {
        [TestMethod]
        public void AcceptedTest()
        {
            TechRouteStringBuilder sb = new TechRouteStringBuilder();

            TechRouteNode root = new TechRouteNode();
            TechRouteNode node1 = new TechRouteNode();
            node1.Id = 1;
            node1.Name = "Лучший";
            node1.PartitionName = "Лучший";
            node1.WorkshopName = "Лучший";
            TechRouteNode node2 = new TechRouteNode();
            node2.Id = 2;
            node2.Name = "участок";
            node2.PartitionName = "участок";
            node2.WorkshopName = "участок";
            TechRouteNode node3 = new TechRouteNode();
            node3.Id = 3;
            node3.Name = "в мире";
            node3.PartitionName = "в мире";
            node3.WorkshopName = "в мире";
            root.Add(node1);
            root.Add(node2);
            root.Add(node3);
            string str = sb.Build("1;2;3||1", root);

            Assert.AreEqual("Лучший-участок-в мире/Лучший",str);

        }


        [TestMethod]
        public void ErrorTest()
        {
            TechRouteStringBuilder sb = new TechRouteStringBuilder();

            TechRouteNode root = new TechRouteNode();
            TechRouteNode node1 = new TechRouteNode();
            node1.Id = 1;
            node1.Name = "Лучший";
            node1.PartitionName = "Лучший";
            node1.WorkshopName = "Лучший";
            TechRouteNode node2 = new TechRouteNode();
            node2.Id = 2;
            node2.Name = "участок";
            node2.PartitionName = "участок";
            node2.WorkshopName = "участок";
            TechRouteNode node3 = new TechRouteNode();
            node3.Id = 3;
            node3.Name = "в мире";
            node3.PartitionName = "в мире";
            root.Add(node1);
            root.Add(node2);
            root.Add(node3);
            string str = sb.Build("1;2;3", root);

            Assert.AreEqual("Лучший-участок-Ошибка",str);

        }

    }
}
