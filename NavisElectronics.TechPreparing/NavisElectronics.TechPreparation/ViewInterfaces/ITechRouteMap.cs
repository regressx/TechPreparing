namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    public interface ITechRouteMap
    {
        event EventHandler<SaveClickEventArgs> GoToOldArchive;
        event EventHandler<EditTechRouteEventArgs> EditTechRouteClick;
        event EventHandler Load;
        event EventHandler<SaveClickEventArgs> EditNoteClick;
        event EventHandler<ClipboardEventArgs> CopyClick;
        event EventHandler<ClipboardEventArgs> PasteClick;
        event EventHandler<SaveClickEventArgs> ShowClick;
        event EventHandler CreateReportClick;
        event EventHandler CreateDevideList;
        event EventHandler SetNodesToComplectClick;
        event EventHandler CreateCooperationList;


        event EventHandler<ClipboardEventArgs> SetInnerCooperation;
        event EventHandler<ClipboardEventArgs> RemoveInnerCooperation;

        void Show();
        void SetTreeModel(TreeModel treeModel);
        ICollection<MyNode> GetSelectedRows();
        MyNode GetMainNode();
    }
}