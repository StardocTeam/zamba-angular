$(document).ready(function () {
            $(".moneda").each(function () {
                $(this).keyup(function (e) {
                  /* if (DecimalCheck($('#' + e.target.id))) {*/
                        SumarCampos();
                  /* }*/
                })
            });
        });
        function zamba_save_onclick_DocInsertModal(event) {
            if (document.getElementById("zamba_index_2719").value == "" || document.getElementById("zamba_index_2719").value == "0") {
               
                swal("Debe completar el Total de la Transacci\u00F3n.");
                document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
                CloseLoading();
                event.preventDefault();
            } else if ($('input[type=text]').hasClass('error')) {
                swal("Verifique que todos los datos de los campos sean v\u00E1lidos");
                document.getElementById("hdnRuleId").name = "zamba_rule_cancel";
                CloseLoading();
                event.preventDefault();
            } else {
                swal("Se guard\u00F3 exitosamente!");
                document.getElementById("hdnRuleId").name = "zamba_rule_save";
            }
        }
        function CloseLoading() {
            setTimeout("parent.hideLoading();", 500);
        }
        function SumarCampos() {
            //Obtiene cada valor y lo convierte a decimal

            var capital = 0;
            var Letrado  = 0;
            var LetradoCia = 0;
            var Mediador = 0;
            var Peritos = 0;
            var TasaJusticia = 0;
            var OtrosGastos = 0;

            if ($("#zamba_index_2744").val() != '' && parseFloat($("#zamba_index_2744").val().replace('', '0')) > 0)
                capital = parseFloat($("#zamba_index_2744").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            if ($("#zamba_index_2813").val() != '' && parseFloat($("#zamba_index_2813").val().replace('', '0')) > 0)
                Letrado = parseFloat($("#zamba_index_2813").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            if ($("#zamba_index_2814").val() != '' && parseFloat($("#zamba_index_2814").val().replace('', '0')) > 0)
                LetradoCia = parseFloat($("#zamba_index_2814").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            if ($("#zamba_index_2815").val() != '' && parseFloat($("#zamba_index_2815").val().replace('', '0')) > 0)
                Mediador = parseFloat($("#zamba_index_2815").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            if ($("#zamba_index_2816").val() != '' && parseFloat($("#zamba_index_2816").val().replace('', '0')) > 0)
                Peritos = parseFloat($("#zamba_index_2816").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            if ($("#zamba_index_2817").val() != '' && parseFloat($("#zamba_index_2817").val().replace('', '0')) > 0)
                TasaJusticia = parseFloat($("#zamba_index_2817").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));

            if ($("#zamba_index_2818").val() != '' && parseFloat($("#zamba_index_2818").val().replace('', '0')) > 0)
                OtrosGastos = parseFloat($("#zamba_index_2818").val().replace('.', '').replace('.', '').replace('.', '').replace(',', '.'));



            //Suma los valores obtenidos y lo asigna al indice Total
            $("#zamba_index_2719").val(Redondear(capital + Letrado + LetradoCia + Mediador + Peritos + TasaJusticia + OtrosGastos ));
        }
       