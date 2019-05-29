using System;

namespace NavisElectronics.ListOfCooperation.Entities
{
    /// <summary>
    /// Класс, описывающий некоторые данные тех. процесса, такие как Id и наименование
    /// </summary>
    [Serializable]
    public class TechProcess
    {
        public long Id { get;set; }
        public string Name { get; set; }

        public static TechProcess Empty
        {
            get { return null; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}