using System.ComponentModel;

namespace NavisElectronics.ListOfCooperation.ViewModels
{
    /// <summary>
    /// Класс для задания параметров, которые будут отображаться в окне просмотра
    /// </summary>
    public class Parameter
    {
        [Browsable(false)]
        public string Name { get; set; }
        public string Value { get; set; }
    }
}