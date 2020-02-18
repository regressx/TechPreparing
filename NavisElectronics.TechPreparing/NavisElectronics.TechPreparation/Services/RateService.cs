using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using NavisElectronics.TechPreparation.Calculations;
using NavisElectronics.TechPreparation.Enums;
using NavisElectronics.TechPreparation.Interfaces.Entities;
namespace NavisElectronics.TechPreparation.Services
{
    /// <summary>
    /// Сервис-фасад. Позволяет получить норму расхода материала на операцию
    /// </summary>
    internal class RateService
    {


        public class AsssembleUnit
        {
            internal long Id { get; set; }
            internal long RelationId { get; set; }
            internal double Amount { get; set; }
        }

        private readonly CalculationEngine _calculationEngine;

        public RateService(CalculationEngine calculationEngine)
        {
            _calculationEngine = calculationEngine;
        }

        public MeasuredValue Find(RateCatalog rateCatalog,
                                             string materialName,
                                             string operationName,
                                             string operationMode,
                                             long operationVersionId)
        {

            double result = 0d;
            // если всё правильно организовать, то поиск за log(n)
            MaterialCatalogNode material = (MaterialCatalogNode)rateCatalog.FindMaterial(materialName);
            OperationCatalogNode operationCatalog = material.FindOperation(operationName);
            ModeOperationCatalogNode operationModeCatalogNode = operationCatalog.FindOperationMode(operationMode);
            if (operationModeCatalogNode.ObjectTypeToCalculateAttribute == new Guid("cad00167-306c-11d8-b4e9-00304f19f545"))
            {
                operationModeCatalogNode.ActionType = ActionType.GetFromComposition;
            }
            else
            {
                operationModeCatalogNode.ActionType = ActionType.GetAttributeFromOneObject;
            }
            //здесь выбираем, откуда нужно забирать данные
            switch (operationModeCatalogNode.ActionType)
            {
                case ActionType.GetAttributeFromOneObject:

                    // получить тип, который надо искать. Это может быть или операция, или заготовка
                    //Guid type = operationModeCatalogNode.ObjectTypeToCalculateAttribute;

                    long versionId = FindBlank(operationVersionId);

                    result = GetRate(versionId, operationModeCatalogNode.FormulaText);

                    break;

                case ActionType.GetFromComposition:
                    IEnumerable<AsssembleUnit> elements = FindAssembleUnits(operationVersionId);

                    double currentAmount = 0;
                    foreach(AsssembleUnit unit in elements)
                    {
                        long referenceOnObjectVersionId = 0;
                        using (SessionKeeper keeper = new SessionKeeper())
                        {
                            IDBObject currentObject = keeper.Session.GetObject(unit.Id);
                            IDBAttribute referenceOnObjectAttribute = currentObject.GetAttributeByID(1217);

                            IDBRelation currentRelation = keeper.Session.GetRelation(unit.RelationId);
                            IDBAttribute amountAttribute = currentRelation.GetAttributeByID(1129);
                            currentAmount = ((MeasuredValue)amountAttribute.Value).Value;
                            referenceOnObjectVersionId = (long)referenceOnObjectAttribute.Value;
                        }
                        double currentRate = GetRate(referenceOnObjectVersionId, operationModeCatalogNode.FormulaText)*currentAmount;
                        result += currentRate;
                    }

                    break;

            }

            return new MeasuredValue(result, operationModeCatalogNode.MeasureId);
        }


