using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aga.Controls.Tree;
using NavisElectronics.Orders.ViewModels;
using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders
{
    public class MainFormPresenter:IPresenter<long, CancellationToken>
    {
        private readonly IMainView _view;
        private readonly MainFormModel _model;
        private CancellationToken _token;
        private long _orderVersionId;
        public MainFormPresenter(IMainView view, MainFormModel model)
        {
            _view = view;
            _model = model;
            _view.Load += View_Load;

        }

        private async void View_Load(object sender, System.EventArgs e)
        {
            IntermechReader reader = new IntermechReader();
            try
            {
                IntermechTreeElement root = await reader.GetFullOrderAsync(_orderVersionId, _token);
                TreeModel treeModel = _model.GetTreeModel(root);
                _view.UpdateTreeModel(treeModel);

            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Операция загрузки заказа была отменена");
            }
        }

        public void Run(long parameter, CancellationToken token)
        {
            _orderVersionId = parameter;
            _token = token;
            _view.Show();
        }
    }
}