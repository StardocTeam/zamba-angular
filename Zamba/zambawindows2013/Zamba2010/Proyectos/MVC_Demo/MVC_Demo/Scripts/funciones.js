$(document).ready(coloreaResultados);
$(document).ready(addLinks);
$(document).ready(photos);

function coloreaResultados()
{
    $("#resultados tr").hover
    (
        function()
        {
            $(this).addClass("gris");
        },
        function()
        {
            $(this).removeClass("gris");
        }
    )
}

function borrar(id)
{
    if(confirm("¿Desea borrar esta pelicula?"))
    {
        $.get("/pelicula/borrar/" + id, 
            function(data) 
            {
                var row = $("#tr-" + id);     
                row.fadeOut(1000, function() { row.remove(); } );
            },
            function()
            {
                alert("Se produjo un error al eliminar la pelicula seleccionada");
            }
        );
    }
}

function addLinks()
{   
    $('a#editar').each(function()
    {
        this.href = "/pelicula/editar/" + $(this).attr("rel");
    });
    $('a#detalle').each(function()
    {
        this.href = "/pelicula/detalle/" + $(this).attr("rel");
    });                             
    $('a#borrar').each(function()
    {
        this.href = "javascript:borrar('" + $(this).attr("rel") + "')";
    });
    $('a#nueva').each(function()
    {
        this.href = "/pelicula/nuevo/";
    });
    $('a#listado').each(function()
    {
        this.href = "/pelicula/listar/";
    }); 
    $('a#mnu-buscar').attr("href", "/pelicula/buscar/");
    $('a#mnu-buscarajx').attr("href", "/pelicula/buscarAjax/");
    $('a#mnu-listar').attr("href", "/pelicula/listar/");
    $('a#mnu-nuevo').attr("href", "/pelicula/nuevo/");
}

function LlenaResultadosBusqueda(responseText)
{
    $("#resultados").html(responseText.get_data());
    coloreaResultados();
    addLinks();
}

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
			        width: '50px', /* Set width back to default */
			        height: '50px', /* Set height back to default */
			        padding: '5px'
		        }, 400);
        });
    });
}    