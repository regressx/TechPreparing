using System.Collections.Generic;
using NavisElectronics.TechPreparation.Interfaces;
using NavisElectronics.TechPreparation.Interfaces.Entities;

namespace NavisElectronics.TechPreparation.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class IntermechTreeElementWrapper:IStructElement
    {
        private IntermechTreeElement _root;

        public IntermechTreeElementWrapper(IntermechTreeElement root)
        {
            _root = root;
        }

        public long Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }

        public IList<IStructElement> Children
        {
            get; set;
        }

        public void Add(IStructElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}