namespace NavisElectronics.Orders.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Kernel.Search;
    using Reports;

    public class SupportingRepository
    { 
        /// <summary>
        /// Получает коллекцию цветов для уровней состава изделия
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public ICollection<LevelColor> GetHexStringColorsCollection()
        {
            List<LevelColor> collection = new List<LevelColor>();
            long id = 1690368;
            DataTable table;

            using (SessionKeeper keeper = new SessionKeeper())
            {
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
                    new ColumnDescriptor(18090,
                        AttributeSourceTypes.Object,
                        ColumnContents.Text,
                        ColumnNameMapping.Index,
                        SortOrders.NONE,
                        0), // цвет в hex String
                    new ColumnDescriptor(15294,
                        AttributeSourceTypes.Object,
                        ColumnContents.Text,
                        ColumnNameMapping.Index,
                        SortOrders.ASC,
                        0), // уровень вложенности
                };

                // забрать содержимое папки Imbase
                table = compositionService.LoadComposition(
                    keeper.Session.SessionGUID,
                    id,
                    1003,
                    new List<ColumnDescriptor>(columns),
                    string.Empty,
                    1095);
            }

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    LevelColor levelColor = new LevelColor();
                    levelColor.Level = Convert.ToInt32(row[3]);
                    if (row[2] != DBNull.Value)
                    {
                        levelColor.HexColorString = "FF" + Convert.ToString(row[2]);
                    }
                    collection.Add(levelColor);
                }
            }

            Comparison<LevelColor> comparison = (color, levelColor) =>
                {
                    return color.Level.CompareTo(levelColor.Level);
                };

            collection.Sort(comparison);

            return collection;

        }



                /// <summary>
        /// Получает коллекцию цветов для уровней состава изделия
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public ICollection<DocumentType> GetDocumentTypes()
        {
            ICollection<DocumentType> documentTypes = new List<DocumentType>();

            long id = 1689979;
            DataTable table;

            using (SessionKeeper keeper = new SessionKeeper())
            {
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

                    new ColumnDescriptor(13,
                        AttributeSourceTypes.Object,
                        ColumnContents.Text,
                        ColumnNameMapping.Index,
                        SortOrders.NONE,
                        0), // Краткое наименование
                };

                // забрать содержимое папки Imbase
                table = compositionService.LoadComposition(
                    keeper.Session.SessionGUID,
                    id,
                    1003,
                    new List<ColumnDescriptor>(columns),
                    string.Empty,
                    1095);
            }

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    DocumentType docType = new DocumentType();
                    if (row[2] != DBNull.Value)
                    {
                        docType.Name = Convert.ToString(row[2]);
                    }

                    if (row[3] != DBNull.Value)
                    {
                        docType.ShortName = Convert.ToString(row[3]);
                    }

                    documentTypes.Add(docType);
                }
            }


            return documentTypes;
        }
    }
}