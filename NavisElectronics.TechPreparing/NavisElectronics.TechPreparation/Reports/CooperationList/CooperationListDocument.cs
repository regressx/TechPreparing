namespace NavisElectronics.TechPreparation.Reports.CooperationList
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Interfaces.Entities;

    /// <summary>
    /// Документ ведомости кооперации
    /// </summary>
    public class CooperationListDocument
    {
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

    }
}