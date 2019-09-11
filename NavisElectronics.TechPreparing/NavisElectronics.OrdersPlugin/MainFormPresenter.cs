using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavisElectronics.Orders
{
    public class MainFormPresenter:IPresenter<long, CancellationToken>
    {
        private readonly IMainView _view;
        private CancellationToken _token;

        public MainFormPresenter(IMainView view)
        {
            _view = view;
            _view.Load += View_Load;
        }

        private async void View_Load(object sender, System.EventArgs e)
        {
            await Task.Delay(-1, _token);
            MessageBox.Show("Задача прервана");
        }

        public void Run(long parameter, CancellationToken token)
        {
            _token = token;
            _view.Show();
        }
    }
}