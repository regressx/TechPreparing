﻿using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.ListOfCooperation.ViewModels
{
    public class ErrorElement
    {
        private readonly string _message;
        private CooperationNode _treeElement;

        public ErrorElement(string message, CooperationNode treeElement)
        {
            _message = message;
            _treeElement = treeElement;
        }


        public string Message
        {
            get { return _message; }
        }

        public CooperationNode TreeElement
        {
            get { return _treeElement; }

        }
    }
}