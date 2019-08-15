using System.Collections.Generic;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Presenters;

namespace NavisElectronics.TechPreparation.Views
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StructDialogView : Form, IStructDialogView<IStructElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructDialogView"/> class.
        /// </summary>
        public StructDialogView()
        {
            InitializeComponent();
        }

        public void BuildView(ICollection<TreeColumn> columnsToBuild, TreeViewSettings settings)
        {
            int i = 0;
            foreach (TreeColumn column in columnsToBuild)
            {
                treeViewAdv1.Columns.Add(column);
                NodeTextBox control = new NodeTextBox();
                control.DataPropertyName = settings.DataProperties[i];
                control.ParentColumn = column;
                treeViewAdv1.NodeControls.Add(control);
                i++;
            }
        }

        public void FillTree(TreeModel model)
        {
            treeViewAdv1.Model = null;
            treeViewAdv1.Model = model;
        }
    }
}
