namespace NavisElectronics.TechPreparation.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using NavisElectronics.ListOfCooperation.Entities;
    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// Представление типов тех. отхода
    /// </summary>
    public partial class WithdrawalTypeView : Form
    {
        /// <summary>
        /// Корень тех. отхода
        /// </summary>
        private readonly WithdrawalType _root;

        /// <summary>
        /// Узел заказа
        /// </summary>
        private readonly IntermechTreeElement _intermechTreeElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="WithdrawalTypeView"/> class.
        /// </summary>
        /// <param name="root">
        /// The root.
        /// </param>
        /// <param name="intermechTreeElement">
        /// The intermech tree element.
        /// </param>
        /// <param name="collection">
        /// The collection.
        /// </param>
        public WithdrawalTypeView(WithdrawalType root, IntermechTreeElement intermechTreeElement, ICollection<ExtractedObject> collection)
        {
            _root = root;
            _intermechTreeElement = intermechTreeElement;
            InitializeComponent();
            TreeNode mainNode = new TreeNode(root.Description);
            BuildTreeRecursive(mainNode, root);
            treeView1.Nodes.Add(mainNode);
            dataGridView1.DataSource = collection.ToList();
        }


        /// <summary>
        /// The build tree recursive.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="typeNode">
        /// The type node.
        /// </param>
        private void BuildTreeRecursive(TreeNode node, WithdrawalType typeNode)
        {
            foreach (WithdrawalType type in typeNode.Types)
            {
                TreeNode childNode = new TreeNode(type.Description);
                node.Nodes.Add(childNode);
                if (type.Types.Count > 0)
                {
                    BuildTreeRecursive(childNode, type);
                }
            }
        }

        private void autoPickWithdrawalTypeButton_Click(object sender, EventArgs e)
        {

            // чистим у всех элементов атрибут "Тех. отход"
            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(_intermechTreeElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();
                elementFromQueue.TypeOfWithDrawal = string.Empty;

                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            // заполняем
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                ExtractedObject exctractedObject = row.DataBoundItem as ExtractedObject;
                exctractedObject.WithdrawalType = string.Empty;
                if (exctractedObject.Case != null)
                {
                    if (exctractedObject.Case.Contains("01005")
                        || exctractedObject.Case.Contains("0201")
                        || exctractedObject.Case.Contains("0402"))
                    {
                        exctractedObject.WithdrawalType = "1";
                    }

                    if (exctractedObject.Case.Contains("0603") || exctractedObject.Case.Contains("0805") ||
                        exctractedObject.Case.Contains("1206"))
                    {
                        exctractedObject.WithdrawalType = "2";
                    }
                }

                if (exctractedObject.Name.Contains("Контакт"))
                {
                    exctractedObject.WithdrawalType = "8";
                }

                if (exctractedObject.Id == 1296378
                    || exctractedObject.Id == 1375547
                    || exctractedObject.Id == 1273863
                    || exctractedObject.Id == 1313405
                    || exctractedObject.Id == 1375619)
                {
                    exctractedObject.WithdrawalType = "9";
                }

                if (exctractedObject.Id == 1314359 || exctractedObject.Id == 1343207)
                {
                    exctractedObject.WithdrawalType = "10";
                }

                if (exctractedObject.WithdrawalType != null)
                {
                    if (exctractedObject.WithdrawalType.Length > 0)
                    {
                        ICollection<IntermechTreeElement> elements = _intermechTreeElement.Find(exctractedObject.Id);
                        foreach (IntermechTreeElement element in elements)
                        {
                            element.TypeOfWithDrawal = exctractedObject.WithdrawalType;
                        }
                    }
                }
            }
        }

        private void pickCurrentWithdrawalTypeButton_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedCells[0].OwningRow;
            ExtractedObject taggedObject = row.DataBoundItem as ExtractedObject;
            WithdrawalTypeView view = new WithdrawalTypeView(_root, _intermechTreeElement, taggedObject.Elements);
            view.Show();
        }
    }
}
