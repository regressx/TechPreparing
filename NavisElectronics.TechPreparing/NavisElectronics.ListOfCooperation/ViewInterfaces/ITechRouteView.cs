using Aga.Controls.Tree;

namespace NavisElectronics.ListOfCooperation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Entities;
    using EventArguments;

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