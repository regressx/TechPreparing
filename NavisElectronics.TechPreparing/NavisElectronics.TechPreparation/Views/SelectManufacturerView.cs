// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectSomethingView.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the SelectManufacturerView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// The select manufacturer view.
    /// </summary>
    public partial class SelectManufacturerView : Form
    {
        private readonly ICollection<Agent> _agents;
        public string SelectedAgentId { get; set; }
        public string SelectedAgentName { get; set; }

        public SelectManufacturerView(ICollection<Agent> agents)
        {
            _agents = agents;
            InitializeComponent();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox.Items.Count > 0)
            {
                Agent filterAgent = (Agent)listBox.SelectedItem;
                SelectedAgentId = filterAgent.Id.ToString();
                SelectedAgentName = filterAgent.Name;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void SelectManufacturerView_Load(object sender, EventArgs e)
        {
            foreach (Agent agent in _agents)
            {
                listBox.Items.Add(agent);
            }
        }
    }
}
