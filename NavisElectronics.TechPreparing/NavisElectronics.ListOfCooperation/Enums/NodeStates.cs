using System;

namespace NavisElectronics.ListOfCooperation.Enums
{
    [Flags]
    public enum NodeStates
    {
        Modified = 1,
        Added = 2,
        Deleted = 4,
        Default = 8
    }
}