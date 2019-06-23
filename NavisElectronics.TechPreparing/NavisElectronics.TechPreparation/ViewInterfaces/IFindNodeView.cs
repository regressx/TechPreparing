namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Интерфейс для окна поиска по обозначению
    /// </summary>
    public interface IFindNodeView
    {
        event EventHandler FindButtonClick;
        event EventHandler<CooperationNode> NodeClick;
        event FormClosingEventHandler FormClosing;
        string Designation { get;}

        IList<TreeNodeAdv> Nodes { get; set; }
        void Show();
        void FillListBox(IList<TreeNodeAdv> list);
    }
}