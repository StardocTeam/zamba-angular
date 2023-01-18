function Libreria_AgregarProducto()
{
    var articuloid = $('#articulo').val();
    var articulo   = $("#articulo option[value='" + articuloid + "']").text();
    var unidad     = $('#unidad').val();
    var cantidad   = $('#cantidad').val();
    var row;
    var IdRow;
    var actualizado = false;
    var pos_arti = 0;
    
    // Por las dudas ocultar el div que muestra el resultado del envio
    $('#mensaje_envio_pedido').css('display', 'none');
    
    // Buscar si el producto ya fue agregado, si lo fue
    // actualizar la cantidad
	$('#detalle_pedido tbody > tr').each(function() 
	{					
		var prod_a = $(this).find('td').eq(1).text(); // producto
		var cant_a = parseInt($(this).find('td').eq(2).text()); // cantidad
		var unid_a = $(this).find('td').eq(3).text(); // unidades
		
		// si es el mismo prod, con la misma unidad y el que existe
		// tiene mas de cero unidades (si es cero esta eliminado)
		if(prod_a == articulo && unid_a == unidad)
		{
            // Visualizar por default el detalle del pedido
            $('#DetallePedido').css('display', '');		
		
		    // si tenia cero estaba borrado, hacerlo visible
		    if (cant_a == 0)
		    {
		        $(this).show();
		        Libreria_UpdateHiddenCant(1);
		    }
            
	        var nueva_cant = parseInt(cant_a) + parseInt(cantidad);
		    
	        // actualizar la cantidad visible
		    $(this).find('td').eq(2).text(nueva_cant);
			
		    // posicion del array donde esta el producto
		    pos_arti = parseInt($(this).find('td').eq(0).text());
			
		    // actualizar la cantidad hidden
		    $('#cant_' + pos_arti).val(nueva_cant);
			
		    actualizado = true;		        
		}
	});	
    
    if(!actualizado)
    {
        // Contador para identificar univocamente la fila agregada para poder eliminarla
        IdRow = Libreria_GetIdRow();
        
        // posicion del articulo en el array
        pos_arti = Libreria_GetHiddenCant();
        
        // Visualizar por default el detalle del pedido
        $('#DetallePedido').css('display', '');

        // Armar la fila con el detalle del item agregado
        row  = "<tr id='" + IdRow + "'>";
        row += "    <td style='display:none'>" + pos_arti + "</td>";
        row += "    <td>" + articulo + "</td>";
        row += "    <td>" + cantidad + "</td>";
        row += "    <td>" + unidad + "</td>";
        row += "    <td>";
        row += "        <a href='javascript:Libreria_EliminarProducto(" + IdRow + ");'>";
        row += "            <img border='0' src='" + raiz + "/imgs/delete_icon.png' title='Eliminar del pedido'>";
        row += "        </a>";
        row += "    </td>";
        row += "    <input type='hidden' name='articulos[" + pos_arti + "].Id'       value='" + articuloid + "'>";
        row += "    <input type='hidden' name='articulos[" + pos_arti + "].Articulo' value='" + articulo + "'>";
        row += "    <input type='hidden' id='cant_" + pos_arti + "' name='articulos[" + pos_arti + "].Cantidad' value='" + cantidad + "'>";
        row += "    <input type='hidden' name='articulos[" + pos_arti + "].Unidad'   value='" + unidad + "'>";
        row += "</tr>";    

        // Agregar la fila generada a la tabla
        $('#detalle_pedido > tbody:last').append(row);

        // Actualizar el hidden con la cant de productos
        Libreria_UpdateHiddenCant(1);
        
        // Guardar en el form el nombre del usuario
        $('#usuario').val($('#usuario_zamba').val());
    }
}

function Libreria_GetIdRow()
{
    // obtener la cantidad actual de productos en el pedido
    var aux_cont_productos = parseInt($('#aux_cont_productos').val());
    
    // uno mas ...
    aux_cont_productos = aux_cont_productos + 1;
    
    // actualiza input oculto
    $('#aux_cont_productos').val(aux_cont_productos);
    
    return aux_cont_productos;
}

function Libreria_EliminarProducto(id)
{
    var row = $("#" + id);

    // efectito
    row.fadeOut(1000, function() 
    { 
        // ocultar la fila
        row.hide();
        
        // posicion del array donde esta el producto
		pos_arti = parseInt(row.find('td').eq(0).text());
    			
		// actualizar la cantidad hidden y del td
		row.find('td').eq(2).text(0);
		$('#cant_' + pos_arti).val(0);
		
        // Actualizar el hidden con la cant de productos
        Libreria_UpdateHiddenCant(-1);
    });
}

function Libreria_GetHiddenCant()
{
    // cantidad actual de productos en el pedido
    return parseInt($('#cant_productos').val());
}

function Libreria_UpdateHiddenCant(cant_update)
{
    // cantidad actual
    var cant_actual = Libreria_GetHiddenCant() + cant_update;

    // Actualiza el campo sumando el valor pasado por parametro
    $('#cant_productos').val(cant_actual);

    // Si no quedan items ocultar el detalle del pedido
    if(!cant_actual)
        $('#DetallePedido').css('display', 'none');
}

function Libreria_Resultado(responseText)
{
    var resul = responseText.get_data();
    var msj;
    
     hideLoading();
    
    if(resul)
        msj = "Su pedido ha sido enviado con &eacute;xito";
    else
        msj = "Se produjo un error al enviar su pedido";
    
    $('#div_form_pedido').css('display', 'none');
    $('#DetallePedido').css('display', 'none');    
    
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
	$("#ProductosLibreria").validate({	
		rules: 
		{
		    usuario_zamba: "required",
			articulo: "required",
			unidades: "required",
			paquetes: "required"
		},
		messages: 
		{
		    usuario_zamba: "<br>Por favor seleccione su nombre y apellido",
			articulo: "<br>Por favor seleccione un art&iacute;culo",
			unidades: "<br>Por favor ingrese una cantidad",
			paquetes: "<br>Por favor ingrese una cantidad"
		}
	});
});

// Validacion usuario
$().ready(function() 
{
	// validate signup form on keyup and submit
	$("#frmusuario").validate({	
		rules: 
		{
		    usuario_zamba: "required"
		},
		messages: 
		{
		    usuario_zamba: "<br>Por favor seleccione su nombre y apellido"
		}
	});
});

$(document).ready(function()
{
	$.spin.imageBasePath = raiz + '/imgs/';
	$('#cantidad').spin({
		min: 1,
		interval: 1,
		lock: true,
		timeInterval: 100
	});
	$('#cantidad').val(1);
});