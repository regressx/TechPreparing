namespace NavisElectronics.TechPreparation.Services
{
    using NavisArchiveWork.Data;
    using NavisArchiveWork.Model;

    /// <summary>
    /// Сервис, который открывает папки старого архива предприятия
    /// </summary>
    public class OpenFolderService
    {
        /// <summary>
        /// Класс, который умеет парсить обозначение, формировать полный путь и открывать папки
        /// </summary>
        private readonly Search _search;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFolderService"/> class.
        /// </summary>
        /// <param name="search">
        /// Класс для осуществления поиска по децимальному номеру в архиве предприятия
        /// </param>
        public OpenFolderService(Search search)
        {
            _search = search;
        }

        /// <summary>
        /// Метод для открытия папки по указанному обозначению
        /// </summary>
        /// <param name="designation">
        /// Обозначение узла
        /// </param>
        public void OpenFolder(string designation)
        {
            FileDesignation fileDesignation = _search.GetFileDesignation(designation);
            string path = _search.GetFullPath(fileDesignation);
            _search.StepToFolder(path);
        }

    }
}