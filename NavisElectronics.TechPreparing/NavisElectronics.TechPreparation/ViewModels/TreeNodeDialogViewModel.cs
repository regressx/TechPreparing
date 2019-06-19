namespace NavisElectronics.TechPreparation.ViewModels
{
    using NavisElectronics.TechPreparation.Entities;
    using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

    /// <summary>
    /// Модель-фасад для обслуживания окна выбора тех. подготовки из других заказов
    /// </summary>
    public class TreeNodeDialogViewModel
    {
        /// <summary>
        /// Метод строит дерево из тех. подготовки выбранного заказа
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="MyNode"/>.
        /// </returns>
        public MyNode BuildTree(IntermechTreeElement element)
        {
            MyNode mainNode = new MyNode(element.Id.ToString());
            mainNode.Designation = element.Designation;
            mainNode.Name = element.Name;
            mainNode.Tag = element;
            BuildNodeRecursive(mainNode, element);
            return mainNode;
        }

        /// <summary>
        /// Получение составного узла рекурсивно
        /// </summary>
        /// <param name="mainNode">
        /// Главный узел
        /// </param>
        /// <param name="element">
        /// Элемент, из которого получаем данные
        /// </param>
        private void BuildNodeRecursive(MyNode mainNode, IntermechTreeElement element)
        {
            if (element.Children.Count > 0)
            {
                foreach (IntermechTreeElement child in element.Children)
                {
                    MyNode childNode = new MyNode(child.Id.ToString());
                    childNode.Id = child.Id;
                    childNode.Designation = child.Designation;
                    childNode.Name = child.Name;
                    childNode.Tag = child;
                    mainNode.Nodes.Add(childNode);
                    BuildNodeRecursive(childNode, child);
                }
            }
        }



    }
}