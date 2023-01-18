//$().ready(addLinks);
$().ready(photos);
$().ready(busquedaInicial);

// guarda la letra que se busco para el paginador
var letra_buscada = '';

function addLinks()
{   
    $('a#lnk_letra').each(function()
    {
        this.href = "javascript:BuscarPorLetra('" + $(this).attr("rel") + "', 1);";
    });
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

function busquedaInicial()
{
    BuscarPorLetra('a', 1);
}

function BuscarPorLetra(letra, pagina)
{    
    //limpiar el text de busqueda
    $("#abuscar").val('');
    
    //guardar la letra buscada, la usa la funcion que arma los links de pagina
    letra_buscada = letra;
    
    showLoading();
    
    $("#resultados_busqueda").load(raiz + "/telefonos" + extension + "/listar/?pagina=" + pagina + "&letra=" + letra, hideLoading);
}

function IrPaginaContactos(pagina)
{
    // si hay algo en el textbox es una busqueda "libre", sino es por letra
    if($("#abuscar").val() != "")
    {
        // setear la pagina a la que quiere ir
        $('#pagina').val(pagina);
        
        // enviar el form simulando el click en el submit (no funciono haciendo submit del formulario)
        $('#submit').click();
        
        // poner la pagina como 1 por si hace una busqueda nueva, sino los resultados
        // estarian paginados en la misma pagina que la ultima busqueda
        $('#pagina').val(1);
    }
    else
    {
        BuscarPorLetra(letra_buscada, pagina);
    }
}

function SiguienteBusqueda()
{
    var letras = new Array("a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");
    
    // posicion de la letra de busqueda actual
    var pos = letras.findIndex(letra_buscada);
    
    // siguiente letra
    pos++;
    
    // si se acabo el abecedario volver al inicio
    if(pos > letras.length - 1)
        pos = 0;
        
    // nueva letra a buscar
    letra_buscada = letras[pos];
    
    // buscar
    BuscarPorLetra(letra_buscada, 1);
}

Array.prototype.findIndex = function(value)
{
    var ctr = "";
    for (var i=0; i < this.length; i++) 
    {
        // use === to check for Matches. ie., identical (===), ;
        if (this[i] == value) 
        {
            return i;
        }
    }
    return ctr;
};

// Validacion
$().ready(function()
{
	// validate signup form on keyup and submit
	$('form').validate({
		rules: 
		{
			abuscar: "required"
		},
		messages: 
		{
			abuscar: "&nbsp;Ingrese un texto a buscar"
		}
	});	
});

function photos()
{
    $('img#photo').each(function() 
    {
        $(this).hover(function() 
        {
	        $(this).css({'z-index' : '100'}); /*Add a higher z-index value so this image stays on top*/ 
	        $(this).addClass("hover").stop() /* Add class of "hover", then stop animation queue buildup*/
		        .animate({
			        marginTop: '-50px', /* The next 4 lines will vertically align this image */ 
			        marginLeft: '-50px',
			        top: '50%',
			        left: '50%',
			        width: '174px', /* Set new width */
			        height: '174px', /* Set new height */
			        padding: '10px'
		        }, 200); /* this value of "200" is the speed of how fast/slow this hover animates */
	        } , function() {
	        $(this).css({'z-index' : '0'}); /* Set z-index back to 0 */
	        $(this).removeClass("hover").stop()  /* Remove the "hover" class , then stop animation queue buildup*/
		        .animate({
			        marginTop: '0', /* Set alignment back to default */
			        marginLeft: '0',
			        top: '0',
			        left: '0',
			        width: '40px', /* Set width back to default */
			        height: '40px', /* Set height back to default */
			        padding: '5px'
		        }, 400);
        });
    });
} 