using NavisElectronics.Orders.Enums;

namespace NavisElectronics.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Aga.Controls.Tree;
    using EventArguments;
    using TechPreparation.Interfaces.Entities;
    using System.Linq;

    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
            treeViewAdv.RowDraw += TreeViewAdv_RowDraw;
            
        }


        #region Events

        public event EventHandler DownloadAndUpdate;
        public event EventHandler Save;
        public event EventHandler StartChecking;
        public event EventHandler AbortLoading;
        public event EventHandler<ReportStyle> CreateReport;
        public event EventHandler DecryptDocumentNames;
        public event EventHandler<ProduceEventArgs> SetProduceClick;
        public event EventHandler AddNoteToThisNode;
        public event EventHandler AddNoteToThisNodeInWholeTree;


        #endregion

        #region IMainViewRealization

        public void UpdateTreeModel(IntermechTreeElement root)
        {
            TreeModel treeModel = GetTreeModel(root);
            treeViewAdv.Model = null;
            treeViewAdv.Model = treeModel;
        }

        public void UpdateSaveLabel(string message)
        {
            saveInfoLabel.Text = message;
        }

        public OrderNode GetSelectedTreeElement()
        {
            if (treeViewAdv.SelectedNode != null)
            {
                return ((OrderNode)treeViewAdv.SelectedNode.Tag);
            }
            throw new NullReferenceException("Не был выбран никакой узел дерева");
        }

        #endregion


        // TODO Эти два метода надо бы перенести в MainViewModel
        public TreeModel GetTreeModel(IntermechTreeElement elementToView)
        {
            OrderNode root = new OrderNode();
            root.VersionId = elementToView.Id;
            root.ObjectId = elementToView.ObjectId;
            root.Type = elementToView.Type;
            root.Amount = elementToView.Amount;
            root.AmountWithUse = elementToView.AmountWithUse;
            root.Name = elementToView.Name;
            root.Designation = elementToView.Designation;
            root.PcbVersion = elementToView.PcbVersion;
            root.IsPcb = elementToView.IsPcb;
            root.Tag = elementToView;
            GetOrderNodeRecursive(root, elementToView);
            TreeModel model = new TreeModel();
            model.Nodes.Add(root);
            return model;
        }

        private void GetOrderNodeRecursive(OrderNode root, IntermechTreeElement elementToView)
        {
            foreach (IntermechTreeElement child in elementToView.Children)
            {
                OrderNode node = new OrderNode();
                node.VersionId = child.Id;
                node.ObjectId = child.ObjectId;
                node.DoNotProduce = child.ProduseSign;
                node.Type = child.Type;
                node.Designation = child.Designation;
                node.Name = child.Name;
                node.FirstUse = child.FirstUse;
                node.Status = child.LifeCycleStep;
                node.Amount = child.Amount;
                node.AmountWithUse = child.AmountWithUse;
                node.Letter = child.Letter;
                node.ChangeNumber = child.ChangeNumber;
                node.ChangeDocument = child.ChangeDocument;
                node.Note = child.RelationNote;
                node.RelationType = child.RelationName;
                node.Tag = child;
                node.PcbVersion = child.PcbVersion;
                node.IsPcb = child.IsPcb;
                if (node.IsPcb)
                {
                    if (string.IsNullOrEmpty(node.Note))
                    {
                        node.Note = "V" + node.PcbVersion.ToString();
                    }
                    else
                    {
                        node.Note = node.Note + "V" + node.PcbVersion.ToString();
                    }
                }

                if (!string.IsNullOrEmpty(child.SubstituteInfo))
                {
                    if (string.IsNullOrEmpty(node.Note))
                    {
                        node.Note = child.SubstituteInfo;
                    }
                    else
                    {
                        node.Note = node.Note + ", " + child.SubstituteInfo;
                    }
                }



                root.Nodes.Add(node);
                GetOrderNodeRecursive(node, child);
            }
        }




        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AbortLoading != null)
            {
                AbortLoading(sender, e);
            }
        }


        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {
            if (StartChecking != null)
            {
                StartChecking(sender, e);
            }
        }

        private void TreeViewAdv_RowDraw(object sender, TreeViewRowDrawEventArgs e)
        {
            IntermechTreeElement node = ((IntermechTreeElement)((OrderNode)e.Node.Tag).Tag);
            if (node.CooperationFlag)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
            }

            if (node.ProduseSign)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkKhaki), 0, e.RowRect.Top, ((Control)sender).Width, e.RowRect.Height);
                ((OrderNode)e.Node.Tag).Note = node.RelationNote;
            }
        }

        private void SaveOrderButton_Click(object sender, EventArgs e)
        {
            if (Save != null)
            {
                Save(this, EventArgs.Empty);
            }
        }

        private void LoadAndUpdateButtonStrip_Click(object sender, EventArgs e)
        {
            if (DownloadAndUpdate != null)
            {
                DownloadAndUpdate(sender, e);
            }
        }

        private void produceInCurrentNodeButton_Click(object sender, EventArgs e)
        {
            if (SetProduceClick != null)
            {
                IntermechTreeElement selectedElement =
                    (IntermechTreeElement)((OrderNode)treeViewAdv.SelectedNode.Tag).Tag;

                SetProduceClick(sender, new ProduceEventArgs(selectedElement, false, ProduceIn.OnlyThisNode));
                treeViewAdv.Invalidate();
            }
        }

        private void produceInAllTreeButton_Click(object sender, EventArgs e)
        {
            if (SetProduceClick != null)
            {
                IntermechTreeElement selectedElement =
                    (IntermechTreeElement)((OrderNode)treeViewAdv.SelectedNode.Tag).Tag;

                SetProduceClick(sender, new ProduceEventArgs(selectedElement, false, ProduceIn.AllTree));
                treeViewAdv.Invalidate();
            }
        }

        private void notProdInCurrentNodeButton_Click(object sender, EventArgs e)
        {
            if (SetProduceClick != null)
            {
                IntermechTreeElement selectedElement =
                    (IntermechTreeElement)((OrderNode)treeViewAdv.SelectedNode.Tag).Tag;

                SetProduceClick(sender, new ProduceEventArgs(selectedElement, true, ProduceIn.AllTree));
                treeViewAdv.Invalidate();
            }
        }

        private void notProdInTreeButton_Click(object sender, EventArgs e)
        {
            if (SetProduceClick != null)
            {
                IntermechTreeElement selectedElement =
                    (IntermechTreeElement)((OrderNode)treeViewAdv.SelectedNode.Tag).Tag;

                SetProduceClick(sender, new ProduceEventArgs(selectedElement, true, ProduceIn.AllTree));
                treeViewAdv.Invalidate();
            }
        }

        private void CreateReportClickExcel_Click(object sender, EventArgs e)
        {
            if (CreateReport != null)
            {
                CreateReport(this, ReportStyle.Excel);
            }
        }

        private void CreateReportClickIPS_Click(object sender, EventArgs e)
        {
            if (CreateReport != null)
            {
                CreateReport(this, ReportStyle.IPS);
            }
        }

        private void DecryptDocumentsButton_Click(object sender, EventArgs e)
        {
            if (DecryptDocumentNames != null)
            {
                DecryptDocumentNames(sender,EventArgs.Empty);
            }
        }

        public OrderNode GetRootOrderNode()
        {
            OrderNode rootNode = treeViewAdv.AllNodes.First().Tag as OrderNode;
            return rootNode;
        }

        private void OnAddNoteOnlyThisNodeButtonClick(object sender, EventArgs e)
        {
            if (AddNoteToThisNode!=null)
            {
                AddNoteToThisNode(sender, e);
            }
        }

        private void OnAddNoteInWholeTreeButtonClick(object sender, EventArgs e)
        {
            if (AddNoteToThisNodeInWholeTree != null)
            {
                AddNoteToThisNodeInWholeTree(sender, e);
            }
        }
    }
}
