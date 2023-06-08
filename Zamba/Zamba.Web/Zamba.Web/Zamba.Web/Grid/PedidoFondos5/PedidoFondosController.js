//var app = angular.module("app", ["xeditable"]);

//app.run(function (editableOptions) {
//    editableOptions.theme = 'bs3';
//});


app.controller('BenefCtrl', function ($scope, $filter, $http, gridService) {

    $scope.IsViewMode = false;
    $scope.IsEditingMode = false;
    $scope.deleteItems = false;


    $scope.asociatedResults = [];

    $scope.LoadAsociatedResults = function (parentResultId, parentResultEntityId, associatedIds) {

        var d = gridService.getAsociatedResults(parentResultId, parentResultEntityId, associatedIds);
        if (d != 'null') {

            var associatedResults = getFormattedResults(JSON.parse(d));
            //            .done(function (d) {
            if (associatedResults == "") {
                var newObj = GetNewResult();
                $scope.asociatedResults = [];
                $scope.setTotal();
                $scope.asociatedResults.push(newObj);
                $scope.IsViewMode = false;
                $scope.IsEditingMode = true;

                return;
            }
            else {

                $scope.asociatedResults = associatedResults;
                $scope.setTotal();
                $scope.IsViewMode = true;
                $scope.IsEditingMode = false;

            }
        }
        setTagValueById("hdnCount", $scope.asociatedResults.length);
    };

    $scope.sorterFunc = function (results) {
        try {
            if ($scope.IsEditingMode) {
                return parseInt(results.data.$$hashKey)
            }
            else {
                return parseInt(results.data.concept)
            }

        } catch (e) {
            return parseInt(results.data.concept)

        }

    }


    $scope.setElementPartNumber = function (result) {
        if (result.data.amount != undefined && result.data.amount != '') {
            result.data.amount = String(parseFloat(result.data.amount.replace('.', '').replace('.', '').replace('.', '').replace(',', '.')).toFixed(2)).replace('.', ',')
                .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
            if (result.data.amount == "NaN" || result.data.amount == "" || isNaN(parseFloat(result.data.amount))) {
                result.data.amount = '';
                swal("", "Debe completar el campo importe con un numero", "info");
                return;
            }
        }
        else {
            //swal("", "Debe completar el campo importe con un numero", "info");
            return;
        }
    }

    $scope.setTotal = function () {
        var total = 0;
        if ($scope.asociatedResults != "" && $scope.asociatedResults.length > 0) {
            for (var result in $scope.asociatedResults) {
                var amount = null;
                if (!String($scope.asociatedResults[result].data.amount) == ""
                    && String($scope.asociatedResults[result].data.amount) != null) {
                    amount = String($scope.asociatedResults[result].data.amount).replace(/\./g, "").replace(",", ".");
                } else {
                    amount = "0";
                }

                total = total + parseFloat(amount);
            }
        }

        $scope.total = String(total.toFixed(2)).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");

    }


    function getFormattedResults(results) {
        var resultsFormated = [];
        var newResult = null;
        if (results == null) {
            results = [];
        }
        if (results.length > 0) {
            for (var result in results) {
                newResult = GetFormattedResult(results[result]);
                resultsFormated.push(newResult);
                newResult = null;
            }
        }
        return resultsFormated;
    };

    function GetFormattedResult(AssociatedResult) {
        var currentAmount = '';
        if (AssociatedResult.IMPORTE == null) {
            currentAmount = '';
        }
        else {
            currentAmount = String(parseFloat(AssociatedResult.IMPORTE).toFixed(2)).replace(".", ",").replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
        }

        var result = {
            id: AssociatedResult.RESULTID,
            resultId: getElementFromQueryString("docid"),
            entityId: $scope.entityId,
            data: {
                favorTo: AssociatedResult.A_FAVOR_DE,
                commitmentNumber: AssociatedResult.COMPROMISO_NRO,
                payMethod: getElementFromListoByElementIndex($scope.MetodoAttributeLists, AssociatedResult.MEDIOS_DE_PAGO),
                cbu: AssociatedResult.CBU,
                cuit: AssociatedResult.CUIT,
                concept: getElementFromListoByElementIndex($scope.ConceptoAttributeLists, AssociatedResult.CONCEPTO),
                alternativeConcept: AssociatedResult.CONCEPTOALTERNATIVO,
                cuitpro: AssociatedResult.CUITPRO,
                amount: currentAmount,
                email: AssociatedResult.MAIL,
                personNumber: AssociatedResult.NRO_DE_PERSONA,
				estadoCompromiso: getElementFromListoByElementIndexAndSplit($scope.EstadoAttributeLists, AssociatedResult.ESTADO_DE_COMPROMISO,','),
				tentativaPago: Validate_Date(AssociatedResult.FECHA_TENTATIVA_DE_PAGO)
            },
            isDeleted: false,
            isNew: false,
            isEdited: false,
            IsComplete: false
        };
        return result;
    }

    function Validate_Date(date){
        if (date != undefined && 
            date != null && 
            date != "")
        {
            return new Date(date).toLocaleDateString();
        }else{
            return "";
        }
    }

    function getElementFromListoByElementIndex(array, element) {
        var valor = null;
        array.forEach(function (item) {
            if (item.split("-")[0] == String(element)) { valor = item; }
        });
        if ((valor == null || valor == '') && String(element) != null) {
            valor = element;
        }
        return valor;
    }
	
 	function getElementFromListoByElementIndexAndSplit(array, element, splitChar) {
        var valor = null;
        array.forEach(function (item) {
            if (item.split(splitChar)[0] == String(element)) { valor = item.split(splitChar)[1]; }
        });
        if ((valor == null || valor == '') && String(element) != null) {
            valor = element;
        }
        return valor;
    }


    $scope.getColumn = function (AttributeId) {
        var col = null;
        $($scope.asociatedColumns).each(function (index, column) {
            if (column.Id == AttributeId) {
                col = column;
                return false;
            }
        });
        return col;
    };

    $scope.getColumnDropDown = function (AttributeId) {
        var IsDropDown = false;
        $($scope.asociatedColumns).each(function (index, column) {
            if (column.Id == AttributeId) {
                if (column.DropDown > 0) {
                    IsDropDown = true;
                    return false;
                }
                else {
                    IsDropDown = false;
                    return false;
                }

            }
        });
        return IsDropDown;

    }

    $scope.ConceptoAttributeLists = ["1-Capital", "2-Intereses", "3-Honorarios abogado actor", "4-Honorarios mediador",
        "5-Honorarios estudio externo", "6-Tasa de justicia",
        "7-Sobretasa", "8-Honorarios perito ingeniero",
        "9-Honorarios perito Medico", "10-Honorarios perito Psicologo",
        "11-Honorarios perito Contador", "12-IVA Deposito",
        "13-Deposito por Recurso", "14-Aporte abogado Actor",
        "15-Aportes Mediador", "16-Aportes estudio Externo",
        "17-Aportes Perito Ingeniero", "18-Aportes Perito Medico", "19-Aportes Perito Psicologo", "20-Aportes Perito Contador", "22-Otros", "23-Honorarios de Cierre de Mediación si acuerdo", "24-Gastos Iniciales"];

    $scope.MetodoAttributeLists = ["1-Cheque",
        "2-Transferencia CBU Cuenta Bancaria",
        "3-Transferencia CBU Cuenta Judicial",
        "4-Compensación Contable", "5-Cheque Certificado", "6-Cheque Imputado"];

		$scope.EstadoAttributeLists = [
		
		"ANULADO DESPUÉS DE PASAR A OF,ANULADO",
		"ANULADO DESPUES DE PASAR A OF Y GENERÓ EL COMPROMISO COMPENSATORIO NRO:,ANULADO",
		"ANULADO EN RECTOR, PREVIAMENTE ELIMINADO EN LA INTERFACE,CON ERROR",
		"COMPROMISO BORRADO DE LA INTERFACE DE OF,ANULADO",
		"EN OF - COMPROMISO PENDIENTE DE PAGO EN ESTADO  *NECESITA REVALIDACION*  COMUNICARSE CON EL SECTOR PAGOS,CON ERROR",
		"EN OF - COMPROMISO RETENIDO EN ORACLE FINANCIAL - COMUNICARSE CON EL SECTOR DE PAGOS,CON ERROR",
		"EN OF - CON CHEQUE EMITIDO PERO NO ENTREGADO.,CHEQUE EMITIDO",
		"EN OF - CON CHEQUE ENTREGADO.,CHEQUE ENTREGADO",
		"EN OF - CON ERROR EN LA INTERFACE. COMUNICARSE CON SECTOR PAGOS.,CON ERROR",
		"EN OF - CON MÉTODO COMPENSATORIO,TRANSFERIDO",
		"EN OF - CON TRANSFERENCIA A SUCURSAL DE BANCO PROVINCIA,TRANSFERIDO",
		"EN OF - PAGADO POR MÉTODO ELECTRÓNICO,TRANSFERIDO",
		"EN OF - PENDIENTE DE PROCESAMIENTO POR TESORERÍA.,CONFORMADO- PENDIENTE DE PROCESAMIENTO",
		"EN OF - PENDIENTE IMPORTACIÓN.  SE PROCESARÁ MAS TARDE.,FALTA CONFORMAR",
		"EN RECTOR -  ANULADO ANTES DE PASAR A OF,ANULADO",
		"EN RECTOR - COMPROMISO COMPENSATORIO DEL COMPROMISO ANULADO NRO:,CON ERROR",
		"EN RECTOR - CONFORMADO CON PROBLEMAS AL TRANSFERIR A OF , CON ERRORES,CON ERROR",
		"EN RECTOR - CONFORMADO Y PENDIENTE DE EXPORTAR A OF,CONFORMADO- PENDIENTE DE PROCESAMIENTO",
		"EN RECTOR - EN PROCESO DE LIQUIDACIÓN STRO,CON ERROR",
		"EN RECTOR - LIQUIDADO PENDIENTE DE AUTORIZACIÓN GERENCIAL,FALTA CONFORMAR",
		"EN RECTOR - LIQUIDADO SIN CONFORMAR,FALTA CONFORMAR",
		"FALTA ANULAR EN RECTOR - ANULADO DE LA INTERFACE DE OF,CON ERROR",
		"FALTA ANULAR EN RECTOR - ANULADO DESPUES DE PASAR A OF,ANULADO",
		"PASADO A OF Y OF NO INFORMA SITUACIÓN ALGUNA - AVISAR A SISTEMAS.,CON ERROR",
		"11 - INCONSISTENCIA, COMUNICAR A SISTEMAS.  ANULADO DESPUES DE PASAR A OF Y NO GENERÓ EL COMPROMISO COMPENSATORIO EN RECTOR.,CON ERROR",
        "12 - INCONSISTENCIA, COMUNICAR A SISTEMAS.  ANULADO EN RECTOR Y NO EN OF.,CON ERROR",
        "EN OF - PENDIENTE IMPORTACIÓN.  SE PROCESARÁ MAS TARDE., TRANSFERIDO",
		];

    $scope.showAttributeDescription = function (Attribute) {
        var selected = [];
        if (Attribute.Data) {
            selected = $filter('filter')($scope.loadAttributeList(Attribute.Id), { Data: Attribute.Data });
        }
        return selected.length ? selected[0].DataDescription : Attribute.Data;
    };

    // filter users to show
    $scope.filterResult = function (result) {
        return (result.isDeleted != undefined || result.isDeleted !== true);
    };

    // mark user as deleted
    $scope.deleteResult = function (result) {
        try {

            var i = 0;
            while (i < $scope.asociatedResults.length) {
                if ($scope.asociatedResults[i].id == result.id) {
                    $scope.asociatedResults.splice(i, 1)
                } else {
                    ++i;
                }
            }

            if (result.id != null) {
                gridService.deleteResult(result);
            }

            //   $scope.IsEditingMode = true;
            //   $scope.IsViewMode = false;

            $scope.saveTable();

            $scope.LoadAsociatedResults($scope.parentResultId, $scope.entityId, $scope.parentEntityId);

            $scope.IsEditingMode = true;
            $scope.IsViewMode = false;


            $scope.setTotal();
            setTagValueById("hdnCount", $scope.asociatedResults.length);

            $scope.$apply();
        }
        catch (e) {

        }
    };

    $scope.addResult = function () {

        $scope.saveTable();

        $scope.LoadAsociatedResults($scope.parentResultId, $scope.entityId, $scope.parentEntityId);

        var newObject = GetNewResult();
        if ($scope.asociatedResults == null || $scope.asociatedResults == undefined) {
            $scope.asociatedResults = [];
        }

        $scope.asociatedResults.push(newObject);
        $scope.setTotal();
        $scope.IsViewMode = false;
        $scope.IsEditingMode = true;

    };

    $scope.EditResultMode = function () {
        $scope.deleteItems = false;
        $scope.IsViewMode = false;
        $scope.IsEditingMode = true;

        for (var result in $scope.asociatedResults) {
            $scope.asociatedResults[result].isDeleted = false;
            $scope.asociatedResults[result].isNew = false;
            $scope.asociatedResults[result].isEdited = true;
        }

    }

    $scope.EditResult = function (result) {
        $scope.IsEditingMode = true;
        $scope.IsViewMode = false;
        result.isNew = false;
        result.isEdited = true;
    }

    $scope.cancelModification = function () {
        $scope.IsEditingMode = false;
        $scope.IsViewMode = true;
        $scope.LoadAsociatedResults($scope.parentResultId, $scope.entityId, $scope.parentEntityId);
    }

    // cancel all changes
    $scope.cancel = function () {
        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
    };

    // save edits
    $scope.saveTable = function () {
        var SaveResult = true;
        for (var result in $scope.asociatedResults) {
            console.log($scope.asociatedResults);

            // mark as new
            if ($scope.asociatedResults[result].isNew) {
                //if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
                //    ($scope.asociatedResults[result].data.alternativeConcept != undefined &&
                //        $scope.asociatedResults[result].data.alternativeConcept != null && $scope.asociatedResults[result].data.alternativeConcept != '')) {
                //    $scope.asociatedResults[result].data.concept = "22 Otros: " + String($scope.asociatedResults[result].data.concept).replace("22 Otros: ", "").replace("22-Otros", "") + $scope.asociatedResults[result].data.alternativeConcept;
                //}

                if (gridService.insertResult($scope.asociatedResults[result]) == true) {
                    $scope.asociatedResults[result].isNew = false;
                }
                else { SaveResult = false;}
            } else if ($scope.asociatedResults[result].isNew == false || $scope.asociatedResults[result].isDeleted == false) {
                //if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
                //    ($scope.asociatedResults[result].data.alternativeConcept != undefined &&
                //        $scope.asociatedResults[result].data.alternativeConcept != null && $scope.asociatedResults[result].data.alternativeConcept != '')) {
                //    $scope.asociatedResults[result].data.concept = "22 Otros: " + String($scope.asociatedResults[result].data.concept).replace("22 Otros: ", "").replace("22-Otros", "") + $scope.asociatedResults[result].data.alternativeConcept;

                //}
                if (gridService.saveResult($scope.asociatedResults[result]) == true) {
                    $scope.asociatedResults[result].isEdited = false;
                }
                else {
                    SaveResult = false;}
            }

        }
        return SaveResult;
    };




    // save edits
    $scope.validateTable = function () {
        try {

            var msg = '';
            var array = [];
            for (var result in $scope.asociatedResults) {
                console.log($scope.asociatedResults);

                if ($scope.asociatedResults[result].data.cuit == undefined) $scope.asociatedResults[result].data.cuit = '';
                if ($scope.asociatedResults[result].data.cbu == undefined) $scope.asociatedResults[result].data.cbu = '';
                if ($scope.asociatedResults[result].data.email == undefined) $scope.asociatedResults[result].data.email = '';

                if (validateRequeredFields($scope.asociatedResults[result]) == true && $scope.getValidate($scope.asociatedResults[result]) == true && $scope.getValidateEmail($scope.asociatedResults[result].data.email) == true && $scope.getValidateCuit($scope.asociatedResults[result].data.cuit) == true && $scope.getValidateCuitChars($scope.asociatedResults[result].data.cuit) == true) {
                    if ($scope.asociatedResults[result].data.concept == "22-Otros") {
                        if ($scope.asociatedResults[result].data.alternativeConcept == undefined ||
                            $scope.asociatedResults[result].data.alternativeConcept == null || $scope.asociatedResults[result].data.alternativeConcept == '') {
                            msg = "Por favor, ingrese concepto alternativo";
                            window.localStorage.setItem("valido", "false");
                            $scope.IsEditingMode = true;
                            $scope.IsViewMode = false;
                            return msg;
                        }
                    }

                    if ($scope.asociatedResults[result].data.concept == "12-IVA Deposito") {
                        if ($scope.asociatedResults[result].data.cuitpro == undefined ||
                            $scope.asociatedResults[result].data.cuitpro == null || $scope.asociatedResults[result].data.cuitpro == '') {
                            msg = "Por favor, ingrese el cuit del profesional";
                            window.localStorage.setItem("valido", "false");
                            $scope.IsEditingMode = true;
                            $scope.IsViewMode = false;
                            return msg;
                        }
                        else if ($scope.ValidateCuit($scope.asociatedResults[result].data.cuitpro) == false) {
                            msg = "El campo CUIT Profesional debe tener 11 digitos";
                            window.localStorage.setItem("valido", "false");
                            $scope.IsEditingMode = true;
                            $scope.IsViewMode = false;
                            return msg;

                        }
                        else if ($scope.ValidateCuitChars($scope.asociatedResults[result].data.cuitpro) == false) {
                            msg = "El campo CUIT no debe contener ni puntos (.), ni guiones (-)";
                            window.localStorage.setItem("valido", "false");
                            $scope.IsEditingMode = true;
                            $scope.IsViewMode = false;
                            return msg;

                        }


                        //  $scope.asociatedResults[result].data.concept = "22 Otros: " + String($scope.asociatedResults[result].data.concept).replace("22 Otros: ", "").replace("22-Otros", "") + $scope.asociatedResults[result].data.alternativeConcept;


                    }
                    var find = $scope.asociatedResults.length;
                    if ($scope.asociatedResults[result].data.amount == "" || $scope.asociatedResults[result].data.favorTo == null || $scope.asociatedResults[result].data.cuit == null || $scope.asociatedResults[result].data.email == null) {
                        $scope.asociatedResults[result].IsComplete = false;
                        msg = "Por favor Cargar todos los datos en beneficiarios";
                        window.localStorage.setItem("valido", "false");
                        $scope.IsEditingMode = true;
                        $scope.IsViewMode = false;

                        return msg;
                    }
                    else if (result == find - 1 && $scope.asociatedResults[result].data.amount != "" && $scope.asociatedResults[result].data.favorTo != null && $scope.asociatedResults[result].data.cuit != null && $scope.asociatedResults[result].data.email != null) {
                        $scope.asociatedResults[result].IsComplete = true;
                        window.localStorage.setItem("valido", "true");
                        $scope.IsEditingMode = false;
                        $scope.IsViewMode = true;

                        return msg;
                    }



                } else {

                    $scope.asociatedResults[result].IsComplete = false;
                    window.localStorage.setItem("valido", "false");
                    $scope.IsEditingMode = true;
                    $scope.IsViewMode = false;
                    msg = "Por favor Cargar todos los datos en beneficiarios";
                    return msg;




                }

            }

            return '';

        } catch (e) {
            return e;
        }
    };




    function validateRequeredFields(result) {
        var isValidated = false;
        var IsValidatedEmptyCbu = false;
        var isValidatedCuit = false;

        if (result.data.payMethod == undefined || result.data.payMethod == '' || result.data.payMethod == null) {
            return false;
        }

        if (result.data.cbu == '' || result.data.cbu == null) {
            IsValidatedEmptyCbu = true;
        } else {
            IsValidatedEmptyCbu = false;
        }

        if (IsValidatedEmptyCbu) {
            isValidatedCbu = result.data.payMethod != "2-Transferencia CBU Cuenta Bancaria" && result.data.payMethod != "3-Transferencia CBU Cuenta Judicial";
        } else {
            isValidatedCbu = true;
        }



        if (result.data.cuit == '' || result.data.cuit == null) {
            isValidatedCuit = (result.data.payMethod == "3-Transferencia CBU Cuenta Judicial" ||
                result.data.concept == "6-Tasa de justicia" ||
                result.data.concept == "7-Sobretasa" ||
                result.data.concept == "12-IVA Deposito" ||
                result.data.concept == "14-Aporte abogado Actor" ||
                result.data.concept == "15-Aportes Mediador" ||
                result.data.concept == "16-Aportes estudio Externo" ||
                result.data.concept == "17-Aportes Perito Ingeniero" ||
                result.data.concept == "18-Aportes Perito Medico" ||
                result.data.concept == "19-Aportes Perito Psicologo" ||
                result.data.concept == "20-Aportes Perito Contador" ||
                result.data.concept == "13-Deposito por Recurso");
        } else {
            isValidatedCuit = true;
        }

        var isDataValidate = result.data.favorTo != '' &&
            result.data.payMethod != '' &&
            result.data.concept != '' &&
            result.data.amount != '' && (result.data.email != undefined || result.data.email != '');

        if (isDataValidate && isValidatedCbu && isValidatedCuit) {
            isValidated = true;
        }

        return isValidated;
    }


    $scope.getValidate = function (result) {

        if (result.data.cbu != undefined && String(result.data.cbu).length != 22 && (result.data.payMethod == "2-Transferencia CBU Cuenta Bancaria" && result.data.payMethod == "3-Transferencia CBU Cuenta Judicial")) {
            swal("", "El campo CBU debe tener 22 digitos", "warning");
            return false;
        }
        return true;
    }

    $scope.ValidateCBURequiered = function (result) {
        if (result.data.cbu != undefined && String(result.data.cbu).length > 0 && (result.data.payMethod == "1-Cheque" || result.data.payMethod == "5-Cheque Certificado" || result.data.payMethod == "6-Cheque Imputado")) {
            swal("", "Ha seleccionado un metodo de pago, que no requiere CBU y el mismo esta cargado.", "warning");
            return false;
        }
        return true;
    }

    $scope.ValidateAmountRequiered = function (result) {
        if (result.data.amount != undefined && String(result.data.amount).length > 0 && parseFloat(String(result.data.amount).replace(".", "").replace(",", ".")) == 0) {
            // swal("", "El Importe no puede ser cero", "warning");
            result.data.amount = '';
            return false;
        }
        return true;
    }

    $scope.getValidateCuit = function (result) {
        if (result != undefined && result != null && String(result).length != 11 && String(result).length > 0) {
            swal("", "El campo CUIT debe tener 11 digitos", "warning");
            return false;
        }
        return true;
    }

    $scope.getValidateCuitChars = function (result) {
        if (String(result).contains(".") || String(result).contains("-")) {
            swal("", "El campo CUIT no debe contener ni puntos (.), ni guiones (-)", "warning");
            return false;
        }
        return true;
    }

    $scope.ValidateCuit = function (result) {
        if (result != undefined && result != null && String(result).length != 11 && String(result).length > 0) {
            //swal("", "El campo CUIT debe tener 11 digitos", "warning");
            return false;
        }
        return true;
    }

    $scope.ValidateCuitChars = function (result) {
        if (String(result).contains(".") || String(result).contains("-")) {
            // swal("", "El campo CUIT no debe contener ni puntos (.), ni guiones (-)", "warning");
            return false;
        }
        return true;
    }

    $scope.getValidateEmail = function (result) {
        if (result == undefined && result == undefined && result == null || String(result).contains("@") == false && String(result).contains(".") == false) {
            swal("", "El campo E-MAIL debe ser una dirección valida", "info");
            return false;
        }
        return true;

    }

    function GetNewResult() {
        var result = {
            id: null,
            parentResultId: $scope.parentResultId,
            parentEntityId: $scope.parentEntityId,
            entityId: $scope.entityId,
            data: {
                favorTo: '',
                commitmentNumber: '',
                payMethod: '',
                cbu: '',
                cuit: '',
                concept: '',
                amount: '',
                email: '',
                personNumber: '',
                alternativeConcept: '',
                cuitpro: '',
				estadoCompromiso: '',
				tentativaPago: '',
            },
            isDeleted: false,
            isNew: true,
            isEdited: false,
            num: $scope.asociatedResults.length + 1
        };
        return result;
    }

    //swal("", "Por favor, ingrese el CUIT", "error");

    $scope.ValidateCharacters = function (numberOfCharactersRequered, valueToValidate, nameinput) {
        ValidateNumberOfCharacters(numberOfCharactersRequered, valueToValidate, nameinput)
    };
});

