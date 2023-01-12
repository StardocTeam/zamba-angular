function Comunicandonos_Resultado(responseText)
{
    var resul = responseText.get_data();
    var msj;
    
    hideLoading();
        
    if(resul)
        msj = "Su mensaje ha sido enviado con &eacute;xito";
    else
        msj = "Se produjo un error al enviar su mensaje";
    
    $('#mensaje_envio').css('display', '');
    $('#mensaje_envio').html(msj);
}

function ajaxValidate() 
{
    var resul = $('form').validate().form();
   
    if(resul)
    {
        $('#div_form_comunicandonos').css('display', 'none');
        showLoading();
    }
    else
        hideLoading();
        
    return resul;
}

// Validacion
$().ready(function() 
{
	// validate signup form on keyup and submit
	$('form').validate({
		rules: 
		{
		    usuario: "required",
	    	mensaje: "required"
		},
		messages: 
		{
		    usuario: "<br>Por favor seleccione su nombre y apellido",
            mensaje: "<br>Por favor ingrese su mensaje"
		}
	});	
});