namespace NavisElectronics.TechPreparation.Reports
{
    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// Интерфейс для создания отчета
    /// </summary>
    public interface IReport
    {
        /// <summary>
        /// Метод создания отчета
        /// </summary>
        /// <param name="mainElement">
        /// Элемент, отчет по которому мы хотим получить
        /// </param>
        /// <param name="path">
        /// Под каким именем сохранять
        /// </param>
        /// <param name="agent">
        /// Кто делает
        /// </param>
        void Create(Node mainElement, string path);
    }
}