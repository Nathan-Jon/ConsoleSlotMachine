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

        public bool ValidateIsZeroOrBelow(double value)
        {
            return value <= 0;
        }

        public bool ValidateTextIsDouble(string text)
        {
            if (!string.IsNullOrEmpty(text) && double.TryParse(text, out double value))
                return true;

            return false;
        }

        public bool ValidateValueIsHigher(double highValue, double lowValue)
        {
            return highValue >= lowValue;
        }

    }
}
