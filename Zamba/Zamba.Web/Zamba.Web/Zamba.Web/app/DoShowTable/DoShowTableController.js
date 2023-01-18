//var app = angular.module('DoShowTable', ['ngAnimate', 'ngSanitize', 'ui.bootstrap']);
var details = [];
var ListImpr = [];
var Result = [];
var value;

app.controller('Modal', function ($uibModal, $log, $filter, $http, $scope, DoShowTableServices) {
    var pc = this;
    Result = DoShowTableServices.getResults();
    Result = JSON.parse(Result);
    pc.data = Result;

    $scope.headElements = ['ID', 'First', 'Last', 'Handle'];

    pc.open = function (size) {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '../../app/DoShowTable/DoShowTable.html',
            controller: 'ModalInstanceCtrl',
            controllerAs: 'pc',
            size: size,
            resolve: {
                data: function () {
                    return pc.data;
                }
            }
        });

        modalInstance.result.then(function () {
           
        });
    };
});

app.controller('ModalInstanceCtrl', function ($uibModalInstance, $scope, data, DoShowTableServices) {
    var pc = this;
    pc.data = data;

    pc.ok = function () {
        $scope.parentTaskId = getElementFromQueryString("taskid");
        $scope.RuleId = 175975;
        NroHojaRuta = $("#zamba_index_139601").val();

        DoShowTableServices.DoshowtableInsert(ListImpr, $scope.parentTaskId, NroHojaRuta, $scope.RuleId);

        loadGrillaFormDoShowTable();
        $uibModalInstance.close();
    };

    pc.cancel = function () {
        details = [];
        ListImpr = [];
        Result = [];

        value = "";
        choice = "";
        $uibModalInstance.dismiss('cancel');
    };


    $scope.checkoptions = function (choice) {
        for (var i = 0; i < $scope.List.length; i++) {
            if ($scope.List[i].NroGuia == choice) {
                value = i;
            }
        }
        //var index = ListImpr.indexOf(value);
        //var Evaluated;
        var ListBool;

        if (document.getElementsByName("checkTable")[value].parentNode.parentNode.classList[0] == "CellColor") {
            document.getElementsByName("checkTable")[value].parentNode.parentNode.removeAttribute("class");
            ListBool = false;
        } else {
            document.getElementsByName("checkTable")[value].parentNode.parentNode.setAttribute("class", "CellColor");
            ListBool = true;
        }

        ListaParaServicio(ListBool, choice);
    };

    function ListaParaServicio(ListBool, choice) {
        if (ListBool == true) {
            ListImpr.push($scope.List[value].NroGuia);
            visuaizarBtn();
        } else if (ListBool == false) {
            for (var i = 0; i < ListImpr.length; i++) {
                if (ListImpr[i] == choice) {
                    ListImpr.splice(i, 1);
                    visuaizarBtn();
                }
            }
        }
    }

    function visuaizarBtn() {
        if (ListImpr.length > 0) {
            $("#btnGardar").removeAttr("disabled");
        } else {
            $("#btnGardar").attr("disabled", true);
        }
    }

    function getElementFromQueryString(element) {
        var url = window.location.href;
        var segments = url.split("&");
        var value = null;

        segments.forEach(function (valor) {
            if (valor.includes(element)) { value = valor.split("=")[1]; }
        });

        return value;
    }

    //Actualiza la grilla Kendo para mostrar los registros insertados.
    function loadGrillaFormDoShowTable() {
        try {
            setTimeout(function () { document.getElementsByTagName("zamba-associated")[0].querySelector(".BtnRefresh").click(); });
        } catch (e) {
            console.error(e);
        }
    }    
});