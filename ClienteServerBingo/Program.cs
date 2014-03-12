using ClienteServerBingo.Core;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServerBingo
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                CoreConection coreConection = new CoreConection();

  

                if (!options.Balotas.Equals(""))
                {
                    coreConection.SendBalotas(options.User, options.Balotas);
                }
            }
        }
    }
}
