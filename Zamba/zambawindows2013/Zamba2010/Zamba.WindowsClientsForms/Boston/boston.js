/*
 *	Funciones genéricas para los formularios de Boston.
 */

function SetRuleId(sender){
	document.getElementById("hdnRuleId").name = sender.id;               
	frmMain.submit();
}
function SetAsocId(sender){
	document.getElementById("hdnAsocId").name = sender.id;
	frmMain.submit();
}
function CheckMaxLength(Object){
	var len = parseInt(Object.getAttribute("maxlength"), 10); 
	return (Object.value.length < len);
}
function CheckMaxLengthAfterLosingFocus(Object){
	var len = parseInt(Object.getAttribute("maxlength"), 10); 
	if (Object.value.length > len){
		alert("Se ha superado el límite máximo de " + len + " caracteres.\Se truncará dicho texto hasta el límite.");
		Object.value = Object.value.substring(0, len);
	}
}
function DecimalCheck(obj){
	//Verifica que un numero sea decimal de 2 posiciones
    var RE = /^\d*\.?\d{0,2}$/;
    if(RE.test(obj.val())){
		obj.removeClass("error");
        return true;
    }else{
		obj.addClass("error"); 
        return false;
    }
}
function IntegerCheck(e){
	//Verifica que el número sea un entero
	if(e.which!=8 && e.which!=0 && (e.which<48 || e.which>57))
		return false;	
}
function PorcentualCheck(obj){
	//Verifica un numero porcentual de 2 decimales
    var RE = /^\d*\.?\d{0,2}$/;
	var value = obj.val();
    if(value >= 0 && value <= 100 && RE.test(value)){
		obj.removeClass("error");
        return true;
    }else{
		obj.addClass("error"); 
        return false;
    }
}
function Redondear(num) {
	//Redondea a 2 dígitos después de la coma
	return Math.round(num*100)/100;
}
String.prototype.trim = function () {
	return this.replace(/^\s*/, "").replace(/\s*$/, "");
}
function CargarFechaIndice(indice){
	$('#' + indice).datepicker({
		changeMonth: true,
		changeYear: true,
		showOn: 'both', 
		buttonText: '',
		buttonImageOnly: true,
		duration: ""
	});
}
function esDigito(sChr){
	var sCod = sChr.charCodeAt(0);
	return ((sCod > 47) && (sCod < 58));
}
function valSep(oTxt){
	var sep;
	//Busca algún caracter separador y lo guarda (acá podrían agregarse nuevos formatos)
	if(oTxt.value.indexOf('/') > -1){
		sep = '/';
	} else if(oTxt.value.indexOf('-') > -1){
		sep = '-';
	}
	//Verifica que haya encontrado algo
	if(sep.length > 0){
		//Verifica que haya más de un separador
		if(oTxt.value.indexOf(sep) != oTxt.value.lastIndexOf(sep)){
			//Verifica que existan dos digitos antes del primer separador
			if(oTxt.value.indexOf(sep) != 2){
				//En caso de no existir agrega un cero delante
				oTxt.value = "0" + oTxt.value;
				//Verifica nuevamente los dos dígitos
				if(oTxt.value.indexOf(sep) != 2){
					//Si pasa por acá es porque tenía más de 2 dígitos de entrada
					return false;
				}
			}
			//Verifica lo mismo pero con el siguiente separador
			if(oTxt.value.lastIndexOf(sep) != 5){
				var i = oTxt.value.substring(0,3);
				var f = oTxt.value.substring(3);
				oTxt.value = i + "0" + f;
				if(oTxt.value.lastIndexOf(sep) != 5){
					return false;
				}
			}
			return true;
		}
	} else {
		alert("Los días, meses y años deben estar separados por el caracter / o el -");
		return false;
	}
}
function finMes(oTxt){
	var nMes = parseInt(oTxt.value.substr(3, 2), 10);
	var nRes = 0;
	switch (nMes)
	{
		case 1: nRes = 31; break;
		case 2: nRes = 29; break;
		case 3: nRes = 31; break;
		case 4: nRes = 30; break;
		case 5: nRes = 31; break;
		case 6: nRes = 30; break;
		case 7: nRes = 31; break;
		case 8: nRes = 31; break;
		case 9: nRes = 30; break;
		case 10: nRes = 31; break;
		case 11: nRes = 30; break;
		case 12: nRes = 31; break;
	}
	return nRes;
}
function valDia(oTxt){
	var bOk = false;
	var nDia = parseInt(oTxt.value.substr(0, 2), 10);
	bOk = bOk || ((nDia >= 1) && (nDia <= finMes(oTxt)));
	return bOk;
}
function valMes(oTxt){
	var bOk = false;
	var nMes = parseInt(oTxt.value.substr(3, 2), 10);
	bOk = bOk || ((nMes >= 1) && (nMes <= 12));
	return bOk;
}
function valAno(oTxt){
	var bOk = true;
	var nAno = oTxt.value.substr(6);
	bOk = bOk && ((nAno.length == 2) || (nAno.length == 4));
	if (bOk)
	{
		for (var i = 0; i < nAno.length; i++)
		{
			bOk = bOk && esDigito(nAno.charAt(i));
		}
	}
	return bOk;
}
function valFecha(oTxt){
	var bOk = true;
	if (oTxt.value != "")
	{
		bOk = bOk && (valSep(oTxt));
		bOk = bOk && (valAno(oTxt));
		bOk = bOk && (valMes(oTxt));
		bOk = bOk && (valDia(oTxt));
		
		if (!bOk)
		{
			alert("La fecha ingresada es invalida");
			oTxt.focus();
		}
	}
}
//Método para sumar n dias hábiles.
Date.prototype.SumarLaborables = function(n) { 		
	for(var i=0; i < n; i++) { 
		this.setTime( this.getTime() + 24*60*60*1000 ); 
		//Si es sábado o domingo se cuenta otro día. 
		if( (this.getDay()==6) || (this.getDay()==0) )    
			i--;  
	} 
	return this; 
} 
//Suma a una fecha determinada X cantidad de días hábiles y devuelve la fecha calculada.
function CalcularLaborales(n, fecIni){	
	var dd = GetDD(fecIni);
	var mm = GetMM(fecIni);
	var yy = GetYY(fecIni);
	var fec = new Date(yy,mm,dd);
	return fec.SumarLaborables(n).format("d/m/Y");
}
Date.prototype.format = function(format) {
	var returnStr = '';
	var replace = Date.replaceChars;
	for (var i = 0; i < format.length; i++) {
		var curChar = format.charAt(i);
		if (i - 1 >= 0 && format.charAt(i - 1) == "\\") { 
			returnStr += curChar;
		}
		else if (replace[curChar]) {
			returnStr += replace[curChar].call(this);
		} else if (curChar != "\\"){
			returnStr += curChar;
		}
	}
	return returnStr;
}; 
Date.replaceChars = {
	shortMonths: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
	longMonths: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
	shortDays: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
	longDays: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
	
	//Day
	d: function() { return (this.getDate() < 10 ? '0' : '') + this.getDate(); },
	D: function() { return Date.replaceChars.shortDays[this.getDay()]; },
	j: function() { return this.getDate(); },
	l: function() { return Date.replaceChars.longDays[this.getDay()]; },
	N: function() { return this.getDay() + 1; },
	S: function() { return (this.getDate() % 10 == 1 && this.getDate() != 11 ? 'st' : (this.getDate() % 10 == 2 && this.getDate() != 12 ? 'nd' : (this.getDate() % 10 == 3 && this.getDate() != 13 ? 'rd' : 'th'))); },
	w: function() { return this.getDay(); },
	z: function() { var d = new Date(this.getFullYear(),0,1); return Math.ceil((this - d) / 86400000); }, //Fixed now
	//Week
	W: function() { var d = new Date(this.getFullYear(), 0, 1); return Math.ceil((((this - d) / 86400000) + d.getDay() + 1) / 7); }, //Fixed now
	//Month
	F: function() { return Date.replaceChars.longMonths[this.getMonth()]; },
	m: function() { return (this.getMonth() < 9 ? '0' : '') + (this.getMonth() + 1); },
	M: function() { return Date.replaceChars.shortMonths[this.getMonth()]; },
	n: function() { return this.getMonth() + 1; },
	t: function() { var d = new Date(); return new Date(d.getFullYear(), d.getMonth(), 0).getDate() }, //Fixed now, gets #days of date
	//Year
	L: function() { var year = this.getFullYear(); return (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0)); },	//Fixed now
	o: function() { var d  = new Date(this.valueOf());  d.setDate(d.getDate() - ((this.getDay() + 6) % 7) + 3); return d.getFullYear();}, //Fixed now
	Y: function() { return this.getFullYear(); },
	y: function() { return ('' + this.getFullYear()).substr(2); },
	//Time
	a: function() { return this.getHours() < 12 ? 'am' : 'pm'; },
	A: function() { return this.getHours() < 12 ? 'AM' : 'PM'; },
	B: function() { return Math.floor((((this.getUTCHours() + 1) % 24) + this.getUTCMinutes() / 60 + this.getUTCSeconds() / 3600) * 1000 / 24); }, //Fixed now
	g: function() { return this.getHours() % 12 || 12; },
	G: function() { return this.getHours(); },
	h: function() { return ((this.getHours() % 12 || 12) < 10 ? '0' : '') + (this.getHours() % 12 || 12); },
	H: function() { return (this.getHours() < 10 ? '0' : '') + this.getHours(); },
	i: function() { return (this.getMinutes() < 10 ? '0' : '') + this.getMinutes(); },
	s: function() { return (this.getSeconds() < 10 ? '0' : '') + this.getSeconds(); },
	u: function() { return "Not Yet Supported"; },
	//Timezone
	e: function() { return "Not Yet Supported"; },
	I: function() { return "Not Yet Supported"; },
	O: function() { return (-this.getTimezoneOffset() < 0 ? '-' : '+') + (Math.abs(this.getTimezoneOffset() / 60) < 10 ? '0' : '') + (Math.abs(this.getTimezoneOffset() / 60)) + '00'; },
	P: function() { return (-this.getTimezoneOffset() < 0 ? '-' : '+') + (Math.abs(this.getTimezoneOffset() / 60) < 10 ? '0' : '') + (Math.abs(this.getTimezoneOffset() / 60)) + ':00'; }, //Fixed now
	T: function() { var m = this.getMonth(); this.setMonth(0); var result = this.toTimeString().replace(/^.+ \(?([^\)]+)\)?$/, '$1'); this.setMonth(m); return result;},
	Z: function() { return -this.getTimezoneOffset() * 60; },
	//Full Date/Time
	c: function() { return this.format("Y-m-d\\TH:i:sP"); }, //Fixed now
	r: function() { return this.toString(); },
	U: function() { return this.getTime() / 1000; }
};
function GetDD(fec) {
	return parseInt(fec.substr(0, 2), 10);
}
function GetMM(fec) {
	return (parseInt(fec.substr(3, 2), 10) - 1);
}
function GetYY(fec) {
	return parseInt(fec.substr(6));
}
function comprobarSiBisisesto(anio){
	if ( ( anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) 
	{
		return true;
	}
	else 
	{
		return false;
	}
}