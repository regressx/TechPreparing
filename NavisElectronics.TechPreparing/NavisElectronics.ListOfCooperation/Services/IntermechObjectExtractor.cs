namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;

    using NavisElectronics.ListOfCooperation.Entities;
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.Enums;

    /// <summary>
    /// Класс для извлечения представленного типа объекта из IPS из дерева
    /// </summary>
    public class IntermechObjectExtractor
    {
        /// <summary>
        /// Возвращает уникальную коллекцию элементов определенного типа, входящих в некоторый узел. Элементы по кооперации не задействованы
        /// </summary>
        /// <param name="mainElement">
        /// Узел, из которого выбираем элементы
        /// </param>
        /// <param name="type">
        /// Тип извлекаемого объекта
        /// </param>
        /// <returns>
        /// The <see cref="ICollection{IntermechTreeElement}"/>.
        /// </returns>
        public ICollection<ExtractedObject> ExctractObjects(IntermechTreeElement mainElement, IntermechObjectTypes type)
        {
            IDictionary<long, ExtractedObject> uniqueElements = new Dictionary<long, ExtractedObject>();
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(mainElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                // если по кооперации, то пропускаем
                if (elementFromQueue.CooperationFlag)
                {
                    continue;
                }

                if (elementFromQueue.Type == 1088 || elementFromQueue.Type == 1125)
                {
                    elementFromQueue.Type = (int)IntermechObjectTypes.Material;
                }

                if (elementFromQueue.Type == (int)type)
                {
                    ExtractedObject uniqueElement = null;
                    if (uniqueElements.ContainsKey(elementFromQueue.Id))
                    {
                        uniqueElement = uniqueElements[elementFromQueue.Id];
                        uniqueElement.Total += elementFromQueue.TotalAmount;
                    }
                    else
                    {
                        // создадим объект для последующего пополнения
                        uniqueElement = new ExtractedObject()
                        {
                            Id = elementFromQueue.Id,
                            Name = elementFromQueue.Name,
                            MeasureUnits = elementFromQueue.MeasureUnits,
                            Case = elementFromQueue.Case,
                            WithdrawalType = elementFromQueue.TypeOfWithDrawal,
                            Total = elementFromQueue.TotalAmount
                        };
                        uniqueElements.Add(elementFromQueue.Id, uniqueElement);
                    }

                    // родитель для материала
                    IntermechTreeElement parent = elementFromQueue.Parent;

                    ExtractedObject reportElement = new ExtractedObject();

                    // основная информация о родителе материала и количество материала на этого родителя
                    reportElement.Id = parent.Id;
                    reportElement.Type = parent.Type;
                    reportElement.Name = parent.Name;
                    reportElement.Designation = parent.Designation;
                    reportElement.StockRate = elementFromQueue.StockRate;
                    reportElement.Amount = elementFromQueue.Amount;
                    reportElement.ParentUse = parent.AmountWithUse;
                    reportElement.AmountWithUse = elementFromQueue.AmountWithUse;
                    
                    // где находится материал
                    reportElement.Path = elementFromQueue.GetFullPathByObjectId();
                    uniqueElement.RegisterElement(reportElement);
                }

                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            return uniqueElements.Values;
        }
    }
}