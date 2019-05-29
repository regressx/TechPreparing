using System.Collections.Generic;

namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// Группа заменителей
    /// </summary>
    public class SubstituteGroup
    {
        private int _number;

        /// <summary>
        /// Список актуальных заменителей
        /// </summary>
        private IList<IProduct> _actualSub;

        /// <summary>
        /// Список групп допустимых заменителей
        /// </summary>
        private IList<SubstituteSubGroup> _subGroups;


        /// <summary>
        /// Initializes a new instance of the <see cref="SubstituteGroup"/> class.
        /// </summary>
        /// <param name="number">
        /// The number.
        /// </param>
        public SubstituteGroup(int number)
        {
            _number = number;
            _actualSub = new List<IProduct>();
            _subGroups = new List<SubstituteSubGroup>();
        }

        /// <summary>
        /// Список групп допустимых заменителей
        /// </summary>
        public IList<SubstituteSubGroup> SubGroups
        {
            get { return _subGroups; }
        }

        /// <summary>
        /// Список актуальных заменителей
        /// </summary>
        public IList<IProduct> ActualSub
        {
            get { return _actualSub; }
        }

        public int Number
        {
            get { return _number; }
        }
    }
}