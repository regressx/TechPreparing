namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;
    using System.Text;

    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    public class MapTreeOnListService
    {
        private MyNode _mainElement;
        public IList<MyNode> MapTreeOnList(MyNode mainElement)
        {
            _mainElement = mainElement;
            IList<MyNode> list = new List<MyNode>();
            list.Add(mainElement);
            mainElement.NumberInOrder = "1";
            mainElement.Level = 1;
            mainElement.NumberOnLevel = 1;
            MapTreeOnListRecursive(list, mainElement);
            return list;
        }


        internal void MapTreeOnListRecursive(IList<MyNode> elements, MyNode mainElement)
        {
            if (mainElement.Nodes.Count > 0)
            {
                foreach (MyNode node in mainElement.Nodes)
                {
                    node.Level = ((MyNode)node.Parent).Level + 1;
                    node.NumberOnLevel = node.Index + 1;
                    node.NumberInOrder = GetNumberInOrder(node);

                    elements.Add(node);
                    MapTreeOnListRecursive(elements, node);
                }
            }
        }

        private string GetNumberInOrder(MyNode node)
        {
            Stack<MyNode> stack = new Stack<MyNode>();
            stack.Push(node);
            MyNode nodeForWork = node;
            while (nodeForWork.Parent != _mainElement)
            {      
                nodeForWork = nodeForWork.Parent as MyNode;
                stack.Push(nodeForWork);
            }
            stack.Push(_mainElement);

            StringBuilder sb = new StringBuilder();

            if (stack.Count > 0)
            {
                sb.Append((stack.Pop().NumberOnLevel).ToString());
            }
            
            // а здесь заполним

            while (stack.Count > 0)
            {
                sb.AppendFormat(".{0}", (stack.Pop().NumberOnLevel).ToString());
            }

            return sb.ToString();

        }


    }
}