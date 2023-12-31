﻿using System.Collections.Generic;

namespace SlotMachine.DataTypes
{
    public interface IWheelRow
    {
        IList<WheelCell> WheelCells { get; set; }
        string GetCellValuesAsText(string seperator);
        decimal GetTotalRowCoefficient();
        bool IsWinningRow();
    }
}