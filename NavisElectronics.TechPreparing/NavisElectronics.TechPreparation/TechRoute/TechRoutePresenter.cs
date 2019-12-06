// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechRoutePresenter.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the TechRoutePresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using EventArguments;
    using Interfaces.Entities;
    using ViewInterfaces;
    using ViewModels;

    /// <summary>
    /// The tech route presenter.
    /// </summary>
    public class TechRoutePresenter : IPresenter<TechRouteNode, IList<TechRouteNode>>
    {
        /// <summary>
        /// The _view.
        /// </summary>
        private readonly ITechRouteView _view;

        /// <summary>
        /// The _model.
        /// </summary>
        private readonly TechRouteModel _model;

        /// <summary>
        /// The _organization struct.
        /// </summary>
        private TechRouteNode _organizationStruct;

        /// <summary>
        /// The _organization struct.
        /// </summary>
        private IList<TechRouteNode> _resultNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRoutePresenter"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        public TechRoutePresenter(ITechRouteView view, TechRouteModel model)
        {
            _view = view;
            _view.Load += _view_Load;
            _model = model;
            _view.RouteNodeClick += _view_RouteNodeClick;
            _view.RemoveNodeClick += _view_RemoveNodeClick;
        }

        private void _view_Load(object sender, EventArgs e)
        {
            TreeModel model = _model.GetModel(_organizationStruct);
            _view.FillWorkShop(model);
        }

        private void _view_RemoveNodeClick(object sender, RemoveNodeEventArgs e)
        {
            _resultNode.RemoveAt(e.IndexToRemove);
            _model.Remove(e.IndexToRemove);
            _view.FillListBox(_model.GetTechRouteNodes());
            _view.FillTextBox(_model.GetTechRouteNodes());
        }

        private void _view_RouteNodeClick(object sender, RouteNodeClickEventAgrs e)
        {
            _model.Add(e.Node);
            _resultNode.Add(e.Node);
            _view.FillListBox(_model.GetTechRouteNodes());
            _view.FillTextBox(_model.GetTechRouteNodes());
        }

        public void Run(TechRouteNode parameter, IList<TechRouteNode> resultNode)
        {
            _organizationStruct = parameter;
            _resultNode = resultNode;
            _view.Show();
        }
    }
}