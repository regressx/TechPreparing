// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TechRoutesMapModel.cs" company="NavisElectronics">
//   ---
// </copyright>
// <summary>
//   Defines the TechRoutesMapModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Intermech.Navigator.DBObjects;

namespace NavisElectronics.TechPreparation.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Aga.Controls.Tree;
    using Entities;
    using IO;
    using Services;
    using TreeNodes;

    /// <summary>
    /// The tech routes map model.
    /// </summary>
    public class TechRoutesMapModel
    {
        /// <summary>
        /// репозиторий с данными
        /// </summary>
        private readonly IDataRepository _reader;


        /// <summary>
        /// Класс, умеющий получать данные по отдельному агенту
        /// </summary>
        private TechAgentDataExtractor _dataExtractor;

        /// <summary>
        /// менеджер буфера обмена
        /// </summary>
        private ClipboardManager _clipboardManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TechRoutesMapModel"/> class.
        /// </summary>
        /// <param name="reader">
        /// Репозиторий с данными
        /// </param>
        public TechRoutesMapModel(IDataRepository reader)
        {
            _reader = reader;
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
        /// Метод получает модель дерева для отображения
        /// </summary>
        /// <param name="element">
        /// Корень
        /// </param>
        /// <param name="whoIsMainInOrder">
        /// Кто главный в заказе
        /// </param>
        /// <param name="agentFilter">
        /// Агент, по которому фильтруем данные
        /// </param>
        /// <returns>
        /// The <see cref="MyNode"/>.
        /// </returns>
        public TreeModel GetTreeModel(IntermechTreeElement root, string whoIsMainInOrder, string agentFilter, TechRouteNode techRouteNode, IDictionary<long, Agent> agents)
        {
            TreeModel model = new TreeModel();
            if (whoIsMainInOrder != agentFilter)
            {
                ICollection<IntermechTreeElement> elements = new List<IntermechTreeElement>();
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {
                    IntermechTreeElement elementFromQueue = queue.Dequeue();
                    if (elementFromQueue.Agent == agentFilter)
                    {
                        elements.Add(elementFromQueue);
                        continue;
                    }

                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }

                foreach (IntermechTreeElement myElement in elements)
                {
                    MyNode myNode = CreateNode(myElement);
                    BuildNodeRecursive(myNode, myElement, agentFilter, techRouteNode, agents);
                    model.Nodes.Add(myNode);
                }
            }
            else
            {
                MyNode mainNode = CreateNode(root);
                
                // строим всё
                BuildNodeRecursive(mainNode, root, agentFilter, techRouteNode, agents);
                model.Nodes.Add(mainNode);
            }


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
        /// Получение составного узла рекурсивно
        /// </summary>
        /// <param name="mainNode">
        /// Главный узел
        /// </param>
        /// <param name="element">
        /// Элемент, из которого получаем данные
        /// </param>
        /// <param name="agentFilter">
        /// Фильтр по предприятию-изготовителю
        /// </param>
        private void BuildNodeRecursive(MyNode mainNode, IntermechTreeElement element, string agentFilter, TechRouteNode techRouteNode, IDictionary<long, Agent> agents)
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
                    //childNode.Type = child.Type;
                    childNode.PcbVersion = child.PcbVersion;
                    childNode.IsPcb = child.IsPCB;
                    //childNode.Designation = child.Designation;
                    //childNode.Name = child.Name;
                    //childNode.Amount = child.Amount.ToString("F0");
                    //childNode.AmountWithUse = (int)child.AmountWithUse;
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
                            string[] routeNodes = route.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

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

                        childNode.Route = sb.ToString().TrimEnd(new char[] { ' ', '\\' });
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

                    BuildNodeRecursive(childNode, child, agentFilter, techRouteNode, agents);
                }
            }
        }

        /// <summary>
        /// Асинхронно получаем данные о узлах тех. маршрута
        /// </summary>
        /// <returns>Возвращает коллекцию узлов тех. маршрута</returns>
        public Task<TechRouteNode> GetWorkShops()
        {
            return _reader.GetWorkshopsAsync();
        }

        /// <summary>
        /// Асинхронно получаем данные об агентах
        /// </summary>
        /// <returns>Возвращает коллекцию узлов тех. маршрута</returns>
        public async Task<IDictionary<long, Agent>> GetAgents()
        {
            ICollection<Agent> agents = await _reader.GetAllAgentsAsync();

            IDictionary<long, Agent> agentsDictionary = new Dictionary<long, Agent>();
            foreach (Agent agent in agents)
            {
                if (!agentsDictionary.ContainsKey(agent.Id))
                {
                    agentsDictionary.Add(agent.Id, agent);
                }
            }

            return agentsDictionary;
        }


    }
}