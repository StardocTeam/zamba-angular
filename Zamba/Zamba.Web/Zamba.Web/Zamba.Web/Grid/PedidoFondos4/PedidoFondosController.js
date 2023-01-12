//var app = angular.module("app", ["xeditable", 'angular.filter']);

//app.run(function (editableOptions) {
//    editableOptions.theme = 'bs3';
//});


app.controller('BenefCtrl', function ($scope, $filter, $http, gridService) {

    $scope.IsInsertingMode = false;
    $scope.IsViewMode = false;
    $scope.IsEditingMode = false;
    $scope.deleteItems = false;
    $scope.firtTime = false;

    $scope.asociatedResults = [];

    $scope.LoadAsociatedResults = function (parentResultId, parentResultEntityId, associatedIds) {

        var d = gridService.getAsociatedResults(parentResultId, parentResultEntityId, associatedIds);
        if (d != 'null') {

            var associatedResults = getFormattedResults(JSON.parse(d));
            //            .done(function (d) {
            if (associatedResults == "") {
                console.log("No se pudo obtener los asociados");
                var newObj = GetNewResult();
                $scope.asociatedResults = [];
                $scope.setTotal();
                $scope.asociatedResults.push(newObj);
                $scope.firtTime = true;
                $scope.IsInsertingMode = true;
                $scope.IsViewMode = false;

                return;
            }
            else {
                $scope.firtTime = false;
                $scope.asociatedResults = associatedResults;
                $scope.setTotal();
                $scope.IsViewMode = true;
            }
            //}).error(function (err) {
            //    console.log(err);
            //    return;
            //});
        }
        setTagValueById("hdnCount", $scope.asociatedResults.length);
    };

    $scope.sorterFunc = function (results) {
        return parseInt(results.data.concept)
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
            swal("", "Debe completar el campo importe con un numero", "info");
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
                amount: currentAmount,
                email: AssociatedResult.MAIL,
                personNumber: AssociatedResult.NRO_DE_PERSONA
            },
            isDeleted: false,
            isNew: false,
            isEdited: false,
            IsComplete: false
        };
        return result;
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
        result.isNew = false;
        result.isEdited = false;
        result.isDeleted = true;

        gridService.deleteResult(result);

        for (var i = 0; i < $scope.asociatedResults.length; i++) {
            if ($scope.asociatedResults[i].id === result.id) {
                $scope.asociatedResults.splice(i, 1);
            }
        }

        $scope.setTotal();
        setTagValueById("hdnCount", $scope.asociatedResults.length);

        //$scope.deleteItems = true;
        //$scope.saveTable();
        //$scope.LoadAsociatedResults($scope.parentResultId, $scope.entityId, $scope.parentEntityId);
        $scope.$apply();
    };

    $scope.addResult = function () {

        $scope.saveTable();

        $scope.IsInsertingMode = true;
        $scope.IsViewMode = false;
        $scope.IsEditMode = false;

        var newObject = GetNewResult();
        if ($scope.asociatedResults == null || $scope.asociatedResults == undefined || $scope.firtTime) {
            $scope.asociatedResults = [];
        }

        $scope.asociatedResults.push(newObject);
    };

    $scope.addResultConcept = function (benef) {

        $scope.saveTable();

        $scope.IsInsertingMode = true;
        $scope.IsViewMode = false;
        $scope.IsEditMode = false;

        var newObject = GetNewResultConcept(benef);
        if ($scope.asociatedResults == null || $scope.asociatedResults == undefined || $scope.firtTime) {
            $scope.asociatedResults = [];
        }

        $scope.asociatedResults.push(newObject);
    };

    $scope.MergeBenef = function (benef) {

        for (var i in $scope.asociatedResults) {

            var result = $scope.asociatedResults[i];
            if ((benef.id == undefined || result.id == undefined || benef.id != result.id) && benef.data.cuit == result.data.cuit) {
                if ((result.data.favorTo == null || result.data.favorTo == '') && (benef.data.favorTo != null || benef.data.favorTo != '')) {
                    result.data.favorTo = benef.data.favorTo;
                    result.isEdited = true;
                }
                if ((result.data.favorTo != null || result.data.favorTo != '') && (benef.data.favorTo == null || benef.data.favorTo == '')) {
                    benef.data.favorTo = result.data.favorTo;
                }

                if ((result.data.email == null || result.data.email == '') && (benef.data.email != null || benef.data.email != '')) {
                    result.data.email = benef.data.email;
                    result.isEdited = true;
                }
                if ((result.data.email != null || result.data.email != '') && (benef.data.email == null || benef.data.email == '')) {
                    benef.data.email = result.data.email;
                }

                if ((result.data.cbu == null || result.data.cbu == '') && (benef.data.cbu != null || benef.data.cbu != '')) {
                    result.data.cbu = benef.data.cbu;
                    result.isEdited = true;
                }
                if ((result.data.cbu != null || result.data.cbu != '') && (benef.data.cbu == null || benef.data.cbu == '')) {
                    benef.data.cbu = result.data.cbu;
                }

                if ((result.data.personNumber == null || result.data.personNumber == '') && (benef.data.personNumber != null || benef.data.personNumber != '')) {
                    result.data.personNumber = benef.data.personNumber;
                    result.isEdited = true;
                }
                if ((result.data.personNumber != null || result.data.personNumber != '') && (benef.data.personNumber == null || benef.data.personNumber == '')) {
                    benef.data.personNumber = result.data.personNumber;
                }

                if ((result.data.payMethod == null || result.data.payMethod == '') && (benef.data.payMethod != null || benef.data.payMethod != '')) {
                    result.data.payMethod = benef.data.payMethod;
                    result.isEdited = true;
                }
                if ((result.data.payMethod != null || result.data.payMethod != '') && (benef.data.payMethod == null || benef.data.payMethod == '')) {
                    benef.data.payMethod = result.data.payMethod;
                }

                $scope.saveTable();
                $scope.IsViewMode = false;
                $scope.IsInsertingMode = false;
                $scope.IsEditingMode = true;
            }
        }

    };

    $scope.EditResultMode = function () {
        $scope.IsInsertingMode = false;
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
        $scope.IsInsertingMode = false;
        result.isNew = false;
        result.isEdited = true;
    }

    $scope.cancelModification = function () {
        $scope.IsEditingMode = false;
        $scope.IsInsertingMode = false;
        $scope.IsViewMode = true;
        $scope.LoadAsociatedResults($scope.parentResultId, $scope.entityId, $scope.parentEntityId);
    }

    // cancel all changes
    $scope.cancel = function () {
        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
    };

    // save edits
    $scope.saveTable = function () {
        var array = [];
        for (var result in $scope.asociatedResults) {
            console.log($scope.asociatedResults);

            // mark as new
            if ($scope.asociatedResults[result].isNew) {
                if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
                    $scope.asociatedResults[result].data.alternativeConcept != null) {
                    $scope.asociatedResults[result].data.concept = $scope.asociatedResults[result].data.alternativeConcept;
                }

                gridService.insertResult($scope.asociatedResults[result]);
                $scope.asociatedResults[result].isNew = false;
            }

            // mark as not new
            if ($scope.asociatedResults[result].isEdited) {
                if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
                    $scope.asociatedResults[result].data.alternativeConcept != null) {
                    $scope.asociatedResults[result].data.concept = $scope.asociatedResults[result].data.alternativeConcept;
                }
                gridService.saveResult($scope.asociatedResults[result]);
                $scope.asociatedResults[result].isEdited = false;
            }

        }
        for (var i = 0; i < array.length; i++) {
            $scope.asociatedResults.splice(array[i], 1);
        }
        $scope.IsInsertingMode = false;
        $scope.IsEditingMode = false;
        $scope.deleteItems = false;
        $scope.LoadAsociatedResults($scope.parentResultId, $scope.entityId, $scope.parentEntityId);
        $scope.IsViewMode = true;
        return true;
    };

    // save edits
    $scope.validateTable = function () {
        var array = [];
        for (var result in $scope.asociatedResults) {
            console.log($scope.asociatedResults);

            $scope.MergeBenef($scope.asociatedResults[result]);

            if (validateRequeredFields($scope.asociatedResults[result])) {
                if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
                    $scope.asociatedResults[result].data.alternativeConcept != null) {
                    if ($scope.asociatedResults[result].data.alternativeConcept == null) {
                        swal("", "Por favor, ingrese concepto alternativo", "error");
                        localStorage.setItem("valido", "false");
                        return false;
                    }
                    $scope.asociatedResults[result].data.concept = $scope.asociatedResults[result].data.alternativeConcept;

                }
                $scope.asociatedResults[result].IsComplete = true;

                localStorage.setItem("valido", "true");
                return true;

            } else {
                $scope.asociatedResults[result].IsComplete = false;
                swal("", "Por favor Cargar todos los datos en beneficiarios");
                localStorage.setItem("valido", "false");
                return false;
            }


        }

    };


    //// save edits
    //$scope.saveTable = function () {
    //    var array = [];
    //    for (var result in $scope.asociatedResults) {
    //        console.log($scope.asociatedResults);
    //        // actually delete user
    //        if ($scope.asociatedResults[result].isDeleted) {
    //            gridService.deleteResult($scope.asociatedResults[result]);
    //            //
    //            array.push(result);
    //        }

    //        // mark as new
    //        if ($scope.asociatedResults[result].isNew) {
    //            if (validateRequeredFields($scope.asociatedResults[result])) {
    //                if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
    //                    $scope.asociatedResults[result].data.alternativeConcept != null) {
    //                    if ($scope.asociatedResults[result].data.alternativeConcept == null) {
    //                        swal("", "Por favor, ingrese concepto alternativo", "error");
    //                        localStorage.setItem("valido", "false");
    //                        return false;
    //                    }
    //                    $scope.asociatedResults[result].data.concept = $scope.asociatedResults[result].data.alternativeConcept;

    //                }
    //                gridService.insertResult($scope.asociatedResults[result]);

    //                $scope.asociatedResults[result].isNew = false;
    //                localStorage.setItem("valido", "true");

    //            } else {

    //                swal("", "Por favor Cargar todos los datos en beneficiarios");
    //                localStorage.setItem("valido", "false");
    //                return false;


    //            }

    //        }

    //        // mark as not new
    //        if ($scope.asociatedResults[result].isEdited) {
    //            //gridService.saveResult($scope.asociatedResults[result].data, $scope.asociatedResults[result].id);
    //            if (validateRequeredFields($scope.asociatedResults[result])) {
    //                if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
    //                    $scope.asociatedResults[result].data.alternativeConcept != null) {
    //                    if ($scope.asociatedResults[result].data.alternativeConcept == null) {
    //                        swal("", "Por favor, ingrese concepto alternativo", "error");
    //                        localStorage.setItem("valido", "false");
    //                        return false;
    //                    }
    //                    $scope.asociatedResults[result].data.concept = $scope.asociatedResults[result].data.alternativeConcept;

    //                }
    //                gridService.saveResult($scope.asociatedResults[result]);

    //                $scope.asociatedResults[result].isEdited = false;
    //            } else {
    //                swal("", "Por favor, ingrese todos los campos", "error");
    //                localStorage.setItem("valido", "false");
    //                return false;
    //            }

    //        }

    //    }
    //    for (var i = 0; i < array.length; i++) {
    //        $scope.asociatedResults.splice(array[i], 1);
    //    }
    //    $scope.IsInsertingMode = false;
    //    $scope.IsEditingMode = false;
    //    $scope.deleteItems = false;
    //    $scope.LoadAsociatedResults($scope.parentResultId, $scope.entityId, $scope.parentEntityId);
    //    $scope.IsViewMode = true;
    //};


    function validateRequeredFields(result) {
        var isValidated = false;
        var IsValidatedEmptyCbu = false;
        var isValidatedCuit = false;

        if (result.data.cbu == '' || result.data.cbu == null) {
            IsValidatedEmptyCbu = true;
        } else {
            IsValidatedEmptyCbu = false;
        }

        if (IsValidatedEmptyCbu) {
            isValidatedCbu = result.data.payMethod == "1-Cheque" || result.data.payMethod == "4-Compensación Contable" || result.data.payMethod == "5-Cheque Certificado" || result.data.payMethod == "6-Cheque Imputado";
        } else {
            isValidatedCbu = true;
        }

        //var isValidatedCbu = ((result.data.payMethod == "1-Cheque" || result.data.payMethod == "4-Compensación Contable") && !IsValidatedEmptyCbu);
        //var isValidatedCuit = (result.data.payMethod == "3-Transferencia CBU Cuenta Judicial" && result.data.cuit == '')

        //var isValidatedCuit = ((result.data.payMethod == "3-Transferencia CBU Cuenta Judicial" ||
        //    result.data.concept == "7-Tasa de justicia" ||
        //    result.data.concept == "8-Sobretasa" ||
        //    result.data.concept == "13-IVA Deposito" ||
        //    result.data.concept == "15-Aporte abogado Actor" ||
        //    result.data.concept == "16-Aportes Mediador" ||
        //    result.data.concept == "17-Aportes estudio Externo" ||
        //    result.data.concept == "18-Aportes Perito Ingeniero" ||
        //    result.data.concept == "19-Aportes Perito Medico" ||
        //    result.data.concept == "20-Aportes Perito Psicologo" ||
        //    result.data.concept == "21-Aportes Perito Contador" ||
        //    result.data.concept == "14-Deposito por Recurso" ||
        //    result.data.concept == "13-IVA Deposito");

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
            result.data.amount != '' &&
            result.data.email != '';

        if (isDataValidate && isValidatedCbu && isValidatedCuit) {
            isValidated = true;
        }

        return isValidated;
    }


    $scope.getValidate = function (result) {

        if (String(result).length < 22 && String(result).length > 0 && result != null) {
            swal("", "El campo CBU debe tener 22 digitos", "warning");
            return;
        }


    }
    $scope.getValidateCuit = function (result) {
        if (String(result).length < 11 && String(result).length > 0 && result != null) {
            swal("", "El campo CUIT debe tener 11 digitos", "warning");
            return;
        }

    }
    $scope.getValidateCuitChars = function (result) {
        if (String(result).contains(".") || String(result).contains("-")) {
            swal("", "El campo CUIT no debe contener ni puntos (.), ni guiones (-)", "warning");
            return;
        }
    }
    $scope.getValidateEmail = function (result) {
        if (String(result).contains("@")) {

        }
        else {
            swal("", "El campo debe contener un signo '@', un punto '.' y debe ser una dirección de email valido", "info");
            return;
        }

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
                personNumber: ''
            },
            isDeleted: false,
            isNew: true,
            isEdited: false
        };
        return result;
    }

    function GetNewResultConcept(benef) {
        var result = {
            id: null,
            parentResultId: $scope.parentResultId,
            parentEntityId: $scope.parentEntityId,
            entityId: $scope.entityId,
            data: {
                favorTo: benef.data.favorTo,
                commitmentNumber: benef.data.commitmentNumber,
                payMethod: benef.data.payMethod,
                cbu: benef.data.cbu,
                cuit: benef.data.cuit,
                concept: '',
                amount: '',
                email: benef.data.email,
                personNumber: benef.data.personNumber
            },
            isDeleted: false,
            isNew: true,
            isEdited: false
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
                            eliminar: "Eliminar"
                        },
                        icon: "warning"
                    }).then(function (value) {
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
        templateUrl: $sce.getTrustedResourceUrl("../../Grid/PedidoFondos4/PedidosFondosEditGrid.html")
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




