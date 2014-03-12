using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ServerBingo
{
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

        public override Task OnConnected()
        {
            //if (!Context.User.Identity.Name.Equals(""))
            //  UserHandler.ConnectedIds.Add(Context.User.Identity.Name, Context.ConnectionId);
            Mensajes.Show("Hub OnConnected " + Context.ConnectionId + "\n");
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            // if (!Context.User.Identity.Name.Equals(""))
            //   UserHandler.ConnectedIds.Remove(Context.User.Identity.Name);
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