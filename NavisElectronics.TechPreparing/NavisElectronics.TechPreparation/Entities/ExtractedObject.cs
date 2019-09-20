﻿// --------------------------------------------------------------------------------------------------------------------
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
    public class ExtractedObject<T> where T: class
    {
        private T _intermechTreeElement;
        private ICollection<ExtractedObject<T>> _elements;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedObject{T}"/> class.
        /// </summary>
        /// <param name="intermechTreeElement">
        /// The intermech Tree Element.
        /// </param>
        public ExtractedObject(T intermechTreeElement)
        {
            _intermechTreeElement = intermechTreeElement;
            _elements = new List<ExtractedObject<T>>();
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
        public ICollection<ExtractedObject<T>> Elements
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

        public string TechRoute { get; set; }


        public T TreeElement
        {
            get { return _intermechTreeElement; }
        }

        /// <summary>
        /// Регистрация элемента
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        public void RegisterElement(ExtractedObject<T> parent)
        {
            _elements.Add(parent);
        }


        public double CountElementsAmountWithStockRate()
        {
            double temp = 0;
            foreach (ExtractedObject<T> element in _elements)
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