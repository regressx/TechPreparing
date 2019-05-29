using System.Collections.Generic;

namespace NavisElectronics.ListOfCooperation.Entities
{
    /// <summary>
    /// Узел тех. процесса
    /// </summary>
    public class TechRouteNode
    {
        /// <summary>
        /// Дочерние узлы
        /// </summary>
        private IList<TechRouteNode> _nodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRouteNode"/> class.
        /// </summary>
        public TechRouteNode()
        {
            _nodes = new List<TechRouteNode>();
        }

        /// <summary>
        /// Идентификатор тех. узла
        /// </summary>
        public long Id { get;set; }

        /// <summary>
        /// Тип
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Участок
        /// </summary>
        public string PartitionName { get; set; }

        /// <summary>
        /// Цех
        /// </summary>
        public string WorkshopName { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор участка
        /// </summary>
        public long PartitionId { get; set; }

        /// <summary>
        /// Идентификатор цеха
        /// </summary>
        public long WorkshopId { get; set; }

        /// <summary>
        /// Получить имя
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCaption()
        {
            // если название участка пустое
            if (string.IsNullOrEmpty(PartitionName))
            {
                // то это цех. Возвращаем имя цеха
                return WorkshopName;
            }
            else
            {
                // если цех пустой, то это ошибка.
                if (string.IsNullOrEmpty(WorkshopName))
                {
                    return "Ошибка";
                }
                else
                {
                    return PartitionName;
                }
            }

        }

        /// <summary>
        /// Метод добавления узла
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        public void Add(TechRouteNode node)
        {
            _nodes.Add(node);
        }

        /// <summary>
        /// Дочерние узлы
        /// </summary>
        public IList<TechRouteNode> Children
        {
            get
            {
                return _nodes;
            }
        }

        /// <summary>
        /// Поиск в ширину узла
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="TechRouteNode"/>.
        /// </returns>
        public TechRouteNode Find(long id)
        {
            TechRouteNode nodeToFind = null;
            Queue<TechRouteNode> queue = new Queue<TechRouteNode>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                TechRouteNode elementFromQueue = queue.Dequeue();
                if (elementFromQueue.Id == id)
                {
                    nodeToFind = elementFromQueue;
                    queue.Clear();
                    break;
                }

                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (TechRouteNode child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
            return nodeToFind;
        }
    }
}