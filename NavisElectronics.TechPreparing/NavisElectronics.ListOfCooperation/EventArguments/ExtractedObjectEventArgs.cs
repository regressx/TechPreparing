﻿namespace NavisElectronics.TechPreparation.EventArguments
{
    using System;

    using NavisElectronics.ListOfCooperation.Entities;

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