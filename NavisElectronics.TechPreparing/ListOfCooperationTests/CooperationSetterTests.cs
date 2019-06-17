using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListOfCooperationTests
{
    using NavisElectronics.TechPreparation.Services;

    [TestClass]
    public class CooperationSetterTests
    {
        [TestMethod]
        public void CutStringTest()
        {
            string str = "NNNN.000000.000";

            CooperationSetter setter = new CooperationSetter();
            string temp = setter.CutTheDesignation(str);


            Assert.AreEqual("000000.000", temp);
        }
    }
}
