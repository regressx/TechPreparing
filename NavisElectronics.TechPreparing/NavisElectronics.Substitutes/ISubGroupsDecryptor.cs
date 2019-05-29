using System.Collections.Generic;

namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// Интерфейс для расшифровки набора групп допустимых заменителей
    /// </summary>
    public interface ISubGroupsDecryptor
    {
        /// <summary>
        /// Метод расшифровывает наборы групп допустимых заменителей, например, ставит "или" между группами и следит, чтобы позиции были на своих местах
        /// </summary>
        /// <param name="subGroups">
        /// Набор групп допустимых заменителей
        /// </param>
        void DescriptSubGroups(IList<SubstituteSubGroup> subGroups);

        /// <summary>
        /// Метод получает и расставляет информацию для актуального заменителя на основе данных из групп допустимых заменителей
        /// </summary>
        /// <param name="subGroups">
        /// Группы допустимых заменителей
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetInfoAboutGroupsForActualElement(IList<SubstituteSubGroup> subGroups);

        /// <summary>
        /// The set info about substitutes from another groups recursive.
        /// </summary>
        /// <param name="subGroups">
        /// The sub groups.
        /// </param>
        void SetInfoAboutSubstitutesFromAnotherGroupsRecursive(IList<SubstituteSubGroup> subGroups);
    }
}