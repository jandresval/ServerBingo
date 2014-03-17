using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using ServerBingo.Config;
using ServerBingo.Util;
using ServerBingo.ModelsView;
using ServerBingo.Models;

namespace ServerBingo
{
    public static class UserHandler
    {
        public static Dictionary<string, string> Connections = new Dictionary<string, string>();
    }


    public class BingoHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {
            Mensajes.Show(name + " : " + message);
            Clients.All.Send(name, message);
        }

        public void SendtoUser(string name, string message)
        {
            Mensajes.Show(name + " : " + message);
            //string userId = "";
            //if (UserHandler.ConnectedIds.TryGetValue(name, out userId))
            //Clients.User(userId).send(message);
            Clients.All.Send(name, message);
        }

        public void ConectarUsurio(UsuarioConexion usuarioConexion)
        {

        }

        public void DevolverUsuario(string name)
        {
            Bingousuario bingoUsuario = null;
            string conectionId = "";

            if (UserHandler.Connections.TryGetValue(name, out conectionId))
            {
                BingoServerEntities db = new BingoServerEntities();

                bingoUsuario = (from n in db.Bingousuarios
                                             where n.Alias == name
                                             select n).First();

                Clients.Client(conectionId).DevolverInfoUsuario(bingoUsuario);
            }
            
        }

        public IEnumerable<Bingousuario> DevolverUsuario(string name)
        {

            BingoServerEntities db = new BingoServerEntities();

            var bingoUsuario = from n in db.Bingousuarios
                            where n.Alias == name
                            select n;

            return bingoUsuario;
        }

        public override Task OnConnected()
        {


            if (Context.QueryString["User"].Equals(""))
            {
                throw new HubException(ConfigManager.ErrorUsuarioNoEnviado);
            }

            /*if (Context.QueryString["ip"].Equals(""))
            {
                throw new HubException(ConfigManager.ErrorIpNoEnviada);
            }

            if (!Utils.ValideIp(Context.QueryString["ip"].ToString()))
            {
                throw new HubException(ConfigManager.ErrorIpNoValida);
            }

            if (Context.QueryString["mac"].Equals(""))
            {
                throw new HubException(ConfigManager.ErrorMacNoEnviada);
            }

            if (!Utils.ValidateMac(Context.QueryString["mac"].ToString()))
            {
                throw new HubException(ConfigManager.ErrorMacNoValida);
            }*/

            lock (UserHandler.Connections)
            {
                if (UserHandler.Connections.ContainsKey(Context.QueryString["User"]))
                {
                    UserHandler.Connections.Remove(Context.QueryString["User"]);
                }
                UserHandler.Connections.Add(Context.QueryString["User"], Context.ConnectionId);
            }
            
            Mensajes.Show("Hub OnConnected " + Context.ConnectionId + "\n");
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            Mensajes.Show("Hub OnDisconnected " + Context.ConnectionId + "\n");
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            Mensajes.Show("Hub OnReconnected " + Context.ConnectionId + "\n");
            return base.OnReconnected();
        }

    }
}