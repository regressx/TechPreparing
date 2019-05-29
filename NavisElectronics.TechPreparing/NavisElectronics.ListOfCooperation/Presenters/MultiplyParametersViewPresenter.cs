using NavisElectronics.ListOfCooperation.ViewInterfaces;

namespace NavisElectronics.ListOfCooperation.Presenters
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ViewModels;

    public class MultiplyParametersViewPresenter
    {
        private IMultiplyParametersView _view;
        private IMultiplyParametersViewModel _model;

        public MultiplyParametersViewPresenter(IMultiplyParametersView view, IMultiplyParametersViewModel model)
        {
            _view = view;
            _model = model;
            _view.Load += _view_Load;
        }

        private void _view_Load(object sender, System.EventArgs e)
        {
            _view.FillGrid(_model.GetRegisteredParameters());
        }

        public IList<Parameter> GetParameters()
        {
            return _model.GetRegisteredParameters();
        }



        public DialogResult Run()
        {
            return _view.ShowDialog();
        }
    }
}