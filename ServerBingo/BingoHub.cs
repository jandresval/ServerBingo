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
        public static Dictionary<string, UsuarioConexion> Connections = new Dictionary<string, UsuarioConexion>();
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
            lock (UserHandler.Connections)
            {
                usuarioConexion.conectionId = Context.ConnectionId;

                if (UserHandler.Connections.ContainsKey(usuarioConexion.Alias))
                {
                    UserHandler.Connections.Remove(usuarioConexion.Alias);
                }
                UserHandler.Connections.Add(usuarioConexion.Alias, usuarioConexion);
            }
        }

        public void SendUsuario(string name)
        {
            Bingousuario bingoUsuario = null;
            UsuarioConexion usuConection = new UsuarioConexion();

            if (UserHandler.Connections.TryGetValue(name, out usuConection))
            {
                BingoServerEntities db = new BingoServerEntities();

                bingoUsuario = (from n in db.Bingousuarios
                                             where n.Alias == name
                                             select n).First();

                Clients.Client(usuConection.conectionId).DevolverInfoUsuario(bingoUsuario);
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