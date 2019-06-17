// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveNodeEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the RemoveNodeEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



namespace NavisElectronics.TechPreparation.EventArguments
{
    using System;

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