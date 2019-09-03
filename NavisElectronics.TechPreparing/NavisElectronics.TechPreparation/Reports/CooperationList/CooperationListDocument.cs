using System.Collections.Generic;
using System.Collections.ObjectModel;
using NavisElectronics.TechPreparation.Entities;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Reports.CooperationList
{
    /// <summary>
    /// Документ ведомости кооперации
    /// </summary>
    public class CooperationListDocument
    {
        /// <summary>
        /// Обычные страницы
        /// </summary>
        //private IList<MyPageDescription<IntermechTreeElement>> _commonPages;

        /// <summary>
        /// Страницы с печатными платами
        /// </summary>
        //private IList<MyPageDescription<IntermechTreeElement> _pcbPages;

        /// <summary>
        /// Набор обычных элементов
        /// </summary>
        private IList<IntermechTreeElement> _commonObjects;

        /// <summary>
        /// Набор печатных плат
        /// </summary>
        private IList<IntermechTreeElement> _pcbObjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationListDocument"/> class.
        /// </summary>
        public CooperationListDocument()
        {
            //_commonPages = new List<MyPageDescription<IntermechTreeElement>>();
            //_pcbPages = new List<MyPageDescription<IntermechTreeElement>>();
            _commonObjects = new List<IntermechTreeElement>();
            _pcbObjects = new List<IntermechTreeElement>();
        }

        /// <summary>
        /// Общие объекты ведомости кооперации, как например, детали, сборочные единицы, комплекты
        /// </summary>
        public ICollection<IntermechTreeElement> CommonObjects
        {
            get { return new ReadOnlyCollection<IntermechTreeElement>(_commonObjects); }
        }

        /// <summary>
        /// Печатные платы
        /// </summary>
        public ICollection<IntermechTreeElement> PcbObjects
        {
            get { return new ReadOnlyCollection<IntermechTreeElement>(_pcbObjects); }
        }

        /// <summary>
        /// Присвоить обычные объекты
        /// </summary>
        /// <param name="commonObjects">Обычные объекты</param>
        public void SetCommonObjects(IList<IntermechTreeElement> commonObjects)
        {
            _commonObjects = commonObjects;
        }

        /// <summary>
        /// Присвоить объекты печатных плат
        /// </summary>
        /// <param name = "pcbObjects" >Печатные платы</ param >
        public void SetPcbObjects(IList<IntermechTreeElement> pcbObjects)
        {
            _pcbObjects = pcbObjects;
        }

        /// <summary>
        /// Получить страницы обычных данных
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<MyPageDescription<ExtractedObject>> GetCommonPages()
        //{
        //    return _commonPages;
        //}

        /// <summary>
        /// Получить страницы печатных плат
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<MyPageDescription<ExtractedObject>> GetPcbPages()
        //{
        //    return _pcbPages;
        //}
    }
}