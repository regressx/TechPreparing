using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using NavisElectronics.TechPreparation.Presenters;
using Intermech.Expert.User;
using Intermech.Interfaces.Document;
using Intermech.Interfaces.Expert;
//using Jace;
//using Jace.Execution;
//using Jace.Operations;
//using Jace.Tokenizer;
using NavisElectronics.TechPreparation.Enums;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Services;

namespace NavisElectronics.TechPreparation
{
    public class OperationCalculationCommandProvider : ICommandsProvider
    {
        private readonly RateService _rateService;
        
        //private readonly CalculationEngine _engine;
        private RateCatalog _rateCatalog;

        public OperationCalculationCommandProvider(RateService rateService)
        {
            _rateService = rateService;
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
                //commandsInfo.Add("CalculateOperation",
                //    new CommandInfo(TriggerPriority.ItemCategory, OnClickCalculateOperation));
            }

            // Вернём список
            return commandsInfo;
        }

        //private void GetWorkshopInternal()
        //{
        //    _rateCatalog = new RateCatalog();

        //    TechRouteNode mainNode = new TechRouteNode();
        //    mainNode.Id = 1968475;
        //    mainNode.Name = "Цеха";
        //    Queue<TechRouteNode> queue = new Queue<TechRouteNode>();
        //    queue.Enqueue(mainNode);

        //    while (queue.Count > 0)
        //    {
        //        TechRouteNode nodeFromQueue = queue.Dequeue();

        //        using (SessionKeeper keeper = new SessionKeeper())
        //        {
        //            IDBObject myObject = keeper.Session.GetObject(nodeFromQueue.Id);

        //            // Сервис для получения составов
        //            ICompositionLoadService compositionService =
        //                (ICompositionLoadService)keeper.Session.GetCustomService(
        //                    typeof(ICompositionLoadService));

        //            // Получим состав по связи Простая связь с сортировкой
        //            // Необходимые колонки
        //            ColumnDescriptor[] columns =
        //            {
        //                new ColumnDescriptor(
        //                    (int)ObligatoryObjectAttributes.F_OBJECT_ID,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.Text,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // идентификатор версии объекта
        //                new ColumnDescriptor(
        //                    (int)ObligatoryObjectAttributes.F_OBJECT_TYPE,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.Text,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // тип объекта
        //                new ColumnDescriptor(9,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.Text,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // обозначение
        //                new ColumnDescriptor(10,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.Text,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // наименование
        //                new ColumnDescriptor(1190,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.Text,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // цех
        //                new ColumnDescriptor(1194,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.Text,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // участок

        //                new ColumnDescriptor(1190,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.ID,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // код цеха

        //                new ColumnDescriptor(1194,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.ID,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // код участка
        //                new ColumnDescriptor(11101,
        //                    AttributeSourceTypes.Object,
        //                    ColumnContents.Text,
        //                    ColumnNameMapping.Index,
        //                    SortOrders.NONE,
        //                    0), // производитель

        //            };


        //            DataTable articlesComposition = compositionService.LoadComposition(
        //                keeper.Session.SessionGUID,
        //                myObject.ObjectID,
        //                1003,
        //                new List<ColumnDescriptor>(columns),
        //                string.Empty,
        //                1095);

        //            foreach (DataRow row in articlesComposition.Rows)
        //            {
        //                TechRouteNode node = new TechRouteNode();
        //                node.Id = (long)row[0];
        //                node.Type = (int)row[1];
        //                node.Name = (string)row[3];
        //                if (row[4] == DBNull.Value)
        //                {
        //                    node.WorkshopName = string.Empty;
        //                }
        //                else
        //                {
        //                    node.WorkshopName = (string)row[4];
        //                }

        //                if (row[5] == DBNull.Value)
        //                {
        //                    node.PartitionName = string.Empty;
        //                }
        //                else
        //                {
        //                    node.PartitionName = (string)row[5];
        //                }

        //                node.WorkshopId = row[6] == DBNull.Value ? 0 : (long)row[6];
        //                node.PartitionId = row[7] == DBNull.Value ? 0 : (long)row[7];

