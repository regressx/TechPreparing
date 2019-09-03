using System.ComponentModel;

namespace NavisElectronics.TechPreparation.Reports.CooperationList
{
    using System.Collections.Generic;
    using Aga.Controls.Tree;
    using Entities;
    using Interfaces.Entities;
    using Intermech.Document.Client;
    using Intermech.Document.Model;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Document;
    using ViewModels.TreeNodes;

    /// <summary>
    /// Класс для создания "бумажного" документа Ведомость кооперации
    /// </summary>
    public class CooperationListView : IDocumentTypeFactory
    {
        /// <summary>
        /// id шаблона ведомости кооперации из IPS
        /// </summary>
        private const int TemplateId = 1485992;

        /// <summary>
        /// Метод создания документа "Ведомость кооперации"
        /// </summary>
        /// <param name="mainElement">
        /// Главный элемент, для которого делается кооперация
        /// </param>
        /// <param name="name">
        /// Задайте наименование файла
        /// </param>
        /// <param name="currentManufacturer">
        /// Текущий производитель. Для ведомости кооперации можно задать пустым
        /// </param>
        public void Create(Node mainElement, string name)
        {
            MyNode myNode = mainElement as MyNode;

            long newObjectId = -1;
            IDBObject myTestDbObject;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObjectCollection collectionOfReports = keeper.Session.GetObjectCollection(1169);
                myTestDbObject = collectionOfReports.Create(1169);
                newObjectId = myTestDbObject.ObjectID;
                IDBAttribute nameAttribute = myTestDbObject.Attributes.FindByID(10);
                nameAttribute.Value = name + " " + "ВК";

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

            //// получаю главную таблицу напрямую из документа
            //TableData mainNode = mainDocument.FindNode("WorkPlace") as TableData;

            //// строку получаю из шаблона документа
            //TableData rowTemplate = docTemplate.FindNode("Row") as TableData;

            //DocumentTreeNode titleList = docTemplate.FindNode("Title");
            //DocumentTreeNode titleListInstance = titleList.CloneFromTemplate();

            DocumentTreeNode firstPage = docTemplate.FindNode("DataPage");
            DocumentTreeNode pcbPage = docTemplate.FindNode("PcbPage");
            //mainDocument.AddChildNode(titleListInstance, false, false);


            // строку получаю из шаблона документа
            TableData rowTemplate = docTemplate.FindNode("Row") as TableData;
            
            // строку получаю из шаблона документа
            TableData rowPcbTemplate = docTemplate.FindNode("RowPCB") as TableData;


            CooperationListFactory cooperationListFactory = new CooperationListFactory(myNode);
            CooperationListDocument cooperationListDocument = cooperationListFactory.Create();

            ICollection<IntermechTreeElement> commonObjects = cooperationListDocument.CommonObjects;

            if (commonObjects.Count > 0)
            {
                DocumentTreeNode commonPageInstance = firstPage.CloneFromTemplate();
                mainDocument.AddChildNode(commonPageInstance, false, false);
            }

            // получаю главную таблицу основных элементов напрямую из документа, предварительно добавив туда нужные страницы
            TableData mainNode = mainDocument.FindNode("WorkPlace") as TableData;

            int i = 1;
            foreach (IntermechTreeElement commonObject in commonObjects)
            {
                double total = 0;

                TableData rowInstanse = rowTemplate.CloneFromTemplate() as TableData;

                TextData numberCell = rowInstanse.FindNode("NumberInOrder") as TextData;
                numberCell.AssignText(i.ToString(), false, false, false);

                TextData nameDesCell = rowInstanse.FindNode("Name+Designation") as TextData;
                nameDesCell.AssignText(string.Format("{0} {1}", commonObject.Designation, commonObject.Name).Trim(), false, false, false);

                TextData measureUnitsCell = rowInstanse.FindNode("MeasureUnits") as TextData;
                measureUnitsCell.AssignText(string.Format("{0}", commonObject.MeasureUnits).Trim(), false, false, false);

                TextData stockRateCell = rowInstanse.FindNode("StockRate") as TextData;
                stockRateCell.AssignText(string.Format("{0:F3}", commonObject.StockRate).Trim(), false, false, false);


                TextData totalCell = rowInstanse.FindNode("Total") as TextData;
                totalCell.AssignText(string.Format("{0:F3}", commonObject.TotalAmount).Trim(), false, false, false);

                TextData sampleSizeCell = rowInstanse.FindNode("SampleSize") as TextData;
                sampleSizeCell.AssignText(string.Format("{0}", commonObject.SampleSize).Trim(), false, false, false);

                TextData techProcess = rowInstanse.FindNode("TechProcess") as TextData;
                techProcess.AssignText(string.Format("{0}", commonObject.TechProcessReference).Trim(), false, false, false);

                mainNode.AddChildNode(rowInstanse, false, false);

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

                    TableData parentRowInstance = rowTemplate.CloneFromTemplate() as TableData;
                    if (parentRowInstance != null)
                    {
                        TextData parentDataCell = parentRowInstance.FindNode("Parent") as TextData;

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
                        TextData amountCell = parentRowInstance.FindNode("Amount") as TextData;
                        if (amountCell != null)
                        {
                            amountCell.AssignText(string.Format("{0:F3}", parent.Amount).Trim(), false, false, false);
                        }

                    }

                    if (parentRowInstance != null)
                    {
                        TextData amountWithUseCell = parentRowInstance.FindNode("AmountInOrder") as TextData;
                        if (amountWithUseCell != null)
                        {
                            amountWithUseCell.AssignText(string.Format("{0:F3}", parent.AmountWithUse).Trim(), false, false,
                                false);
                        }

                    }

                    total += parent.AmountWithUse;
                    mainNode.AddChildNode(parentRowInstance, false, false);
                }
                i++;
            }

