using System;

namespace SlotMachine
{
    public class InputValidator : IInputValidator
    {
        public IConfigReader ConfigReader { get; }

        public InputValidator(IConfigReader configReader) 
        {
            ConfigReader = configReader ?? throw new NullReferenceException($"{nameof(ConfigReader)} not found when creating {nameof(InputValidator)}");
        }

        public bool ValidateIsZeroOrBelow(decimal value)
        {
            return value <= 0;
        }

        public bool ValidateTextIsDecimal(string text)
        {
            if (!string.IsNullOrEmpty(text) && decimal.TryParse(text, out decimal value))
                return true;

            return false;
        }

        public bool ValidateValueIsHigher(decimal highValue, decimal lowValue)
        {
            return highValue >= lowValue;
        }

    }
}
