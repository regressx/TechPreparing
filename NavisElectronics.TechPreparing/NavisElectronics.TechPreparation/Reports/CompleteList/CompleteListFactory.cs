namespace NavisElectronics.TechPreparation.Reports.CompleteList
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Aga.Controls.Tree;
    using Enums;
    using Interfaces.Entities;
    using Intermech.Document.Client;
    using Intermech.Document.Model;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Compositions;
    using Intermech.Interfaces.Document;
    using Intermech.Kernel.Search;
    using ViewModels.TreeNodes;

    /// <summary>
    /// Класс для создания комплектовочной карты
    /// </summary>
    public class CompleteListViewFactory : IDocumentTypeFactory
    {
        /// <summary>
        /// id шаблона
        /// </summary>
        private const int TemplateId = 1224531;

        /// <summary>
        /// The _amount with use in root.
        /// </summary>
        private int _amountWithUseInRoot = 0;
        
        /// <summary>
        /// Метод создает отчет по документам сборки и всех ее входящих
        /// </summary>
        /// <param name="mainElement">
        /// Элемент, по которому строится комплектовочная карта
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="currentManufacturer">
        /// Производитель, от лица которого создается документ
        /// </param>
        public void Create(Node mainElement, string name)
        {
            MyNode nodeForReport = mainElement as MyNode;
            IntermechTreeElement root = nodeForReport.Tag as IntermechTreeElement;

            long newObjectId = -1;
            IDBObject myTestDbObject;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObjectCollection collectionOfReports = keeper.Session.GetObjectCollection(1050);
                myTestDbObject = collectionOfReports.Create(1050);
                newObjectId = myTestDbObject.ObjectID;
                IDBAttribute nameAttribute = myTestDbObject.Attributes.FindByID(10);
                nameAttribute.Value = string.Format("{0} {1} KK", nodeForReport.Designation, nodeForReport.Name);

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
            TextData assemblyTextData = template.FindNode("DesignationHeader") as TextData;
            TextData nameTextData = template.FindNode("NameHeader") as TextData;

            TextData assemblyTextData2 = template.FindNode("DesignationHeader #2") as TextData;
            TextData nameTextData2 = template.FindNode("NameHeader #2") as TextData;

            IntermechTreeElement mainParentBeforeOrder = null;


            mainParentBeforeOrder = root;

            string changeNumberSp = string.Empty;
            string changeNumberDrawing = string.Empty;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                // Сервис для получения составов
                ICompositionLoadService compositionService =
                    (ICompositionLoadService)keeper.Session.GetCustomService(typeof(ICompositionLoadService));

                // Получим конструкторский состав на сборку
                // Необходимые колонки
                ColumnDescriptor[] columns =
                {
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // идентификатор объекта
                    new ColumnDescriptor((int) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object,
                        ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0), // тип объекта
                    new ColumnDescriptor(1035, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index,
                        SortOrders.NONE, 0), //
                };

                // Поиск состава
                // сборки, комплекты, детали
                DataTable docs = compositionService.LoadComposition(keeper.Session.SessionGUID,
                    root.Id, 1004, new List<ColumnDescriptor>(columns), string.Empty, 1259, 1247);
                foreach (DataRow dataRow in docs.Rows)
                {
                    int type = (int)dataRow[1];
                    if (type == 1259)
                    {
                        if (dataRow[2] != DBNull.Value)
                        {
                            changeNumberSp = (string)dataRow[2];
                        }

                    }
                    else
                    {
                        if (dataRow[2] != DBNull.Value)
                        {
                            changeNumberDrawing = (string)dataRow[2];
                        }
                    }
                }
            }


            while (mainParentBeforeOrder.Parent.Type != 1019 && mainParentBeforeOrder.Parent.Type != 0)
            {
                mainParentBeforeOrder = mainParentBeforeOrder.Parent;
            }

            TextData changeNumberSpData = template.FindNode("ChangeNumberSP") as TextData;
            TextData changeNumberSpData2 = template.FindNode("ChangeNumberSP #2") as TextData;

            TextData changeNumberDrawingData = template.FindNode("ChangeNumberDrawing") as TextData;
            TextData changeNumberDrawingData2 = template.FindNode("ChangeNumberDrawing #2") as TextData;



            TextData amountData = template.FindNode("AmountInOrder") as TextData;
            TextData amountData2 = template.FindNode("AmountInOrder #2") as TextData;

            TextData amountWithUseData = template.FindNode("AmountWithUse") as TextData;
            TextData amountWithUseData2 = template.FindNode("AmountWithUse #2") as TextData;

            TextData rootNameData = template.FindNode("RootName") as TextData;
            TextData rootNameData2 = template.FindNode("RootName #2") as TextData;

            rootNameData.AssignText(mainParentBeforeOrder.Designation, false, false, false);
            rootNameData2.AssignText(mainParentBeforeOrder.Designation, false, false, false);


            IList<IntermechTreeElement> list = mainParentBeforeOrder.Find(root.ObjectId);

            foreach (IntermechTreeElement node in list)
            {
                _amountWithUseInRoot += (int)node.AmountWithUse;
            }


            amountWithUseData.AssignText(_amountWithUseInRoot.ToString(), false, false, false);
            amountWithUseData2.AssignText(_amountWithUseInRoot.ToString(), false, false, false);

            amountData.AssignText(mainParentBeforeOrder.Amount.ToString("F0"), false, false, false);
            amountData2.AssignText(mainParentBeforeOrder.Amount.ToString("F0"), false, false, false);

            assemblyTextData.AssignText(root.Designation, false, false, false);
            nameTextData.AssignText(root.Name, false, false, false);

            assemblyTextData2.AssignText(root.Designation, false, false, false);
            nameTextData2.AssignText(root.Name, false, false, false);

            changeNumberSpData.AssignText(changeNumberSp, false, false, false);
            changeNumberSpData2.AssignText(changeNumberSp, false, false, false);

            changeNumberDrawingData.AssignText(changeNumberDrawing, false, false, false);
            changeNumberDrawingData2.AssignText(changeNumberDrawing, false, false, false);


            // Создать документ на основе шаблона
            mainDocument = new ImDocument(template, true, true);
            
            // Оригинальный шаблон из БД используется только для генерации документа. Созданный документ содержит копию шаблона с которым он взаимодействует.
            // Генерация внутренних элементов документа должна проходить на основе внутреннего шаблона документа
            docTemplate = mainDocument.DocumentTemplate as ImDocument;

            // главный узел
            TableData mainNode = mainDocument.FindNode("MainTable") as TableData;
            
            FillTemplate(docTemplate, mainNode, root);

            mainDocument.UpdateLayout(true);

            DocumentEditorPlugin.SaveImDocumentObjectFile(newObjectId, mainDocument, name + "КК", 0, false);
            using (SessionKeeper keeper = new SessionKeeper())
            {
                myTestDbObject.CommitCreation(true, false);
            }
        }

        /// <summary>
        /// Заполнение шаблона комплектовочной карты
        /// </summary>
        /// <param name="documentTemplate">
        /// The document Template.
        /// </param>
        /// <param name="mainNode">
        /// The main node.
        /// </param>
        /// <param name="root">
        /// The root.
        /// </param>
        private void FillTemplate(ImDocument documentTemplate, TableData mainNode, IntermechTreeElement root)
        {
            foreach (IntermechTreeElement intermechProduct in root.Children)
            {
                switch ((IntermechObjectTypes)intermechProduct.Type)
                {
                    case IntermechObjectTypes.Detail:
                        TableData partNode = mainNode.FindFirstNodeByName("Детали") as TableData;
                        if (partNode == null)
                        {
                            // узел нового раздела спецификации
                            partNode = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Детали");
                        }

                        if (intermechProduct.Children.Count > 0)
                        {
                            foreach (IntermechTreeElement child in intermechProduct.Children)
                            {
                                // если не материал, значит это изделие-заготовка, и она тоже должна попасть в спецификацию/комплектовочную карту
                                if (child.Type != 1128 && child.Type != 1088 && child.Type != 1125 && child.Type != 1096)
                                {

                                    IntermechTreeElement cloneElement = (IntermechTreeElement)child.Clone();
                                    TableData tempOtherTable = null;

                                    switch (child.Type)
                                    {
                                        case 1074:
                                            tempOtherTable = mainNode.FindFirstNodeByName("Сборочные единицы") as TableData;
                                            break;
                                        case 1138:
                                            tempOtherTable = mainNode.FindFirstNodeByName("Прочие изделия") as TableData;
                                            break;
                                        case 1105:
                                            tempOtherTable = mainNode.FindFirstNodeByName("Стандартные изделия") as TableData;
                                            break;
                                    }

                                    if (tempOtherTable == null)
                                    {
                                        switch (child.Type)
                                        {
                                            case 1074:
                                                tempOtherTable = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Сборочные единицы");
                                                break;
                                            case 1138:
                                                tempOtherTable = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Прочие изделия");
                                                break;
                                            case 1105:
                                                tempOtherTable = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Стандартные изделия");
                                                break;
                                        }
                                    }

                                    cloneElement.Name = string.Format("{0} (изделие-заготовка для {1})", child.Name, intermechProduct.Designation);
                                    cloneElement.Amount = intermechProduct.Amount;

                                    AddNewRowIntoPart(tempOtherTable, cloneElement);
                                }
                            }
                        }

                        AddNewRowIntoPart(partNode, intermechProduct);
                        break;

                    case IntermechObjectTypes.Other:
                        TableData otherProductsNode = mainNode.FindFirstNodeByName("Прочие изделия") as TableData;
                        if (otherProductsNode == null)
                        {
                            // узел нового раздела спецификации
                            otherProductsNode = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Прочие изделия");
                        }
                        AddNewRowIntoPart(otherProductsNode, intermechProduct);
                        break;
                    case IntermechObjectTypes.Assembly:
                        TableData assembliesNode = mainNode.FindFirstNodeByName("Сборочные единицы") as TableData;
                        if (assembliesNode == null)
                        {
                            // узел нового раздела спецификации
                            assembliesNode = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Сборочные единицы");
                        }
                        AddNewRowIntoPart(assembliesNode, intermechProduct);
                        break;
                    case IntermechObjectTypes.Material:
                        TableData materialsNode = mainNode.FindFirstNodeByName("Материалы") as TableData;
                        if (materialsNode == null)
                        {
                            // узел нового раздела спецификации
                            materialsNode = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Материалы");
                        }
                        AddNewRowIntoPart(materialsNode, intermechProduct);
                        break;
                    case IntermechObjectTypes.StandartDetails:
                        TableData standartProductsNode = mainNode.FindFirstNodeByName("Стандартные изделия") as TableData;
                        if (standartProductsNode == null)
                        {
                            // узел нового раздела спецификации
                            standartProductsNode = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Стандартные изделия");
                        }
                        AddNewRowIntoPart(standartProductsNode, intermechProduct);
                        break;
                    case IntermechObjectTypes.Suite:
                        TableData suitesProductNode = mainNode.FindFirstNodeByName("Комплекты") as TableData;
                        if (suitesProductNode == null)
                        {
                            // узел нового раздела спецификации
                            suitesProductNode = AddNewPart(documentTemplate, mainNode, "PartOfSpecification", "Комплекты");
                        }
                        AddNewRowIntoPart(suitesProductNode, intermechProduct);
                        break;
                }
            }
        }

        /// <summary>
        /// Добавление нового раздела спецификации
        /// </summary>
        /// <param name="template">
        /// The template.
        /// </param>
        /// <param name="mainNode">
        /// The main node.
        /// </param>
        /// <param name="partId">
        /// The part id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="TableData"/>.
        /// </returns>
        private TableData AddNewPart(ImDocument template, TableData mainNode, string partId, string name)
        {
            // узел нового раздела спецификации
            TableData childNode = template.FindNode(partId) as TableData;

            // создаем этот узел из шаблона
            TableData myNode = childNode.CloneFromTemplate() as TableData;
            myNode.Name = name;
            
            // добавляем его в главный узел
            mainNode.AddChildNode(myNode, false, false);

            // строка заголовка
            TableData header = myNode.Nodes[0] as TableData;
            TextData headerCell = header.Nodes[2] as TextData;

            // меняем текст в одной из ячеек
            headerCell.AssignText(name, false, false, false);

            return myNode;
        }

        /// <summary>
        /// Добавление новой строки в нужный раздел
        /// </summary>
        /// <param name="partNode">
        /// The part node.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see cref="TableData"/>.
        /// </returns>
        private TableData AddNewRowIntoPart(TableData partNode, IntermechTreeElement product)
        {
            TableData headerRow = null;
            headerRow = partNode.Nodes[1].Clone() as TableData;
            headerRow.Name = string.Format("{0} {1}", product.Designation, product.Name);
            TextData numberCell = headerRow.Nodes[0] as TextData;
            numberCell.AssignText(product.Position, false, false, false);

            TextData desCell = headerRow.Nodes[1] as TextData;
            desCell.AssignText(product.Designation, false, false, false);

            TextData nameCell = headerRow.Nodes[2] as TextData;
            nameCell.AssignText(product.Name, false, false, false);

            TextData amountCell = headerRow.Nodes[5] as TextData;
            amountCell.AssignText(product.Amount.ToString(), false, false, false);

            TextData amountWithUseCell = headerRow.Nodes[6] as TextData;
            amountWithUseCell.AssignText((product.Amount * _amountWithUseInRoot).ToString("F3"), false, false, false);

            TextData substituteInfoCell = headerRow.Nodes[11] as TextData;
            substituteInfoCell.AssignText(product.SubstituteInfo, false, false, false);

            partNode.AddChildNode(headerRow, false, false);
            return headerRow;
        }
    }
}