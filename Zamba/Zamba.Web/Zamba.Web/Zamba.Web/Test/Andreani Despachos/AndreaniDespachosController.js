
var ZambaWebRestApiURL = location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/api";
var serviceBaseAndreani = ZambaWebRestApiURL + "/andreaniServices/";
function TestAsmx(key) {
    var pathName = null;
    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": "../../Services/ViewsService.asmx/getValueFromWebConfig?key=" + key,
        "method": "GET",
        "headers": {
            "cache-control": "no-cache"
        },
        "success": function (response) {
            

        },
        "error": function (data, status, headers, config) {
            console.log(data);
        }
    });
    return pathName;
}

$(document).ready(function () {

    ;
    $("#login").click(function (n) {
        var userID = $("#user").val()
        var Password = $("#password").val()
        var respuesta = login(userID, Password);
        $("#respuesta_login").val(JSON.stringify(respuesta));
    }
    );
    TestAsmx("/api");
    

    $("#listar_provincias").click(function (n) {
        var Provincias = ObtenerProvincias();
        $.each(Provincias, function (index, item) {
            $('#items_provincia').append(
                $('<option></option>').val(item.value).html(item.text)
            );
        });
        $("#respuesta_provincias_sucursales").val(JSON.stringify(Provincias));

    })
    $("#test").click(function (n) {
        Test();
    });
    $("#nueva_orden").click(function (n) {
        var nro_guia = $("#nro_guia").val()
        var respuesta = CrearNuevaOrden(nro_guia);
        $("#respuesta_orden").val(JSON.stringify(respuesta));
    });
    $("#obtenerorden").click(function (n) {
        var nro_orden = $("#envio_id").val()
        var respuesta = ObtenerOrdenCreada(nro_orden);
        $("#respuesta_orden").val(JSON.stringify(respuesta));
    });
    $("#obtener_envio").click(function (n) {
        var envio_id = $("#envio_id").val()
        var respuesta = ObtenerEnvio(envio_id);
        $("#respuesta_envio").val(JSON.stringify(respuesta));
    });
    $("#obtener_envio_trazas").click(function (n) {
        var envio_id = $("#envio_id").val()
        var respuesta = ObtenerTrazaEnvio(envio_id);
        $("#respuesta_envio").val(JSON.stringify(respuesta));
    });
    //obtener_envio_trazas
    //$("#buscar_envio").click(function (n) {
    //    var codigo_cliente = $("#CodigoCliente");
    //    var IdProducto = $("#IdProducto");
    //    var NroDocumentoDestinatario = $("#NroDocDestinatario");
    //    var FechaDesdeCreacion = $("#FechaCreacionDesde");
    //    var FechaHastaCreacion = $("#FechaCreacionHasta");
    //    var respuesta = BuscarEnvio(codigo_cliente, IdProducto, NroDocumentoDestinatario, FechaDesdeCreacion, FechaHastaCreacion)
    //    $("#respuesta_envio").val(respuesta);
    //});

    /// seleccionar sucursal inicio ///
    $("#listar_sucursales").click(function (n) {
        //CrearDropDownListProvinciasSucursales();
        ListarSucursales();
    });
    $("#" + IDListaProvincia).change(function (n) {
        SeleccionProvincia();
    }
    );

    /// seleccionar sucursal corta ///
});

/// seleccionar sucursal continua ///
var ListaDeProvincias = {};
var ListaSucursales;
var IDListaSucursales = "items_sucursales";
var IDListaProvincia = "items_provincia";
function CrearDropDownListProvinciasSucursales() {
    var respuesta = ObtenerSucursales();
    ListaSucursales = respuesta;
    ListaDeProvincias = ObtenerLaListaDeProvincias(ListaSucursales);
    $('#' + IDListaProvincia).children().remove().end()
    $.each(ListaDeProvincias, function (index, item) {
        $('#' + IDListaProvincia).append(
            $('<option></option>').val(item).html(item)
        );
    });
    $("#respuesta_provincias_sucursales").val(JSON.stringify(respuesta));
}

function ActualizarInformacionEnvio(nro_tracking) {
    var sucursales = [];
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
            sucursales = response;
        },
        error: function (error) {
            return error
        }
    });
    return sucursales;
}
function ObtenerSucursales() {
    var sucursales = [];
    var requestListaSucursales = {
        userId: GetUID()
    };
    $.ajax({
        type: "POST",
        url: serviceBaseAndreani + "ObtenerListaSucursales",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(requestListaSucursales),
        success: function (response) {
            sucursales = response;
        },
        error: function (error) {
            return error
        }
    });
    return sucursales;
}
function ObtenerLaListaDeProvincias(sucursales) {
    var arr = [];
    for (var i = 0; i < sucursales.length; i++) {
        if (!arr.includes(sucursales[i].direccion.region)) {
            arr.push(sucursales[i].direccion.region);
        }
    }
    return arr.sort();
}
function filtrarSucursalesPorProvincia(provincia) {
    var filtro = ListaSucursales.filter(function (obj) {
        return (obj.direccion.region.toString() == provincia);
    });
    return filtro;
}
function SeleccionProvincia() {
    $('#' + IDListaSucursales).children().remove().end()
    var sucursalesFiltradas = filtrarSucursalesPorProvincia($("#" + IDListaProvincia + " option:selected").val());
    $.each(sucursalesFiltradas, function (index, item) {
        $('#' + IDListaSucursales).append(

            $('<option></option>').val(item.id).html(item.direccion.localidad + " - "
                + item.direccion.componentesDeDireccion[0].contenido
                + " (" + item.direccion.codigoPostal + ")")
        );
    });
}

