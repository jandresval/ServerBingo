using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerBingo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBasicOperation" in both code and config file together.
    [ServiceContract]
    public interface IBasicOperation
    {

        [OperationContract]
        int IniciarProcesoDescargaTablas(string name);

        [OperationContract]
        string RetornarTablas(string name, int inicial, int final);
    }
}
