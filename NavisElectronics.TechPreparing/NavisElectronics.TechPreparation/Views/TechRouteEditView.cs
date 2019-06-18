﻿namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;

    using Aga.Controls.Tree;

    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.EventArguments;
    using NavisElectronics.TechPreparation.ViewInterfaces;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    public partial class TechRouteEditView : Form, ITechRouteView
    {
        private readonly string _data;

        public TechRouteEditView(string data, TechRouteNode node)
        {
            _data = data;
            InitializeComponent();
        }

        public event EventHandler<RouteNodeClickEventAgrs> RouteNodeClick;
        public event EventHandler<RemoveNodeEventArgs> RemoveNodeClick;


        public void FillWorkShop(TreeModel model)
        {
            treeViewAdv1.Model = null;
            treeViewAdv1.Model = model;
            treeViewAdv1.ExpandAll();
        }

        public void FillListBox(IList<TechRouteNode> nodes)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < nodes.Count; i++)
            {
                listBox1.Items.Add(string.Format("{0}-", nodes[i].GetCaption()));
            }
        }

        public void FillTextBox(IList<TechRouteNode> nodes)
        {
            textBox1.Text = string.Empty;
            for (int i = 0; i < nodes.Count; i++)
            {
                textBox1.AppendText(string.Format("{0}-", nodes[i].GetCaption()));
            }

            textBox1.Text = textBox1.Text.TrimEnd('-');
        }

        private void TechRouteView_Load(object sender, EventArgs e)
        {
            this.Text = _data;
        }

        private void buttonRemoveNode_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                EventHandler<RemoveNodeEventArgs> temp = Volatile.Read(ref RemoveNodeClick);
                if (temp != null)
                {
                    temp(sender, new RemoveNodeEventArgs(listBox1.SelectedIndex));
                }
            }
        }

        private void treeViewAdv1_DoubleClick(object sender, EventArgs e)
        {
            TreeNodeAdv selectedNode = treeViewAdv1.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Children.Count == 0)
                {
                    EventHandler<RouteNodeClickEventAgrs> temp = Volatile.Read(ref RouteNodeClick);
                    if (temp != null)
                    {
                        TechRouteNodeView node = selectedNode.Tag as TechRouteNodeView;
                        temp(sender, new RouteNodeClickEventAgrs(node.Tag as TechRouteNode));
                    }
                }
                else
                {
                    MessageBox.Show("Не стоит пытаться добавить этот узел в маршрут. Выберите тот, у которого нет дочерних узлов");
                }
            }
        }

        private void loadFactoryStructButton_Click(object sender, EventArgs e)
        {

        }
    }
}