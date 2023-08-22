using System;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachine.DataTypes
{
    public class WheelRow : IWheelRow
    {
        /// <summary>
        /// List of Cells this row holds
        /// </summary>
        public IList<WheelCell> WheelCells { get; set; } = new List<WheelCell>();

        public WheelRow()
        {
        }

        /// <summary>
        /// Constructor for wheel rows, generates all the cells found in it and applies the new values
        /// </summary>
        /// <param name="random"></param>
        /// <param name="numberOfColumns"></param>
        public WheelRow(IList<WheelCell> cells)
        {
            WheelCells = cells;
        }

        /// <summary>
        /// Get the row text for all of the Cells
        /// </summary>
        /// <returns></returns>
        public string GetCellValuesAsText(string seperator)
        {
            return string.Join(seperator, WheelCells.Select(x => ((char)x.Value)));
        }

        /// <summary>
        /// Calculate total coefficient value from all cells in row
        /// </summary>
        /// <returns></returns>
        public double GetTotalRowCoefficient()
        {
            if (WheelCells == null || !WheelCells.Any())
                return 0;

            return WheelCells.Sum(x => x.Coefficient);
        }

        /// <summary>
        /// Return if row has all matching values
        /// </summary>
        /// <returns></returns>
        public bool IsWinningRow()
        {
            if (WheelCells == null || !WheelCells.Any())
                return false;

            return !WheelCells.Any(x => x.Value != WheelCells[0].Value && x.Value != CellValueEnum.Wildcard);

        }
    }
}