app.directive('ngConfirmClick', [
    function () {
        return {
            link: function (scope, element, attr) {
                var msg = attr.ngConfirmClick || "Are you sure?";
                var clickAction = attr.confirmedClick;
                element.bind('click', function (event) {


                    swal("Estas seguro de eliminar este beneficiario?", {
                        buttons: {
                            cancel: "Cancelar",
                            eliminar: "Eliminar",
                        },
                        icon: "warning"
                    }
                    ).then(function (value) {
                        switch (value) {

                            case "eliminar":
                                scope.$eval(clickAction);
                                swal("Beneficiario Eliminado!", "", "success");
                                break;
                            case "Cancelar":
                                break;
                            default:
                                break;
                        }
                    });




                    //swal({
                    //    title: 'Confirmacion?',
                    //    text: "Estas seguro de eliminar este beneficiario?",
                    //    type: 'warning',
                    //    showCancelButton: true,
                    //    confirmButtonColor: '#3085d6',
                    //    cancelButtonColor: '#d33',
                    //    confirmButtonText: 'Eliminar'
                    //}).then(function () {
                    //    scope.$eval(clickAction);
                    //    swal(
                    //        '',
                    //        'Beneficiario Eliminado.',
                    //        'success'
                    //    );
                    //});
                });
            }
        };
    }])

