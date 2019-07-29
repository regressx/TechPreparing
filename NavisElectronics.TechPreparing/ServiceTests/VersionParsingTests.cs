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
    }
}
