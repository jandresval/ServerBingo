using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClienteFoxDLL
{

    [ClassInterface(ClassInterfaceType.None), Guid("63C6BD1A-3DF7-4700-8CE6-22854B44F399")]
    [ComDefaultInterface(typeof(ILauncher))]
    public class Funciones : ILauncher
    {
        private HubConnection hubConnection = null;
        private IHubProxy myHubProxy = null;
        private Config config = null;

        public Funciones()
        {
            config = new Config();
        }

       public string ConectarServidor()
        {
            try
            {
                hubConnection = new HubConnection(config.GetAppSettingValue("direccionServidor"));

                myHubProxy = hubConnection.CreateHubProxy(config.GetAppSettingValue("nombreServidor"));

                hubConnection.Start().Wait(10000);

                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public string DesconectarServidor()
        {
            try
            {
                hubConnection.Dispose();
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DesconectarUsuario(string nombreUsuario)
        {
            try
            {
                myHubProxy.Invoke("DesconectarUsuario", nombreUsuario).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        throw new Exception(config.GetAppSettingValue("errorDesconectarCliente") + " : " + task.Exception.GetBaseException());
                    }
                }).Wait();
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ListadoUsuarios()
        {
            
            try
            {
                string resultado = myHubProxy.Invoke<string>("ListadoUsuarios").Result;
                return resultado;
            }
            catch
            {
                return string.Empty;
            }
        }

        public string EnviarUsuario(string name)
        {
            try
            {
                myHubProxy.Invoke("SendUsuario", name).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        throw new Exception(config.GetAppSettingValue("errorEnvioIniciJuego") + ":" + task.Exception);
                    }
                });
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string IniciarJuego()
        {
            try
            {
                myHubProxy.Invoke("IniciarJuego").ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            throw new Exception(config.GetAppSettingValue("errorEnvioInicioJuego") + ":" + task.Exception);
                        }
                    });
                return "Ok";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string EnviarBalotas(string balota)
        {
            try
            {
                myHubProxy.Invoke("EnviarBalota", balota).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            throw new Exception(config.GetAppSettingValue("errorEnvioBalota") + ":" + task.Exception);
                        }
                    });
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
