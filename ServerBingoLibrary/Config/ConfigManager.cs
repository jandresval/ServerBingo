using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ServerBingo.Config
{
    public class ConfigManager
    {
        #region Configuracion
        public static string LogFile {
            get { return GetVariableConfiguration("logFile"); } 
        }

        public static string PatternValidateIp
        {
            get { return GetVariableConfiguration("validateIP"); }
        }

        public static string PatternValidateMac { 
            get { return GetVariableConfiguration("validateMac"); } 
        }

        #endregion

        #region Mensajes

        public static string ErrorUsuarioNoEnviado {
            get { return GetVariableConfiguration("usuarioNoExiste"); } 
        }

        public static string ErrorIpNoEnviada { 
            get { return GetVariableConfiguration("ipNoExiste"); } 
        }

        public static string ErrorIpNoValida { 
            get { return GetVariableConfiguration("ipNoValida"); } 
        }

        public static string ErrorMacNoEnviada {
            get { return GetVariableConfiguration("macNoExiste"); } 
        }

        public static string ErrorMacNoValida { get { return GetVariableConfiguration("macNoValida"); } }

        #endregion


        private static string GetVariableConfiguration(string variable)
        {
            return ConfigurationManager.AppSettings[variable].ToString();
        }


    }
}
