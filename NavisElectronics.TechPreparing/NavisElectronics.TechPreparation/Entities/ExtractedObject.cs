// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtractedObject.cs" company="">
//   
// </copyright>
// <summary>
//   Сущность описывает извлекаемый из дерева объект
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Сущность описывает извлекаемый из дерева объект
    /// </summary>
    public class ExtractedObject
    {
        private ICollection<ExtractedObject> _elements;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedObject"/> class.
        /// </summary>
        public ExtractedObject()
        {
            _elements = new List<ExtractedObject>();
        }

        /// <summary>
        /// Идентификатор изделия
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Обозначение
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество в родителе
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Применяемость родителя
        /// </summary>
        /// 
        public double ParentUse { get; set; }

        /// <summary>
        /// Количество с применяемостью
        /// </summary>
        public double AmountWithUse { get; set; }
        
        /// <summary>
        /// Коэффициент запаса
        /// </summary>
        public double StockRate { get; set; }

        /// <summary>
        /// Итого
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Путь в дереве до изделия
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Единицы измерения
        /// </summary>
        public string MeasureUnits { get; set; }

        /// <summary>
        /// Тип корпуса элемента
        /// </summary>
        public string Case { get; set; }


        /// <summary>
        /// Тип технологического отхода
        /// </summary>
        public string WithdrawalType { get; set; }

        /// <summary>
        /// Флаш кооперации
        /// </summary>
        public bool CooperationFlag { get; set; }

        /// <summary>
        /// Является ли узел печатной платой
        /// </summary>
        public bool IsPcb { get; set; }

        /// <summary>
        /// Версия печатной платы
        /// </summary>
        public string PcbVersion { get; set; }

        /// <summary>
        /// Получает набор элементов, составляющих этот
        /// </summary>
        public ICollection<ExtractedObject> Elements
        {
            get { return _elements; }
        }

        /// <summary>
        /// Объем выборки
        /// </summary>
        public string SampleSize { get; set; }

        /// <summary>
        /// Ссылка на тех. процесс
        /// </summary>
        public string TechProcessReference { get; set; }

        /// <summary>
        /// Тех. задание
        /// </summary>
        public string TechTask { get; set; }

        /// <summary>
        /// Регистрация элемента
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        public void RegisterElement(ExtractedObject parent)
        {
            //if (_elements.ContainsKey(parent.Id))
            //{
            //    ExtractedObject parentFromDictionary = _elements[parent.Id];
                
            //}
            //else
            //{
            //    _elements.Add(parent.Id, parent);
            //}

            _elements.Add(parent);
        }


        public double CountElementsAmountWithStockRate()
        {
            double temp = 0;
            foreach (ExtractedObject element in _elements)
            {
                temp += element.AmountWithUse;
            }

            double stockRate = 1;
            if (StockRate > 1)
            {
                stockRate = StockRate;
            }

            return temp * stockRate;
        }

    }
}