namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.ViewModels;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Интерфейс формы работы с кооперацией
    /// </summary>
    public interface ICooperationView
    {
        event EventHandler Load;
        event EventHandler<MultipleNodesSelectedEventArgs> SetCooperationClick;
        event EventHandler<MultipleNodesSelectedEventArgs> DeleteCooperationClick;
        event EventHandler<MultipleNodesSelectedEventArgs> SetTechProcessReferenceClick;
        event EventHandler<MultipleNodesSelectedEventArgs> DeleteTechProcessReferenceClick;
        event EventHandler<MultipleNodesSelectedEventArgs> SetNoteClick;
        event EventHandler<MultipleNodesSelectedEventArgs> DeleteNoteClick;
        event EventHandler CheckListOfCooperation;
        event EventHandler<MultipleNodesSelectedEventArgs> SetParametersClick;
        event EventHandler<MultipleNodesSelectedEventArgs> SearchInArchiveClick;
        event EventHandler<MultipleNodesSelectedEventArgs> FindInTreeClick;
        event EventHandler GlobalSearchClick;
        event EventHandler<MultipleNodesSelectedEventArgs> SetTechTaskClick;

        /// <summary>
        /// Заполнить дерево данными
        /// </summary>
        /// <param name="model"></param>
        void FillTree(TreeModel model);

        /// <summary>
        /// The show.
        /// </summary>
        void Show();

        TreeViewAdv GetTreeView();

        ICollection<CooperationNode> GetMainNodes();
    }
}