// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectManufacturerPresenter.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the SelectManufacturerPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Presenters
{
    using System.Collections.Generic;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewInterfaces;

    public class SelectManufacturerPresenter:IPresenter<Parameter<Agent>>
    {
        private ISelectManufacturerView _view;

        private Parameter<Agent> _parameter;

        public SelectManufacturerPresenter(ISelectManufacturerView view)
        {
            _view = view;
            _view.Load += _view_Load;
            _view.SelectAgent += _view_SelectAgent;
        }

        private void _view_SelectAgent(object sender, Agent e)
        {
            Agent filterAgent = _parameter.GetParameter(_parameter.Count - 1);
            filterAgent.Id = e.Id;
            filterAgent.Name = e.Name;
        }

        private void _view_Load(object sender, System.EventArgs e)
        {
            ICollection<Agent> agents = new List<Agent>();
            for (int i = 0; i < _parameter.Count - 1; i++)
            {
                agents.Add(_parameter.GetParameter(i));
            }

            _view.FillAgents(agents);
        }

        public void Run(Parameter<Agent> parameter)
        {
            _parameter = parameter;
            _view.Show();
        }
    }
}