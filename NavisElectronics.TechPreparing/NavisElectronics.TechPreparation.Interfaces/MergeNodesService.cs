namespace NavisElectronics.TechPreparation.Interfaces
{
    using System.Collections.Generic;
    using Entities;
    using Enums;

    /// <summary>
    /// Сервис для слияния веток
    /// </summary>
    public class MergeNodesService
    {
        /// <summary>
        /// Метод слияния деревьев
        /// </summary>
        /// <param name="oldElement">
        /// Старое дерево
        /// </param>
        /// <param name="newElement">
        /// Новое дерево
        /// </param>
        /// <param name="elementFromUpdateInitialized">
        /// Элемент, к которому применяем обновление
        /// </param>
        public void Merge(IntermechTreeElement oldElement, IntermechTreeElement newElement, IntermechTreeElement elementFromUpdateInitialized)
        {
            // ищем элемент инициализации в старом дереве
            IntermechTreeElement elementInOldTree = oldElement.FindByObjectIdPath(elementFromUpdateInitialized.GetFullPathByObjectId());
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>(); // очередь из элементов старого дерева
            queue.Enqueue(elementInOldTree);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                if (elementFromQueue.NodeState == NodeStates.Deleted)
                {
                    elementFromQueue.Parent.Remove(elementFromQueue);
                    continue;
                }

                IntermechTreeElement elementFromDatabase = newElement.FindByObjectIdPath(elementFromQueue.GetFullPathByObjectId()); // получить узел нового дерева по адресу старого

                if (elementFromDatabase.NodeState == NodeStates.Modified)
                {
                    elementFromQueue.Id = elementFromDatabase.Id;
                    elementFromQueue.PositionDesignation = elementFromDatabase.PositionDesignation;
                    elementFromQueue.Position = elementFromDatabase.Position;
                    elementFromQueue.MeasureUnits = elementFromDatabase.MeasureUnits;
                    elementFromQueue.ChangeNumber = elementFromDatabase.ChangeNumber;
                    elementFromQueue.IsPcb = elementFromDatabase.IsPcb;
                    elementFromQueue.PcbVersion = elementFromDatabase.PcbVersion;
                    elementFromQueue.Type = elementFromDatabase.Type;
                    elementFromQueue.Class = elementFromDatabase.Class;
                    elementFromQueue.Supplier = elementFromDatabase.Supplier;
                    elementFromQueue.PartNumber = elementFromDatabase.PartNumber;
                    elementFromQueue.SubstituteGroupNumber = elementFromDatabase.SubstituteGroupNumber;
                    elementFromQueue.SubstituteNumberInGroup = elementFromDatabase.SubstituteNumberInGroup;
                    elementFromQueue.Case = elementFromDatabase.Case;

                    if (elementFromDatabase.Amount >= 0.000001)
                    {
                        elementFromQueue.Amount = elementFromDatabase.Amount;
                    }
                }

                if (elementFromQueue.NodeState == NodeStates.Default)
                {
                    elementFromQueue.Id = elementFromDatabase.Id;
                    elementFromQueue.PositionDesignation = elementFromDatabase.PositionDesignation;
                    elementFromQueue.Position = elementFromDatabase.Position;
                    elementFromQueue.MeasureUnits = elementFromDatabase.MeasureUnits;
                    elementFromQueue.IsPcb = elementFromDatabase.IsPcb;
                    elementFromQueue.PcbVersion = elementFromDatabase.PcbVersion;
                    elementFromQueue.Type = elementFromDatabase.Type;
                    elementFromQueue.Class = elementFromDatabase.Class;
                    elementFromQueue.Supplier = elementFromDatabase.Supplier;
                    elementFromQueue.PartNumber = elementFromDatabase.PartNumber;
                    elementFromQueue.SubstituteGroupNumber = elementFromDatabase.SubstituteGroupNumber;
                    elementFromQueue.SubstituteNumberInGroup = elementFromDatabase.SubstituteNumberInGroup;
                    elementFromQueue.Case = elementFromDatabase.Case;

                    if (elementFromQueue.Children.Count > 0)
                    {
                        foreach (IntermechTreeElement child in elementFromQueue.Children)
                        {
                            queue.Enqueue(child);
                        }
                    }
                }

                // Добавить новые узлы
                foreach (IntermechTreeElement child in elementFromDatabase.Children)
                {
                    if (child.NodeState == NodeStates.Added)
                    {
                        elementFromQueue.Add(child);
                    }
                }
            }
        }

    }
}