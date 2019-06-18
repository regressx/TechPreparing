﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationViewModel.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Реализация модели для окна модуля Кооперации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Services;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Реализация модели для окна модуля Кооперации
    /// </summary>
    public class CooperationViewModel
    {
        private readonly OpenFolderService _openFolderService;

        public CooperationViewModel(OpenFolderService openFolderService)
        {
            _openFolderService = openFolderService;
        }

        /// <summary>
        /// Метод получения модели из входных данных
        /// </summary>
        /// <param name="element"> Элемент дерева, полученный из базы данных</param>
        /// <returns>Модель дерева для отображения</returns>
        public TreeModel GetModel(IntermechTreeElement element, string whoIsMainInOrder, string agentFilter)
        {
            TreeModel model = new TreeModel();

            if (whoIsMainInOrder != agentFilter)
            {
                ICollection<IntermechTreeElement> cooperationElements = new List<IntermechTreeElement>();
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(element);
                while (queue.Count > 0)
                {
                    IntermechTreeElement elementFromQueue = queue.Dequeue();
                    if (elementFromQueue.Agent == agentFilter)
                    {
                        cooperationElements.Add(elementFromQueue);
                    }
                    else
                    {
                        
                        foreach (IntermechTreeElement child in elementFromQueue.Children)
                        {
                            queue.Enqueue(child);
                        }
                    }
                }

                foreach (IntermechTreeElement cooperationElement in cooperationElements)
                {
                    CooperationNode mainNode = new CooperationNode();
                    mainNode.Designation = element.Designation;
                    mainNode.Name = element.Name;
                    mainNode.Amount = element.Amount.ToString();
                    mainNode.Note = element.RouteNote;
                    mainNode.CooperationFlag = element.CooperationFlag;
                    mainNode.Tag = element;
                    BuildNodeRecursive(mainNode, cooperationElement, whoIsMainInOrder, agentFilter);
                    model.Nodes.Add(mainNode);
                }
            }
            else
            {
                CooperationNode mainNode = new CooperationNode();
                mainNode.Designation = element.Designation;
                mainNode.Name = element.Name;
                mainNode.Amount = element.Amount.ToString();
                mainNode.Note = element.RouteNote;
                mainNode.CooperationFlag = element.CooperationFlag;
                mainNode.Tag = element;
                
                // строим всё
                BuildNodeRecursive(mainNode, element, whoIsMainInOrder, agentFilter);
                model.Nodes.Add(mainNode);
            }



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
        private void BuildNodeRecursive(CooperationNode mainNode, IntermechTreeElement element, string whoIsMainInOrder, string agentFilter)
        {
            if (element.Children.Count > 0)
            {
                foreach (IntermechTreeElement child in element.Children)
                {
                    if (child.Type == 1138 || child.Type == 1105 || child.Type == 1128)
                    {
                        continue;
                    }

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
                    if (!child.Agent.Contains(agentFilter))
                    {
                        childNode.CooperationFlag = true;
                    }
                    childNode.IsPcb = child.IsPCB;
                    childNode.PcbVersion = child.PcbVersion;
                    childNode.TechTask = child.TechTask;
                    childNode.Tag = child;
                    mainNode.Nodes.Add(childNode);
                    BuildNodeRecursive(childNode, child, whoIsMainInOrder, agentFilter);
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
        /// <param name="designation">Обозначение</param>
        /// <returns></returns>
        private string CutTheDesignation(string designation)
        {
            int firstDotIndex = designation.IndexOf('.');
            if (firstDotIndex > -1)
            {
                return designation.Substring(firstDotIndex + 1);
            }
            return designation;
        }

    }
}