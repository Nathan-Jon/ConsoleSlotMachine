using System;
using System.Collections.Generic;

namespace SlotMachine.DataTypes
{
    public class CellValueLogic : ICellValueLogic
    {
        private List<WheelCell> WeightedCellValues { get; set; }
        Random RandomNumberGenerator { get; set; }

        public CellValueLogic(Random randomNumberGenerator)
        {
            WeightedCellValues = new List<WheelCell>()
            {
                new WheelCell(CellValueEnum.Apple, (decimal)0.4, 45),
                new WheelCell(CellValueEnum.Banana, (decimal)0.6, 35),
                new WheelCell(CellValueEnum.Pineapple, (decimal)0.8, 15),
                new WheelCell(CellValueEnum.Wildcard, 0, 5),
            };

            RandomNumberGenerator = randomNumberGenerator;
        }

        public WheelCell GetRandomWeightedValue()
        {
            int totalWeight = 0;
            foreach (WheelCell cell in WeightedCellValues)
            {
                totalWeight += cell.Weighting;
            }
            int randomValue = RandomNumberGenerator.Next(1, totalWeight + 1);


            var processedWeight = 0;
            foreach (WheelCell cell in WeightedCellValues)
            {
                processedWeight += cell.Weighting;
                if(randomValue <= processedWeight)
                {
                    return cell;
                }
            }

            return null;
        }
    }
}