app.directive('zambaGrid', function ($sce) {
    return {
        restrict: 'E',
        //scope: false,
        //        replace: false,
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId;
            $scope.isReadOnly = attributes.readOnly == 'true';
            $scope.isEditOnly = attributes.editOnly == 'true';


            $scope.formName = attributes.formName;

            $scope.BenefAllInputEnable = false;
            $scope.BenefNroPersonaEnable = false;
            $scope.BenefCompromisoEnable = false;
            $scope.BenefFTP = '';
            $scope.BenefEDC = '';

            switch ($scope.formName) {
                case "PF":
                    $scope.BenefAllInputEnable = false;
                    $scope.BenefNroPersonaEnable = false;
                    $scope.BenefCompromisoEnable = false;
                    $scope.BenefFTP = '';
                    $scope.BenefEDC = '';
                    break;
                case "PFPreLiq":
                    $scope.BenefAllInputEnable = true;
                    $scope.BenefNroPersonaEnable = true;
                    $scope.BenefCompromisoEnable = false;
                    $scope.BenefFTP = '';
                    $scope.BenefEDC = '';

                    break;
                case "PFLiq":
                    $scope.BenefAllInputEnable = true;
                    $scope.BenefNroPersonaEnable = true;
                    $scope.BenefCompromisoEnable = true;
                    $scope.BenefFTP = '';
                    $scope.BenefEDC = '';
                    break;
                case "PFCons":
                    $scope.BenefAllInputEnable = true;
                    $scope.BenefNroPersonaEnable = false;
                    $scope.BenefCompromisoEnable = false;
                    $scope.BenefFTP = '';
                    $scope.BenefEDC = '';
                    break;


            }



            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { $scope.parentResultId = valor.split("=")[1]; }

            });
            $scope.parentEntityId = "2544,1020129";
            $scope.LoadAsociatedResults($scope.parentResultId, $scope.entityId, $scope.parentEntityId);
        },
        templateUrl: $sce.getTrustedResourceUrl("../../Grid/PedidoFondos5/PedidosFondosEditGrid.html?v=249")
    };
})


