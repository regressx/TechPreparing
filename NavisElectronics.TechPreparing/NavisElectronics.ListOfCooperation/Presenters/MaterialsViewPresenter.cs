// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaterialsViewPresenter.cs" company="">
//   
// </copyright>
// <summary>
//   Представитель для окна Электронной ведомости материалов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Presenters
{
    using NavisElectronics.ListOfCooperation;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;
    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.ViewInterfaces;
    using NavisElectronics.TechPreparation.ViewModels;
    using NavisElectronics.TechPreparation.Views;

    /// <summary>
    /// Представитель для окна Электронной ведомости материалов
    /// </summary>
    public class MaterialsViewPresenter
    {
        /// <summary>
        /// Модель для электронной ведомости материалов
        /// </summary>
        private readonly MaterialsViewModel _model;

        /// <summary>
        /// Представление электронной ведомости материалов
        /// </summary>
        private readonly IMaterialsView _view;

        /// <summary>
        /// Главный элемент дерева
        /// </summary>
        private readonly IntermechTreeElement _mainElement;

        /// <summary>
        /// Тип собираемого объекта
        /// </summary>
        private readonly IntermechObjectTypes _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialsViewPresenter"/> class.
        /// </summary>
        /// <param name="view">
        /// Представление
        /// </param>
        /// <param name="mainElement">
        /// Главный элемент дерева
        /// </param>
        /// <param name="type">
        /// Тип выгружаемых изделий
        /// </param>
        public MaterialsViewPresenter(IMaterialsView view, IntermechTreeElement mainElement, IntermechObjectTypes type)
        {
            _model = new MaterialsViewModel();
            _view = view;
            _mainElement = mainElement;
            _type = type;
            _view.Load += View_Load;
            _view.SaveClick += View_SaveClick;
            _view.IntermechObjectClick += View_IntermechObjectClick;
        }

        private InfoInTablePresenter _presenter;
        private int _index;
        private void View_IntermechObjectClick(object sender, ExtractedObjectEventArgs e)
        {
            InfoInTableView tableView = new InfoInTableView();
            _index = e.RowIndex;
            _presenter = new InfoInTablePresenter(tableView, new InfoInTableModel(_mainElement), e.ExtractedObject);
            _presenter.NotifyClient += Presenter_NotifyClient;
            _presenter.Run();
        }

        private void Presenter_NotifyClient(object sender, System.EventArgs e)
        {
            _presenter.NotifyClient -= Presenter_NotifyClient;
            _view.RedrawRow(_index);
        }

        /// <summary>
        /// Обработчик события сохранения данных
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void View_SaveClick(object sender, System.EventArgs e)
        {
            _model.Save(_mainElement);
        }

        /// <summary>
        /// Обработчик события загрузки формы
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void View_Load(object sender, System.EventArgs e)
        {
            _view.FillDataGrid(_model.GetObjects(_mainElement, _type));
        }

        public void Run()
        {
            _view.Show();
        }
    }
}