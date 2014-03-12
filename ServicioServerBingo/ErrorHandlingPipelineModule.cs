using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServerBingo
{
    public class ErrorHandlingPipelineModule : HubPipelineModule
    {
        protected override void OnIncomingError(ExceptionContext ex, IHubIncomingInvokerContext context)
        {
            Debug.WriteLine("=> Exception " + ex.Error.Message);
            if (ex.Error.InnerException != null)
            {
                Debug.WriteLine("=> Inner Exception " + ex.Error.InnerException.Message);
            }
            base.OnIncomingError(ex, context);
        }
    }
}
