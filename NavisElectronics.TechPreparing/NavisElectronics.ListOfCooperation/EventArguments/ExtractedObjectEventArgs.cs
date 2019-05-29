using System;
using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.ListOfCooperation.EventArguments
{
    public class ExtractedObjectEventArgs:EventArgs
    {
        private ExtractedObject _exctractedObject;
        private int _rowIndex;

        public ExtractedObjectEventArgs(ExtractedObject exctractedObject, int rowIndex)
        {
            _exctractedObject = exctractedObject;
            _rowIndex = rowIndex;
        }

        public ExtractedObject ExtractedObject
        {
            get { return _exctractedObject; }
        }

        public int RowIndex
        {
            get { return _rowIndex; }
        }
    }
}