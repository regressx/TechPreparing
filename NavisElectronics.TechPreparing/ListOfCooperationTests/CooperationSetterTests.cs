using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.ListOfCooperation.Logic;
using NavisElectronics.ListOfCooperation.Services;

namespace ListOfCooperationTests
{
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
