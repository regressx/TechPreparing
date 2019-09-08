using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListOfCooperationTests
{
    [TestClass]
    public class ClipboardTests
    {
        NavisElectronics.TechPreparation.Services.ClipboardManager _manager;

        public ClipboardTests()
        {
            _manager = new NavisElectronics.TechPreparation.Services.ClipboardManager();
        }

        [TestMethod]
        public void CopyPasteTest()
        {
            throw new NotImplementedException();
        }
    }
}
