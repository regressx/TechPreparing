using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Reports.MaterialsList
{
    using Aga.Controls.Tree;
    using Intermech.Document.Client;
    using Intermech.Document.Model;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Document;
    using System.Collections.Generic;
    using System.Linq;

    public class MaterialsListDocumentView : IDocumentTypeFactory
    {
        private const long TemplateId = 1987754;

        private MaterialsListDocumentModel _model;

        public MaterialsListDocumentView(MaterialsListDocumentModel model)
        {
            _model = model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainElement"></param>
        /// <param name="name"></param>
        public void Create(Node mainElement, string name)
        {
            IntermechTreeElement element = (IntermechTreeElement)mainElement.Tag;

            // получить сгенерированный документ
            MaterialsListDocumentModel doc = _model.GenerateFrom(element);

            // далее отображаем его на бумагу

            long newObjectId = -1;
            IDBObject myTestDbObject;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObjectCollection collectionOfReports = keeper.Session.GetObjectCollection(1169);
                myTestDbObject = collectionOfReports.Create(1169);
                newObjectId = myTestDbObject.ObjectID;
                IDBAttribute nameAttribute = myTestDbObject.Attributes.FindByID(10);
                nameAttribute.Value = name + " " + "ВСН";

                // пока сделаем без связи

                //IDBRelationCollection relationCollection = keeper.Session.GetRelationCollection(1003); // по связи "Простая связь с сортировкой"
                //relationCollection.Create(techRoutesList.MainProductId, newObjectId);
            }

            // главный документ
            ImDocument mainDocument;

            // шаблон документа
            ImDocument docTemplate = null;

            // Загрузить шаблон документа из БД
            ImDocument template = DocumentEditorPlugin.LoadDocumentFromDBObject(TemplateId, 0);

            // Создать документ на основе шаблона
            mainDocument = new ImDocument(template, true, true);

            // Оригинальный шаблон из БД используется только для генерации документа. Созданный документ содержит копию шаблона с которым он взаимодействует.
            // Генерация внутренних элементов документа должна проходить на основе внутреннего шаблона документа
            docTemplate = mainDocument.DocumentTemplate as ImDocument;


            DocumentTreeNode mainMaterialsPage = docTemplate.FindNode("MainMaterialsPage");
            DocumentTreeNode techTaskMaterialsPage = docTemplate.FindNode("TechTaskMaterialsPage");
            DocumentTreeNode secondaryMaterials = docTemplate.FindNode("AccessoryMaterialsPage");

            if (doc.MainMaterials.Any())
            {
                DocumentTreeNode commonPageInstance = mainMaterialsPage.CloneFromTemplate();
                mainDocument.AddChildNode(commonPageInstance, false, false);

                // строку получаю из шаблона документа
                TableData rowTemplate = docTemplate.FindNode("Row") as TableData;

                // получаю главную таблицу основных элементов напрямую из документа, предварительно добавив туда нужные страницы
                TableData mainNode = mainDocument.FindNode("WorkPlace") as TableData;

                WriteToDoc(doc.MainMaterials, mainNode, rowTemplate, string.Empty);

            }

            if (doc.MaterialsFromTechTasks.Any())
            {
                DocumentTreeNode commonPageInstance = techTaskMaterialsPage.CloneFromTemplate();
                mainDocument.AddChildNode(commonPageInstance, false, false);

                TableData rowTemplate = docTemplate.FindNode("Row #2") as TableData;

                TableData mainNode = mainDocument.FindNode("WorkPlace #2") as TableData;

                WriteToDoc(doc.MaterialsFromTechTasks, mainNode, rowTemplate, " #2");

            }

            if (doc.SecondaryMaterials.Any())
            {
                DocumentTreeNode commonPageInstance = secondaryMaterials.CloneFromTemplate();
                mainDocument.AddChildNode(commonPageInstance, false, false);

                TableData rowTemplate = docTemplate.FindNode("Row #3") as TableData;

                TableData mainNode = mainDocument.FindNode("WorkPlace #3") as TableData;

                WriteToDoc(doc.SecondaryMaterials, mainNode, rowTemplate, " #3");
            }



            // строку получаю из шаблона документа
            //TableData rowTechTaskTemplate = docTemplate.FindNode("RowPCB") as TableData;

            //TableData rowSecondaryMaterials = docTemplate.FindNode("RowPCB") as TableData;


            mainDocument.UpdateLayout(true);


            // добавляем лист регистрации изменений
            DocumentTreeNode regListInstance = null;
            DocumentTreeNode regList = docTemplate.FindNode("LRI");
            regListInstance = regList.CloneFromTemplate();
            mainDocument.AddChildNode(regListInstance, false, false);


            DocumentEditorPlugin.SaveImDocumentObjectFile(newObjectId, mainDocument, name, 0, false);
            using (SessionKeeper keeper = new SessionKeeper())
            {
                myTestDbObject.CommitCreation(true, false);
            }


        }

        private void WriteToDoc(IEnumerable<IntermechTreeElement> elements, TableData table, TableData rowTemplate, string parameterToFind)
        {
            int i = 1;
            foreach (IntermechTreeElement commonObject in elements)
            {
                double total = 0;

                TableData rowInstanse = rowTemplate.CloneFromTemplate() as TableData;

                TextData numberCell = rowInstanse.FindNode("NumberInOrder" + parameterToFind) as TextData;
                numberCell.AssignText(i.ToString(), false, false, false);

                TextData nameDesCell = rowInstanse.FindNode("Name+Designation"+parameterToFind) as TextData;
                nameDesCell.AssignText(string.Format("{0} {1}", commonObject.Designation, commonObject.Name).Trim(), false, false, false);

                TextData measureUnitsCell = rowInstanse.FindNode("MeasureUnits"+parameterToFind) as TextData;
                measureUnitsCell.AssignText(string.Format("{0}", commonObject.MeasureUnits).Trim(), false, false, false);

                // пройтись по родителям и собрать total
                IDictionary<long, IntermechTreeElement> uniqueParents = new Dictionary<long, IntermechTreeElement>();
                foreach (IntermechTreeElement parent in commonObject.Children)
                {
                    if (uniqueParents.ContainsKey(parent.Id))
                    {
                        IntermechTreeElement parentFromDictionary = uniqueParents[parent.Id];
                        parentFromDictionary.AmountWithUse += parent.AmountWithUse;
                    }
                    else
                    {
                        uniqueParents.Add(parent.Id, parent);
                    }
                }

                foreach (IntermechTreeElement parent in uniqueParents.Values)
                {
                    total += parent.TotalAmount;
                }


                TextData totalCell = rowInstanse.FindNode("Total"+ parameterToFind) as TextData;
                totalCell.AssignText(string.Format("{0:F2}", total).Trim(), false, false, false);


                table.AddChildNode(rowInstanse, false, false);

                foreach (IntermechTreeElement parent in uniqueParents.Values)
                {
                    TableData parentRowInstance = rowTemplate.CloneFromTemplate() as TableData;
                    if (parentRowInstance != null)
                    {
                        TextData parentDataCell = parentRowInstance.FindNode("Parent"+ parameterToFind) as TextData;

                        if (parentDataCell != null)
                        {
                            parentDataCell.AssignText(
                                string.Format("{0} {1}", parent.Designation, parent.Name).Trim(),
                                false,
                                false,
                                false);
                        }
                    }

                    if (parentRowInstance != null)
                    {
                        TextData amountCell = parentRowInstance.FindNode("Amount"+ parameterToFind) as TextData;
                        if (amountCell != null)
                        {
                            amountCell.AssignText(string.Format("{0:F0}", parent.Amount).Trim(), false, false, false);
                        }

                    }

                    if (parentRowInstance != null)
                    {
                        TextData amountWithUseCell = parentRowInstance.FindNode("AmountInOrder"+ parameterToFind) as TextData;
                        if (amountWithUseCell != null)
                        {
                            amountWithUseCell.AssignText(string.Format("{0:F0}", parent.AmountWithUse).Trim(), false, false,
                                false);
                        }
                    }

                    if (parentRowInstance != null)
                    {
                        TextData stockRateCell = parentRowInstance.FindNode("StockRate"+ parameterToFind) as TextData;
                        if (stockRateCell != null)
                        {
                            stockRateCell.AssignText(string.Format("{0:F2}", parent.StockRate).Trim(), false, false,
                                false);
                        }
                    }

                    table.AddChildNode(parentRowInstance, false, false);
                }
                i++;
            }
        }


    }
}