// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationListPlugin.cs" company="">
//   
// </copyright>
// <summary>
//   Плагин для работы с ведомостью кооперации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Presenters;
using NavisElectronics.TechPreparation.TreeComparer;
using System;

namespace NavisElectronics.TechPreparation
{
    internal class OrdersComparerCommandProvider : ICommandsProvider
    {
        private readonly IPresentationFactory _presentationFactory;

        public OrdersComparerCommandProvider(IPresentationFactory presentationFactory)
        {
            _presentationFactory = presentationFactory;
        }

        public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            return new CommandsInfo();
        }

        public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            // ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе даных 
            // для того, чтобы проверить, можно ли отображать команду меню или нет!

            // Список добавленных или перекрытых команд контекстного меню
            CommandsInfo commandsInfo = new CommandsInfo();

            // Есть один выделенный элемент
            if (items != null && items.Count == 1)
            {
                commandsInfo.Add("OrderCompareTo",
                    new CommandInfo(TriggerPriority.ItemCategory, OnClickCompareOrders));
            }

            // Вернём список
            return commandsInfo;
        }

        private void OnClickCompareOrders(ISelectedItems items, IServiceProvider viewServices, object additionalInfo)
        {
            INodeID nodeId = items.GetItemID(0);
            long id = nodeId.GetObjVerID(); // определяем id


            DescriptorCollection descrs = new DescriptorCollection();
            descrs.Add(new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(Intermech.SystemGUIDs.objtypeOrders)));
            
            // загрузить список существующих заказов
            // Предлагаем пользователю выбрать какой-либо объект (только один)
            long[] selectedObjects = SelectionWindow.SelectObjects("Выберите объект заказа для сравнения",
                                                                    "Выберите объект заказа для сравнения",
                                                                    new Intermech.Navigator.CustomNode.Descriptor("Объекты с составами", descrs),
                                                                    SelectionOptions.SelectObjects | // Выбирать в окне можно узлы, содержащие объекты
                                                                    SelectionOptions.DisableSelectAbstractTypes |       // Запретить в окне выбирать абстрактные типы объектов
                                                                    SelectionOptions.DisableSelectFromTree            // Запретить выбирать в окне из дерева "Навигатора"
                                                                    );

            if (selectedObjects == null)
            {
                return;
            }
 
            IPresenter<IntermechTreeElement> treeComparerPresenter = _presentationFactory.GetPresenter<TreeComparerPresenter, IntermechTreeElement>();
            treeComparerPresenter.Run(new IntermechTreeElement());
        }
    }
}