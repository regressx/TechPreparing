namespace NavisElectronics.TechPreparation.EventArguments
{
    using System;
    using System.Collections.Generic;

    using NavisElectronics.ListOfCooperation.Entities;

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