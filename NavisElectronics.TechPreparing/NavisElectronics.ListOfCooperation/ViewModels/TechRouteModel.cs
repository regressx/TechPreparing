using System.Collections.Generic;
using Aga.Controls.Tree;
using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.ListOfCooperation.ViewModels
{
    public class TechRouteModel
    {

        private IList<TechRouteNode> _nodes;

        public TechRouteModel()
        {
            _nodes = new List<TechRouteNode>();
        }

        public void Add(TechRouteNode node)
        {
            _nodes.Add(node);
        }

        public void Remove(int index)
        {
            _nodes.RemoveAt(index);
        }

        public IList<TechRouteNode> GetTechRouteNodes()
        {
            return _nodes;
        }

        public TreeModel GetModel(TechRouteNode element)
        {
            TechRouteNodeView mainNode = new TechRouteNodeView();
            mainNode.Name = element.Name;
            mainNode.Tag = element;
            BuildNodeRecursive(mainNode, element);
            TreeModel model = new TreeModel();
            model.Nodes.Add(mainNode);
            return model;
        }

        /// <summary>
        /// Получение составного узла рекурсивно
        /// </summary>
        /// <param name="mainNode">
        /// Главный узел
        /// </param>
        /// <param name="element">
        /// Элемент, из которого получаем данные
        /// </param>
        private void BuildNodeRecursive(TechRouteNodeView mainNode, TechRouteNode element)
        {
            if (element.Children.Count > 0)
            {
                foreach (TechRouteNode child in element.Children)
                {
                    TechRouteNodeView childNode = new TechRouteNodeView();
                    childNode.Id = child.Id;
                    childNode.Type = child.Type;
                    childNode.Name = child.Name;
                    childNode.WorkshopName = child.WorkshopName;
                    childNode.PartitionName = child.PartitionName;
                    childNode.Tag = child;
                    mainNode.Nodes.Add(childNode);
                    BuildNodeRecursive(childNode, child);
                }
            }
        }

    }
}