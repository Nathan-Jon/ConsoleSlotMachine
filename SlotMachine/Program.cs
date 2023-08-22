using SlotMachine.DataTypes;
using System;

namespace SlotMachine
{
    internal class Program
    {

        static void Main(string[] args)
        {
            IConfigReader configReader = new ConfigReader();
            Random rand = new Random();
            IInputValidator inputValidator = new InputValidator(configReader);
            ICellValues cellValues = new CellValues(rand);
            IGridLogic gridLogic = new GridLogic(configReader, cellValues);

            ISlotMachineLogic slotMachineLogic = new SlotMachineLogic(inputValidator, configReader, gridLogic);

            slotMachineLogic.StartGame();
        }
    }
}

