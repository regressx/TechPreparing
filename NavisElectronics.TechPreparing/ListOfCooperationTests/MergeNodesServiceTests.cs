using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace ListOfCooperationTests
{
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Services;

    [TestClass]
    public class MergeNodesServiceTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            MergeNodesService myService = new MergeNodesService();


            IntermechTreeElement oldTree = new IntermechTreeElement();
            IntermechTreeElement oldElement1 = new IntermechTreeElement();
            IntermechTreeElement oldElement2 = new IntermechTreeElement();
            IntermechTreeElement oldElement3 = new IntermechTreeElement();
            IntermechTreeElement oldElement4 = new IntermechTreeElement();


            IntermechTreeElement newTree = new IntermechTreeElement();
            IntermechTreeElement newElement1 = new IntermechTreeElement();
            IntermechTreeElement newElement2 = new IntermechTreeElement();
            IntermechTreeElement newElement4 = new IntermechTreeElement();
            IntermechTreeElement newElement5 = new IntermechTreeElement();
            IntermechTreeElement newElement6 = new IntermechTreeElement();
        }
    }
}
