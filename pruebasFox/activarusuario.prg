
USE d:\projects\c#\serverbingo\serverbingo\app_data\bingousuarios.dbf IN 0 SHARED
BROWSE LAST

activar=.t.
saldoactuald = 30000

UPDATE Bingo!bingousuario SET saldoactual=saldoactuald,activo=activar WHERE alias='jaime5'

CLOSE TABLES
