*
* vfpjson
*
* ---------------------------------- 
* Ignacio Guti�rrez Torrero
* SAIT Software Administrativo
* www.sait.com.mx
* +52(653)534-8800
* Monterrey M�xico
* -----------------------------------
*
* Libreria para el manejo de JSON en VFP
*
* Gracias a Google por el codigo de Json de Dart
* Thanks Google for the code in Json Dart
*     http://code.google.com/p/dart/source/browse/trunk/dart/lib/json/json.dart
*
*
* Codificar y Decodificar JSON
*
* Puedes usar las funciones:
*		json_encode(xExpr)	te regresa la cadena Json, que representa al objeto que se pasa como parametro
*		json_decode(cJson)  te regresa el objeto representado en cJson
*
* Tambien puedes usar directamente la clase:
*
*	oJson = newobject('json','json.prg')
*	oCliente = oJson.decode( ' { "nombre":"Ignacio" , "apellido":"Gutierrez", "edad":33 } ')
*	? oJson.encode(oCliente)
*	? oCliente.get('nombre')
*	? oCliente.get('apellido')
*
*
* VFPJSON  Encode and Decode JSON for VFP
* Examples:
*	oJson = newobject('json','json.prg')
*	oCustomer = oJson.decode( ' { "name":"Ignacio" , "lastname":"Gutierrez", "age":33 } ')
*	? oJson.encode(oCustomer)
*	? oCustomer.get('name')
*	? oCustomer.get('lastname')
*
*
lRunTest = .f.
if lRunTest
	testJsonClass()
endif
return


function json_encode(xExpr)
	if vartype(_json)<>'O'
		public _json
		_json = newobject('json')
	endif
return _json.encode(@xExpr)


function json_decode(cJson)
local retval
	if vartype(_json)<>'O'
		public _json
		_json = newobject('json')
	endif
	retval = _json.decode(cJson)
	if not empty(_json.cError)
		return null
	endif
return retval

function json_getErrorMsg()
return _json.cError
	


