using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.EventArguments
{
    using System;

    /// <summary>
    /// Аргумент для события по очистке данных о кооперации
    /// </summary>
    public class BoundTreeElementEventArgs : EventArgs
    {
        /// <summary>
        /// Элемент, который передаем классу
        /// </summary>
        private IntermechTreeElement _element;

        /// <summary>
        /// Индекс строки
        /// </summary>
        private readonly int _rowIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoundTreeElementEventArgs"/> class.
        /// </summary>
        /// <param name="element">
        /// Элемент, в котором хотим убрать кооперацию
        /// </param>
        /// <param name="rowIndex">
        /// The row Index.
        /// </param>
        public BoundTreeElementEventArgs(IntermechTreeElement element, int rowIndex)
        {
            _element = element;
            _rowIndex = rowIndex;
        }


        /// <summary>
        /// Возвращает элемент для удаления в нем кооперации
        /// </summary>
        public IntermechTreeElement Element
        {
            get { return _element; }
        }

        /// <summary>
        /// Получить индекс строки
        /// </summary>
        public int RowIndex
        {
            get { return _rowIndex; }
        }
    }
}