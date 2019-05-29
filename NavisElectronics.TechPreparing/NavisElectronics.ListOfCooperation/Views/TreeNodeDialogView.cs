using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Intermech.Expressions.Exceptions;
using NavisElectronics.ListOfCooperation.Entities;
using NavisElectronics.ListOfCooperation.ViewInterfaces;
using NavisElectronics.ListOfCooperation.ViewModels;

namespace NavisElectronics.ListOfCooperation.Views
{
    /// <summary>
    /// Представление для загрузки тех. подготовки из заказов, выполненных ранее
    /// </summary>
    public partial class TreeNodeDialogView : Form, ITreeNodeDialogView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNodeDialogView"/> class.
        /// </summary>
        public TreeNodeDialogView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка кнопки Принять
        /// </summary>
        public event EventHandler<IntermechTreeElement> AcceptClick;

        /// <summary>
        /// Заполняем дерево данными
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        public void FillTree(TreeModel model)
        {
            treeViewAdv1.Model = null;
            treeViewAdv1.Model = model;
        }

        /// <summary>
        /// Получение выбранного узла
        /// </summary>
        /// <returns>
        /// The <see cref="IntermechTreeElement"/>.
        /// </returns>
        public IntermechTreeElement GetSelectedNode()
        {
            TreeNodeAdv selectedNode = treeViewAdv1.SelectedNode;
            MyNode mySelectedNode = selectedNode.Tag as MyNode;
            return mySelectedNode.Tag as IntermechTreeElement;
        }

        /// <summary>
        /// Обработчик кнопки "Принять"
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void AcceptButton_Click(object sender, System.EventArgs e)
        {
            if (AcceptClick != null)
            {
                AcceptClick(sender, GetSelectedNode());
            }
        }
    }
}
