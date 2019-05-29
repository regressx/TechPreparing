using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NavisElectronics.IPS1C.IntegratorService.Exceptions;


namespace NavisElectronics.IPS1C.IntegratorService.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Сущность для передачи данных из сервиса в другие приложения. Все поля намеренно являются строками в надежде, что будут везде поняты однозначно
    /// </summary>
    [DataContract(IsReference = true)]
    [Serializable]
    public class ProductTreeNode : ICloneable
    {
        private ICollection<ProductTreeNode> _products;

        public ProductTreeNode()
        {
            _products = new List<ProductTreeNode>();
        }

        /// <summary>
        /// IPS Id
        /// </summary>
        [DataMember]
        public string Id1 { get; set; }

        /// <summary>
        /// IPS Id
        /// </summary>
        [DataMember]
        public string ObjectId1 { get; set; }

        /// <summary>
        /// Номер типа из IPS
        /// </summary>
        [DataMember]
        public string Type1 { get; set; }
        
        /// <summary>
        /// Обозначение
        /// </summary>
        [DataMember]
        public string Designation1 { get; set; }


        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember]
        public string Name1 { get; set; }


        /// <summary>
        /// Применяемость
        /// </summary>
        [DataMember]
        public string Amount1 { get; set; }


        /// <summary>
        /// Идентификационный номер производителя
        /// </summary>
        [DataMember]
        public string PartNumber1 { get;set; }


        /// <summary>
        /// Флаг кооперации.
        /// </summary>
        [DataMember]
        public string CooperationFlag1 { get; set; }

        /// <summary>
        /// Номер группы допустимых замен
        /// </summary>
        [DataMember]
        public string SubstituteGroup1 { get; set; }


        /// <summary>
        /// Номер в группе допустимых замен
        /// </summary>
        [DataMember]
        public string NumberInSubstituteGroup1 { get; set; }

        /// <summary>
        /// Информация о доп. заменах
        /// </summary>
        [DataMember]
        public string SubstituteInfo1 { get; set; }

        /// <summary>
        /// Позиция в спецификации
        /// </summary>
        [DataMember]
        public string PositionInSpecification1 { get; set; }

        /// <summary>
        /// Позиционное обозначение
        /// </summary>
        [DataMember]
        public string PositionDesignation1 { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        [DataMember]
        public string Note1 { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        [DataMember]
        public string Supplier1 { get; set; }

        /// <summary>
        /// Единицы изменения
        /// </summary>
        [DataMember]
        public string MeasureUnits1 { get; set; }

        /// <summary>
        /// Класс изделия
        /// </summary>
        [DataMember]
        public string Class1 { get; set; }

        [DataMember]
        public string LastVersion1 { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        [DataMember]
        public string Agent { get; set; }

        /// <summary>
        /// Тех. запас
        /// </summary>
        [DataMember]
        public string StockRate { get; set; }

        /// <summary>
        /// Объем выборки
        /// </summary>
        [DataMember]
        public string SampleSize { get; set; }

        /// <summary>
        /// Признак того, что узел должен участвовать в комплектовании
        /// </summary>
        [DataMember]
        public string IsComplectNodeComponent { get; set; }

        /// <summary>
        /// Признак того, что узел является печатной платой
        /// </summary>
        [DataMember]
        public string IsPCB { get; set; }

        /// <summary>
        /// Версия печатной платы
        /// </summary>
        [DataMember]
        public string PcbVersion { get; set; }

        /// <summary>
        /// Сведения о ТЗ на печатную плату
        /// </summary>
        [DataMember]
        public string TechTaskOnPCB { get; set; }

        /// <summary>
        /// Тип технологического отхода
        /// </summary>
        [DataMember]
        public string TypeOfWithDrawal { get; set; }

        /// <summary>
        /// Тип корпуса
        /// </summary>
        [DataMember]
        public string Case { get; set; }


        [DataMember]
        public ProductTreeNode Parent { get; set; }

        [DataMember]
        public ICollection<ProductTreeNode> Products
        {
            get { return _products; }
        }

        public void Add(ProductTreeNode childNode)
        {
            _products.Add(childNode);
            childNode.Parent = this;
        }

        public void Remove(ProductTreeNode nodeToRemove)
        {
            _products.Remove(nodeToRemove);
        }

        /// <summary>
        /// Ищет в ширину элемент по его пути
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductTreeNode Find(string path)
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
            Stack<ProductTreeNode> stackElements = new Stack<ProductTreeNode>();
            FindNodeRecursive(queue, this, stackElements);

            if (stackElements.Count != lines.Count)
            {
                throw new TreeNodeNotFoundException("Элемент не был найден в дереве по такому пути");
            }

            return stackElements.Peek();

        }


        private void FindNodeRecursive(Queue<long> queue, ProductTreeNode elementWhereFind, Stack<ProductTreeNode> stackElements)
        {
            stackElements.Push(elementWhereFind);
            while (queue.Count > 0)
            {
                long childId = queue.Dequeue();
                bool foundFlag = false;
                foreach (ProductTreeNode childElement in elementWhereFind.Products)
                {
                    if (childId == Convert.ToInt64(childElement.ObjectId1))
                    {
                        foundFlag = true;
                        FindNodeRecursive(queue, childElement, stackElements);
                    }
                }

                if (!foundFlag)
                {
                    throw new NullReferenceException(string.Format("Элемент с номером {0} в узле {1} не найден", childId, elementWhereFind));
                }
            }
        }

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
    }
}