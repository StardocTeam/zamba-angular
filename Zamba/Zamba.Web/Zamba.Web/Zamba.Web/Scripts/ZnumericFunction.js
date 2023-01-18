$(document).ready(function () {

    if ($("input[operation=sum]").length > 0 && $("input[operation=result]").length > 0) {

        // esta funcion es para la suma, se realiza asi para poder inyectarle otras operaciones matematicas

        var operation;

        for (var i = 0; i < $("input[operation]").length; i++) {

            //si se quiere agregar otras operaciones se debe agregar aca otro if 

            if ($("input[operation]")[i].getAttribute("operation") == "sum") {
                operation = "sum";
                break;
            }
        }

         switch(operation) {
             case 'sum':
                subcriptionEventSum();
                funtionsum();
                break;
            case 'rest':
                break;
            case 'div':
                break;
            default:
                console.log("No se encontro operador para la operacion matematica");
         }


        function funtionsum() {

            var resultsum = 0;

            for (var i = 0; i < $("input[operation=sum]").length; i++) {

                var value = parseFloat($($("input[operation=sum]")[i]).val());

                resultsum = + value;
            }

            $("input[operation=result]").val(resultsum.toString());

        }

        function subcriptionEventSum() {

            for (var i = 0; i < $("input[operation=sum]").length; i++) {

                var val = $("input[operation=sum]")[i].getAttribute("id");
                $('#' + val + '').on('change', funtionsum);

               
            }

        }

      }
   
    
});
