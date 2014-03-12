using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServerBingo
{
    public class Mensajes
    {
        

        public static void Show(string mensaje)
        {
            EventLog eventLog = new EventLog();
            WindowsServiceManager SM = new WindowsServiceManager();

            if (SM.IsServiceInstalled("BingoMessageBus"))
            {
                if (!System.Diagnostics.EventLog.SourceExists("BingoMessageBus"))
                {
                    System.Diagnostics.EventLog.CreateEventSource(
                        "BingoMessageBus", "BingoMessageBusLog");
                }
                eventLog.Source = "BingoMessageBus";
                eventLog.Log = "BingoMessageBusLog";
                eventLog.WriteEntry(mensaje);
            }
            else
            {
                Console.WriteLine(mensaje);
            }

        }
    }
}
