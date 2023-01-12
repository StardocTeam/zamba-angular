$(document).ready(function()
{
    $('a#lnk_buscar_forms').each(function()
    {
        this.href = "javascript:BuscarForms()";
    });
    $('a#lnk_filtrar_forms').each(function()
    {
        this.href = "javascript:BuscarForms()";
    });    
    //$('a#lnk_filtrar_forms').each(function()
    //{
    //    this.href = "javascript:FiltrarForms()";
    //});
});

// funcion para buscar formularios por el string ingresado
function BuscarForms()
{
    location.href = raiz + "/formularios" + extension + "/buscar/?pagina=1&buscar=" + $('#buscar_form').val() + "&categ=" + $('#categoria_form').val();
}

function verformulario(id)
{
	location.href = raiz + "/formularios" + extension + "/bajarformulario/" + id;
}

// funcion para filtrar formularios por la categoria seleccionada
// function FiltrarForms()
// {
//    location.href = raiz + "/formularios" + extension + "/filtrar/?pagina=1&categ=" + $('#categoria_form').val();
// }