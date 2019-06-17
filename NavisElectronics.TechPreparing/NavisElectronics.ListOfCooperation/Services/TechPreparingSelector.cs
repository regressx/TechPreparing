namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;

    using Intermech.Interfaces.Client;
    using Intermech.Navigator;

    /// <summary>
    /// Реализация интерфейса выбора созданной тех. подготовки
    /// </summary>
    public class TechPreparingSelector : ITechPreparingSelector<long>
    {
        /// <summary>
        /// Показывает окно с заказами, из которых можно выбрать тех. подготовку
        /// </summary>
        /// <returns></returns>
        public IList<long> Select()
        {
            return SelectionWindow.SelectObjects("Выбор заказа с тех. подготовкой",
                "Выберите нужный Вам заказ", 1019,
                SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
        }
    }
}