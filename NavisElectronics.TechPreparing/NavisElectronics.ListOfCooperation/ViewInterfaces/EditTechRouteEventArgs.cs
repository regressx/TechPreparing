using System;

namespace NavisElectronics.ListOfCooperation.ViewInterfaces
{
    /// <summary>
    /// Аргументы события редактирования тех. маршрута
    /// </summary>
    public class EditTechRouteEventArgs : EventArgs
    {
        /// <summary>
        /// Это поле означает, что идет добавление нового маршрута к уже существующему, либо идет создание нового
        /// </summary>
        private readonly bool _append;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTechRouteEventArgs"/> class.
        /// </summary>
        /// <param name="append">
        /// Это поле означает, что идет добавление нового маршрута к уже существующему, либо идет создание нового
        /// </param>
        public EditTechRouteEventArgs(bool append)
        {
            _append = append;
        }

        /// <summary>
        /// Это поле означает, что идет добавление нового маршрута к уже существующему, либо идет создание нового
        /// </summary>
        public bool Append
        {
            get { return _append; }
        }
    }
}