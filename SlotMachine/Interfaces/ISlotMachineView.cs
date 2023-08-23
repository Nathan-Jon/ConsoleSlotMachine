using SlotMachine.DataTypes;
using System.Collections.Generic;

namespace SlotMachine
{
    public interface ISlotMachineView
    {
        void GameOver();
        void ClearScene();
        void DisplayCurrentBalance(decimal currentBalance);
        decimal RequestStakeAmount(decimal accountBalance, decimal stakeAmount);
        void DisplayWinnings(decimal winnings);
        void DrawGrid(IList<IWheelRow> rows);
        void PressEnterToContinue();
        decimal RequestBalanceDeposit();
        decimal RequestStakeAmount(decimal availableFunds);
    }
}