using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Windows.Forms;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    public interface ITreeComparerView
    {
        event EventHandler Load;
        event EventHandler Upload;
        event EventHandler Download;
        event EventHandler Compare;
        event EventHandler<IntermechTreeElement> PushChanges;
        event EventHandler<IntermechTreeElement> EditCooperationClick;
        event EventHandler<IntermechTreeElement> EditTechRoutesClick;
        event EventHandler<ComparerNode> EditAmount;
        event EventHandler<ComparerNode> JumpInit;
        event EventHandler<ComparerNode> FindInOldArchive;

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
    }
}