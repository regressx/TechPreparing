using System;
using System.Collections.Generic;
using System.Data;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using NavisElectronics.TechPreparation.Enums;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Services;

namespace NavisElectronics.TechPreparation
{
    internal class OperationCalculationCommandProvider : ICommandsProvider
    {
        private readonly RateService _rateService;

        //private readonly CalculationEngine _engine;
        private RateCatalog _rateCatalog;

        public OperationCalculationCommandProvider(RateService rateService)
        {
            _rateService = rateService;
        }


        public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
        {
            // ВНИМАНИЕ! Основное требование к данному методу – нельзя выполнять обращения к базе даных 
            // для того, чтобы проверить, можно ли отображать команду меню или нет!

            // Список добавленных или перекрытых команд контекстного меню
            CommandsInfo commandsInfo = new CommandsInfo();

            // Вернём список
            return commandsInfo;
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
                commandsInfo.Add("CalculateOperation",
                    new CommandInfo(TriggerPriority.ItemCategory, OnClickCalculateOperation));
            }

            // Вернём список
            return commandsInfo;
        }

        private void GetRateCatalog()
        {
            _rateCatalog = new RateCatalog();
            long id = 1968475;

            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject myObject = keeper.Session.GetObject(id);

                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(
                        typeof(ICompositionLoadService));

                // Получим состав по связи Простая связь с сортировкой
                // Необходимые колонки
                ColumnDescriptor[] columns =
                {
                        new ColumnDescriptor(
                            (int)ObligatoryObjectAttributes.F_OBJECT_ID,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // идентификатор версии объекта
                        new ColumnDescriptor(
                            (int)ObligatoryObjectAttributes.F_OBJECT_TYPE,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // тип объекта
                        new ColumnDescriptor(10,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // наименование
                        new ColumnDescriptor(17862,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // текстовое описание
                        new ColumnDescriptor(1254,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // единица измерения
                        new ColumnDescriptor(12645,
                            AttributeSourceTypes.Object,
                            ColumnContents.Text,
                            ColumnNameMapping.Index,
                            SortOrders.NONE,
                            0), // типы объектов для поиска при вычислении значений атрибутов
                    };


                DataTable composition = compositionService.LoadComposition(keeper.Session.SessionGUID,
                                                                            myObject.ObjectID,
                                                                            1003,
                                                                            new List<ColumnDescriptor>(columns),
                                                                            string.Empty,
                                                                            1095);

                foreach (DataRow row in composition.Rows)
                {
                    MaterialCatalogNode materialCatalogNode = new MaterialCatalogNode();
                    materialCatalogNode.Id = (long)row[0];
                    materialCatalogNode.Name = row[2] == DBNull.Value ? string.Empty : (string)row[2];
                    _rateCatalog.AddMaterial(materialCatalogNode);
                }

                foreach (MaterialCatalogNode materialCatalogNode in _rateCatalog.Materials)
                {
                    composition = compositionService.LoadComposition(keeper.Session.SessionGUID,
                                                                        materialCatalogNode.Id,
                                                                        1003,
                                                                        new List<ColumnDescriptor>(columns),
                                                                        string.Empty,
                                                                        1095);

                    foreach (DataRow row in composition.Rows)
                    {
                        OperationCatalogNode operation = new OperationCatalogNode();
                        operation.Id = (long)row[0];
                        operation.Name = row[2] == DBNull.Value ? string.Empty : (string)row[2];

                        DataTable compositionOfOperation = compositionService.LoadComposition(keeper.Session.SessionGUID,
                                                    operation.Id,
                                                    1003,
                                                    new List<ColumnDescriptor>(columns),
                                                    string.Empty,
                                                    1095);
                        foreach (DataRow operationModeRow in compositionOfOperation.Rows)
                        {
                            ModeOperationCatalogNode operationMode = new ModeOperationCatalogNode();
                            operationMode.Id = (long)operationModeRow[0];
                            operationMode.Name = operationModeRow[2] == DBNull.Value ? string.Empty : (string)operationModeRow[2];
                            operationMode.FormulaText = operationModeRow[3] == DBNull.Value ? string.Empty : (string)operationModeRow[3];

                            IDBObject operationModeObject = keeper.Session.GetObject(operationMode.Id);
                            IDBAttribute measureAttribute = operationModeObject.GetAttributeByID(1254);
                            if (measureAttribute!=null)
                            {
                                operationMode.MeasureId = (long)measureAttribute.Value;
                            }

                            operationMode.ObjectTypeToCalculateAttribute = operationModeRow[5] == DBNull.Value ? Guid.Empty : new Guid((string)operationModeRow[5]);


                            operationMode.ActionType = ActionType.GetAttributeFromOneObject;

                            operation.Add(operationMode);
                        }
                        
                        materialCatalogNode.Add(operation);
                    }
                }


            }
        }


        private void OnClickCalculateOperation(ISelectedItems items, IServiceProvider viewservices, object additionalinfo)
        {
            GetRateCatalog();

            INodeID nodeId = items.GetItemID(0);

            // идентификатор версии операции
            long id = nodeId.GetObjVerID();

            // получить наименование операции
            string operationName = string.Empty;
            string operationMode = string.Empty;

            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject operationObject = keeper.Session.GetObject(id);
                operationName = (string)operationObject.GetAttributeByID(10).Value;

                IDBAttribute imbaseReferenceAttribute = operationObject.GetAttributeByID(1092);

                long objectImbaseReference = (long)imbaseReferenceAttribute.Value;

                // это папка из справочника, откуда была создана операция
                IDBObject objectFromImbase = keeper.Session.GetObject(objectImbaseReference);

                IDBAttribute modeOperationNameAttribute = objectFromImbase.GetAttributeByID(18101);
                if (modeOperationNameAttribute == null)
                {
                    throw new ArgumentNullException("modeOperationNameAttribute", "Задайте атрибут Наименование атрибута для поиска у операции в справочнике операций");
                }

                int modeOfOperation = MetaDataHelper.GetAttributeByTypeNameID((string)modeOperationNameAttribute.Value); 

                IDBAttribute modeOfOperationAttribute = operationObject.GetAttributeByID(modeOfOperation);

                IDBObject operationModeObject = keeper.Session.GetObject((long)modeOfOperationAttribute.Value);
                operationMode = (string)operationModeObject.GetAttributeByID(10).Value;
            }

            ColumnDescriptor[] mainColumns =
            {
                new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                    ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                    ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                new ColumnDescriptor(9, AttributeSourceTypes.Object,
                ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // обозначение
                new ColumnDescriptor(10, AttributeSourceTypes.Object,
                ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // наименование
                new ColumnDescriptor(-20, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0), // Id связи 
            };


            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                // забрать материалы и собираемые единицы
                DataTable table = compositionService.LoadComposition(keeper.Session, id, 1002, mainColumns,
                    Intermech.SystemGUIDs.filtrationBaseVersions, 1128, 1201);

                int type = 0;
                string name = string.Empty;

                // проходим по каждому материалу
                foreach (DataRow row in table.Rows)
                {
                    type = (int)row[1];
                    if (type == 1128)
                    {
                        name = row[3] == DBNull.Value ? string.Empty : (string)row[3];

                        IDBRelation relation = keeper.Session.GetRelation((long)row[4]);
                        MeasuredValue rate = _rateService.Find(_rateCatalog, name, operationName, operationMode, id);

                        IDBAttribute amountAtt = relation.GetAttributeByID(1129);
                        if (amountAtt == null)
                        {
                            relation.Attributes.AddAttribute(1129,false);
                        }

                        amountAtt = relation.GetAttributeByID(1129);
                        amountAtt.Value = rate;

                    }
                }
            }
        }
    }
}