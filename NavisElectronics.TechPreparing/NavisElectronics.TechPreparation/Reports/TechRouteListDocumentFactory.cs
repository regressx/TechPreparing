namespace NavisElectronics.TechPreparation.Reports
{
    using System.Collections.Generic;
    using Aga.Controls.Tree;
    using Interfaces.Entities;
    using Intermech.Document.Client;
    using Intermech.Document.Model;
    using Intermech.Interfaces;
    using Intermech.Interfaces.Document;
    using Services;
    using ViewModels.TreeNodes;

    /// <summary>
    /// Класс создает представление документа
    /// </summary>
    public class TechRouteListDocumentFactory : IDocumentTypeFactory
    {
        private MapTreeOnListService _mapTreeOnListService;

        public TechRouteListDocumentFactory()
        {
            _mapTreeOnListService = new MapTreeOnListService();
        }

        /// <summary>
        /// id шаблона
        /// </summary>
        private const int TemplateId = 1224285;

        public void Create(Node node, string name) 
        {
            MyNode myNode = node as MyNode;

            long newObjectId = -1;
            IDBObject myTestDbObject;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObjectCollection collectionOfReports = keeper.Session.GetObjectCollection(1169);
                myTestDbObject = collectionOfReports.Create(1169);
                newObjectId = myTestDbObject.ObjectID;
                IDBAttribute nameAttribute = myTestDbObject.Attributes.FindByID(10);
                nameAttribute.Value = name;

                // пока сделаем без связи


                // IDBRelationCollection relationCollection = keeper.Session.GetRelationCollection(1003); // по связи "Простая связь с сортировкой"
                // relationCollection.Create(techRoutesList.MainProductId, newObjectId);
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

            TableData orderNode = mainDocument.FindNode("OrderTable") as TableData;
            TextData orderText = orderNode.FindNode("Order") as TextData;

            IntermechTreeElement tagElement = node.Tag as IntermechTreeElement;
            IntermechTreeElement orderElement = GetOrderElement(tagElement);
            orderText.AssignText(orderElement.Name, false, false, false);


            TableData designationTable = mainDocument.FindNode("DesignationTable") as TableData;
            TextData deviceDesignation = designationTable.FindNode("DeviceDesignation") as TextData;
            deviceDesignation.AssignText(myNode.Designation, false, false, false);

            TableData nameTable = mainDocument.FindNode("NameTable") as TableData;
            TextData deviceName = nameTable.FindNode("DeviceName") as TextData;
            deviceName.AssignText(myNode.Name, false, false, false);


            // получаю главную таблицу напрямую из документа
            TableData mainNode = mainDocument.FindNode("WorkPlace") as TableData;

            // строку получаю из шаблона документа
            TableData rowTemplate = docTemplate.FindNode("Row") as TableData;

            IList<MyNode> products = _mapTreeOnListService.MapTreeOnList(myNode);

            foreach (MyNode product in products)
            {

                TableData rowInstanse = rowTemplate.CloneFromTemplate() as TableData;

                TextData numberCell = rowInstanse.Nodes[0] as TextData;
                numberCell.AssignText(product.NumberInOrder, false, false, false);

                TextData designationCell = rowInstanse.Nodes[1] as TextData;
                designationCell.AssignText(product.Designation, false, false, false);

                TextData nameCell = rowInstanse.Nodes[2] as TextData;
                nameCell.AssignText(product.Name, false, false, false);

                TextData amountCell = rowInstanse.Nodes[3] as TextData;
                amountCell.AssignText(product.Amount, false, false, false);

                TextData routeCell = rowInstanse.Nodes[4] as TextData;
                routeCell.AssignText(product.Route, false, false, false);

                TextData noteCell = rowInstanse.Nodes[5] as TextData;
                noteCell.AssignText(product.Note, false, false, false);


                // Красим сборки, комплекты, делаем жирными
                // if (product.Type == 1078 || product.Type == 1074 || product.Type == 1097)
                // {
                // foreach (var rowInstanseNode in rowInstanse.Nodes)
                // {
                // TextData cell = rowInstanseNode as TextData;
                // cell.CharFormat = new CharFormat(new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold));
                // cell.overrideFlags |= (OverrideFlags)0x4000; // щелкаем флагом, чтобы закрепить цвет и всё остальное
                // }
                // }
                mainNode.AddChildNode(rowInstanse, false, false);
            }

            mainDocument.UpdateLayout(true);

            // добавляем лист регистрации изменений
            DocumentTreeNode regListInstance = null;
            DocumentTreeNode regList = docTemplate.FindNode("LRI");
            regListInstance = regList.CloneFromTemplate();
            mainDocument.AddChildNode(regListInstance, false, false);

            DocumentEditorPlugin.SaveImDocumentObjectFile(newObjectId, mainDocument, name, 0, false);
            using (SessionKeeper keeper = new SessionKeeper())
            {
                myTestDbObject.CommitCreation(true, true);
            }
        }


        private IntermechTreeElement GetOrderElement(IntermechTreeElement element)
        {
            Stack<IntermechTreeElement> stack = new Stack<IntermechTreeElement>();
            IntermechTreeElement myElement = null;
            stack.Push(element);
            while (stack.Count > 0)
            {
                myElement = stack.Pop();
                if (myElement.Parent != null)
                {
                    stack.Push(myElement.Parent);
                }
            }

            return myElement;
        }
    }
}