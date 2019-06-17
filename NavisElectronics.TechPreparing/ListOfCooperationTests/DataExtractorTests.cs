
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ListOfCooperationTests
{
    using NavisElectronics.TechPreparation.Services;

    [TestClass]
    public class DataExtractorTests
    {
        [TestMethod]
        public void GetStringTest()
        {
            string data = "<1:123;124;125/><2:666;777;888/>";

            TechAgentDataExtractor dataExtractor = new TechAgentDataExtractor();
            string testValue = dataExtractor.ExtractData(data, "1");


            string expectedValue = "123;124;125";


            Assert.AreEqual(expectedValue, testValue);
        }

        [TestMethod]
        public void RemoveTest()
        {

            string data = "<1:123;124;125/><2:666;777;888/>";

            TechAgentDataExtractor dataExtractor = new TechAgentDataExtractor();
            string testValue = dataExtractor.RemoveData(data, "1");


            string expectedValue = "<2:666;777;888/>";


            Assert.AreEqual(expectedValue, testValue);
        }

    }
}
