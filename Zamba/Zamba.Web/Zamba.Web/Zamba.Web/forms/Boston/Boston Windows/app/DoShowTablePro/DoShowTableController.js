// Code goes here
//var app = angular.module('DoShowTable', ['ngAnimate', 'ngSanitize', 'ui.bootstrap']);
var ListImpr = [];
var Result = [];
var value;

var ruleId;
var FormVariables = [];
var parentTaskId;
var ListIdResult;

var datasourceColumnId;
var datasourceColumn;

app.controller('Modal', function ($uibModal, $log, $filter, $http, $scope, DoShowTableServices) {
    var pc = this;

    pc.open = function (size) {
        pc.data = [];
        pc.valueForRuleQuery = [];

        $scope.LoadResults($scope.reportId, $scope.valueForRuleQuery, $scope.parentTaskId);

        if (pc.data.length > 0) {
            ListImpr = [];
            ruleId = $scope.ruleId;
            FormVariables = FormVariables;
            parentTaskId = $scope.parentTaskId;

            modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '../../app/DoShowTablePro/DoShowTable.html',
                controller: 'ModalInstanceCtrl',
                controllerAs: 'pc',
                size: size,
                resolve: {
                    data: function () {
                        return pc.data;
                    },
                    FormVariables: function () {
                        return pc.FormVariables;
                    },
                    ruleId: function () {
                        return pc.ruleId;
                    },
                    parentTaskId: function () {
                        return pc.parentTaskId;
                    }
                }
            });
        } else {
            console.log("No se encontraron registros.");
            var scope_taskController = angular.element($("#taskController")).scope();
            scope_taskController.EvaluateRuleExecutionResult(pc.vars);
        }
    };

    $scope.LoadResults = function (reportId, valueForRuleQuery, parentTaskId) {
        try {
            SaveFormData();

            if (valueForRuleQuery != "") 
                FormVariables = ValuesFromTable(valueForRuleQuery);

            if (($scope.reportId == undefined || $scope.reportId == "") && $scope.datasourceRuleId != "") {
                Result = DoShowTableServices.getResultsOrigin($scope.datasourceRuleId, reportId, FormVariables, parentTaskId, $scope.datasourceVariable);
                    
                pc.data = JSON.parse(Result.data);
                Result.data = pc.data;
                pc.vars = JSON.parse(Result.vars);
                Result.vars = pc.vars;
            }

            if (($scope.datasourceRuleId == undefined || $scope.datasourceRuleId == "") && $scope.reportId != "") {
                Result = DoShowTableServices.getResults(reportId, FormVariables, parentTaskId);

                Result = JSON.parse(Result);
                pc.data = Result.data;
            }

            return Result;
        } catch (e) {
            console.log(e);
        }
    };

    function SaveFormData() {
        $(".autoSave").each(function (index) {
            SaveIndex(this);
        });
    };

    function ValuesFromTable(valueForRuleQuery) {
        var ResultValues = [];
        var valor = valueForRuleQuery.split(",");

        for (var i = 0; i < valor.length; i++) {

            var columns = valor[i].split("=");

            var VarName = columns[0];
            var IndexValue = $("#" + columns[1]).val();

            ResultValues.push({ name: VarName, value: IndexValue });

            var jsonlist = JSON.stringify(ResultValues);
        }
        return jsonlist;
    }
});

app.controller('ModalInstanceCtrl', function ($uibModalInstance, $scope, data, DoShowTableServices) {
    var pc = this;
    pc.data = data;
    ListresultsId = [];

    pc.ok = function () {
        ListIdResult = JSON.stringify(ListresultsId);
        ListIdResult = ListIdResult.replace(/[([^\]^"]*/g, "");
        var insert = DoShowTableServices.DoshowtableInsert(ruleId, FormVariables, parentTaskId, ListIdResult, $scope.datasourceColumnId, datasourceColumn);

        insert = JSON.parse(insert);        
        var scope_taskController = angular.element($("#taskController")).scope();
        scope_taskController.EvaluateRuleExecutionResult(insert);

        $scope.ListresultsId = [];
        $uibModalInstance.close();
        loadGrillaFormDoShowTable();
    };

    pc.cancel = function () {
        ListImpr = [];
        Result = [];
        value = "";
        choice = "";
        $scope.ListresultsId = [];
        $uibModalInstance.dismiss('cancel');
        
        console.log("Recargando pagina...");
        location.reload();
    };

    $scope.checkoptions = function (choice) {
        if (multipleSelect == "false") {
            $(document.getElementsByName("checkTable"))
                .each(function (index, element) {
                    if (index != choice) {
                        element.parentNode.parentNode.removeAttribute("class");
                        element.checked = false;
                    }                    
                })
            ListImpr = [];
        }

        var ListBool;
        if (ListImpr.indexOf(choice) === -1) {
            ListImpr.push(choice);
            document.getElementsByName("checkTable")[choice].parentNode.parentNode.setAttribute("class", "CellColor");
            ListBool = true;
        } else {
            var a = ListImpr.indexOf(choice)
            ListImpr.splice(a, 1);
            document.getElementsByName("checkTable")[choice].parentNode.parentNode.removeAttribute("class");
            ListBool = false;
        }

        visuaizarBtn(ListImpr);
        ListaParaServicio(ListImpr);

    };

    function ListaParaServicio(ListImpr) {
        ListresultsId = [];
        for (var i = 0; i < ListImpr.length; i++) {
            var value = ListImpr[i];
            var Resultlist = Result.data[value][datasourceColumnId];
            ListresultsId.push(Resultlist);
        }

    }

    function visuaizarBtn(ListImpr) {
        if (ListImpr.length > 0) {
            $("#btnGardar").removeAttr("disabled");
        } else {
            $("#btnGardar").attr("disabled", true);
        }
    }

    function loadGrillaFormDoShowTable() {
        try {
            setTimeout(function () { document.getElementsByTagName("zamba-associated")[0].querySelector(".BtnRefresh").click(); });

        } catch (e) {
            console.log(e);
        }
    }
    
    var item = {
        name: '',
        value: ''
    };
});

app.directive('zambaDoshowtable', function ($sce) {

    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {            
            $(element).attr('innerHtml', attributes.value);
            $scope.ruleId = attributes.ruleId;
            $scope.reportId = attributes.reportId;
            datasourceColumnId = attributes.datasourceColumnId;
            datasourceColumn = attributes.datasourceColumn;
            $scope.valueForRuleQuery = attributes.valueForRuleQuery;
            $scope.parentTaskId = getElementFromQueryString("taskid");            
            $scope.datasourceRuleId = attributes.datasourceRuleId;
            $scope.datasourceVariable = attributes.datasourceVariable;
            $scope.buttonText = attributes.buttonText;
            multipleSelect = attributes.multipleSelect;
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/DoShowTablePro/DoShowTablePro.html'),
    };
});