app.controller('insertController', function ($http, $scope, insertServices) {

    $scope.attributeId = 0;
    $scope.autoComplete = function (attributeId, attributeValue) {

        $scope.attributeId = attributeId.replace('ucDocTypesIndexs_', '');
        $("")
        var entityId = $("#ucDocTypes_DocTypes").val();
        var indexs = [];

        var oneIndexs = false;
        $(".insertAttribute").each(
            function (index, item) {
                var val = $(item).val();
                var id = $(item).attr('Id').replace('ucDocTypesIndexs_', '');
                if (val != null && val != '') {
                    indexs.push({ id: id, value: val });
                    oneIndexs = true;
                }
            });

        if (indexs.length > 0 && oneIndexs == true) {
            swal({
                title: "Buscando datos para completar!",
                text: '',
                icon: "info",
                timer: 3000
            });
            
           insertServices.autoComplete(entityId, indexs, $scope.completeIndexs);
          
        }
    };

    $scope.completeIndexs = function (result) {
        $scope.newResult = result;
        if ($scope.newResult != null) {
            $($scope.newResult._indexs).each(function (index,item) {

                if (item._DataTemp != undefined && item._DataTemp != ''  &&    $scope.attributeId != item._id) {
                    var inputAttr = $("#ucDocTypesIndexs_" + item._id);
                    if (item._DropDown == 2 &&  $(inputAttr).hasClass("generatelistdinamic")) {
                        if (item._dataDescription != null && item._dataDescription != '') {
                            $(inputAttr).val(item._DataTemp + ' - ' + item._dataDescription).change();
                        }
                        else {
                            $(inputAttr).val(item._DataTemp).change();
                        }
                            }
                    else if (item._dropDown == 1) {
                        $(inputAttr).val(item._DataTemp).change();
                    }
                    else {
                        $(inputAttr).val(item._DataTemp).change();
                    }
                    $(inputAttr).css('border-color','blue');
                }
            });
        }
        else {
        }
    };

    $scope.insertMode = true;
    $scope.barcodeId = 0;
    $scope.insertReady = true;
    $scope.FileAdded = false;

    $scope.generateBC = function () {
        var entityId = $("#ucDocTypes_DocTypes").val();
        var indexs = [];

        var oneIndexs = false;
        $(".insertAttribute").each(
            function (index, item) {
                var val = $(item).val();
                var id = $(item).attr('Id').replace('ucDocTypesIndexs_', '');
                if (val != null && val != '') 
                    indexs.push({ id: id, value: val });
                    //$('#ucDocTypesIndexs_' + id + 'UP').parent().parent().addClass("printAttribute");
                
            });

        if (indexs.length > 0) {

            $(".insertAttribute").each(
                function (index, item) {
                    var val = $(item).val();
                    var id = $(item).attr('Id').replace('ucDocTypesIndexs_', '');
                    if (val != null && val != '') {
                        indexs.push({ id: id, value: val });
                        $('#ucDocTypesIndexs_' + id + 'UP').parent().parent().addClass("printAttribute");
                    }
                    else {
                        $('#ucDocTypesIndexs_' + id + 'UP').parent().parent().addClass("emptyAttribute");
                        $('#ucDocTypesIndexs_' + id + 'UP').parent().parent().hide();
                    }
                });

            $scope.barcodeId = insertServices.generateBC(entityId, indexs);

            if ($scope.barcodeId != null && $scope.barcodeId > 0) {
                swal({
                    title: "Caratula generada. OK!",
                    text: 'NRO: ' + $scope.barcodeId,
                    icon: "info",
                    timer: 2000
                }).then((function () {
                    window.print();

                    $(".emptyAttribute").each(function (index, item) {
                        $(item).removeClass("emptyAttribute");
                        $(item).show();
                    });

                    $(".printAttribute").each(function (index, item) {
                        $(item).removeClass("printAttribute");
                    });


                }));
            }
            else {
                swal('Error', 'Error al generar la caratula', 'error');
            }
        }
        else {
            
            swal('Datos requeridos', 'Tenes que ingresar al menos un dato para continuar', 'warning');

        }
    };



    $scope.ReplicarBC = function () {
        var entityId = $("#ucDocTypes_DocTypes").val();
        var indexs = [];

        var oneIndexs = false;
        $(".insertAttribute").each(
            function (index, item) {
                var val = $(item).val();
                var id = $(item).attr('Id').replace('ucDocTypesIndexs_', '');
                if (val != null && val != '') {
                    indexs.push({ id: id, value: val });
                }
            });

        if (indexs.length > 0) {
             insertServices.replicarBC(entityId, indexs, $scope.barcodeId);           
                swal({
                    title: "Caratula replicada. OK!",
                    text: 'NRO: ' + $scope.barcodeId,
                    icon: "info",
                    timer: 2000
                });
           
        }
        else {
            swal('Datos requeridos', 'Tenes que ingresar al menos un dato para continuar', 'warning');

        }
    };


    $scope.PrintBC = function () {
        var entityId = $("#ucDocTypes_DocTypes").val();
        var indexs = [];

        var oneIndexs = false;
        $(".insertAttribute").each(
            function (index, item) {
                var val = $(item).val();
                var id = $(item).attr('Id').replace('ucDocTypesIndexs_', '');
                if (val != null && val != '') {
                    indexs.push({ id: id, value: val });
                    $('#ucDocTypesIndexs_' + id + 'UP').parent().parent().addClass("printAttribute");
                }
                else {
                    $('#ucDocTypesIndexs_' + id + 'UP').parent().parent().addClass("emptyAttribute");
                    $('#ucDocTypesIndexs_' + id + 'UP').parent().parent().hide();
                }
            });

        if (indexs.length > 0) {

            if ($scope.barcodeId != null && $scope.barcodeId > 0) {
                swal({
                    title: "Se reimprime caratula!",
                    text: 'NRO: ' + $scope.barcodeId,
                    icon: "info",
                    timer: 2000
                }).then((function () {
                    window.print();

                    $(".emptyAttribute").each(function (index, item) {
                        $(item).removeClass("emptyAttribute");
                        $(item).show();
                    });

                    $(".printAttribute").each(function (index, item) {
                        $(item).removeClass("printAttribute");
                    });


                }));
            }
            else {
            }
        }
        else {

        }
    };

});


function autocompleteIndexForInsert(sender) {
    var attributeId = $(sender).attr("id");
    var attributeValue = $(sender).val();
    console.log(attributeId);
    console.log(attributeValue);

    var insertControllerScope = angular.element($("#insertFieldset")).scope();
    var attributes = insertControllerScope.autoComplete(attributeId, attributeValue);


}


