using System.ComponentModel;
using Intermech.Expressions.Exceptions;

namespace NavisElectronics.TechPreparation.ViewModels.TreeNodes
{
    using Aga.Controls.Tree;

    /// <summary>
    /// Наследник Node для компонента Aga.Controls.Tree. Наделяем его всякими свойствами для отображения, а затем строим с его помощью модель
    /// </summary>
    public class MyNode : Node
    {
        [Category("Основные данные из IPS")] 
        [DisplayName("Идентификатор версии объекта в IPS")]
        [Description("Уникальный идентификатор версии объекта")]
        public long Id { get; set; }


        [Category("Основные данные из IPS")] 
        [DisplayName("Идентификатор объекта в IPS")]
        [Description("Уникальный идентификатор объекта. Одному этому идентификатору может соответствовать множество идентификаторов версий объектов")]
        public long ObjectId { get; set; }


        /// <summary>
        /// Является ли печатной платой
        /// </summary>
        [Category("Печатная плата")] 
        [DisplayName("Печатная плата")]
        [Description("Является ли узел печатной платой")]
        public bool IsPcb { get; set; }

        /// <summary>
        /// Версия печатной платы
        /// </summary>
        [Category("Печатная плата")] 
        [DisplayName("Версия печатной платы")]
        [Description("Версия печатной платы, например V1, V2 или 1,2")]
        public int PcbVersion { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>

        [Category("Основные данные из IPS")] 
        [DisplayName("Наименование")]
        [Description("Наименование узла")]
        public string Name { get; set; }
        /// <summary>
        /// Обозначение
        /// </summary>
        [Category("Основные данные из IPS")] 
        [DisplayName("Обозначение")]
        [Description("Децимальный или любое другое уникальное обозначение изделия")]
        public string Designation { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        [Category("Основные конструкторские данные")] 
        [DisplayName("Количество по спецификации")]
        [Description("Количество по спецификации")]
        public string Amount { get; set; }



        /// <summary>
        /// Строка маршрута
        /// </summary>
        [Category("Технологические данные из IPS")] 
        [DisplayName("Строка маршрута")]
        [Description("Представляет собой строковое отображение цехозаходов из тех. процесса")]
        public string Route { get; set; }

        /// <summary>
        /// Примечание к маршруту
        /// </summary>

        [Category("Примечания")] 
        [DisplayName("Примечание технолога")]
        [Description("Заметки технолога")]
        public string Note { get; set; }
        
        /// <summary>
        /// Флаг кооперации
        /// </summary>
        [Category("Технологические данные из IPS")] 
        [DisplayName("Кооперация")]
        [Description("Будет ли изготавливаться на стороне или средствами предприятия-изготовителя")]
        public bool CooperationFlag { get; set; }

        /// <summary>
        /// Информация о заменах
        /// </summary>
        [Category("Основные конструкторские данные")] 
        [DisplayName("Информация о заменах")]
        [Description("Информация о заменах")]
        public string SubInfo { get; set; }

        /// <summary>
        /// Изготовитель
        /// </summary>
        [DisplayName("Изготовитель")]
        [Description("Изготовитель")]
        public string Agent { get; set; }
        
        /// <summary>
        /// Номер по порядку
        /// </summary>
        [Browsable(false)]
        public string NumberInOrder { get;set; }
        
        /// <summary>
        /// Тип изделия
        /// </summary>
        [Browsable(false)] 
        public int Type { get; set; }

        /// <summary>
        /// Уровень в дереве
        /// </summary>
        [Browsable(false)]
        public int Level { get; set; }

        /// <summary>
        /// Номер узла на уровне
        /// </summary>
        [Browsable(false)]
        public int NumberOnLevel { get; set; }

        /// <summary>
        /// Количество с применяемостью
        /// </summary>
        [Category("Основные конструкторские данные")] 
        [DisplayName("Количество с учетом применяемости")]
        [Description("Количество с учетом применяемости")]
        public double AmountWithUse { get; set; }

        /// <summary>
        /// True, если узел изготавливает несколько предприятий
        /// </summary>
        [Browsable(false)]
        public bool IsMultipleAgents { get; set; }

        /// <summary>
        /// True, если изготовитель узла отличается от выбранного по фильтру
        /// </summary>
        [Browsable(false)]
        public bool AnotherAgent { get; set; }

        /// <summary>
        /// Наличие внутрипроизводственной кооперации у узла
        /// </summary>
        [Category("Технологические данные")] 
        [DisplayName("Внутрипроизводственная кооперация")]
        [Description("Внутрипроизводственная кооперация")]
        public bool InnerCooperation { get; set; }

        /// <summary>
        /// Наличие внутрипроизводственной кооперации у узла
        /// </summary>
        public bool ContainsInnerCooperation { get; set; }

        /// <summary>
        /// Является ли узлом для комплектования
        /// </summary>
        [Category("Технологические данные")] 
        [DisplayName("Не изготавливать")]
        [Description("Не изготавливать в этом заказе")]
        public bool DoNotProduce { get; set; }

        [Category("Технологические данные из IPS")] 
        [DisplayName("Не изготавливать")]
        [Description("Не изготавливать в этом заказе")]
        public string TechTask { get; set; }

        [Category("Технологические данные из IPS")] 
        [DisplayName("Ссылка на групповой/типовой ТП")]
        [Description("Ссылка на групповой/типовой ТП")]
        public string TechProcessReference { get; set; }

        [Category("Технологические данные из IPS")] 
        [DisplayName("Технологический запас")]
        [Description("Технологический запас")]
        public double StockRate { get; set; }

        [Category("Технологические данные из IPS")] 
        [DisplayName("Объем выборки")]
        [Description("Объем выборки")]
        public string SampleSize { get; set; }

        /// <summary>
        /// Примечание по связи 
        /// </summary>
        [Category("Примечания")] 
        [DisplayName("Примечание конструктора")]
        [Description("Заметки конструктора")]
        public string RelationNote { get; set; }

    }
}