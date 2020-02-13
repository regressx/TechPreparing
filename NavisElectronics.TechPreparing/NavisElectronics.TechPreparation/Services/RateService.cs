using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Jace;
using NavisElectronics.TechPreparation.Calculations;
//using Jace;
//using Jace.Execution;
//using Jace.Operations;
//using Jace.Tokenizer;
using NavisElectronics.TechPreparation.Enums;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Services
{
    /// <summary>
    /// Сервис-фасад. Позволяет получить норму расхода материала на операцию
    /// </summary>
    public class RateService
    {
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


            // получить параметры из формулы
            IEnumerable<string> parametersToFind = ParametersParser.GetParameters(operationModeCatalogNode.FormulaText);

            //здесь выбираем, откуда нужно забирать данные
            switch (operationModeCatalogNode.ActionType)
            {
                case ActionType.GetAttributeFromOneObject:

                    // получить тип, который надо искать. Это может быть или операция, или заготовка
                    Guid type = operationModeCatalogNode.ObjectTypeToCalculateAttribute;

                    long objectTypeId = FindBlank(operationVersionId);

                    char incrementChar = 'A';
                    IList<FormulaVariable> formulaVariables = new List<FormulaVariable>();
                    // найти у этого типа объекта атрибут и его значение. Гарантирую, что не будет более чем 26 параметров
                    foreach (string parameter in parametersToFind)
                    {
                        int attributeId = MetaDataHelper.GetAttributeByTypeNameID(parameter);
                        using (SessionKeeper keeper = new SessionKeeper())
                        {
                            IDBObject blankObject = keeper.Session.GetObject(objectTypeId);
                            IDBAttribute currentAttribute = blankObject.GetAttributeByID(attributeId);

                            FormulaVariable formulaVariable = new FormulaVariable();
                            formulaVariable.Name = parameter;
                            if (currentAttribute!=null && currentAttribute.Value != DBNull.Value)
                            {
                                MeasuredValue value = (MeasuredValue)currentAttribute.Value;

                                formulaVariable.Value = value.Value;
                                formulaVariable.Units = value.MeasureID;
                                formulaVariable.PseudoName = incrementChar;
                                formulaVariables.Add(formulaVariable);
                            }
                        }
                        incrementChar++;
                    }

                    foreach(FormulaVariable variable in formulaVariables)
                    {
                        string newFormula = operationModeCatalogNode.FormulaText.Replace("[" + variable.Name + "]", variable.Value.ToString("F6"));
                        operationModeCatalogNode.FormulaText = newFormula;
                    }

                    result = _calculationEngine.Calculate(operationModeCatalogNode.FormulaText);

                    break;
            }

            return new MeasuredValue(result, operationModeCatalogNode.MeasureId);
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