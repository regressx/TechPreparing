// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechRoutesMapModel.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the TechRoutesMapModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NavisElectronics.TechPreparation.ViewModels
{
    using System.Collections.Generic;
    using System.Text;
    using Aga.Controls.Tree;
    using Interfaces.Entities;
    using Services;
    using TreeNodes;

    /// <summary>
    /// The tech routes map model.
    /// </summary>
    public class TechRoutesMapModel
    {
        private readonly OpenFolderService _openFolderService;
        private readonly ShowFileManager _showFileManager;

        /// <summary>
        /// менеджер буфера обмена
        /// </summary>
        private ClipboardManager _clipboardManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRoutesMapModel"/> class.
        /// </summary>
        /// <param name="openFolderService">
        /// The open Folder Service.
        /// </param>
        /// <param name="showFileManager">
        /// просмоторщик
        /// </param>
        public TechRoutesMapModel(OpenFolderService openFolderService, ShowFileManager showFileManager)
        {
            _openFolderService = openFolderService;
            _showFileManager = showFileManager;
            _clipboardManager = new ClipboardManager();
        }

        /// <summary>
        /// The paste.
        /// </summary>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        public void Paste(ICollection<MyNode> nodes)
        {
            _clipboardManager.Paste(nodes);
        }

        /// <summary>
        /// The copy.
        /// </summary>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        public void Copy(ICollection<MyNode> nodes)
        {
            _clipboardManager.Copy(nodes);
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
            myNode.IsPcb = myElement.IsPCB;
            myNode.Designation = myElement.Designation;
            myNode.Name = myElement.Name;
            myNode.Amount = myElement.Amount.ToString("F3");
            myNode.Route = myElement.TechRoute;
            myNode.Note = myElement.RouteNote;
            myNode.CooperationFlag = myElement.CooperationFlag;
            myNode.InnerCooperation = myElement.InnerCooperation;
            myNode.ContainsInnerCooperation = myElement.ContainsInnerCooperation;
            myNode.Agent = myElement.Agent == null ? string.Empty : myElement.Agent;
            myNode.Tag = myElement;
            myNode.IsToComplect = myElement.IsToComplect;
            myNode.TechPreparing = myElement.TechTask;
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
                    if (child.Type == 1128 || child.Type == 1105 || child.Type == 1138 || child.Type == 1088 || child.Type == 1125)
                    {
                        continue;
                    }

                    MyNode childNode = new MyNode();
                    childNode.Id = child.Id;
                    childNode.ObjectId = child.ObjectId;
                    childNode.Type = child.Type;
                    childNode.PcbVersion = child.PcbVersion;
                    childNode.IsPcb = child.IsPCB;
                    childNode.Designation = child.Designation;
                    childNode.Name = child.Name;
                    childNode.Amount = child.Amount.ToString("F0");
                    childNode.AmountWithUse = (int)child.AmountWithUse;
                    childNode.TechPreparing = child.TechTask;
                    childNode.StockRate = child.StockRate;
                    childNode.SampleSize = childNode.SampleSize;

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

                    childNode.SubInfo = child.SubstituteInfo;
                    childNode.CooperationFlag = child.CooperationFlag;
                    childNode.InnerCooperation = child.InnerCooperation;
                    childNode.ContainsInnerCooperation = child.ContainsInnerCooperation;
                    childNode.Tag = child;
                    childNode.IsToComplect = child.IsToComplect;
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


        /// <summary>
        /// The show product card.
        /// </summary>
        /// <param name="element">
        /// Указанный элемент
        /// </param>
        public void ShowProductCard(IntermechTreeElement element)
        {
            _showFileManager.Show(element.Id, element.Type);
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
    }
}