using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Aga.Controls.Tree;
using Intermech.Interfaces.Client;
using NavisElectronics.TechPreparation.EventArguments;
using NavisElectronics.TechPreparation.Interfaces.Entities;
using NavisElectronics.TechPreparation.Main;
using NavisElectronics.TechPreparation.Reports;
using NavisElectronics.TechPreparation.TechRouteMap;
using NavisElectronics.TechPreparation.ViewInterfaces;
using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

namespace NavisElectronics.TechPreparation.Views
{
    public partial class TechRoutesMapWithDataGrid : Form, ITechRouteMap
    {
        private readonly string _manufacturerViewSelectedAgentName;

        /// <summary>
        /// Событие при редактировании тех. маршрута
        /// </summary>
        public event EventHandler<EditTechRouteEventArgs> EditTechRouteClick;
        public event EventHandler<EditTechRouteEventArgs> EditMassTechRouteClick;
        public event EventHandler<SaveClickEventArgs> EditNoteClick;
        public event EventHandler<NodesCollectionEventArgs> DeleteRouteClick;
        public event EventHandler<NodesCollectionEventArgs> CopyClick;
        public event EventHandler<NodesCollectionEventArgs> PasteClick;
        public event EventHandler DownloadInfoFromIPS;
        public event EventHandler UpdateNodeFromIps;

        public event EventHandler<SaveClickEventArgs> GoToOldArchive;
        public event EventHandler CreateReportClick;
        public event EventHandler CreateDevideList;
        public event EventHandler CreateCooperationList;
        public event EventHandler<NodesCollectionEventArgs> SetInnerCooperation;
        public event EventHandler<NodesCollectionEventArgs> RemoveInnerCooperation;
        public event EventHandler RefreshTree;

        public TechRoutesMapWithDataGrid()
        {
            InitializeComponent();
        }

        public void SetTreeModel(TreeModel treeModel)
        {
            treeViewAdv.Model = treeModel;
        }

        public ICollection<MyNode> GetSelectedRows()
        {
            ICollection<MyNode> answer = new List<MyNode>();
            ReadOnlyCollection<TreeNodeAdv> nodes = treeViewAdv.SelectedNodes;
            foreach (TreeNodeAdv node in nodes)
            {
                MyNode currentNode = node.Tag as MyNode;
                answer.Add(currentNode);
            }

            return answer;
        }

        public MyNode GetMainNode()
        {
            MyNode node = treeViewAdv.Root.Children[0].Tag as MyNode;
            return node;
        }

        public TreeViewAdv GetTreeView()
        {
            return treeViewAdv;
        }

        private void treeViewAdv1_RowDraw(object sender, TreeViewRowDrawEventArgs e)
        {
            MyNode node = e.Node.Tag as MyNode;
            if (node.CooperationFlag)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

            if (node.InnerCooperation)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Aquamarine), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

            if (node.DoNotProduce)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }
        }

        private void EditNote_Click(object sender, EventArgs e)
        {
            EventHandler<SaveClickEventArgs> temp = Volatile.Read(ref EditNoteClick);
            if (temp != null)
            {
                MyNode node = treeViewAdv.SelectedNode.Tag as MyNode;
                temp(sender, new SaveClickEventArgs(node));
            }
        }

        private void CopyRouteButton_Click(object sender, EventArgs e)
        {
            EventHandler<NodesCollectionEventArgs> temp = Volatile.Read(ref CopyClick);
            if (temp != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv.SelectedNodes)
                {
                    nodes.Add(node.Tag as MyNode);
                }

                temp(sender, new NodesCollectionEventArgs(nodes));
            }
        }

        private void goToArchiveButton_Click(object sender, EventArgs e)
        {
            if (GoToOldArchive != null)
            {
                MyNode selectedNode = treeViewAdv.SelectedNodes[0].Tag as MyNode;
                GoToOldArchive(sender, new SaveClickEventArgs(selectedNode));
            }
        }


        private void createReport_Click(object sender, EventArgs e)
        {
            if (CreateReportClick != null)
            {
                CreateReportClick(sender, e);
            }
        }

        private void createDevideList_Click(object sender, EventArgs e)
        {
            if (CreateDevideList != null)
            {
                CreateDevideList(sender, e);
            }
        }

        private void TechRoutesMap_Load(object sender, EventArgs e)
        {
            Text = string.Format("Ведомость технологических маршрутов {0}", _manufacturerViewSelectedAgentName);
        }

        private void ExpandAllButton_Click(object sender, EventArgs e)
        {
            treeViewAdv.ExpandAll();
        }

        private void CollapseAllButton_Click(object sender, EventArgs e)
        {
            treeViewAdv.CollapseAll();
        }

        private void SetInnerCooperationButton_Click(object sender, EventArgs e)
        {
            if (SetInnerCooperation != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv.SelectedNodes)
                {
                    nodes.Add(node.Tag as MyNode);
                }

                SetInnerCooperation(sender, new NodesCollectionEventArgs(nodes));
            }
        }

        private void RemoveInnerCooperationButton_Click(object sender, EventArgs e)
        {
            if (RemoveInnerCooperation != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv.SelectedNodes)
                {
                    nodes.Add(node.Tag as MyNode);
                }

                RemoveInnerCooperation(sender, new NodesCollectionEventArgs(nodes));
            }
        }

        private void createSingleCompleteListMenuItem_Click(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService(new Interfaces.Services.RecountService());
            MyNode selectedNode = treeViewAdv.SelectedNode.Tag as MyNode;
            reportService.CreateReport(selectedNode, selectedNode.Name, ReportType.CompleteList, DocumentType.Intermech);
        }

        private void createCooperationListMenuItem_Click(object sender, EventArgs e)
        {
            if (CreateCooperationList != null)
            {
                CreateCooperationList(sender, e);
            }
        }


        private void updateFromIPSButton_Click(object sender, EventArgs e)
        {
            if (UpdateNodeFromIps != null)
            {
                UpdateNodeFromIps(sender, e);
            }
        }

        private void treeViewAdv_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (treeViewAdv.SelectedNode != null)
                {
                    MyNode selectedNode = treeViewAdv.SelectedNode.Tag as MyNode;
                    if (selectedNode != null)
                    {
                        propertyGrid1.SelectedObject = selectedNode;
                    }
                }
            }
        }

        private void produceButton_Click(object sender, EventArgs e)
        {
            SetProduct(false);
        }

        private void doNotProduceButton_Click(object sender, EventArgs e)
        {
            SetProduct(true);
        }

        private void SetProduct(bool value)
        {
            Queue<MyNode> queue = new Queue<MyNode>();
            foreach (TreeNodeAdv node in treeViewAdv.SelectedNodes)
            {
                queue.Enqueue(node.Tag as MyNode);
            }

            while (queue.Count > 0)
            {
                MyNode nodeFromQueue = queue.Dequeue();
                IntermechTreeElement taggedElement = (IntermechTreeElement)nodeFromQueue.Tag;
                nodeFromQueue.DoNotProduce = value;
                taggedElement.ProduseSign = value;

                string note;
                note = value ? "НЕ ИЗГОТАВЛИВАТЬ" : string.Empty;

                nodeFromQueue.RelationNote = note;
                taggedElement.RelationNote = note;
                foreach (Node node in nodeFromQueue.Nodes)
                {
                    queue.Enqueue((MyNode)node);
                }
            }
        }

    }
}
