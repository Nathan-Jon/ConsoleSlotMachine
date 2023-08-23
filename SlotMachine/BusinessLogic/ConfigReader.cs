using System;
using System.Configuration;
using System.Linq;

namespace SlotMachine
{
    public class ConfigReader : IConfigReader
    {
        public ConfigReader() { }

        public string GetStringConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public int GetIntConfigValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return int.Parse(value);
                }
                catch (Exception)
                {
                    throw new Exception($"failed to cast config for {key} as int");
                }
            }

            return 0;
        }

        public bool GetBoolConfigValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return bool.Parse(value);
                }
                catch (Exception)
                {
                    throw new Exception($"failed to cast config for {key} as bool");
                }
            }
            return false;
        }

    }
}
