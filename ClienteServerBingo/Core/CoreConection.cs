using ClienteServerBingo.Config;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServerBingo.Core
{
    public class CoreConection
    {

        private IHubProxy myHubProxy;

        public CoreConection()
        {
            
        }

        public void SendBalotas(string user, string balota)
        {
            var hubConnection = new HubConnection(ConfigManager.Url);
            myHubProxy = hubConnection.CreateHubProxy("BingoHub");

            hubConnection.Start().Wait(10000);

            try
            {
                myHubProxy.Invoke("Send", user, balota).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine("!!! There was an error opening the connection:{0} \n", task.Exception.GetBaseException());
                        }

                    }).Wait(10000);
            }
            catch
            {
                Console.WriteLine(ConfigManager.ErrorIntentarAgain);
            }
        }
    }
}
