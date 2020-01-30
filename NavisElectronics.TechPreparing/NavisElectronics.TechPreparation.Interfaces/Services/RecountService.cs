using NavisElectronics.TechPreparation.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavisElectronics.TechPreparation.Interfaces.Services
{
    public class RecountService
    {
        public void RecountAmount(IntermechTreeElement root)
        {
            // расчет применяемостей
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                IntermechTreeElement parent = elementFromQueue.Parent;

                if (parent != null)
                {
                    elementFromQueue.UseAmount = (int)Math.Round(parent.AmountWithUse, MidpointRounding.ToEven);
                    elementFromQueue.AmountWithUse = elementFromQueue.UseAmount * elementFromQueue.Amount;
                    elementFromQueue.TotalAmount = elementFromQueue.AmountWithUse * elementFromQueue.StockRate;
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

    }
}
