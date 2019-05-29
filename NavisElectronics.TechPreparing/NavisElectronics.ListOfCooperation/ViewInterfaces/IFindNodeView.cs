namespace NavisElectronics.ListOfCooperation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Aga.Controls.Tree;

    /// <summary>
    /// Интерфейс для окна поиска по обозначению
    /// </summary>
    public interface IFindNodeView
    {
        event EventHandler FindButtonClick;
        event EventHandler<TreeNodeAdv> NodeClick;
        event FormClosingEventHandler FormClosing;
        string Designation { get;}

        IList<TreeNodeAdv> Nodes { get; set; }
        void Show();
        void FillListBox(IList<TreeNodeAdv> list);
    }
}