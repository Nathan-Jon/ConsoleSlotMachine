using System.Collections.Generic;

namespace SlotMachine.DataTypes
{
    public interface ICellValues
    {
        WheelCell GetRandomWeightedValue();
    }
}