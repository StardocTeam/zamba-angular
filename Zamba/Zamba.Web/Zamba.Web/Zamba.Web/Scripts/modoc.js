//validacion numero de despacho modoc	
		function validateNroDespacho() {
        var dataDespacho = document.getElementById("zamba_index_139548").value;			
		if (dataDespacho.length != 16 || /[a-z]$/g.test(dataDespacho)) {
        swal("", "Por favor, El numero de Despacho debe contener 16 caracteres y las letras en mayusculas", "info");
		}}
		//validacion de fecha embarque modoc
		function FechaVtoEmb() {
		var fecha = document.getElementById("zamba_index_149662").value;
        var date = getDateToday();
        if(fecha < date) {
         swal("","La fecha de vencimiento no puede ser menor a la fecha actual","info");
         return;		 
		}		
		}
		//validacion de fecha oficializacion modoc
		function FechaOfic() {
		var fecha = document.getElementById("zamba_index_139560").value;
        var date = getDateToday();
        if(fecha < date) {
         swal("","La fecha de vencimiento no puede ser menor a la fecha actual","info");
         return;		 
		}		
		}