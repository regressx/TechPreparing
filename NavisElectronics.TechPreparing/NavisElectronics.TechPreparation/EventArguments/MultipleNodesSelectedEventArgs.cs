namespace NavisElectronics.TechPreparation.EventArguments
{
    using System;
    using System.Collections.Generic;

    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

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