using SlotMachine.DataTypes;
using System.Collections.Generic;

namespace SlotMachine
{
    public interface ISlotMachineLogic
    {
        IList<IWheelRow> GenerateWheelRows(int numberOfWheelRows = 4, int numberOfColoums = 3);
        decimal CalculateWinnings(IList<IWheelRow> rows, decimal stakeAmount);
        decimal CalculateCurrentBalance(decimal balance, decimal winnings, decimal stakeAmount);
    }
}