//var app = angular.module("ZambaSignFileApp", []);

app.controller('ZambaSignFileController', function ($scope, $filter, $http, ZambaSignFileService) {

    $scope.doReception = function () {
        var solicitudFirmaDigital = getNewSolicitudFirmaDigital();
        solicitudFirmaDigital.userId = parseInt(GetUID());
        setValuesFromSolicitudFirmaDigital(solicitudFirmaDigital);
        var result = ZambaSignFileService.doReception(solicitudFirmaDigital);

        if (result != null) {
            if (result.responseText != undefined) {
                swal('Error al Informar a AFIP la Recepcion, error: ' + result.responseText).then(function (result) { window.location.reload(); });
            }
            else {
                if (result != undefined && (result.result != undefined || result.Message != undefined || result.status != undefined)) {
                }
                else {
                    result = JSON.parse(result);
                }
                if (result != undefined && result.result != undefined && (result.result == 2)) {
                    if (result.RecepcionResponse.codError == 0) {
                        //zamba_rule_165720
                        var entityId = getElementFromQueryString("DocType");
                        var ResultId = getElementFromQueryString("docid");
                        swal('Documento Recepcionado ok en AFIP', '', 'success');
                        executeTaskRule(165720, ResultId, RefreshCurrentPage);
                      
                    }
                    else {
                        swal('Error al Informar a AFIP la Recepcion, error: ' + result.RecepcionResponse.codError + ': ' + result.RecepcionResponse.descError).then(function (result) { window.location.reload(); });
                    }
                }
                else if (result != undefined && result.result != undefined && result.result == 3) {
                    //zamba_rule_165720
                    var entityId = getElementFromQueryString("DocType");
                    var ResultId = getElementFromQueryString("docid");
                    swal('El legajo ya habia sido Recepcionado ok en AFIP previamente', '', 'success');
                    executeTaskRule(165720, ResultId, RefreshCurrentPage);
                   
                }
                else {
                    if (result.codError != undefined) {
                        swal('Error al Informar a AFIP, error: ' + result.codError + ': ' + result.descError).then(function (result) { window.location.reload(); });
                    }
                    else if (result.Message != undefined) {
                        swal('Error al Informar a AFIP, error: ' + result.Message).then(function (result) { window.location.reload(); });
                    }
                    else {
                        swal('Error al Informar a AFIP, error: ' + result).then(function (result) { window.location.reload(); });
                    }
                }
            }
        }
        else {
            if (result.codError != undefined) {
                swal('Error al Informar a AFIP, error: ' + result.codError + ': ' + result.descError).then(function (result) { window.location.reload(); });
            }
            else if (result.Message != undefined) {
                swal('Error al Informar a AFIP, error: ' + result.Message).then(function (result) { window.location.reload(); });
            }
            else {
                swal('Error al Informar a AFIP, error: ' + result).then(function (result) { window.location.reload(); });
            }
        }
    }




    $scope.signFile = function () {
        var solicitudFirmaDigital = getNewSolicitudFirmaDigital();
        solicitudFirmaDigital.userId = parseInt(GetUID());
        setValuesFromSolicitudFirmaDigital(solicitudFirmaDigital);
        var result = ZambaSignFileService.signFile(solicitudFirmaDigital);

        if (result != null) {
            if (result.responseText != undefined) {
                swal('Error al Informar a AFIP, error: ' + result.responseText).then(function (result) { window.location.reload(); });
            }
            else {
                if (result != undefined && (result.result != undefined || result.Message != undefined || result.status != undefined)) {
                }
                else {
                    result = JSON.parse(result);
                }

                if (result != undefined && result.result != undefined && (result.result == 2)) {
                    if (result.RecepcionResponse.codError == 0) {
                        if (result.DigitalizacionResponse.codError == 0) {
                            //zamba_rule_165752
                            var entityId = getElementFromQueryString("DocType");
                            var ResultId = getElementFromQueryString("docid");
                            swal('Documento Firmado y pasado a Estado DIGI ok en AFIP', '', 'success');
                            executeTaskRule(165752, ResultId, RefreshCurrentPage);
                       
                        }
                        else {
                            swal('Error al Informar a AFIP la Digitalizacion, error: ' + result.DigitalizacionResponse.codError + ': ' + result.DigitalizacionResponse.descError).then(function (result) { window.location.reload(); });
                        }
                    }
                    else {
                        swal('Error al Informar a AFIP la Recepcion, error: ' + result.RecepcionResponse.codError + ': ' + result.RecepcionResponse.descError).then(function (result) { window.location.reload(); });
                    }
                }
                else if (result != undefined && result.result != undefined && (result.result == 3)) {
                    //zamba_rule_165752
                    var entityId = getElementFromQueryString("DocType");
                    var ResultId = getElementFromQueryString("docid");
                    swal('Documento Firmado y pasado a Estado DIGI ok en AFIP', '', 'success');
                    executeTaskRule(165752, ResultId, RefreshCurrentPage);
                  
                }
                else {
                    if (result.codError != undefined) {
                        swal('Error al Informar a AFIP, error: ' + result.codError + ': ' + result.descError).then(function (result) { window.location.reload(); });
                    }
                    else if (result.Message != undefined) {
                        swal('Error al Informar a AFIP, error: ' + result.Message).then(function (result) { window.location.reload(); });
                    }
                    else {
                        swal('Error al Informar a AFIP, error: ' + result).then(function (result) { window.location.reload(); });
                    }
                }
            }
        }
        else {
            if (result.codError != undefined) {
                swal('Error al Informar a AFIP, error: ' + result.codError + ': ' + result.descError).then(function (result) { window.location.reload(); });
            }
            else if (result.Message != undefined) {
                swal('Error al Informar a AFIP, error: ' + result.Message).then(function (result) { window.location.reload(); });
            }
            else {
                swal('Error al Informar a AFIP, error: ' + result).then(function (result) { window.location.reload(); });
            }
        }
    }



    $scope.doGetLegajo = function () {
        var solicitudFirmaDigital = getNewSolicitudFirmaDigital();
        solicitudFirmaDigital.userId = parseInt(GetUID());
        setValuesFromSolicitudFirmaDigital(solicitudFirmaDigital);
        var result = ZambaSignFileService.GetLegajo(solicitudFirmaDigital);

        if (result != null) {
            result = JSON.parse(result);
        }
        else {
            swal('Error al Informar a AFIP, error: ' + result);
        }
    }

    $scope.doGetLegajos = function () {
        var solicitudFirmaDigital = getNewSolicitudFirmaDigital();
        solicitudFirmaDigital.userId = parseInt(GetUID());
        setValuesFromSolicitudFirmaDigital(solicitudFirmaDigital);
        var result = ZambaSignFileService.doGetLegajos(solicitudFirmaDigital);

        if (result != null) {
            result = JSON.parse(result);
        }
        else {
            swal('Error al Informar a AFIP, error: ' + result);
        }
    }




    function getNewSolicitudFirmaDigital() {
        return {
            nroLegajo: null,
            cuitDeclarante: null,
            cuitPSAD2: null,
            cuitIE: null,
            cuitATA: null,
            codigo: null,
            url: null,
            familias: [],
            ticket: null,
            cantidadTotal: null,
            sigea: null,
            nroReferencia: null,
            nroGuia: null,
            nroDespacho: null,
            cantidadFojas: null,
            fechaDespacho: null,
            fechaGeneracion: null,
            fechaHoraAcept: null,
            idEnvio: null,
            indLugarFisico: null,
            userId: null
        }
    }

    function setValuesFromSolicitudFirmaDigital(solicitudFirmaDigital) {
        var cuitDespachante = validateIfValueIsNull($("#zamba_index_139600").val());
        var nroDespacho = validateIfValueIsNull($("#zamba_index_139548").val());
        var codigo = validateIfValueIsNull($("#zamba_index_139603").val());
        solicitudFirmaDigital.cuitDeclarante = cuitDespachante;
        solicitudFirmaDigital.nroDespacho = nroDespacho;
        solicitudFirmaDigital.codigo = codigo;
        solicitudFirmaDigital.cantidadFojas = validateIfValueIsNull($("#zamba_index_139608").val());
    }

    function validateIfValueIsNull(value) {
        var validatedValue = null;
        if (value == null) {
            validatedValue = "";
        } else {
            validatedValue = value;
        }
        return validatedValue;
    }

});



