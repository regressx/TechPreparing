using System.Collections.Generic;
using NavisElectronics.TechPreparation.Interfaces;

namespace NavisElectronics.TechPreparation.Entities
{
    public class WithdrawalTypeWrapper:IStructElement
    {
        public long Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public IList<IStructElement> Children { get; set; }
        public void Add(IStructElement element)
        {
            throw new System.NotImplementedException();
        }
    }
}