using System;
using System.Collections.Generic;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using NavisElectronics.TechPreparation.Entities;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.ViewModels.TreeNodes;
using TenTec.Windows.iGridLib;

namespace NavisElectronics.TechPreparation.Views
{
    public partial class CooperationDialog : Form
    {
        private readonly CooperationNode _root;
        private readonly CooperationNode _elementToShowCooperation;
        private ContextMenuStrip _menuStrip;
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
