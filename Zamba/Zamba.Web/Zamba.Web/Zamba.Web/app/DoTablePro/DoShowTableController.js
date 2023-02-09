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
  

    $scope.open = function (size) {
        $scope.data = [];

        $scope.btn = $scope.editable;

        $scope.LoadResults($scope.reportId, $scope.valueForRuleQuery, $scope.parentTaskId);

        if ($scope.data.length > 0) {
            ListImpr = [];
            ruleId = $scope.ruleId;
            parentTaskId = $scope.parentTaskId;
        } else {
            console.log("No se encontraron registros.");
            var scope_taskController = angular.element($("#taskController")).scope();
            scope_taskController.EvaluateRuleExecutionResult($scope.vars);
        }
    };

    $scope.LoadResults = function (reportId, valueForRuleQuery, parentTaskId) {
        try {
            SaveFormData();

            if (valueForRuleQuery != "")
                FormVariables = ValuesFromTable(valueForRuleQuery);

            if (($scope.reportId == undefined || $scope.reportId == "") && $scope.datasourceRuleId != "") {
                Result = DoShowTableServices.getResultsOrigin($scope.datasourceRuleId, reportId, FormVariables, parentTaskId, $scope.datasourceVariable);

                $scope.data = JSON.parse(Result.data);
                Result.data = $scope.data;
                $scope.vars = JSON.parse(Result.vars);
                Result.vars = $scope.vars;
            }

            if (($scope.datasourceRuleId == undefined || $scope.datasourceRuleId == "") && $scope.reportId != "") {
                Result = DoShowTableServices.getResults(reportId, FormVariables, parentTaskId);

                Result = JSON.parse(Result);
                $scope.data = Result.data;
            }

            return Result;
        } catch (e) {
            console.log(e);
        }
    };

    function saveIndexs() {}

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


    ListresultsId = [];


    $scope.ok = function () {
        ListIdResult = JSON.stringify(ListresultsId);
        ListIdResult = ListIdResult.replace(/[([^\]^"]*/g, "");
        var insert = DoShowTableServices.DoshowtableInsert(ruleId, FormVariables, parentTaskId, ListIdResult, $scope.datasourceColumnId, datasourceColumn);

        insert = JSON.parse(insert);
        var scope_taskController = angular.element($("#taskController")).scope();
        scope_taskController.EvaluateRuleExecutionResult(insert);

        $scope.ListresultsId = [];
        $scope.ListImpr = [];
        ListImpr = [];
        $("#btnGardar").attr("disabled", true);

        //loadGrillaFormDoShowTable();

        $scope.LoadResults($scope.reportId, $scope.valueForRuleQuery, $scope.parentTaskId);

        $scope.RefreshResource();
    };

    $scope.RefreshGrid = function () {
        try {
            $scope.LoadResults($scope.reportId, $scope.valueForRuleQuery, $scope.parentTaskId);
        } catch (e) {
            console.log(e.message);
        }
    }

    $scope.RefreshResource = function () {
        setTimeout(function () {
            if (document.getElementsByTagName("zamba-associated") != undefined) {
                var elementos = document.getElementsByTagName("zamba-associated");
                for (var i = 0; i < elementos.length; i++) {
                    var IdEntity = document.getElementsByTagName("zamba-associated")[i].getAttribute("entities");

                    LoadGrillaForm(IdEntity);
                }
            }
        }, 3000);
    }

    $scope.cancel = function () {
        ListImpr = [];
        Result = [];
        value = "";
        choice = "";
        $scope.ListresultsId = [];

        console.log("Recargando pagina...");
//        location.reload();
    };

    $scope.getEditable = function () {
        if (sessionStorage.getItem("editable")) {
            return sessionStorage.getItem("editable");
        }
    }

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
            $("#btnGuardar").removeAttr("disabled");
        } else {
            $("#btnGardar").attr("disabled", true);
            $("#btnGuardar").attr("disabled", true);
        }
    }

    function loadGrillaFormDoShowTable() {
        try {
            setTimeout(function () {
                $(document.getElementsByTagName("zamba-associated")).each(function (item) { $(item).find(".BtnRefresh").click(); });
            });
        } catch(e) {
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

            $scope.editable = attributes.editableVars;
            if ($scope.editable != undefined) {
                sessionStorage.setItem("editable", $scope.editable);
            } else {
                sessionStorage.setItem("editable", "false");
            }

            $scope.open('lg');

        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/DoTablePro/DoTable.html?v=168'),
    };
});