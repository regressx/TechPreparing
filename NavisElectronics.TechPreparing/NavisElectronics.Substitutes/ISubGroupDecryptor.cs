namespace NavisElectronics.Substitutes
{
    /// <summary>
    /// Интерфейс для расшифровки набора элементов в одной группе допустимых заменителей
    /// </summary>
    public interface ISubGroupDecryptor
    {
        /// <summary>
        /// Метод расшифровывает набор элементов в одной группе допустимых заменителей
        /// </summary>
        /// <param name="group">Группа допустимых заменителей</param>
        /// <param name="showPositions">
        /// Переменная, где указываем, показываем позицию или полное имя элемента
        /// </param>
        void DecryptElements(SubstituteSubGroup group, bool showPositions);

        /// <summary>
        /// Метод получает информацию о наборе допустимых заменителей в группе и составляет строку для актуального заменителя, чтобы получилось,
        /// например: допускается замена на ... и список позиций, определенный в группе допустимых заменителей.
        /// </summary>
        /// <param name="group">группа допустимых заменителей</param>
        /// <param name="showPositions">
        /// Переменная, где указываем, показываем позицию или полное имя элемента
        /// </param>
        /// <returns>Возращает результирующую строку с перечнем компонентов, составляющих группу допустимых заменителей</returns>
        string GetInfoAboutElementsForActualElement(SubstituteSubGroup group, bool showPositions);
    }
}