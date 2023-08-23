using SlotMachine.DataTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace SlotMachine
{
    public class SlotMachineController : ISlotMachineController
    {
        public IConfigReader ConfigReader { get; private set; }
        public ISlotMachineLogic GridLogic { get; private set; }
        public ISlotMachineView SlotMachineView { get; private set; }

        public SlotMachineController(IConfigReader configReader, ISlotMachineLogic gridLogic, ISlotMachineView slotMachineView)
        {
            ConfigReader = configReader ?? throw new ArgumentNullException(nameof(configReader));
            GridLogic = gridLogic ?? throw new ArgumentNullException(nameof(gridLogic));
            SlotMachineView = slotMachineView ?? throw new ArgumentNullException(nameof(slotMachineView));
        }


        public void StartGame()
        {
            decimal totalAmount = SlotMachineView.RequestBalanceDeposit();
            CoreGameLoop(totalAmount);
            SlotMachineView.GameOver();
        }


        private void CoreGameLoop(decimal accountBalance)
        {
            decimal stakeAmount = 0;
            bool firstSpin = true;
            bool autoSpin = ConfigReader.GetBoolConfigValue(ConfigConstants.AutoSpinWithSameStake);
            while (accountBalance > 0)
            {
                // If this is the first spin, or auto spin is disabled, Request the stake amount for the round
                if(firstSpin || !firstSpin && !autoSpin)
                {
                    stakeAmount = SlotMachineView.RequestStakeAmount(accountBalance);
                } 
                else if(autoSpin)
                {
                    stakeAmount = SlotMachineView.RequestStakeAmount(accountBalance, stakeAmount);
                }
                

                //Generate and draw the Slot machine Grid
                IList<IWheelRow> rows = GridLogic.GenerateWheelRows(ConfigReader.GetIntConfigValue(ConfigConstants.NumberOfWheelRows));
                SlotMachineView.DrawGrid(rows);


                decimal winnings = GridLogic.CalculateWinnings(rows, stakeAmount);
                accountBalance = GridLogic.CalculateCurrentBalance(accountBalance, winnings, stakeAmount);

                // Calculate and apply the winnings based on the slot results
                SlotMachineView.DisplayWinnings(winnings);
                SlotMachineView.DisplayCurrentBalance(accountBalance);

                if (accountBalance <= 0)
                    break;

                firstSpin = false;

                SlotMachineView.PressEnterToContinue();
                SlotMachineView.ClearScene();
            }
        }
    }
}
