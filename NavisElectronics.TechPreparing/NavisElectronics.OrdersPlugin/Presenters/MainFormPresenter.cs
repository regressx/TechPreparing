﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Aga.Controls.Tree;
using NavisElectronics.Orders.EventArguments;
using NavisElectronics.Orders.ViewModels;
using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparing.Data.Helpers;

namespace NavisElectronics.Orders.Presenters
{
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
            _view.AbortLoading += _view_AbortLoading;
            _view.DoNotProduceClick += _view_DoNotProduceClick;
        }

        private void _view_DoNotProduceClick(object sender, ProduceEventArgs e)
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


        private void _view_AbortLoading(object sender, EventArgs e)
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
            IntermechReader reader = new IntermechReader();
            try
            {
                _root = await reader.GetDataFromFileAsync(_orderVersionId, ConstHelper.FileAttribute);

                // расчет применяемостей
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(_root);
                while (queue.Count > 0)
                {
                    IntermechTreeElement elementFromQueue = queue.Dequeue();
                    IntermechTreeElement parent = elementFromQueue.Parent;
                    if (parent != null)
                    {
                        elementFromQueue.UseAmount = (int)parent.AmountWithUse;
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