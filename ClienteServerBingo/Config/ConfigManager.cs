using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ClienteServerBingo.Config
{
    public class ConfigManager
    {

        public static string BaseIP { 
            get { return ConfigurationManager.AppSettings["BaseIp"].ToString(); } 
        }

        public static string Port { 
            get { return ConfigurationManager.AppSettings["Port"].ToString(); } 
        }

        public static string ApplicationName { 
            get { return ConfigurationManager.AppSettings["ApplicationName"].ToString(); } 
        }

        public static string HelpServer { 
            get { return ConfigurationManager.AppSettings["Signal"].ToString(); } 
        }

        public static string Url { 
            get { return "http://" + BaseIP + ":" + Port + "/" + ApplicationName + "/" + HelpServer; } 
        }

        public static string ErrorIntentarAgain { get { return ConfigurationManager.AppSettings["ErrorIntentarAgain"].ToString(); } }

    }
}
