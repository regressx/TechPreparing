// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TreeNodeDialogPresenter.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Представитель окна загрузки уже созданной тех. подготовки из ранее выполненных заказов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewInterfaces;
    using NavisElectronics.TechPreparation.ViewModels;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Представитель окна загрузки уже созданной тех. подготовки из ранее выполненных заказов
    /// </summary>
    public class TreeNodeDialogPresenter : BasePresenter<Parameter<IntermechTreeElement>>
    {
        /// <summary>
        /// Представление
        /// </summary>
        private readonly ITreeNodeDialogView _view;

        /// <summary>
        /// Модель для обработки запросов с формы
        /// </summary>
        private readonly TreeNodeDialogViewModel _model;

        private Parameter<IntermechTreeElement> _parameter;


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
        public override void Run(Parameter<IntermechTreeElement> parameter)
        {
            _parameter = parameter;
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
            _parameter.SetParameter(1, e);
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
            MyNode mainNode = _model.BuildTree(_parameter.GetParameter(0));
            TreeModel model = new TreeModel();
            model.Nodes.Add(mainNode);
            _view.FillTree(model);
        }
    }
}