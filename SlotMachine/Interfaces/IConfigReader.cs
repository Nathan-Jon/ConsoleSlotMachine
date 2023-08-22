namespace SlotMachine
{
    public interface IConfigReader
    {
        bool GetBoolConfigValue(string key);
        int GetIntConfigValue(string key);
        string GetStringConfigValue(string key);
    }
}