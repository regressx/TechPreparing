using System;
using System.Windows.Forms;
using NavisElectronics.Orders.Enums;
using NavisElectronics.Orders.EventArguments;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using TenTec.Windows.iGridLib;

namespace NavisElectronics.Orders
{
    /// <summary>
    /// The main form with grid.
    /// </summary>
    public partial class MainFormWithGrid : Form, IMainView
    {
        public MainFormWithGrid()
        {
            InitializeComponent();
        }

        public event EventHandler DownloadAndUpdate;
        public event EventHandler Save;
        public event EventHandler StartChecking;
        public event EventHandler AbortLoading;
        public event EventHandler<ReportStyle> CreateReport;
        public event EventHandler<ProduceEventArgs> SetProduceClick;

        public void UpdateTreeModel(IntermechTreeElement treeModel)
        {
            iGrid1.Rows.Clear();
            iGrid1.Cols.Clear();

            // Var to reference the last added row
            iGRow myRow;

            // Create one column for our tree
            iGrid1.Cols.Add("Designation");

            // Create one column for our tree
            iGrid1.Cols.Add("Name");

            // Add the first root node
            myRow = iGrid1.Rows.Add();
            myRow.TreeButton = iGTreeButtonState.Visible;
            myRow.Cells[0].Value = treeModel.Designation;
            myRow.Cells[1].Value = treeModel.Name;

            BuildRecursive(iGrid1, treeModel, 1);

        }

        private void BuildRecursive(iGrid iGrid, IntermechTreeElement root, int level)
        {
            foreach (IntermechTreeElement child in root.Children)
            {
                // Add one child node to the root
                iGRow myRow = iGrid.Rows.Add();
                myRow.Level = level;
                myRow.TreeButton = iGTreeButtonState.Hidden;
                myRow.Cells[0].Value = child.Designation;
                myRow.Cells[1].Value = child.Name;

                if (child.Children.Count > 0)
                {
                    level++;
                    myRow.TreeButton = iGTreeButtonState.Visible;
                    BuildRecursive(iGrid, child, level);
                    level--;
                }

            }
        }


        public void UpdateSaveLabel(string message)
        {
            throw new NotImplementedException();
        }
    }
}
