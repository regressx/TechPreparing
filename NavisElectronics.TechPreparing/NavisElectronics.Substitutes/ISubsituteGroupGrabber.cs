using System.Collections.Generic;

namespace NavisElectronics.Substitutes
{
    public interface ISubsituteGroupGrabber
    {
        /// <summary>
        /// Получаем список групп допустимых замен
        /// </summary>
        ICollection<SubstituteGroup> SubGroups { get; }

        /// <summary>
        /// Получает набор групп заменителей из всех входящих
        /// </summary>
        void GetGroups();
    }
}