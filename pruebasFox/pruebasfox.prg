
jsonObject = NEWOBJECT('myObj','d:\projects\c#\serverbingo\pruebasfox\libreriafox\json.prg')

oNet = CREATEOBJECT("ClienteFoxDLL.Funciones")

TRY

?oNet.ConectarServidor()

*Desconectar usuarios
 ?oNet.desconectarUsuario("Jaime5")

*!*	*Listar usuarios
*!*	cInfo = oNet.ListadoUsuarios()

*!*	objetoRetorno = json_decode(cInfo)

*!*	FOR EACH oMyVar IN objetoRetorno.array
*!*		?oMyVar
*!*	ENDFOR

*!*	* Enviar Usuario
*!*	?oNet.EnviarUsuario("Jaime5")

*!*	*Iniciar Juegos

*!*	?oNet.IniciarJuego()

?oNet.DesconectarServidor ()


CATCH TO oErr
 oErr.UserValue = "Nested CATCH message: Unable to handle"
      ?[: Nested Catch! (Unhandled: Throw oErr Object Up) ]  
      ?[  Inner Exception Object: ]
      ?[  Error: ] + STR(oErr.ErrorNo) 
      ?[  LineNo: ] + STR(oErr.LineNo) 
      ?[  Message: ] + oErr.Message 
      ?[  Procedure: ] + oErr.Procedure 
      ?[  Details: ] + oErr.Details 
      ?[  StackLevel: ] + STR(oErr.StackLevel) 
      ?[  LineContents: ] + oErr.LineContents
      ?[  UserValue: ] + oErr.UserValue 
FINALLY
	Release oNet
ENDTry



