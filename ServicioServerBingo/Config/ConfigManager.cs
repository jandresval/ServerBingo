using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ServicioServerBingo.Config
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

    }
}
