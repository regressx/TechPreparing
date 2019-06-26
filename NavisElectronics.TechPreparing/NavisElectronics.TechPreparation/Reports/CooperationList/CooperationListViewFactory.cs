// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CooperationListViewFactory.cs" company="">
//   
// </copyright>
// <summary>
//   Класс для создания "бумажного" документа Ведомость кооперации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.Reports.CooperationList
{
    using System.Collections.Generic;

    using Aga.Controls.Tree;

    using Intermech.Document.Client;
    using Intermech.Document.Model;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Document;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Класс для создания "бумажного" документа Ведомость кооперации
    /// </summary>
    public class CooperationListViewPublisher : IDocumentTypeFactory
    {
        /// <summary>
        /// id шаблона ведомости кооперации из IPS
        /// </summary>
        private const int TemplateId = 1224283;

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
            CooperationNode myNode = mainElement as CooperationNode;

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


            foreach (MyPageDescription<ExtractedObject> page in cooperationListDocument.GetCommonPages())
            {
                DocumentTreeNode dataPageInstance = firstPage.CloneFromTemplate();
                TableData tableHeader = dataPageInstance.FindNode("Header") as TableData;
                byte k = 1; // индекс текущей ячейки в таблице шаблона
                IDictionary<long, byte> dictionaryAssemblies = new Dictionary<long, byte>();
                foreach (ExtractedObject intermechAssembly in page.Assemblies)
                {
                    TextData assemblyTextData = tableHeader.FindNode(string.Format("Assembly{0}", k)) as TextData;
                    assemblyTextData.AssignText(string.Format("{0} {1}",intermechAssembly.Designation, intermechAssembly.Name), false, false, false);
                    TextData useTextData = tableHeader.FindNode(string.Format("Use{0}", k)) as TextData;
                    useTextData.AssignText(intermechAssembly.AmountWithUse.ToString("F0"),false, false, false);
                    dictionaryAssemblies.Add(intermechAssembly.Id, k);
                    k++;
                }
                TableData workPlace = dataPageInstance.FindNode("WorkPlace") as TableData;

                foreach (ExtractedObject cooperationNode in page.Elements)
                {
                    TableData tableRow = rowTemplate.CloneFromTemplate() as TableData;
                    TableData emptyRow = rowTemplate.CloneFromTemplate() as TableData;
                    TextData desRow = tableRow.Nodes[1] as TextData;
                    TextData nameRow = tableRow.Nodes[2] as TextData;
                    TextData unitsRow = tableRow.Nodes[3] as TextData;

                    foreach (ExtractedObject element in cooperationNode.Elements)
                    {
                        if (dictionaryAssemblies.ContainsKey(element.Id))
                        {
                            TextData valueCell = tableRow.Nodes[4+dictionaryAssemblies[element.Id]] as TextData;
                            TextData valueEmptyCell = emptyRow.Nodes[4+dictionaryAssemblies[element.Id]] as TextData;
                            valueCell.AssignText(element.Amount.ToString("F0"), false, false, false);
                            valueEmptyCell.AssignText("X", false, false, false);
                        }
                    }

                    desRow.AssignText(cooperationNode.Designation, false, false, false);
                    nameRow.AssignText(cooperationNode.Name, false, false, false);
                    unitsRow.AssignText(cooperationNode.MeasureUnits, false, false, false);

                    workPlace.AddChildNode(tableRow, false, false);
                    workPlace.AddChildNode(emptyRow, false, false);
                }
                mainDocument.AddChildNode(dataPageInstance, false, false);
            }

            foreach (MyPageDescription<ExtractedObject> page in cooperationListDocument.GetPcbPages())
            {
                DocumentTreeNode dataPageInstance = pcbPage.CloneFromTemplate();
                TableData tableHeader = dataPageInstance.FindNode("HeaderPCB") as TableData;
                int k = 1; // индекс текущей ячейки в таблице шаблона
                foreach (ExtractedObject intermechAssembly in page.Assemblies)
                {
                    TextData assemblyTextData = tableHeader.FindNode(string.Format("AssemblyPcb{0}", k)) as TextData;
                    assemblyTextData.AssignText(string.Format("{0} {1}",intermechAssembly.Designation,intermechAssembly.Name), false, false, false);
                    k++;
                }
                TableData workPlace = dataPageInstance.FindNode("WorkPlacePCB") as TableData;

                foreach (ExtractedObject cooperationNode in page.Elements)
                {
                    TableData tableRow = rowPcbTemplate.CloneFromTemplate() as TableData;
                    TableData emptyRow = rowPcbTemplate.CloneFromTemplate() as TableData;
                    TextData desRow = tableRow.Nodes[1] as TextData;
                    TextData nameRow = tableRow.Nodes[2] as TextData;
                    desRow.AssignText(cooperationNode.Designation, false, false, false);
                    nameRow.AssignText(cooperationNode.Name, false, false, false);
                    workPlace.AddChildNode(tableRow, false, false);
                    workPlace.AddChildNode(emptyRow, false, false);
                }

                mainDocument.AddChildNode(dataPageInstance, false, false);
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