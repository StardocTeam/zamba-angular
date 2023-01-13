

        function SetAsocId(sender) {
            document.getElementById("hdnAsocId").name = sender.id;
            frmMain.submit();
        }

        //Crea las validaciones de los campos
        function makerequired() {
            $('#aspnetForm').validate(
                 {
                     onsubmit: false,
                     ignore: ".disabled",
                     rules: {
                         //FHV
                         zamba_index_1205: {
                             required: true,
                             maxlength: 10,
                             digits: {
                                 depends: function(element) {
                                     return $(element).val() == " ";
                                 }
                             }
                         },
                         //Fecha ingreso FHV
                         zamba_index_1207: {
                             maxlength: 10,
                             required: true,
                             date: true
                         },
                         //Fecha de Respuesta
                         zamba_index_1267: {
                             maxlength: 10,
                             date: true
                         },
                         //Nro.FHV
                         zamba_index_1206: {
                             maxlength: 10
                         },
                         //Caudal FHV m3/h
                         zamba_index_1212: {
                             maxlength: 12,
                             number: true,
                             required: true
                         },
                         //Caudal FHV m3/dia
                         zamba_index_1213: {
                             maxlength: 12,
                             number: true,
                             required: true
                         }

                     },
                     messages: {
                         //Fecha ingreso FHV
                         zamba_index_1207: {
                             maxlength: "Se ha exedido el l�mite de 10 caracteres.",
                             required: "Es necesario una fecha de ingreso.",
                             date: "Debe ingresar una fecha."
                         },
						 //Fecha de Respuesta
						 zamba_index_1267: {
                             maxlength: "Se ha exedido el limite de 10 caracteres.",
                             required: "Es necesario una fecha de respuesta.",
                             date: "Debe ingresar una fecha."
                         },
                         //FHV
                         zamba_index_1205: {
                             maxlength: "Se ha exedido el l�mite de 10 caracteres.",
                             required: "Es necesario un FHV.",
                             digits: "Es necesario un FHV."
                         },
                         //Nro.FHV
                         zamba_index_1206: {
                             maxlength: "Se ha exedido el l�mite de 10 caracteres.",
                             required: "Es necesario un numero de FHV."
                         },
                         //Caudal FHV m3/h
                         zamba_index_1212: {
                             maxlength: "Se ha exedido el l�mite de 12 caracteres.",
                             number: "Debe ingresar solo numeros.",
                             required: "Es necesario un caudal FHV m3/h."
                         },
                         //Caudal FHV m3/dia
                         zamba_index_1213: {
                             maxlength: "Se ha exedido el l�mite de 12 caracteres.",
                             number: "Debe ingresar solo numeros.",
                             required: "Es necesario un caudal FHV m3/dia."
                         }
                     }
                 });
             }

             function ValidateLength(element, RequiredLength) {
                 var Str = element.value;
                 $(element).valid();
                 if (Str.length < RequiredLength)
                     return true;
                 return false;
             }

             //Funci�n que evalua si habilitar o no un control en base a uno o dos valores
             function ConditionalEnable(ObjSource, ObjDestiny, Value, Value2) {
                 if ($("#" + ObjSource).val() == Value || (Value2 != undefined && $("#" + ObjSource).val() == Value2)) {

                     //Quita la clase que ignora el validate y valida el control nuevamente
                     //$("#" + ObjDestiny).removeClass("disable");
                     $("#" + ObjDestiny).valid();
                     //Habilita el datepicker
                    // $("#" + ObjDestiny).datepicker("enable");
                     //Muestra el asterisco para marcar que es un campo requerido.
                     $("#lbl_" + ObjDestiny).show();
                   //  $("#" + ObjDestiny).attr('disabled', '');
					 $("#" + ObjDestiny).addClass("required");
                     //                if ($("#" + ObjDestiny).datepicker("option", "disabled") === undefined) {
                     //                    $("#" + ObjDestiny).attr('disabled', 'disabled');
                     //                }
                 }
                 else {
                   //  $("#" + ObjDestiny).attr('disabled', 'disabled');
                  //   $("#" + ObjDestiny).val("");
                     //Agrega la clase que ignora el validate y valida el control nuevamente
                    // $("#" + ObjDestiny).addClass("disable");
					 $("#" + ObjDestiny).removeClass("required");
                     $("#" + ObjDestiny).valid();
                     //Oculta el asterisco para marcar que es un campo requerido.
                     $("#lbl_" + ObjDestiny).hide();
                     //deshabilita el datepicker
                //     $("#" + ObjDestiny).datepicker("disable");
                 }
             }
  
             $(document).ready(function () {
                 makeCalendar('zamba_index_1207');
				 makeCalendar('zamba_index_1267');
                 makerequired();
				 
                 ConditionalEnable("zamba_index_1205", "zamba_index_1206", "Si");
				ConditionalEnable("zamba_index_1205", "zamba_index_1267", "Si", "Denegada");
				$("#zamba_index_1205").change(function () {
					ConditionalEnable("zamba_index_1205", "zamba_index_1206", "Si");
					ConditionalEnable("zamba_index_1205", "zamba_index_1267", "Si", "Denegada");
				});
				
                $("#zamba_save").click(ValidateBeforeSave);
             });
    
