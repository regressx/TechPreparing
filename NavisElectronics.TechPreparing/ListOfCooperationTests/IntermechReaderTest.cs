using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.ListOfCooperation.Entities;

namespace ListOfCooperationTests
{
    [TestClass]
    public class IntermechReaderTest
    {
        [TestMethod]
        public void SplitAmountTest()
        {
            string value = "12 шт";

            string[] answer = value.Split(' ');
            Assert.AreEqual("шт", answer[1]);
            Assert.AreEqual("12", answer[0]);
        }

        [TestMethod]
        public void ExtractPcbVersionTest1()
        {
            IntermechReader reader = new IntermechReader();
            int x = reader.ExtractPcbVersion("TDCK.000000.000v1.PcbDoc");
            Assert.AreEqual(1, x);
        }

        [TestMethod]
        public void ExtractPcbVersionTest2()
        {
            IntermechReader reader = new IntermechReader();
            int x = reader.ExtractPcbVersion("TDCK.000000.000v87.PcbDoc");
            Assert.AreEqual(87, x);
        }

        [TestMethod]
        public void ExtractPcbVersionTest3()
        {
            IntermechReader reader = new IntermechReader();
            int x = reader.ExtractPcbVersion("TDCK.000000.000v333.PcbDoc");
            Assert.AreEqual(333, x);
        }

    }
}
