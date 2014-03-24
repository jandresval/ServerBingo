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
using System.Data.Entity;
using Newtonsoft.Json;

namespace ServerBingo
{
    public static class UserHandler
    {
        public static Dictionary<string, UsuarioConexion> Connections = new Dictionary<string, UsuarioConexion>();

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
    }


    public class BingoHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }


        public void ConectarUsuarioJSON(string objetoConexion)
        {
            UsuarioConexion usuarioConexion = JsonConvert.DeserializeObject<UsuarioConexion>(objetoConexion);
            
            ConectarUsuario(usuarioConexion);

        }

        public void ConectarUsuario(UsuarioConexion usuarioConexion)
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

            BingoServerEntities db = new BingoServerEntities();

            try
            {
                

                Bingousuario bingoUsuario = (from n in db.Bingousuarios
                                             where n.Alias == usuarioConexion.Alias 
                                             select n).FirstOrDefault();

                if (bingoUsuario == null)
                {
                    bingoUsuario = new Bingousuario
                    {
                        Alias = usuarioConexion.Alias,
                        Ip = usuarioConexion.Ip,
                        Macadress = usuarioConexion.Macaddress,
                        Gpslatitud = usuarioConexion.Gpslatitud,
                        Gpslongitud = usuarioConexion.Gpslongitud,
                        Imagenuser = usuarioConexion.Imagenuser,
                        Ultimafechaconexion = DateTime.Now,
                        Ultimafechadejuego = DateTime.Now,
                        Socsucursal = ConfigManager.SocSucursal,
                        Activo = false
                    };

                    db.Bingousuarios.Add(bingoUsuario);

                    db.SaveChanges();

                }
                else
                {
                    bingoUsuario.Ultimafechaconexion = DateTime.Now;
                    bingoUsuario.Ultimafechadejuego = DateTime.Now;
                    db.Bingousuarios.Attach(bingoUsuario);
                    db.Entry(bingoUsuario).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch
            {
                
            }

        }

        public void SendUsuario(string name)
        {
            Bingousuario bingoUsuario = null;
            UsuarioConexion usuConection = new UsuarioConexion();

            if (UserHandler.Connections.TryGetValue(name, out usuConection))
            {
                BingoServerEntities db = new BingoServerEntities();
                try
                {
                    if (!db.Database.Exists())
                    {
                        return;
                    }

                    bingoUsuario = (from n in db.Bingousuarios
                                    where n.Alias == name &&
                                    n.Activo == true
                                    select n).FirstOrDefault();

                    Clients.Client(usuConection.conectionId).DevolverInfoUsuario(bingoUsuario);
                }
                catch
                {

                }

            }

        }

        public void DesconectarUsuario(string name)
        {
            UsuarioConexion usuConexion = UserHandler.RetornarConection(name);

            if (!usuConexion.conectionId.Equals(""))
            {
                Clients.Client(usuConexion.conectionId).Desconectar();
            }

        }


        public string DevolverUsuarioJSON(string name)
        {
            Bingousuario bingoUsuairo = DevolverUsuario(name);
            if (bingoUsuairo == null)
            {
                bingoUsuairo = new Bingousuario();
            }

            return JsonConvert.SerializeObject(bingoUsuairo);

        }

        public Bingousuario DevolverUsuario(string name)
        {
            BingoServerEntities db = new BingoServerEntities();
            try
            {
                if (!db.Database.Exists())
                {
                    return null;
                }

                Bingousuario bingoUsuario = (from n in db.Bingousuarios
                                             where n.Alias == name &&
                                             n.Activo == true
                                             select n).FirstOrDefault();

                return bingoUsuario;
            }
            catch
            {
                throw new HubException("Error al devolver usuario.");
            }

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