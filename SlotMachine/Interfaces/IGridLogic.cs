using SlotMachine.DataTypes;
using System.Collections.Generic;

namespace SlotMachine
{
    public interface IGridLogic
    {
        IList<IWheelRow> GenerateWheelRows(int numberOfWheelRows = 4, int numberOfColoums = 3);

        void DrawGrid(IList<IWheelRow> rows);
    }
}