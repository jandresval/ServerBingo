using ServerBingo.ModelsView;
using ServerBingoModel.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerBingo
{
    public static class UserHandler
    {
        public static Dictionary<string, UsuarioConexion> Connections = new Dictionary<string, UsuarioConexion>();

        public static Dictionary<string, List<BingotblView>> DataConnections = new Dictionary<string, List<BingotblView>>();

        public static UsuarioConexion RetornarConection(string name)
        {
            UsuarioConexion usuConection = new UsuarioConexion();
            if (UserHandler.Connections.TryGetValue(name, out usuConection))
            {
                return usuConection;
            }
            else
            {
                return usuConection;
            }

        }

        public static bool ValidarMac(UsuarioConexion usuario)
        {
            UsuarioConexion buscar = RetornarConection(usuario.Alias);

            if (buscar.Macaddress.Equals(usuario.Macaddress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RemoveConectionWithId(string conectionId)
        {
            string name = "";
            foreach (KeyValuePair<string, UsuarioConexion> item in Connections)
            {
                UsuarioConexion usu = item.Value;
                if (usu.conectionId == conectionId)
                {
                    name = item.Key;
                    break;

                }
            }
            if (!name.Equals(""))
            {
                lock (Connections)
                {
                    Connections.Remove(name);
                }
            }
        }

        public static List<string> ListadodeUsuarios()
        {
            List<string> retorno = Connections.Keys.ToList();
            return retorno;
        }
    }
}