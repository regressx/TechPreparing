using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.IPS1C.IntegratorService;
using NavisElectronics.IPS1C.IntegratorService.Entities;

namespace ServiceTests
{
    [TestClass]
    public class VersionParsingTests
    {
        [TestMethod]
        public void LastThreeDigitsOfDesignationTest()
        {
            string innerString = "ХХХХ.000000.000-01";

            Service service = new Service();
            string result = service.GetLastNumbersOfDesignation(innerString);

            Assert.AreEqual("000", result);
        }

        [TestMethod]
        public void CheckVersionTest()
        {

            ProductTreeNode node = new ProductTreeNode();
            ProductTreeNode node1 = new ProductTreeNode();

            string des = "ХХХХ.000000.135";

            string innerString1 = "ХХХХ.000000.133V1.PcbDoc";
            string innerString2 = "ХХХХ.000000.135V2.PcbDoc";

            Service service = new Service();

            string last = service.GetLastNumbersOfDesignation(des);

            string pcbVersion = service.CheckVersion(new object[] {innerString1, innerString2},
                last, node, node1);

            Assert.AreEqual("2", pcbVersion);
        }


    }
}
