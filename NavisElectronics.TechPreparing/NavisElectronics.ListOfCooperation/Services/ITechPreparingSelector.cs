using System.Collections.Generic;

namespace NavisElectronics.ListOfCooperation.Services
{
    public interface ITechPreparingSelector<T>
    {
        IList<T> Select();
    }
}