        //                node.ManufacturerId = row[8] == DBNull.Value ? 0 : Convert.ToInt64(row[8]);

        //                nodeFromQueue.Add(node);
        //            }

        //            foreach (TechRouteNode child in nodeFromQueue.Children)
        //            {
        //                queue.Enqueue(child);
        //            }
        //        }
        //    }
        //    return mainNode;
        //}


        private void OnClickCalculateOperation(ISelectedItems items, IServiceProvider viewservices, object additionalinfo)
        {
            long typeOfOperation = 0;

            INodeID nodeId = items.GetItemID(0);

            // идентификатор версии операции
            long id = nodeId.GetObjVerID();


            // получить наименование операции
            // получить состав операции
            // по каждому наименованию материала осуществить поиск в справочнике норм расхода
            // определить по справочнику, где искать требуемые для расчета атрибуты и количества
            // получить формулу из справочника
            // подставить недостающие параметры в формулу
            // рассчитать формулу
            // рассчитать количество на основе статистических данных


            // получить наименование операции
            string operationName = string.Empty;
            string operationMode = string.Empty;

            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObject operationObject = keeper.Session.GetObject(id);
                operationName = (string)operationObject.GetAttributeByID(10).Value;
                IDBAttribute modeOfOperationAttribute = operationObject.GetAttributeByID(18098);
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

                        IDBRelation relationId = keeper.Session.GetRelation((long)row[4]);
                        double sum = 0;
                        
                        ModeOperationCatalogNode rate = _rateService.Find(_rateCatalog, name, operationName, operationMode);

                        switch (rate.ActionType)
                        {
                            case ActionType.GetAttributeFromOneObject:

                                break;
                            case ActionType.GetAttributeFromAbunchOfObjects:

                                break;
                        }

                        // Разобрать формулу по переменным при помощи Jace
                        string formula = rate.FormulaText;

                        MessageBox.Show(formula);
                        //TokenReader reader = new TokenReader(CultureInfo.InvariantCulture);
                        //List<Token> tokens = reader.Read(formula);

                        //IFunctionRegistry functionRegistry = new FunctionRegistry(false);

                        //AstBuilder astBuilder = new AstBuilder(functionRegistry, false);
                        //Operation operation = astBuilder.Build(tokens);

                        //Dictionary<string, double> variables = new Dictionary<string, double>();
                        //foreach (Variable variable in GetVariables(operation))
                        //{
                        //    double res = 0;
                        //    variables.Add(variable.Name, res);
                        //}

                        //IExecutor executor = new Interpreter();
                        //double result = executor.Execute(operation, null, null, variables);

                        // sum += ((MeasuredValue)value).Value * pair.Value.AmountOfContacts * pair.Value.Amount;

                        //IDBAttribute amountAttribute = relationId.GetAttributeByID(1129);
                        //if (amountAttribute == null)
                        //{
                        //    relationId.Attributes.AddAttribute(1129, false);
                        //}
                        //amountAttribute = relationId.GetAttributeByID(1129);
                        //amountAttribute.Value = new MeasuredValue(sum, rate.MeasureId);
                    }
                }

            }


            //using (SessionKeeper keeper = new SessionKeeper())
            //{
            //    // Сервис для получения составов
            //    ICompositionLoadService compositionService =
            //        (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

            //    int type = -1;
            //    long routeId = id;

            //    // крутим, пока не найдем маршрут обработки
            //    while (type != 1037)
            //    {
            //        DataTable table = compositionService.LoadCompositionApplicability(keeper.Session, routeId, 1002, columnsForTechRoutes,
            //            Intermech.SystemGUIDs.filtrationBaseVersions, 1117, 1037,1110);

            //        foreach (DataRow row in table.Rows)
            //        {
            //            routeId = (long) row[0];
            //            type = (int)row[1];
            //        }

            //        if (table.Rows.Count != 1)
            //        {
            //            break;
            //        }
            //    }

            //    if (type != 1037)
            //    {
            //        return;
            //    }

            //    ColumnDescriptor[] columnsForBlank =
            //    {
            //        new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
            //            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
            //        new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
            //            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
            //        new ColumnDescriptor(1220, AttributeSourceTypes.Object,
            //        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0) // вид изделия
            //    };


            //    // Поиск состава
            //    DataTable blankTable = compositionService.LoadComposition(keeper.Session, routeId, 1002, new List<ColumnDescriptor>(columnsForBlank), Intermech.SystemGUIDs.filtrationBaseVersions, 1090); // забрать заготовку

            //    // проверим и оповестим пользователя, что отсутствует заготовка
            //    if (blankTable.Rows.Count == 0)
            //    {
            //        MessageBox.Show("У Вас отсутствует объект типа Заготовка в составе маршрута обработки");
            //        return;
            //    }

            //    ColumnDescriptor[] columnsForAssemblies =
            //    {
            //        new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
            //            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
            //        new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
            //            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0) // тип объекта
            //    };

            //    DataTable assembliesTable = compositionService.LoadCompositionApplicability(keeper.Session, routeId, 1002, columnsForAssemblies,
            //        Intermech.SystemGUIDs.filtrationBaseVersions, 1074, 1078, 1097);

            //    if (assembliesTable.Rows.Count != 1)
            //    {
            //        MessageBox.Show("Маршрут обработки обязательно должен быть прикреплен к изделию, причем только к одному!");
            //        return;
            //    }

            //    long assemblyId = (long)assembliesTable.Rows[0][0];

            //    ColumnDescriptor[] columns =
            //    {
            //        new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
            //        new ColumnDescriptor((int)ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),  // тип объекта
            //        new ColumnDescriptor(17887, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип монтажа компонента
            //        new ColumnDescriptor(10176, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // диаметр отверстия
            //        new ColumnDescriptor(18080, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)  // количество контактов
            //    };

            //    DataTable composition = compositionService.LoadComposition(keeper.Session,assemblyId,1,columns,Intermech.SystemGUIDs.filtrationBaseVersions,1074,1138);

            //    // получить все элементы ручного монтажа
            //    IDictionary<string, Element> statisticDictionary = new Dictionary<string, Element>();

            //    foreach (DataRow row in composition.Rows)
            //    {
            //        string mountType = row[2] !=DBNull.Value ?  (string)row[2]: string.Empty;
            //        string key = row[3] !=DBNull.Value ?  (string)row[3]: string.Empty;
            //        int amountOfContacts = row[4] == DBNull.Value ? 0 : Convert.ToInt32(row[4]);
            //        if (mountType != string.Empty && mountType == "Ручной монтаж")
            //        {
            //            if (statisticDictionary.ContainsKey(key))
            //            {
            //                Element elementFromDictionary = statisticDictionary[key];
            //                elementFromDictionary.AmountOfContacts += amountOfContacts;
            //            }
            //            else
            //            {
            //                statisticDictionary.Add(key, new Element 
            //                {
            //                    Amount = 1,
            //                    AmountOfContacts = amountOfContacts,
            //                    Diameter = key
            //                });
            //            }
            //        }
            //    }

            //    ColumnDescriptor[] columnsForMaterialsInOperation =
            //    {
            //        new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
            //            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
            //        new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
            //            ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
            //        new ColumnDescriptor(-20, AttributeSourceTypes.Relation, ColumnContents.Value, ColumnNameMapping.Index, SortOrders.NONE, 0), // Id связи 
            //    };

            //    DataTable materialsInOperation = compositionService.LoadComposition(keeper.Session,id,1002,columnsForMaterialsInOperation,Intermech.SystemGUIDs.filtrationBaseVersions,1128);
                


            //    object value = null;

            //    //Dictionary<string, double> variables = new Dictionary<string, double>();
            //    //variables.Add("var1", 2.5);
            //    //variables.Add("var2", 3.4);

            //    //CalculationEngine engine = new CalculationEngine();
            //    //double res = engine.Calculate("var1*var2", variables);



            //    foreach (DataRow row in materialsInOperation.Rows)
            //    {
            //        long materialId = (long)row[0];
            //        double sum = 0;

            //        IDBObject materialObject = keeper.Session.GetObject(materialId);
            //        IDBRelation relationId = keeper.Session.GetRelation((long)row[2]);
            //        IDBAttribute holeDiameterAttribute = materialObject.GetAttributeByID(10176);
            //        IDBAttribute normsAtrribute = relationId.GetAttributeByID(1223);

            //        if (holeDiameterAttribute != null)
            //        {
            //            long measureId = 0;
            //            long holeMeasureId = 0;
            //            foreach (KeyValuePair<string, Element> pair in statisticDictionary)
            //            {
            //                MeasuredValue measureValue = MeasureHelper.ConvertToMeasuredValue(pair.Key);
            //                holeDiameterAttribute.Value = measureValue;
            //                holeMeasureId = measureValue.MeasureID;

            //                IExpertUser expertUser = new ExpertUser();

            //                IExpertTask expertTask = expertUser.GetExpertTask();

            //                ExpertResult result = expertTask.Calculate(1223, materialId, out value);
            //                if (result == ExpertResult.OK)
            //                {
            //                    normsAtrribute.Value = value;

            //                    measureId = ((MeasuredValue)value).MeasureID;
            //                    sum += ((MeasuredValue)value).Value * pair.Value.AmountOfContacts * pair.Value.Amount;
            //                }
            //            }

            //            IDBAttribute amountAttribute = relationId.GetAttributeByID(1129);
            //            if (amountAttribute == null)
            //            {
            //                relationId.Attributes.AddAttribute(1129, false);
            //            }
            //            amountAttribute = relationId.GetAttributeByID(1129);
            //            amountAttribute.Value = new MeasuredValue(sum, measureId);

            //            holeDiameterAttribute.Value = new MeasuredValue(0, holeMeasureId);
            //            if (normsAtrribute != null)
            //            {
            //                normsAtrribute.Value = new MeasuredValue(0, measureId);
            //            }

            //        }
            //    }

            //}

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

        //private IEnumerable<Variable> GetVariables(Operation operation)
        //{
        //    List<Variable> variables = new List<Variable>();
        //    GetVariables(operation, variables);
        //    return variables;
        //}

        //private void GetVariables(Operation operation, List<Variable> variables)
        //{
        //    if (operation.DependsOnVariables)
        //    {
        //        if (operation.GetType() == typeof(Variable))
        //        {
        //            variables.Add((Variable)operation);
        //        }
        //        else if (operation.GetType() == typeof(Addition))
        //        {
        //            Addition addition = (Addition)operation;
        //            GetVariables(addition.Argument1, variables);
        //            GetVariables(addition.Argument2, variables);
        //        }
        //        else if (operation.GetType() == typeof(Multiplication))
        //        {
        //            Multiplication multiplication = (Multiplication)operation;
        //            GetVariables(multiplication.Argument1, variables);
        //            GetVariables(multiplication.Argument2, variables);
        //        }
        //        else if (operation.GetType() == typeof(Subtraction))
        //        {
        //            Subtraction substraction = (Subtraction)operation;
        //            GetVariables(substraction.Argument1, variables);
        //            GetVariables(substraction.Argument2, variables);
        //        }
        //        else if (operation.GetType() == typeof(Division))
        //        {
        //            Division division = (Division)operation;
        //            GetVariables(division.Dividend, variables);
        //            GetVariables(division.Divisor, variables);
        //        }
        //        else if (operation.GetType() == typeof(Exponentiation))
        //        {
        //            Exponentiation exponentiation = (Exponentiation)operation;
        //            GetVariables(exponentiation.Base, variables);
        //            GetVariables(exponentiation.Exponent, variables);
        //        }
        //        else if (operation.GetType() == typeof(Function))
        //        {
        //            Function function = (Function)operation;
        //            foreach (Operation argument in function.Arguments)
        //            {
        //                GetVariables(argument, variables);
        //            }
        //        }
        //    }
        //}


    }
}