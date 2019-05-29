using System;
using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.ListOfCooperation.EventArguments
{
    public class TreeNodeAgentValueEventArgs: EventArgs
    {
        private IntermechTreeElement _treeElement;
        private readonly string _key;


        public TreeNodeAgentValueEventArgs(IntermechTreeElement treeElement, string key)
        {
            _treeElement = treeElement;
            _key = key;
        }

        public IntermechTreeElement TreeElement
        {
            get { return _treeElement; }
        }

        public string Key
        {
            get { return _key; }
        }
    }
}
