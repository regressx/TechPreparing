namespace NavisElectronics.Substitutes
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The sub groups decriptor.
    /// </summary>
    public class SubGroupsDecryptor : ISubGroupsDecryptor
    {
        /// <summary>
        /// Интерфейс для расшифровки одной группы допустимых заменителей
        /// </summary>
        private readonly ISubGroupDecryptor _subGroupDecryptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubGroupsDecryptor"/> class.
        /// </summary>
        /// <param name="subGroupDecryptor">
        /// The sub group decryptor.
        /// </param>
        public SubGroupsDecryptor(ISubGroupDecryptor subGroupDecryptor)
        {
            _subGroupDecryptor = subGroupDecryptor;
        }


        private int _currentGroup = 0;

        /// <summary>
        /// Метод расшифровывает наборы групп допустимых заменителей, например, ставит "или" между группами и следит, чтобы позиции были на своих местах
        /// </summary>
        /// <param name="subGroups">
        /// Набор групп допустимых заменителей
        /// </param>
        public void DescriptSubGroups(IList<SubstituteSubGroup> subGroups)
        {
            // проходимся по группам допустимых замен и расшифровываем данные
            for (int i = 0; i < subGroups.Count; i++)
            {
                _subGroupDecryptor.DecryptElements(subGroups[i], false);
            }
        }

        public void SetInfoAboutSubstitutesFromAnotherGroupsRecursive(IList<SubstituteSubGroup> subGroups)
        {
            if (subGroups.Count == 0)
            {
                _currentGroup = 0;
                return;
            }

            string temp = subGroups[_currentGroup].GetResultSubstituteInfoFromThisGroup();
            if (subGroups.Count > 1)
            {
                for (int i = 0; i < subGroups.Count; i++)
                {
                    if (i == _currentGroup)
                    {
                        continue;
                    }
                    for (int j = 0; j < subGroups[i].Subsitutes.Count; j++)
                    {
                        subGroups[i].AppendGroupInfoToElement("или взамен " + temp, j);
                    }
                }
                if (_currentGroup != subGroups.Count - 1)
                {
                    _currentGroup++;
                    SetInfoAboutSubstitutesFromAnotherGroupsRecursive(subGroups);
                }
            }

            _currentGroup = 0;
        }


        /// <summary>
        /// Метод получает и расставляет информацию для актуального заменителя на основе данных из групп допустимых заменителей
        /// </summary>
        /// <param name="subGroups">
        /// Группы допустимых заменителей
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetInfoAboutGroupsForActualElement(IList<SubstituteSubGroup> subGroups)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (SubstituteSubGroup subGroup in subGroups)
            {
                i++;
                string str = _subGroupDecryptor.GetInfoAboutElementsForActualElement(subGroup,false);
                sb.Append(str);
                if (i < subGroups.Count)
                {
                    sb.Append(" или ");
                }
            }
            return sb.ToString();
        }
    }
}
