namespace NavisElectronics.IPS1C.IntegratorService.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Класс для передачи данных из базы данных в сервис
    /// </summary>
    public class DataBaseProduct
    {

        private readonly ICollection<DataBaseProduct> _products = null;


        public DataBaseProduct()
        {
            _products = new List<DataBaseProduct>();
        }

        public DataBaseProduct Parent { get; set; }

        public void Add(DataBaseProduct product)
        {
            _products.Add(product);
            product.Parent = this;
        }

        public void Remove(DataBaseProduct product)
        {
            _products.Remove(product);
        }

        public ICollection<DataBaseProduct> Products
        {
            get { return _products; }
        }

        /// <summary>
        /// Идентификационный номер изделия
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование изделия
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Обозначение
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Номер группы допустимых замен
        /// </summary>
        public int SubstituteGroupNumber { get; set; }

        /// <summary>
        /// Номер в группе допустимых замен
        /// </summary>
        public int SubstituteNumberInGroup { get; set; }

        /// <summary>
        /// Информация о замене
        /// </summary>
        public string SubstituteInfo { get; set; }

        /// <summary>
        /// Позиция в спецификации
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Тип изделия
        /// </summary>
        public int Type { get; set; }


        public string PartNumber { get; set; }

        public bool CooperationFlag { get; set; }
        public string PositionInSpecification { get; set; }
        public string PositionDesignation { get; set; }
        public string Note { get; set; }
        public string Supplier { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public string AmountAsString { get; set; }

        public string Class { get; set; }

        public string LastVersion { get; set; }

        /// <summary>
        /// Id объекта
        /// </summary>
        public long ObjectId { get; set; }

        public string MeasureUnits { get; set; }

        /// <summary>
        /// Тип корпуса
        /// </summary>
        public string Case { get; set; }
    }
}