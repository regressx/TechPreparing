using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.Exceptions;

namespace ListOfCooperationTests
{
    [TestClass]
    public class FindNodeTest
    {
        [TestMethod]
        public void GetFullPathTest()
        {
            
            IntermechTreeElement element1 = new IntermechTreeElement();
            element1.Id = -1380657;
            element1.Designation = "-1380657";

            IntermechTreeElement element2 = new IntermechTreeElement();
            element2.Id = 1207905;
            element2.Designation = "1207905";

            IntermechTreeElement element3 = new IntermechTreeElement();
            element3.Id = 1204409;
            element3.Designation = "1204409";

            element1.Add(element2);
            element2.Add(element3);

            IntermechTreeElement element4 = new IntermechTreeElement();
            element4.Id = 444;
            element4.Designation = "Test Element";
            element1.Add(element4);

            string path = element3.GetFullPathByVersionId();

            Assert.AreEqual("-1380657\\1207905\\1204409", path);


        }

        [TestMethod]
        public void Find()
        {
            
            IntermechTreeElement element1 = new IntermechTreeElement();
            element1.Id = -1380657;
            element1.Designation = "1";
            IntermechTreeElement element2 = new IntermechTreeElement();
            element2.Id = 222;
            element2.Designation = "2";
            IntermechTreeElement element3 = new IntermechTreeElement();
            element3.Id = 333;
            element3.Designation = "3";
            element1.Add(element2);
            element2.Add(element3);

            IntermechTreeElement element4 = new IntermechTreeElement();
            element4.Id = 1207905;
            element4.Designation = "Test Element";
            element1.Add(element4);

            IntermechTreeElement element5 = new IntermechTreeElement();
            element5.Id = 1204409;
            element5.Designation = "ElementToFind";
            element4.Add(element5);


            string path = "-1380657\\1207905\\1204409";

            IntermechTreeElement element = element1.FindByVersionIdPath(path);

            Assert.AreEqual(1204409, element.Id);
        }

        [ExpectedException(typeof(TreeNodeNotFoundException))]
        [TestMethod]
        public void FindWithException()
        {
            
            IntermechTreeElement element1 = new IntermechTreeElement();
            element1.Id = 111;
            element1.Designation = "1";
            IntermechTreeElement element2 = new IntermechTreeElement();
            element2.Id = 222;
            element2.Designation = "2";
            IntermechTreeElement element3 = new IntermechTreeElement();
            element3.Id = 333;
            element3.Designation = "3";
            element1.Add(element2);
            element2.Add(element3);

            IntermechTreeElement element4 = new IntermechTreeElement();
            element4.Id = 444;
            element4.Designation = "Test Element";
            element1.Add(element4);

            IntermechTreeElement element5 = new IntermechTreeElement();
            element5.Id = 555;
            element5.Designation = "ElementToFind";
            element4.Add(element5);


            string path = "111\\444\\555";


            IntermechTreeElement element = element2.FindByVersionIdPath(path);

            Assert.AreEqual(555, element.Id);
        }


    }
}
