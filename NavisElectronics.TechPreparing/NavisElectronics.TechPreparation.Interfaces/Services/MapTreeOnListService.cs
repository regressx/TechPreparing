using System;

namespace NavisElectronics.TechPreparation.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Text;

    public class MapTreeOnListService<T> where T : class, ITreeNode
    {
        private T _mainElement;

        public IList<T> MapTreeOnList(T mainElement)
        {
            if (mainElement == null)
            {
                throw new ArgumentNullException("mainElement", "Вы не указали элемент!");
            }

            _mainElement = mainElement;
            IList<T> list = new List<T>();
            list.Add(mainElement);
            mainElement.NumberInOrder = "1";
            mainElement.Level = 1;
            mainElement.NumberOnLevel = 1;
            MapTreeOnListRecursive(list, mainElement);
            return list;
        }


        internal void MapTreeOnListRecursive(IList<T> elements, T mainElement)
        {
            foreach (T node in mainElement.Nodes)
            {
                if (node.TypeId == 1138 || node.TypeId == 1128 || node.TypeId == 1105)
                {
                    continue;
                }

                if (node.TypeId == 1052 || node.TypeId == 1074 || node.TypeId == 1078 || node.TypeId == 1097 || node.TypeId == 1159)
                {
                    node.Level = ((T)node.Parent).Level + 1;
                    node.NumberOnLevel = node.Index;
                    node.NumberInOrder = GetNumberInOrder(node);
                }

                elements.Add(node);
                if (node.DoNotProduce)
                {
                    continue;
                }
                MapTreeOnListRecursive(elements, node);
            }
        }

        private string GetNumberInOrder(T node)
        {
            Stack<T> stack = new Stack<T>();
            stack.Push(node);
            T nodeForWork = node;
            while (nodeForWork.Parent != _mainElement)
            {      
                nodeForWork = nodeForWork.Parent as T;
                stack.Push(nodeForWork);
            }
            stack.Push(_mainElement);

            // выпишем первый элемент
            StringBuilder sb = new StringBuilder();

            if (stack.Count > 0)
            {
                sb.Append((stack.Pop().NumberOnLevel).ToString());
            }
            
            // а здесь заполним остальные через . , если есть
            while (stack.Count > 0)
            {
                sb.AppendFormat(".{0}", (stack.Pop().NumberOnLevel).ToString());
            }

            return sb.ToString();

        }


    }
}