using System;

namespace NavisElectronics.TechPreparation.Interfaces.Enums
{
    /// <summary>
    /// Перечисление состояний узла дерева
    /// </summary>
    [Flags]
    public enum NodeStates
    {
        /// <summary>
        /// Есть изменения
        /// </summary>
        Modified = 1,

        /// <summary>
        /// Узел добавлен
        /// </summary>
        Added = 2,

        /// <summary>
        /// Узел удален
        /// </summary>
        Deleted = 4,

        /// <summary>
        /// Ничего не изменилось
        /// </summary>
        Default = 8
    }
}