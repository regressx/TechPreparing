using System.Collections.Generic;
using System.Text;

namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// Группа допустимых заменителей
    /// </summary>
    public class SubstituteSubGroup
    {
        private int _number;
        private List<IProduct> _substitutes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubstituteSubGroup"/> class.
        /// </summary>
        public SubstituteSubGroup(int number)
        {
            _number = number;
            _substitutes = new List<IProduct>();
        }

        /// <summary>
        /// Получает список заменителей в группе
        /// </summary>
        public List<IProduct> Subsitutes
        {
            get { return _substitutes; }
        }

        /// <summary>
        /// Номер группы заменителей
        /// </summary>
        public int Number
        {
            get { return _number; }
        }

        public void AppendGroupInfoToElement(string infoFromAnotherGroup, int elementNumber)
        {
            string newInfo = string.Format("{0} {1}", _substitutes[elementNumber].SubstituteInfo, infoFromAnotherGroup);
            _substitutes[elementNumber].SubstituteInfo = newInfo;
        }

        public string GetResultSubstituteInfoFromThisGroup()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _substitutes.Count; i++)
            {
                sb.Append(_substitutes[i].Position);
                if (i != _substitutes.Count - 1)
                {
                    sb.Append("совместно с поз. ");
                }
            }
            return string.Format("{0}", sb.ToString());
        }
    }
}