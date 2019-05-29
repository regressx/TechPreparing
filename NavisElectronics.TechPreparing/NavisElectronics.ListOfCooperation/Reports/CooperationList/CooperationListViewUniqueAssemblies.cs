using System;
using System.Collections.Generic;
using System.Text;
using Aga.Controls.Tree;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.ViewModels;

namespace NavisElectronics.ListOfCooperation.Reports.CooperationList
{
    public class CooperationListViewUniqueAssemblies:IDocumentTypeFactory
    {
                /// <summary>
        /// id шаблона ведомости кооперации из IPS
        /// </summary>
        private const int TemplateId = 1528533;

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
        public void Create(Node mainElement, string name, Agent currentManufacturer)
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

            DocumentTreeNode firstPage = docTemplate.FindNode("DataPage");
            DocumentTreeNode pcbPage = docTemplate.FindNode("PcbPage");

            // строку получаю из шаблона документа
            TableData rowTemplate = docTemplate.FindNode("Row") as TableData;
            
            // строку получаю из шаблона документа
            TableData rowPcbTemplate = docTemplate.FindNode("RowPCB") as TableData;


            CooperationListFactoryUniqueAssemblies cooperationListFactory = new CooperationListFactoryUniqueAssemblies(myNode);
            CooperationListDocument cooperationListDocument = cooperationListFactory.Create();

            ICollection<ExtractedObject> commonObjects = cooperationListDocument.CommonObjects;

            if (commonObjects.Count > 0)
            {
                DocumentTreeNode commonPageInstance = firstPage.CloneFromTemplate();
                mainDocument.AddChildNode(commonPageInstance, false, false);
            }

            // получаю главную таблицу основных элементов напрямую из документа, предварительно добавив туда нужные страницы
            TableData mainNode = mainDocument.FindNode("WorkPlace") as TableData;

