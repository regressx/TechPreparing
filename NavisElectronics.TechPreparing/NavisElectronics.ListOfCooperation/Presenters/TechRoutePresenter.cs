namespace NavisElectronics.ListOfCooperation.Presenters
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using Entities;
    using EventArguments;
    using ViewInterfaces;
    using ViewModels;

    public class TechRoutePresenter
    {
        private ITechRouteView _view;
        private TechRouteModel _model;
        private readonly TechRouteNode _mainNode;

        public TechRoutePresenter(ITechRouteView view, TechRouteModel model, TechRouteNode mainNode)
        {
            _view = view;
            _view.Load += _view_Load;
            _model = model;
            _mainNode = mainNode;
            _view.RouteNodeClick += _view_RouteNodeClick;
            _view.RemoveNodeClick += _view_RemoveNodeClick;
        }

        private void _view_Load(object sender, System.EventArgs e)
        {
            TreeModel model = _model.GetModel(_mainNode);
            _view.FillWorkShop(model);
        }

        private void _view_RemoveNodeClick(object sender, RemoveNodeEventArgs e)
        {
            _model.Remove(e.IndexToRemove);
            _view.FillListBox(_model.GetTechRouteNodes());
            _view.FillTextBox(_model.GetTechRouteNodes());
        }

        private void _view_RouteNodeClick(object sender, RouteNodeClickEventAgrs e)
        {
            _model.Add(e.Node);
            _view.FillListBox(_model.GetTechRouteNodes());
            _view.FillTextBox(_model.GetTechRouteNodes());
        }

        public DialogResult Run()
        {
           return _view.ShowDialog();
        }

        public IList<TechRouteNode> GetTechRoute()
        {
            return new List<TechRouteNode>();
        }
    }
}