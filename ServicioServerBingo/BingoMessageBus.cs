using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Host.HttpListener;
using ServicioServerBingo.Config;

namespace ServicioServerBingo
{
    public partial class BingoMessageBus : ServiceBase
    {

        IDisposable SignalR;

        public BingoMessageBus()
        {
            InitializeComponent();
        }

        public void Start()
        {
            string url = "http://" + ConfigManager.BaseIp() + ":" + ConfigManager.Port();
            SignalR = WebApp.Start(url);
            Globals.WindowsService = this;
        }

        public new void Stop()
        {
            SignalR.Dispose();
        }

        protected override void OnStart(string[] args)
        {
            Start();
            
        }

        protected override void OnStop()
        {
            Stop();
        }

    }
}
