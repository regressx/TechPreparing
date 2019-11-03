using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.IPS1C.IntegratorService;

namespace ServiceTests
{
    [TestClass]
    public class TechDisposalTests
    {
        [TestMethod]
        public void TestAboveOrEqual1000()
        {
            Service service = new Service();
            string str = service.GetTechDisposalInternal("0402", 1000d);
            Assert.AreEqual("1100,000000", str);
        }

        [TestMethod]
        public void TestUnder1000()
        {
            Service service = new Service();
            string str = service.GetTechDisposalInternal("0402", 999d);
            Assert.AreEqual("1099,000000", str);
        }


        [TestMethod]
        public void Test1206AboveOrEqual1000()
        {
            Service service = new Service();
            string str = service.GetTechDisposalInternal("1206", 1001d);
            Assert.AreEqual("1051,050000", str);
        }

        [TestMethod]
        public void Test1206Under1000()
        {
            Service service = new Service();
            string str = service.GetTechDisposalInternal("1206", 999d);
            Assert.AreEqual("1049,000000", str);
        }


    }
}
