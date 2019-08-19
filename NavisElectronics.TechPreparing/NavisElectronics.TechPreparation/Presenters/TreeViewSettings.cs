using System.Collections.Generic;
using Aga.Controls.Tree;
using NavisElectronics.TechPreparation.Interfaces;

namespace NavisElectronics.TechPreparation.Presenters
{
    public class TreeViewSettings
    {
        private IList<string> _dataProperties;
        private IList<TreeColumn> _columns;

        public TreeViewSettings()
        {
            _columns = new List<TreeColumn>();
            _dataProperties = new List<string>();
        }

        public IList<string> DataProperties
        {
            get { return _dataProperties; }
            set { _dataProperties = value; }
        }

        public IList<TreeColumn> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public void AddColumn(TreeColumn column, string dataPropertyName)
        {
            DataProperties.Add(dataPropertyName);
            Columns.Add(column);
        }

        public IStructElement ElementToBuild { get; set; }

        public IStructElement Result { get; set; }
    }
}