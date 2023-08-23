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
            ICellValueLogic cellValues = new CellValueLogic(rand);
            ISlotMachineLogic gridLogic = new SlotMachineLogic(configReader, cellValues);
            ISlotMachineView view = new SlotMachineView(inputValidator, configReader);

            ISlotMachineController slotMachineLogic = new SlotMachineController(configReader, gridLogic, view);

            slotMachineLogic.StartGame();
        }
    }
}