/// seleccionar sucursal fin ///

















////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////// Metodos/////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////

function login(user, password) {
    var Datalogin = {
        userID: user,
        Password: password
    };
    $.ajax({
        url: serviceBaseAndreani + "Login",
        type: "POST",
        data: JSON.stringify(Datalogin),
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            return response;
        },
        error: function (error) {
            return error;
        }
    });
}
function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
};

function GetHeaderAuthorization() {
    var user;
    var token;
    if (localStorage.authorizationData != undefined) {
        var authorizationData = JSON.parse(localStorage.authorizationData);
        user = authorizationData.UserId;
        token = authorizationData.token;
    }
    else {
        user = getUrlParameter("UserId");
        token = getUrlParameter("Token");
    }
        var header = "Basic " + window.btoa(user + ":" + token);
        return header;
    }
function Test() {
    
    
    var data = {
        userId: 1
    };
    var url = "http://localhost/ZambaWeb.RestApi/search/GetUser?userId=1";
    $.ajax({
    type: "GET",
        url: url,
        async: false,
        //data: JSON.stringify(data),
        success: function (response) {
            return response;
        },
        error: function (error) {
            return error
        }
    });
    
}

function ObtenerProvincias() {

    var provincias = [];
    $.ajax({
        type: "POST",
        url: serviceBaseAndreani + "ListarProvincias",
        async: false,
        success: function (response) {
            return response;
        },
        error: function (error) {
            return error
        }
    });
    return provincias;

}
function ListarSucursales() {
    var requestListaSucursales = {
        userId: GetUID()
    };
    var sucursales = [];
    $.ajax({
        type: "POST",
        url: serviceBaseAndreani + "ListarSucursales",
        async: false,
        data: JSON.stringify(requestListaSucursales),
        success: function (response) {
            sucursales = response;
        },
        error: function (error) {
            return error
        }
    });
    return sucursales;

}

function BuscarEnvio(codigo_cliente, IdProducto, NroDocumentoDestinatario, FechaDesdeCreacion, FechaHastaCreacion) {

    var requestBuscarOrdenCreada = {
        codigoCliente: codigo_cliente,
        IdProducto: IdProducto,
        NroDocumentoDestinatario: NroDocumentoDestinatario,
        fechaCreacionDesde: FechaDesdeCreacion,
        fechaCreacionHasta: FechaHastaCreacion,
        userId: GetUID()
    };
    $.ajax({
        url: serviceBaseAndreani + "BuscarEnvio",
        type: "POST",
        data: JSON.stringify(requestBuscarOrdenCreada),
        //dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            return response;
        },
        error: function (error) {
            return error
        }
    });
}

function CrearNuevaOrden(nro_guia) {

    var requestNuevaOrden = {
        nro_guia: nro_guia,
       // sucursal_id: '3',
        userId: GetUID()
    };
    var ret;
    $.ajax({
        url: serviceBaseAndreani + "CrearNuevaOrden",
        type: "POST",
        data: JSON.stringify(requestNuevaOrden),
        //dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            ret = response;
        },
        error: function (error) {
            ret = error
        }
    });
    return ret;
}

function ObtenerOrdenCreada(nro_orden) {

    var requestNuevaOrden = {
        nro_tracking: nro_orden,
        userId: GetUID()
    };
    $.ajax({
        url: serviceBaseAndreani + "ObtenerOrdenCreada",
        type: "POST",
        data: JSON.stringify(requestNuevaOrden),
        //dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            return response;
        },
        error: function (error) {
            return error
        }
    });
}

function ObtenerEnvio(envio_id) {

    var requestNuevaOrden = {
        nro_tracking: envio_id,
        userId: GetUID()
    };
    $.ajax({
        url: serviceBaseAndreani + "ObtenerEnvio",
        type: "POST",
        data: JSON.stringify(requestNuevaOrden),
        //dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            return response;
        },
        error: function (error) {
            return error
        }
    });
}


function ObtenerTrazaEnvio(envio_id) {

    var requestTrazaEnvio = {
        nro_tracking: envio_id,
        userId: GetUID()
    };
    $.ajax({
        url: serviceBaseAndreani + "ObtenerTrackingEnvio",
        type: "POST",
        data: JSON.stringify(requestTrazaEnvio),
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (response) {
            return response;
        },
        error: function (error) {
            return error
        }
    });
}


function getValueFromWebConfig(key) {
    var pathName = null;
    $.ajax({
        "async": false,
        "crossDomain": true,
        "url": "../../Services/ViewsService.asmx/getValueFromWebConfig?key=" + key,
        "method": "GET",
        "headers": {
            "cache-control": "no-cache"
        },
        "success": function (response) {
            if (response.childNodes[0].innerHTML == undefined) {
                pathName = response.childNodes[0].textContent;
            } else {
                pathName = response.childNodes[0].innerHTML;
            }

        },
        "error": function (data, status, headers, config) {
            console.log(data);
        }
    });
    return pathName;
}


