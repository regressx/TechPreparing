namespace NavisElectronics.Orders.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using EventArguments;
    using TechPreparation.Interfaces.Entities;
    using ViewModels;

    public class MainFormPresenter : IPresenter<long, CancellationTokenSource>
    {
        private readonly IMainView _view;
        private readonly MainFormModel _model;
        private CancellationTokenSource _tokenSource;
        private long _orderVersionId;
        private IntermechTreeElement _root;

        public MainFormPresenter(IMainView view, MainFormModel model)
        {
            _view = view;
            _model = model;
            _view.Load += View_Load;
            _view.StartChecking += View_StartChecking;
            _view.AbortLoading += View_AbortLoading;
            _view.Save += View_Save;
            _view.DoNotProduceClick += View_DoNotProduceClick;
            _view.DownloadAndUpdate += View_DownloadAndUpdate;
        }

        private void View_DownloadAndUpdate(object sender, EventArgs e)
        {

        }

        private async void View_Save(object sender, EventArgs e)
        {
            try
            {
                _view.UpdateSaveLabel("Начинаю сохранение");
                await _model.WriteBlobAttributeAsync(_orderVersionId, _root, 17964, _root.Name);
                _view.UpdateSaveLabel("Последнее сохранение в " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                _view.UpdateSaveLabel("Ошибка при сохранении");
                throw new Exception(ex.Message, ex);
            }
        }

        private void View_DoNotProduceClick(object sender, ProduceEventArgs e)
        {
            SetNodeProduceSign(e.Element, e.ProduceSign);
        }

        private void SetNodeProduceSign(IntermechTreeElement element, bool value)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(element);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.ProduseSign = value;

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private void View_AbortLoading(object sender, EventArgs e)
        {
            _tokenSource.Cancel();
        }

        private void View_StartChecking(object sender, EventArgs e)
        {
            ListsComparerPresenter presenter = new ListsComparerPresenter(new ListsComparerForm());
            presenter.Run(_root);
        }

        private async void View_Load(object sender, System.EventArgs e)
        {
            try
            {
                // проверяем наличие атрибута "Двоичные данные заказа"
                if (await _model.AttributeExist(_orderVersionId, 17964))
                {
                    _root = await _model.ReadDataFromBlobAttribute<IntermechTreeElement>(_orderVersionId, 17964);
                }
                else
                {
                    // грузим, если атрибута не было
                    _root = await _model.GetFullOrderAsync(_orderVersionId, _tokenSource.Token);
                }


                // на всякий случай пересчитаем применяемости
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(_root);
                while (queue.Count > 0)
                {
                    IntermechTreeElement elementFromQueue = queue.Dequeue();
                    IntermechTreeElement parent = elementFromQueue.Parent;
                    if (parent != null)
                    {
                        elementFromQueue.UseAmount = (int)Math.Round(parent.AmountWithUse, MidpointRounding.ToEven);
                        elementFromQueue.AmountWithUse = elementFromQueue.UseAmount * elementFromQueue.Amount;
                        elementFromQueue.TotalAmount = elementFromQueue.AmountWithUse * elementFromQueue.StockRate;
                    }

                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }

                TreeModel treeModel = _model.GetTreeModel(_root);
                _view.UpdateTreeModel(treeModel);

            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Операция загрузки заказа была отменена");
            }
        }

        public void Run(long parameter, CancellationTokenSource tokenSource)
        {
            _orderVersionId = parameter;
            _tokenSource = tokenSource;
            _view.Show();
        }
    }
}