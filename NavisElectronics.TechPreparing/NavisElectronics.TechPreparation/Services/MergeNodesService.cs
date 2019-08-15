using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;

    /// <summary>
    /// Класс сое
    /// </summary>
    public class MergeNodesService
    {
        public void Merge(IntermechTreeElement oldElement, IntermechTreeElement newElement, IntermechTreeElement elementFromUpdateInitialized)
        {
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
                    elementFromQueue.IsPCB = elementFromDatabase.IsPCB;
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
                    elementFromQueue.IsPCB = elementFromDatabase.IsPCB;
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