namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.ViewModels;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Интерфейс формы работы с кооперацией
    /// </summary>
    public interface ICooperationView
    {
        event EventHandler SaveClick;
        event EventHandler Load;
        event EventHandler<MultipleNodesSelectedEventArgs> SetCooperationClick;
        event EventHandler<MultipleNodesSelectedEventArgs> DeleteCooperationClick;
        event EventHandler<MultipleNodesSelectedEventArgs> SetTechProcessReferenceClick;
        event EventHandler<MultipleNodesSelectedEventArgs> DeleteTechProcessReferenceClick;
        event EventHandler<MultipleNodesSelectedEventArgs> SetNoteClick;
        event EventHandler<MultipleNodesSelectedEventArgs> DeleteNoteClick;
        event EventHandler CheckListOfCooperation;
        event EventHandler<MultipleNodesSelectedEventArgs> SetParametersClick;
        event EventHandler PutDownCooperation;
        event EventHandler<MultipleNodesSelectedEventArgs> SearchInArchiveClick;
        event EventHandler<MultipleNodesSelectedEventArgs> FindInTreeClick;
        event EventHandler GlobalSearchClick;
        event EventHandler<MultipleNodesSelectedEventArgs> CreateCooperationClick;
        event EventHandler<MultipleNodesSelectedEventArgs> SetTechTaskClick;
        event EventHandler<MultipleNodesSelectedEventArgs> SetPcbClick;
        event EventHandler<MultipleNodesSelectedEventArgs> CreateCompleteListClick;

        event EventHandler ExpandAllNodesClick;
        event EventHandler CollapseAllNodesClick;
        /// <summary>
        /// Заполнить дерево данными
        /// </summary>
        /// <param name="model"></param>
        void FillTree(TreeModel model);

        /// <summary>
        /// The show.
        /// </summary>
        void Show();

        CooperationNode GetMainNode();

        TreeViewAdv GetTreeView();
        void JumpToNode(TreeNodeAdv cooperationNode);
    }
}