using System;

namespace NavisElectronics.ListOfCooperation.EventArguments
{
    public class RemoveNodeEventArgs:EventArgs
    {
        private int indexToRemove;

        public RemoveNodeEventArgs(int indexToRemove)
        {
            this.indexToRemove = indexToRemove;
        }

        public int IndexToRemove
        {
            get { return indexToRemove; }
        }
    }
}