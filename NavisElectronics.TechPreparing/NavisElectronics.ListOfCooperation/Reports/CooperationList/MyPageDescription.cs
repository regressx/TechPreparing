using System.Collections.Generic;
using NavisElectronics.ListOfCooperation.Entities;

namespace NavisElectronics.ListOfCooperation.Reports
{
    public class MyPageDescription<T> where T:class
    {
        private ICollection<T> _elements;
        private IDictionary<long, ExtractedObject> _assemblies;

        public MyPageDescription()
        {
            _elements = new List<T>();
            _assemblies = new Dictionary<long, ExtractedObject>();
        }

        public int ElementsOnPage { get; set; }
        public int AssembliesOnPage { get; set; }
        public int PageNumber { get; set; }

        public ICollection<T> Elements
        {
            get { return _elements; }
        }


        public void RegisterAssembly(ExtractedObject element)
        {
            if (!_assemblies.ContainsKey(element.Id))
            {
                _assemblies.Add(element.Id, element);
            }
        }

        public ICollection<ExtractedObject> Assemblies
        {
            get { return _assemblies.Values; }
        }

        /// <summary>
        /// Признак того, что страница заполнена
        /// </summary>
        public bool IsFull { get; set; }
    }
}