            ICollection<IntermechTreeElement> pcbObjects = cooperationListDocument.PcbObjects;
            i = 1;

            if (pcbObjects.Count > 0)
            {
                DocumentTreeNode commonPageInstance = pcbPage.CloneFromTemplate();
                mainDocument.AddChildNode(commonPageInstance, false, false);
            }

            // получаю главную таблицу основных элементов напрямую из документа
            TableData mainNodePCb = mainDocument.FindNode("WorkPlacePCB") as TableData;


            foreach (IntermechTreeElement pcbObject in pcbObjects)
            {
                TableData rowInstanse = rowPcbTemplate.CloneFromTemplate() as TableData;

                TextData numberCell = rowInstanse.FindNode("NumberInOrderPCB") as TextData;
                numberCell.AssignText(i.ToString(), false, false, false);

                TextData nameDesCell = rowInstanse.FindNode("Name + Designation PCB") as TextData;
                nameDesCell.AssignText(string.Format("{0} {1}", pcbObject.Designation, pcbObject.Name).Trim(), false, false, false);

                TextData pcbVersionCell = rowInstanse.FindNode("VersionPCB") as TextData;
                pcbVersionCell.AssignText(string.Format("{0}", pcbObject.PcbVersion).Trim(), false, false, false);

                TextData measureUnitsCell = rowInstanse.FindNode("MeasureUnitsPCB") as TextData;
                measureUnitsCell.AssignText(string.Format("{0}", pcbObject.MeasureUnits).Trim(), false, false, false);

                TextData stockRateCell = rowInstanse.FindNode("StockRatePCB") as TextData;
                stockRateCell.AssignText(string.Format("{0:F3}", pcbObject.StockRate).Trim(), false, false, false);

                TextData totalCell = rowInstanse.FindNode("TotalPCB") as TextData;
                totalCell.AssignText(string.Format("{0:F3}", pcbObject.TotalAmount).Trim(), false, false, false);

                TextData sampleSizeCell = rowInstanse.FindNode("SampleSizePCB") as TextData;
                sampleSizeCell.AssignText(string.Format("{0}", pcbObject.SampleSize).Trim(), false, false, false);

                TextData techProcessPCB = rowInstanse.FindNode("TechProcessPCB") as TextData;
                techProcessPCB.AssignText(string.Format("{0}", pcbObject.TechProcessReference).Trim(), false, false, false);

                mainNodePCb.AddChildNode(rowInstanse, false, false);

                foreach (IntermechTreeElement parent in pcbObject.Children)
                {
                    TableData parentRowInstance = rowPcbTemplate.CloneFromTemplate() as TableData;
                    TextData parentDataCell = parentRowInstance.FindNode("ParentPCB") as TextData;
                    parentDataCell.AssignText(string.Format("{0} {1}", parent.Designation, parent.Name).Trim(), false, false, false);

                    TextData amountCell = parentRowInstance.FindNode("AmountPCB") as TextData;
                    amountCell.AssignText(string.Format("{0:F3}", parent.Amount).Trim(), false, false, false);

                    TextData amountWithUseCell = parentRowInstance.FindNode("AmountInOrderPCB") as TextData;
                    amountWithUseCell.AssignText(string.Format("{0:F3}", parent.AmountWithUse).Trim(), false, false, false);

                    mainNodePCb.AddChildNode(parentRowInstance, false, false);
                }

                i++;
            }




            mainDocument.UpdateLayout(true);

            DocumentEditorPlugin.SaveImDocumentObjectFile(newObjectId, mainDocument, name, 0, false);
            using (SessionKeeper keeper = new SessionKeeper())
            {
                myTestDbObject.CommitCreation(true, false);
            }
        }

    }
}