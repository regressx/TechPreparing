using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Views
{
    public partial class CompareTwoNodesView : Form
    {
        public CompareTwoNodesView(IntermechTreeElement leftElement, IntermechTreeElement rightElement)
        {
            InitializeComponent();
            propertyGrid1.SelectedObject = leftElement.Clone();
            propertyGrid2.SelectedObject = rightElement.Clone();
        }
    }
}
