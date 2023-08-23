using System;
using System.Runtime.InteropServices;

namespace SlotMachine
{
    /// <summary>
    /// Wheel Cell is used to store the possible slot results
    /// </summary>
    public class WheelCell
    {
        /// <summary>
        /// Enum Value of the cell
        /// </summary>
        public CellValueEnum Value { get; private set; }

        /// <summary>
        /// Probablity weighting of the cell
        /// </summary>
        public int Weighting { get; private set; }
        /// <summary>
        /// Coefficient used for calculating winnings
        /// </summary>
        public decimal Coefficient { get; private set; }

        public WheelCell()
        { }

        public WheelCell(CellValueEnum cellValue, decimal coefficient, int weighting)
        {
            Value = cellValue;
            Coefficient = coefficient;
            Weighting = weighting;
        }
    }
}
