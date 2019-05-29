namespace NavisElectronics.ListOfCooperation.Services
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Enums;
    using Exceptions;

    public class TreeComparerService
    {
        public void Compare(IntermechTreeElement oldElement, IntermechTreeElement newElement)
        {
            Queue<IntermechTreeElement> queue1 = new Queue<IntermechTreeElement>();
            queue1.Enqueue(oldElement);
            while (queue1.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue1.Dequeue();
                elementFromQueue.NodeState = NodeStates.Default;
                try
                {
                    IntermechTreeElement elementInNewTree = newElement.FindByObjectIdPath(elementFromQueue.GetFullPathByObjectId());
                    elementInNewTree.CooperationFlag = elementFromQueue.CooperationFlag;
                }
                catch (TreeNodeNotFoundException)
                {
                    // если мы здесь, значит в составе заказа не нашелся объект. Давайте попробуем найти хоть что-то
                    elementFromQueue.NodeState = NodeStates.Deleted;
                }

                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue1.Enqueue(child);
                    }
                }
            }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           Queue<IntermechTreeElement> queue2 = new Queue<IntermechTreeElement>();
            queue2.Enqueue(newElement); // очередь из нового дерева
            while (queue2.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue2.Dequeue();
                elementFromQueue.NodeState = NodeStates.Default;
                try
                {
                    string path = elementFromQueue.GetFullPathByObjectId();

                    // ищем узел в старом дереве
                    IntermechTreeElement elementToFind =
                        oldElement.FindByObjectIdPath(path);

                    if (elementFromQueue.PositionDesignation == null)
                    {
                        elementFromQueue.PositionDesignation = string.Empty;
                    }

                    if (elementFromQueue.Position == null)
                    {
                        elementFromQueue.Position = string.Empty;
                    }

                    if (elementToFind.PositionDesignation == null)
                    {
                        elementToFind.PositionDesignation = string.Empty;
                    }

                    if (elementToFind.Position == null)
                    {
                        elementToFind.Position = string.Empty;
                    }

                    if (elementToFind.ChangeNumber == null)
                    {
                        elementToFind.ChangeNumber = string.Empty;
                    }

                    if (elementFromQueue.ChangeNumber == null)
                    {
                        elementFromQueue.ChangeNumber = string.Empty;
                    }

                    if ((Math.Round(elementToFind.Amount, 6) - Math.Round(elementFromQueue.Amount, 6)) != 0
                        || elementToFind.ChangeNumber != elementFromQueue.ChangeNumber
                        || elementToFind.PcbVersion != elementFromQueue.PcbVersion
                        || elementToFind.Id != elementFromQueue.Id
                        || elementToFind.MeasureUnits != elementFromQueue.MeasureUnits)
                    {
                        elementFromQueue.NodeState = NodeStates.Modified;
                    }

                }
                catch (TreeNodeNotFoundException)
                {
                    elementFromQueue.NodeState = NodeStates.Added;
                }

                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue2.Enqueue(child);
                    }
                }
            }
        }
    }
}