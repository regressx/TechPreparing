using NavisElectronics.TechPreparation.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavisElectronics.Orders.Services
{
    public interface IAddNoteStrategy
    {
        void AddNote(IntermechTreeElement element, string note);
    }

    internal class AddNoteToThisNodeStrategy : IAddNoteStrategy
    {
        public void AddNote(IntermechTreeElement element, string note)
        {
            string oldNote = element.Note == null ? string.Empty : element.Note;
            element.Note = string.Format($"{note} {oldNote}"); 
        }
    }

    internal class AddNoteToWholeTreeStrategy : IAddNoteStrategy
    {
        public void AddNote(IntermechTreeElement element, string note)
        {
            // дойти до корня
            IntermechTreeElement root = element;
            while(root.Parent!=null)
            {
                root = root.Parent;
            }

            // найти все вхождения узла во всём дереве
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                if (elementFromQueue.ObjectId == element.ObjectId)
                {
                    string oldNote = elementFromQueue.Note == null ? string.Empty : elementFromQueue.Note;
                    elementFromQueue.Note = string.Format($"{note} {oldNote}");
                }

                foreach(IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }

            }
        }
    }
}
