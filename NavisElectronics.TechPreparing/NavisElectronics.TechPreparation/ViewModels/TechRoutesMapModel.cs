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
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.IO;
    using NavisElectronics.TechPreparation.Services;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// The tech routes map model.
    /// </summary>
    public class TechRoutesMapModel
    {
        /// <summary>
        /// The _data extractor.
        /// </summary>
        private TechAgentDataExtractor _dataExtractor;

        /// <summary>
        /// The _clipboard manager.
        /// </summary>
        private ClipboardManager _clipboardManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRoutesMapModel"/> class.
        /// </summary>
        public TechRoutesMapModel()
        {
            _dataExtractor = new TechAgentDataExtractor();
            _clipboardManager = new ClipboardManager();
        }

        /// <summary>
        /// The paste.
        /// </summary>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        /// <param name="agentFilter">
        /// The agent filter.
        /// </param>
        public void Paste(ICollection<MyNode> nodes, string agentFilter)
        {
            _clipboardManager.Paste(nodes, agentFilter);
        }

        /// <summary>
        /// The copy.
        /// </summary>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        /// <param name="agentFilter">
        /// The agent filter.
        /// </param>
        public void Copy(ICollection<MyNode> nodes, string agentFilter)
        {
            _clipboardManager.Copy(nodes, agentFilter);
        }

        /// <summary>
        /// The build tree.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <param name="techRouteNode">
        /// The tech route node.
        /// </param>
        /// <param name="agentFilter">
        /// The agent filter.
        /// </param>
        /// <param name="agents">
        /// The agents.
        /// </param>
        /// <returns>
        /// The <see cref="MyNode"/>.
        /// </returns>
        public MyNode BuildTree(IntermechTreeElement element, TechRouteNode techRouteNode,  string whoIsMainInOrder, string agentFilter, IDictionary<long, Agent> agents)
        {
            MyNode mainNode = new MyNode(element.Id.ToString());
            mainNode.Id = element.Id;
            mainNode.Type = element.Type;
            mainNode.PcbVersion = element.PcbVersion;
            mainNode.IsPcb = element.IsPCB;
            mainNode.Designation = element.Designation;
            mainNode.Name = element.Name;
            mainNode.Amount = element.Amount.ToString();
            mainNode.Route = element.TechRoute;
            mainNode.Note = element.RouteNote;
            mainNode.CooperationFlag = element.CooperationFlag;
            mainNode.InnerCooperation = element.InnerCooperation;
            mainNode.ContainsInnerCooperation = element.ContainsInnerCooperation;
            mainNode.Agent = element.Agent == null ? string.Empty : element.Agent;
            mainNode.Tag = element;
            mainNode.IsToComplect = element.IsToComplect;
            mainNode.TechPreparing = element.TechTask;
            if (element.TechProcessReference != null)
            {
                mainNode.TechProcessReference = element.TechProcessReference.Name;
            }

            BuildNodeRecursive(mainNode, element, techRouteNode, agentFilter, agents);
            return mainNode;
        }


        /// <summary>
        /// Получение составного узла рекурсивно
        /// </summary>
        /// <param name="mainNode">
        /// Главный узел
        /// </param>
        /// <param name="element">
        /// Элемент, из которого получаем данные
        /// </param>
        /// <param name="techRouteNode">
        /// Главное дерево цехов
        /// </param>
        /// <param name="agentFilter">
        /// Фильтр по предприятию-изготовителю
        /// </param>
        /// <param name="agents">
        /// The agents.
        /// </param>
        private void BuildNodeRecursive(MyNode mainNode, IntermechTreeElement element, TechRouteNode techRouteNode, string agentFilter, IDictionary<long, Agent> agents)
        {
            if (element.Children.Count > 0)
            {
                foreach (IntermechTreeElement child in element.Children)
                {
                    if (child.Type == 1128 || child.Type == 1105 || child.Type == 1138 || child.Type == 1088 || child.Type == 1125)
                    {
                        continue;
                    }

                    MyNode childNode = new MyNode(child.Id.ToString());

                    childNode.Id = child.Id;
                    childNode.Type = child.Type;
                    childNode.PcbVersion = child.PcbVersion;
                    childNode.IsPcb = child.IsPCB;
                    childNode.Designation = child.Designation;
                    childNode.Name = child.Name;
                    childNode.Amount = child.Amount.ToString("F0");
                    childNode.Type = child.Type;
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
                        string data = _dataExtractor.ExtractData(child.TechRoute, agentFilter);
 
                        // здесь существующие маршруты по выделенному предприятию
                        string[] routes = data.Split(new char[] { '|', '|' }, StringSplitOptions.RemoveEmptyEntries);

                        StringBuilder sb = new StringBuilder();

                        foreach (string route in routes)
                        {
                            string[] routeNodes = route.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

                            if (routeNodes.Length > 0 && routeNodes[0] != string.Empty)
                            {

                                TechRouteNode routeNode = techRouteNode.Find(Convert.ToInt64(routeNodes[0]));
                                if (routeNode != null)
                                {
                                    sb.Append(routeNode.GetCaption());
                                }

                            }
                            for (int i = 1; i < routeNodes.Length; i++)
                            {
                                TechRouteNode routeNode = techRouteNode.Find(Convert.ToInt64(routeNodes[i]));
                                string value = "null";
                                if (routeNode != null)
                                {
                                    value = routeNode.GetCaption();
                                }
                                sb.AppendFormat("-{0}", value);
                            }
                            sb.Append(" \\ ");
                        }

                        childNode.Route = sb.ToString().TrimEnd(new char[] {' ', '\\' });
                    }

                    if (child.RouteNote != null)
                    {
                        string routeNote = _dataExtractor.ExtractData(child.RouteNote, agentFilter);
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

                            if (!child.Agent.Contains(agentFilter))
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

                    BuildNodeRecursive(childNode, child, techRouteNode, agentFilter, agents);
                }
            }
        }

        /// <summary>
        /// Асинхронно получаем данные о узлах тех. маршрута
        /// </summary>
        /// <returns>Возвращает коллекцию узлов тех. маршрута</returns>
        public Task<TechRouteNode> GetWorkShops()
        {
            IntermechReader reader = new IntermechReader();
            return reader.GetWorkshopsAsync();
        }




    }
}