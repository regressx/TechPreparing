namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.Entities;

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
            if (listBox1.Items.Count > 0)
            {
                Agent filterAgent = (Agent)listBox1.SelectedItem;
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
                listBox1.Items.Add(agent);
            }
        }
    }
}
