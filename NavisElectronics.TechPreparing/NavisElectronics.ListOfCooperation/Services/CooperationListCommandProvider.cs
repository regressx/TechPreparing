// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationListCommandProvider.cs" company="">
//   
// </copyright>
// <summary>
//   Провайдер команд для пункта меню
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Services
{
    using System;

    using Intermech.Extensions;
    using Intermech.Interfaces;
    using Intermech.Navigator.ContextMenu;
    using Intermech.Navigator.Interfaces;

    using NavisElectronics.TechPreparation.Presenters;

    /// <summary>
    /// Провайдер команд для пункта меню
    /// </summary>
    public class CooperationListCommandProvider : ICommandsProvider
    {
        /// <summary>
        /// Фабрика представителей
        /// </summary>
        private readonly IPresentationFactory _presentationFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationListCommandProvider"/> class.
        /// </summary>
        /// <param name="presentationFactory">
        /// Фабрика представителей
        /// </param>
        public CooperationListCommandProvider(IPresentationFactory presentationFactory)
        {
            _presentationFactory = presentationFactory;
        }

        /// <summary>
        /// The get merged commands.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <param name="viewServices">
        /// The view services.
        /// </param>
        /// <returns>
        /// The <see cref="CommandsInfo"/>.
        /// </returns>
        public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            // ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе даных 
            // для того, чтобы проверить, можно ли отображать команду меню или нет!

            // Список добавленных или перекрытых команд контекстного меню
            CommandsInfo commandsInfo = new CommandsInfo();

            // Есть один выделенный элемент
            if (items != null && items.Count == 1)
            {
                commandsInfo.Add("EditTechLists",
                    new CommandInfo(TriggerPriority.ItemCategory, OnClickCreateCooperationList));
            }

            // Вернём список
            return commandsInfo;
        }

        /// <summary>
        /// Метод получает какие-то команды контекстного меню.
        /// </summary>
        /// <param name="items">Выбранные элементы</param>
        /// <param name="viewServices">Провайдер сервисов</param>
        /// <returns><see cref="CommandsInfo"/></returns>
        public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            // ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе даных 
            // для того, чтобы проверить, можно ли отображать команду меню или нет!

            // Список добавленных или перекрытых команд контекстного меню
            CommandsInfo commandsInfo = new CommandsInfo();

            // Вернём список
            return commandsInfo;
        }

        /// <summary>
        /// Обработчик события нажатия на редактирование технологических ведомостей
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <param name="viewservices">
        /// The viewservices.
        /// </param>
        /// <param name="additionalinfo">
        /// The additionalinfo.
        /// </param>
        private void OnClickCreateCooperationList(ISelectedItems items, IServiceProvider viewservices, object additionalinfo)
        {

            INodeID nodeId = items.GetItemID(0);
            long id = nodeId.GetObjVerID(); // определяем id
            //if (id > 0)
            //{
            //    throw new Exception("Прежде, чем что-то делать с технологическими ведомостями, возьмите заказ на редактирование с помощью контекстного меню или нажатием кнопки F9");
            //}

            IDBObject orderObject = null;
            string name = string.Empty;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                orderObject = keeper.Session.GetObject(id);
                name = orderObject.Caption;
            }

            IPresenter<long> mainPresenter = _presentationFactory.GetPresenter<MainPresenter, long>();

            mainPresenter.Run(id);

        }
    }
}