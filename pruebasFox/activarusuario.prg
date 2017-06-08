USE g:\projects\c#\serverbingo\serverbingo\app_data\bingousuarios.dbf IN 0 SHARED

activar=.t.
saldoactuald = 150000
usuario = "jaime4"

UPDATE Bingousuarios SET saldoactual=saldoactuald,activo=activar WHERE alias=usuario

use
