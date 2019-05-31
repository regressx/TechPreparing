namespace NavisElectronics.ListOfCooperation.Presenters
{
    using System;
    using Aga.Controls.Tree;
    using Entities;
    using ViewInterfaces;
    using ViewModels;

    /// <summary>
    /// Представитель окна загрузки уже созданной тех. подготовки из ранее выполненных заказов
    /// </summary>
    public class TreeNodeDialogPresenter : BasePresenter<IntermechTreeElement, IntermechTreeElement>
    {
        /// <summary>
        /// Представление
        /// </summary>
        private readonly ITreeNodeDialogView _view;

        /// <summary>
        /// Модель для обработки запросов с формы
        /// </summary>
        private readonly TreeNodeDialogViewModel _model;


        /// <summary>
        /// The _element to build.
        /// </summary>
        private IntermechTreeElement _elementToBuild;

        private IntermechTreeElement _elementToCopy;


        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNodeDialogPresenter"/> class.
        /// </summary>
        /// <param name="treeNodeDialog">
        /// The tree node dialog.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        public TreeNodeDialogPresenter(ITreeNodeDialogView treeNodeDialog, TreeNodeDialogViewModel model)
        {
            _view = treeNodeDialog;
            _model = model;
            _view.Load += _treeNodeDialog_Load;
            _view.AcceptClick += _view_AcceptClick;
        }

        /// <summary>
        /// Запуск представителя.
        /// </summary>
        /// <param name="parameter">
        /// Передаем дерево
        /// </param>
        public override void Run(IntermechTreeElement parameter, IntermechTreeElement element)
        {
            _elementToBuild = parameter;
            _elementToCopy = element;
            _view.Show();
        }


        /// <summary>
        /// Обработчик события нажатия кнопки "Принять"
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _view_AcceptClick(object sender, IntermechTreeElement e)
        {
            _elementToCopy = e;
            _view.Close();
        }

        /// <summary>
        /// Обрабочик события загрузки формы
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _treeNodeDialog_Load(object sender, EventArgs e)
        {
            MyNode mainNode = _model.BuildTree(_elementToBuild);
            TreeModel model = new TreeModel();
            model.Nodes.Add(mainNode);
            _view.FillTree(model);
        }
    }
}