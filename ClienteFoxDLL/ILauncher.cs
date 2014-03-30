using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClienteFoxDLL
{
    [InterfaceType(ComInterfaceType.InterfaceIsDual), Guid("C19F079E-7937-4E82-95C5-8DE191EE0D43")]
    public interface ILauncher
    {
        string ConectarServidor();

        string DesconectarServidor();

        string DesconectarUsuario(string nombreUsuario);

        string ListadoUsuarios();

        string EnviarUsuario(string name);

        string IniciarJuego();
    }
}
