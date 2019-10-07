using System;
using Aga.Controls.Tree;

namespace NavisElectronics.Orders
{
    public interface IMainView : IView
    {
        event EventHandler StartChecking;
        event EventHandler AbortLoading;
        void UpdateTreeModel(TreeModel treeModel);
    }
}