            int i = 1;
            foreach (ExtractedObject commonObject in commonObjects)
            {
                TableData rowInstanse = rowTemplate.CloneFromTemplate() as TableData;

                TextData numberCell = rowInstanse.FindNode("NumberInOrder") as TextData;
                numberCell.AssignText(i.ToString(), false, false, false);

                TextData nameDesCell = rowInstanse.FindNode("Name+Designation") as TextData;
                nameDesCell.AssignText(string.Format("{0} {1} {2:F0} шт.", commonObject.Designation, commonObject.Name,commonObject.AmountWithUse).Trim(), false, false, false);

                mainNode.AddChildNode(rowInstanse, false, false);


                IDictionary<long, ExtractedObject> elements = new Dictionary<long, ExtractedObject>();

                foreach (ExtractedObject parent in commonObject.Elements)
                {
                    ExtractedObject elementFromDictionary;
                    if (elements.ContainsKey(parent.Id))
                    {
                        elementFromDictionary = elements[parent.Id];
                        elementFromDictionary.AmountWithUse += parent.AmountWithUse;
                    }
                    else
                    {
                        elements.Add(parent.Id, parent);
                    }
                }


                foreach (ExtractedObject value in elements.Values)
                {

                    value.AmountWithUse = value.Amount * commonObject.AmountWithUse;

                    TableData parentRowInstance = rowTemplate.CloneFromTemplate() as TableData;
                    if (parentRowInstance != null)
                    {
                        TextData parentDataCell = parentRowInstance.FindNode("Parent") as TextData;

                        if (parentDataCell != null)
                        {
                            parentDataCell.AssignText(
                                string.Format("{0} {1}", value.Designation, value.Name).Trim(),
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
                            amountCell.AssignText(string.Format("{0:F3}", value.Amount).Trim(), false, false, false);
                        }

                    }

                    if (parentRowInstance != null)
                    {
                        TextData amountWithUseCell = parentRowInstance.FindNode("AmountInOrder") as TextData;
                        if (amountWithUseCell != null)
                        {
                            amountWithUseCell.AssignText(string.Format("{0:F3}", value.AmountWithUse).Trim(), false, false,
                                false);
                        }
                    }

                    TextData measureUnitsCell = parentRowInstance.FindNode("MeasureUnits") as TextData;
                    measureUnitsCell.AssignText(string.Format("{0}", value.MeasureUnits).Trim(), false, false, false);

                    TextData stockRateCell = parentRowInstance.FindNode("StockRate") as TextData;
                    stockRateCell.AssignText(string.Format("{0:F2}", value.StockRate).Trim(), false, false, false);

                    TextData totalCell = parentRowInstance.FindNode("Total") as TextData;
                    totalCell.AssignText(string.Format("{0:F2}", value.AmountWithUse * value.StockRate).Trim(), false, false, false);

                    TextData sampleSizeCell = parentRowInstance.FindNode("SampleSize") as TextData;
                    sampleSizeCell.AssignText(string.Format("{0}", value.SampleSize).Trim(), false, false, false);

                    TextData techProcessPCB = parentRowInstance.FindNode("TechProcess") as TextData;
                    techProcessPCB.AssignText(string.Format("{0}", value.TechProcessReference).Trim(), false, false, false);


                    mainNode.AddChildNode(parentRowInstance, false, false);
                }
                i++;
            }

            ICollection<ExtractedObject> pcbObjects = cooperationListDocument.PcbObjects;
            i = 1;

            if (pcbObjects.Count > 0)
            {
                DocumentTreeNode commonPageInstance = pcbPage.CloneFromTemplate();
                mainDocument.AddChildNode(commonPageInstance, false, false);
            }

            // получаю главную таблицу основных элементов напрямую из документа
            TableData mainNodePCb = mainDocument.FindNode("WorkPlacePCB") as TableData;


            foreach (ExtractedObject commonObject in pcbObjects)
            {
                TableData rowInstanse = rowPcbTemplate.CloneFromTemplate() as TableData;

                TextData numberCell = rowInstanse.FindNode("NumberInOrderPCB") as TextData;
                numberCell.AssignText(i.ToString(), false, false, false);

                TextData nameDesCell = rowInstanse.FindNode("Name + Designation PCB") as TextData;
                nameDesCell.AssignText(string.Format("{0} {1}", commonObject.Designation, commonObject.Name).Trim(), false, false, false);

                TextData pcbVersionCell = rowInstanse.FindNode("VersionPCB") as TextData;
                pcbVersionCell.AssignText(string.Format("{0}", commonObject.PcbVersion).Trim(), false, false, false);

                TextData measureUnitsCell = rowInstanse.FindNode("MeasureUnitsPCB") as TextData;
                measureUnitsCell.AssignText(string.Format("{0}", commonObject.MeasureUnits).Trim(), false, false, false);

                TextData stockRateCell = rowInstanse.FindNode("StockRatePCB") as TextData;
                stockRateCell.AssignText(string.Format("{0:F2}", commonObject.StockRate).Trim(), false, false, false);

                TextData totalCell = rowInstanse.FindNode("TotalPCB") as TextData;
                totalCell.AssignText(string.Format("{0:F2}", commonObject.Total).Trim(), false, false, false);

                TextData sampleSizeCell = rowInstanse.FindNode("SampleSizePCB") as TextData;
                sampleSizeCell.AssignText(string.Format("{0}", commonObject.SampleSize).Trim(), false, false, false);



                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(commonObject.TechProcessReference))
                {
                    sb.Append(commonObject.TechProcessReference);
                }

                if (!string.IsNullOrEmpty(commonObject.TechTask))
                {
                    sb.Append(", " + commonObject.TechTask);

                    TextData techTaskPcb = rowInstanse.FindNode("TechTask") as TextData;
                    techTaskPcb.AssignText("Есть", false, false, false);

                }


                TextData techProcessPCB = rowInstanse.FindNode("TechProcessPCB") as TextData;
                techProcessPCB.AssignText(sb.ToString(), false, false, false);


                mainNodePCb.AddChildNode(rowInstanse, false, false);

                foreach (ExtractedObject parent in commonObject.Elements)
                {
                    TableData parentRowInstance = rowPcbTemplate.CloneFromTemplate() as TableData;
                    TextData parentDataCell = parentRowInstance.FindNode("ParentPCB") as TextData;
                    parentDataCell.AssignText(string.Format("{0} {1}", parent.Designation, parent.Name).Trim(), false, false, false);

                    TextData amountCell = parentRowInstance.FindNode("AmountPCB") as TextData;
                    amountCell.AssignText(string.Format("{0:F2}", parent.Amount).Trim(), false, false, false);

                    TextData amountWithUseCell = parentRowInstance.FindNode("AmountInOrderPCB") as TextData;
                    amountWithUseCell.AssignText(string.Format("{0:F2}", parent.CountElementsAmountWithStockRate()).Trim(), false, false, false);

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