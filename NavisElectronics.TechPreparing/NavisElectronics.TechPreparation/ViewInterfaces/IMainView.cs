namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.EventArguments;

    public interface IMainView
    {
        event EventHandler NavisElectronicsFilterClick;
        event EventHandler KbNavisFilterClick;
        event EventHandler Load;
        event EventHandler<TreeNodeMouseClickEventArgs> NodeMouseClick;
        event EventHandler<TreeNodeAgentValueEventArgs> CellValueChanged;
        event EventHandler ApplyButtonClick;
        event EventHandler ClearCooperationClick;
        event EventHandler EditTechRoutesClick;
        event EventHandler UpdateClick;
        event EventHandler EditMainMaterialsClick;
        event EventHandler EditStandartDetailsClick;
        event EventHandler LoadPreparationClick;
        event EventHandler EditWithdrawalTypeClick;
        event EventHandler RefreshClick;
        event EventHandler CheckAllReadyClick;

        void Show();
        void FillTree(TreeNode mainNode);
        void FillGridColumns(ICollection<Agent> agents);
        void FillAgent(string agentId);
        TreeNode GetMainTreeElement();
        string GetNote();
        void FillNote(string orderElementNote);


        void UnLockButtons();
        void LockButtons();
    }
}