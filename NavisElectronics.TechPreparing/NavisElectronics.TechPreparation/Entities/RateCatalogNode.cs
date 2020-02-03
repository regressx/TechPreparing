﻿using NavisElectronics.TechPreparation.Interfaces.Collections;
using System.Collections;
using System.Collections.Generic;

namespace NavisElectronics.TechPreparation.Interfaces.Entities
{
    /// <summary>
    ///  Класс представляет собой узел каталога нормы расхода
    /// </summary>
    public abstract class RateCatalogNode : System.IComparable<RateCatalogNode>, System.IComparable
    {
        private SortedList<string, RateCatalogNode> _nodes;
        
        /// <summary>
        ///Конструктор по умолчанию 
        /// </summary>
        public RateCatalogNode()
        {
            _nodes = new SortedList<string, RateCatalogNode>();
        }

        public string Name { get; set; }

        public RateCatalogNode Parent { get; set; }

        public RateCatalogNode Find(string key)
        {
            if (_nodes.ContainsKey(key))
            {
                return _nodes[key];
            }
            throw new KeyNotFoundException(string.Format($"В каталоге {Name} нет указанного ключа {key}"));
        }

        public void Add(RateCatalogNode nodeToAdd)
        {
            _nodes.Add(nodeToAdd.Name,nodeToAdd);
        }

        public int CompareTo(RateCatalogNode other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public int CompareTo(object obj)
        {
            return CompareTo((RateCatalogNode)obj);
        }
    }
}