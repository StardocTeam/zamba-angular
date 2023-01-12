function ExtendValidator() {
    //-----------------------------------------------------------------------------------------------------------------------
    //Validadores por valores(mayores y menores)
    //-----------------------------------------------------------------------------------------------------------------------

    //Se agregan validadores para mayor y menor al validador de jquery
    //Estos tambien permiten usar valores numericos, y fechas, por campos o valores fijos
    jQuery.validator.addMethod("greaterThan",
        function (value, element, params) {
            var element = document.getElementById(params);
            var comparateValue = (element) ? element.value : params;
            
            if (value == "")
                return true;

            if (!/Invalid|NaN/.test(new Date(value))) {
                return new parseDate(value) < parseDate(comparateValue);
            }

            return isNaN(value) && isNaN($(params).val())
                || (Number(value) < Number(comparateValue));
        }, 'Debe ser mayor a {0}.');

    jQuery.validator.addMethod("lessThan",
        function (value, element, params) {
            var element = document.getElementById(params);
            var comparateValue = (element) ? element.value : params;

            if (value == "")
                return true;

            if (!/Invalid|NaN/.test(new Date(value))) {
                return parseDate(value) > parseDate(comparateValue);
            }

            return isNaN(value) && isNaN($(params).val())
                || (Number(value) > Number(comparateValue));
        }, 'Debe ser menor a {0}.');

    jQuery.validator.addMethod("greaterEqualThan",
        function (value, element, params) {
            var element = document.getElementById(params);
            var comparateValue = (element) ? element.value : params;

            if (value == "")
                return true;

            if (!/Invalid|NaN/.test(new Date(value))) {
                return new parseDate(value) <= parseDate(comparateValue);
            }

            return isNaN(value) && isNaN($(params).val())
                || (Number(value) <= Number(comparateValue));
        }, 'Debe ser mayor o igual a {0}.');

        jQuery.validator.addMethod("lessEqualThan",
        function (value, element, params) {
            var element = document.getElementById(params);
            var comparateValue = (element) ? element.value : params;

            if (value == "")
                return true;

            if (!/Invalid|NaN/.test(new Date(value))) {
                return new parseDate(value) >= parseDate(comparateValue);
            }

            return isNaN(value) && isNaN($(params).val())
                || (Number(value) >= Number(comparateValue));
        }, 'Debe ser menor o igual a {0}.');

    //-----------------------------------------------------------------------------------------------------------------------
    //Validadores por fechas validas
    //-----------------------------------------------------------------------------------------------------------------------
    jQuery.validator.addMethod("dateAR",
        function (value, element, params) {
            if (params && value != "" && value != null && element) {
                if (parseDate(value)) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else
                return true;
        }, 'Debe introducir una fecha valida.');

        return true;
}