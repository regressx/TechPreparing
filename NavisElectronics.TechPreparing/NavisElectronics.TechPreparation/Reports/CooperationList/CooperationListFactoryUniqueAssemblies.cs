// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationListFactoryUniqueAssemblies.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the CooperationListFactoryUniqueAssemblies type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Reports.CooperationList
{
    using System;
    using System.Collections.Generic;
    using Aga.Controls.Tree;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    public class CooperationListFactoryUniqueAssemblies
    {
        /// <summary>
        /// Узел, для которого делается ведомость кооперации
        /// </summary>
        private readonly MyNode _mainNode;

        /// <summary>
        /// Опять же узел, для которого делается ведомость кооперации, только это уже прикрепленный сырой элемент
        /// </summary>
        private readonly IntermechTreeElement _root;


        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationListFactoryInDepth"/> class.
        /// </summary>
        /// <param name="mainNode">
        /// Выбранный Вами узел
        /// </param>
        public CooperationListFactoryUniqueAssemblies(MyNode mainNode)
        {
            _mainNode = mainNode;
            _root = mainNode.Tag as IntermechTreeElement;
        }

        /// <summary>
        /// Метод создания отчета
        /// </summary>
        /// <returns>
        /// The <see cref="CooperationListDocument"/>.
        /// </returns>
        public CooperationListDocument Create()
        {
            // заполнить словарик уникальных элементов, делающихся по кооперации
            IDictionary<long, ExtractedObject> uniqueAssembliesContainsCooperationNodes = new Dictionary<long, ExtractedObject>();

            // Очередь для прохода в ширину
            Queue<MyNode> queue = new Queue<MyNode>();
            queue.Enqueue(_mainNode);
            while (queue.Count > 0)
            {
                MyNode nodeFromQueue = queue.Dequeue();
                ExtractedObject assemblyWithCooperation = null;
                
                if (uniqueAssembliesContainsCooperationNodes.ContainsKey(nodeFromQueue.Id))
                {
                    assemblyWithCooperation =
                        uniqueAssembliesContainsCooperationNodes[nodeFromQueue.Id];
                    assemblyWithCooperation.AmountWithUse += ((IntermechTreeElement)nodeFromQueue.Tag).AmountWithUse;
                }

                foreach (Node node in nodeFromQueue.Nodes)
                {
                    MyNode myNode = (MyNode)node;

                    if (myNode.Type == 1078 || myNode.Type == 1074 || myNode.Type == 1097 || myNode.Type == 1159 || myNode.Type == 1052)
                    {
                        if (myNode.CooperationFlag && !myNode.IsPcb)
                        {
                            ExtractedObject currentObject = new ExtractedObject();
                            currentObject.Id = myNode.Id;
                            currentObject.Type = myNode.Type;
                            currentObject.Name = myNode.Name;
                            currentObject.Designation = myNode.Designation;
                            currentObject.CooperationFlag = myNode.CooperationFlag;
                            currentObject.IsPcb = myNode.IsPcb;
                            currentObject.PcbVersion = myNode.PcbVersion.ToString();
                            currentObject.MeasureUnits = ((IntermechTreeElement)myNode.Tag).MeasureUnits;
                            currentObject.TechProcessReference = myNode.TechProcessReference;
                            currentObject.StockRate = myNode.StockRate;
                            currentObject.SampleSize = ((IntermechTreeElement) myNode.Tag).SampleSize;
                            currentObject.Amount = Convert.ToDouble(myNode.Amount);

                            if (!uniqueAssembliesContainsCooperationNodes.ContainsKey(nodeFromQueue.Id))
                            {
                                assemblyWithCooperation = new ExtractedObject();
                                assemblyWithCooperation.Id = nodeFromQueue.Id;
                                assemblyWithCooperation.Name = nodeFromQueue.Name;
                                assemblyWithCooperation.Designation = nodeFromQueue.Designation;
                                assemblyWithCooperation.AmountWithUse += ((IntermechTreeElement)nodeFromQueue.Tag).AmountWithUse;
                                uniqueAssembliesContainsCooperationNodes.Add(nodeFromQueue.Id, assemblyWithCooperation);
                            }

                            assemblyWithCooperation.RegisterElement(currentObject);
                        }
                    }
                }



                if (nodeFromQueue.Type == 1078 || nodeFromQueue.Type == 1074 || nodeFromQueue.Type == 1097)
                {
                    foreach (var node in nodeFromQueue.Nodes)
                    {
                        var child = (MyNode)node;

                        if (!child.CooperationFlag)
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
            }


            // заполнить словарик уникальных элементов, делающихся по кооперации
            IDictionary<long, ExtractedObject> uniqueCooperationElements = new Dictionary<long, ExtractedObject>();

            // Очередь для прохода в ширину
            Queue<MyNode> myQueue = new Queue<MyNode>();
            myQueue.Enqueue(_mainNode);
            while (myQueue.Count > 0)
            {
                MyNode nodeFromQueue = myQueue.Dequeue();

                if (nodeFromQueue.Type == 1078 || nodeFromQueue.Type == 1074 || nodeFromQueue.Type == 1159 ||
                    nodeFromQueue.Type == 1052 || nodeFromQueue.Type == 1097)
                {
                    if (nodeFromQueue.CooperationFlag && nodeFromQueue.IsPcb)
                    {
                        if (!uniqueCooperationElements.ContainsKey(nodeFromQueue.Id))
                        {
                            ExtractedObject currentObject = new ExtractedObject();
                            currentObject.Id = nodeFromQueue.Id;
                            currentObject.Type = nodeFromQueue.Type;
                            currentObject.Name = nodeFromQueue.Name;
                            currentObject.Designation = nodeFromQueue.Designation;
                            currentObject.CooperationFlag = nodeFromQueue.CooperationFlag;
                            currentObject.IsPcb = nodeFromQueue.IsPcb;
                            currentObject.PcbVersion = nodeFromQueue.PcbVersion.ToString();
                            currentObject.MeasureUnits = ((IntermechTreeElement)nodeFromQueue.Tag).MeasureUnits;
                            currentObject.TechProcessReference = nodeFromQueue.TechProcessReference;
                            currentObject.StockRate = nodeFromQueue.StockRate;
                            currentObject.SampleSize = ((IntermechTreeElement)nodeFromQueue.Tag).SampleSize;
                            currentObject.TechTask = ((IntermechTreeElement)nodeFromQueue.Tag).TechTask;


                            uniqueCooperationElements.Add(nodeFromQueue.Id, currentObject);
                            IList<IntermechTreeElement> elementsInTree = _root.Find(nodeFromQueue.Id);

                            foreach (IntermechTreeElement intermechTreeElement in elementsInTree)
                            {
                                if (intermechTreeElement.Parent.CooperationFlag == false)
                                {
                                    ExtractedObject parentReportElement = new ExtractedObject();
                                    parentReportElement.Type = intermechTreeElement.Parent.Type;
                                    parentReportElement.Designation = intermechTreeElement.Parent.Designation;
                                    parentReportElement.Name = intermechTreeElement.Parent.Name;
                                    parentReportElement.Id = intermechTreeElement.Parent.Id;
                                    parentReportElement.Amount = intermechTreeElement.Amount;
                                    parentReportElement.AmountWithUse = intermechTreeElement.AmountWithUse;
                                    currentObject.RegisterElement(parentReportElement);
                                }
                            }
                        }
                    }
                }

                // все потомки кооперационных сборок и деталей не должны попасть в очередь
                if (!nodeFromQueue.CooperationFlag)
                {
                    if (nodeFromQueue.Nodes.Count > 0)
                    {
                        foreach (var node in nodeFromQueue.Nodes)
                        {
                            var child = (MyNode)node;
                            myQueue.Enqueue(child);
                        }
                    }
                }
            }

            CooperationListDocument document = new CooperationListDocument();

            document.SetCommonObjects(uniqueAssembliesContainsCooperationNodes.Values);
            document.SetPcbObjects(uniqueCooperationElements.Values);

            return document;

        }
    }
}