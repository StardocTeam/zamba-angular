function Desperfectos_Resultado(responseText)
{
    var resul = responseText.get_data();
    var msj;
    
    hideLoading();
    
    if(resul)
        msj = "Su mensaje ha sido enviado con &eacute;xito";
    else
        msj = "Se produjo un error al enviar su mensaje";
    
    $('#div_form_desperfectos').css('display', 'none');
    
    $('#mensaje_envio_desperfecto').css('display', '');
    $("#mensaje_envio_desperfecto").html(msj);
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
			sector: "required",
			interno: "required",			
			email: {
				required: true,
				email: true
			},
			lugar: "required",
			descripcion: "required"
		},
		messages: 
		{
			usuario: "<br>Por favor ingrese su nombre y apellido",
            sector: "<br>Por favor ingrese su sector",
            interno: "<br>Por favor ingrese su interno",			
			email: "<br>Por favor ingrese una direcci&oacute;n de email v&aacute;lida",
            lugar: "<br>Por favor ingrese el lugar donde se produjo el desperfecto",			
            descripcion: "<br>Por favor ingrese una descripci&oacute;n del problema"
		}
	});	
});