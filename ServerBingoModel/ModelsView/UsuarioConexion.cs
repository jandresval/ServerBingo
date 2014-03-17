using ServerBingo.Config;
using ServerBingo.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerBingo.ModelsView
{
    public class UsuarioConexion
    {
        public string Alias { get; set; }
        private string _macaddress;
        public string Macaddress
        {
            get 
            {
                return _macaddress;
            }
 
            set 
            {
                if (!value.Equals("") && Utils.ValidateMac(value))
                {
                    _macaddress = value;
                }
                else
                {
                    throw new Exception(ConfigManager.ErrorMacNoValida);
                }
            } 
        }
        private string _ip;
        public string Ip {
            get
            {
                return _ip;
            }
            set 
            {
                if (!value.Equals("") && Utils.ValideIp(value))
                {
                    _ip = value;
                }
                else
                {
                    throw new Exception(ConfigManager.ErrorIpNoValida);
                }
            } 
        }
        public Nullable<double> Gpslatitud { get; set; }
        public Nullable<double> Gpslongitud { get; set; }
        public byte[] Imagenuser { get; set; }
        public string conectionId { get; set; }
    }
}