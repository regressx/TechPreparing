using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.Interfaces
{
    public interface ITreeNode
    {
        int TypeId { get; set; }
        string NumberInOrder { get; set; }
        int Level { get; set; }
        int NumberOnLevel { get; set; }
        ICollection<ITreeNode> Nodes { get; }
        ITreeNode Parent { get; set; }
        int Index { get; }
        bool DoNotProduce { get; set; }
    }
}