using NavisElectronics.TechPreparation.Entities;

namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;

    using Intermech.Interfaces.Client;
    using Intermech.Navigator;

    /// <summary>
    /// Реализация интерфейса выбора созданной тех. подготовки
    /// </summary>
    public class TechPreparingSelector : ITechPreparingSelector<IdOrPath>
    {
        /// <summary>
        /// Показывает окно с заказами, из которых можно выбрать тех. подготовку
        /// </summary>
        /// <returns></returns>
        public IList<IdOrPath> Select()
        {

            long [] array = SelectionWindow.SelectObjects("Выбор заказа с тех. подготовкой",
                "Выберите нужный Вам заказ", 1019,
                SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);

            IList<IdOrPath> list = new List<IdOrPath>();

            foreach (long id in array)
            {
                IdOrPath path = new IdOrPath();
                path.Id = id;
                list.Add(path);
            }

            return list;
        }
    }
}