using ServerBingo.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBingo
{
    public class Mensajes
    {
        

        public static void Show(string mensaje)
        {
            /*EventLog eventLog = new EventLog();

            if (!System.Diagnostics.EventLog.SourceExists("BingoMessageBus"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "BingoMessageBus", "BingoMessageBusLog");
            }
            eventLog.Source = "BingoMessageBus";
            eventLog.Log = "BingoMessageBusLog";
            eventLog.WriteEntry(mensaje);*/

            //Console.WriteLine(mensaje);

            using (StreamWriter w = File.AppendText(ConfigManager.LogFile))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :");
                w.WriteLine("  :{0}", mensaje);
                w.WriteLine("-------------------------------");
            }

        }
    }
}
