using SlotMachine.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachine
{
    public class SlotMachineView : ISlotMachineView
    {

        private IInputValidator InputValidator { get; set; }
        private IConfigReader ConfigReader { get; set; }

        public SlotMachineView(IInputValidator inputValidator, IConfigReader configReader)
        {
            InputValidator = inputValidator ?? throw new NullReferenceException($"{nameof(InputValidator)} not found when creating {nameof(SlotMachineView)}");
            ConfigReader = configReader ?? throw new NullReferenceException($"{nameof(ConfigReader)} not found when creating {nameof(SlotMachineView)}");
        }

        public decimal RequestBalanceDeposit()
        {
            Console.WriteLine("Please deposit money you would like to play with:");

            decimal totalAmount = 0;

            do
            {
                string number = Console.ReadLine();

                if (InputValidator.ValidateTextIsDecimal(number))
                {
                    totalAmount = Decimal.Round(Convert.ToDecimal(number), 2);
                }
                else
                {
                    Console.WriteLine("Please input a number");
                }

            } while (totalAmount == 0);

            Console.WriteLine($"Total Funds : {totalAmount}");

            return totalAmount;
        }

        public decimal RequestStakeAmount(decimal accountBalance)
        {
            int minimumStake = ConfigReader.GetIntConfigValue(ConfigConstants.MinimumStakeValue);

            Console.WriteLine("Enter Stake Amount:");

            if (minimumStake != 0)
                Console.WriteLine($"The minimum stake is : {minimumStake}");

            decimal stakeAmount = 0;

            do
            {
                string text = Console.ReadLine();

                bool isDecimal = InputValidator.ValidateTextIsDecimal(text);
                if (isDecimal)
                {
                    decimal amount = Convert.ToDecimal(text);

                    //*NOTE* Add validation to decimal point here

                    if (InputValidator.ValidateIsZeroOrBelow(amount))
                    {
                        Console.WriteLine($"Stake amount cannot be lower than, or equal to 0 please use a higher stake");
                    }
                    if (!InputValidator.ValidateValueIsHigher(accountBalance, amount))
                    {
                        Console.WriteLine($"Stake amount cannot be higher then your total funds, please use a lower stake");
                        continue;
                    }
                    if (!InputValidator.ValidateValueIsHigher(amount, ConfigReader.GetIntConfigValue(ConfigConstants.MinimumStakeValue)))
                    {
                        Console.WriteLine($"Stake amount cannot be lower than the minimum stake, please use a higher stake");
                        continue;
                    }

                    stakeAmount = Decimal.Round(Convert.ToDecimal(amount), 2);

                }
                else
                {
                    Console.WriteLine("Please input a number");
                }

            } while (stakeAmount == 0);

            return stakeAmount;
        }

        public decimal RequestStakeAmount(decimal accountBalance, decimal stakeAmount)
        {
            if (stakeAmount <= accountBalance)
                return RequestStakeAmount(accountBalance);

            Console.WriteLine($"Stake Amount: {stakeAmount}");
            return stakeAmount;
        }

        public void PressEnterToContinue()
        {
            Console.WriteLine("Press 'Enter' to continue");
            Console.ReadLine();
        }

        public void DisplayWinnings(decimal winnings)
        {
            Console.WriteLine($"Congratulations, you have won: {winnings}");
        }

        public void DisplayCurrentBalance(decimal currentBalance)
        {
            Console.WriteLine($"Current Balance : {Math.Round(currentBalance, 2)}");
        }

        public void GameOver()
        {
            Console.WriteLine("Game Over");
            Console.ReadLine();
        }

        public void ClearScene()
        {
            if (!ConfigReader.GetBoolConfigValue(ConfigConstants.AutoSpinWithSameStake))
                Console.Clear();
        }

        public void DrawGrid(IList<IWheelRow> rows)
        {
            if (rows.Any())
            {
                foreach (IWheelRow row in rows)
                {
                    if (row.IsWinningRow() && ConfigReader.GetBoolConfigValue(ConfigConstants.HighlightWinningRow))
                        Console.BackgroundColor = ConsoleColor.Red;

                    Console.WriteLine(row.GetCellValuesAsText(ConfigReader.GetStringConfigValue(ConfigConstants.ColumnSeperatorValue)));

                    Console.ResetColor();
                }
            }
        }
    }
}
