using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServerBingo
{

    public static class UserHandler
    {
        public static Dictionary<string, string> ConnectedIds = new Dictionary<string,string>();
    }

    public class BingoHub : Hub
    {

        EventLog eventLog = new EventLog();

        public BingoHub()
        {
            if (!System.Diagnostics.EventLog.SourceExists("BingoMessageBus")) 
            {         
                System.Diagnostics.EventLog.CreateEventSource(
                    "BingoMessageBus", "BingoMessageBusLog");
            }
            eventLog.Source = "BingoMessageBus";
            eventLog.Log = "BingoMessageBusLog";
        }

        /// <summary>
        /// Context instance to access client connections to broadcast to
        /// </summary>
        public static IHubContext HubContext
        {
            get
            {
                if (_context == null)
                    _context = GlobalHost.ConnectionManager.GetHubContext<BingoHub>();

                return _context;
            }
        }
        static IHubContext _context = null;
        
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
