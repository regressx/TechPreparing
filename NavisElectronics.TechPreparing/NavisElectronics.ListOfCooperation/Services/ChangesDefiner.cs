namespace NavisElectronics.ListOfCooperation.Services
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Класс накладывает новые данные на старые
    /// </summary>
    public class ChangesDefiner
    {
        /// <summary>
        /// Метод присваивает новые данные в старом дереве
        /// </summary>
        /// <param name="oldElement">
        /// Входное старое дерево
        /// </param>
        /// <param name="newElement">
        /// Входное новое дерево
        /// </param>
        /// <returns>
        /// The <see cref="IntermechTreeElement"/>.
        /// Возвращает обновленное дерево
        /// </returns>
        public IntermechTreeElement GetChanges(IntermechTreeElement oldElement, IntermechTreeElement newElement)
        {
            // проходимся в ширину
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(newElement);

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                IntermechTreeElement element = oldElement.FindByVersionIdPath(elementFromQueue.GetFullPathByVersionId());

                element.CooperationFlag = elementFromQueue.CooperationFlag;
                element.Note = elementFromQueue.Note;
                element.TechProcessReference = elementFromQueue.TechProcessReference;
                element.SampleSize = elementFromQueue.SampleSize;
                element.StockRate = elementFromQueue.StockRate;
                element.TechTask = elementFromQueue.TechTask;
                element.IsPCB = elementFromQueue.IsPCB;
                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }


            return oldElement;
        }
    }
}