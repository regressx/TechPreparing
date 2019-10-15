using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.Interfaces
{
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
            // когда отправляем вновь добавленный узел в старое дерево
            if (elementFromUpdateInitialized.NodeState == NodeStates.Added)
            {
                Stack<IntermechTreeElement> stack = new Stack<IntermechTreeElement>();

                // добавить в стек сам элемент
                stack.Push(elementFromUpdateInitialized);

                // найти первого родителя, который не имеет статус добавлен или удален
                IntermechTreeElement firstParentOfAddedElement = elementFromUpdateInitialized.Parent;
                stack.Push(firstParentOfAddedElement);

                while (firstParentOfAddedElement.NodeState != NodeStates.Default && firstParentOfAddedElement.NodeState != NodeStates.Modified)
                {
                    firstParentOfAddedElement = firstParentOfAddedElement.Parent;
                    stack.Push(firstParentOfAddedElement);
                }

                // ищем этого первого родителя в старом дереве
                IntermechTreeElement thisParentInOldTree = oldElement.FindByObjectIdPath(firstParentOfAddedElement.GetFullPathByObjectId());
                stack.Pop();
                while (stack.Count > 0)
                {
                    IntermechTreeElement elementFromStack = stack.Pop();

                    // делаем клон и избавляемся от дочерних узлов
                    IntermechTreeElement nodeToAdd = (IntermechTreeElement)elementFromStack.Clone();
                    nodeToAdd.Clear();
                    thisParentInOldTree.Add(nodeToAdd);
                    thisParentInOldTree = nodeToAdd;
                }
            }


            if (elementFromUpdateInitialized.NodeState == NodeStates.Modified)
            {
                IntermechTreeElement elementInOldTree = oldElement.FindByObjectIdPath(elementFromUpdateInitialized.GetFullPathByObjectId());


            }

            //// ищем элемент инициализации в старом дереве
            //IntermechTreeElement elementInOldTree = oldElement.FindByObjectIdPath(elementFromUpdateInitialized.GetFullPathByObjectId());
            //Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>(); // очередь из элементов старого дерева
            //queue.Enqueue(elementInOldTree);
            //while (queue.Count > 0)
            //{
            //    IntermechTreeElement elementFromQueue = queue.Dequeue();

            //    if (elementFromQueue.NodeState == NodeStates.Deleted)
            //    {
            //        elementFromQueue.Parent.Remove(elementFromQueue);
            //        continue;
            //    }

            //    IntermechTreeElement elementFromDatabase = newElement.FindByObjectIdPath(elementFromQueue.GetFullPathByObjectId()); // получить узел нового дерева по адресу старого

            //    if (elementFromDatabase.NodeState == NodeStates.Modified)
            //    {
            //        // Нельзя просто заменить объект друг на друга. Лучше скопировать поля и то, не все
            //        elementFromQueue.Id = elementFromDatabase.Id;
            //        elementFromQueue.PositionDesignation = elementFromDatabase.PositionDesignation;
            //        elementFromQueue.Position = elementFromDatabase.Position;
            //        elementFromQueue.MeasureUnits = elementFromDatabase.MeasureUnits;
            //        elementFromQueue.ChangeNumber = elementFromDatabase.ChangeNumber;
            //        elementFromQueue.IsPcb = elementFromDatabase.IsPcb;
            //        elementFromQueue.PcbVersion = elementFromDatabase.PcbVersion;
            //        elementFromQueue.Type = elementFromDatabase.Type;
            //        elementFromQueue.Class = elementFromDatabase.Class;
            //        elementFromQueue.Supplier = elementFromDatabase.Supplier;
            //        elementFromQueue.PartNumber = elementFromDatabase.PartNumber;
            //        elementFromQueue.SubstituteGroupNumber = elementFromDatabase.SubstituteGroupNumber;
            //        elementFromQueue.SubstituteNumberInGroup = elementFromDatabase.SubstituteNumberInGroup;
            //        elementFromQueue.Case = elementFromDatabase.Case;
            //        elementFromQueue.MountingType = elementFromDatabase.MountingType;
            //        elementFromQueue.TechTask = elementFromDatabase.TechTask;
            //        elementFromQueue.Name = elementFromDatabase.Name;

            //        // здесь эта штука, чтобы случайно не обновить реальное, но очень маленькое число, на 0
            //        if (elementFromDatabase.Amount >= 0.000001)
            //        {
            //            elementFromQueue.Amount = elementFromDatabase.Amount;
            //        }
            //    }

            //    if (elementFromQueue.NodeState == NodeStates.Default)
            //    {
            //        elementFromQueue.Id = elementFromDatabase.Id;
            //        elementFromQueue.PositionDesignation = elementFromDatabase.PositionDesignation;
            //        elementFromQueue.Position = elementFromDatabase.Position;
            //        elementFromQueue.MeasureUnits = elementFromDatabase.MeasureUnits;
            //        elementFromQueue.IsPcb = elementFromDatabase.IsPcb;
            //        elementFromQueue.PcbVersion = elementFromDatabase.PcbVersion;
            //        elementFromQueue.Type = elementFromDatabase.Type;
            //        elementFromQueue.Class = elementFromDatabase.Class;
            //        elementFromQueue.Supplier = elementFromDatabase.Supplier;
            //        elementFromQueue.PartNumber = elementFromDatabase.PartNumber;
            //        elementFromQueue.SubstituteGroupNumber = elementFromDatabase.SubstituteGroupNumber;
            //        elementFromQueue.SubstituteNumberInGroup = elementFromDatabase.SubstituteNumberInGroup;
            //        elementFromQueue.Case = elementFromDatabase.Case;

            //        if (elementFromQueue.Children.Count > 0)
            //        {
            //            foreach (IntermechTreeElement child in elementFromQueue.Children)
            //            {
            //                queue.Enqueue(child);
            //            }
            //        }
            //    }

            //    // Добавить новые узлы
            //    foreach (IntermechTreeElement child in elementFromDatabase.Children)
            //    {
            //        if (child.NodeState == NodeStates.Added)
            //        {
            //            elementFromQueue.Add((IntermechTreeElement)child.Clone());
            //        }
            //    }
            //}
        }

    }
}