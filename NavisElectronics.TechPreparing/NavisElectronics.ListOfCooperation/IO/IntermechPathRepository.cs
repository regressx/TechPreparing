namespace NavisElectronics.TechPreparation.IO
{
    using System.Collections.Generic;
    using System.Data;

    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Kernel.Search;

    /// <summary>
    /// Класс умеет получать сетевые пути из IMBase
    /// </summary>
    public class IntermechPathRepository : NavisArchiveWork.Data.IRepository
    {
        private const int IdOfNodeCatalog = 1481505;
        private const int descriptionAttribute = 7;

        IList<PathContainer> _lines = new List<PathContainer>();


        /// <summary>
        /// Initializes a new instance of the <see cref="IntermechPathRepository"/> class.
        /// </summary>
        public IntermechPathRepository()
        {
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject pathes = keeper.Session.GetObject(IdOfNodeCatalog);
                
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(
                        typeof(ICompositionLoadService));

                // Получим состав по связи Простая связь с сортировкой
                // Необходимые колонки
                ColumnDescriptor[] columns =
                {
                    new ColumnDescriptor(
                        (int)ObligatoryObjectAttributes.F_OBJECT_ID,
                        AttributeSourceTypes.Object,
                        ColumnContents.Text,
                        ColumnNameMapping.Index,
                        SortOrders.NONE,
                        0), // идентификатор версии объекта
                    new ColumnDescriptor(
                        (int)ObligatoryObjectAttributes.F_OBJECT_TYPE,
                        AttributeSourceTypes.Object,
                        ColumnContents.Text,
                        ColumnNameMapping.Index,
                        SortOrders.NONE,
                        0), // тип объекта
                    new ColumnDescriptor(10,
                        AttributeSourceTypes.Object,
                        ColumnContents.Text,
                        ColumnNameMapping.Index,
                        SortOrders.NONE,
                        0), // наименование

                    new ColumnDescriptor(descriptionAttribute,
                        AttributeSourceTypes.Object,
                        ColumnContents.ID,
                        ColumnNameMapping.Index,
                        SortOrders.NONE,
                        0), // описание
                };


                DataTable articlesComposition = compositionService.LoadComposition(
                    keeper.Session.SessionGUID,
                    pathes.ObjectID,
                    1003,
                    new List<ColumnDescriptor>(columns),
                    string.Empty,
                    1095);

                foreach (DataRow row in articlesComposition.Rows)
                {
                    PathContainer pathContainer= new PathContainer((string)row[2], (string)row[3]);
                    _lines.Add(pathContainer);
                }

            }
        }

        /// <summary>
        /// Метод получает часть пути к папке, например, ТДЦК переходит к tdck\\tdck
        /// </summary>
        /// <param name="value">
        /// Здесь value - это обозначение производителя, например, ТДЦК, АПМА и прочее
        /// </param>
        /// <returns>
        /// Возвращает содержимое атрибута Краткое наименование папок раздела IMbase Сетевой путь к старому архиву
        /// </returns>
        public string GetManufacturePath(string value)
        {
            // обычный линейный поиск. Быстро и просто, потому что мало элементов
            foreach (PathContainer line in _lines)
            {
                if (line.Name.Equals(value))
                {
                    return line.PseudoName;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Возвращает сетевой путь к старому архиву предприятия
        /// </summary>
        /// <returns>
        /// Возвращает содержимое атрибута Описание папок раздела Сетевой путь к старому архиву из IMBase
        /// </returns>
        public string GetNetPath()
        {
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject netPath = keeper.Session.GetObject(IdOfNodeCatalog);
                IDBAttribute netPathAttribute = netPath.GetAttributeByID(descriptionAttribute);
                if (netPathAttribute != null)
                {
                    return netPathAttribute.AsString;
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// Вложенный класс для хранения некоторой структуры децимального номера
        /// </summary>
        class PathContainer
        {
            private string _name;
            private string _pseudoName;

            public PathContainer(string name, string pseudoName)
            {
                _name = name;
                _pseudoName = pseudoName;
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public string PseudoName
            {
                get { return _pseudoName; }
                set { _pseudoName = value; }
            }
        }

    }
}