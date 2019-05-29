using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.EventArguments;

namespace NavisElectronics.ListOfCooperation
{
    /// <summary>
    /// Интерфейс представления для всех таблиц
    /// </summary>
    public interface ITableView
    {
        event EventHandler Load;
        event EventHandler<ExtractedObjectCollectionEventArgs> EditClick;
        DialogResult ShowDialog();
        void Show();
        void FillGrid(IList<ExtractedObject> elements);
        void SetFormCaption(string format);
    }
}