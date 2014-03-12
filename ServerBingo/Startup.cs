using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using System.Web.Routing;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(ServerBingo.Startup))]

namespace ServerBingo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());

            app.Map("/signalr", map => {
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration()
                {
                    EnableJSONP =true,
                    EnableJavaScriptProxies = true,
                    EnableDetailedErrors = true
                };

                map.RunSignalR(hubConfiguration);

            });

        }
    }
}
