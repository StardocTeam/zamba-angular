//var app = angular.module("RequestApp", ["xeditable"]);

//app.run(function (editableOptions) {
//    editableOptions.theme = 'bs3';
//});



app.controller('RequestCtrl', function ($scope, $q, $log, $filter, $timeout, $http, gridService) {

    $scope.isMainView = true;
    $scope.isInserting = false;
    $scope.isEditing = false;
    $scope.deleteItems = false;
    $scope.firtTime = false;

    $scope.indexsSelectedToDelete = [];

    $scope.asociatedResults = [];
    $scope.TipoReclamante = [];

    $scope.UnidadAttributeLists = ["1-Cientos", "2-Unidad", "3-Resma", "4-Millar"];

    $scope.delegationsList = CargarListaDelegaciones();
    $scope.ProductoAttributeLists = CargarListaProductos();
    $scope.costCenterList = CargarListaCentroCostos();
    $scope.MonedaAttributeLists = CargarTipoMoneda();
    $scope.asociatedColumns = [
        {
            Id: 1, Name: 'Linea', Field: 'line', Type: 'string', Visible: true, DropDown: 0, Width: 2,
            List: []
        },
        {
            Id: 10, Name: 'Artículo', Field: 'product', Type: 'string', Visible: true, DropDown: 1,
            List: []
        },
        {
            Id: 50, Name: 'Moneda', Field: 'Tipo de moneda', Type: 'string', Visible: true, DropDown: 1,
            List: []
        },
        {
            Id: 40, Name: 'P. Unitario', Field: 'PRECIO_UNITARIO', Type: 'decimal', Visible: true, DropDown: 0,
            List: []
        },
        {
            Id: 20, Name: 'Unidad', Field: 'Unidad', Type: 'string', Visible: true, DropDown: 1,
            List: []
        },
        {
            Id: 30, Name: 'Cantidad', Field: 'Cantidad', Type: 'int', Visible: true, DropDown: 0,
            List: []
        },
        {
            Id: 70, Name: 'Precio', Field: 'Precio', Type: 'decimal', Visible: true, DropDown: 0,
            List: []
        },
        {
            Id: 70, Name: 'C. de Costos', Field: 'Precio', Type: 'decimal', Visible: true, DropDown: 1,
            List: []
        },
        {
            Id: 70, Name: 'Delegaciones', Field: 'Precio', Type: 'decimal', Visible: true, DropDown: 1,
            List: []
        }
    ];

    $scope.MainView = function () {
        return $scope.isInserting == false && $scope.isEditing == false;
    }


    $scope.LoadAsociatedResults = function (parentResultId, parentResultEntityId, associatedIds) {

        var d = gridService.getAsociatedResults(parentResultId, parentResultEntityId, associatedIds);
        if (d != 'null' && d != []) {
            var associatedResults = getFormattedResults(JSON.parse(d));
            if (associatedResults == "") {
                console.log("No se pudo obtener los asociados");
                $scope.firtTime = true;
                $scope.setTotal();
                return;
            }
            else {
                $scope.firtTime = false;
                $scope.asociatedResults = associatedResults;
                $scope.setTotal();
            }
            setTagValueById("hdnCount", $scope.asociatedResults.length);
        }
        else
            $scope.firtTime = true;

    };


    $scope.setPrice = function (result) {
        if (result.data.quantity != "" && result.data.quantity.toString() != "NaN" &&
            result.data.unitPrice != "" && result.data.unitPrice.toString() != "NaN") {
            result.data.price = parseFloat(result.data.quantity) * parseFloat(result.data.unitPrice);
            result.data.price.toFixed(2)
        } else {
            result.data.price = null;
            swal("", "Por favor, ingrese todos los campos o corregir incorrectos.", "error");

        }
    }
    //Funcion que asigna a Precio el producto entre P. Unitario y Cantidad de la grilla.
    $scope.setPriceWithCulture = function (result) {
        var miUnitPrice = parseFloat(result.data.unitPrice.toString().replaceAll('.', '').replace(',', '.')).toFixed(2);

        if (result.data.quantity != undefined && result.data.quantity != "" && result.data.quantity.toString() != "NaN" &&
            result.data.unitPrice != "" && result.data.unitPrice.toString() != "NaN") {
            result.data.price = parseFloat(result.data.quantity) * miUnitPrice;
            result.data.price = result.data.price.toFixed(2).replaceAll('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
        }
        else {
            result.data.price = null;
            //swal("", "Por favor, ingrese todos los campos o corregir incorrectos.", "error");

        }

    }


    //Funcion que permite reemplazar todas las coincidencias de un caracter por otro pasados por parametro.
    String.prototype.replaceAll = function (search, replacement) {
        return this.split(search).join(replacement);
    };

    //Funcion controlado por Angular que renderiza a importe un valor logico obtenido de una fuente.
    $scope.render_ImporteForNgGrid = function (result) {

        if (result.data.quantity != undefined && result.data.quantity != "" && result.data.quantity.toString() != "NaN" &&
            result.data.unitPrice != "" && result.data.unitPrice.toString() != "NaN") {
            result.data.unitPrice = parseFloat(result.data.unitPrice.replaceAll('.', '').replace(',', '.')).toFixed(2).replace('.', ',');
            result.data.unitPrice = result.data.unitPrice.replaceAll('.', '').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");

        }
    }

    function getFormattedResults(results) {
        var resultsFormated = [];
        var newResult = null;
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
                line: AssociatedResult.LINEA,
                product: getElementFromListoByElementIndex($scope.ProductoAttributeLists, AssociatedResult.ARTICULO),
                unitPrice: AssociatedResult.PRECIO_UNITARIO,
                measure: AssociatedResult.UNIDAD_DE_MEDIDA,
                quantity: AssociatedResult.CANTIDAD,
                typeOfCurrency: AssociatedResult.TIPO_DE_MONEDA,
                price: AssociatedResult.PRECIO,
                costCenter: AssociatedResult.CENTRO_DE_COSTOS,
                delegations: AssociatedResult.DELEGACIONES,
                description: AssociatedResult.DESCRIPCION_DE_LA_SOLICITUD
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
        return valor;
    }

    $scope.setProductParam = function (item, result) {
        if (result == null) {
            $scope.product = item;
        } else {
            result.data.product = item;
        }
    }


    $scope.setCostCenterParam = function (item, result) {
        if (result == null) {
            $scope.costCenter = item;
        } else {
            result.data.costCenter = item;
        }
    }

    $scope.setdelegationsParam = function (item, result) {
        if (result == null) {
            $scope.delegations = item;
        } else {
            result.data.delegations = item;
        }
    }




    // $scope.LoadAsociatedResults(18180618, $scope.entityId, $scope.AssociatedIds);

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

    // $scope.AttributeLists = [];
    $scope.loadAttributeList = function (AttributeId) {

        //if ($scope.AttributeLists[AttributeId] != null && $scope.AttributeLists[AttributeId].length > 0) {
        //    return $scope.AttributeLists[AttributeId];
        //}
        //else {
        //    var col = $scope.getColumn(AttributeId);
        //    if (col != null && col.List != undefined && col.List != null && col.List.length > 0) {
        //        return col.List;
        //    }
        //    else {
        //        return gridService.LoadAttributeList(AttributeId).then(function (response) {
        //            $scope.AttributeLists[AttributeId] = response;
        //            return $scope.AttributeLists[AttributeId];

        //        });
        //    }
        //}
        //$scope.AttributeLists = ["Conyuge", "Cónyuge por Sí y por hijos", "Concubina", "Concubina por Sí y por sus hijos", "Hijos", "Hermanos", "Padres", "Otros"];



    };

    //$scope.showGroup = function (user) {
    //    if (user.group && $scope.groups.length) {
    //        var selected = $filter('filter')($scope.groups, { id: user.group });
    //        return selected.length ? selected[0].text : 'Not set';
    //    } else {
    //        return user.groupName || 'Not set';
    //    }
    //};

    $scope.showAttributeDescription = function (Attribute) {
        var selected = [];
        if (Attribute.Data) {
            selected = $filter('filter')($scope.loadAttributeList(Attribute.Id), { Data: Attribute.Data });
        }
        return selected.length ? selected[0].DataDescription : Attribute.Data;
    };

    $scope.checkName = function (data, id) {
        if (id === 2 && data !== 'awesome') {
            return "Username 2 should be `awesome`";
        }
    };

    // filter users to show
    $scope.filterResult = function (result) {
        return (result.isDeleted != undefined || result.isDeleted !== true);
    };

    // mark user as deleted
    $scope.deleteResult = function (resultId, index) {

        var list = [];

        if ($scope.isMainView && $scope.asociatedResults[index].id != null) {
            gridService.deleteResult($scope.asociatedResults[index]);
        }


        for (var i = 0; i < $scope.asociatedResults.length; i++) {
            if (i != index) {
                list.push($scope.asociatedResults[i]);
            }
        }
        $scope.asociatedResults = list;

        //var filtered = $filter('filter')($scope.asociatedResults, { id: resultId });
        //if (filtered.length) {
        //    if (filtered[0].isDeleted == true) {
        //        filtered[0].isDeleted = false;
        //        var index = $scope.indexsSelectedToDelete.indexOf(resultId);
        //        if (index > -1) {
        //            $scope.indexsSelectedToDelete.splice(index, 1);
        //        } 
        //    }else{
        //        $scope.indexsSelectedToDelete.push(resultId);
        //        filtered[0].isDeleted = true;
        //    }
        //    //filtered[0].isDeleted = true;
        //    $scope.deleteItems = true;
        //}
        $scope.setTotal();

    };


    //Funcion Angular que establece el total de la sumatoria de la compra de bienes y servicios contratados.
    $scope.setTotal = function () {
        var total = 0;
        if ($scope.asociatedResults != "" && $scope.asociatedResults.length > 0) {
            for (var result in $scope.asociatedResults) {
                if (isNaN($scope.asociatedResults[result].data.price)) {
                    total += parseFloat($scope.asociatedResults[result].data.price.replaceAll('.', '').replace(',', '.'));
                }
                else {
                    total += parseFloat($scope.asociatedResults[result].data.price);
                }
            }
        }

        $scope.total = total.toFixed(2).replace('.', ',').replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
        setTagValueById("zamba_index_163", total.toFixed(2));
    }




    $scope.insertResult = function (result) {
        var newObject = GetNewResult();

        if ($scope.asociatedResults.length == 0) {
            newObject.data.line = 1;
        } else {
            newObject.data.line = getMaximunLineNumber() + 1;
        }

        newObject.data.product = $scope.product;
        newObject.data.unitPrice = result.data.unitPrice;
        newObject.data.measure = result.data.measure;
        newObject.data.quantity = result.data.quantity;
        newObject.data.typeOfCurrency = result.data.typeOfCurrency;
        newObject.data.price = result.data.price;
        newObject.data.description = result.data.description;
        newObject.data.costCenter = $scope.costCenter;
        newObject.data.delegations = $scope.delegations;

        if (ValidarComprasyServicios(newObject)) {

            newObject.isNew = true;
            if ($scope.asociatedResults == null || $scope.asociatedResults == undefined) {
                $scope.asociatedResults = [];
            }

            if ($scope.asociatedResults.length != 0) {
                var FirstItem = $scope.asociatedResults[0].data.typeOfCurrency
                var LastTypeofcurrency = result.data.typeOfCurrency;
                if (FirstItem != LastTypeofcurrency) {
                    swal("", "Por favor, Verificar tipo de moneda", "info");
                    return;
                }
            }

            $scope.asociatedResults.push(newObject);
            newObject = null;
            result.data.line = null;
            result.data.unitPrice = null;
            result.data.measure = null;
            result.data.quantity = null;
            result.data.typeOfCurrency = null;
            result.data.price = null;
            result.data.description = '';
            $scope.costCenter = null;
            $scope.delegations = null;
            $scope.isMainView = true;
            $scope.setProductParam('', null);

            $scope.setCostCenterParam('', null);
            $scope.setdelegationsParam('', null);

            $scope.setTotal();
        } else {
            swal("", "Por favor, ingrese todos los campos o corregir incorrectos.", "error");
            return;
        }



    }

    function ValidarComprasyServicios(result) {
        var CYS = false;
        var datosvalidar = $scope.product != null &&
            $scope.delegations != null &&
            $scope.delegations != "" &&
            result.data.measure != null &&
            result.data.typeOfCurrency != null &&
            result["data"]["unitPrice"] != null &&
            result["data"]["quantity"] != null &&
            $scope.costCenter != null &&
            $scope.costCenter != "";
        var isCorrectData =
            $scope.delegationsList.includes($scope.delegations) &&
            $scope.ProductoAttributeLists.includes($scope.product) &&
            $scope.costCenterList.includes($scope.costCenter);

        if (datosvalidar && isCorrectData) {
            CYS = true;
        }
        return CYS;
    }

    function validateFieldsInEdittingMode(result) {
        var isValidated = false;
        var datosvalidar = $scope.product != null &&
            result.data.delegations != null &&
            result.data.delegations != "" &&
            result.data.costCenter != null &&
            result.data.costCenter != "" &&
            result.data.measure != null &&
            result.data.typeOfCurrency != null &&
            result.data.unitPrice != null &&
            !isNaN(parseFloat(result.data.unitPrice)) &&
            result.data.unitPrice.toString() != "NaN" &&
            result.data.quantity != null &&
            !isNaN(parseFloat(result.data.quantity)) &&
            result.data.quantity.toString() != "NaN";

        var isCorrectData =
            $scope.delegationsList.includes(result.data.delegations) &&
            $scope.ProductoAttributeLists.includes(result.data.product) &&
            $scope.costCenterList.includes(result.data.costCenter);

        if (datosvalidar && isCorrectData) {
            isValidated = true;
        }
        return isValidated;
    }

    function getMaximunLineNumber() {
        var list = [];
        for (var result in $scope.asociatedResults) {
            list.push(parseInt($scope.asociatedResults[result].data.line));
        }
        return Math.max.apply(null, list);
    }

    $scope.addResult = function () {
        $scope.isInserting = true;


        //var newObject = GetNewResult();
        //if ($scope.asociatedResults == null || $scope.asociatedResults == undefined) {
        //    $scope.asociatedResults = [];
        //}

        //$scope.asociatedResults.push(newObject);
    };

    $scope.EditResultMode = function () {
        $scope.isInserting = false;
        $scope.deleteItems = false;
        $scope.isMainView = false;
        $scope.isEditing = true;

        for (var result in $scope.asociatedResults) {
            $scope.asociatedResults[result].isDeleted = false;
            $scope.asociatedResults[result].isNew = false;
            $scope.asociatedResults[result].isEdited = true;
        }
    }

    $scope.EditResult = function (result, nombre, tipo) {
        $scope.isEditing = true;
        $scope.isMainView = false;
        $scope.isInserting = false;
        var filtered = $filter('filter')($scope.asociatedResults, { id: result.id });
        if (filtered.length) {
            filtered[0].isEdited = true;
            //nombre, tipo
            //filtered[0].data.reclaimentType = $scope.resultEditing.reclaimantType || '';
            //filtered[0].data.reclaimantName = $scope.resultEditing.reclaimantName || '';    
            filtered[0].data.reclaimentType = tipo || '';
            filtered[0].data.reclaimantName = nombre || '';
        }
        $scope.setTotal();
    }

    $scope.cancelModification = function (result) {
        $scope.isEditing = false;
        $scope.isInserting = false;
        $scope.deleteItems = false;
        $scope.isMainView = true;

        result.data.line = null;
        result.data.unitPrice = null;
        result.data.quantity = null;
        result.data.price = null;
        result.data.description = '';

        $scope.setProductParam('', null);
        $scope.setMeasureParam('2-Unidad', null);
        $scope.settypeOfCurrencyParam('PES', null);
        $scope.setCostCenterParam('', null);
        $scope.setdelegationsParam('', null);

        for (var result in $scope.asociatedResults) {
            $scope.asociatedResults[result].isDeleted = false;
            $scope.asociatedResults[result].isNew = false;
            $scope.asociatedResults[result].isEdited = false;
        }
        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
    }

    // cancel all changes
    $scope.cancel = function () {
        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
    };

    // save edits
    $scope.saveTable = function () {
        $scope.convertToDecimal();

        var array = [];

        for (var result in $scope.asociatedResults) {

            // actually delete user
            if ($scope.asociatedResults[result].isDeleted) {
                gridService.deleteResult($scope.asociatedResults[result]);
                array.push(result);
            }

            // mark as not new
            if ($scope.asociatedResults[result].isNew) {

                $scope.asociatedResults[result].isNew = false;
                gridService.insertResult($scope.asociatedResults[result]);

            }

            // mark as not new
            if ($scope.asociatedResults[result].isEdited) {
                if (validateFieldsInEdittingMode($scope.asociatedResults[result])) {
                    $scope.asociatedResults[result].isEdited = false;
                    //gridService.saveResult($scope.asociatedResults[result].data, $scope.asociatedResults[result].id);
                    gridService.saveResult($scope.asociatedResults[result], $scope.asociatedResults[result].id, $scope.resultId);
                } else {
                    swal("", "Por favor, ingrese todos los campos o corregir incorrectos.", "error");
                    return false;
                }
            }
        }



        $scope.isInserting = false;
        $scope.isEditing = false;
        $scope.deleteItems = false;
        $scope.isMainView = true;

        var resultToDelete = array.length;
        var list = [];
        for (var i = 0; i < $scope.asociatedResults.length; i++) {
            //$scope.asociatedResults.splice(array[i], 1);
            if (!array.includes(String(i))) {
                list.push($scope.asociatedResults[i]);
            }
        }
        $scope.asociatedResults = list;
        $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
        return true;

    };

    //funcion que convierte valores de importe a decimal.
    $scope.convertToDecimal = function () {
        for (var result in $scope.asociatedResults) {
            if (isNaN($scope.asociatedResults[result].data.unitPrice)) {
                $scope.asociatedResults[result].data.unitPrice = parseFloat($scope.asociatedResults[result].data.unitPrice.replaceAll('.', '').replace(',', '.'));
                $scope.asociatedResults[result].data.price = parseFloat($scope.asociatedResults[result].data.price.replaceAll('.', '').replace(',', '.'));
            }
        }
    }


    //$scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.AssociatedIds);

    function GetNewResult() {
        var result = {
            id: null,
            resultId: getElementFromQueryString("docid"),
            entityId: 106,
            data: {
                line: '',
                product: '',
                unitPrice: '',
                measure: '',
                quantity: '',
                typeOfCurrency: '',
                price: '',
                description: '',
                costCenter: '',
                delegations: ''
            },
            isDeleted: false,
            isNew: true,
            isEdited: false
        };
        return result;
    }


    //-------------------------------------------------------------------------



    $scope.simulateQuery = false;
    $scope.isDisabled = false;

    // list of `state` value/display objects
    //self.states = loadAll();

    //$scope.loadAll = function () {
    //    return $scope.ProductoAttributeLists.map(function (state) {
    //        return {
    //            state
    //        };
    //    });
    //}

    function convertObjectToArray(obj) {
        var result = Object.keys(obj).map(function (key) {
            return [obj[key]];
        });
        return result;
    }

    $scope.newState = function (state) {
        console.log("Sorry! You'll need to create a Constitution for " + state + " first!");
    }


    function createFilterFor(query) {
        var lowercaseQuery = angular.lowercase(query);
        return function filterFn(contact) {
            return (contact._lowername.indexOf(lowercaseQuery) != -1);
        }
    }


    $scope.querySearch = function (query) {
        var results = query ? convertObjectToArray($scope.loadAll()).filter(createFilterFor(query)) : $scope.loadAll(),
            deferred;
        if ($scope.simulateQuery) {
            deferred = $q.defer();
            $timeout(function () { deferred.resolve(results); }, Math.random() * 1000, false);
            return deferred.promise;
        } else {
            return results;
        }
    };

    $scope.searchTextChange = function (text) {
        $log.info('Text changed to ' + text);
    }

    $scope.selectedItemChange = function (item) {
        $log.info('Item changed to ' + JSON.stringify(item));
    }

    $scope.createFilterFor = function (query) {
        var lowercaseQuery = query.toLowerCase();

        return function filterFn(state) {
            return (state.value.indexOf(lowercaseQuery) === 0);
        };

    };



    //-------------------------------------------------------------------------

});



app.directive('zambaGrid', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: false,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.entityId = attributes.entityId;
            $scope.isReadOnly = attributes.readOnly == 'true';

            var url = window.location.href;
            var segments = url.split("&");
            var resultId = null;
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { resultId = valor.split("=")[1]; }
            });
            $scope.resultId = resultId;
            //$scope.associatedIds = attributes.associatedIds;
            $scope.associatedIds = "18, 16";
            $scope.LoadAsociatedResults($scope.resultId, $scope.entityId, $scope.associatedIds);
            $scope.isMainView = true;

            //$scope.indexs = gridServiceFactory.getAssociatedIndex($scope.resultId, $scope.entityId)
        },
        templateUrl: $sce.getTrustedResourceUrl("../../Grid/SolCompCont2/SolCompContEditGrid2.html")
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

