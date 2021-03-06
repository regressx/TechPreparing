﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechRoutesMapModel.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the TechRoutesMapModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Aga.Controls.Tree;
    using Interfaces;
    using Interfaces.Entities;
    using Services;
    using TreeNodes;

    /// <summary>
    /// The tech routes map model.
    /// </summary>
    internal class TechRoutesMapModel
    {
        private readonly OpenFolderService _openFolderService;
        private readonly IDataRepository _repository;
        private readonly TechRouteSetService _techRouteSetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRoutesMapModel"/> class.
        /// </summary>
        /// <param name="openFolderService">
        /// The open Folder Service.
        /// </param>
        /// <param name="showFileManager">
        /// просмоторщик
        /// </param>
        /// <param name="repository"></param>
        /// <param name="techRouteSetService"></param>
        public TechRoutesMapModel(OpenFolderService openFolderService,IDataRepository repository, TechRouteSetService techRouteSetService)
        {
            _openFolderService = openFolderService;
            _repository = repository;
            _techRouteSetService = techRouteSetService;
        }

        /// <summary>
        /// Метод получает модель дерева для отображения
        /// </summary>
        /// <param name="root">
        /// Корень
        /// </param>
        /// <param name="whoIsMainInOrder">
        /// Кто главный в заказе
        /// </param>
        /// <param name="techRouteNode">
        /// Структура предприятия, по которому тех. процесс был разработан
        /// </param>
        /// <param name="agents">
        /// Набор организаций
        /// </param>
        /// <returns>
        /// The <see cref="TreeModel"/>.
        /// Возвращает модель дерева
        /// </returns>
        public TreeModel GetTreeModel(IntermechTreeElement root, string whoIsMainInOrder, TechRouteNode techRouteNode, IDictionary<long, Agent> agents)
        {
            TreeModel model = new TreeModel();
            MyNode mainNode = CreateNode(root);
            mainNode.Agent = agents[long.Parse(mainNode.Agent)].Name;
            BuildNodeRecursive(mainNode, root, root.Agent, techRouteNode, agents);
            model.Nodes.Add(mainNode);
            return model;
        }

        /// <summary>
        /// Просто маппинг элемента на его представление
        /// </summary>
        /// <param name="myElement">
        /// Элемент для маппинга
        /// </param>
        /// <returns>
        /// The <see cref="MyNode"/>.
        /// </returns>
        private MyNode CreateNode(IntermechTreeElement myElement)
        {
            MyNode myNode = new MyNode();
            myNode.Id = myElement.Id;
            myNode.ObjectId = myElement.ObjectId;
            myNode.Type = myElement.Type;
            myNode.PcbVersion = myElement.PcbVersion;
            myNode.IsPcb = myElement.IsPcb;
            myNode.Designation = myElement.Designation;
            myNode.Name = myElement.Name;
            myNode.Amount = myElement.Amount.ToString("F3");
            myNode.AmountWithUse = myElement.AmountWithUse;
            myNode.Route = myElement.TechRoute;
            myNode.Note = myElement.RouteNote;
            myNode.CooperationFlag = myElement.CooperationFlag;
            myNode.InnerCooperation = myElement.InnerCooperation;
            myNode.ContainsInnerCooperation = myElement.ContainsInnerCooperation;
            myNode.Agent = myElement.Agent == null ? string.Empty : myElement.Agent;
            myNode.Tag = myElement;
            myNode.DoNotProduce = myElement.ProduseSign;
            myNode.TechTask = myElement.TechTask;

            if (myElement.TechProcessReference != null)
            {
                myNode.TechProcessReference = myElement.TechProcessReference.Name;
            }

            return myNode;
        }


        /// <summary>
        /// The build node recursive.
        /// </summary>
        /// <param name="mainNode">
        /// Представление узла
        /// </param>
        /// <param name="element">
        /// Узел, который надо представить
        /// </param>
        /// <param name="whoIsMainInOrder">
        /// Организация, которая главная в заказе
        /// </param>
        /// <param name="techRouteNode">
        /// структура предприятия
        /// </param>
        /// <param name="agents">
        /// Набор организаций
        /// </param>
        private void BuildNodeRecursive(MyNode mainNode, IntermechTreeElement element, string whoIsMainInOrder, TechRouteNode techRouteNode, IDictionary<long, Agent> agents)
        {
            if (element.Children.Count > 0)
            {
                foreach (IntermechTreeElement child in element.Children)
                {

                    // пропускаем всё неинтересное
                    if (child.RelationName == "Технологический состав" || child.RelationName == "Документ" || child.Type == 1128 || child.Type == 1105 || child.Type == 1138 || child.Type == 1088 || child.Type == 1125)
                    {
                        continue;
                    }

                    MyNode childNode = new MyNode();
                    childNode.Id = child.Id;
                    childNode.ObjectId = child.ObjectId;
                    childNode.Type = child.Type;
                    childNode.PcbVersion = child.PcbVersion;
                    childNode.IsPcb = child.IsPcb;
                    childNode.Designation = child.Designation;
                    childNode.Name = child.Name;
                    childNode.Amount = child.Amount.ToString("F0");
                    childNode.AmountWithUse = child.AmountWithUse;
                    childNode.TechTask = child.TechTask;
                    childNode.StockRate = child.StockRate;
                    childNode.SampleSize = child.SampleSize;
                    childNode.TechTask = child.TechTask;
                    
                    if (childNode.IsPcb)
                    {
                        childNode.Image = Properties.Resources.pcb_16;
                        string pcbName = string.Format("{0} (V{1})", childNode.Name, child.PcbVersion);
                        childNode.Name = pcbName;
                    }




                    if (child.TechProcessReference != null)
                    {
                        childNode.TechProcessReference = child.TechProcessReference.Name;
                    }

                    // если маршрут заполнен
                    if (child.TechRoute != null)
                    {
                        // выцепляем данные по фильтру предприятия
                        string data = child.TechRoute;

                        TechRouteStringBuilder techRouteStringBuilder = new TechRouteStringBuilder();
                        string str = techRouteStringBuilder.Build(data, techRouteNode);
                        childNode.Route = str.TrimEnd(new char[] { ' ', '\\' });
                    }

                    // примечание
                    if (child.RouteNote != null)
                    {
                        string routeNote = child.RouteNote;
                        childNode.Note = routeNote;
                    }
                    else
                    {
                        childNode.Note = string.Empty;
                    }

                    if (child.RelationNote != null)
                    {
                        string note = child.RelationNote;
                        childNode.RelationNote = note;
                    }

                    childNode.SubInfo = child.SubstituteInfo;
                    childNode.CooperationFlag = child.CooperationFlag;
                    childNode.InnerCooperation = child.InnerCooperation;
                    childNode.ContainsInnerCooperation = child.ContainsInnerCooperation;
                    childNode.Tag = child;
                    childNode.DoNotProduce = child.ProduseSign;
                    mainNode.Nodes.Add(childNode);

                    if (child.Agent != null)
                    {
                        if (child.Agent != string.Empty)
                        {

                            if (child.Agent.Split(';').Length > 1)
                            {
                                childNode.IsMultipleAgents = true;
                            }

                            if (!child.Agent.Contains(whoIsMainInOrder))
                            {
                                childNode.CooperationFlag = true;
                                childNode.AnotherAgent = true;
                                StringBuilder sb = new StringBuilder();

                                string[] lines = child.Agent.Split(';');

                                if (lines.Length > 0)
                                {
                                    sb.AppendFormat(agents[long.Parse(lines[0])].Name);
                                }

                                for (int i = 1; i < lines.Length; i++)
                                {
                                    sb.AppendFormat(";{0}", agents[long.Parse(lines[i])].Name);
                                }

                                childNode.Agent = sb.ToString();
                                continue;
                            }
                        }
                    }

                    BuildNodeRecursive(childNode, child, whoIsMainInOrder, techRouteNode, agents);
                }
            }
        }


        /// <summary>
        /// Переход в папку архива предприятия
        /// </summary>
        /// <param name="designation">
        /// The designation.
        /// </param>
        public void GoToOldArchive(string designation)
        {
            _openFolderService.OpenFolder(designation);
        }

        public void SetInnerCooperation(IntermechTreeElement element, bool value)
        {
            element.InnerCooperation = value;
            element.ContainsInnerCooperation = value;
            IntermechTreeElement parent = element.Parent;

            while (parent != null)
            {
                parent.ContainsInnerCooperation = value;
                parent = parent.Parent;
            }

        }

        public Task DownloadTechInfoFromIPS(MyNode mainNode)
        {
            Action action = () =>
            {
                DownloadTechInfoFromIPSInternal(mainNode);
            };

            return Task.Run(action);
        }


        private void DownloadTechInfoFromIPSInternal(MyNode mainNode)
        {

            IntermechTreeElement root = mainNode.Tag as IntermechTreeElement;
            if (root == null)
            {
                return;
            }

            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();

            foreach (IntermechTreeElement child in root.Children)
            {
                queue.Enqueue(child);
            }

            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                // найти маршрут обработки
                // найти заготовку
                // если это деталь, то надо забрать нормы расхода материала с неё и подтянуть к материалу детали

            }
        }

        public async Task UpdateNodeFromIPS(MyNode node, IDictionary<long, TechRouteNode> organizationStructDictionary, string organizationName, string productionType)
        {
            // здесь мы получили один или несколько тех. процессов с цехозаходами внутри. Тех. процессы отсортированы в порядке следования атрибута связи "Сортировка"
            ICollection<TechRouteNode> routes = null;
            try
            {
                routes = await _repository.GetTechRouteAsync((IntermechTreeElement)node.Tag, organizationStructDictionary, organizationName,productionType);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message, ex);
            }

            if (routes.Count == 1)
            {
                IList<TechRouteNode> techRouteNodesList = routes.ToList()[0].Children;
                if (techRouteNodesList.Count > 1)
                {
                    _techRouteSetService.SetTechRoute(new List<MyNode>() { node }, techRouteNodesList[0].Children, false);

                    for (int i = 1; i < techRouteNodesList.Count; i++)
                    {
                        _techRouteSetService.SetTechRoute(new List<MyNode>() { node }, techRouteNodesList[i].Children, true);
                    }
                }

                if (techRouteNodesList.Count == 1)
                {
                    _techRouteSetService.SetTechRoute(new List<MyNode>() { node }, techRouteNodesList[0].Children, false);
                }

                Queue<MyNode> queue = new Queue<MyNode>();
                queue.Enqueue(node);
                while (queue.Count > 0)
                {
                    MyNode nodeFromQueue = queue.Dequeue();
                    nodeFromQueue.CooperationFlag = ((IntermechTreeElement)nodeFromQueue.Tag).CooperationFlag;

                    foreach (MyNode child in nodeFromQueue.Nodes)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            if (routes.Count > 1)
            {
                node.Route = "ЕСТЬ НЕСКОЛЬКО МАРШРУТОВ!";
            }
        }
    }
}