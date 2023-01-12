$(document).ready(function()
{	
	setTimeout("ChequearIndicePorValorErroneo('zamba_index_26463', 'El sistema no ha validado la vigencia del componente, por favor realice una consulta al área Técnica')", 100);					
	setTimeout("ChequearIndicePorValorErroneo('zamba_index_26463', 'Pendiente Consulta a Tecnica')", 100);					
	setTimeout("ChequearIndicePorValorErroneo('zamba_index_26463', 'Vigencia y estado  rechazada por Tecnica  manualmente')", 100);					
	setTimeout("ChequearIndicePorValorErroneo('zamba_index_26462', 'No posee cobertura financiera, realizar consulta a cobranzas')", 100);					
	setTimeout("ChequearIndicePorValorErroneo('zamba_index_26462', 'Pendiente Consulta a Cobranzas')", 100);					
	setTimeout("ChequearIndicePorValorErroneo('zamba_index_26462', 'Cobertura rechazada por cobranzas manualmente')", 100);					
	setTimeout("ChequearIndicePorValorErroneo('zamba_index_26489', 'Siniestro Prescripto')", 100);					

	// Valida solo numericos
	// Agregar class="solonum" a todos los inputs que se quieran validar
	$(".solonums").each(function()
	{
		$(this).keypress(function (e)
		{
			if (e.which!=8 && e.which!=0 && e.which!=44 && e.which!=46 && (e.which<48 || e.which>57))
			{
				return false;
			}
		})
	});
});	
				
function ChequearIndicePorValorErroneo(id, value)
{
	var index = $('#' + id);
	
	if(index)
	{
		if(index.val() == value)
		{	
			index.addClass('error');
			index.css('border-color', 'red');
		}
	}
}