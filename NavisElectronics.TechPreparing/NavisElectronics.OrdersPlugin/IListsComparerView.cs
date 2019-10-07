using System;

namespace NavisElectronics.Orders
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Интерфейс окна 
    /// </summary>
    public interface IListsComparerView : IView
    {
        /// <summary>
        /// Заполнить грид
        /// </summary>
        /// <param name="firstDictionaryValues">
        /// Значения из словаря
        /// </param>
        void FillFirstGrid(IList<ReportElement> firstDictionaryValues);


        /// <summary>
        /// Заполнить грид
        /// </summary>
        /// <param name="secondDictionaryValues">
        /// Значения из словаря
        /// </param>
        void FillSecondGrid(IList<ReportElement> secondDictionary);

        /// <summary>
        /// The load csv.
        /// </summary>
        event EventHandler LoadCsv;
        event EventHandler StartComparing;
    }
}