*
* json class
*
*
define class json as custom


	nPos=0
	nLen=0
	cJson=''
	cError=''


	*
	* Genera el codigo cJson para parametro que se manda
	*
	function encode(xExpr)
	local cTipo
		* Cuando se manda una arreglo, 
		if type('ALen(xExpr)')=='N'
			cTipo = 'A'
		Else
			cTipo = VarType(xExpr)
		Endif
		
		Do Case
		Case cTipo=='D'
			return '"'+dtos(xExpr)+'"'
		Case cTipo=='N'	
			return Transform(xExpr)
		Case cTipo=='L'	
			return iif(xExpr,'true','false')
		Case cTipo=='X'	
			return 'null'
		Case cTipo=='C'
			xExpr = allt(xExpr)
			xExpr = StrTran(xExpr, '\', '\\' )
			xExpr = StrTran(xExpr, '/', '\/' )
			xExpr = StrTran(xExpr, Chr(13), '\n' )
			xExpr = StrTran(xExpr, Chr(10), '\f' )
			xExpr = StrTran(xExpr, '"', '\"' )
			return '"'+xExpr+'"'

		case cTipo=='O'
			local cProp, cJsonValue, cRetVal, aProp[1]
			=AMembers(aProp,xExpr)
			cRetVal = ''
			for each cProp in aProp
				*? cProp
				*? cRetVal
				if type('xExpr.'+cProp)=='U' or cProp=='CLASS'
					* algunas propiedades pueden no estar definidas
					* como: activecontrol, parent, etc
					loop
				endif
				if type( 'ALen(xExpr.'+cProp+')' ) == 'N'
					*
					* es un arreglo, recorrerlo usando los [ ] y eval
					*
					Local i,nTotElem
					cJsonValue = ''
					nTotElem = Eval('ALen(xExpr.'+cProp+')')
					For i=1 to nTotElem
						cJsonValue = cJsonValue + ' , ' +  this.encode( Eval( 'xExpr.'+cProp+'['+Str(i)+']' ))
					Next
					cJsonValue = '[' + substr(cJsonValue,4) + ']'
				else
					*
					* es otro tipo de dato normal C, N, L
					*
					cJsonValue = this.encode( evaluate( 'xExpr.'+cProp ) )				
				endif
				if left(cProp,1)=='_'
					cProp = substr(cProp,2)
				endif
				cRetVal = cRetVal + ',' + '"' + lower(cProp) + '":' + cJsonValue
			next
			return '{' + substr(cRetVal,2) + '}'

		case cTipo=='A'
			local valor, cRetVal
			cRetVal = ''	
			for each valor in xExpr
				cRetVal = cRetVal + ',' +  this.encode( @valor ) 			
			next
			return  '[' + substr(cRetVal,2) + ']'
			
		endcase

	return ''





	*
	* regresa un elemento representado por la cadena json que se manda
	*
	
	function decode(cJson)
	local retValue
		cJson = StrTran(cJson,chr(9),'')
		cJson = StrTran(cJson,chr(10),'')
		cJson = StrTran(cJson,chr(13),'')
		cJson = this.fixUnicode(cJson)
		this.nPos  = 1
		this.cJson = cJson
		this.nLen  = len(cJson)
		this.cError = ''
		retValue = this.parsevalue()
		if not empty(this.cError)
			return null
		endif
		if this.getToken()<>null
			this.setError('Junk at the end of JSON input')
			return null
		endif
	return retValue
		
	
	function parseValue()
	local token
		token = this.getToken()
		if token==null
			this.setError('Nothing to parse')
			return null
		endif
		do case
		case token=='"'
			return this.parseString()
		case isdigit(token) or token=='-'
			return this.parseNumber()
		case token=='n'
			return this.expectedKeyword('null',null)
		case token=='f'
			return this.expectedKeyword('false',.f.)
		case token=='t'
			return this.expectedKeyword('true',.t.)
		case token=='{'
			return this.parseObject()
		case token=='['
			return this.parseArray()
		otherwise
			this.setError('Unexpected token')
		endcase
	return
		
	
	function expectedKeyword(cWord,eValue)
		for i=1 to len(cWord)
			cChar = this.getChar()
			if cChar <> substr(cWord,i,1)
				this.setError("Expected keyword '" + cWord + "'")
				return ''
			endif
			this.nPos = this.nPos + 1
		next
	return eValue
	

	function parseObject()
	local retval, cPropName, xValue
		retval = createObject('myObj')
		this.nPos = this.nPos + 1 && Eat {
		if this.getToken()<>'}'
			do while .t.
				cPropName = this.parseString()
				if not empty(this.cError)
					return null
				endif
				if this.getToken()<>':'
					this.setError("Expected ':' when parsing object")
					return null
				endif
				this.nPos = this.nPos + 1
				xValue = this.parseValue()
				if not empty(this.cError)
					return null
				endif				
				** Debug ? cPropName, type('xValue')
				retval.set(cPropName, xValue)
				if this.getToken()<>','
					exit
				endif
				this.nPos = this.nPos + 1
			enddo
		endif
		if this.getToken()<>'}'
			this.setError("Expected '}' at the end of object")
			return null
		endif
		this.nPos = this.nPos + 1
	return retval


	function parseArray()
	local retVal, xValue
		retval = createObject('MyArray')
		this.nPos = this.nPos + 1	&& Eat [
		if this.getToken() <> ']'
			do while .t.
				xValue = this.parseValue()
				if not empty(this.cError)
					return null
				endif
				retval.add( xValue )
				if this.getToken()<>','
					exit
				endif
				this.nPos = this.nPos + 1
			enddo
			if this.getToken() <> ']'
				this.setError('Expected ] at the end of array')
				return null
			endif
		endif
		this.nPos = this.nPos + 1
	return retval
	

	function parseString()
	local cRetVal, c
		if this.getToken()<>'"'
			this.setError('Expected "')
			return ''
		endif
		this.nPos = this.nPos + 1 	&& Eat "
		cRetVal = ''
		do while .t.
			c = this.getChar()
			if c==''
				return ''
			endif
			if c == '"'
				this.nPos = this.nPos + 1
				exit
			endif
			if c == '\'
				this.nPos = this.nPos + 1
				if (this.nPos>this.nLen)
					this.setError('\\ at the end of input')
					return ''
				endif
				c = this.getChar()
				if c==''
					return ''
				endif
				do case
				case c=='"'
					c='"'
				case c=='\'
					c='\'
				case c=='/'
					c='/'
				case c=='b'
					c=chr(8)
				case c=='t'
					c=chr(9)
				case c=='n'
					c=chr(10)
				case c=='f'
					c=chr(12)
				case c=='r'
					c=chr(13)
				otherwise
					******* FALTAN LOS UNICODE
					this.setError('Invalid escape sequence in string literal')
					return ''
				endcase
			endif
			cRetVal = cRetVal + c
			this.nPos = this.nPos + 1
		enddo
	return cRetVal
					

	**** Pendiente numeros con E
	function parseNumber()
	local nStartPos,c, isInt, cNumero
		if not ( isdigit(this.getToken()) or this.getToken()=='-')
			this.setError('Expected number literal')
			return 0
		endif
		nStartPos = this.nPos
		c = this.getChar()
		if c == '-'
			c = this.nextChar()
		endif
		if c == '0'
			c = this.nextChar()
		else
			if isdigit(c)
				c = this.nextChar()
				do while isdigit(c)
					c = this.nextChar()
				enddo
			else
				this.setError('Expected digit when parsing number')
				return 0
			endif
		endif
		
		isInt = .t.
		if c=='.'
			c = this.nextChar()
			if isdigit(c)
				c = this.nextChar()
				isInt = .f.
				do while isDigit(c)
					c = this.nextChar()
				enddo
			else
				this.setError('Expected digit following dot comma')
				return 0
			endif
		endif
		
		cNumero = substr(this.cJson, nStartPos, this.nPos - nStartPos)
	return val(cNumero)



	function getToken()
	local char1
		do while .t.
			if this.nPos > this.nLen
				return null
			endif
			char1 = substr(this.cJson, this.nPos, 1)
			if char1==' '
				this.nPos = this.nPos + 1
				loop
			endif
			return char1
		enddo
	return
	
		
		
	function getChar()
		if this.nPos > this.nLen
			this.setError('Unexpected end of JSON stream')
			return ''
		endif
	return substr(this.cJson, this.nPos, 1)
	
	function nextChar()
		this.nPos = this.nPos + 1
		if this.nPos > this.nLen
			return ''
		endif
	return substr(this.cJson, this.nPos, 1)
	
	function setError(cMsg)
		this.cError= 'ERROR parsing JSON at Position:'+allt(str(this.nPos,6,0))+' '+cMsg
	return 


	function fixUnicode(cStr)
		cStr = StrTran(cStr,'\u00e1','�')
		cStr = StrTran(cStr,'\u00e9','�')
		cStr = StrTran(cStr,'\u00ed','�')
		cStr = StrTran(cStr,'\u00f3','�')
		cStr = StrTran(cStr,'\u00fa','�')
		cStr = StrTran(cStr,'\u00c1','�')
		cStr = StrTran(cStr,'\u00c9','�')
		cStr = StrTran(cStr,'\u00cd','�')
		cStr = StrTran(cStr,'\u00d3','�')
		cStr = StrTran(cStr,'\u00da','�')
		cStr = StrTran(cStr,'\u00f1','�')
		cStr = StrTran(cStr,'\u00d1','�')
	return cStr



enddefine





* 
* class used to return an array
*
define class myArray as custom
	nSize = 0
	dimension array[1]

	function add(xExpr)
		this.nSize = this.nSize + 1
		dimension this.array[this.nSize]
		this.array[this.nSize] = xExpr
	return

	function get(n)
	return this.array[n]

enddefine



*
* class used to simulate an object
* all properties are prefixed with 'prop' to permit property names like: error, init
* that already exists like vfp methods
*
define class myObj as custom
Hidden ;
	ClassLibrary,Comment, ;
	BaseClass,ControlCount, ;
	Controls,Objects,Object,;
	Height,HelpContextID,Left,Name, ;
	Parent,ParentClass,Picture, ;
	Tag,Top,WhatsThisHelpID,Width
		
	function set(cPropName, xValue)
		cPropName = '_'+cPropName
		if type('this.'+cPropName)=='U'
			this.addProperty(cPropName,xValue)
		else
			local cmd
			cmd = 'this.'+cPropName+'=xValue'
			&cmd
		endif
	return
	
	procedure get (cPropName)
		cPropName = '_'+cPropName
		If type('this.'+cPropName)=='U'
			return ''
		Else
			local cmd
			cmd = 'return this.'+cPropName
			&cmd
		endif
	return ''
enddefine





function testJsonClass
	clear
	set decimal to 10
	oJson = newObject('json')
	
	
	? 'Test Basic Types'
	? '----------------'
	? oJson.decode('null')
	? oJson.decode('true')
	? oJson.decode('false')
	?
	? oJson.decode('791123')
	? oJson.decode('791123.45')
	? oJson.decode('791123.45.')
	? oJson.decode('"nacho gtz"')
	if not empty(oJson.cError)
		? oJson.cError
		return
	endif
	? oJson.decode('"nacho gtz\nEs \"bueno\"\nMuy Bueno\ba"')
	if not empty(oJson.cError)
		? oJson.cError
		return
	endif
	
	? 'Test Array'
	? '----------'
	arr = oJson.decode('[3.1416,"Ignacio",false,null]')
	? arr.get(1), arr.get(2), arr.get(3), arr.get(4)
	arr = oJson.decode('[ ["Hugo","Paco","Luis"] , [ 8,9,11] ] ')
	nombres = arr.get(1)
	edades  = arr.get(2)
	? nombres.get(1), edades.get(1)
	? nombres.get(2), edades.get(2)
	? nombres.get(3), edades.get(3)
	?
	? 'Test Object'
	? '-----------'
	obj = oJson.decode('{"nombre":"Ignacio", "edad":33.17, "isGood":true}')
	? obj.get('nombre'), obj.get('edad'), obj.get('isGood')
	? obj._Nombre, obj._Edad, obj._IsGood
	obj = oJson.decode('{"jsonrpc":"1.0", "id":1, "method":"sumArray", "params":[3.1415,2.14,10],"version":1.0}')
	? obj.get('jsonrpc'), obj._jsonrpc
	? obj.get('id'), obj._id
	? obj.get('method'), obj._method
	? obj._Params.array[1], obj._Params.get(1)

	?
	? 'Test nested object'
	? '------------------'
	cJson = '{"jsonrpc":"1.0", "id":1, "method":"upload", "params": {"data":{ "usrkey":"288af77b", "sendto":"ignacio@sait.com.mx", "name":"Ignacio is \"Nacho\"","expires":"20120731" }}}'
	obj = oJson.decode(cJson)
	if not empty(oJson.cError)
		? oJson.cError
		return
	endif
	? cJson
	? 'method -->',obj._method
	? 'usrkey -->',obj._params._data._usrkey
	? 'sendto -->',obj._params._data._sendto
	? 'name  --->',obj._params._data._name
	? 'expires ->',obj._params._data._expires

	?
	? 'Test empty object'
	? '-----------------'
	cJson = '{"result":null,"error":{"code":-3200.012,"message":"invalid usrkey","data":{}},"id":"1"}'
	obj = oJson.decode(cJson)
	if not empty(oJson.cError)	
		? oJson.cError
		return
	endif
	? cJson
	? 'result -->',obj._result, obj.get('result')
	oError = obj.get('error')
	? 'ErrCode ->',obj._error._code, oError.get('code')
	? 'ErrMsg -->',obj._error._message, oError.get('message')
	? 'id  ----->',obj._id, obj.get('id')
	?  type("oError._code")

	?
	? 'Probar decode-enconde-decode-encode'
	? '------------------------------------'
	cJson = ' {"server":"", "user":"", "password":"" ,'+;
			' "port":0, "auth":false, "ssl":false, "timeout":20, "error":404}' 
	? cJson
	oSmtp = json_decode(cJson)
	cJson =  json_encode(oSmtp)
	? cJson
	oSmtp = json_decode(cJson)
	cJson =  json_encode(oSmtp)
	? cJson

	* Probar falla
	? 
	? 'Probar una falla en el json'
	? '---------------------------'
	cJson = ' {"server":"", "user":"", "password":"" ,'
	oSmtp = json_decode(cJson)
	if not empty(json_getErrorMsg())
		? json_getErrorMsg()
	endif

	?
	? 'Pruebas Finalizadas'
return

