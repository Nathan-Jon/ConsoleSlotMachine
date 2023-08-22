using SlotMachine.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachine
{
    public class GridLogic : IGridLogic
    {
        private IConfigReader ConfigReader { get; set; }
        private ICellValues CellValues { get; set; }

        public GridLogic(IConfigReader configReader, ICellValues cellValues)
        {
            ConfigReader = configReader ?? throw new NullReferenceException($"{nameof(ConfigReader)} not found when creating {nameof(this.GetType)}");
            CellValues = cellValues ?? throw new NullReferenceException($"{nameof(CellValues)} not found when creating {nameof(this.GetType)}");
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

        public void DrawGrid(IList<IWheelRow> rows)
        {
            if (rows.Any())
            {
                Console.WriteLine("_________________________");

                foreach (IWheelRow row in rows)
                {
                    if (row.IsWinningRow() && ConfigReader.GetBoolConfigValue(ConfigConstants.HighlightWinningRow))
                        Console.BackgroundColor = ConsoleColor.Yellow;

                    Console.WriteLine(row.GetCellValuesAsText(ConfigReader.GetStringConfigValue(ConfigConstants.ColumnSeperatorValue)));

                    Console.ResetColor();
                }

                Console.WriteLine("_________________________");
            }
        }
    }
}
