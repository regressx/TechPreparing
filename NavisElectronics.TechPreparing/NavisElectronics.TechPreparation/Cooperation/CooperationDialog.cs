namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Entities;
    using Interfaces.Entities;
    using TenTec.Windows.iGridLib;
    using ViewModels.TreeNodes;

    /// <summary>
    /// The cooperation dialog.
    /// </summary>
    public partial class CooperationDialog : Form
    {
        /// <summary>
        /// The _root.
        /// </summary>
        private readonly CooperationNode _root;

        /// <summary>
        /// The _element to show cooperation.
        /// </summary>
        private readonly CooperationNode _elementToShowCooperation;

        /// <summary>
        /// The _menu strip.
        /// </summary>
        private readonly ContextMenuStrip _menuStrip;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationDialog"/> class.
        /// </summary>
        /// <param name="root">
        /// The root.
        /// </param>
        /// <param name="elementToShowCooperation">
        /// The element to show cooperation.
        /// </param>
        public CooperationDialog(CooperationNode root, CooperationNode elementToShowCooperation)
        {
            _root = root;
            _elementToShowCooperation = elementToShowCooperation;
            InitializeComponent();
            _menuStrip = new ContextMenuStrip();


            ToolStripMenuItem menuItemSetAllCooperation = new ToolStripMenuItem("Проставить всем",null,
                (sender, args) =>
                {
                    foreach (iGRow row in iGrid1.Rows)
                    {
                        SetCooperation(true, row);
                    }
                });

            ToolStripMenuItem menuItemSetCooperation = new ToolStripMenuItem("Проставить кооперацию",Properties.Resources.if_stock_new_meeting_21476,
                (sender, args) =>
                {
                    iGRow row = iGrid1.SelectedCells[0].Row;
                    SetCooperation(true,row);
                });

            ToolStripMenuItem menuItemRemoveCooperation = new ToolStripMenuItem("Убрать кооперацию",null,
                (sender, args) =>
                {
                    iGRow row = iGrid1.SelectedCells[0].Row;
                    SetCooperation(false, row);
                });

            _menuStrip.Items.Add(menuItemSetCooperation);
            _menuStrip.Items.Add(menuItemSetAllCooperation);
            _menuStrip.Items.Add(menuItemRemoveCooperation);
            iGrid1.ContextMenuStrip = _menuStrip;
        }

        private void SetCooperation(bool value, iGRow row)
        {
            ExtractedObject<CooperationNode> extractedObject = (ExtractedObject<CooperationNode>)row.Tag;
            CooperationNode elementFromExtactedObject = extractedObject.TreeElement;

            Queue<CooperationNode> queue = new Queue<CooperationNode>();
            queue.Enqueue(elementFromExtactedObject);
            while (queue.Count > 0)
            {
                CooperationNode elementFromQueue = queue.Dequeue();
                elementFromQueue.CooperationFlag = value;
                IntermechTreeElement taggedElement = (IntermechTreeElement)elementFromQueue.Tag;
                taggedElement.CooperationFlag = value;
                foreach (var child in elementFromQueue.Nodes)
                {
                    queue.Enqueue((CooperationNode)child);
                }
            }

            row.Cells[2].Value = value;
        }

        private async void CooperationDialog_Load(object sender, System.EventArgs e)
        {
            Func<ICollection<ExtractedObject<CooperationNode>>> func = () =>
            {
                ICollection<ExtractedObject<CooperationNode>> extractedObjects = new List<ExtractedObject<CooperationNode>>();

                Queue<CooperationNode> queue = new Queue<CooperationNode>();
                queue.Enqueue(_root);
                while (queue.Count > 0)
                {
                    CooperationNode elementFromQueue = queue.Dequeue();
                    if (elementFromQueue.Id == _elementToShowCooperation.Id)
                    {
                        ExtractedObject<CooperationNode> currentObject = new ExtractedObject<CooperationNode>(elementFromQueue)
                        {
                            CooperationFlag = elementFromQueue.CooperationFlag,
                            Designation = ((CooperationNode)elementFromQueue.Parent).Designation,
                            Name = ((CooperationNode)elementFromQueue.Parent).Name
                        };
                        extractedObjects.Add(currentObject);
                    }

                    foreach (var child in elementFromQueue.Nodes)
                    {
                        queue.Enqueue((CooperationNode)child);
                    }
                }

                return extractedObjects;
            };

            ICollection<ExtractedObject<CooperationNode>> objectsToShow = await Task.Run(func);

            iGrid1.Rows.Clear();
            iGrid1.Rows.AddRange(objectsToShow.Count);
            int i = 0;
            foreach (ExtractedObject<CooperationNode> extractedObject in objectsToShow)
            {
                iGrid1.Rows[i].Cells[0].Value = extractedObject.Designation;
                iGrid1.Rows[i].Cells[1].Value = extractedObject.Name;
                iGrid1.Rows[i].Cells[2].Value = extractedObject.CooperationFlag;
                iGrid1.Rows[i].Tag = extractedObject;
                i++;
            }

        }
    }
}
