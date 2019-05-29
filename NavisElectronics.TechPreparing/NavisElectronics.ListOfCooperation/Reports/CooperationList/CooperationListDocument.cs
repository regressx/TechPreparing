namespace NavisElectronics.ListOfCooperation.Reports.CooperationList
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Документ ведомости кооперации
    /// </summary>
    public class CooperationListDocument
    {
        /// <summary>
        /// Обычные страницы
        /// </summary>
        private IList<MyPageDescription<ExtractedObject>> _commonPages;

        /// <summary>
        /// Страницы с печатными платами
        /// </summary>
        private IList<MyPageDescription<ExtractedObject>> _pcbPages;

        /// <summary>
        /// Набор обычных элементов
        /// </summary>
        private ICollection<ExtractedObject> _commonObjects;

        /// <summary>
        /// Набор печатных плат
        /// </summary>
        private ICollection<ExtractedObject> _pcbObjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationListDocument"/> class.
        /// </summary>
        public CooperationListDocument()
        {
            _commonPages = new List<MyPageDescription<ExtractedObject>>();
            _pcbPages = new List<MyPageDescription<ExtractedObject>>();
            _commonObjects = new List<ExtractedObject>();
            _pcbObjects = new List<ExtractedObject>();
        }

        /// <summary>
        /// Общие объекты ведомости кооперации, как например, детали, сборочные единицы, комплекты
        /// </summary>
        public ICollection<ExtractedObject> CommonObjects
        {
            get { return _commonObjects; }
        }

        /// <summary>
        /// Печатные платы
        /// </summary>
        public ICollection<ExtractedObject> PcbObjects
        {
            get { return _pcbObjects; }
        }


        /// <summary>
        /// Присвоить страницы с обычными объектами
        /// </summary>
        /// <param name="pages"></param>
        public void SetCommonPages(ICollection<MyPageDescription<ExtractedObject>> pages)
        {
            _commonPages = new List<MyPageDescription<ExtractedObject>>(pages);
        }

        /// <summary>
        /// Присвоить страницы с печатными платами
        /// </summary>
        /// <param name="pages"></param>
        public void SetPcbPages(ICollection<MyPageDescription<ExtractedObject>> pages)
        {
            _pcbPages = new List<MyPageDescription<ExtractedObject>>(pages);
        }


        /// <summary>
        /// Присвоить обычные объекты
        /// </summary>
        /// <param name="commonObjects"></param>
        public void SetCommonObjects(ICollection<ExtractedObject> commonObjects)
        {
            _commonObjects = commonObjects;
        }

        /// <summary>
        /// Присвоить объекты печатных плат
        /// </summary>
        /// <param name="pcbObjects"></param>
        public void SetPcbObjects(ICollection<ExtractedObject> pcbObjects)
        {
            _pcbObjects = pcbObjects;
        }

        /// <summary>
        /// Получить страницы обычных данных
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MyPageDescription<ExtractedObject>> GetCommonPages()
        {
            return _commonPages;
        }

        /// <summary>
        /// Получить страницы печатных плат
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MyPageDescription<ExtractedObject>> GetPcbPages()
        {
            return _pcbPages;
        }
    }
}