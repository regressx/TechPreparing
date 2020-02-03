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

        public ModeOperationCatalogNode Find(RateCatalog rateCatalog,
                                             string materialName,
                                             string operationName,
                                             string operationMode)
        {
            MaterialCatalogNode material = (MaterialCatalogNode)rateCatalog.FindMaterial(materialName);
            OperationCatalogNode operationCatalog = material.FindOperation(operationName);
            ModeOperationCatalogNode operationModeCatalogNode = operationCatalog.FindOperationMode(operationMode);
            return operationModeCatalogNode;
        }

    }

    public class MaterialCatalogNode : RateCatalogNode
    {
        public OperationCatalogNode FindOperation(string operationName)
        {
            return (OperationCatalogNode)base.Find(operationName);
        }
    }

    public class OperationCatalogNode : RateCatalogNode
    {
        public string Name { get; set; }
        public ModeOperationCatalogNode FindOperationMode(string operationMode)
        {
            return (ModeOperationCatalogNode)base.Find(operationMode);
        }

    }

    public class ModeOperationCatalogNode : RateCatalogNode
    {
        public string FormulaText { get; set; }
        public ActionType ActionType { get; set; }
        public int ObjectTypeToCalculateAttribute { get; set; }
        public long MeasureId { get; set; }
    }
}