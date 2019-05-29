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
    public class TreeNodeDialogPresenter : BasePresenter<IntermechTreeElement>
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

        /// <summary>
        /// Элемент, тех. подготовку которого мы копируем
        /// </summary>
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
        /// Элемент, тех. подготовку которого мы копируем
        /// </summary>
        public IntermechTreeElement ElementToCopy
        {
            get { return _elementToCopy; }
        }

        /// <summary>
        /// Запуск представителя.
        /// </summary>
        /// <param name="parameter">
        /// Передаем дерево
        /// </param>
        public override void Run(IntermechTreeElement parameter)
        {
            _elementToBuild = parameter;
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