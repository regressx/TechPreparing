using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.Logic;
using NavisElectronics.ListOfCooperation.Services;

namespace ListOfCooperationTests
{
    [TestClass]
    public class GatherTreeFromDatasetTests
    {
        [TestMethod]
        public void GatherTreeTest()
        {

            DataSetGatheringService serv = new DataSetGatheringService();
            DataSet ds = serv.CreateDataset();


            DataRow orderRow = ds.Tables["Order"].NewRow();
            orderRow["Name"] = "Заказ 1";
            orderRow["BigNote"] = "Большое примечание";
            orderRow["OrderId"] = 1;
            orderRow["OrderObjectId"] = 2;
            ds.Tables["Order"].Rows.Add(orderRow);


            DataRow productRow1 = ds.Tables["Product"].NewRow();
            productRow1["Type"] = 1074;
            productRow1["Id"] = 2;
            productRow1["Name"] = "Сборка 1";
            productRow1["Designation"] = "ХХХХ.000000.001";

            DataRow productRow2 = ds.Tables["Product"].NewRow();
            productRow2["Type"] = 1052;
            productRow2["Id"] = 3;
            productRow2["Name"] = "Деталь 2";
            productRow2["Designation"] = "ХХХХ.000000.002";

            DataRow productRow3 = ds.Tables["Product"].NewRow();
            productRow3["Type"] = 1074;
            productRow3["Id"] = 4;
            productRow3["Name"] = "Сборка 3";
            productRow3["Designation"] = "ХХХХ.000000.003";


            DataRow productRow4 = ds.Tables["Product"].NewRow();
            productRow4["Type"] = 1052;
            productRow4["Id"] = 5;
            productRow4["Name"] = "Деталь 4";
            productRow4["Designation"] = "ХХХХ.000000.004";


            DataRow productRow5 = ds.Tables["Product"].NewRow();
            productRow5["Type"] = 1074;
            productRow5["Id"] = 6;
            productRow5["Name"] = "Сборка 5";
            productRow5["Designation"] = "ХХХХ.000000.005";

            ds.Tables["Product"].Rows.Add(productRow1);
            ds.Tables["Product"].Rows.Add(productRow2);
            ds.Tables["Product"].Rows.Add(productRow3);
            ds.Tables["Product"].Rows.Add(productRow4);
            ds.Tables["Product"].Rows.Add(productRow5);

            DataRow relationRow1 = ds.Tables["ProductRelations"].NewRow();
            relationRow1["ParentId"] = 1;
            relationRow1["ChildId"] = 2;
            relationRow1["Amount"] = 2;

            DataRow relationRow2 = ds.Tables["ProductRelations"].NewRow();
            relationRow2["ParentId"] = 1;
            relationRow2["ChildId"] = 3;
            relationRow2["Amount"] = 3;

            DataRow relationRow3 = ds.Tables["ProductRelations"].NewRow();
            relationRow3["ParentId"] = 2;
            relationRow3["ChildId"] = 4;
            relationRow3["Amount"] = 2;

            DataRow relationRow4 = ds.Tables["ProductRelations"].NewRow();
            relationRow4["ParentId"] = 4;
            relationRow4["ChildId"] = 5;
            relationRow4["Amount"] = 2;

            DataRow relationRow5 = ds.Tables["ProductRelations"].NewRow();
            relationRow5["ParentId"] = 4;
            relationRow5["ChildId"] = 6;
            relationRow5["Amount"] = 2;

            ds.Tables["ProductRelations"].Rows.Add(relationRow1);
            ds.Tables["ProductRelations"].Rows.Add(relationRow2);
            ds.Tables["ProductRelations"].Rows.Add(relationRow3);
            ds.Tables["ProductRelations"].Rows.Add(relationRow4);
            ds.Tables["ProductRelations"].Rows.Add(relationRow5);


            IntermechTreeElement treeElement = new IntermechTreeElement();
            treeElement.Id = (long)ds.Tables["Order"].Rows[0]["OrderId"];
            treeElement.Name = (string)ds.Tables["Order"].Rows[0]["Name"];
            treeElement.Note = (string)ds.Tables["Order"].Rows[0]["BigNote"];


            TreeBuilderService service = new TreeBuilderService();
            treeElement = service.Build(ds);

            Assert.AreEqual(treeElement.Id, 1);
            Assert.AreEqual(treeElement[0].Id, 2);
            Assert.AreEqual(treeElement[1].Id, 3);
            Assert.AreEqual(treeElement[0][0][0].Id, 5);
            Assert.AreEqual(treeElement[0][0][1].Id, 6);
        }
    }
}
