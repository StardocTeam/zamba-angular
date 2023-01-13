$(document).ready(addFuncionalidad);

//Estas son para hobbit
//var raiz = "/IntranetMarsh";
//var extension = ".aspx";

//Estas son para desarrollo
var raiz = "";
var extension = "";

function LeerNoticia(id)
{
	location.href = raiz + "/noticias" + extension + "/leer/" + id;
}

function ir_a_formulario(url)
{
    location.href = raiz + "/formularios" + extension + url;
}

function addFuncionalidad()
{   
    $('a#link_home').attr('href', raiz + "/");
	$('a#link_noticias').attr('href', raiz + "/noticias" + extension + "/");
    $('a#link_vision').attr('href', raiz + "/vision" + extension + "/");
    $('a#link_mmc-compliance').attr('href', raiz + "/mmc_compliance" + extension + "/");
    $('a#link_learning_connect').attr('href', "http://global.marsh.com/");
    $('a#link_learning_connect').attr("target", "_blank");
    $('a#link_formularios').attr('href', raiz + "/formularios" + extension + "/");
    $('a#link_servicios').attr('href', raiz + "/servicios" + extension + "/");   
    $('a#link_libreria').attr('href', raiz + "/libreria" + extension + "/");  
    $('a#link_desperfectos').attr('href', raiz + "/desperfectos" + extension + "/");
    $('a#link_tarjetas_comerciales').attr('href', raiz + "/tarjetas_comerciales" + extension + "/");
    $('a#link_otros_servicios').attr('href', raiz + "/otros_servicios" + extension + "/");    
    $('a#link_plan_contingencias').attr('href', raiz + "/seguridad_higiene" + extension + "/?file=plan-de-vacuacion-13052009.pptx");
    $('a#link_arbol_llamadas').attr('href', raiz + "/seguridad_higiene" + extension + "/?file=appendix-h-call-tree-29052009.xls");
    $('a#link_telefonos').attr('href', raiz + "/telefonos" + extension + "/");   
    $('a#link_comunicandonos').attr('href', raiz + "/comunicandonos" + extension + "/");
	
    $('a#link_mas_noticias').each(function()
    {
        this.href = raiz + "/noticias" + extension + "/pagina/?pagina=" + $(this).attr("rel");
    });        
    $('a#link_ver_form').each(function()
    {
        //this.href = raiz + "/formularios" + extension + "/ver/" + $(this).attr("rel");
    });
    $('#img_loading').each(function()
    {
        $(this).css('display', 'none');
    });
    $('a#lnk_buscar').each(function()
    {
        this.href = "javascript:Buscar()";
    });
}

// busqueda de formularios desde el header
function Buscar()
{
    //location.href = raiz + "/formularios" + extension + "/buscar/?pagina=1&categ=&buscar=" + $('#buscar').val();    
    window.open('http://www.marsh.com.ar/auto.cfm?myurl=marsh%2Fbuscar.cfm&search=' + $('#buscar').val());
}

// funcion generia, carga lo que le manden a un div
function CargaDivBusqueda(responseText)
{
    hideLoading();
    $('#resultados_busqueda').html(responseText.get_data());
}

// bloquea los controles y muestra el "loading", usado cuando se llama a Ajax
function showLoading()
{
    $('input').each(function(){    
        $(this).attr('disabled', 'true');    
    });
    
    $.blockUI({ 
        showOverlay: false, 
        centerY: true, 
        css: {
			baseZ: '9999',
            width: '350px', 
            border: 'none', 
            padding: '15px', 
            backgroundColor: '#000', 
            '-webkit-border-radius': '10px', 
            '-moz-border-radius': '10px', 
            opacity: .7, 
            color: '#fff',
			top: '150px'
        } 
    });         
}

// desbloquea controles, oculta mensaje "loading"
function hideLoading()
{
    $.unblockUI();
    
    $('input').each(function(){    
        $(this).attr('disabled', '');    
    });    
}