USE g:\projects\c#\serverbingo\serverbingo\app_data\bingousuarios.dbf IN 0 SHARED

activar=.f.
saldoactuald = 0
usuario = "jaime4"

UPDATE Bingousuarios SET saldoactual=saldoactuald,activo=activar WHERE alias=usuario

USE