        private double GetRate(long versionId, string formulaText)
        {
            // получить параметры из формулы
            IEnumerable<string> parametersToFind = ParametersParser.GetParameters(formulaText);

            char incrementChar = 'A';
            IList<FormulaVariable> formulaVariables = new List<FormulaVariable>();
            
            // найти у этого типа объекта атрибут и его значение. Гарантирую, что не будет более чем 26 параметров
            foreach (string parameter in parametersToFind)
            {
                int attributeId = MetaDataHelper.GetAttributeByTypeNameID(parameter);
                using (SessionKeeper keeper = new SessionKeeper())
                {
                    IDBObject currentObject = keeper.Session.GetObject(versionId);
                    IDBAttribute currentAttribute = currentObject.GetAttributeByID(attributeId);

                    FormulaVariable formulaVariable = new FormulaVariable();
                    formulaVariable.Name = parameter;
                    if (currentAttribute != null && currentAttribute.Value != DBNull.Value)
                    {
                        MeasuredValue value = null;
                        try
                        {
                            value = (MeasuredValue)currentAttribute.Value;
                        }
                        catch(InvalidCastException)
                        {
                            value = new MeasuredValue(Convert.ToDouble(currentAttribute.Value), 2804);
                        }

                        formulaVariable.Value = value.Value;
                        formulaVariable.PseudoName = incrementChar;
                        formulaVariables.Add(formulaVariable);
                    }
                }
                incrementChar++;
            }

            foreach (FormulaVariable variable in formulaVariables)
            {
                string newFormula = formulaText.Replace("[" + variable.Name + "]", variable.Value.ToString("F6",CultureInfo.InvariantCulture));
                formulaText = newFormula;
            }

            return Convert.ToDouble(_calculationEngine.Calculate(formulaText));

        }



        private IEnumerable<AsssembleUnit> FindAssembleUnits(long operationId)
        {
            ICollection<AsssembleUnit> collection = new List<AsssembleUnit>();

            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                ColumnDescriptor[] columnsForOperationComposition =
{
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor(-20, AttributeSourceTypes.Relation,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор отношения
                };

                DataTable assemblyUnitsTable = compositionService.LoadComposition(keeper.Session, operationId, 1002, new List<ColumnDescriptor>(columnsForOperationComposition), Intermech.SystemGUIDs.filtrationBaseVersions, 1201); // забрать собираемые единицы

                foreach (DataRow row in assemblyUnitsTable.Rows)
                {
                    AsssembleUnit asssembleUnit = new AsssembleUnit();
                    asssembleUnit.Id = (long)row[0];
                    asssembleUnit.RelationId = (long)row[1];
                    collection.Add(asssembleUnit);
                }
            }

            return collection;

        }


        private long FindBlank(long operationVersionId)
        {
            // подняться вверх до маршрута обработки
            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                int type = -1;
                long routeId = operationVersionId;


                ColumnDescriptor[] columnsForTechRoutes =
                {
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0) // тип объекта
                };


                // крутим, пока не найдем маршрут обработки
                while (type != 1037)
                {
                    // смотрим применяемость
                    DataTable table = compositionService.LoadCompositionApplicability(keeper.Session, routeId, 1002, columnsForTechRoutes,
                        Intermech.SystemGUIDs.filtrationBaseVersions, 1117, 1037, 1110);

                    foreach (DataRow row in table.Rows)
                    {
                        routeId = (long)row[0];
                        type = (int)row[1];
                    }

                    if (table.Rows.Count != 1)
                    {
                        break;
                    }
                }

                if (type != 1037)
                {
                    throw new System.Exception("У операции нет родителя в виде маршрута обработки!");
                }

                ColumnDescriptor[] columnsForBlank =
                {
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор версии объекта
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(1220, AttributeSourceTypes.Object,
                    ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0) // вид изделия
                };


                // Поиск состава
                DataTable blankTable = compositionService.LoadComposition(keeper.Session, routeId, 1002, new List<ColumnDescriptor>(columnsForBlank), Intermech.SystemGUIDs.filtrationBaseVersions, 1090); // забрать заготовку

                // проверим и оповестим пользователя, что отсутствует заготовка
                if (blankTable.Rows.Count == 0)
                {
                    throw new System.Exception("У Вас отсутствует объект типа Заготовка в составе маршрута обработки");
                }

                return (long)blankTable.Rows[0][0];
            }
        }
    }


}