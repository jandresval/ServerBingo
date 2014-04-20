using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerBingo;

namespace UnitTestServerBingo
{
    [TestClass]
    public class UTServerBingo
    {
        [TestMethod]
        public void DesconectarUsuarioTest()
        {
            BingoHub bingoHub = new BingoHub();

            bingoHub.DesconectarUsuario("jaime5");

        }

        [TestMethod]
        public void ListadoUsuariosTest()
        {
            BingoHub bingoHub = new BingoHub();

            string listadoUsuarios = bingoHub.ListadoUsuarios();

            Assert.AreNotEqual("", listadoUsuarios);

        }

        [TestMethod]
        public void IniciarProcesoDescargaTablasTest()
        {
            BasicOperation bingoHub = new BasicOperation();

            int numeroTablas = bingoHub.IniciarProcesoDescargaTablas("jandresv5");

            Assert.AreNotEqual(0, numeroTablas);

        }

        [TestMethod]
        public void RetornarTablasTest()
        {
            BasicOperation bingoHub = new BasicOperation();

            string retorno = bingoHub.RetornarTablas("jandresv5", 0, 100);

            Assert.AreNotEqual(string.Empty, retorno);

        }

    }
}
