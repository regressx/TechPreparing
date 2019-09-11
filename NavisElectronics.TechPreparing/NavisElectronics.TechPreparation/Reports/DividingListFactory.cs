using System;

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
    /// Фабрика для создания разделительной ведомости
    /// </summary>
    public class DividingListFactory : IDocumentTypeFactory
    {
        /// <summary>
        /// id шаблона разделительной ведомости из IPS
        /// </summary>
        private const int TemplateId = 1381532;

        /// <summary>
        /// Метод создания разделительной ведомости
        /// </summary>
        /// <param name="mainElement">
        /// Выбранный элемент, для которого разделительная ведомость формируется
        /// </param>
        /// <param name="name">
        /// Под каким именем будет сохранен документ
        /// </param>
        public void Create(Node mainElement, string name)
        {
            MyNode myNode = (MyNode)mainElement;
            IntermechTreeElement parentElement = ((IntermechTreeElement)myNode.Tag).Parent;

            string mainAgent = myNode.Agent;
            
            // подняться вверх до заказа и посмотреть, кто главный в заказе
            while (parentElement != null)
            {
                mainAgent = parentElement.Agent;
                parentElement = parentElement.Parent;
            }



            long newObjectId = -1;
            IDBObject myTestDbObject;
            using (SessionKeeper keeper = new SessionKeeper())
            {
                IDBObjectCollection collectionOfReports = keeper.Session.GetObjectCollection(1169);
                myTestDbObject = collectionOfReports.Create(1169);
                newObjectId = myTestDbObject.ObjectID;
                IDBAttribute nameAttribute = myTestDbObject.Attributes.FindByID(10);
                nameAttribute.Value = name;

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

            TextData deviceDesignationText = mainDocument.FindNode("DeviceDesignation") as TextData;
            deviceDesignationText.AssignText(myNode.Designation, false, false, false);

            TextData deviceName = mainDocument.FindNode("DeviceName") as TextData;
            deviceName.AssignText(myNode.Name, false, false, false);

            // хочу заполнить графу производителя в заголовке. Надо заметить, что следует сделать это в копии шаблона документа, а не просто в документе
            TableData tableHeader = docTemplate.FindNode("TableHeader") as TableData;
            TextData manufacturerNode = tableHeader.FindNode("Manufacturer") as TextData;
            manufacturerNode.AssignText(mainAgent, false, false, false);


            // получаю главную таблицу напрямую из документа
            TableData mainNode = mainDocument.FindNode("WorkPlace") as TableData;

            // строку получаю из шаблона документа
            TableData rowTemplate = docTemplate.FindNode("Row") as TableData;

            // делаем глубокую копию дерева, но не всего, а лишь тех узлов, которые подходят под условие
            MyNode deepCopyNode = GetNodeDeepCopy(myNode);

            // после всего этого отмапим его на список
            MapTreeOnListService treeOnListService = new MapTreeOnListService();
            IList<MyNode> products = treeOnListService.MapTreeOnList(deepCopyNode);


            foreach (MyNode product in products)
            {
                TableData rowInstanse = rowTemplate.CloneFromTemplate() as TableData;

                TextData numberCell = rowInstanse.Nodes[0] as TextData;
                numberCell.AssignText(product.NumberInOrder, false, false, false);

                TextData captionCell = rowInstanse.Nodes[1] as TextData;
                captionCell.AssignText(string.Format("{0} {1}", product.Name, product.Designation), false, false, false);

                TextData agentCell = rowInstanse.Nodes[3] as TextData;

                agentCell.AssignText(product.Agent, false, false, false);

                // сотрем, если совпадает с изготовителем заказа
                if (product.Agent == mainAgent)
                {
                    agentCell.AssignText(string.Empty, false, false, false);
                }

                // если изготовитель отсутствует или равен mainAgent, отмечает +
                if (product.Agent == null || product.Agent == mainAgent)
                {
                    TextData manufacturerCell = rowInstanse.Nodes[2] as TextData;
                    manufacturerCell.AssignText("+", false, false, false);
                }

                TextData noteCell = rowInstanse.Nodes[4] as TextData;
                if (product.InnerCooperation)
                {
                    noteCell.AssignText("ВПК", false, false, false);
                }
                else
                {
                    noteCell.AssignText(product.Note, false, false, false);
                }

                mainNode.AddChildNode(rowInstanse, false, false);
            }

            mainDocument.UpdateLayout(true);
            DocumentEditorPlugin.SaveImDocumentObjectFile(newObjectId, mainDocument, name, 0, false);
            using (SessionKeeper keeper = new SessionKeeper())
            {
                myTestDbObject.CommitCreation(true, false);
            }
        }

        private MyNode GetNodeDeepCopy(MyNode nodeToCopy)
        {
            MyNode mainNode = CreateNewNode(nodeToCopy);
            
            // надо раскрыть первый уровень обязательно
            foreach (MyNode childNode in nodeToCopy.Nodes)
            {
                MyNode childNodeCopy = CreateNewNode(childNode);
                mainNode.Nodes.Add(childNodeCopy);
                GetNodeDeepCopyRecursive(childNodeCopy, childNode);
            }

            return mainNode;
        }

        private MyNode CreateNewNode(MyNode template)
        {
            MyNode node = new MyNode();
            node.Agent = template.Agent;
            node.Note = template.Note;
            node.Designation = template.Designation;
            node.Name = template.Name;
            node.InnerCooperation = template.InnerCooperation;
            return node;
        }

        private void GetNodeDeepCopyRecursive(MyNode node, MyNode nodeToCopy)
        {
            if (nodeToCopy.ContainsInnerCooperation || nodeToCopy.IsMultipleAgents)
            {
                foreach (var node1 in nodeToCopy.Nodes)
                {
                    var childNode = (MyNode)node1;

                    MyNode newNode = null;
                    
                    // добавить кооперационный элемент
                    if (childNode.AnotherAgent || childNode.ContainsInnerCooperation)
                    {
                        newNode = CreateNewNode(childNode);
                        node.Nodes.Add(newNode);
                    }

                    if (newNode != null)
                    {
                        GetNodeDeepCopyRecursive(newNode, childNode);
                    }

                }
            }
        }


    }
}