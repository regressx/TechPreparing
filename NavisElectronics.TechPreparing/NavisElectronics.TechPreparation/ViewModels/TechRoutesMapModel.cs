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
        public TreeModel GetTreeModel(IntermechTreeElement element, string whoIsMainInOrder, string agentFilter)
        {
            TreeModel model = new TreeModel();
            if (whoIsMainInOrder != agentFilter)
            {

                ICollection<IntermechTreeElement> cooperationElements = new List<IntermechTreeElement>();
                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(element);
                while (queue.Count > 0)
                {
                    IntermechTreeElement elementFromQueue = queue.Dequeue();
                    if (elementFromQueue.Agent == agentFilter)
                    {
                        cooperationElements.Add(elementFromQueue);
                        continue;
                    }

                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }

                }

                foreach (IntermechTreeElement cooperationElement in cooperationElements)
                {
                    MyNode cooperationNode = new MyNode();
                    cooperationNode.Id = cooperationElement.Id;
                    cooperationNode.Type = cooperationElement.Type;
                    cooperationNode.Designation = cooperationElement.Designation;
                    cooperationNode.Name = cooperationElement.Name;
                    cooperationNode.Amount = cooperationElement.Amount.ToString("F3");
                    cooperationNode.AmountWithUse = cooperationElement.AmountWithUse.ToString("F3");
                    cooperationNode.TotalAmount = cooperationElement.TotalAmount.ToString("F3");
                    cooperationNode.StockRate = cooperationElement.StockRate.ToString("F3");
                    cooperationNode.SampleSize = cooperationElement.SampleSize;
                    cooperationNode.TechProcessReference = cooperationElement.TechProcessReference;
                    cooperationNode.Note = cooperationElement.Note;
                    cooperationNode.SubstituteInfo = cooperationElement.SubstituteInfo;
                    cooperationNode.CooperationFlag = cooperationElement.CooperationFlag;
                    cooperationNode.IsPcb = cooperationElement.IsPCB;
                    cooperationNode.PcbVersion = cooperationElement.PcbVersion;
                    cooperationNode.TechTask = cooperationElement.TechTask;
                    cooperationNode.Tag = cooperationElement;

                    BuildNodeRecursive(cooperationNode, cooperationElement, agentFilter);
                    model.Nodes.Add(cooperationNode);
                }




            }
            else
            {
                MyNode mainNode = new MyNode(element.Id.ToString());
                mainNode.Id = element.Id;
                mainNode.Type = element.Type;
                mainNode.PcbVersion = element.PcbVersion;
                mainNode.IsPcb = element.IsPCB;
                mainNode.Designation = element.Designation;
                mainNode.Name = element.Name;
                mainNode.Amount = element.Amount.ToString("F3");
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

                // строим всё
                BuildNodeRecursive(mainNode, element, agentFilter);

                model.Nodes.Add(mainNode);
            }


            return model;
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
        private void BuildNodeRecursive(MyNode mainNode, IntermechTreeElement element, string agentFilter)
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
            return _reader.GetWorkshopsAsync();
        }

        /// <summary>
        /// Асинхронно получаем данные об агентах
        /// </summary>
        /// <returns>Возвращает коллекцию узлов тех. маршрута</returns>
        private Task<ICollection<Agent>> GetAgents()
        {
            return _reader.GetAllAgentsAsync();
        }


    }
}