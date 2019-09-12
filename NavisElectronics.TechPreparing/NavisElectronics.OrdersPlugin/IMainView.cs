using Aga.Controls.Tree;

namespace NavisElectronics.Orders
{
    public interface IMainView : IView
    {
        void UpdateTreeModel(TreeModel treeModel);
    }
}