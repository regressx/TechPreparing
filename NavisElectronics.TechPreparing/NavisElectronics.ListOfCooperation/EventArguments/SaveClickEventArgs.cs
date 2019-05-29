namespace NavisElectronics.ListOfCooperation.EventArguments
{
    using ViewModels;

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