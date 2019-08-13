// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationListPlugin.cs" company="">
//   
// </copyright>
// <summary>
//   Плагин для работы с ведомостью кооперации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation
{
    using System;

    using Intermech;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Plugins;
    using Intermech.Navigator.ContextMenu;
    using Intermech.Navigator.Interfaces;
    using Presenters;
    using Services;

    using Ninject;

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

                menu.Nodes.Add(new MenuTemplateNode("EditTechLists", "Редактировать технологические ведомости", -1, 2, int.MaxValue));

                CommonModule module = new CommonModule();
                IKernel kernel = new StandardKernel();
                kernel.Load(module);

                kernel.Bind<IPresentationFactory>().To<PresentationFactory>().WithConstructorArgument("container", kernel);

                PresentationFactory presentationFactory = kernel.Get<PresentationFactory>();
                ICommandsProvider prov = new CooperationListCommandProvider(presentationFactory);

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
            get { return "Электронный редактор технологических ведомостей"; }

        }
    }
}
