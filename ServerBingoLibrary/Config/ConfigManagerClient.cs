using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ServerBingoLibrary.Config
{

    public class ConfigManagerClient 
    {
        #region Configuracion

        public static string DireccionServidor
        {
            get { return GetVariableConfiguration("direccionServidor"); }
        }

        public static string NombreServidor
        {
            get { return GetVariableConfiguration("nombreServidor"); }
        }

        #endregion

        #region Mensajes

        public static string MensajeDesconeccion
        {
            get { return GetVariableConfiguration("errorDesconectarCliente"); }
        }

        #endregion


        private static string GetVariableConfiguration(string variable)
        {
            return ConfigurationManager.AppSettings[variable].ToString();
        }
    }
}

