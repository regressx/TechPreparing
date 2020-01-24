using System.Collections.Generic;
using System.Text;
using NavisElectronics.TechPreparation.EventArguments;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Presenters;

namespace NavisElectronics.TechPreparation.DividingList
{
    public class DividingListPresenter
    {
        private IntermechTreeElement _rootElement;

        private void _mainView_ClearManufacturerClick(object sender, BoundTreeElementEventArgs e)
        {
            IntermechTreeElement selectedElement = e.Element;
            
            // проходом в ширину очистить всем входящим изготовителя
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(selectedElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.Agent = string.Empty;
                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    child.Agent = string.Empty;
                    queue.Enqueue(child);
                }
            }
            //_mainView.UpdateAgent(string.Empty);

            // TODO по идее надо еще и вверх подняться, чтобы убрать 
        }

        private void _mainView_CellValueChanged(object sender, TreeNodeAgentValueEventArgs e)
        {
            string agentsValues = GatherAgents(e);
            e.TreeElement.Agent = agentsValues;

            // Раздаем кооперацию потомкам
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            foreach (IntermechTreeElement child in e.TreeElement.Children)
            {
                queue.Enqueue(child);
            }

            while (queue.Count > 0)
            {
                IntermechTreeElement child = queue.Dequeue();
                child.Agent = agentsValues;
                if (child.Children.Count > 0)
                {
                    foreach (IntermechTreeElement childNodes in child.Children)
                    {
                        childNodes.Agent = agentsValues;
                        queue.Enqueue(childNodes);
                    }
                }
            }

            //раздаем изготовителя родителям
            SetParentCooperationRecursive(e.TreeElement, e.Key);
            //_mainView.UpdateAgent(e.TreeElement.Agent);

        }


        private void SetParentCooperationRecursive(IntermechTreeElement globalTreeElement, string key)
        {
            if (globalTreeElement.Parent != null)
            {
                IntermechTreeElement parent = globalTreeElement.Parent;
                if (parent == _rootElement)
                {
                    return;
                }

                string agents = GatherAgents(new TreeNodeAgentValueEventArgs(parent, key));
                parent.Agent = agents;
                SetParentCooperationRecursive(parent, key);
            }
        }


        private string GatherAgents(TreeNodeAgentValueEventArgs element)
        {
            IDictionary<long, string> agents = new Dictionary<long, string>();
            
            // собрать старых агентов
            string oldAgents = element.TreeElement.Agent;

            if (oldAgents == null)
            {
                oldAgents = string.Empty;
            }


            if (oldAgents != string.Empty)
            {
                ICollection<string> oldAgentsCollection = oldAgents.Split(';');

                foreach (string s in oldAgentsCollection)
                {
                    long key = long.Parse(s);
                    if (!agents.ContainsKey(key))
                    {
                        agents.Add(long.Parse(s), s);
                    }
                }
            }

            if (!agents.ContainsKey(long.Parse(element.Key)))
            {
                agents.Add(long.Parse(element.Key), element.Key);
            }


            ICollection<string> agentsValues = agents.Values;

            StringBuilder sb = new StringBuilder();
            foreach (string agentsValue in agentsValues)
            {
                sb.AppendFormat("{0};", agentsValue);
            }

            string value = sb.ToString().TrimEnd(';');

            return value;
        }

        private void _mainView_NodeMouseClick(object sender, TreeNodeClickEventArgs e)
        {
            // TODO Здесь должна появиться проверка на изменение предыдущего узла, если было какое-либо изменение


            //if (e.Element.Agent != null)
            //{
            //    _mainView.UpdateAgent(e.Element.Agent);
            //}

        }
    }
}