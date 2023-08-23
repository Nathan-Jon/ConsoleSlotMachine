namespace SlotMachine
{
    public interface IInputValidator
    {
        bool ValidateIsZeroOrBelow(decimal value);
        bool ValidateTextIsDecimal(string text);
        bool ValidateValueIsHigher(decimal highValue, decimal lowValue);
    }
}