////////////////////////////////////////////// Cargar listas //////////////////////////////////////////////

function CargarListaCentroCostos() {
    var ReportID = "1525076"; //SLST81
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "ReportID": ReportID
        }
    };
    var ListaArray = [];
    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetResultsByReportId',
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data) {

                ListaCentroCosto = $.parseJSON(data);
                for (i = 0; i < ListaCentroCosto.length; i++)
                    ListaArray.push(ListaCentroCosto[i]["ITEM"]);
            }
        ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Status: " + textStatus); console.log("Error: " + errorThrown);
        }
    })
    return ListaArray;
}

function CargarListaProductos() {

    var ReportID = "1525075"; //SLST81
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "ReportID": ReportID
        }
    };
    var ListaArray = [];
    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetResultsByReportId',
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data) {

                ListaProductos = $.parseJSON(data);
                for (i = 0; i < ListaProductos.length; i++)
                    ListaArray.push(ListaProductos[i]["CODIGO"].toString() + '-' + ListaProductos[i]["DESCRIPCION"]);
            }
        ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Status: " + textStatus); console.log("Error: " + errorThrown);
        }
    })
    return ListaArray;
}

function CargarListaDelegaciones() {
    var ReportID = "1525074"; // SLST197
    var ListaArray = [];
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "ReportID": ReportID
        }
    };
    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetResultsByReportId',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data) {
                ListaDelegaciones = $.parseJSON(data);
                for (i = 0; i < ListaDelegaciones.length; i++)
                    ListaArray.push(ListaDelegaciones[i]["DESCRIPCION"]);
            }
        ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Status: " + textStatus); console.log("Error: " + errorThrown);
        }
    })
    return ListaArray;
}
function CargarTipoMoneda() {
    var ReportID = "1525077"; // SLST197
    var ListaArray = [];
    var genericRequest = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "ReportID": ReportID
        }
    };
    $.ajax({
        type: "POST",
        url: serviceBase + '/search/GetResultsByReportId',
        data: JSON.stringify(genericRequest),

        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data) {
                ListaTipoMoneda = $.parseJSON(data);
                for (i = 0; i < ListaTipoMoneda.length; i++)
                    ListaArray.push(ListaTipoMoneda[i]["ITEM"]);
            }
        ,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("Status: " + textStatus); console.log("Error: " + errorThrown);
        }
    })
    return ListaArray;
}



