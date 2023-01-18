//var app = angular.module("app", ["xeditable", "angular.filter"]);

//app.run(function (editableOptions) {
//    editableOptions.theme = 'bs3';
//});


app.controller('BenefCtrl', function ($scope, $filter, $http, gridService) {

    $scope.IsInsertingMode = false;
    $scope.IsViewMode = true;
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
                return;
            }
            else {
                $scope.firtTime = false;
                $scope.asociatedResults = associatedResults;
                $scope.setTotal();
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
        result.data.amount = String(parseFloat(result.data.amount).toFixed(2)).replace('.', ',')
            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
    }

    $scope.setTotal = function () {
        var total = 0;
        if ($scope.asociatedResults != "" && $scope.asociatedResults.length > 0) {
            for (var result in $scope.asociatedResults) {
                var amount = String($scope.asociatedResults[result].data.amount).replace(".", "").replace(",", ".")
                
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
                amount: String(parseFloat(AssociatedResult.IMPORTE).toFixed(2)).replace(".", ",").replace(/\B(?=(\d{3})+(?!\d)\.?)/g, "."),
                realamount: parseFloat(AssociatedResult.IMPORTE).toFixed(2),
                email: AssociatedResult.MAIL,
                personNumber: AssociatedResult.NRO_DE_PERSONA
            },
            isDeleted: false,
            isNew: false,
            isEdited: false
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
        "7-Sobretasa", "9-Honorarios perito ingeniero",
        "9-Honorarios perito Medico", "10-Honorarios perito Psicologo",
        "11-Honorarios perito Contador", "12-IVA Deposito",
        "13-Deposito por Recurso", "14-Aporte abogado Actor",
        "15-Aportes Mediador", "16-Aportes estudio Externo",
        "17-Aportes Perito Ingeniero", "18-Aportes Perito Medico", "19-Aportes Perito Psicologo", "20-Aportes Perito Contador", "22-Otros"];

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
        $scope.deleteItems = true;
    };

    $scope.addResult = function () {

        $scope.IsInsertingMode = true;
        $scope.IsViewMode = false;
        $scope.IsEditMode = false;

        var newObject = GetNewResult();
        if ($scope.asociatedResults == null || $scope.asociatedResults == undefined || $scope.firtTime) {
            $scope.asociatedResults = [];
        }

        $scope.asociatedResults.push(newObject);
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
            // actually delete user
            if ($scope.asociatedResults[result].isDeleted) {
                gridService.deleteResult($scope.asociatedResults[result]);
                //
                array.push(result);
            }

            // mark as new
            if ($scope.asociatedResults[result].isNew) {
                if (validateRequeredFields($scope.asociatedResults[result])) {
                    if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
                        $scope.asociatedResults[result].data.alternativeConcept != null) {
                        if ($scope.asociatedResults[result].data.alternativeConcept == null) {
                            swal("", "Por favor, ingrese concepto alternativo", "error");
                            return;
                        }
                        $scope.asociatedResults[result].data.concept = $scope.asociatedResults[result].data.alternativeConcept;
                       
                    }
                    gridService.insertResult($scope.asociatedResults[result]);
                    swal("", "Por favor, Para enviar la solicitud debe oprimir el boton enviar al final del formulario", "info");
                    $scope.asociatedResults[result].isNew = false;
                } else {
                    swal("", "Por favor, ingrese todos los campos", "error");
                    return;
                }
               
            }

            // mark as not new
            if ($scope.asociatedResults[result].isEdited) {
                //gridService.saveResult($scope.asociatedResults[result].data, $scope.asociatedResults[result].id);
                if (validateRequeredFields($scope.asociatedResults[result])) {
                    if ($scope.asociatedResults[result].data.concept == "22-Otros" &&
                        $scope.asociatedResults[result].data.alternativeConcept != null) {
                        if ($scope.asociatedResults[result].data.alternativeConcept == null) {
                            swal("", "Por favor, ingrese concepto alternativo", "error");
                            return;
                        }
                        $scope.asociatedResults[result].data.concept = $scope.asociatedResults[result].data.alternativeConcept;
                       
                    }
                    gridService.saveResult($scope.asociatedResults[result]);
                    swal("","Por favor, Para enviar la solicitud debe oprimir el boton enviar al final del formulario", "info");
    
                    $scope.asociatedResults[result].isEdited = false;
                } else {
                    swal("", "Por favor, ingrese todos los campos", "error");
                    return;
                }
               
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
    };

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
                result.data.concept == "7-Tasa de justicia" ||
                result.data.concept == "8-Sobretasa" ||
                result.data.concept == "13-IVA Deposito" ||
                result.data.concept == "15-Aporte abogado Actor" ||
                result.data.concept == "16-Aportes Mediador" ||
                result.data.concept == "17-Aportes estudio Externo" ||
                result.data.concept == "18-Aportes Perito Ingeniero" ||
                result.data.concept == "19-Aportes Perito Medico" ||
                result.data.concept == "20-Aportes Perito Psicologo" ||
                result.data.concept == "21-Aportes Perito Contador" ||
                result.data.concept == "14-Deposito por Recurso" ||
                result.data.concept == "13-IVA Deposito");
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
                realamount:0,
                email: '',
                personNumber: ''
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

            switch ($scope.formName) {
                case "PF":
                    $scope.BenefAllInputEnable = false;
                    $scope.BenefNroPersonaEnable = false;
                    $scope.BenefCompromisoEnable = false;
                    break;
                case "PFPreLiq":
                    $scope.BenefAllInputEnable = true;
                    $scope.BenefNroPersonaEnable = true;
                    $scope.BenefCompromisoEnable = false;
                    
                    break;
                case "PFLiq":
                    $scope.BenefAllInputEnable = true;
                    $scope.BenefNroPersonaEnable = true;
                    $scope.BenefCompromisoEnable = true;
                    break;
                case "PFCons":
                    $scope.BenefAllInputEnable = true;
                    $scope.BenefNroPersonaEnable = false;
                    $scope.BenefCompromisoEnable = false;
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
        templateUrl: $sce.getTrustedResourceUrl("../../Grid/PedidoFondos/PedidosFondosEditGrid.html")
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


