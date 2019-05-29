using System;
using System.Collections.Generic;
using Aga.Controls.Tree;
using NavisElectronics.ListOfCooperation.ViewInterfaces;
using NavisElectronics.ListOfCooperation.ViewModels;
using NavisElectronics.ListOfCooperation.Views;

namespace NavisElectronics.ListOfCooperation.Presenters
{
    public class FindNodePresenter
    {
        public event EventHandler<TreeNodeAdv> SearchInitClick;
        public event EventHandler ViewClosing;
        private IFindNodeView _view;
        private readonly TreeViewAdv _treeView;

        public FindNodePresenter(IFindNodeView view, TreeViewAdv treeView)
        {
            _view = view;
            _treeView = treeView;
            _view.FindButtonClick += _view_FindButtonClick;
            _view.NodeClick += _view_NodeClick;
            _view.FormClosing += _view_FormClosing;
        }

        private void _view_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (ViewClosing != null)
            {
                ViewClosing(sender, e);
            }
        }

        private void _view_NodeClick(object sender, TreeNodeAdv e)
        {
            EventHandler<TreeNodeAdv> temp = SearchInitClick;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void _view_FindButtonClick(object sender, System.EventArgs e)
        {
            IList<TreeNodeAdv> list = new List<TreeNodeAdv>();

            foreach (TreeNodeAdv node in _treeView.AllNodes)
            {
                CooperationNode tagNode = node.Tag as CooperationNode;
                if (tagNode.Designation != null)
                {
                    if (tagNode.Designation.Contains(_view.Designation))
                    {
                        list.Add(node);
                    }
                }

            }

            foreach (TreeNodeAdv adv in list)
            {
                _view.FillListBox(list);
            }
        }

        public string GetDesignation()
        {
            return _view.Designation;
        }

        public void Run()
        {
            _view.Show();
        }

    }
}