using SlotMachine.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachine
{
    public class SlotMachineLogic : ISlotMachineLogic
    {
        private IConfigReader ConfigReader { get; set; }
        private ICellValueLogic CellValues { get; set; }

        public SlotMachineLogic(IConfigReader configReader, ICellValueLogic cellValues)
        {
            ConfigReader = configReader ?? throw new ArgumentNullException(nameof(configReader));
            CellValues = cellValues ?? throw new ArgumentNullException(nameof(cellValues));
        }

        public IList<IWheelRow> GenerateWheelRows(int numberOfWheelRows = 4, int numberOfColoums = 3)
        {
            List<IWheelRow> wheelRows = new List<IWheelRow>();

            for (int i = 0; i < numberOfWheelRows; i++)
            {
                List<WheelCell> cells = new List<WheelCell>();

                for (int x = 0; x < numberOfColoums; x++)
                {
                    cells.Add(CellValues.GetRandomWeightedValue());
                }

                wheelRows.Add(new WheelRow(cells));
            }

            return wheelRows;
        }

        /// <summary>
        /// Calculate The winnings from the spin results provided
        /// </summary>
        /// <param name="winningRows"></param>
        /// <returns></returns>
        public decimal CalculateWinnings(IList<IWheelRow> rows, decimal stakeAmount)
        {
            decimal winnings = 0;

            if (!rows.Any(x => x.IsWinningRow()))
                return winnings;

            if (rows.Any(x => x.IsWinningRow()))
            {
                double totalWinCoefficient = rows.Where(x => x.IsWinningRow()).Sum(x => x.GetTotalRowCoefficient());

                //Calculate Winnings
                winnings = (decimal)totalWinCoefficient * stakeAmount;
            }

            return Math.Round(winnings, 2);
        }

        public decimal CalculateCurrentBalance(decimal balance, decimal winnings, decimal stakeAmount)
        {
            return balance += Math.Round((winnings - stakeAmount), 2);
        }
    }
}
