using System;
using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.Interfaces
{
    /// <summary>
    /// Простейший элемент структуры дерева
    /// </summary>
    public interface IStructElement
    {
        long Id { get; set; }
        int Type { get; set; }
        string Name { get; set; }
        IList<IStructElement> Children { get; set; }
        void Add(IStructElement element);
    }
}