using System.Collections.Generic;
using System.IO;
using System.Xml;
using Aga.Controls.Tree;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.Services;

namespace NavisElectronics.ListOfCooperation.ViewModels
{
    /// <summary>
    /// Реализация модели для окна модуля Кооперации
    /// </summary>
    public class CooperationViewModel
    {
        private readonly OpenFolderService _openFolderService;
        private readonly IntermechTreeElement _oldElement;
        private readonly IntermechTreeElement _newElement;

        public CooperationViewModel(OpenFolderService openFolderService, IntermechTreeElement oldElement, IntermechTreeElement newElement)
        {
            _openFolderService = openFolderService;
            _oldElement = oldElement;
            _newElement = newElement;
        }

        /// <summary>
        /// Метод получения модели из входных данных
        /// </summary>
        /// <param name="element"> Элемент дерева, полученный из базы данных</param>
        /// <returns>Модель дерева для отображения</returns>
        public TreeModel GetModel(IntermechTreeElement element)
        {
            CooperationNode mainNode = new CooperationNode();
            mainNode.Designation = element.Designation;
            mainNode.Name = element.Name;
            mainNode.Amount = element.Amount.ToString();
            mainNode.Note = element.RouteNote;
            mainNode.CooperationFlag = element.CooperationFlag;
            mainNode.Tag = element;
            BuildNodeRecursive(mainNode, element);
            TreeModel model = new TreeModel();
            model.Nodes.Add(mainNode);
            return model;
        }

        /// <summary>
        /// Получение составного узла рекурсивно
        /// </summary>
        /// <param name="mainNode">
        /// Главный узел
        /// </param>
        /// <param name="element">
        /// Элемент, из которого получаем данные
        /// </param>
        private void BuildNodeRecursive(CooperationNode mainNode, IntermechTreeElement element)
        {
            if (element.Children.Count > 0)
            {
                foreach (IntermechTreeElement child in element.Children)
                {
                    CooperationNode childNode = new CooperationNode();
                    childNode.Id = child.Id;
                    childNode.Type = child.Type;
                    childNode.Designation = child.Designation;
                    childNode.Name = child.Name;
                    childNode.Amount = child.Amount.ToString("F3");
                    childNode.AmountWithUse = child.AmountWithUse.ToString("F3");
                    childNode.TotalAmount = child.TotalAmount.ToString("F3");
                    childNode.StockRate = child.StockRate.ToString("F3");
                    childNode.SampleSize = child.SampleSize;
                    childNode.TechProcessReference = child.TechProcessReference;
                    childNode.Note = child.Note;
                    childNode.SubstituteInfo = child.SubstituteInfo;
                    childNode.CooperationFlag = child.CooperationFlag;
                    childNode.IsPcb = child.IsPCB;
                    childNode.PcbVersion = child.PcbVersion;
                    childNode.TechTask = child.TechTask;
                    childNode.Tag = child;
                    mainNode.Nodes.Add(childNode);
                    BuildNodeRecursive(childNode, child);
                }
            }
        }

        /// <summary>
        /// Проверка простановки кооперации и различных атрибутов: что есть ссылка на тех. процесс, заполнены выборка и коэф. запаса
        /// </summary>
        /// <param name="treeElement"></param>
        /// <exception cref="FileNotFoundException"></exception>
        public void CheckReady(CooperationNode treeElement)
        {
            ICollection<string> dependencies = new List<string>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Designations.xml");

                XmlElement mainXmlElement = xmlDoc.DocumentElement;

                foreach (XmlNode node in mainXmlElement.ChildNodes)
                {
                    dependencies.Add(node.Attributes["BeginWith"].Value);
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Не найден файл Designations.xml");
            }

            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            queue.Enqueue(treeElement);
            while (queue.Count > 0)
            {
                CooperationNode elementFromQueue = queue.Dequeue();
                elementFromQueue.Error = false;
                if (elementFromQueue.Designation != null)
                {
                    string cuttedDesignation = CutTheDesignation(elementFromQueue.Designation);
                    foreach (string s in dependencies)
                    {
                        if (cuttedDesignation.StartsWith(s))
                        {
                            if (!elementFromQueue.CooperationFlag)
                            {
                                elementFromQueue.Error = true;
                            }
                            break;
                        }
                    }
                }

                if (elementFromQueue.CooperationFlag)
                {
                    if (elementFromQueue.SampleSize == string.Empty)
                    {
                        elementFromQueue.Error = true;
                    }

                    if (elementFromQueue.StockRate == string.Empty)
                    {
                        elementFromQueue.Error = true;
                    }
                    else
                    {
                        if (elementFromQueue.StockRate.Contains("-"))
                        {
                            elementFromQueue.Error = true;
                        }
                    }

                    if (elementFromQueue.TechProcessReference == null)
                    {
                        elementFromQueue.Error = true;
                    }
                }

                if (elementFromQueue.Nodes.Count > 0)
                {
                    foreach (Node child in elementFromQueue.Nodes)
                    {
                        queue.Enqueue((CooperationNode)child);
                    }
                }
            }
        }

        /// <summary>
        /// Режет строку до первой точки
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string CutTheDesignation(string str)
        {
            int firstDotIndex = str.IndexOf('.');
            if (firstDotIndex > -1)
            {
                return str.Substring(firstDotIndex + 1);
            }
            return str;
        }

    }
}