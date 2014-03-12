using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ClienteServerBingo.Config
{
    public static class ConfigManager
    {

        public static string BaseIp()
        {
            return ConfigurationManager.AppSettings["BaseIp"].ToString();
        }

        public static string Port()
        {
            return ConfigurationManager.AppSettings["Port"].ToString();
        }

        public static string ApplicationName()
        {
            return ConfigurationManager.AppSettings["ApplicationName"].ToString();
        }

        public static string HelpServer()
        {
            return ConfigurationManager.AppSettings["Signal"].ToString();
        }

        public static string Url()
        {
            return "http://" + BaseIp() + ":" + Port() + "/" + ApplicationName() + "/" + HelpServer();
        }

    }
}
