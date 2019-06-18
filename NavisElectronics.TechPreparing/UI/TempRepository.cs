using System.Collections.Generic;
using System.IO;
using NavisArchiveWork.Data;

namespace UI
{
    public class TempRepository : IRepository
    {
        IList<PathContainer> _lines = new List<PathContainer>();
        private string _netPath;
        public TempRepository()
        {
            using (StreamReader sr = new StreamReader("lines.txt"))
            {
                _netPath = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] lineMembers = sr.ReadLine().Split(';');
                    PathContainer pathContainer = new PathContainer(lineMembers[0], lineMembers[1]);
                    _lines.Add(pathContainer);
                }
            }
        }

        public string GetManufacturePath(string value)
        {
            // обычный линейный поиск. Быстро и просто, потому что мало элементов
            foreach (PathContainer line in _lines)
            {
                if (line.Name.Equals(value))
                {
                    return line.PseudoName;
                }
            }

            return string.Empty;
        }

        public string GetNetPath()
        {
            return _netPath;
        }


        /// <summary>
        /// Вложенный класс для хранения некоторой структуры децимального номера
        /// </summary>
        class PathContainer
        {
            private string _name;
            private string _pseudoName;

            public PathContainer(string name, string pseudoName)
            {
                _name = name;
                _pseudoName = pseudoName;
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public string PseudoName
            {
                get { return _pseudoName; }
                set { _pseudoName = value; }
            }
        }

    }
}