var ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";
var serviceBaseAndreani = ZambaWebRestApiURL + "/andreaniServices/";
var link_etiqueta = "";
var link_tracking = "";

function InicializarPestaniasAndreani() {
    if (EsOrigenR3()) {
        $(".andreani").show();
        if (TieneOrdenCreada()) {

            ActualizarCodigoBarra();
            setTimeout(function () {
                ActualizarInformacionEnvio($('#zamba_index_150682').val());
                ActualizarInformacionLinks();
            }, 1000);
        }
        else {
            var mensaje = "No existe una orden de envio."
            $("#zamba_index_150726").val(mensaje);
            $("#zamba_index_150727").val(mensaje);
            $("#zamba_index_150728").val(mensaje);
            $("#Actualizar_Informacion_Envio").hide();
        }
    }
	
	try{

	if ($('#zamba_index_150682').val() == '' && $('#zamba_index_139587').val() == 'R3')
	{
		$('#REEnviarOrdenAndreani').show();
			}
	}
	catch(error){
		
		
	}
    
}

function confirmarGuia(obj) {
    try {
        if (EsOrigenR3()) {
                var nro_guia = $("#zamba_index_139614").val();
                var respuesta = CrearNuevaOrden(nro_guia);
                if (respuesta.responseText != null) {
                    var mensaje = JSON.parse(respuesta.responseText);
                    if (mensaje.ExceptionMessage != null) {
                        swal("Se ha producido un error mientras intentaba finalizar la guia de despacho.")
                        return true;
                    }
                }
                if (respuesta.error != null) {
                    swal("Se produjo un error al intentar crear el envio (" + respuesta.error + ")")
                    return true;
                }
            }
        SetRuleId(obj);
        return true;
    }
    catch (error) {
        swal("Se produjo un error.");
        return false;
    }

}
function CrearNuevaOrden(nro_guia) {
    var requestNuevaOrden = {
        nro_guia: nro_guia,
        userId: GetUID()
    };
    var ret;
    $.ajax({
        url: serviceBaseAndreani + "CrearNuevaOrden",
        type: "POST",
        data: JSON.stringify(requestNuevaOrden),        
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            ret = response;
            $("#zamba_index_150682").val(response.numeroDeTracking);
	},
        error: function (error) {
            $("#zamba_index_150682").val(response.numeroDeTracking);

        }
    });
    return ret;
}
function ActualizarInformacionEnvio(nro_tracking) {
    var requestTracking = {
        userId: GetUID(),
        nro_tracking: nro_tracking
    };
    $.ajax({
        type: "POST",
        url: serviceBaseAndreani + "ActualizarInformacionEnvio",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(requestTracking),
        success: function (response) {
            if (response.error != null) {
                swal(response.error);
                return;
            }
            $("#zamba_index_150726").val(response.NotificacionOrdenEnvio);
            $("#zamba_index_150727").val(response.NotificacionEnvio);
            $("#zamba_index_150728").val(response.NotificacionTracking);
            LoadGrillaForm('149105');
            LoadGrillaForm('149092');
            LoadGrillaForm('149093');
        },
        error: function (error) {
            swal("No se pudo actualizar la informacion de envío")
            return error
        }
    });
    return false;
}
function ActualizarInformacionLinks() {
    var requestLinks = {
        userId: GetUID(),
        nro_guia: $("#zamba_index_139614").val()
    };
    $.ajax({
        type: "POST",
        url: serviceBaseAndreani + "ObtenerLinking",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(requestLinks),
        success: function (response) {
            $(response).each(function (index, obj) {
                if (obj.meta == "@tracking")
                    link_tracking = obj.contenido;
                if (obj.meta == "@etiqueta")
                    link_etiqueta = obj.contenido;
            });
        },
        error: function (error) {

        }
    });
}

function ActualizarCodigoBarra() {
    var nro_tracking = $("#zamba_index_150682").val();
    $("#nro_tracking_code_bar")[0].src = "https://developers.andreani.com/herramientas/barcode?codigo=" + nro_tracking + "&height=30";
}

function click_button_linking_andreani(tipo_link) {
    var link_abrir;
    if (tipo_link == "tracking")
        link_abrir = link_tracking
    else
        link_abrir = link_etiqueta
    window.open(link_abrir, '_blank');
    return false;
}

function TieneOrdenCreada() {
    var nro_tracking = $("#zamba_index_150682").val();
    return  (nro_tracking != null && !nro_tracking== "")
}
function EsOrigenR3() {
    if ($("#zamba_index_139587").val() != null)
        return ($("#zamba_index_139587").val().substring(0, 2) == "R3")
    return false;
}