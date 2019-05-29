using System;
using System.Collections.Generic;
using NavisElectronics.IPS1C.IntegratorService.Entities;
using NavisElectronics.IPS1C.IntegratorService.Exceptions;
using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.IPS1C.IntegratorService.Logic
{
    public class Changes
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
        /// The <see cref="TreeElement"/>.
        /// Возвращает обновленное дерево
        /// </returns>
        public ProductTreeNode Define(ProductTreeNode oldElement, IntermechTreeElement newElement, string agentFilter)
        {
            // проходимся в ширину
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(newElement);

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                ProductTreeNode element = null;
                try
                {
                    element = oldElement.Find(elementFromQueue.GetFullPathByObjectId());
                }
                catch (TreeNodeNotFoundException)
                {
                    continue;
                }

                element.CooperationFlag1 = elementFromQueue.CooperationFlag.ToString();
                element.StockRate = elementFromQueue.StockRate.ToString("F6");
                element.SampleSize = elementFromQueue.SampleSize;
                element.Agent = elementFromQueue.Agent;
                element.IsPCB = elementFromQueue.IsPCB.ToString();
                element.PcbVersion = elementFromQueue.PcbVersion.ToString();
                element.TechTaskOnPCB = elementFromQueue.TechTask;
                element.TypeOfWithDrawal = elementFromQueue.TypeOfWithDrawal.ToString();
                element.IsComplectNodeComponent = elementFromQueue.IsToComplect.ToString();
                element.Case = elementFromQueue.Case;
                if (element.Type1 == "1128")
                {
                    element.Amount1 = elementFromQueue.Amount.ToString("F6");
                }

                if (element.Agent != null)
                {
                    if (!element.Agent.Contains(agentFilter))
                    {
                        element.CooperationFlag1 = "True";
                    }
                }

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


        public ProductTreeNode Filter(ProductTreeNode root, string agentFilter)
        {
            Queue<ProductTreeNode> queue = new Queue<ProductTreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                ProductTreeNode nodeFromQueue = queue.Dequeue();

                if (nodeFromQueue.Agent != null)
                {
                    if (!nodeFromQueue.Agent.Contains(agentFilter))
                    {
                        nodeFromQueue.CooperationFlag1 = "True";
                    }
                }

                if (nodeFromQueue.Products.Count > 0)
                {
                    foreach (ProductTreeNode product in nodeFromQueue.Products)
                    {
                        queue.Enqueue(product);
                    }
                }

            }

            return root;
        }

    }
}