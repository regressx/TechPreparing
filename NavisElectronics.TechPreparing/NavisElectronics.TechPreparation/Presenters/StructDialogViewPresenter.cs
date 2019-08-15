using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.ViewModels;

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using Aga.Controls.Tree;
    using Aga.Controls.Tree.NodeControls;
    using ViewInterfaces;

    /// <summary>
    /// The struct dialog view presenter.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="V">
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
        }

        private void _view_Load(object sender, EventArgs e)
        {
            ICollection<TreeColumn> columnsToBuild = _settings.Columns;
            _view.BuildView(columnsToBuild, _settings);
            _model.BuildTree((V)_settings.ElementToBuild, _settings);
        }

        public void Run(TreeViewSettings settings)
        {
            _settings = settings;
            _view.Show();
        }

    }

    public class TreeViewSettings
    {
        private IList<string> _dataProperties;
        private IList<TreeColumn> _columns;

        public TreeViewSettings()
        {
            _columns = new List<TreeColumn>();
            _dataProperties = new List<string>();
        }

        public IList<string> DataProperties
        {
            get { return _dataProperties; }
            set { _dataProperties = value; }
        }

        public IList<TreeColumn> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public void AddColumn(TreeColumn column, string dataPropertyName)
        {
            DataProperties.Add(dataPropertyName);
            Columns.Add(column);
        }

        public IStructElement ElementToBuild { get; set; }

        public IStructElement Result { get; set; }
    }

    public interface IStructDialogView<T> : IView where T : IStructElement
    {
        event EventHandler Load;
        void BuildView(ICollection<TreeColumn> columnsToBuild, TreeViewSettings settings);
        void FillTree(TreeModel model);
    }
}