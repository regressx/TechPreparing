using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders
{
    public class MainFormPresenter:IPresenter<long, CancellationToken>
    {
        private readonly IMainView _view;
        private CancellationToken _token;
        private long _orderVersionId;
        public MainFormPresenter(IMainView view)
        {
            _view = view;
            _view.Load += View_Load;
        }

        private async void View_Load(object sender, System.EventArgs e)
        {
            IntermechReader reader = new IntermechReader();
            IntermechTreeElement root = await reader.GetFullOrderAsync(_orderVersionId, _token);
            MessageBox.Show(root.Name);
        }

        public void Run(long parameter, CancellationToken token)
        {
            _orderVersionId = parameter;
            _token = token;
            _view.Show();
        }
    }
}