using SlotMachine.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlotMachine
{
    public class SlotMachineLogic : ISlotMachineLogic
    {
        public IInputValidator InputValidator { get; private set; }
        public IConfigReader ConfigReader { get; private set; }
        public IGridLogic GridLogic { get; private set; }

        public SlotMachineLogic(IInputValidator inputValidator, IConfigReader configReader, IGridLogic gridLogic)
        {
            InputValidator = inputValidator ?? throw new NullReferenceException($"{nameof(InputValidator)} not found when creating {nameof(SlotMachineLogic)}");
            ConfigReader = configReader ?? throw new NullReferenceException($"{nameof(ConfigReader)} not found when creating {nameof(SlotMachineLogic)}");
            GridLogic = gridLogic ?? throw new NullReferenceException($"{nameof(GridLogic)} not found when creating {nameof(SlotMachineLogic)}");
        }


        public void StartGame()
        {
            double totalAmount = RequestStartingAmount();
            CoreGameLoop(totalAmount);
            EndGame();
        }

        private double RequestStakeAmount(double availableFunds)
        {
            int minimumStake = ConfigReader.GetIntConfigValue(ConfigConstants.MinimumStakeValue);

            Console.WriteLine("Enter Stake Amount:");

            if(minimumStake != 0)
                Console.WriteLine($"The minimum stake is : {minimumStake}");

            double stakeAmount = 0;

            do
            {
                string text = Console.ReadLine();

                bool isDouble = InputValidator.ValidateTextIsDouble(text);
                if (isDouble)
                {
                    double amount = Convert.ToDouble(text);

                    if(InputValidator.ValidateIsZeroOrBelow(amount))
                    {
                        Console.WriteLine($"Stake amount cannot be lower than, or equal to 0 please use a higher stake");
                    }
                    if (!InputValidator.ValidateValueIsHigher(availableFunds, amount))
                    {
                        Console.WriteLine($"Stake amount cannot be higher then your total funds, please use a lower stake");
                        continue;
                    }
                    if (!InputValidator.ValidateValueIsHigher(amount, ConfigReader.GetIntConfigValue(ConfigConstants.MinimumStakeValue)))
                    {
                        Console.WriteLine($"Stake amount cannot be lower than the minimum stake, please use a higher stake");
                        continue;
                    }

                    stakeAmount = amount;

                }
                else
                {
                    Console.WriteLine("Please input a number");
                }

            } while (stakeAmount == 0);

            //Console.WriteLine($"Stake Amount : {stakeAmount}");

            return stakeAmount;
        }

        private double RequestStartingAmount()
        {
            Console.WriteLine("Please deposit money you would like to play with:");

            double totalAmount = 0;

            do
            {
                string number = Console.ReadLine();

                if (InputValidator.ValidateTextIsDouble(number))
                {
                    totalAmount = Convert.ToDouble(number);
                }
                else
                {
                    Console.WriteLine("Please input a number");
                }

            } while (totalAmount == 0);

            Console.WriteLine($"Total Funds : {totalAmount}");

            return totalAmount;
        }

        private void CoreGameLoop(double totalAmount)
        {
            double stakeAmount = 0;
            bool firstSpin = true;
            while (totalAmount > 0)
            {
                // If this is the first spin, or auto spin is disabled, Request the stake amount for the round
                if(firstSpin || !firstSpin && !ConfigReader.GetBoolConfigValue(ConfigConstants.AutoSpinWithSameStake))
                {
                    stakeAmount = RequestStakeAmount(totalAmount);

                }

                //Generate and draw the Slot machine Grid
                IList<IWheelRow> rows = GridLogic.GenerateWheelRows(ConfigReader.GetIntConfigValue(ConfigConstants.NumberOfWheelRows));
                GridLogic.DrawGrid(rows);


                // Calculate and apply the winnings based on the slot results
                double winnings = CalculateWinnings(rows, stakeAmount);
                if (winnings != 0)
                {
                    Console.WriteLine($"Congratulations, you have won: {winnings}");
                }

                totalAmount += winnings - stakeAmount;

                Console.WriteLine($"Current Balance : {Math.Round(totalAmount, 2)}");

                if (totalAmount <= 0)
                    break;

                firstSpin = false;
                Console.WriteLine("_________________________");

            }
        }


        /// <summary>
        /// Calculate The winnings from the spin results provided
        /// </summary>
        /// <param name="winningRows"></param>
        /// <returns></returns>
        private double CalculateWinnings(IList<IWheelRow> rows, double stakeAmount)
        {
            double winnings = 0;

            if (!rows.Any(x => x.IsWinningRow()))
                return winnings;


            if (rows.Any(x => x.IsWinningRow()))
            {
                double totalWinCoefficient = rows.Where(x => x.IsWinningRow()).Sum(x => x.GetTotalRowCoefficient());

                //Calculate Winnings
                winnings = totalWinCoefficient * stakeAmount;
            }

            return winnings;
        }

        /// <summary>
        /// Trigger Game Over message
        /// </summary>
        private void EndGame()
        {
            Console.WriteLine("Game Over");
            Console.ReadLine();
        }
    }
}
