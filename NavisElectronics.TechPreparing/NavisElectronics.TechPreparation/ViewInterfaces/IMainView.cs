// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMainView.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the IMainView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Aga.Controls.Tree;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Entities;
    using EventArguments;

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
        void UpdateLabelText(string message);
        void UpdateProgressBar(int progressReportPercent);
        void Show();
        void FillTree(TreeModel mainNode);
        void FillGridColumns(ICollection<Agent> agents);
        void FillAgent(string agentId);
        string GetNote();
        void FillNote(string orderElementNote);
        void UnLockButtons();
        void LockButtons();
        void UpdateCaptionText(string orderName);
    }
}