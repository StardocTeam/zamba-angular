function FuncionNewLegajoPSAD() {


    var CantidadDias;
    Swal.fire({
        title: 'Ingrese la cantidad de días a consultar',
        html: `<input type="text" id="Cantidad" class="swal2-input" placeholder="Dias">`,
        confirmButtonText: 'Aceptar',
        focusConfirm: false,
        preConfirm: () => {
            const Cantidad = Swal.getPopup().querySelector('#Cantidad').value
            CantidadDias = Cantidad
            return { Cantidad: Cantidad }
        }
    }).then((result) => {
        $("#zamba_index_139600").val();
        InvocacionDeServicio($("#zamba_index_139600").val(), result.value.Cantidad)
        /* Swal.fire(`Cantidad: ${result.value.Cantidad}`.trim())*/
    })

}

function InvocacionDeServicio(cuit, cantidad) {

      var url = 'https://gd.modoc.com.ar/ZambaPreNew.RestApi/api/signpdf/GetLegajosAllDesp?despachante='+ cuit +'&userid=160419&days='+ cantidad +'';

    $.ajax({
        type: 'POST',
        url:  url,
        async: false,
        //data: { currentUserId: GetUID() },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            
            if (data != undefined && data != "" && data != null) {
				data=JSON.parse(data);
				
				Swal.fire({
					html: `<br><br><div id="ResultadosText"></div><br><br><div id="Resultados"></div>`,
					confirmButtonText: 'Aceptar',
					focusConfirm: false,
					
				});
				 $("#ResultadosText").append("<h5 style='font-weight:bold;'>"+data.Legajos.length+" Legajos encontrados para el Despachante CUIT Nro: "+cuit+"</h5>");
				
				for(var i = 0; i < data.Legajos.length; i++){
					
					$("#Resultados").append("<div>"+data.Legajos[i].NroLegajo+"</div>");
				}
            } 
        },
        error: function (data) {
            console.log(data);
        }
    });

}