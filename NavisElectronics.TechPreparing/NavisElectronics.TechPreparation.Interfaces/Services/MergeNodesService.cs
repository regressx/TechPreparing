using NavisElectronics.TechPreparation.Interfaces.Enums;

namespace NavisElectronics.TechPreparation.Interfaces.Services
{
    using System.Collections.Generic;
    using Entities;

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
                
                // меняем состояние на состояние по умолчанию
                elementFromUpdateInitialized.NodeState = NodeStates.Default;

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
                    elementFromStack.NodeState = NodeStates.Default;
                    
                    // делаем клон и избавляемся от дочерних узлов
                    IntermechTreeElement nodeToAdd = (IntermechTreeElement)elementFromStack.Clone();
                    nodeToAdd.Clear();
                    thisParentInOldTree.Add(nodeToAdd);
                    thisParentInOldTree = nodeToAdd;
                }
            }

            // если элемент просто был модифицирован
            if (elementFromUpdateInitialized.NodeState == NodeStates.Modified)
            {
                IntermechTreeElement elementInOldTree = oldElement.FindByObjectIdPath(elementFromUpdateInitialized.GetFullPathByObjectId());
                elementFromUpdateInitialized.NodeState = NodeStates.Default;
                elementInOldTree.Id = elementFromUpdateInitialized.Id;
                elementInOldTree.PcbVersion = elementFromUpdateInitialized.PcbVersion;
                elementInOldTree.ChangeNumber = elementFromUpdateInitialized.ChangeNumber;
                elementInOldTree.ChangeDocument = elementFromUpdateInitialized.ChangeDocument;
                elementInOldTree.Position = elementFromUpdateInitialized.Position;
                elementInOldTree.PositionDesignation = elementFromUpdateInitialized.PositionDesignation;
                elementInOldTree.Amount = elementFromUpdateInitialized.Amount;
                elementInOldTree.MeasureUnits = elementFromUpdateInitialized.MeasureUnits;
                elementInOldTree.Name = elementFromUpdateInitialized.Name;
                elementInOldTree.RelationNote = elementFromUpdateInitialized.RelationNote;
                elementInOldTree.ProduseSign = elementFromUpdateInitialized.ProduseSign;
            }
        }

    }
}