function getElementFromQueryString(element) {
    var url = window.location.href;
    var segments = url.split("&");
    var value = null;
    segments.forEach(function (valor) {
        if (valor.includes(element)) { value = valor.split("=")[1]; }
    });
    return value;
}

function setTagValueById(tagId, value) {
    $("#" + tagId).val(value);
}




//enviar
function ValidarDatosFaltantes() {

    try {
        var scope = angular.element($("#appID")).scope();
    SaveFormData();

    //Validaciones para submit(save).
    var valido = false;

    if ($("#zamba_index_10287")[0].checked == false && ($("#zamba_index_2719").val() == '' || $("#zamba_index_2719").val() == 0 || $("#zamba_index_2719").val() == '0,00' || $("#zamba_index_2719").val() == '0.00')) {
        swal("", "Por favor,carga Estimacion de Pagos");
         valido = false;

    } else if ($("#zamba_index_10284").val() == "") {
        swal("", "Por favor, ingresa Estado.");
        valido = false;
    } else if ($("#hdnCount").val() == 0) {
        swal("", "Por favor,carga al menos un beneficiario");
        valido = false;



    } else if ($("#zamba_index_1020168").val() == "") {
        swal("", "Por favor,carga la fecha de pago");
        valido = false;
    } else if (ValidarFechaMayorIgualActual($("#zamba_index_1020168").val())) {
        swal("", "Por favor, carga una Fecha de Pago Valida.");
        valido = false;
    }

    else {
       

        if (scope.saveTable() == true) {
            var msg = scope.validateTable();
            if (msg == '') {
                swal("Enviando Pedido...", "", "success");

                window.localStorage.removeItem("valido");
                valido = true;
            }
            else {
                swal("", msg, "error");
                valido = false;
                window.localStorage.removeItem("valido");
               
                
            }
        }
        else {
            swal("No se pudieron guardar los datos, por favor llamar a la mesa de ayuda", "", "warning");
          
            valido = false;
        }
        }

        if (valido == false)
            scope.LoadAsociatedResults(scope.parentResultId, scope.entityId, scope.parentEntityId);

    return valido;

    } catch (e) {
        console.error(e);
       
        return false;
    }

    }

