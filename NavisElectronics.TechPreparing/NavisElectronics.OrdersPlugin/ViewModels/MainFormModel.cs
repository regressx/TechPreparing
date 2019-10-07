using Aga.Controls.Tree;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders.ViewModels
{
    public class MainFormModel
    {
        public TreeModel GetTreeModel(IntermechTreeElement elementToView)
        {
            OrderNode root = new OrderNode();
            root.Amount = elementToView.Amount;
            root.AmountWithUse = elementToView.AmountWithUse;
            root.Name = elementToView.Name;
            root.Designation = elementToView.Designation;
            root.Tag = elementToView;
            GetOrderNodeRecursive(root, elementToView);
            TreeModel model = new TreeModel();
            model.Nodes.Add(root);
            return model;
        }

        private void GetOrderNodeRecursive(OrderNode root, IntermechTreeElement elementToView)
        {
            foreach (IntermechTreeElement child in elementToView.Children)
            {
                OrderNode node = new OrderNode();
                node.Amount = child.Amount;
                node.AmountWithUse = child.AmountWithUse;
                node.Name = child.Name;
                node.Designation = child.Designation;
                node.Tag = child;
                root.Nodes.Add(node);
                GetOrderNodeRecursive(node, child);
            }
        }

    }
}