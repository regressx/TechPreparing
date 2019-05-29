using System.Windows.Forms;
using NavisElectronics.ListOfCooperation.ViewInterfaces;

namespace NavisElectronics.ListOfCooperation.Presenters
{
    public class ParametersViewPresenter
    {
        private IParametersView _view;

        public ParametersViewPresenter(IParametersView view)
        {
            _view = view;
        }

        public DialogResult Run()
        {
            return _view.ShowDialog();
        }

        public double GetStockRate()
        {
            return _view.GetStockRate();
        }

        public string GetSampleSize()
        {
            return _view.GetSampleSize();
        }
    }
}