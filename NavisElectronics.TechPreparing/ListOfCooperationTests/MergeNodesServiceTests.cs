namespace ListOfCooperationTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NavisElectronics.TechPreparation.Enums;
    using NavisElectronics.TechPreparation.Interfaces;
    using NavisElectronics.TechPreparation.Interfaces.Entities;

    [TestClass]
    public class MergeNodesServiceTests
    {
        [TestMethod]
        public void AddDeepNewNodes()
        {
            MergeNodesService myService = new MergeNodesService();

            IntermechTreeElement oldTree = new IntermechTreeElement();
            oldTree.Name = "oldTree";
            oldTree.ObjectId = 1;

            IntermechTreeElement newTree = new IntermechTreeElement();
            newTree.Name = "newTree";
            newTree.ObjectId = 1;
            newTree.NodeState = NodeStates.Default;

            IntermechTreeElement newElement1 = new IntermechTreeElement();
            newElement1.Name = "newElement1";
            newElement1.NodeState = NodeStates.Added;
            newTree.Add(newElement1);

            IntermechTreeElement newElement2 = new IntermechTreeElement();
            newElement2.NodeState = NodeStates.Added;
            newElement2.Name = "newElement2";
            newElement1.Add(newElement2);

            IntermechTreeElement newElement3 = new IntermechTreeElement();
            newElement3.NodeState = NodeStates.Added;
            newElement3.Name = "newElement3";
            newElement1.Add(newElement3);

            myService.Merge(oldTree, newTree, newElement2);

            Assert.AreEqual(oldTree[0].Name,newElement1.Name);
            Assert.AreEqual(oldTree[0].Children.Count,1);
            Assert.AreEqual(oldTree[0].Children[0].Name, newElement2.Name);
        }


        [TestMethod]
        public void AddDeepNewNodesWithModifiedParentAndNewElement3()
        {
            MergeNodesService myService = new MergeNodesService();

            IntermechTreeElement oldTree = new IntermechTreeElement();
            oldTree.Name = "oldTree";
            oldTree.ObjectId = 1;

            IntermechTreeElement newTree = new IntermechTreeElement();
            newTree.Name = "newTree";
            newTree.ObjectId = 1;
            newTree.NodeState = NodeStates.Modified;

            IntermechTreeElement newElement1 = new IntermechTreeElement();
            newElement1.Name = "newElement1";
            newElement1.NodeState = NodeStates.Added;
            newTree.Add(newElement1);

            IntermechTreeElement newElement2 = new IntermechTreeElement();
            newElement2.NodeState = NodeStates.Added;
            newElement2.Name = "newElement2";
            newElement1.Add(newElement2);

            IntermechTreeElement newElement3 = new IntermechTreeElement();
            newElement3.NodeState = NodeStates.Added;
            newElement3.Name = "newElement3";
            newElement1.Add(newElement3);

            myService.Merge(oldTree, newTree, newElement3);

            Assert.AreEqual(oldTree[0].Name,newElement1.Name);
            Assert.AreEqual(oldTree[0].Children.Count, 1);
            Assert.AreEqual(oldTree[0].Children[0].Name, newElement3.Name);
        }

        [TestMethod]
        public void AddModifiedWithNewNodes()
        {
            MergeNodesService myService = new MergeNodesService();

            IntermechTreeElement oldTree = new IntermechTreeElement();
            oldTree.Name = "oldTree";
            oldTree.ObjectId = 1;

            IntermechTreeElement newTree = new IntermechTreeElement();
            newTree.Name = "newTree";
            newTree.ObjectId = 1;
            newTree.NodeState = NodeStates.Modified;

            IntermechTreeElement newElement1 = new IntermechTreeElement();
            newElement1.Name = "newElement1";
            newElement1.NodeState = NodeStates.Added;
            newTree.Add(newElement1);

            IntermechTreeElement newElement2 = new IntermechTreeElement();
            newElement2.NodeState = NodeStates.Added;
            newElement2.Name = "newElement2";
            newElement1.Add(newElement2);

            myService.Merge(oldTree, newTree, newElement2);
            Assert.AreEqual(oldTree.Children.Count, 1);
            Assert.AreEqual(oldTree[0].NodeState, NodeStates.Added);
        }

        [TestMethod]
        public void UpdateModifiedNode()
        {
            MergeNodesService myService = new MergeNodesService();

            IntermechTreeElement oldTree = new IntermechTreeElement();
            oldTree.Name = "oldTree";
            oldTree.Id = 1001;
            oldTree.ObjectId = 1;
            oldTree.NodeState = NodeStates.Default;

            IntermechTreeElement elementInOldTree = new IntermechTreeElement();
            elementInOldTree.Name = "newElement1";
            elementInOldTree.Id = 1002;
            elementInOldTree.ObjectId = 1003;
            elementInOldTree.NodeState = NodeStates.Default;
            oldTree.Add(elementInOldTree);


            IntermechTreeElement newTree = new IntermechTreeElement();
            newTree.Name = "newTree";
            newTree.Id = 1004;
            newTree.ObjectId = 1;
            newTree.NodeState = NodeStates.Modified;

            IntermechTreeElement newElement1 = new IntermechTreeElement();
            newElement1.Name = "newElement1";
            newElement1.Id = 1006;
            newElement1.ObjectId = 1003;
            newElement1.NodeState = NodeStates.Modified;
            newTree.Add(newElement1);

            myService.Merge(oldTree, newTree, newTree);
            myService.Merge(oldTree, newTree, newElement1);
            Assert.AreEqual(newTree.Id, oldTree.Id);
            Assert.AreEqual(newElement1.Id, elementInOldTree.Id);
        }
    }
}
