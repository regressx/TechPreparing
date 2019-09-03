namespace NavisElectronics.TechPreparation.Reports.CooperationList
{
    using System.Collections.Generic;
    using Interfaces.Entities;
    using ViewModels.TreeNodes;

    /// <summary>
    /// Этот класс позволяет составить ведомость кооперации, так сказать, в глубину. Все элементы и все входящие уникальны. Ни один элемент не будет расбросан по отчету
    /// </summary>
    public class CooperationListFactory
    {
        /// <summary>
        /// Узел, для которого делается ведомость кооперации
        /// </summary>
        private readonly MyNode _mainNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationListFactory"/> class. 
        /// </summary>
        /// <param name="mainNode">
        /// Выбранный Вами узел
        /// </param>
        public CooperationListFactory(MyNode mainNode)
        {
            _mainNode = mainNode;
        }


        /// <summary>
        /// Метод создания отчета
        /// </summary>
        /// <returns>
        /// The <see cref="CooperationListDocument"/>.
        /// </returns>
        public CooperationListDocument Create()
        {

            IntermechTreeElement taggedElement = (IntermechTreeElement)_mainNode.Tag;

            // заполнить словарик уникальных элементов, делающихся по кооперации
            IDictionary<long, IntermechTreeElement> uniqueCooperationElements = new Dictionary<long, IntermechTreeElement>();

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
                        IntermechTreeElement parentElement = (IntermechTreeElement)nodeFromQueue.Parent.Tag;
                        if (!uniqueCooperationElements.ContainsKey(nodeFromQueue.Id))
                        {
                            IntermechTreeElement currentObject = new IntermechTreeElement()
                            {
                                Id = nodeFromQueue.Id,
                                Type = nodeFromQueue.Type,
                                Name = nodeFromQueue.Name,
                                Designation = nodeFromQueue.Designation,
                                CooperationFlag = nodeFromQueue.CooperationFlag,
                                IsPCB = nodeFromQueue.IsPcb,
                                PcbVersion = (byte)nodeFromQueue.PcbVersion,
                                MeasureUnits = ((IntermechTreeElement)nodeFromQueue.Tag).MeasureUnits,
                                TechProcessReference = new TechProcess()
                                {
                                    Name = nodeFromQueue.TechProcessReference
                                },
                                StockRate = nodeFromQueue.StockRate,
                                SampleSize = nodeFromQueue.SampleSize
                            };

                            uniqueCooperationElements.Add(nodeFromQueue.Id, currentObject);
                            currentObject.Add(parentElement);
                        }
                        else
                        {
                            IntermechTreeElement currentObject = uniqueCooperationElements[nodeFromQueue.Id];
                            currentObject.Add(parentElement);
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
            Queue<IntermechTreeElement> elementsQueue = new Queue<IntermechTreeElement>();
            Queue<IntermechTreeElement> pcpQueue = new Queue<IntermechTreeElement>();

            foreach (KeyValuePair<long, IntermechTreeElement> node in uniqueCooperationElements)
            {
                if (node.Value.IsPCB)
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