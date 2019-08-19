using NavisElectronics.TechPreparation.Entities;
using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using Aga.Controls.Tree;
    using Aga.Controls.Tree.NodeControls;
    using Interfaces;
    using ViewInterfaces;
    using ViewModels;

    /// <summary>
    /// The struct dialog view presenter.
    /// </summary>
    /// <typeparam name="T">
    /// Узел представления в дереве
    /// </typeparam>
    /// <typeparam name="V">
    /// Элемент, который надо представить
    /// </typeparam>
    public class StructDialogViewPresenter<T, V> where T : Node, new() where V : IStructElement
    {
        private IStructDialogView<IStructElement> _view;
        private TreeViewSettings _settings;
        private StructDialogViewModel<T, V> _model;
        public StructDialogViewPresenter(IStructDialogView<IStructElement> view, StructDialogViewModel<T, V> model)
        {
            _view = view;
            _view.Load += _view_Load;
            _model = model;
            _view.AcceptClick += _view_AcceptClick;
        }

        private void _view_AcceptClick(object sender, EventArgs e)
        {
            _settings.Result = (V)_view.GetSelectedNode().Tag;
        }

        private void _view_Load(object sender, EventArgs e)
        {
            ICollection<TreeColumn> columnsToBuild = _settings.Columns;
            _view.BuildView(columnsToBuild, _settings);
            TreeModel model = _model.BuildTree((V)_settings.ElementToBuild, _settings);
            _view.FillTree(model);
        }

        public void Run(TreeViewSettings settings)
        {
            _settings = settings;
            _view.Show();
        }

    }

    public interface IStructDialogView<T> : IView where T : IStructElement
    {
        event EventHandler Load;
        event EventHandler AcceptClick;
        Node GetSelectedNode();
        void BuildView(ICollection<TreeColumn> columnsToBuild, TreeViewSettings settings);
        void FillTree(TreeModel model);
    }
}