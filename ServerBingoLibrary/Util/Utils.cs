using ServerBingo.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ServerBingo.Util
{
    public class Utils
    {
        public static bool ValideIp(string ip)
        {
            try
            {
                if (Regex.IsMatch(ip, ConfigManager.PatternValidateIp))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static bool ValidateMac(string mac)
        {
            try
            {
                if (Regex.IsMatch(mac, ConfigManager.PatternValidateMac))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}