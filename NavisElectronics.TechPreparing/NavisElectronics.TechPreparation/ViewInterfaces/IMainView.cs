// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMainView.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the IMainView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.EventArguments;

    /// <summary>
    /// Интерфейс главной формы
    /// </summary>
    public interface IMainView
    {
        event EventHandler CooperationClick;
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