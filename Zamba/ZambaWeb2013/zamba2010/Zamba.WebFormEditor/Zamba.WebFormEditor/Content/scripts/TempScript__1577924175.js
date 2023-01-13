
        function SetRuleId(sender) {
            document.getElementById("hdnRuleId").name = sender.id;
            frmMain.submit();
        }

        function makeCalendar(id, limit) {
            switch (limit) {
                case "smaller": $(function() {
                                    $('#' + id).datepicker({
                                        changeMonth: true,
                                        changeYear: true,
                                        showOn: 'both',
                                        buttonText: 'Abrir calendario',
                                        buttonImage: 'images/calendar.png',
                                        buttonImageOnly: true,
                                        duration: "",
                                        maxDate: '+0d',
                                        dateFormat: 'dd/mm/yy',
                                        constrainInput: true
                                    });
                                });
                                break;
                    case "bigger": $(function() {
                                    $('#' + id).datepicker({
                                        changeMonth: true,
                                        changeYear: true,
                                        showOn: 'both',
                                        buttonText: 'Abrir calendario',
                                        buttonImage: 'images/calendar.png',
                                        buttonImageOnly: true,
                                        duration: "",
                                        minDate: "+0d",
                                        dateFormat: 'dd/mm/yy',
                                        constrainInput: true
                                    });
                                });
                        break;
                    case "normal": $(function() {
                        $('#' + id).datepicker({
                            changeMonth: true,
                            changeYear: true,
                            showOn: 'both',
                            buttonText: 'Abrir calendario',
                            buttonImage: 'images/calendar.png',
                            buttonImageOnly: true,
                            duration: "",
                            dateFormat: 'dd/mm/yy',
                            constrainInput: true
                        });
                    });
                        break;
            }
        }

        //Funci�n que evalua si habilitar o no un control en base a si el control fuente est� vacio
        function ConditionalEnable(ObjSource, ObjDestiny) {
            if ($("#" + ObjSource).val() != "") {

                //Quita la clase que ignora el validate y valida el control nuevamente
                $("#" + ObjDestiny).removeClass("disable");
                $("#" + ObjDestiny).valid();
                //Habilita el datepicker
                $("#" + ObjDestiny).datepicker("enable");
                //Muestra el asterisco para marcar que es un campo requerido.
                $("#lbl_" + ObjDestiny).show();
               // $("#" + ObjDestiny).attr('disabled', '');

              //  if ($("#" + ObjDestiny).datepicker("option", "disabled") === undefined) {
               //     $("#" + ObjDestiny).attr('disabled', 'disabled');
              //  }
            }
            else {                
              //  $("#" + ObjDestiny).attr('disabled', 'disabled');
                $("#" + ObjDestiny).val("");
                //Agrega la clase que ignora el validate y valida el control nuevamente
                $("#" + ObjDestiny).addClass("disable");
                $("#" + ObjDestiny).valid();
                //Oculta el asterisco para marcar que es un campo requerido.
                $("#lbl_" + ObjDestiny).hide();
                //deshabilita el datepicker
                $("#" + ObjDestiny).datepicker("disable");
            }
        }

        function ValidateLength(element, RequiredLength) {
            var Str = element.value;
            $(element).valid();
            if (Str.length < RequiredLength)
                return true;
            return false;
        }

        //Funcion que eval�a si una tecla pulsada pertenece al tipo dado
        function KeyIsValid(e, TypeToValidate, Obj) {

            var key = e.charCode || e.keyCode || 0;
            var isValid = false;

            switch (TypeToValidate) {
                case "numeric":
                    if (key >= 48 && key <= 57)
                        isValid = true;
                    break;
                case "date":
                    if ((key >= 48 && key <= 57) || key == 47)
                        isValid = true;
                    break;
                case "2decimal":
                    var currentValue = $("#" + Obj).val();
                    var commaPos = currentValue.indexOf('.');
                    var caretPos = $("#" + Obj).caret().start;

                    if (key == 46)
                        isValid = (commaPos == -1 && caretPos != 0);
                    else {
                        if ((key >= 48 && key <= 57)) {
                            if (commaPos == -1) {
                                isValid = (currentValue.length < 6);
                            }
                            else {
                                if (caretPos > commaPos) {
                                    isValid = (currentValue.length - 3 < commaPos);
                                }
                                else
                                    isValid = (commaPos < 6);
                            }
                        }
                    }
                    break;
            }

            return isValid;
        }

        //Crea las validaciones de los campos
        function makerequired() {
            $('#aspnetForm').validate(
				{
				    onsubmit: false,
				    ignore: ".disabled",
				    rules: {
				        zamba_index_1224: {
				            required: true,
				            maxlength: 14
				        },
				        zamba_index_1229: {
				            maxlength: 300
				        }
				    },
				    messages: {
				        zamba_index_1224: {
				            required: "Es necesario un n�mero de ODT.",
				            maxlength: "Se ha exedido el l�mite de 14 caracteres."
				        },
				        zamba_index_1229: {
				            maxlength: "Se ha exedido el l�mite de 300 caracteres."
				        }
				    }
				});
        }

        $(document).ready(function() {
             makeCalendar('zamba_index_1225', "smaller");
            makeCalendar('zamba_index_1226', "bigger");

            makerequired();

            ConditionalEnable('zamba_index_1225', 'zamba_index_1226');
            
            $("#zamba_save").click(ValidateBeforeSave);
        });				
	
    
