namespace NavisElectronics.TechPreparation.ViewInterfaces
{
    using System;
    using System.Collections.Generic;
    using Aga.Controls.Tree;
    using EventArguments;
    using Interfaces.Entities;

    /// <summary>
    /// Интерфейс для окна редактирования тех. маршрутов
    /// </summary>
    public interface ITechRouteView : IView
    {
        /// <summary>
        /// The load.
        /// </summary>
        event EventHandler Load;

        /// <summary>
        /// The route node click.
        /// </summary>
        event EventHandler<RouteNodeClickEventAgrs> RouteNodeClick;

        /// <summary>
        /// The remove node click.
        /// </summary>
        event EventHandler<RemoveNodeEventArgs> RemoveNodeClick;

        /// <summary>
        /// Заполнить структуру организации
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        void FillWorkShop(TreeModel model);

        /// <summary>
        /// заполнить список узлов
        /// </summary>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        void FillListBox(IList<TechRouteNode> nodes);

        /// <summary>
        /// Заполнить текстбокс текстом из узлов
        /// </summary>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        void FillTextBox(IList<TechRouteNode> nodes);



    }
}