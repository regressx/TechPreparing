using System;
using Aga.Controls.Tree;
using NavisElectronics.Orders.Enums;
using NavisElectronics.Orders.EventArguments;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.Orders
{
    /// <summary>
    /// The MainView interface.
    /// </summary>
    public interface IMainView : IView
    {
        event EventHandler DownloadAndUpdate;
        event EventHandler Save;
        event EventHandler StartChecking;
        event EventHandler AbortLoading;
        event EventHandler<ReportStyle> CreateReport;


        event EventHandler<ProduceEventArgs> SetProduceClick; 
        void UpdateTreeModel(IntermechTreeElement treeModel);
        void UpdateSaveLabel(string message);

    }
}