using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteFoxDLL
{
    public class Config
    {
        public string GetAppSetting(Configuration config, string key)
        {
            KeyValueConfigurationElement element = config.AppSettings.Settings[key];
            if (element != null)
            {
                string value = element.Value;
                if (!string.IsNullOrEmpty(value))
                    return value;
            }
            return string.Empty;
        }

        public string GetAppSettingValue(string myKey)
        {
            Configuration config = null;
            string exeConfigPath = this.GetType().Assembly.Location;
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(exeConfigPath);
            }
            catch 
            {
            }
            if (config != null)
            {
                string myValue = GetAppSetting(config, myKey);
                return myValue;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
