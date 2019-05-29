using System;
using System.Collections.Generic;
using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.ListOfCooperation.EventArguments
{
    public class ExtractedObjectCollectionEventArgs:EventArgs
    {
        private ICollection<ExtractedObject> _exctractedObjectsCollection;

        public ExtractedObjectCollectionEventArgs(ICollection<ExtractedObject> exctractedObjectsCollection)
        {
            _exctractedObjectsCollection = exctractedObjectsCollection;
        }

        public ICollection<ExtractedObject> ExctractedObjectsCollection
        {
            get { return _exctractedObjectsCollection; }
        }
    }
}