function ValidarFechaMayorIgualActual(valueDate) {
    var currentDate = new Date();
    var dateParts = valueDate.split('/');
    var validateDate = new Date(dateParts[1] + "/" + dateParts[0] + "/" + dateParts[2]);

    if (Date.parse(currentDate) >= Date.parse(validateDate)) {
        return true;
    } else {
        return false;
    }
}

function SaveFormData() {

    try {
        var entityId = GetDocTypeId();
        var parentResultId = GetDOCID();
        var taskId = GetTASKID();

        if (entityId == null) {
                       entityId = 0;
        }


        saveIndexValidated(10284, entityId, parentResultId, taskId, $("#zamba_index_10284").val());
        saveIndexValidated(1020168, entityId, parentResultId, taskId, $("#zamba_index_1020168").val());


        saveIndexValidated(2793, entityId, parentResultId, taskId, $("#zamba_index_2793").val());
        saveIndexValidated(10293, entityId, parentResultId, taskId, $("#zamba_index_10293").val());
        saveIndexValidated(10294, entityId, parentResultId, taskId, $("#zamba_index_10294").val());
        saveIndexValidated(10295, entityId, parentResultId, taskId, $("#zamba_index_10295").val());
        saveIndexValidated(10296, entityId, parentResultId, taskId, $("#zamba_index_10296").val());
        saveIndexValidated(1025183, entityId, parentResultId, taskId, $("#zamba_index_1025183").val());
        saveIndexValidated(10297, entityId, parentResultId, taskId, $("#zamba_index_10297").val());
        saveIndexValidated(2801, entityId, parentResultId, taskId, $("#zamba_index_2801").val());
        saveIndexValidated(10298, entityId, parentResultId, taskId, $("#zamba_index_10298").val());
        saveIndexValidated(10288, entityId, parentResultId, taskId, $("#zamba_index_10288").val());
        saveIndexValidated(10319, entityId, parentResultId, taskId, $("#zamba_index_10319").val());
        saveIndexValidated(2719, entityId, parentResultId, taskId, $("#zamba_index_2719").val());


        if ($("#zamba_index_10287").attr("checked") == 'checked') {
            saveIndexValidated(10287, entityId, parentResultId, taskId, $("#zamba_index_10287"), 1);
        }
        else {
            saveIndexValidated(10287, entityId, parentResultId, taskId, $("#zamba_index_10287"), 0);
        }

    } catch (e) {

    }
}


function GuardarBorrador() {
    var scope = angular.element($("#appID")).scope();
    if (scope.saveTable() == true) {
        swal("Datos guardados en Borrador", "", "success");
        SetRuleId(this);
        return true;

    }
    else {
        swal("No se pudieron guardar los datos, por favor llamar a la mesa de ayuda", "", "warning");
        return false;
    }
}


function validateBenef() {
    var valido = false;
    if (window.localStorage.getItem("valido").contains("true")) {
        SetRuleId(sender);
        valido = true;
        window.localStorage.removeItem("valido");
    } else {
        swal("", "Por favor Cargar todos los Campos de beneficiarios", "error");
        valido = false;
        window.localStorage.removeItem("valido");
    }
    return valido;

}