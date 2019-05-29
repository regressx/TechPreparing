namespace NavisElectronics.ListOfCooperation.Reports
{
    /// <summary>
    /// Вид отчета
    /// </summary>
    public enum ReportType
    {
        /// <summary>
        /// Ведомость кооперации
        /// </summary>
        ListOfCooperation,

        /// <summary>
        /// Ведомость тех. маршрутов
        /// </summary>
        ListOfTechRoutes,

        /// <summary>
        /// Разделительная ведомость
        /// </summary>
        DividingList,

        /// <summary>
        /// Комплектовочная карта
        /// </summary>
        CompleteList,

        /// <summary>
        /// Полная комплектовочная карта с уникальными элементами и количествами на сборочную единицу и комплекты
        /// </summary>
        FullCompleteList
    }
}