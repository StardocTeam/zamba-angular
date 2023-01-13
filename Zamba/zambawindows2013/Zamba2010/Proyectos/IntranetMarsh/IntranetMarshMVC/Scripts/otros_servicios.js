function OtrosServicios_Resultado(responseText)
{
    var resul = responseText.get_data();
    var msj;
    
     hideLoading();
    
    if(resul)
        msj = "Su pedido ha sido enviado con &eacute;xito";
    else
        msj = "Se produjo un error al enviar su pedido";
    
    $('#div_form_pedido').css('display', 'none');
    
    $('#mensaje_envio_pedido').css('display', '');
    $('#mensaje_envio_pedido').html(msj);
}

function ajaxValidate() 
{
    var resul = $('form').validate().form();
   
    if(resul)
        showLoading();
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
