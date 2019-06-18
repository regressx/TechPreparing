// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITechPreparingSelector.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ITechPreparingSelector type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



namespace NavisElectronics.TechPreparation.Services
{
    using System.Collections.Generic;

    public interface ITechPreparingSelector<T>
    {
        IList<T> Select();
    }
}