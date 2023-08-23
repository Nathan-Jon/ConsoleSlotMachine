using System.Collections.Generic;

namespace SlotMachine.DataTypes
{
    public interface ICellValueLogic
    {
        WheelCell GetRandomWeightedValue();
    }
}