// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPresenter.cs" company="NavisElectronics">
// ---
// </copyright>
// <summary>
//   Посредник между главной формой и ее моделью
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NavisElectronics.TechPreparation.Data;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Helpers;
using NavisElectronics.TechPreparation.Main;
using NavisElectronics.TechPreparation.TechRouteMap;
using NavisElectronics.TechPreparation.TreeComparer;

namespace NavisElectronics.TechPreparation.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using Entities;
    using Enums;
    using EventArguments;
    using Exceptions;
    using Interfaces.Entities;
    using Interfaces.Services;
    using Services;
    using ViewInterfaces;
    using ViewModels;
    using ViewModels.TreeNodes;
    using Views;

    /// <summary>
    /// Посредник между главной формой и ее моделью
    /// </summary>
    public class MainPresenter : BasePresenter<long>
    {
        /// <summary>
        /// Представление
        /// </summary>
        private readonly IMainView _mainView;
        

        /// <summary>
        /// Модель для этого представления
        /// </summary>
        private readonly MainViewModel _model;

        /// <summary>
        /// Фабрика представителей. Умеет выдавать представителя по указке
        /// </summary>
        private readonly IPresentationFactory _presentationFactory;

        /// <summary>
        /// Номер версии объекта, для которого запрашивается состав или атрибут
        /// </summary>
        private long _rootVersionId;

        /// <summary>
        /// Корень
        /// </summary>
        private IntermechTreeElement _rootElement;

        /// <summary>
        /// Словарик агентов
        /// </summary>
        private IDictionary<long, Agent> _agents;

        /// <summary>
        /// Структура предприятия
        /// </summary>
        private TechRouteNode _organizationStruct = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPresenter"/> class. 
        /// Конструктор
        /// </summary>
        /// <param name="mainView">
        /// Интерфейс главного окна
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="presentationFactory">
        /// The presentation Factory.
        /// </param>
        public MainPresenter(IMainView mainView, MainViewModel model, IPresentationFactory presentationFactory)
        {
            _model = model;
            _model.Saving += _model_Saving;
            _mainView = mainView;
            _presentationFactory = presentationFactory;
            _mainView.Load += _view_Load;
            _mainView.CheckOk += MainViewCheckOk;
            _mainView.EditTechRoutes += EditTechRoutes;
            _mainView.CompareTwoTrees += CompareTwoTrees;
            _mainView.Save += Save;
        }

        /// <summary>
        /// Оповещаем представителя о том, что идет сохранение
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void _model_Saving(object sender, SaveServiceEventArgs e)
        {
            _mainView.UpdateStatusLabel(e.Message);
        }

        /// <summary>
        /// Обработчик нажатия кнопки проверки, что всё готово
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainViewCheckOk(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            IntermechTreeElement mainElement = _rootElement as IntermechTreeElement;
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(mainElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                
                // если по кооперации, то пропускаем
                if (elementFromQueue.CooperationFlag || elementFromQueue.RelationName == "Документ" || elementFromQueue.ProduseSign)
                {
                    continue;
                }

                if (elementFromQueue.Amount < 0.000001)
                {
                    string str = string.Format("В узле {0} {1} из состава {2} {3} не указано количество",
                        elementFromQueue.Designation,
                        elementFromQueue.Name,
                        elementFromQueue.Parent.Designation,
                        elementFromQueue.Parent.Name);

                    sb.Append(str + Environment.NewLine);
                }

                if (elementFromQueue.MeasureUnits == string.Empty)
                {
                    string str = string.Format("В узле {0} {1} из состава {2} {3} не указана единица измерения",
                        elementFromQueue.Designation,
                        elementFromQueue.Name,
                        elementFromQueue.Parent.Designation,
                        elementFromQueue.Parent.Name);

                    sb.Append(str + Environment.NewLine);
                }

                if (elementFromQueue.Agent == string.Empty)
                {
                    string str = string.Format("В узле {0} {1} из состава {2} {3} не указан изготовитель",
                        elementFromQueue.Designation,
                        elementFromQueue.Name,
                        elementFromQueue.Parent.Designation,
                        elementFromQueue.Parent.Name);

                    sb.Append(str + Environment.NewLine);
                }


                if (string.IsNullOrEmpty(elementFromQueue.TechRoute) && !elementFromQueue.ProduseSign)
                {
                    string str = string.Format("В узле {0} {1} из состава {2} {3} не указан маршрут",
                        elementFromQueue.Designation,
                        elementFromQueue.Name,
                        elementFromQueue.Parent.Designation,
                        elementFromQueue.Parent.Name);

                    sb.Append(str + Environment.NewLine);
                }


                if (elementFromQueue.Type == 1159 || elementFromQueue.Type == 1052)
                {
                    if (elementFromQueue.Children.Count == 0)
                    {
                        string str = string.Format("В узле {0} {1} из состава {2} {3} не указан материал",
                            elementFromQueue.Designation,
                            elementFromQueue.Name,
                            elementFromQueue.Parent.Designation,
                            elementFromQueue.Parent.Name);

                        sb.Append(str + Environment.NewLine);
                    }
                }

                foreach (IntermechTreeElement child in elementFromQueue.Children)
                {
                    queue.Enqueue(child);
                }
            }

            ReportForm rf = new ReportForm(sb.ToString());
            rf.Show();
        }

        private void CompareTwoTrees(object sender, EventArgs e)
        {
            IPresenter<IntermechTreeElement> presenter =
                _presentationFactory.GetPresenter<TreeComparerPresenter, IntermechTreeElement>();
            presenter.Run(_rootElement);
        }

        private void EditTechRoutes(object sender, EventArgs e)
        {
            IPresenter<Parameter<object>, TechRouteNode, IDictionary<long, Agent>> presenter = _presentationFactory.GetPresenter<TechRouteMapPresenter, Parameter<object>, TechRouteNode, IDictionary<long, Agent>>();
            Parameter<object> parameter = new Parameter<object>();
            parameter.AddParameter(_rootElement);
            parameter.AddParameter(_mainView);
            presenter.Run(parameter, _organizationStruct, _agents);
            _mainView.LayoutMdi(MdiLayout.TileVertical);
        }

        private async void Save(object sender, EventArgs e)
        {
            IntermechTreeElement mainTreeElement = _rootElement;
            await _model.WriteIntoFileAttributeAsync(_rootVersionId, mainTreeElement);
        }

        /// <summary>
        /// The _view_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private async void _view_Load(object sender, EventArgs e)
        {
            //TreeModel treeModel = new TreeModel();
            //ViewNode waitingNode = new ViewNode();
            _mainView.UpdateStatusLabel("Пожалуйста, подождите. Идет загрузка данных");
            //treeModel.Nodes.Add(waitingNode);
            //_mainView.FillTree(treeModel);


            _mainView.LockButtons();

            // загрузить производителей
            ICollection<Agent> agents = await _model.GetAllAgentsAsync();
            _agents = new Dictionary<long, Agent>();
            foreach (Agent agent in agents)
            {
                _agents.Add(agent.Id, agent);
            }
            
            // проверка наличия файла со структурой предприятия
            bool fileStructEmpty = await _model.AttributeExist(_rootVersionId, ConstHelper.OrganizationStructAttribute);
            if (!fileStructEmpty)
            {
                // загрузить структуру предприятия из справочника Imbase
                _organizationStruct = await _model.GetWorkShopsAsync();

                TechRouteNodeAdapter organizationStructWrapper = new TechRouteNodeAdapter(_organizationStruct).Wrap(_organizationStruct);

                TreeViewSettings settings = new TreeViewSettings();
                settings.AddColumn(new TreeColumn("Наименование", 250), "Name");
                settings.ElementToBuild = organizationStructWrapper;
                
                StructDialogViewPresenter<TechRouteNodeView, TechRouteNodeAdapter> presenter = new StructDialogViewPresenter<TechRouteNodeView, TechRouteNodeAdapter>(new StructDialogView(), new StructDialogViewModel<TechRouteNodeView, TechRouteNodeAdapter>());
                presenter.Run(settings);

                if (settings.Result != null)
                {
                    TechRouteNodeAdapter resultNode = (TechRouteNodeAdapter)settings.Result;
                    _organizationStruct = resultNode.TechRouteNode;
                    await _model.WriteBlobAttributeAsync<TechRouteNode>(_rootVersionId, resultNode.TechRouteNode, ConstHelper.OrganizationStructAttribute, "Структура предприятия " + resultNode.Name, new SerializeStrategyBson<TechRouteNode>());
                }
                else
                {
                    MessageBox.Show("Не указана структура предприятия: закройте окно тех. подготовки и откройте заново");
                    return;
                }
            }
            else
            {
                _organizationStruct = await _model.ReadDataFromBlobAttribute<TechRouteNode>(_rootVersionId, ConstHelper.OrganizationStructAttribute);
            }


            // если при загрузке производитель не выбран, то ничего не будем грузить
            if (_organizationStruct.ManufacturerId == 0)
            {
                throw new OrganizationStructNotDownloadedException(
                    "У корневого элемента дерева организации не указан код организации-производителя");
            }

            // пробуем загрузить из файла
            bool fromFile = false;

            try
            {
                _rootElement = await _model.GetTreeFromFileAsync(_rootVersionId);
                if (_rootElement.Id != _rootVersionId)
                {
                    _rootElement.Id = _rootVersionId;
                }
                fromFile = true;
            }
            catch (FileAttributeIsEmptyException)
            {
                fromFile = false;
            }

            // Если не из файла, то надо загрузить из атрибута "Двоичные данные заказа"
            if (!fromFile)
            {
                // проверить атрибут
                if (!await _model.AttributeExist(_rootVersionId,17964))
                {
                    throw new FileAttributeIsEmptyException("У заказа отсутствует заполненный атрибут Двоичные данные заказа. Обратитесь к сотруднику, составлявшему заказ!");
                }

                _rootElement = await _model.ReadDataFromBlobAttribute<IntermechTreeElement>(_rootVersionId, 17964);
                _rootElement.Agent = _organizationStruct.ManufacturerId.ToString();

                Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
                queue.Enqueue(_rootElement);
                while (queue.Count > 0)
                {
                    IntermechTreeElement currentElement = queue.Dequeue();
                    currentElement.Agent = _rootElement.Agent;
                    foreach (IntermechTreeElement child in currentElement.Children)
                    {
                        queue.Enqueue(child);
                    }
                }

            }

            string orderName = string.Format($"{_rootElement.Name} {_agents[long.Parse(_rootElement.Agent)]}. Тех. подготовка");
            _mainView.UpdateCaption(orderName);
            _mainView.UnLockButtons();
            _mainView.UpdateStatusLabel("Загружено. Не сохранено");
        }




        /// <summary>
        /// Запуск главной формы
        /// </summary>
        /// <param name="rootVersionId">
        /// идентификатор версии объекта заказа
        /// </param>
        public override void Run(long rootVersionId)
        {
            _rootVersionId = rootVersionId;
            _mainView.Show();
        }
    }
}