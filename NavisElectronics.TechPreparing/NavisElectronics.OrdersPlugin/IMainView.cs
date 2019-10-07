using System;
using Aga.Controls.Tree;
using NavisElectronics.Orders.EventArguments;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders
{
    public interface IMainView : IView
    {
        event EventHandler StartChecking;
        event EventHandler AbortLoading;
        event EventHandler<ProduceEventArgs> DoNotProduceClick; 
        void UpdateTreeModel(TreeModel treeModel);
    }
}