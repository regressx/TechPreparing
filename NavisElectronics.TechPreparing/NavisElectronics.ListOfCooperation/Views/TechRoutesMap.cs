using NavisElectronics.ListOfCooperation.Reports;

namespace NavisElectronics.ListOfCooperation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using EventArguments;
    using Intermech.Interfaces.AutoSelection;
    using Intermech.Interfaces.Client;
    using ViewInterfaces;
    using ViewModels;

    public partial class TechRoutesMap : Form, ITechRouteMap
    {
        private readonly string _manufacturerViewSelectedAgentName;

        /// <summary>
        /// Событие при редактировании тех. маршрута
        /// </summary>
        public event EventHandler<EditTechRouteEventArgs> EditTechRouteClick;

        public event EventHandler<SaveClickEventArgs> SaveClick;
        public event EventHandler<SaveClickEventArgs> EditClick;
        public event EventHandler<ClipboardEventArgs> CopyClick;
        public event EventHandler<ClipboardEventArgs> PasteClick;
        public event EventHandler<SaveClickEventArgs> ShowClick;
        public event EventHandler CreateReportClick;
        public event EventHandler CreateDevideList;
        public event EventHandler SetNodesToComplectClick;
        public event EventHandler CreateCooperationList;
        public event EventHandler<ClipboardEventArgs> SetInnerCooperation;
        public event EventHandler<ClipboardEventArgs> RemoveInnerCooperation;
        public event EventHandler<SaveClickEventArgs> CreateCooperationListClick;

        public TechRoutesMap(string manufacturerViewSelectedAgentName, bool saveButtonEnable)
        {
            _manufacturerViewSelectedAgentName = manufacturerViewSelectedAgentName;
            InitializeComponent();
            if (!saveButtonEnable)
            {
                SaveButton.Enabled = false;
            }
        }

        public void SetTreeModel(TreeModel treeModel)
        {
            treeViewAdv1.Model = treeModel;

        }

        public ICollection<MyNode> GetSelectedRows()
        {
            ICollection<MyNode> answer = new List<MyNode>();
            ReadOnlyCollection<TreeNodeAdv> nodes = treeViewAdv1.SelectedNodes;
            foreach (TreeNodeAdv node in nodes)
            {
                MyNode currentNode = node.Tag as MyNode;
                answer.Add(currentNode);
            }
            return answer;
        }

        public MyNode GetMainNode()
        {
            MyNode node = treeViewAdv1.Root.Children[0].Tag as MyNode;
            return node;
        }

        private void treeViewAdv1_RowDraw(object sender, TreeViewRowDrawEventArgs e)
        {
            MyNode node = e.Node.Tag as MyNode;
            if (node.CooperationFlag)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            EventHandler<SaveClickEventArgs> temp = Volatile.Read(ref SaveClick);
            if (temp != null)
            {
                MyNode node = treeViewAdv1.Root.Children[0].Tag as MyNode;
                temp(sender, new SaveClickEventArgs(node));
            }
        }

        private void EditNote_Click(object sender, EventArgs e)
        {
            EventHandler<SaveClickEventArgs> temp = Volatile.Read(ref EditClick);
            if (temp != null)
            {
                MyNode node = treeViewAdv1.SelectedNode.Tag as MyNode;
                temp(sender, new SaveClickEventArgs(node));
            }
        }

        private void CopyRouteButton_Click(object sender, EventArgs e)
        {
            EventHandler<ClipboardEventArgs> temp = Volatile.Read(ref CopyClick);
            if (temp != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv1.SelectedNodes)
                {
                    nodes.Add(node.Tag as MyNode);
                }
                temp(sender, new ClipboardEventArgs(nodes));
            }
        }

        private void PasteRouteButton_Click(object sender, EventArgs e)
        {
            EventHandler<ClipboardEventArgs> temp = Volatile.Read(ref PasteClick);
            if (temp != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv1.SelectedNodes)
                {
                    nodes.Add(node.Tag as MyNode);
                }
                temp(sender, new ClipboardEventArgs(nodes));
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            EventHandler<SaveClickEventArgs> temp = Volatile.Read(ref ShowClick);
            if (temp != null)
            {
                MyNode node = treeViewAdv1.SelectedNode.Tag as MyNode;
                temp(sender, new SaveClickEventArgs(node));
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IAutoSelectionService autoSelectionService = ServicesManager.GetService(typeof(IAutoSelectionService)) as IAutoSelectionService;

            if (autoSelectionService == null)
            {
                return;
            }

            List<long> list = autoSelectionService.ExecuteSelection(-1283382, AutoSelectionMode.All);



            //if (list != null)
            //{
            //    if (list.Count != 0)
            //    {
            //        INotificationService notificationService = ServicesManager.GetService(typeof(INotificationService)) as INotificationService;
            //        if (notificationService != null)
            //        {
            //            notificationService.FireEvent(null, new DBRelationsEventArgs("RelationsCreated", list));
            //        }
            //    }
            //}
        }

        private void goToArchiveButton_Click(object sender, EventArgs e)
        {
            //MyNode selectedNode = treeViewAdv1.SelectedNodes[0].Tag as MyNode;
            //FileDesignation fd = _search.GetFileDesignation(selectedNode.Designation);
            //_search.StepToFolder(_search.GetFullPath(fd));
        }


        private void createReport_Click(object sender, EventArgs e)
        {
            if (CreateReportClick != null)
            {
                CreateReportClick(sender,e);
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
            treeViewAdv1.ExpandAll();
        }

        private void CollapseAllButton_Click(object sender, EventArgs e)
        {
            treeViewAdv1.CollapseAll();
        }

        private void SetInnerCooperationButton_Click(object sender, EventArgs e)
        {
            if (SetInnerCooperation != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv1.SelectedNodes)
                {
                    nodes.Add(node.Tag as MyNode);
                }
                SetInnerCooperation(sender, new ClipboardEventArgs(nodes));
            }
        }

        private void RemoveInnerCooperationButton_Click(object sender, EventArgs e)
        {
            if (RemoveInnerCooperation != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv1.SelectedNodes)
                {
                    nodes.Add(node.Tag as MyNode);
                }
                RemoveInnerCooperation(sender, new ClipboardEventArgs(nodes));
            }
        }

        private void createSingleCompleteListMenuItem_Click(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService();
            MyNode selectedNode = treeViewAdv1.SelectedNode.Tag as MyNode;
            reportService.CreateReport(selectedNode, selectedNode.Name, ReportType.CompleteList, DocumentType.Intermech, null);
        }

        private void createFullCompleteListMenuItem_Click(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService();
            MyNode selectedNode = treeViewAdv1.SelectedNode.Tag as MyNode;
            reportService.CreateReport(selectedNode, selectedNode.Name, ReportType.FullCompleteList, DocumentType.Excel, null);
        }

        private void SetNodesForComplectButton_Click(object sender, EventArgs e)
        {
            if (SetNodesToComplectClick != null)
            {
                SetNodesToComplectClick(sender, e);
            }
        }

        private void createCooperationListMenuItem_Click(object sender, EventArgs e)
        {
            if (CreateCooperationList != null)
            {
                CreateCooperationList(sender, e);
            }
        }

        private void createCompleteCardsForWholeOrder_Click(object sender, EventArgs e)
        {

        }

        private void addIntoExistingRouteButton_Click(object sender, EventArgs e)
        {
            EventHandler<EditTechRouteEventArgs> temp = Volatile.Read(ref EditTechRouteClick);
            if (temp != null)
            {
                temp(sender, new EditTechRouteEventArgs(true));
            }
        }

        private void createNewRouteButton_Click(object sender, EventArgs e)
        {
            EventHandler<EditTechRouteEventArgs> temp = Volatile.Read(ref EditTechRouteClick);
            if (temp != null)
            {
                temp(sender, new EditTechRouteEventArgs(false));
            }
        }
    }
}
