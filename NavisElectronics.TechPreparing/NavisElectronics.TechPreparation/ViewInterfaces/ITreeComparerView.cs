using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using Interfaces.Entities;
    using ViewModels.TreeNodes;

    /// <summary>
    /// Интерфейс представления сравнения двух деревьев
    /// </summary>
    public interface ITreeComparerView
    {
        event EventHandler Load;
        event EventHandler Download;
        event EventHandler Compare;
        event EventHandler PushChanges;
        event EventHandler<ComparerNode> DeleteNodeClick;
        event EventHandler<IntermechTreeElement> EditCooperationClick;
        event EventHandler<IntermechTreeElement> EditTechRoutesClick;
        event EventHandler<ComparerNode> JumpInit;
        event EventHandler<ComparerNode> FindInOldArchive;
        event EventHandler<CompareTwoNodesEventArgs> CompareTwoNodesClick;

        void FillOldTree(TreeModel mainElement);
        void FillNewTree(TreeModel model);

        void LockButtons();
        void UnlockButtons();

        DialogResult ShowDialog();


        ComparerNode GetOldNode();
        ComparerNode GetNewNode();
        void Show();
        void ExpandFirstTree();
        void ExpandSecondTree();
        void JumpToNode(object sender, ComparerNode nodeToFind);
        TreeViewAdv GetNewTree();
        TreeViewAdv GetOldTree();
        ICollection<ComparerNode> GetSelectedNodesInRightTree();
    }
}