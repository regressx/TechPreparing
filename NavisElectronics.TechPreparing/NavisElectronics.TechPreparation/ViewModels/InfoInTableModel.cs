// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfoInTableModel.cs" company="">
//   
// </copyright>
// <summary>
//   Модель для представления InfoInTable
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// Модель для представления InfoInTable
    /// </summary>
    public class InfoInTableModel
    {
        private readonly IntermechTreeElement _mainElement;

        public InfoInTableModel(IntermechTreeElement mainElement)
        {
            _mainElement = mainElement;
        }

        public IList<ExtractedObject> GetUniqueElements(ExtractedObject material)
        {
            IDictionary<long, ExtractedObject> uniqueElements = new Dictionary<long, ExtractedObject>();

            foreach (ExtractedObject element in material.Elements)
            {
                ExtractedObject newElement;
                if (uniqueElements.ContainsKey(element.Id))
                {
                    newElement = uniqueElements[element.Id];
                    newElement.ParentUse += element.ParentUse;
                    newElement.AmountWithUse += element.AmountWithUse;
                }
                else
                {
                    newElement = new ExtractedObject();
                    newElement.Id = element.Id;
                    newElement.Name = element.Name;
                    newElement.Designation = element.Designation;
                    newElement.Amount = element.Amount;
                    newElement.ParentUse = element.ParentUse;
                    newElement.StockRate = element.StockRate;
                    newElement.Type = element.Type;
                    newElement.AmountWithUse = element.AmountWithUse;
                    uniqueElements.Add(newElement.Id, newElement);
                }

                // пересчитаем итог
                newElement.Total = newElement.AmountWithUse * newElement.StockRate;
                newElement.RegisterElement(element);
            }
            return uniqueElements.Values.ToList();
        }

        /// <summary>
        /// Метод отражает изменения данных на элементах дерева
        /// </summary>
        /// <param name="materials">
        /// Узлы, которые требуется изменить
        /// </param>
        /// <param name="parent">
        /// Родитель, для которого производятся изменения
        /// </param>
        /// <param name="amount">
        /// Количество
        /// </param>
        /// <param name="stockRate">
        /// Коэффициент запаса
        /// </param>
        public void SetParametersToSelectedElements(ICollection<ExtractedObject> materials, ExtractedObject parent, double amount, double stockRate)
        {
            foreach (ExtractedObject element in materials)
            {
                parent.Total -= element.Total;
                element.Amount = (float)amount;
                element.StockRate = stockRate;

                // для объектов-заместителей тоже следует пересчитать данные
                foreach (ExtractedObject child in element.Elements)
                {
                    child.Amount = element.Amount;
                    child.StockRate = element.StockRate;

                    child.AmountWithUse = child.Amount * child.ParentUse;
                    child.Total = child.AmountWithUse * child.StockRate;
                    parent.Total += child.Total;

                    // пересчитываем для основного дерева
                    IntermechTreeElement elementInTree = _mainElement.FindByObjectIdPath(child.Path);
                    elementInTree.Amount = (float)amount;
                    elementInTree.StockRate = stockRate;
                    elementInTree.AmountWithUse = elementInTree.Amount * elementInTree.Parent.AmountWithUse;
                    elementInTree.TotalAmount = elementInTree.AmountWithUse * elementInTree.StockRate;
                }
            }
        }

    }
}