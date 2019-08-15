using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.EventArguments;

    /// <summary>
    /// Интерфейс для окна редактирования тех. маршрутов
    /// </summary>
    public interface ITechRouteView
    {
        event EventHandler Load;
        event EventHandler<RouteNodeClickEventAgrs> RouteNodeClick;
        event EventHandler<RemoveNodeEventArgs> RemoveNodeClick;
        DialogResult ShowDialog();
        void FillWorkShop(TreeModel model);
        void FillListBox(IList<TechRouteNode> nodes);
        void FillTextBox(IList<TechRouteNode> nodes);



    }
}