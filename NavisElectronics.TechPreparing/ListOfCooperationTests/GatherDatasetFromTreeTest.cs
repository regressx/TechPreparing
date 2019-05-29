using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.Logic;
using NavisElectronics.ListOfCooperation.Services;

namespace ListOfCooperationTests
{
    [TestClass]
    public class GatherDatasetFromTreeTest
    {
        [TestMethod]
        public void GatherDataSetTest()
        {
            IntermechTreeElement mainElement = new IntermechTreeElement();
            mainElement.Id = 1;
            mainElement.Agent = string.Empty;
            mainElement.Amount = 1;
            mainElement.Name = "Заказ";
            mainElement.Note = "Примечание к заказу";
            mainElement.Type = 0;
            mainElement.CooperationFlag = false;

            IntermechTreeElement firstAssembly = new IntermechTreeElement();
            firstAssembly.Id = 2;
            firstAssembly.Agent = string.Empty;
            firstAssembly.Amount = 2;
            firstAssembly.Name = "Первая сборка";
            firstAssembly.Designation = "ХХХХ.000000.000";
            firstAssembly.Note = "Примечание к сборке";
            firstAssembly.Type = 1074;
            firstAssembly.CooperationFlag = false;


            IntermechTreeElement secondElement = new IntermechTreeElement();
            secondElement.Id = 3;
            secondElement.Agent = string.Empty;
            secondElement.Amount = 1;
            secondElement.Name = "Вторая сборка";
            secondElement.Designation = "ХХХХ.000000.001";
            secondElement.Note = "Примечание к сборке";
            secondElement.Type = 1074;
            secondElement.CooperationFlag = false;


            IntermechTreeElement thirdElement = new IntermechTreeElement();
            thirdElement.Id = 4;
            thirdElement.Agent = string.Empty;
            thirdElement.Amount = 1;
            thirdElement.Name = "Деталька";
            thirdElement.Designation = "ХХХХ.000000.002";
            thirdElement.Note = "Примечание к деткальке";
            thirdElement.Type = 1052;
            thirdElement.CooperationFlag = false;

            IntermechTreeElement forthElement = new IntermechTreeElement();
            forthElement.Id = 5;
            forthElement.Agent = string.Empty;
            forthElement.Amount = 2;
            forthElement.Name = "Первая деталь";
            forthElement.Designation = "ХХХХ.000000.003";
            forthElement.Note = "Примечание к детали";
            forthElement.Type = 1052;
            forthElement.CooperationFlag = false;

            mainElement.Add(firstAssembly);

            firstAssembly.Add(secondElement);
            firstAssembly.Add(forthElement);

            secondElement.Add(thirdElement);

            DataSetGatheringService service = new DataSetGatheringService();
            DataSet ds = service.GetDataSetInternal(mainElement);

            Assert.AreEqual(4, ds.Tables["Product"].Rows.Count);
        }
    }
}
