function DatosUsuario()
{   
    //obtiene el usuario seleccionado
    var usuario = $('#usuario').val();
    var estado = '';
    
    //si no hay usuario los controles van a disabled=true
    if(usuario == '')    
        estado = 'true';
    
    // vaciar los inputs
    $('#cargo').val('');
    $('#sector').val('');
    $('#telefono').val('');
    $('#email').val('');

    // activar o desactivar los inputs segun corresponda
    $('#cargo').attr('disabled', estado); 
    $('#sector').attr('disabled', estado); 
    $('#telefono').attr('disabled', estado); 
    $('#email').attr('disabled', estado); 
    $('#submit').attr('disabled', estado);
    
    if(usuario != '')
        TraerDatosUsuario(usuario);
}

function TraerDatosUsuario(usuario)
{
    // poner la imagen de espera
    $('#img_loading').css('display', '');

    // Traer por Ajax los datos
    $.getJSON("getUserData", {nombre_apellido:usuario}, FillUserData);
}

function FillUserData(data)
{
    // cargar los controles con los datos obtenidos
    $('#cargo').val(data.Cargo);
    $('#sector').val(data.Sector);
    $('#telefono').val(data.Interno);
    $('#email').val(data.Email);
    
    // sacar la imagen de espera
    $('#img_loading').css('display', 'none');
}

function Tarjetas_Resultado(responseText)
{
    var resul = responseText.get_data();
    var msj;
    
    hideLoading();
    
    if(resul)
        msj = "Su solicitud ha sido enviada con &eacute;xito";
    else
        msj = "Se produjo un error al enviar su solicitud";
    
    $('#div_form_pedido').css('display', 'none');
    
    $('#mensaje_envio_pedido').css('display', '');
    $("#mensaje_envio_pedido").html(msj);
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
			sector: "required",
			cargo: "required",			
			email: {
				required: true,
				email: true
			},
			telefono: "required"
		},
		messages: 
		{
			cargo: "<br>Por favor ingrese su cargo",
            sector: "<br>Por favor ingrese su sector",
            telefono: "<br>Por favor ingrese su telefono",			
			email: "<br>Por favor ingrese una direcci&oacute;n de email v&aacute;lida"
		}
	});	
});