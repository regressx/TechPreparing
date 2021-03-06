﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationListPlugin.cs" company="">
//   
// </copyright>
// <summary>
//   Плагин для работы с ведомостью кооперации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Intermech;
using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using NavisElectronics.Orders;

namespace NavisElectronics.TechPreparation
{
    using System;
    /// <summary>
    /// Плагин для работы с ведомостью кооперации
    /// </summary>
    public class CooperationListPlugin : IPackage
    {
        /// <summary>
        /// Метод загрузки дополнения для работы с тех. ведомостями
        /// </summary>
        /// <param name="serviceProvider">
        /// The service provider.
        /// </param>
        public void Load(IServiceProvider serviceProvider)
        {
            IFactory factory = ApplicationServices.Container.GetService(typeof(IFactory)) as IFactory;
            if (factory != null)
            {

                MenuTemplate menu = factory.ContextMenuTemplate;

                // Нет меню - свой пункт в нём не создаём - выход
                if (menu == null)
                {
                    return;
                }

                menu.Nodes.Add(new MenuTemplateNode("EditOrder", "Редактировать заказ", -1, 2, int.MaxValue));

                ICommandsProvider prov = new OrderCommandsProvider();

                factory.AddCommandsProvider(Consts.CategoryObjectVersion, 1019, prov);
            }
        }

        /// <summary>
        /// Метод при выгрузке дополнения для работы с ведомостями кооперации
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Unload()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает имя модуля
        /// </summary>
        public string Name
        {
            get { return "Редактор заказов"; }

        }
    }
}
