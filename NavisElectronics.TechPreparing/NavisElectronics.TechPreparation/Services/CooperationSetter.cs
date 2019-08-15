using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    using NavisElectronics.TechPreparation.Entities;

    /// <summary>
    /// Класс, умеющий проставить требуемым узлам кооперацию
    /// </summary>
    public class CooperationSetter
    {
        /// <summary>
        /// The _xml document.
        /// </summary>
        private readonly XmlDocument _xmlDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperationSetter"/> class.
        /// </summary>
        public CooperationSetter()
        {
            _xmlDocument = new XmlDocument();
        }

        /// <summary>
        /// Проставляет для самого узла и всех входящих флаг, что делается по кооперации
        /// </summary>
        /// <param name="treeElement">
        /// The tree element.
        /// </param>
        public void SetCooperation(IntermechTreeElement treeElement)
        {
            ICollection<string> dependencies = new List<string>();
            try
            {
                _xmlDocument.Load("Designations.xml");

                XmlElement mainXmlElement = _xmlDocument.DocumentElement;

                foreach (XmlNode node in mainXmlElement.ChildNodes)
                {
                    dependencies.Add(node.Attributes["BeginWith"].Value);
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Не найден файл Designations.xml");
            }

            Queue<IntermechTreeElement> queue = new Queue<IntermechTreeElement>();
            queue.Enqueue(treeElement);
            while (queue.Count > 0)
            {
                IntermechTreeElement elementFromQueue = queue.Dequeue();

                if (elementFromQueue.Designation != null)
                {
                    string cuttedDesignation = CutTheDesignation(elementFromQueue.Designation);
                    foreach (string s in dependencies)
                    {
                        if (cuttedDesignation.StartsWith(s))
                        {
                            elementFromQueue.CooperationFlag = true;
                            break;
                        }
                    }
                }

                if (elementFromQueue.Children.Count > 0)
                {
                    foreach (IntermechTreeElement child in elementFromQueue.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        /// <summary>
        /// Отрезает последние цифры от точки у обозначения
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal string CutTheDesignation(string str)
        {
            int firstDotIndex = str.IndexOf('.');
            if (firstDotIndex > -1)
            {
                return str.Substring(firstDotIndex + 1);
            }
            return str;
        }

    }
}