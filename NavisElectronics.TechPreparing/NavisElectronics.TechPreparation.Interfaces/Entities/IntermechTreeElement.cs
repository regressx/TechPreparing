namespace NavisElectronics.TechPreparation.Interfaces.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Enums;
    using Exceptions;
    using Newtonsoft.Json;
    using Substitutes;

    /// <summary>
    /// Узел дерева из IPS
    /// </summary>
    [Serializable]
    [JsonObject(IsReference = true)]
    public class IntermechTreeElement : IProduct, ICloneable
    {
        /// <summary>
        /// The _elements.
        /// </summary>
        private IList<IntermechTreeElement> _elements = new List<IntermechTreeElement>();

        /// <summary>
        /// Id версии объекта
        /// </summary>
        [DisplayName("IPS Id")]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [DisplayName("IPS object Id")]
        public long ObjectId { get; set; }

        /// <summary>
        /// Тип объекта
        /// </summary>
        [DisplayName("Тип")]
        [Browsable(false)]
        public int Type { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Обозначение
        /// </summary>
        [DisplayName("Обозначение")]
        public string Designation { get; set; }

        /// <summary>
        /// Номер изменения
        /// </summary>
        [DisplayName("Номер изменения")]
        public string ChangeNumber { get; set; }

        /// <summary>
        /// Флаг кооперации
        /// </summary>
        [DisplayName("Кооперация")]
        public bool CooperationFlag { get; set; }

        /// <summary>
        /// Делается ли узел по внутрипроизводственной кооперации
        /// </summary>
        [DisplayName("Внутрипроизводственная кооперация")]
        public bool InnerCooperation { get; set; }

        /// <summary>
        /// Содержит ли он другие узлы, делающиеся по внутрипроизводственной кооперации
        /// </summary>
        [Browsable(false)]
        public bool ContainsInnerCooperation { get; set; }

        /// <summary>
        /// Количество по спецификации
        /// </summary>
        [DisplayName("Количество в СП")]
        public float Amount { get; set; }

        /// <summary>
        /// Применяемость
        /// </summary>
        [DisplayName("Применяемость")]
        public int UseAmount { get; set; }

        /// <summary>
        /// Количество с применяемостью родителя
        /// </summary>
        [DisplayName("Количество с применяемостью")]
        public double AmountWithUse { get; set; }

        /// <summary>
        /// Всего на изделие, учитывая тех. запас
        /// </summary>
        [DisplayName("Всего на изд.")]
        public double TotalAmount { get; set; }

        /// <summary>
        /// Технологический запас
        /// </summary>
        [DisplayName("Тех. запас")]
        public double StockRate { get; set; }

        /// <summary>
        /// Объем выборки
        /// </summary>
        [DisplayName("Объем выборки")]
        public string SampleSize { get; set; }

        /// <summary>
        /// Ссылка на тех. процесс
        /// </summary>
        [DisplayName("ТТП")]
        public TechProcess TechProcessReference { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        [DisplayName("Примечание объекта")]
        public string Note { get; set; }


        /// <summary>
        /// Примечание
        /// </summary>
        [DisplayName("Примечание в составе")]
        public string RelationNote { get; set; }


        /// <summary>
        /// Номер группы допустимых замен
        /// </summary>
        [Browsable(false)]
        public int SubstituteGroupNumber { get; set; }

        /// <summary>
        /// Номер в группе допустимых замен
        /// </summary>
        [Browsable(false)]
        public int SubstituteNumberInGroup { get; set; }

        /// <summary>
        /// Рсшифровка доп. замен
        /// </summary>
        [DisplayName("Доп. замена")]
        public string SubstituteInfo { get; set; }

        /// <summary>
        /// Позиция в спецификации
        /// </summary>
        [DisplayName("Позиция")]
        public string Position { get; set; }

        /// <summary>
        /// Id отношения родитель - потомок
        /// </summary>
        [Browsable(false)]
        public long RelationId { get; set; }

        /// <summary>
        /// Примечание для ТП
        /// </summary>
        [Browsable(false)]
        public string RouteNote { get; set; }

        /// <summary>
        /// Позиционное обозначение
        /// </summary>
        [DisplayName("Поз. об.")]
        public string PositionDesignation { get; set; }

        /// <summary>
        /// Является ли узел печатной платой
        /// </summary>
        [DisplayName("PCB")]
        public bool IsPcb { get; set; }

        /// <summary>
        /// Версия печатной платы
        /// </summary>
        [DisplayName("Версия Pcb")]
        public byte PcbVersion { get; set; }

        /// <summary>
        /// Есть ли ТЗ на печатную плату
        /// </summary>
        [DisplayName("ТЗ на Pcb")]
        public string TechTask { get; set; }

        /// <summary>
        /// Является ли комплектовочным узлом
        /// </summary>
        [DisplayName("Участвует ли в комплектовании")]
        public bool IsToComplect { get; set; }

        /// <summary>
        /// Ссылка на родителя
        /// </summary>
        [Browsable(false)]
        public IntermechTreeElement Parent { get; set; }

        /// <summary>
        /// Здесь указан номер контрагента
        /// </summary>
        [Browsable(false)]
        public string Agent { get;set; }

        /// <summary>
        /// Здесь перечислены коды тех. маршрутов
        /// </summary>
        [Browsable(false)]
        public string TechRoute { get;set; }

        /// <summary>
        /// Состояние узла
        /// </summary>
        [Browsable(false)]
        public NodeStates NodeState { get; set; }


        /// <summary>
        /// Единицы изм.
        /// </summary>
        [DisplayName("Ед. изм.")]
        public string MeasureUnits { get; set; }

        /// <summary>
        /// Класс изделия
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// Идентификационный номер производителя
        /// </summary>
        public string PartNumber { get;set; }

        /// <summary>
        /// Корпус элемента
        /// </summary>
        public string Case { get; set; }

        /// <summary>
        /// Признак производства
        /// </summary>
        public bool ProduseSign { get; set; }

        /// <summary>
        /// Тип монтажа
        /// </summary>
        public string MountingType { get; set; }


        /// <summary>
        /// Материал из тех. требований или вспомогательный материал, или основной
        /// </summary>
        public string MaterialRegisteredIn { get; set; }

        /// <summary>
        /// Наименование шага жизненного цикла
        /// </summary>
        public string LifeCycleStep { get; set; }

        /// <summary>
        /// Тип связи
        /// </summary>
        public string RelationName { get; set; }

        /// <summary>
        /// Первичная применяемость
        /// </summary>
        public string FirstUse { get; set; }


        /// <summary>
        /// Литера
        /// </summary>
        public string Letter { get; set; }


        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="index">
        /// Индекс потомка
        /// </param>
        /// <returns>
        /// The <see cref="IntermechTreeElement"/>.
        /// </returns>
        public IntermechTreeElement this [int index]
        {
            get { return _elements[index]; }
        }

        /// <summary>
        /// Ищет коллекцию узлов по id
        /// </summary>
        /// <param name="objectId">Id объекта</param>
        /// <returns>Возвращает коллекцию узлов, входящих во всё дерево. Если узлов нет, то вернет пустую коллекцию</returns>
        public IList<IntermechTreeElement> Find(long objectId)
        {
            IList<IntermechTreeElement> elementCollection = new List<IntermechTreeElement>();
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                if (elementFromQueue.ObjectId == objectId)
                {
                    elementCollection.Add(elementFromQueue);
                }
                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (var element in elementFromQueue.Children)
                    {
                        queue.Enqueue(element);
                    }
                }
            }
            return elementCollection;
        }


        /// <summary>
        /// Ищет в ширину первый попавшийся элемент с указанным Id
        /// </summary>
        /// <param name="objectId">Идентификатор объекта</param>
        /// <returns>Первый попавшийся узел</returns>
        public IntermechTreeElement FindFirst(long objectId)
        {
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                if (elementFromQueue.ObjectId == objectId)
                {
                    return elementFromQueue;
                }
                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (var element in elementFromQueue.Children)
                    {
                        queue.Enqueue(element);
                    }
                }
            }
            throw new NullReferenceException("Нет такого элемента в дереве");
        }

        /// <summary>
        /// Ищет в ширину элемент по его пути, состоящем из идентификаторов версий объектов
        /// </summary>
        /// <param name="path">
        /// Путь, состоящий из идентификаторов версий объектов, например 1111\\2222\\3333
        /// </param>
        /// <returns>
        /// <see cref="IntermechTreeElement"/>
        /// </returns>
        public IntermechTreeElement FindByVersionIdPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            IList<string> lines = path.Split('\\');
            Queue<long> queue = new Queue<long>();
            foreach (string s in lines)
            {
                queue.Enqueue(long.Parse(s));
            }

            // уберем первый элемент
            queue.Dequeue();
            Stack<IntermechTreeElement> stackElements = new Stack<IntermechTreeElement>();
            FindNodeRecursiveByVersionIdPath(queue, this, stackElements);

            if (stackElements.Count != lines.Count)
            {
                throw new NullReferenceException(string.Format("Элемент {0} не был найден в дереве по такому пути", path));
            }

            return stackElements.Peek();
        }

        /// <summary>
        /// Ищем элемент в дереве по его пути
        /// </summary>
        /// <param name="path">Путь, состоящий из последовательностей номеров объектов, например, 1111\\222\\333</param>
        /// <returns><see cref="IntermechTreeElement"/></returns>
        public IntermechTreeElement FindByObjectIdPath(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            IList<string> lines = path.Split('\\');
            Queue<long> queue = new Queue<long>();
            foreach (string s in lines)
            {
                queue.Enqueue(long.Parse(s));
            }

            // уберем первый элемент
            queue.Dequeue();
            Stack<IntermechTreeElement> stackElements = new Stack<IntermechTreeElement>();
            FindNodeByObjectIdRecursive(queue, this, stackElements);

            if (stackElements.Count != lines.Count)
            {
                throw new NullReferenceException(string.Format("Элемент {0} не был найден в дереве по такому пути"));
            }

            return stackElements.Peek();
        }


        /// <summary>
        /// Коллекция потомков этого узла. Указан List, потому что не могу нормально сериализовать в другие
        /// </summary>
        [Browsable(false)]
        public List<IntermechTreeElement> Children
        {
            get { return (List<IntermechTreeElement>)_elements; }
            set { _elements = value; }

        }

        /// <summary>
        /// Метод добавления элемента в узел
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public void Add(IntermechTreeElement element)
        {
            element.Parent = this;
            _elements.Add(element);
        }

        /// <summary>
        /// Удаление узла
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public void Remove(IntermechTreeElement element)
        {

            bool found = false;
            
            // ищем линейно
            foreach (IntermechTreeElement node in _elements)
            {
                if (element.ObjectId == node.ObjectId)
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                _elements.Remove(element);
            }

        }




        public void RemoveAt(int index)
        {
            _elements.RemoveAt(index);
        }


        public void Clear()
        {
            _elements.Clear();
        }

        /// <summary>
        /// Метод умеет получать путь элемента, состоящий из номеров объектов, в дереве 
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFullPathByObjectId()
        {
            Stack<long> stack = new Stack<long>();
            stack.Push(this.ObjectId);

            GetFullPathByObjectIdRecursive(stack, this);

            StringBuilder sb = new StringBuilder();

            foreach (long value in stack)
            {
                sb.AppendFormat("{0}\\", value);
            }

            return sb.ToString().TrimEnd('\\');
        }

        public string GetFullPathByVersionId()
        {
            Stack<long> stack = new Stack<long>();
            stack.Push(this.Id);

            GetFullPathRecursive(stack, this);

            StringBuilder sb = new StringBuilder();

            foreach (long value in stack)
            {
                sb.AppendFormat("{0}\\", value);
            }

            return sb.ToString().TrimEnd('\\');
        }

        private void GetFullPathRecursive(Stack<long> stack, IntermechTreeElement element)
        {
            if (element.Parent != null)
            {
                IntermechTreeElement parent = element.Parent;
                stack.Push(parent.Id);
                GetFullPathRecursive(stack, parent);
            }
        }

        /// <summary>
        /// Получает deep Copy этого объекта
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Clone()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return formatter.Deserialize(ms);
            }
        }

        private void FindNodeRecursiveByVersionIdPath(Queue<long> queue, IntermechTreeElement elementWhereFind, Stack<IntermechTreeElement> stackElements)
        {
            stackElements.Push(elementWhereFind);
            while (queue.Count > 0)
            {
                long childId = queue.Dequeue();
                bool foundFlag = false;
                foreach (IntermechTreeElement childElement in elementWhereFind.Children)
                {
                    if (childId == childElement.Id)
                    {
                        foundFlag = true;
                        FindNodeRecursiveByVersionIdPath(queue, childElement, stackElements);
                    }
                }

                if (!foundFlag)
                {
                    throw new TreeNodeNotFoundException(string.Format("Элемент с номером {0} в узле {1} не найден", childId, elementWhereFind));
                }
            }
        }

        private void GetFullPathByObjectIdRecursive(Stack<long> stack, IntermechTreeElement element)
        {
            if (element.Parent != null)
            {
                IntermechTreeElement parent = element.Parent;
                stack.Push(parent.ObjectId);
                GetFullPathByObjectIdRecursive(stack, parent);
            }
        }

        private void FindNodeByObjectIdRecursive(Queue<long> queue, IntermechTreeElement elementWhereFind, Stack<IntermechTreeElement> stackElements)
        {
            stackElements.Push(elementWhereFind);
            while (queue.Count > 0)
            {
                long childId = queue.Dequeue();
                bool foundFlag = false;
                foreach (IntermechTreeElement childElement in elementWhereFind.Children)
                {
                    if (childId == childElement.ObjectId)
                    {
                        foundFlag = true;
                        FindNodeByObjectIdRecursive(queue, childElement, stackElements);
                    }
                }

                if (!foundFlag)
                {
                    throw new TreeNodeNotFoundException(string.Format("Элемент с номером {0} в узле {1} не найден", childId, elementWhereFind));
                }
            }
        }
    }
}