namespace NavisElectronics.ListOfCooperation.Reports.CooperationList
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using ViewModels;

    /// <summary>
    /// The cooperation list factory.
    /// </summary>
    [Obsolete("Не использовать! Этот класс строил Ведомость кооперации в ширину: могли быть повторяющиеся сборки на нескольких листах")]
    public class CooperationListFactory
    {
        /// <summary>
        /// The _main node.
        /// </summary>
        private readonly CooperationNode _mainNode;

        /// <summary>
        /// The _root.
        /// </summary>
        private readonly IntermechTreeElement _root;
        
        /// <summary>
        /// Количество помещающихся на листе столбцов для сборочных единиц
        /// </summary>
        private const byte AssembliesColumnsCount = 6;

        /// <summary>
        /// Количество элементов, отображаемое на странице
        /// </summary>
        private const byte ElementsOnPageCount = 10;


        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationListFactory"/> class.
        /// </summary>
        /// <param name="mainNode">
        /// The main node.
        /// </param>
        public CooperationListFactory(CooperationNode mainNode)
        {
            _mainNode = mainNode;
            _root = mainNode.Tag as IntermechTreeElement;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="CooperationListDocument"/>.
        /// </returns>
        public CooperationListDocument Create()
        {
            // заполнить словарик уникальных элементов, делающихся по кооперации
            IDictionary<long, ExtractedObject> uniqueCooperationElements = new Dictionary<long, ExtractedObject>();

            // Очередь для прохода в ширину
            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            int rootAmount = (int)((IntermechTreeElement)_mainNode.Tag).Amount;
            queue.Enqueue(_mainNode);
            while (queue.Count > 0)
            {
                CooperationNode nodeFromQueue = queue.Dequeue();

                if (nodeFromQueue.Type == 1078 || nodeFromQueue.Type == 1074 || nodeFromQueue.Type == 1159 ||
                    nodeFromQueue.Type == 1052 || nodeFromQueue.Type == 1097)
                {
                    if (nodeFromQueue.CooperationFlag)
                    {
                        if (!uniqueCooperationElements.ContainsKey(nodeFromQueue.Id))
                        {
                            ExtractedObject currentObject = new ExtractedObject();
                            currentObject.Id = nodeFromQueue.Id;
                            currentObject.Type = nodeFromQueue.Type;
                            currentObject.Name = nodeFromQueue.Name;
                            currentObject.Designation = nodeFromQueue.Designation;
                            currentObject.CooperationFlag = nodeFromQueue.CooperationFlag;
                            currentObject.IsPcb = nodeFromQueue.IsPcb;
                            currentObject.PcbVersion = nodeFromQueue.PcbVersion.ToString();
                            currentObject.MeasureUnits = ((IntermechTreeElement)nodeFromQueue.Tag).MeasureUnits;
                            uniqueCooperationElements.Add(nodeFromQueue.Id, currentObject);
                            IList<IntermechTreeElement> elementsInTree = _root.Find(nodeFromQueue.Id);

                            foreach (IntermechTreeElement intermechTreeElement in elementsInTree)
                            {
                                if (intermechTreeElement.Parent.CooperationFlag == false)
                                {
                                    ExtractedObject parentReportElement = new ExtractedObject();
                                    parentReportElement.Type = intermechTreeElement.Parent.Type;
                                    parentReportElement.Designation = intermechTreeElement.Parent.Designation;
                                    parentReportElement.Name = intermechTreeElement.Parent.Name;
                                    parentReportElement.Id = intermechTreeElement.Parent.Id;
                                    parentReportElement.Amount = intermechTreeElement.Amount;
                                    currentObject.RegisterElement(parentReportElement);

                                    IList<IntermechTreeElement> allParents = _root.Find(parentReportElement.Id);
                                    int total = 0;
                                    foreach (IntermechTreeElement treeElement in allParents)
                                    {
                                        if (treeElement.CooperationFlag == false)
                                        {
                                            total += (int)treeElement.AmountWithUse;
                                        }
                                    }

                                    parentReportElement.AmountWithUse = (int)(total / rootAmount);
                                }
                            }
                        }

                    }
                }

                // все потомки кооперационных сборок и деталей не должны попасть в очередь
                if (!nodeFromQueue.CooperationFlag)
                {
                    if (nodeFromQueue.Nodes.Count > 0)
                    {
                        foreach (CooperationNode child in nodeFromQueue.Nodes)
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
            }

            // будет две очереди: одна для печатных плат, а другая для обычных элементов
            Queue<ExtractedObject> elementsQueue = new Queue<ExtractedObject>();
            Queue<ExtractedObject> pcpQueue = new Queue<ExtractedObject>();

            foreach (KeyValuePair<long, ExtractedObject> node in uniqueCooperationElements)
            {
                if (node.Value.IsPcb)
                {
                    pcpQueue.Enqueue(node.Value);
                }
                else
                {
                    elementsQueue.Enqueue(node.Value);
                }
            }

            CooperationListDocument document = new CooperationListDocument();
            document.SetCommonPages(FillPages(elementsQueue));
            document.SetPcbPages(FillPages(pcpQueue));
            return document;
        }


        /// <summary>
        /// The fill pages.
        /// </summary>
        /// <param name="elementsQueue">
        /// The elements queue.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection{MyPageDescription}"/>.
        /// </returns>
        private ICollection<MyPageDescription<ExtractedObject>> FillPages(Queue<ExtractedObject> elementsQueue)
        {
            IList<MyPageDescription<ExtractedObject>> pages = new List<MyPageDescription<ExtractedObject>>();
            while (elementsQueue.Count > 0)
            {
                ExtractedObject nodeFromQueue = elementsQueue.Dequeue();
                
                // закинуть в очередь всех родителей элемента
                Queue<ExtractedObject> allParents = new Queue<ExtractedObject>(nodeFromQueue.Elements);

                MyPageDescription<ExtractedObject> pageDescription = null;
                int continuePageIndex = 0;
                
                // создаем новую страницу, если вообще ничего еще не было создано
                if (pages.Count == 0)
                {
                    pageDescription = new MyPageDescription<ExtractedObject>();
                    pages.Add(pageDescription);
                }
                else
                {
                    // берем первую незаполненную элементами страницу
                    foreach (MyPageDescription<ExtractedObject> page in pages)
                    {
                        if (IsFullOfElements(page) == false)
                        {
                            pageDescription = page;
                            break;
                        }

                        continuePageIndex++;
                    }
                }

                // если все строки заполнены, то создаем новую страницу
                if (pageDescription == null)
                {
                    pageDescription = new MyPageDescription<ExtractedObject>();
                    pages.Add(pageDescription);
                }

                // если найденная страница полностью заполнена сборками, 
                if (pageDescription.Assemblies.Count == AssembliesColumnsCount)
                {
                    // то надо проверить входимость элемента в эти сборки на странице
                    bool founded = Founded(pageDescription, nodeFromQueue);

                    // нашли? Круто, добавляем его на страницу
                    if (founded)
                    {
                        pageDescription.Elements.Add(nodeFromQueue);
                    }
                    else
                    {
                        // нет? Тогда надо прошерстить другие страницы таким же способом и добавить этот элемент туда
                        foreach (MyPageDescription<ExtractedObject> page in pages)
                        {
                            
                        }
                    }
                }
                else
                {
                    while (allParents.Count > 0)
                    {
                        //if (pageDescription.Assemblies.Count < AssembliesColumnsCount)
                        //{
                        //    pageDescription.Elements.Add(nodeFromQueue);
                        //    pageDescription.RegisterAssembly(allParents.Dequeue());
                        //}
                        //else
                        //{
                        //    pageDescription = new MyPageDescription<ExtractedObject>();
                        //    pages.Add(pageDescription);
                        //    pageDescription.Elements.Add(nodeFromQueue);
                        //    pageDescription.RegisterAssembly(allParents.Dequeue());
                        //}
                    }
                }
            }

            return pages;
        }

        private bool Founded(MyPageDescription<ExtractedObject> pageDescription, ExtractedObject nodeFromQueue)
        {
            bool founded = false;
            foreach (ExtractedObject assembly in pageDescription.Assemblies)
            {
                foreach (ExtractedObject parent in nodeFromQueue.Elements)
                {
                    if (parent.Id == assembly.Id)
                    {
                        founded = true;
                        break;
                    }
                }

                if (founded)
                {
                    break;
                }
            }

            return founded;
        }


        /// <summary>
        /// Метод проверяет, заполнена ли таблица элементами полностью или нет
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsFullOfElements(MyPageDescription<ExtractedObject> page)
        {
            if (page.Elements.Count >= ElementsOnPageCount)
            {
                page.IsFull = true;
            }

            return page.IsFull; 
        }
    }
}