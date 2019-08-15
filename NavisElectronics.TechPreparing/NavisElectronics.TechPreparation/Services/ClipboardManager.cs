using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Класс, повторяющий работу буфера обмена, но только в пределах нашего приложения
    /// </summary>
    public class ClipboardManager
    {
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
        /// <param name="agentFilter">Фильтр по изготовителю</param>
        public void Copy(ICollection<MyNode> nodes, string agentFilter)
        {
            IList<TechRouteClipBoard> clipboardList = new List<TechRouteClipBoard>();
            foreach (MyNode node in nodes)
            {
                TechRouteClipBoard clipBoard = new TechRouteClipBoard();
                clipBoard.ForView = node.Route;
                IntermechTreeElement element = node.Tag as IntermechTreeElement;
                TechAgentDataExtractor extractor = new TechAgentDataExtractor();
                string temp = extractor.ExtractData(element.TechRoute, agentFilter);
                clipBoard.ForDatabase = string.Format("<{0}:{1}/>", agentFilter, temp);
                clipboardList.Add(clipBoard);
            }

            _currentClipboardData = clipboardList;
        }

        /// <summary>
        /// Метод вставки из буфера обмена
        /// </summary>
        /// <param name="nodes">Узлы, куда вставляем данные</param>
        /// <param name="agentFilter">Фильтр по изготовителю</param>
        public void Paste(ICollection<MyNode> nodes, string agentFilter)
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
                    node.Route = clipBoardFromList.ForView;
                    IntermechTreeElement element = node.Tag as IntermechTreeElement;
                    TechAgentDataExtractor extractor = new TechAgentDataExtractor();
                    string temp = string.Empty;
                    if (element.TechRoute != null)
                    {
                        temp = extractor.RemoveData(element.TechRoute, agentFilter);
                    }
                    element.TechRoute = string.Format("{0}{1}", temp, clipBoardFromList.ForDatabase);
                    i++;
                }
            }
        }

    }
}