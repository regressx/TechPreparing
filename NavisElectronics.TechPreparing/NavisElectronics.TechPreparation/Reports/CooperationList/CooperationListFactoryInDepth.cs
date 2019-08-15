// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationListFactoryInDepth.cs" company="">
//   
// </copyright>
// <summary>
//   Этот класс позволяет составить закупочную ведомость, так сказать, в глубину. Все элементы и все входящие уникальны. Ни один элемент не будет расбросан по отчету
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Reports.CooperationList
{
    using System.Collections.Generic;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Этот класс позволяет составить закупочную ведомость, так сказать, в глубину. Все элементы и все входящие уникальны. Ни один элемент не будет расбросан по отчету
    /// </summary>
    public class CooperationListFactoryInDepth
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
        public CooperationListFactoryInDepth(MyNode mainNode)
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
            IDictionary<long, ExtractedObject> uniqueCooperationElements = new Dictionary<long, ExtractedObject>();

            // Очередь для прохода в ширину
            Queue<MyNode> queue = new Queue<MyNode>();
            queue.Enqueue(_mainNode);
            while (queue.Count > 0)
            {
                MyNode nodeFromQueue = queue.Dequeue();

                if (nodeFromQueue.Type == 1078 || nodeFromQueue.Type == 1074 || nodeFromQueue.Type == 1159 ||
                    nodeFromQueue.Type == 1052 || nodeFromQueue.Type == 1097)
                {
                    if (nodeFromQueue.CooperationFlag)
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
                            currentObject.SampleSize = nodeFromQueue.SampleSize;

                            uniqueCooperationElements.Add(nodeFromQueue.Id, currentObject);
                            IList<IntermechTreeElement> elementsInTree = _root.Find(nodeFromQueue.ObjectId);

                            foreach (IntermechTreeElement intermechTreeElement in elementsInTree)
                            {
                                if (intermechTreeElement.Parent.CooperationFlag == false)
                                {
                                    ExtractedObject parentReportElement = new ExtractedObject();
                                    parentReportElement.Id = intermechTreeElement.Parent.Id;
                                    parentReportElement.Type = intermechTreeElement.Parent.Type;
                                    parentReportElement.Designation = intermechTreeElement.Parent.Designation;
                                    parentReportElement.Name = intermechTreeElement.Parent.Name;

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
                            queue.Enqueue(child);
                        }
                    }
                }
            }

            // будет две очереди: одна для печатных плат, а другая для обычных элементов
            Queue<ExtractedObject> elementsQueue = new Queue<ExtractedObject>();
            Queue<ExtractedObject> pcpQueue = new Queue<ExtractedObject>();

            foreach (KeyValuePair<long, ExtractedObject> node in uniqueCooperationElements)
            {
                if (node.Value.IsPcb)
                {
                    pcpQueue.Enqueue(node.Value);
                }
                else
                {
                    elementsQueue.Enqueue(node.Value);
                }
            }

            CooperationListDocument document = new CooperationListDocument();

            document.SetCommonObjects(elementsQueue.ToArray());
            document.SetPcbObjects(pcpQueue.ToArray());

            return document;
        }
    }
}