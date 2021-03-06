﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMainView.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the IMainView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Aga.Controls.Tree;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Presenters;
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
    public interface IDividingListView
    {
        event EventHandler Load;
        void Close();

        event EventHandler<TreeNodeClickEventArgs> NodeMouseClick;
        event EventHandler<TreeNodeAgentValueEventArgs> CellValueChanged;

        event EventHandler ApplyButtonClick;
        event EventHandler<BoundTreeElementEventArgs> ClearManufacturerClick;

        event EventHandler UpdateClick;

        event EventHandler EditMainMaterialsClick;

        event EventHandler EditStandartDetailsClick;

        event EventHandler RefreshClick;


        void UpdateLabelText(string message);
        void UpdateProgressBar(int progressReportPercent);
        void Show();
        void FillTree(TreeModel mainNode);
        void FillGrid(ICollection<Agent> agents);
        void UpdateAgent(string agentsInfo);
        string GetNote();
        void FillNote(string orderElementNote);

        void UpdateCaptionText(string orderName);

    }
}