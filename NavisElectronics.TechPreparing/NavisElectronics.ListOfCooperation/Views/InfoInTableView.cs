namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Intermech.Navigator.Controls;

    using NavisArchiveWork.Data;
    using NavisArchiveWork.Model;

    using NavisElectronics.ListOfCooperation.Entities;
    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.ViewInterfaces;

    using TenTec.Windows.iGridLib;

    public partial class InfoInTableView : Form, ITableView
    {

        public event EventHandler<ExtractedObjectCollectionEventArgs> EditClick;
        public InfoInTableView()
        {
            InitializeComponent();
        }

        private void editParametersMenuItem_Click(object sender, EventArgs e)
        {
            if (EditClick != null)
            {
                IList<ExtractedObject> selectedRows = new List<ExtractedObject>();
                iGSelectedCellsCollection selectedCells = iGrid1.SelectedCells;

                IDictionary<int, iGCell> cellsDictionary = new Dictionary<int, iGCell>();
                foreach (iGCell cell in selectedCells)
                {
                    if (!cellsDictionary.ContainsKey(cell.RowIndex))
                    {
                        cellsDictionary.Add(cell.RowIndex,cell);
                    }
                }

                foreach (int rowIndex in cellsDictionary.Keys)
                {
                    ExtractedObject element = iGrid1.Rows[rowIndex].Tag as ExtractedObject;
                    selectedRows.Add(element);
                }

                EditClick(sender, new ExtractedObjectCollectionEventArgs(selectedRows));
            }
        }

        readonly Search _search = new Search(new Repository("ParentDirectoryNames.xml"));
        private void goToArchiveButton_Click(object sender, EventArgs e)
        {
            ExtractedObject selectedNode = iGrid1.SelectedCells[0].Row.Tag as ExtractedObject;
            FileDesignation fd = _search.GetFileDesignation(selectedNode.Designation);
            _search.StepToFolder(_search.GetFullPath(fd));
        }

        public IObjectView GetSelectedObjectView()
        {
            throw new NotImplementedException();
        }



        public void FillGrid(IList<ExtractedObject> elements)
        {
            iGrid1.BeginUpdate();
            try
            {
                iGrid1.Rows.Clear();
                iGrid1.Rows.AddRange(elements.Count);
                for (int i = 0; i < elements.Count; i++)
                {
                    iGrid1.Rows[i].Cells[0].Value = elements[i].Id;
                    iGrid1.Rows[i].Cells[1].Value = elements[i].Designation;
                    iGrid1.Rows[i].Cells[2].Value = elements[i].Name;
                    iGrid1.Rows[i].Cells[3].Value = elements[i].Amount;
                    iGrid1.Rows[i].Cells[4].Value = elements[i].ParentUse;
                    iGrid1.Rows[i].Cells[5].Value = elements[i].AmountWithUse;
                    iGrid1.Rows[i].Cells[6].Value = elements[i].StockRate;
                    iGrid1.Rows[i].Cells[7].Value = elements[i].Total;
                    iGrid1.Rows[i].Tag = elements[i];
                }
            }
            finally
            {
                iGrid1.EndUpdate();
            }
        }

        public void SetFormCaption(string format)
        {
            Text = format;
        }

        private void InfoInTableView_Load(object sender, EventArgs e)
        {
            //Text = string.Format("Сведения о входимости материала {0} {1}", _material.Id, _material.Name);
        }

        private void openObjectCardButton_Click(object sender, EventArgs e)
        {
            ExtractedObject selectedNode = iGrid1.SelectedCells[0].Row.Tag as ExtractedObject;
            if (selectedNode.Id != -1L)
            {
                PropertiesWindow.Execute(string.Empty, string.Empty, selectedNode.Id);
            }
        }
    }

    public interface IObjectView
    {
    }
}
