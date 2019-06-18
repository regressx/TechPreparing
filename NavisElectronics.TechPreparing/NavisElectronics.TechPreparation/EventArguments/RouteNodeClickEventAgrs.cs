// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteNodeClickEventAgrs.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the RouteNodeClickEventAgrs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.EventArguments
{
    using System;

    using NavisElectronics.TechPreparation.Entities;

    public class RouteNodeClickEventAgrs : EventArgs
    {
        private readonly TechRouteNode _node;

        public RouteNodeClickEventAgrs(TechRouteNode node)
        {
            _node = node;
        }

        public TechRouteNode Node
        {
            get { return _node; }
        }
    }
}