using System.Runtime.InteropServices.WindowsRuntime;

namespace NavisElectronics.ListOfCooperation.ViewInterfaces
{
    using System;
    using Aga.Controls.Tree;
    using EventArguments;
    using ViewModels;

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

        /// <summary>
        /// Очистка данных в панели "Вывод"
        /// </summary>
        void ClearOutput();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        void AppendError(ErrorElement element);

        CooperationNode GetMainNode();

        TreeViewAdv GetTreeView();
        void JumpToNode(TreeNodeAdv cooperationNode);
    }
}