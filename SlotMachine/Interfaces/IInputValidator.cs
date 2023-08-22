namespace SlotMachine
{
    public interface IInputValidator
    {
        bool ValidateIsZeroOrBelow(double value);
        bool ValidateTextIsDouble(string text);
        bool ValidateValueIsHigher(double highValue, double lowValue);
    }
}