namespace NavisElectronics.Orders
{
    using System;
    using System.Threading;
    using Intermech.Extensions;
    using Intermech.Navigator.ContextMenu;
    using Intermech.Navigator.Interfaces;
    using Presenters;
    using TechPreparation.Data;
    using TechPreparation.Interfaces.Services;
    using ViewModels;

    public class OrderCommandsProvider : ICommandsProvider
    {
        public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            // ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе даных 
            // для того, чтобы проверить, можно ли отображать команду меню или нет!

            // Список добавленных или перекрытых команд контекстного меню
            CommandsInfo commandsInfo = new CommandsInfo();

            // Есть один выделенный элемент
            if (items != null && items.Count == 1)
            {
                commandsInfo.Add("EditOrder",
                    new CommandInfo(TriggerPriority.ItemCategory, OnClickEditOrder));
            }

            // Вернём список
            return commandsInfo;
        }



        public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            return new CommandsInfo();
        }


        private void OnClickEditOrder(ISelectedItems items, IServiceProvider viewservices, object additionalinfo)
        {
            INodeID nodeId = items.GetItemID(0);
            long id = nodeId.GetObjVerID(); // определяем id
            IPresenter<long, CancellationTokenSource> mainPresenter = new MainFormPresenter(new MainFormWithGrid(), new MainFormModel(new IntermechReader(), new SaveService(new IntermechWriter())));
            mainPresenter.Run(id, new CancellationTokenSource());
        }

    }
}