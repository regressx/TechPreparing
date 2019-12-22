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
    public partial class TechRoutesMap : Form, ITechRouteMap
    {
        private readonly string _manufacturerViewSelectedAgentName;

        /// <summary>
        /// Событие при редактировании тех. маршрута
        /// </summary>
        public event EventHandler<EditTechRouteEventArgs> EditTechRouteClick;
        public event EventHandler<EditTechRouteEventArgs> EditMassTechRouteClick;
        public event EventHandler<SaveClickEventArgs> EditNoteClick;
        public event EventHandler<ClipboardEventArgs> DeleteRouteClick;
        public event EventHandler<ClipboardEventArgs> CopyClick;
        public event EventHandler<ClipboardEventArgs> PasteClick;
        public event EventHandler<SaveClickEventArgs> ShowClick;
        public event EventHandler DownloadInfoFromIPS;
        public event EventHandler UpdateNodeFromIps;

        public event EventHandler<SaveClickEventArgs> GoToOldArchive;
        public event EventHandler CreateReportClick;
        public event EventHandler CreateDevideList;
        public event EventHandler CreateCooperationList;
        public event EventHandler<ClipboardEventArgs> SetInnerCooperation;
        public event EventHandler<ClipboardEventArgs> RemoveInnerCooperation;
        public event EventHandler RefreshTree;

        public TechRoutesMap()
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
            EventHandler<ClipboardEventArgs> temp = Volatile.Read(ref CopyClick);
            if (temp != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv.SelectedNodes)
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
                foreach (TreeNodeAdv node in treeViewAdv.SelectedNodes)
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
                MyNode node = treeViewAdv.SelectedNode.Tag as MyNode;
                temp(sender, new SaveClickEventArgs(node));
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //IAutoSelectionService autoSelectionService = ServicesManager.GetService(typeof(IAutoSelectionService)) as IAutoSelectionService;

            //if (autoSelectionService == null)
            //{
            //    return;
            //}

            //List<long> list = autoSelectionService.ExecuteSelection(-1283382, AutoSelectionMode.All);



            // if (list != null)
            // {
            // if (list.Count != 0)
            // {
            // INotificationService notificationService = ServicesManager.GetService(typeof(INotificationService)) as INotificationService;
            // if (notificationService != null)
            // {
            // notificationService.FireEvent(null, new DBRelationsEventArgs("RelationsCreated", list));
            // }
            // }
            // }
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

                SetInnerCooperation(sender, new ClipboardEventArgs(nodes));
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

                RemoveInnerCooperation(sender, new ClipboardEventArgs(nodes));
            }
        }

        private void createSingleCompleteListMenuItem_Click(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService();
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

        private void deleteRouteMenuItem_Click(object sender, EventArgs e)
        {
            EventHandler<ClipboardEventArgs> temp = Volatile.Read(ref DeleteRouteClick);
            if (temp != null)
            {
                ICollection<MyNode> nodes = new List<MyNode>();
                foreach (TreeNodeAdv node in treeViewAdv.SelectedNodes)
                {
                    nodes.Add(node.Tag as MyNode);
                }

                temp(sender, new ClipboardEventArgs(nodes));
            }
        }

        private void SetTechRoutesButtonButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref DownloadInfoFromIPS);
            if (temp != null)
            {
                temp(sender, EventArgs.Empty);
            }
        }

        private void editTechRoutesButton_Click(object sender, EventArgs e)
        {
            EventHandler<EditTechRouteEventArgs> temp = Volatile.Read(ref EditMassTechRouteClick);
            if (temp != null)
            {
                temp(sender, new EditTechRouteEventArgs(false));
            }
        }

        private void updateFromIPSButton_Click(object sender, EventArgs e)
        {
            if (UpdateNodeFromIps != null)
            {
                UpdateNodeFromIps(sender, e);
            }

        }
    }
}
