using System;
using System.Data;
using Intermech.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NavisElectronics.IPS1C.IntegratorService;

namespace ServiceTests
{
    [TestClass]
    public class LifeCycleTests
    {
        [TestMethod]
        public void GetVersionsOnManufacturing()
        {
            Service service = new Service();

            DataTable table = new DataTable();
            DataColumn versionIdColumn = new DataColumn("F_OBJECT_ID", typeof(long));
            DataColumn stepColumn = new DataColumn("F_LC_STEP", typeof(int));
            DataColumn dateColumn = new DataColumn("F_START_DATE", typeof(DateTime));
            DataColumn versionColumn = new DataColumn("F_VERSION_ID", typeof(int));
            table.Columns.AddRange(new DataColumn[] {versionIdColumn,stepColumn,dateColumn, versionColumn});

            // версия на производстве
            DataRow row1 = table.NewRow();
            row1["F_OBJECT_ID"] = 1890;
            row1["F_LC_STEP"] = 1015;
            row1["F_START_DATE"] = new DateTime(2019, 02, 13, 10,10, 10);
            row1["F_VERSION_ID"] = 0;

            // версия на хранении
            DataRow row2 = table.NewRow();
            row2["F_OBJECT_ID"] = 1890;
            row2["F_LC_STEP"] = 1018;
            row2["F_START_DATE"] = new DateTime(2019, 02, 13, 10, 11, 12);
            row2["F_VERSION_ID"] = 0;
            
            // вторая версия на производстве
            DataRow row3 = table.NewRow();
            row3["F_OBJECT_ID"] = 1891;
            row3["F_LC_STEP"] = 1015;
            row3["F_START_DATE"] = new DateTime(2019, 02, 13, 10, 11, 12);
            row3["F_VERSION_ID"] = 1;

            // вторая версия на хранении
            DataRow row4 = table.NewRow();
            row4["F_OBJECT_ID"] = 1891;
            row4["F_LC_STEP"] = 1018;
            row4["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 13, 15);
            row4["F_VERSION_ID"] = 1;

            // третья версия на производстве
            DataRow row5 = table.NewRow();
            row5["F_OBJECT_ID"] = 1892;
            row5["F_LC_STEP"] = 1015;
            row5["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 13, 15);
            row5["F_VERSION_ID"] = 2;


            // третья версия на хранении
            DataRow row6 = table.NewRow();
            row6["F_OBJECT_ID"] = 1892;
            row6["F_LC_STEP"] = 1018;
            row6["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 33, 18);
            row6["F_VERSION_ID"] = 2;

            // четвертая версия на производстве
            DataRow row7 = table.NewRow();
            row7["F_OBJECT_ID"] = 1893;
            row7["F_LC_STEP"] = 1015;
            row7["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 33, 18);
            row7["F_VERSION_ID"] = 3;

            table.Rows.Add(row1);
            table.Rows.Add(row2);
            table.Rows.Add(row3);
            table.Rows.Add(row4);
            table.Rows.Add(row5);
            table.Rows.Add(row6);
            table.Rows.Add(row7);

            long result = service.SelectVersionOnDate(table, new DateTime(2019,02,13,12,30,00));

            Assert.AreEqual(1892,result);
        }

        [TestMethod]
        public void GetVersionsOnManufacturing2()
        {
            Service service = new Service();
            DataTable table = new DataTable();
            DataColumn versionIdColumn = new DataColumn("F_OBJECT_ID", typeof(long));
            DataColumn stepColumn = new DataColumn("F_LC_STEP", typeof(int));
            DataColumn dateColumn = new DataColumn("F_START_DATE", typeof(DateTime));
            DataColumn versionColumn = new DataColumn("F_VERSION_ID", typeof(int));
            table.Columns.AddRange(new DataColumn[] {versionIdColumn,stepColumn,dateColumn, versionColumn});

            // версия на производстве
            DataRow row1 = table.NewRow();
            row1["F_OBJECT_ID"] = 1890;
            row1["F_LC_STEP"] = 1015;
            row1["F_START_DATE"] = new DateTime(2019, 02, 13, 12,01, 28);
            row1["F_VERSION_ID"] = 0;

            // версия на хранении
            DataRow row2 = table.NewRow();
            row2["F_OBJECT_ID"] = 1890;
            row2["F_LC_STEP"] = 1018;
            row2["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 33, 31);
            row2["F_VERSION_ID"] = 0;
            
            // вторая версия на производстве
            DataRow row3 = table.NewRow();
            row3["F_OBJECT_ID"] = 1891;
            row3["F_LC_STEP"] = 1015;
            row3["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 48, 58);
            row3["F_VERSION_ID"] = 1;
            
            // вторая версия на производстве
            DataRow row4 = table.NewRow();
            row4["F_OBJECT_ID"] = 1891;
            row4["F_LC_STEP"] = 1015;
            row4["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 50, 32);
            row4["F_VERSION_ID"] = 1;

            table.Rows.Add(row1);
            table.Rows.Add(row2);
            table.Rows.Add(row3);
            table.Rows.Add(row4);

            long result = service.SelectVersionOnDate(table, new DateTime(2019,02,13,12,33,31));

            Assert.AreEqual(1890,result);
        }


        [TestMethod]
        public void GetVersionsOnManufacturing3()
        {
            Service service = new Service();

            DataTable table = new DataTable();
            DataColumn versionIdColumn = new DataColumn("F_OBJECT_ID", typeof(long));
            DataColumn stepColumn = new DataColumn("F_LC_STEP", typeof(int));
            DataColumn dateColumn = new DataColumn("F_START_DATE", typeof(DateTime));
            DataColumn versionColumn = new DataColumn("F_VERSION_ID", typeof(int));
            table.Columns.AddRange(new DataColumn[] {versionIdColumn,stepColumn,dateColumn, versionColumn});

            // версия на производстве
            DataRow row1 = table.NewRow();
            row1["F_OBJECT_ID"] = 1890;
            row1["F_LC_STEP"] = 1015;
            row1["F_START_DATE"] = new DateTime(2019, 02, 13, 10,10, 10);
            row1["F_VERSION_ID"] = 0;

            // версия на хранении
            DataRow row2 = table.NewRow();
            row2["F_OBJECT_ID"] = 1890;
            row2["F_LC_STEP"] = 1018;
            row2["F_START_DATE"] = new DateTime(2019, 02, 13, 10, 11, 12);
            row2["F_VERSION_ID"] = 0;
            
            // вторая версия на производстве
            DataRow row3 = table.NewRow();
            row3["F_OBJECT_ID"] = 1891;
            row3["F_LC_STEP"] = 1015;
            row3["F_START_DATE"] = new DateTime(2019, 02, 13, 10, 11, 12);
            row3["F_VERSION_ID"] = 1;

            // вторая версия на хранении
            DataRow row4 = table.NewRow();
            row4["F_OBJECT_ID"] = 1891;
            row4["F_LC_STEP"] = 1018;
            row4["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 13, 15);
            row4["F_VERSION_ID"] = 1;

            // третья версия на производстве
            DataRow row5 = table.NewRow();
            row5["F_OBJECT_ID"] = 1892;
            row5["F_LC_STEP"] = 1015;
            row5["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 13, 15);
            row5["F_VERSION_ID"] = 2;



            // версия на хранении
            DataRow row8 = table.NewRow();
            row8["F_OBJECT_ID"] = 1890;
            row8["F_LC_STEP"] = 1018;
            row8["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 13, 20);
            row8["F_VERSION_ID"] = 0;


            // третья версия на хранении
            DataRow row6 = table.NewRow();
            row6["F_OBJECT_ID"] = 1892;
            row6["F_LC_STEP"] = 1018;
            row6["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 33, 18);
            row6["F_VERSION_ID"] = 2;

            // четвертая версия на производстве
            DataRow row7 = table.NewRow();
            row7["F_OBJECT_ID"] = 1893;
            row7["F_LC_STEP"] = 1015;
            row7["F_START_DATE"] = new DateTime(2019, 02, 13, 12, 33, 18);
            row7["F_VERSION_ID"] = 3;

            table.Rows.Add(row1);
            table.Rows.Add(row2);
            table.Rows.Add(row3);
            table.Rows.Add(row4);
            table.Rows.Add(row5);
            table.Rows.Add(row8);
            table.Rows.Add(row6);
            table.Rows.Add(row7);

            long result = service.SelectVersionOnDate(table, new DateTime(2019,02,13,12,30,00));

            Assert.AreEqual(1892,result);
        }

    }
}
