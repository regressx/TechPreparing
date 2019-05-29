namespace NavisElectronics.ListOfCooperation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Entities;
    using EventArguments;
    using ViewModels;

    /// <summary>
    /// Представитель для 
    /// </summary>
    public class InfoInTablePresenter
    {
        /// <summary>
        /// Представление для 
        /// </summary>
        private readonly ITableView _view;
        private readonly InfoInTableModel _model;
        private readonly ExtractedObject _extractedObject;
        public event EventHandler NotifyClient;

        public InfoInTablePresenter(ITableView view, InfoInTableModel model, ExtractedObject extractedObject)
        {
            _view = view;
            _model = model;
            _extractedObject = extractedObject;
            _view.Load += View_Load;
            _view.EditClick += _view_EditClick;
        }

        private void _view_EditClick(object sender, ExtractedObjectCollectionEventArgs e)
        {
            using (MultiplyParametersView view = new MultiplyParametersView())
            {
                MultiplyParametersViewPresenter presenter = new MultiplyParametersViewPresenter(view, new MultiplyParametersViewModel());

                // меняем параметры выделенных строк
                if (presenter.Run() == DialogResult.OK)
                {
                    IList<Parameter> parameters = presenter.GetParameters();
                    double amount = Convert.ToDouble(parameters[0].Value);
                    double stockRate = Convert.ToDouble(parameters[1].Value);
                    _model.SetParametersToSelectedElements(e.ExctractedObjectsCollection, _extractedObject, amount, stockRate);
                    IList<ExtractedObject> elements = _model.GetUniqueElements(_extractedObject);
                    _view.FillGrid(elements);
                    if (NotifyClient != null)
                    {
                        NotifyClient(sender, EventArgs.Empty);
                    }
                } 
            }
        }

        private void View_Load(object sender, EventArgs e)
        {
            IList<ExtractedObject> elements = _model.GetUniqueElements(_extractedObject);
            _view.FillGrid(elements);
            _view.SetFormCaption(string.Format("Сведения о входимости материала {0} {1}", _extractedObject.Id, _extractedObject.Name));
        }

        public DialogResult RunDialog()
        {
            return _view.ShowDialog();
        }

        public void Run()
        {
            _view.Show();
        }
    }
}