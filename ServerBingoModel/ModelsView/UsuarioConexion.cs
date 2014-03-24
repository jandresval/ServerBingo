using Newtonsoft.Json;
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
        [JsonProperty("Alias")]
        public string Alias { get; set; }
        private string _macaddress;
        [JsonProperty("Macadress")]
        public string Macaddress
        {
            get 
            {
                return _macaddress;
            }
 
            set 
            {
                /*if (!value.Equals("") && Utils.ValidateMac(value))
                {
                    _macaddress = value;
                }
                else
                {
                    throw new Exception(ConfigManager.ErrorMacNoValida);
                }*/
                _macaddress = value;
            } 
        }
        private string _ip;

        [JsonProperty("Ip")]
        public string Ip {
            get
            {
                return _ip;
            }
            set 
            {
                /*if (!value.Equals("") && Utils.ValideIp(value))
                {
                    _ip = value;
                }
                else
                {
                    throw new Exception(ConfigManager.ErrorIpNoValida);
                }*/
                _ip = value;
            } 
        }
        [JsonProperty("Gpslatitud")]
        public Nullable<double> Gpslatitud { get; set; }
        [JsonProperty("Gpslongitud")]
        public Nullable<double> Gpslongitud { get; set; }
        [JsonIgnore]
        public byte[] Imagenuser { get; set; }
        [JsonIgnore]
        public string conectionId { get; set; }
    }
}