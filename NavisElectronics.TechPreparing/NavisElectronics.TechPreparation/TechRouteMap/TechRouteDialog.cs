using NavisElectronics.TechPreparation.Services;

namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Entities;
    using Interfaces.Entities;
    using Presenters;
    using TenTec.Windows.iGridLib;
    using ViewModels.TreeNodes;

    public partial class TechRouteDialog : Form
    {
        /// <summary>
        /// The _root.
        /// </summary>
        private readonly MyNode _root;

        private TechRouteNode _techRouteNode;

        /// <summary>
        /// The _element to show cooperation.
        /// </summary>
        private readonly MyNode _elementToShowTechRoute;

        /// <summary>
        /// The _menu strip.
        /// </summary>
        private readonly ContextMenuStrip _menuStrip;

        private readonly IPresentationFactory _presentationFactory;

        public TechRouteDialog(MyNode root, MyNode elementToShowTechRoute, IPresentationFactory presentationFactory, TechRouteNode techRouteNode)
        {
            InitializeComponent();
            _root = root;
            _elementToShowTechRoute = elementToShowTechRoute;
            _presentationFactory = presentationFactory;
            _techRouteNode = techRouteNode;
            _menuStrip = new ContextMenuStrip();


            ToolStripMenuItem menuItemSetAllCooperation = new ToolStripMenuItem("Проставить указанный маршрут всем элементам",null,
                (sender, args) =>
                {
                    IPresenter<TechRouteNode, IList<TechRouteNode>> presenter = _presentationFactory.GetPresenter<TechRoutePresenter, TechRouteNode, IList<TechRouteNode>>();
                    IList<TechRouteNode> resultNodesList = new List<TechRouteNode>();
                    presenter.Run(_techRouteNode, resultNodesList);

                    if (resultNodesList.Count == 0)
                    {
                        return;
                    }

                    foreach (iGRow row in iGrid1.Rows)
                    {
                        SetTechRoute(resultNodesList, row);
                    }
                });

            ToolStripMenuItem menuItemSetCooperation = new ToolStripMenuItem("Проставить маршрут",Properties.Resources.if_stock_new_meeting_21476,
                (sender, args) =>
                {
                    IPresenter<TechRouteNode, IList<TechRouteNode>> presenter = _presentationFactory.GetPresenter<TechRoutePresenter, TechRouteNode, IList<TechRouteNode>>();
                    IList<TechRouteNode> resultNodesList = new List<TechRouteNode>();
                    presenter.Run(_techRouteNode, resultNodesList);

                    if (resultNodesList.Count == 0)
                    {
                        return;
                    }

                    iGRow row = iGrid1.SelectedCells[0].Row;
                    SetTechRoute(resultNodesList, row);
                });

            ToolStripMenuItem menuItemRemoveCooperation = new ToolStripMenuItem("Убрать маршрут",null,
                (sender, args) =>
                {
                    iGRow row = iGrid1.SelectedCells[0].Row;

                    ExtractedObject<MyNode> extractedObject = (ExtractedObject<MyNode>)row.Tag;
                    MyNode elementFromExtactedObject = extractedObject.TreeElement;
                    elementFromExtactedObject.Route = string.Empty;
                    IntermechTreeElement taggedElement = (IntermechTreeElement)elementFromExtactedObject.Tag;
                    taggedElement.TechRoute = string.Empty;
                    row.Cells[2].Value = string.Empty;
                });

            _menuStrip.Items.Add(menuItemSetCooperation);
            _menuStrip.Items.Add(menuItemSetAllCooperation);
            _menuStrip.Items.Add(menuItemRemoveCooperation);
            iGrid1.ContextMenuStrip = _menuStrip;
        }


        private void SetTechRoute(IList<TechRouteNode> route, iGRow row)
        {
            ExtractedObject<MyNode> extractedObject = (ExtractedObject<MyNode>)row.Tag;
            MyNode elementFromExtactedObject = extractedObject.TreeElement;


            TechRouteSetService setTechRouteService = new TechRouteSetService();
            setTechRouteService.SetTechRoute(new List<MyNode>() { elementFromExtactedObject }, route, false);

            row.Cells[2].Value = elementFromExtactedObject.Route;
        }


        private async void TechRouteDialog_Load(object sender, EventArgs e)
        {
            Func<ICollection<ExtractedObject<MyNode>>> func = () =>
            {
                ICollection<ExtractedObject<MyNode>> extractedObjects = new List<ExtractedObject<MyNode>>();

                Queue<MyNode> queue = new Queue<MyNode>();
                queue.Enqueue(_root);
                while (queue.Count > 0)
                {
                    MyNode elementFromQueue = queue.Dequeue();
                    if (elementFromQueue.Id == _elementToShowTechRoute.Id)
                    {
                        ExtractedObject<MyNode> currentObject = new ExtractedObject<MyNode>(elementFromQueue)
                        {
                            TechRoute = elementFromQueue.Route,
                            Designation = ((MyNode)elementFromQueue.Parent).Designation,
                            Name = ((MyNode)elementFromQueue.Parent).Name
                        };
                        extractedObjects.Add(currentObject);
                    }

                    foreach (var child in elementFromQueue.Nodes)
                    {
                        queue.Enqueue((MyNode)child);
                    }
                }

                return extractedObjects;
            };

            ICollection<ExtractedObject<MyNode>> objectsToShow = await Task.Run(func);

            iGrid1.Rows.Clear();
            iGrid1.Rows.AddRange(objectsToShow.Count);
            int i = 0;
            foreach (ExtractedObject<MyNode> extractedObject in objectsToShow)
            {
                iGrid1.Rows[i].Cells[0].Value = extractedObject.Designation;
                iGrid1.Rows[i].Cells[1].Value = extractedObject.Name;
                iGrid1.Rows[i].Cells[2].Value = extractedObject.TechRoute;
                iGrid1.Rows[i].Tag = extractedObject;
                i++;
            }
        }
    }
}
