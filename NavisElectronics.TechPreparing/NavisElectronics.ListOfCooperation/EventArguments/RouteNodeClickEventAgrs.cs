using System;
using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.ListOfCooperation.EventArguments
{
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