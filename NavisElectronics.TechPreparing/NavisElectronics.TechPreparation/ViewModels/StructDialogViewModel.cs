using System;
using System.Collections.Generic;
using System.Reflection;
using Aga.Controls.Tree;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Presenters;
using NavisElectronics.TechPreparation.ViewModels.TreeNodes;

namespace NavisElectronics.TechPreparation.ViewModels
{
    /// <summary>
    /// The struct dialog view model.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class StructDialogViewModel<T,V> where T : Node, new() where V : IStructElement
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
        public TreeModel BuildTree(V element, TreeViewSettings settings)
        {
            TreeModel model = new TreeModel();
            T mainNode = new T();
            Type type = typeof(T);

            foreach (string dataProperty in settings.DataProperties)
            {
                PropertyInfo propertyInfo = type.GetProperty(dataProperty);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(mainNode,element.Name);
                }

            }

            mainNode.Tag = element;
            BuildNodeRecursive(mainNode, element, settings);
            model.Nodes.Add(mainNode);
            return model;
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
        private void BuildNodeRecursive(T mainNode, V element, TreeViewSettings settings)
        {
            foreach (var structElement in element.Children)
            {
                var child = (V)structElement;
                T childNode = new T();

                Type type = typeof(T);

                foreach (string dataProperty in settings.DataProperties)
                {
                    PropertyInfo propertyInfo = type.GetProperty(dataProperty);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(mainNode, "xyz");
                    }
                }
                childNode.Tag = child;
                mainNode.Nodes.Add(childNode);
                BuildNodeRecursive(childNode, child, settings);
            }
        }
    }
}