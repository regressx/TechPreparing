using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using Aga.Controls.Tree;
using Intermech.Collections;
using Intermech.Interfaces.Client;
using NavisArchiveWork.Data;
using NavisArchiveWork.Model;
using NavisElectronics.ListOfCooperation.EventArguments;
using NavisElectronics.ListOfCooperation.ViewInterfaces;
using NavisElectronics.ListOfCooperation.ViewModels;

namespace NavisElectronics.ListOfCooperation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Entities;

    public partial class CooperationView : Form, ICooperationView
    {
        public CooperationView(bool saveButtonEnable)
        {
            InitializeComponent();
            if (!saveButtonEnable)
            {
                SaveButton.Enabled = false;
            }
        }

        public event EventHandler SaveClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> SetCooperationClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> DeleteCooperationClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> SetTechProcessReferenceClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> DeleteTechProcessReferenceClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> SetNoteClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> DeleteNoteClick;
        public event EventHandler CheckListOfCooperation;
        public event EventHandler<MultipleNodesSelectedEventArgs> SetParametersClick;
        public event EventHandler PutDownCooperation;
        public event EventHandler<MultipleNodesSelectedEventArgs> SearchInArchiveClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> FindInTreeClick;
        public event EventHandler GlobalSearchClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> CreateCooperationClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> SetTechTaskClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> SetPcbClick;
        public event EventHandler<MultipleNodesSelectedEventArgs> CreateCompleteListClick;
        public event EventHandler ExpandAllNodesClick;
        public event EventHandler CollapseAllNodesClick;

        public void FillTree(TreeModel model)
        {
            treeViewAdv1.Model = null;
            treeViewAdv1.Model = model;
        }

        public void ClearOutput()
        {
            listBox1.Items.Clear();
        }

        public void AppendError(ErrorElement element)
        {
            listBox1.Items.Add(element);
        }

        public CooperationNode GetMainNode()
        {
            CooperationNode node = treeViewAdv1.Root.Children[0].Tag as CooperationNode;
            return node;
        }

        public TreeViewAdv GetTreeView()
        {
            return treeViewAdv1;
        }

        public void JumpToNode(TreeNodeAdv cooperationNode)
        {
            treeViewAdv1.SelectedNode = cooperationNode;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref SaveClick);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void CallEvent(EventHandler<MultipleNodesSelectedEventArgs> myEvent, object sender)
        {
            EventHandler<MultipleNodesSelectedEventArgs> temp = Volatile.Read(ref myEvent);
            if (temp != null)
            {
                ICollection<CooperationNode> nodes = new List<CooperationNode>();
                foreach (TreeNodeAdv nodeAdv in treeViewAdv1.SelectedNodes)
                {
                    CooperationNode node = nodeAdv.Tag as CooperationNode;
                    nodes.Add(node);
                }
                temp(sender, new MultipleNodesSelectedEventArgs(nodes));
            }
        }

        private void CooperationButton_Click(object sender, EventArgs e)
        {
            CallEvent(SetCooperationClick, sender);
        }

        private void SetTechProcessButton_Click(object sender, EventArgs e)
        {
            CallEvent(SetTechProcessReferenceClick, sender);
        }

        private void DeleteCoopButton_Click(object sender, EventArgs e)
        {
            CallEvent(DeleteCooperationClick, sender);
        }

        private void ResetTechProcessButton_Click(object sender, EventArgs e)
        {
            CallEvent(DeleteTechProcessReferenceClick, sender);
        }

        private void SetNoteButton_Click(object sender, EventArgs e)
        {
            CallEvent(SetNoteClick, sender);
        }

        private void DeleteNoteButton_Click(object sender, EventArgs e)
        {
            CallEvent(DeleteNoteClick, sender);
        }

        private void CheckButtonClick_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref CheckListOfCooperation);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void SetParametersButton_Click(object sender, EventArgs e)
        {
            CallEvent(SetParametersClick, sender);
        }

        private void SetAllCooperationButton_Click(object sender, EventArgs e)
        {
            EventHandler temp = Volatile.Read(ref PutDownCooperation);
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        private void SearchInArchiveButton_Click(object sender, EventArgs e)
        {
            CallEvent(SearchInArchiveClick, sender);
        }

        private void TreeViewAdv1_RowDraw(object sender, TreeViewRowDrawEventArgs e)
        {
            CooperationNode node = e.Node.Tag as CooperationNode;
            if (node.CooperationFlag)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

            if (node.Error)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.IndianRed), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }
        }

        readonly Search _search = new Search(new Repository("ParentDirectoryNames.xml"));

        private void GoToTheArchiveButton_Click(object sender, EventArgs e)
        {
            CooperationNode selectedNode = treeViewAdv1.SelectedNodes[0].Tag as CooperationNode;
            FileDesignation fd = _search.GetFileDesignation(selectedNode.Designation);
            _search.StepToFolder(_search.GetFullPath(fd));
        }

        private void findInTreeButton_Click(object sender, EventArgs e)
        {
            if (FindInTreeClick != null)
            {
                CooperationNode selectedNode = treeViewAdv1.SelectedNodes[0].Tag as CooperationNode;
                FindInTreeClick(sender, new MultipleNodesSelectedEventArgs(new List<CooperationNode>() {selectedNode}));
            }
        }

        private void SearchInTreeButton_Click(object sender, EventArgs e)
        {
            if (GlobalSearchClick != null)
            {
                GlobalSearchClick(sender, e);
            }
        }

        private void ExpandAllButton_Click(object sender, EventArgs e)
        {
            treeViewAdv1.ExpandAll();
        }

        private void collapseAllButton_Click(object sender, EventArgs e)
        {
            treeViewAdv1.CollapseAll();
        }

        private void createCooperationListButton_Click(object sender, EventArgs e)
        {
            if (CreateCooperationClick != null)
            {
                CooperationNode selectedNode = treeViewAdv1.SelectedNodes[0].Tag as CooperationNode;
                CreateCooperationClick(sender, new MultipleNodesSelectedEventArgs(new List<CooperationNode>() { selectedNode }));
            }
        }

        private void SetTechTaskMenuItem_Click(object sender, EventArgs e)
        {
            if (SetTechTaskClick != null)
            {
                CooperationNode selectedNode = treeViewAdv1.SelectedNodes[0].Tag as CooperationNode;
                SetTechTaskClick(sender, new MultipleNodesSelectedEventArgs(new List<CooperationNode>() { selectedNode }));
            }
        }

        private void SetPcbMenuItem_Click(object sender, EventArgs e)
        {
            if (SetPcbClick != null)
            {
                CooperationNode selectedNode = treeViewAdv1.SelectedNodes[0].Tag as CooperationNode;
                SetPcbClick(sender, new MultipleNodesSelectedEventArgs(new List<CooperationNode>() { selectedNode }));
            }
        }
    }
}
