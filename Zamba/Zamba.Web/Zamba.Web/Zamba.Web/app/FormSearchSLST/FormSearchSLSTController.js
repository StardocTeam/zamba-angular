app.controller('FormSearchSLSTController', function ($scope, $rootScope, FormSearchSLSTService) {
    $scope.listOptions = [];
    $scope.filterText = '';
    $scope.targetControl = undefined;
    $scope.showLabelMoreResult = true;

    $scope.initModal = function (indexId, indexName,limitItems) {        
        $scope.LimitToSlst = limitItems;
        $scope.indexId = indexId;
        $scope.getOptions('', false);        
        $scope.showModal();        
    }
    $scope.LoadResult = function (entityId, docId, indexId) {

        try {
            var ReturnValue = FormSearchSLSTService.LoadResult(entityId, docId, indexId);
            if (ReturnValue != "") {
                $scope.selectedItem = ReturnValue.replace(/['"]+/g, '');
            }

        } catch (e) {
            console.error(e);
        }
    }
    $scope.getOptions = function (filter, showMore) {
        try {
            if (showMore)
                $scope.LimitToSlst += 20;
            else
                $scope.LimitToSlst = $scope.limitStart;
            $scope.listOptions = FormSearchSLSTService.GetListOptions($scope.indexId, filter, $scope.LimitToSlst);
            if ($scope.listOptions.length > $scope.LimitToSlst) {
                $scope.listOptions.pop();
                $scope.showLabelMoreResult = true;
            }
            else {
                $scope.showLabelMoreResult = false;
            }
        } catch (e) {
            console.log(e);
        }        
    }
    $scope.setValueToControl = function (value, code) {
        $scope.targetControl = code + " - " + value;
        $scope.selectedItem = value;
        var idModal = "#ModalSlst_" + $scope.indexId;
        $(idModal).modal('hide');

    }
    $scope.showModal = function () {
        var idModal = "#ModalSlst_" + $scope.indexId;
        if (!$(idModal).hasClass('in')) {
            $(idModal).modal();
            $(idModal).draggable();
            $("#modalSLST > div")[0].childNodes[1].value = "";
        }
        //var modal = $(".modal-backdrop")[0];

        //// esto es porque los estilos del modal hacen que se rompa la vista
        //$(modal).css('opacity', 0);
        //$(modal).css('z-index', 99999);
        //$(modal).css('display', 'contents');
    }
});

app.directive('formSearchSlst', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
            
            $scope.LimitToSlst = Number(attributes.limitTo);
            $scope.limitStart = Number(attributes.limitTo);
            $scope.indexName = attributes.indexName;
            $scope.indexId = attributes.indexId;
            $scope.entityId = attributes.entityId;
            var url = window.location.href;
            var segments = url.split("&");
            segments.forEach(function (valor) {
                if (valor.includes("docid")) { $scope.parentResultId = valor.split("=")[1]; }
            })


            if ($scope.parentResultId != undefined) {
                $scope.LoadResult($scope.entityId, $scope.parentResultId, $scope.indexId);
            }
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/FormSearchSLST/FormSearchSLSTTemplate.html')
    }
});


