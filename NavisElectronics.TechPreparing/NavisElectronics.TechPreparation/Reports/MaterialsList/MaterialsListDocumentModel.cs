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

        public IEnumerable<IntermechTreeElement> MaterialsFromTechTasks => _uniqueMainMaterials.Values.OrderBy(e => e.Name);

        public IEnumerable<IntermechTreeElement> SecondaryMaterials => _uniqueMainMaterials.Values.OrderBy(e => e.Name);

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

                // если это деталь, то надо забрать
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

            switch (place)
            {
                case MaterialPlace.MainMaterial:

                    if (_uniqueMainMaterials.ContainsKey(material.ObjectId))
                    {
                        IntermechTreeElement materialFromDictionary = _uniqueMainMaterials[material.ObjectId];

                    }
                    else
                    {
                        _uniqueMainMaterials.Add(material.ObjectId, material);
                    }
                    break;

                case MaterialPlace.MaterialFromTechTask:
                    break;

                case MaterialPlace.SecondaryMaterial:

                    break;

            }


        }

        private void RegisterParent()
        {

        }
    }
}