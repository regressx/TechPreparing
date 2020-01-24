using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aga.Controls.Tree;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Interfaces.Services;

namespace NavisElectronics.Orders.TreeComparer
{
    /// <summary>
    /// Фасад для основной логики для обслуживания команд от окна TreeComparerView
    /// </summary>
    public class TreeComparerViewModel
    {
        /// <summary>
        /// The _merge nodes service.
        /// </summary>
        private readonly MergeNodesService _mergeNodesService;

        /// <summary>
        /// Репозиторий
        /// </summary>
        private readonly IDataRepository _reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeComparerViewModel"/> class.
        /// </summary>
        /// <param name="reader">
        /// Репозиторий с данными о дереве состава заказа
        /// </param>
        /// <param name="mergeNodesService">
        /// Сервис для слияния веток
        /// </param>
        public TreeComparerViewModel(IDataRepository reader, MergeNodesService mergeNodesService)
        {
            _mergeNodesService = mergeNodesService;
            _reader = reader;
        }


        /// <summary>
        /// Получение модели дерева
        /// </summary>
        /// <param name="element">Элемент, по которому строим модель</param>
        /// <returns><see cref="TreeModel"/></returns>
        public TreeModel GetModel(IntermechTreeElement element)
        {
            ComparerNode mainNode = new ComparerNode();
            mainNode.Id = element.Id;
            mainNode.ObjectId = element.ObjectId;
            mainNode.Designation = element.Designation;
            mainNode.Name = element.Name;
            mainNode.Amount = element.Amount;
            mainNode.CooperationFlag = element.CooperationFlag;
            mainNode.NodeState = element.NodeState;
            mainNode.Tag = element;
            BuildNodeRecursive(mainNode, element);
            TreeModel model = new TreeModel();
            model.Nodes.Add(mainNode);
            return model;
        }

        /// <summary>
        /// The get full order from database async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<IntermechTreeElement> GetFullOrderFromDatabaseAsync(long id, CancellationToken token)
        {
            return _reader.GetFullOrderAsync(id, token);
        }

        /// <summary>
        /// Метод сравнения деревьев
        /// </summary>
        /// <param name="oldElement">
        /// Левое дерево
        /// </param>
        /// <param name="newElement">
        /// Правое дерево
        /// </param>
        public void Compare(IntermechTreeElement oldElement, IntermechTreeElement newElement)
        {
            TreeComparerService comparerService = new TreeComparerService();
            comparerService.Compare(oldElement, newElement);
        }

        /// <summary>
        /// Метод слияния веток из правого дерева в левое
        /// </summary>
        /// <param name="oldElement">
        /// Левое дерево
        /// </param>
        /// <param name="newElement">
        /// Правое дерево
        /// </param>
        /// <param name="elementUpdateInit">
        /// Элемент для переноса
        /// </param>
        public void Upload(IntermechTreeElement oldElement, IntermechTreeElement newElement, IntermechTreeElement elementUpdateInit)
        {
            _mergeNodesService.Merge(oldElement, newElement, elementUpdateInit);
        }


        /// <summary>
        /// The get full path.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="T:long[]"/>.
        /// </returns>
        public long[] GetFullPath(Node node)
        {
            Stack<long> stack = new Stack<long>();
            ComparerNode myNode = node as ComparerNode;
            stack.Push(myNode.ObjectId);
            while (node.Parent != null)
            {
                ComparerNode parentNode = node.Parent as ComparerNode;
                if (parentNode != null)
                {
                    stack.Push(parentNode.ObjectId);
                }
                node = node.Parent;
            }
            return stack.ToArray();
        }

        public ComparerNode Find(ComparerNode nodeWhereToSeek, long[] path)
        {
            Queue<long> queue = new Queue<long>(path);

            if (nodeWhereToSeek.ObjectId == queue.Dequeue())
            {
                return FindNodeRecursive(nodeWhereToSeek, queue);
            }
            else
            {
                throw new Exception("Элемента нет по такому пути");
            }
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
        private void BuildNodeRecursive(ComparerNode mainNode, IntermechTreeElement element)
        {
            if (element.Children.Count > 0)
            {
                foreach (IntermechTreeElement child in element.Children)
                {
                    ComparerNode childNode = new ComparerNode();
                    childNode.Id = child.Id;
                    childNode.ObjectId = child.ObjectId;
                    childNode.Designation = child.Designation;
                    childNode.Name = child.Name;
                    childNode.Amount = child.Amount;
                    childNode.ChangeNumber = child.ChangeNumber;
                    childNode.CooperationFlag = child.CooperationFlag;
                    childNode.NodeState = child.NodeState;
                    childNode.Tag = child;
                    childNode.RelationType = child.RelationName;
                    mainNode.Nodes.Add(childNode);
                    BuildNodeRecursive(childNode, child);
                }
            }
        }


        /// <summary>
        /// The find node recursive.
        /// </summary>
        /// <param name="nodeWhereToSeek">
        /// The node where to seek.
        /// </param>
        /// <param name="queueId">
        /// The queue id.
        /// </param>
        /// <returns>
        /// The <see cref="ComparerNode"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private ComparerNode FindNodeRecursive(ComparerNode nodeWhereToSeek, Queue<long> queueId)
        {
            if (queueId.Count == 0)
            {
                return nodeWhereToSeek;
            }

            long currentId = queueId.Dequeue();
            ComparerNode currentNode = null;
            foreach (Node node in nodeWhereToSeek.Nodes)
            {
                if (((ComparerNode)node).ObjectId == currentId)
                {
                    currentNode = (ComparerNode)node;
                    break;
                }
            }

            if (currentNode == null)
            {
                throw new Exception("Элемента нет по такому пути");
            }
            return FindNodeRecursive(currentNode, queueId);
        }
    }
}