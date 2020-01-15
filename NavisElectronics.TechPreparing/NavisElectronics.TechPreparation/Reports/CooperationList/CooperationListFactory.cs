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
            // Элемент, который выбрал пользователь для работы
            IntermechTreeElement taggedElement = (IntermechTreeElement)_mainNode.Tag;

            // делаем полную его копию
            IntermechTreeElement clonedElement = (IntermechTreeElement)taggedElement.Clone();
            clonedElement.Amount = 1;
            clonedElement.AmountWithUse = 1;
            
            // пересчитаем количества
            Stack<IntermechTreeElement> stack = new Stack<IntermechTreeElement>();
            stack.Push(clonedElement);
            while (stack.Count > 0)
            {
                IntermechTreeElement elementFromStack = stack.Pop();

                IntermechTreeElement parent = elementFromStack.Parent;
                if (parent != null)
                {
                    elementFromStack.AmountWithUse = parent.AmountWithUse * elementFromStack.Amount;
                    elementFromStack.TotalAmount = elementFromStack.AmountWithUse * elementFromStack.StockRate;
                }

                foreach (IntermechTreeElement child in elementFromStack.Children)
                {
                    stack.Push(child);
                }
            }

            // заполнить словарик уникальных элементов, делающихся по кооперации
            IDictionary<long, IntermechTreeElement> uniqueCooperationElements = new Dictionary<long, IntermechTreeElement>();

            // Очередь для прохода в ширину
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(clonedElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement nodeFromQueue = queue.Dequeue();

                if (nodeFromQueue.Type == 1078 || nodeFromQueue.Type == 1074 || nodeFromQueue.Type == 1159 ||
                    nodeFromQueue.Type == 1052 || nodeFromQueue.Type == 1097)
                {
                    if (nodeFromQueue.CooperationFlag && !nodeFromQueue.ProduseSign)
                    {
                        IntermechTreeElement parentElement = (IntermechTreeElement)nodeFromQueue.Parent.Clone();
                        parentElement.Amount = nodeFromQueue.Amount;
                        parentElement.AmountWithUse = nodeFromQueue.AmountWithUse;
                        parentElement.TechTask = nodeFromQueue.TechTask;
                        parentElement.StockRate = nodeFromQueue.StockRate;
                        parentElement.TotalAmount = parentElement.AmountWithUse * parentElement.StockRate;
                        if (!uniqueCooperationElements.ContainsKey(nodeFromQueue.Id))
                        {
                            IntermechTreeElement currentObject = new IntermechTreeElement()
                            {
                                Id = nodeFromQueue.Id,
                                Type = nodeFromQueue.Type,
                                Name = nodeFromQueue.Name,
                                Designation = nodeFromQueue.Designation,
                                CooperationFlag = nodeFromQueue.CooperationFlag,
                                IsPcb = nodeFromQueue.IsPcb,
                                PcbVersion = nodeFromQueue.PcbVersion,
                                MeasureUnits = nodeFromQueue.MeasureUnits,
                                TechProcessReference = nodeFromQueue.TechProcessReference,
                                SampleSize = nodeFromQueue.SampleSize,
                                TechTask = nodeFromQueue.TechTask
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
                    foreach (IntermechTreeElement node in nodeFromQueue.Children)
                    {
                        queue.Enqueue(node);
                    }
                }
            }

            // будет две очереди: одна для печатных плат, а другая для обычных элементов
            Queue<IntermechTreeElement> elementsQueue = new Queue<IntermechTreeElement>();
            Queue<IntermechTreeElement> pcpQueue = new Queue<IntermechTreeElement>();

            foreach (KeyValuePair<long, IntermechTreeElement> node in uniqueCooperationElements)
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