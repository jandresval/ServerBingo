using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServerBingo
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string arg0 = string.Empty;
            if (args.Length > 0)
                arg0 = (args[0] ?? string.Empty).ToLower();

            if (arg0 == "-service")
            {
                RunService();
                return;
            }
            if (arg0 == "-fakeservice")
            {
                FakeRunService();
                return;
            }
            else if (arg0 == "-installservice" || arg0 == "-i")
            {
                WindowsServiceManager SM = new WindowsServiceManager();
                if (!SM.InstallService(Environment.CurrentDirectory + "\\ServicioServerBingo.exe -service",
                        "BingoMessageBus", "Bingo Message Bus Service"))
                    Console.WriteLine("Service install failed.");

                return;
            }
            else if (arg0 == "-uninstallservice" || arg0 == "-u")
            {
                WindowsServiceManager SM = new WindowsServiceManager();
                if (!SM.UnInstallService("MPQueueService"))
                    Console.WriteLine("Service failed to uninstall.");

                return;
            }
        }

        static void RunService()
        {
            var ServicesToRun = new ServiceBase[] { new BingoMessageBus() };
            ServiceBase.Run(ServicesToRun);
        }

        static void FakeRunService()
        {
            var service = new BingoMessageBus();
            service.Start();

            Console.ReadLine();
            // never ends but waits
            
        }
    }
}
