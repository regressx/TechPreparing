using System.Collections.Generic;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

namespace NavisElectronics.TechPreparation.Services
{
    /// <summary>
    /// Класс, повторяющий работу буфера обмена, но только в пределах нашего приложения
    /// </summary>
    public class ClipboardManager
    {
        /// <summary>
        /// The _current clipboard data.
        /// </summary>
        private IList<TechRouteClipBoard> _currentClipboardData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClipboardManager"/> class.
        /// </summary>
        public ClipboardManager()
        {
            _currentClipboardData = new List<TechRouteClipBoard>();
        }

        /// <summary>
        /// Метод копирования элементов в буфере обмена
        /// </summary>
        /// <param name="nodes">Узлы, которые копируем данные</param>
        public void Copy(ICollection<MyNode> nodes)
        {
            IList<TechRouteClipBoard> clipboardList = new List<TechRouteClipBoard>();
            foreach (MyNode node in nodes)
            {
                TechRouteClipBoard clipBoard = new TechRouteClipBoard();
                clipBoard.RouteForView = node.Route;
                IntermechTreeElement element = node.Tag as IntermechTreeElement;
                clipBoard.RouteForDatabase = element.TechRoute;
                clipBoard.Note = element.RouteNote;
                clipboardList.Add(clipBoard);
            }

            _currentClipboardData = clipboardList;
        }

        /// <summary>
        /// Метод вставки из буфера обмена
        /// </summary>
        /// <param name="nodes">Узлы, куда вставляем данные</param>
        public void Paste(ICollection<MyNode> nodes)
        {
            if (_currentClipboardData.Count > 0)
            {
                int i = 0;

                foreach (MyNode node in nodes)
                {
                    if (i == _currentClipboardData.Count)
                    {
                        break;
                    }

                    TechRouteClipBoard clipBoardFromList = _currentClipboardData[i];
                    node.Route = string.Empty;
                    node.Route = clipBoardFromList.RouteForView;
                    node.Note = clipBoardFromList.Note;
                    IntermechTreeElement element = (IntermechTreeElement)node.Tag;

                    element.TechRoute = string.Empty;
                    element.TechRoute = clipBoardFromList.RouteForDatabase;
                    element.RouteNote = clipBoardFromList.Note;
                    i++;
                }
            }
        }

    }
}