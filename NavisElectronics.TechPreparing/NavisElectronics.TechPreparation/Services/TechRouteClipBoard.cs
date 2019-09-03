namespace NavisElectronics.TechPreparation.Services
{
    /// <summary>
    /// Буфер для тех. строки маршрута
    /// </summary>
    public class TechRouteClipBoard
    {
        /// <summary>
        /// Внешний вид строки для отображения
        /// </summary>
        public string RouteForView { get; set; }
        
        /// <summary>
        /// Внешний вид строки для записи в базу
        /// </summary>
        public string RouteForDatabase { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }
    }
}