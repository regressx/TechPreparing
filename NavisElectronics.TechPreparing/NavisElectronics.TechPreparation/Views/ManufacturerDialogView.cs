// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectSomethingView.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the SelectManufacturerView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewInterfaces;

    /// <summary>
    /// The select manufacturer view.
    /// </summary>
    public partial class ManufacturerDialogView : Form, ISelectManufacturerView
    {
        public ManufacturerDialogView()
        {
            InitializeComponent();
        }
        public event EventHandler<Agent> SelectAgent;

        public new void Show()
        {
            this.ShowDialog();
        }

        public void FillAgents(ICollection<Agent> agents)
        {
            foreach (Agent agent in agents)
            {
                listBox.Items.Add(agent);
            }
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox.Items.Count > 0)
            {
                if (SelectAgent != null)
                {
                    Agent filterAgent = (Agent)listBox.SelectedItem;
                    SelectAgent(sender, filterAgent);
                    DialogResult = DialogResult.OK;
                    Close();
                }

            }
        }






    }
}
