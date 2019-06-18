namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;

    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// Сервис простановки параметров
    /// </summary>
    public class SetParametersService
    {
        private IntermechTreeElement _orderElement;

        public SetParametersService(IntermechTreeElement orderElement)
        {
            _orderElement = orderElement;
        }

        /// <summary>
        /// Метод простановки кооперации
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="value"></param>
        public void SetCooperationValue(ICollection<IntermechTreeElement> elements, bool value)
        {
            foreach (IntermechTreeElement element in elements)
            {
                SetCooperationValueInternal(element, value);
            }
        }
        
        /// <summary>
        /// Метод простановки внутрипроизводственной кооперации
        /// </summary>
        /// <param name="elements">Набор элементов, кому надо ее проставить</param>
        /// <param name="value">Какое значение поставить</param>
        public void SetInnerCooperationValue(IList<IntermechTreeElement> elements, bool value)
        {
            foreach (IntermechTreeElement element in elements)
            {
                SetInnerCooperationValueInternal(element, value);
            }
        }




        public void SetTechProcess(ICollection<IntermechTreeElement> elements, TechProcess techProcess)
        {
            foreach (IntermechTreeElement element in elements)
            {
                SetTechProcessReference(element, techProcess);
            }
        }

        public void SetParameters(IList<IntermechTreeElement> elements, double stockRate, string sampleSize)
        {
            foreach (IntermechTreeElement element in elements)
            {
                SetParametersInternal(element, stockRate,sampleSize);
            }
        }


        internal void SetTechProcessReference(IntermechTreeElement element, TechProcess techProcess)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            IList<IntermechTreeElement> allElementsFromTree = _orderElement.Find(element.Id);
            

            foreach (IntermechTreeElement treeElement in allElementsFromTree)
            {
                queue.Enqueue(treeElement);
            }

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.TechProcessReference = techProcess;
                
                // и этих тоже, это дочерние элементы узла из очереди
                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }



        internal void SetCooperationValueInternal(IntermechTreeElement element, bool value)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            IList<IntermechTreeElement> allElementsFromTree = _orderElement.Find(element.Id);
            // всех под кооперацию, всех, кого нашли
            foreach (IntermechTreeElement treeElement in allElementsFromTree)
            {
                queue.Enqueue(treeElement);
            }

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.CooperationFlag = value;
                // и этих тоже, это дочерние элементы узла из очереди
                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        private void SetInnerCooperationValueInternal(IntermechTreeElement element, bool value)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            IList<IntermechTreeElement> allElementsFromTree = _orderElement.Find(element.Id);
            
            // всех под кооперацию! Всех, кого нашли
            foreach (IntermechTreeElement treeElement in allElementsFromTree)
            {
                queue.Enqueue(treeElement);
            }

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.InnerCooperation = value;
                elementFromQueue.ContainsInnerCooperation = value;

                // теперь надо, поднимаясь вверх, у каждого родителя поставить ContainsInnerCooperation в value

                if (elementFromQueue.Parent != null)
                {
                    Queue<IntermechTreeElement> parentQueue = new Queue<IntermechTreeElement>();
                    parentQueue.Enqueue(elementFromQueue.Parent);
                    while (parentQueue.Count > 0)
                    {
                        IntermechTreeElement parent = parentQueue.Dequeue();
                        parent.ContainsInnerCooperation = value;
                        if (parent.Parent != null)
                        {
                            parentQueue.Enqueue(parent.Parent);
                        }
                    }

                }
            }



        }


        internal void SetParametersInternal(IntermechTreeElement element, double stockRate, string sampleSize)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            IList<IntermechTreeElement> allElementsFromTree = _orderElement.Find(element.Id);
            

            foreach (IntermechTreeElement treeElement in allElementsFromTree)
            {
                queue.Enqueue(treeElement);
            }

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.StockRate = stockRate;
                elementFromQueue.SampleSize = sampleSize;
                
                // и этих тоже, это дочерние элементы узла из очереди
                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }



    }
}