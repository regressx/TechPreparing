using System.Collections.Generic;
using System.Globalization;
//using Jace;
//using Jace.Execution;
//using Jace.Operations;
//using Jace.Tokenizer;
using NavisElectronics.TechPreparation.Enums;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Services
{
    /// <summary>
    /// Сервис позволяет получить норму расхода материала на операцию
    /// </summary>
    public class RateService
    {
        //private readonly CalculationEngine _calculationEngine;

        public RateService()
        {
            //_calculationEngine = calculationEngine;
        }

        public OperationCatalogNode Find(RateCatalog rateCatalog, string materialName, string operationName)
        {
            MaterialCatalogNode material = (MaterialCatalogNode)rateCatalog.FindMaterial(materialName);
            OperationCatalogNode operationCatalog = (OperationCatalogNode)material.FindOperation(operationName);
            return operationCatalog;
        }

    }

    public class MaterialCatalogNode : RateCatalogNode
    {
        public RateCatalogNode FindOperation(string operationName)
        {
            if (Nodes.ContainsKey(operationName))
            {
                return Nodes[operationName];
            }

            throw new KeyNotFoundException("У материала нет норм на запрошенную операцию " + operationName);
        }
    }

    public class OperationCatalogNode : RateCatalogNode
    {
        public string FormulaText { get; set; }
        public ActionType ActionType { get; set; }
        public int ObjectTypeToCalculateAttribute{ get; set; }
        public long MeasureId { get; set; }
    }
}