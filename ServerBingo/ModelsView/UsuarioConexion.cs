using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerBingo.ModelsView
{
    public class UsuarioConexion
    {
        public string Alias { get; set; }
        public string Macadress { get; set; }
        public string Ip { get; set; }
        public Nullable<double> Gpslatitud { get; set; }
        public Nullable<double> Gpslongitud { get; set; }
        public byte[] Imagenuser { get; set; }
    }
}