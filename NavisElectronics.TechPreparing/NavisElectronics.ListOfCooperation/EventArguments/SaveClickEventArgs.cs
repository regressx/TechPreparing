namespace NavisElectronics.TechPreparation.EventArguments
{
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    public class SaveClickEventArgs
    {
        private MyNode _node;

        public SaveClickEventArgs(MyNode node)
        {
            _node = node;
        }

        public MyNode Node
        {
            get { return _node; }
        }
    }
}