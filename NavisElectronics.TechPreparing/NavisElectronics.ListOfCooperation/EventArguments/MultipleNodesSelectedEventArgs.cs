using System;
using System.Collections.Generic;
using Aga.Controls.Tree;
using NavisElectronics.ListOfCooperation.ViewModels;

namespace NavisElectronics.ListOfCooperation.EventArguments
{
    public class MultipleNodesSelectedEventArgs: EventArgs
    {
        private ICollection<CooperationNode> _nodes;

        public MultipleNodesSelectedEventArgs(ICollection<CooperationNode> nodes)
        {
            _nodes = nodes;
        }

        public ICollection<CooperationNode> SelectedNodes
        {
            get { return _nodes; }
        }

    }
}