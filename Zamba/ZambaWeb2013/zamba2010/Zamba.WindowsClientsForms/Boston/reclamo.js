function CalcularEstimacion(){
	if($('#zamba_index_2825').val() == 2) {
		var danMat = FormatMoney($('#zamba_index_2792').val());
		
		if(danMat > 0) {
			var tipoResp = $('#zamba_index_2718').val();
			var estimacion;
			
			switch(tipoResp)
			{
				case '0': //Sin responsabilidad
					estimacion = 0;
					break;
				case '1': //Con responsabilidad
					estimacion = FormatMoney($('#zamba_index_2792').val());
					break;
				case '2': //Con concurrencia
					estimacion = FormatMoney($('#zamba_index_2792').val()) / 2;
					break;
			}

			$('#zamba_index_2771').val(estimacion);
		}
	}
}
function FormatMoney(m){
	if (m == '') 
		return 0;
	else
		return parseFloat(m.replace(',', '.'));
}
function HabilitarLesiones(){
	if($('#zamba_index_2825').val() == 1){
		$('#zamba_index_2831').removeAttr("disabled");
		$('#zamba_index_2832').removeAttr("disabled");
		$('#zamba_index_2833').removeAttr("disabled");
	} else {
		$('#zamba_index_2831').val('N');
		$('#zamba_index_2832').val('');
		$('#zamba_index_2833').val('');
		$('#zamba_index_2831').attr('disabled', 'disabled');
		$('#zamba_index_2832').attr('disabled', 'disabled');
		$('#zamba_index_2833').attr('disabled', 'disabled');
	}
}
function VerificarLesiones(){
	if($('#zamba_index_2825').val() == 1 && $('#zamba_index_2831').val() == "S"){
		$('#zamba_index_2832').removeAttr("disabled");
		$('#zamba_index_2833').removeAttr("disabled");
	}
	else{
		$('#zamba_index_2832').val('');
		$('#zamba_index_2833').val('');
		$('#zamba_index_2832').attr('disabled', 'disabled');
		$('#zamba_index_2833').attr('disabled', 'disabled');
	}
}
function VerificarHabilitacionSiniestro(){
	if($("#zamba_index_2695").val() != null && $("#zamba_index_2695").val() != ""){
		$('#zamba_index_2749').attr('disabled', 'disabled');
		$('#zamba_index_2736').attr('disabled', 'disabled');
		$('#zamba_index_1147').attr('disabled', 'disabled');
		$('#zamba_index_2694').attr('disabled', 'disabled');
		$('#zamba_index_14').attr('disabled', 'disabled');
		$('#zamba_rule_10474').attr('disabled', 'disabled');
		$('#zamba_rule_9123').attr('disabled', 'disabled');
	} else {
		$('#zamba_index_2749').removeAttr("disabled");
		$('#zamba_index_2736').removeAttr("disabled");
		$('#zamba_index_1147').removeAttr("disabled");
		$('#zamba_index_2694').removeAttr("disabled");
		$('#zamba_index_14').removeAttr("disabled");
		$('#zamba_rule_10474').removeAttr("disabled");
		$('#zamba_rule_9123').removeAttr("disabled");
	}
}
function VerificarHabilitacionRechazo(){
	if($('#zamba_index_2742').val() == "S"){
		$('#zamba_index_1000002').removeAttr("disabled");
	} else {
		$('#zamba_index_1000002').attr('disabled', 'disabled');
	}
}
function CargarSiniestro(o){
	//Se asegura de que todos los datos se encuentren correctos y guardados.
	if (document.getElementById("zamba_index_2736").value == "" || document.getElementById("zamba_index_2736").value == "0"){
		alert("Por favor, verifique el Nro de Ejercicio antes de continuar");
		document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
	} else if (document.getElementById("zamba_index_1147").value == "" || document.getElementById("zamba_index_1147").value == "0"){
		alert("Por favor, verifique el Nro de Siniestro antes de continuar");
		document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
	} else {
		document.getElementById("hdnRuleId").name = "zamba_rule_save";
		frmMain.submit();
		SetRuleId(o);
	}
}
/* FUNCION OBSOLETA DE VERIFICACION DEL BAREMO. 
 * SE DEJA COMO BKP POR CAMBIOS CONSTANTES EN LAS DEFINICIONES.
function VerificarBaremo(){
	if($('#zamba_index_2825').val() == 2) {
		var danMat = FormatMoney($('#zamba_index_2792').val());
		var oldEstima = FormatMoney($('#zamba_index_2771').val());
		var incorrecto = false;
		
		if(danMat == 0) {
			incorrecto = true;
		}
		if(danMat > oldEstima) {
			incorrecto = true;
		}
		
		if(incorrecto) {
			alert('Verifique que el daño material sea mayor a cero y que no supere a la Estimación');
		}
	}
}
*/
