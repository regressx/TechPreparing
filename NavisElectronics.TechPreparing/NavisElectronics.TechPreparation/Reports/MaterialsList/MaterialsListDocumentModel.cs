using System.Collections.Generic;
using System.Linq;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Interfaces.Services;

namespace NavisElectronics.TechPreparation.Reports.MaterialsList
{
    public class MaterialsListDocumentModel
    {
        enum MaterialPlace
        {
            MainMaterial,
            MaterialFromTechTask,
            SecondaryMaterial
        }


        private RecountService _recountService;
        private IDictionary<long, IntermechTreeElement> _uniqueMainMaterials;
        private IDictionary<long, IntermechTreeElement> _uniqueMaterialsFromTechTask;
        private IDictionary<long, IntermechTreeElement> _uniqueSecondaryMaterials;

        public MaterialsListDocumentModel(RecountService recountService)
        {
            _recountService = recountService;
            _uniqueMainMaterials = new Dictionary<long, IntermechTreeElement>();
            _uniqueMaterialsFromTechTask = new Dictionary<long, IntermechTreeElement>();
            _uniqueSecondaryMaterials = new Dictionary<long, IntermechTreeElement>();
        }

        /// <summary>
        /// Материалы для отчета
        /// </summary>
        public IEnumerable<IntermechTreeElement> MainMaterials => _uniqueMainMaterials.Values.OrderBy(e => e.Name);

        public IEnumerable<IntermechTreeElement> MaterialsFromTechTasks => _uniqueMaterialsFromTechTask.Values.OrderBy(e => e.Name);

        public IEnumerable<IntermechTreeElement> SecondaryMaterials => _uniqueSecondaryMaterials.Values.OrderBy(e => e.Name);

        /// <summary>
        /// Заполняет из указанного узла дерева материалы
        /// </summary>
        /// <param name="element"></param>
        /// <returns>Заполненный данными документ</returns>
        public MaterialsListDocumentModel GenerateFrom(IntermechTreeElement element)
        {
            IntermechTreeElement root = (IntermechTreeElement)element.Clone();
            root.Amount = 1;
            _recountService.RecountAmount(root);

            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                // пропустить, если по кооперации
                if (elementFromQueue.CooperationFlag)
                {
                    continue;
                }

                // если это деталь, то надо забрать материал и добавить в основные материалы
                if (elementFromQueue.Type == 1052 || elementFromQueue.Type == 1159)
                {
                    IntermechTreeElement materialFromDetail = elementFromQueue.Children.First();
                    RegisterMaterial(materialFromDetail,MaterialPlace.MainMaterial);
                }

                if (elementFromQueue.RelationName == "Технологический состав")
                {
                    if (elementFromQueue.MaterialRegisteredIn == "Тех. треб")
                    {
                        RegisterMaterial(elementFromQueue, MaterialPlace.MaterialFromTechTask);
                    }
                    else
                    {
                        RegisterMaterial(elementFromQueue, MaterialPlace.SecondaryMaterial);
                    }
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }

            }

            return this;
        }


        private void RegisterMaterial(IntermechTreeElement material, MaterialPlace place)
        {

            // Выбрать словарь, куда писать
            IDictionary<long, IntermechTreeElement> currentDictionary = null;
            switch (place)
            {
                case MaterialPlace.MainMaterial:
                    currentDictionary = _uniqueMainMaterials;
                    break;

                case MaterialPlace.MaterialFromTechTask:
                    currentDictionary = _uniqueMaterialsFromTechTask;
                    break;

                case MaterialPlace.SecondaryMaterial:
                    currentDictionary = _uniqueSecondaryMaterials;
                    break;

            }

            // выбрать родителя
            IntermechTreeElement parent = new IntermechTreeElement();

            // здесь присваиваем идентификаторы
            parent.Id = material.Parent.Id;
            parent.ObjectId = material.Parent.ObjectId;
            parent.Type = material.Parent.Type;

            // а здесь надо перекинуть данные из материала
            parent.Amount = material.Amount;
            parent.AmountWithUse = material.AmountWithUse;
            parent.StockRate = material.StockRate;


            if (currentDictionary.ContainsKey(material.ObjectId))
            {
                IntermechTreeElement materialFromDictionary = currentDictionary[material.ObjectId];
                materialFromDictionary.Add(parent);
            }
            else
            {
                IntermechTreeElement copyElement = new IntermechTreeElement()
                {
                    Id = material.Id,
                    ObjectId = material.ObjectId,
                    Name = material.Name,
                    MeasureUnits = material.MeasureUnits,
                };
                copyElement.Add(parent);
                currentDictionary.Add(copyElement.ObjectId, copyElement);
            }
        }

        private void RegisterParent()
        {

        }
    }
}