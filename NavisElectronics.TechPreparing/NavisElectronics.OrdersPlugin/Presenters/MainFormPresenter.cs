using Intermech.Client.Core.FormDesigner.Actions.ContextCommand;
using NavisElectronics.Orders.TreeComparer;
using NavisElectronics.Orders.Views;
using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Services;

namespace NavisElectronics.Orders.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;
    using Enums;
    using EventArguments;
    using TechPreparation.Interfaces.Entities;
    using ViewModels;

    public class MainFormPresenter : IPresenter<long, CancellationTokenSource>
    {
        /// <summary>
        /// представление
        /// </summary>
        private readonly IMainView _view;

        /// <summary>
        /// модель представления
        /// </summary>
        private readonly MainFormModel _model;

        /// <summary>
        /// токен отмены
        /// </summary>
        private CancellationTokenSource _tokenSource;

        /// <summary>
        /// The _order version id.
        /// </summary>
        private long _orderVersionId;

        /// <summary>
        /// The _root.
        /// </summary>
        private IntermechTreeElement _root;

        public MainFormPresenter(IMainView view, MainFormModel model)
        {
            _view = view;
            _model = model;
            _view.Load += View_Load;
            _view.StartChecking += View_StartChecking;
            _view.AbortLoading += View_AbortLoading;
            _view.Save += View_Save;
            _view.SetProduceClick += View_ProduceClick;
            _view.DownloadAndUpdate += View_DownloadAndUpdate;
            _view.CreateReport += View_CreateReport;
            _view.DecryptDocumentNames += _view_DecryptDocumentNames;
        }

        private void _view_DecryptDocumentNames(object sender, EventArgs e)
        {
            _model.DecryptDocuments(_root);
            _view.UpdateTreeModel(_root);
            MessageBox.Show("Расшифровка произведена");
        }

        private void View_CreateReport(object sender, ReportStyle e)
        {
            OrderNode selectedTreeElement = _view.GetSelectedTreeElement();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (sfd.FileName != string.Empty)
                    {
                        _model.CreateReport(sfd.FileName, selectedTreeElement, e);
                    }
                    else
                    {
                        MessageBox.Show("Укажите имя файла!");
                    }
                }
            }

        }

        private void View_DownloadAndUpdate(object sender, EventArgs e)
        {
            TreeComparerPresenter treeComparerPresenter = new TreeComparerPresenter(new TreeComparerView(), new TreeComparerViewModel(new IntermechReader(new RecountService()), new MergeNodesService(), new LastVersionService()));
            treeComparerPresenter.Run(_root);
        }

        private async void View_Save(object sender, EventArgs e)
        {
            try
            {
                _view.UpdateSaveLabel("Начинаю сохранение");
                await _model.WriteBlobAttributeAsync(_orderVersionId, _root, 17964, _root.Name, new SerializeStrategyBson<IntermechTreeElement>());
                _view.UpdateSaveLabel("Последнее сохранение в " + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                _view.UpdateSaveLabel("Ошибка при сохранении");
                throw new Exception(ex.Message, ex);
            }
        }

        private void View_ProduceClick(object sender, ProduceEventArgs e)
        {
            SetNodeProduceSign(e.Element, e.ProduceSign, e.WhereToSet);
        }

        private void SetNodeProduceSign(IntermechTreeElement element, bool value, ProduceIn whereSetUp)
        {
            ICollection<IntermechTreeElement> nodesToSet = new List<IntermechTreeElement>();
            nodesToSet.Add(element);
            if (whereSetUp == ProduceIn.AllTree)
            {
                nodesToSet.Clear();
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(_root);
                while (queue.Count > 0)
                {
                    IntermechTreeElement elementFromQueue = queue.Dequeue();

                    if (elementFromQueue.ObjectId == element.ObjectId)
                    {
                        nodesToSet.Add(elementFromQueue);
                    }

                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }

            }

            foreach (IntermechTreeElement treeElement in nodesToSet)
            {
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(treeElement);
                while (queue.Count > 0)
                {
                    IntermechTreeElement elementFromQueue = queue.Dequeue();
                    elementFromQueue.ProduseSign = value;
                    elementFromQueue.RelationNote = value ? "НЕ ИЗГОТАВЛИВАТЬ" : string.Empty;

                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

        }


        private void View_AbortLoading(object sender, EventArgs e)
        {
            _tokenSource.Cancel();
        }

        private void View_StartChecking(object sender, EventArgs e)
        {
        }

        private async void View_Load(object sender, System.EventArgs e)
        {
            _view.UpdateTreeModel(new IntermechTreeElement() { Designation = "Пожалуйста, подождите!", Name = "Идет загрузка заказа..." });

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
                _view.UpdateTreeModel(_root);

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