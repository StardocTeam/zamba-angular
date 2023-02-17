
app.controller('TaskController', function ($scope, $filter, $http, ZambaTaskService, authService) {
    $scope.rules = [];
    $scope.actionRules = null;
    $scope.taskResult = null;

    //authService._KeepAlive();
    //Prepara los parametros para ejecutar "Execute_ActionGrid".
    $scope.executeAcctionGrid = function (ruleAction) {
        var regla;

        if (ruleAction.RuleId != undefined) {
            regla = ruleAction.RuleId;
        } else if (ruleAction.ID != undefined) {
            regla = ruleAction.ID;
        }

        var listaDocIds = [];

        for (var i = 0; i < checkedIds.length; i++) {
            result = $scope.Search.SearchResults[checkedIds[i]];
            listaDocIds.push(result.DOC_ID);
        }

        $scope.Execute_ActionGrid(regla, listaDocIds);
    }

    //Ejecuta una regla pasada por parametro sobre una lista de IDs.
    $scope.ExecuteRule = function (ruleId, resultIds) {
        try {
            return ZambaTaskService.executeTaskRule(ruleId, resultIds);
        } catch (e) {
            console.error("ERROR: " + e + "Lanzado por: [$scope.ExecuteRule(" + ruleId + ", " + resultIds + ")]");
        }
    };

    ///Ejecuta una regla con parametros adicionales y devuelve un resultado en formato JSON (Grilla de Resultados).
    $scope.Execute_ActionGrid = function (ruleId, resultIds, formVars) {
        ZambaTaskService.executeAction_onItems(ruleId, resultIds)
            .then(function (response) {
                
                ret_Response = JSON.parse(response.data).Vars;
                $scope.EvaluateRuleExecutionResult(ret_Response);

                CloseModalActions();
                return ret_Response;
            });
    };

    ///Ejecuta una regla con parametros adicionales  y devuelve un resultado en formato JSON.
    $scope.Execute_ZambaRule = function (ruleId, resultIds, formVars) {
        ZambaTaskService.executeTaskRule(ruleId, resultIds, formVars)
            .then(function (response) {
                var ret_Response = JSON.parse(response.data).Vars;
                $scope.EvaluateRuleExecutionResult(ret_Response);

                return ret_Response;
            });
    };

    ///Ejecuta una regla con parametros adicionales  y devuelve un JSON
    $scope.Execute_zRule = function (ruleId, resultIds, formVars) {
        return ZambaTaskService.executeTaskRule(ruleId, resultIds, formVars);
            
    };

    //Ejecucion de Regla con nuevo Action para completado de atributos.
    $scope.zRule = function (ruleId, inputVars, outputVars) {

       
        let ResultValues = [];
        //assignInputVarsFromAttributes
        try {
            ResultValues = inputVars.map(item => {
                return {
                    name: Object.values(item).toString(),
                    value: document.querySelector("#" + Object.keys(item).toString()).value
                }
            })
        } catch (e) {
            console.log("validar que existan los id que se estan pasando en el form:" + e.message)
        }
        let resultIds = getElementFromQueryString("docid");

        // se realiza validacion ya que si un eleento esta null no dispara la zambarule
        let objectValue = ResultValues.find(item => item.value == "");

        if (objectValue == undefined) {

            // se optiene los valores 
            $scope.Execute_zRule(ruleId, resultIds, JSON.stringify(ResultValues)).then(response => {


                var ret_Response = JSON.parse(response.data).Vars;

                //se mapean los valores en el form
                outputVars.forEach(item => {

                    let itemValue = Object.values(item).toString();
                    document.querySelector("#" + Object.keys(item).toString()).value = ret_Response[itemValue];
                })

                if (ret_Response.accion != undefined) {
                    switch (traslateAction(ret_Response.accion)) {
                       
                        case "executescript":
                            eval(ret_Response.scripttoexecute.replace('window.close();', ''));
                            break;
                       
                        default:
                            console.log("$scope.EvaluateRuleExecutionResult: No reconocio la accion a ejecutar, error ortografico en la variable o falta agregar un 'case' en codigo?");
                            break;
                    }
                }

            })


        }

    };


    //Muestra mensajes y/o acciones según lo que obtenga del parametro.
    $scope.EvaluateRuleExecutionResult = function (executionResult) {
        try {
            if (executionResult.msg != undefined && executionResult.error != "") {
                swal(executionResult.msg);
            } else {
                console.log("$scope.EvaluateRuleExecutionResult: No existe variable 'msg' para mostrar.");

                if (executionResult.error != undefined && executionResult.error != "") {
                    swal('',executionResult.error,'error');
                }
                else {
                    console.log("$scope.EvaluateRuleExecutionResult: No existe variable 'error' para mostrar.");
                }
            }
            

            if (executionResult.accion != undefined) {
                switch (traslateAction(executionResult.accion)) {
                    case "update":
                        location.reload();
                        break;
                    case "close":
                        window.close();
                        break;
                    case "executerule":
                        $scope.Execute_ZambaRule(executionResult.ruleid, getElementFromQueryString("docid"));
                        break;
                    case "executescript":
                        eval(executionResult.scripttoexecute.replace('window.close();',''));
                        break;
                    case "doask":
                        $scope.doAsk(executionResult);
                        break;
                    case "refreshgrid":
                        searchModeGSFn(this, 'MyTasks');
                        break;
                    case "gohome":
                        searchModeGSFn(this, 'Home');
                        break;
                    case "opentask":
                        $scope.opentask();
                        break;
                    case "doshowtable":

                        break;
                    case "domail":
                        Console.log("execute DoMail")
                        break
                    default:
                        console.log("$scope.EvaluateRuleExecutionResult: No reconocio la accion a ejecutar, error ortografico en la variable o falta agregar un 'case' en codigo?");
                        break;
                }
            } else if (executionResult.Vars.accion != undefined) {
                switch (traslateAction(executionResult.Vars.accion)) {
                    case "domail": if (executionResult.Vars != undefined) {
                        let IdInfo = {};
                        let attachsIds = []; IdInfo.Docid = executionResult.Vars["generateddocid"]
                        IdInfo.DocTypeid = executionResult.Vars["nuevatarea.entityid"];
                        attachsIds.push(IdInfo);
                        sessionStorage.setItem("ResultNewTask-" + GetUID(), JSON.stringify(attachsIds)); Email_Click(executionResult.Params.Subject, executionResult.Params.Body, executionResult.Params.To, executionResult.Params.AttachLink, executionResult.Params.SendDocument, executionResult.Params.NextRuleIds, executionResult.Params.MailPathVariable, "", "");
                    } else {
                        swal("Error al ejecutar la DoMail")
                    } break
                    default:
                        console.log("$scope.EvaluateRuleExecutionResult: No reconocio la accion a ejecutar, error ortografico en la variable o falta agregar un 'case' en codigo?");
                        break;
                }
            } else {
                console.log("$scope.EvaluateRuleExecutionResult: No existe accion para ejecutar.");
            }
        } catch (e) {
            console.error(e + " - Lanzado por: [$scope.EvaluateRuleExecutionResult]");
            //swal("Sin resultados para el valor ingresado, intente con otro");
        }
    };

    //---------------------------------------------------------------------------------------------------------------------------------------
    //Metodo que recibe la llamada desde JavaScript puro para invokar a una regla desde un boton
    $scope.Execute_ZambaRuleId = function (event) {
        var ruleId = event.target.attributes["ruleid"].value;
        var resultIds = getElementFromQueryString("docid");

        $scope.Execute_ZambaRule(ruleId, resultIds);
    }

    function CloseModalActions() {
        $("#btnCloseModal").click();
    }

    ///Realiza una pregunta con swal para completar controles dentro del mismo.
    $scope.doAsk = function (executionResult) {
        try {
            var SwalControl = $scope.newInputConfiguration_ForSWAL(executionResult);
            //var SwalControl = $scope.newInputConfiguration_ForSWAL("text");

            //var SwalControl = $scope.newInputConfiguration_ForSWAL(executionResult.inputType, executionResult.classList);
            swal({
                text: executionResult.message,
                icon: "warning",
                content: SwalControl,
                buttons: {
                    ok: "OK",
                    cancel: "Cancelar"
                }
            })
                .then(function (value) {
                    if (value) {
                        var ResultValues = [];

                        var VarName = executionResult.resultvariable;
                        var IndexValue = $(".swal-content__input").val();
                        ResultValues.push({ name: VarName, value: IndexValue });
                        ResultValues = JSON.stringify(ResultValues);

                        $scope.Execute_ZambaRule(executionResult.ruleid, getElementFromQueryString("docid"), ResultValues);
                    } else {
                        $(".swal-content__input").val("");
                    }
                });
        } catch (e) {
            console.error(e + " - Lanzado por: $scope.doAsk");
        }
    };

    //Crea una etiqueta "input" que se usara en un mensaje SWAL. Este adoptara un tipo y comportamiento determinado por parametros.
    $scope.newInputConfiguration_ForSWAL = function (executionResult) {
        try {
            var attribute_Class = "";
            attribute_Class += "swal-content__input ";
            var newInput = document.createElement("input");
            newInput.setAttribute("id", "swal-content__input");
            newInput.setAttribute("autocomplete", "off");

            if (executionResult.function != undefined)
                attribute_Class += executionResult.function;

            if (executionResult.inputtype != undefined)
                newInput.type = executionResult.inputtype;
            else
                newInput.type = ""; //Sin InputType.

            newInput.setAttribute("class", attribute_Class);
            detectSubscriptionClass(newInput, executionResult.function);

            //Dimenciones para tipo "text".
            //TO DO: dimenciones segun tipo de input.
            switch (newInput.type) {
                case "text":
                //Sin dimenciones predeterminadas.
                default:
                    newInput.clientHeight = 37;
                    newInput.clientWidth = 436;

                    newInput.offsetHeight = newInput.clientHeight + 2;
                    newInput.offsetWidth = newInput.clientWidth + 2;
                    newInput.offsetLeft = 20;
                    newInput.offsetTop = 178;

                    newInput.scrollHeight = newInput.clientHeight;
                    newInput.scrollWidth = newInput.clientWidth;
            }

            newInput.isConnected = true;
            return newInput;
        } catch (e) {
            console.error(e + " - Lanzado por: $scope.newInputConfiguration_ForSWAL");
        }
    };


    $scope.getAttributeDescription = function (AttributeId, AttributeValue) {
        return ZambaTaskService.getAttributeDescription(AttributeId, AttributeValue);
    };

    $scope.getAttributeDescriptionMotivoDemanda = function (Motivo, Ramo, reportId) {
        return ZambaTaskService.getAttributeDescriptionMotivoDemanda(Motivo, Ramo, reportId);
    };



    $scope.getAttributeListMotivoDemanda = function (sender) {
        try {
            if ($(sender).val() !== '') {
                ZambaTaskService.getAttributeListMotivoDemanda($(sender).val(), 1525008).then(function (response) {

                    if (response.data !== undefined && response.data !== null) {

                        var motivoList = JSON.parse(response.data)[0].NAMELIST;
                        motivoList = JSON.parse(motivoList);
                        var ListaFinal = [];

                        var founded = false;
                        for (var i = 0; i < motivoList.length; i++) {
                            ListaFinal.push(motivoList[i]["cd_motivo"] + " - " + motivoList[i]["descripcion"]);
                            try {
                                if ($("#zamba_index_10173").val() != undefined && motivoList[i]["cd_motivo"] === $("#zamba_index_10173").val().split(' ')[0]) {
                                    founded = true;
                                }
                            } catch (e) {
                                console.error(e);
                            }
                        }

                        if (founded === false) $("#zamba_index_10173").val('');


                        $("#zamba_index_10173").autocomplete({
                            source: ListaFinal
                        }).focusout(function () {
                            ////var ui;
                            ////for (var i = 0; i < document.getElementsByTagName("ul").length; i++) {
                            ////    if (document.getElementsByTagName("ul")[i].id.indexOf("ui-id") != -1) {
                            ////        if (document.getElementsByTagName("ul")[i].id != "ui-id-1") {
                            ////            ui = document.getElementsByTagName("ul")[i].id;
                            ////        }
                            ////    }
                            ////}

                            ////var Cd = $("#zamba_index_10173").val().substring(0, 3);
                            ////for (var i = 0; i < AutoComplet.length; i++) {
                            ////    if (Cd == AutoComplet[i]["cd_motivo"]) {
                            ////        $("#zamba_index_10173").val(AutoComplet[i]["cd_motivo"] + " - " + AutoComplet[i]["descripcion"]);
                            ////    }
                            ////}

                        }).on("focus", function () {
                            $(this).autocomplete("search", " ");
                        });
                    }

                });


            }
        } catch (e) {
            console.error(e);
            $scope.task.motivodescripcion = '';
        }
    };


    $scope.ObtenerDescripciondeMotivo = function () {
        try {
            if ($("#zamba_index_19").val() !== '' && $("#zamba_index_10173").val() !== '') {

                ZambaTaskService.getAttributeListMotivoDemanda($("#zamba_index_19").val(), 1525008).then(function (response) {

                    if (response.data !== undefined && response.data !== null) {
                        if (response.data != null && response.data != '') {
                            var motivoList = JSON.parse(response.data)[0].NAMELIST;
                            motivoList = JSON.parse(motivoList);
                            var ListaFinal = [];

                            var founded = false;
                            for (var i = 0; i < motivoList.length; i++) {
                                ListaFinal.push(motivoList[i]["cd_motivo"] + " - " + motivoList[i]["descripcion"]);
                                try {
                                    if ($("#zamba_index_10173").val() != undefined && motivoList[i]["cd_motivo"] === $("#zamba_index_10173").val().split(' ')[0]) {
                                        founded = true;
                                        $("#zamba_index_10264").val(motivoList[i]["descripcion"]);
                                        SaveIndexbyId(10264, motivoList[i]["descripcion"]);
                                    }
                                } catch (e) {
                                    console.error(e);
                                }
                            }
                            if (founded === false) {
                                $("#zamba_index_10264").val('');
                                SaveIndexbyId(10264, '');
                                //                            $("#zamba_index_10173").val('');
                                //                          SaveIndexbyId(10173, '');
                            }
                        }
                    }
                });
            }
        } catch (e) {
            console.error(e);
            $scope.task.motivodescripcion = '';
        }
    };

    $scope.MarkAsFavorite = function () {
        $scope.taskResult._IsFavorite = !$scope.taskResult._IsFavorite;
        ZambaTaskService.MarkAsFavorite(GetTASKID(), GetUID(), $scope.taskResult._IsFavorite);
        
    };

    //Obtiene las ruleActions relacionados al usuario.
    $scope.getResultsGridActions = function () {
        if ($scope.rules == undefined || $scope.rules.length == 0) {
            var result = ZambaTaskService.getResultsGridActions(GetUID());
            $scope.rules = result;
        }

        if ($("#chkThumbGrid")[0].checked)
            document.getElementById("panel_ruleActions").hidden = true;        
        else 
            document.getElementById("panel_ruleActions").hidden = false;        

        return $scope.rules;
    }

    //Obtiene las ruleActions relacionados al usuario.
    $scope.LoadUserAction_ForMyTaskGrid = function (STEP_ID, DOC_ID) {
        $scope.actionRules = JSON.parse(ZambaTaskService.LoadUserAction_ForMyTaskGrid(STEP_ID, DOC_ID));

        if ($("#chkThumbGrid")[0].checked)
            document.getElementById("panel_ruleActions").hidden = true;        
        else 
            document.getElementById("panel_ruleActions").hidden = false;
    }

});


function getAttributeListMotivoDemanda(sender) {
    try {
        var scope = angular.element(document.getElementById("taskController")).scope();
        if ($(sender).val() != '')
            scope.getAttributeListMotivoDemanda(sender);

    } catch (e) {
        console.error(e);
    }
}



//Ejecuta o evalua la ejecucion de una de las funciones "subscribeItems" sobre un elemento especifico con parametros determinados.
function detectSubscriptionClass(elemento, claseHTML) {
    try {
        var stringFunction = "subscribeItem_";
        stringFunction += claseHTML + "(elemento);";

        eval(stringFunction);
    } catch (e) {
        console.log(e.message + " lanzado por " + "[detectSubscriptionClass(claseHTML, elemento)]");
    }
}

function LoadMotivoDemandaListAndDescription() {

    try {
        if ($("#zamba_index_19").val() !== undefined && $("#zamba_index_19").val() !== null && $("#zamba_index_19").val() !== '') {
            var scope = angular.element(document.getElementById("taskController")).scope();

            var ramo = $("#zamba_index_19").val();


            if ($("#zamba_index_10173").val() !== undefined && $("#zamba_index_10173").val() !== null && $("#zamba_index_10173").val() !== '') {
                var motivo = $("#zamba_index_10173").val();
                motivo = motivo.split(' ')[0];
                scope.getAttributeDescriptionMotivoDemanda(motivo, ramo, 1525007).then(function (response) {

                    if (response.data !== undefined && response.data !== null)
                        $("#zamba_index_10173").val(motivo + ' - ' + response.data);
                    else
                        $("#zamba_index_10173").val(motivo);
                });
            }

            scope.getAttributeListMotivoDemanda($("#zamba_index_19"));
        }
        else {
            $("#zamba_index_10173").val('');
        }

    } catch (e) {
        console.error(e);

    }


}

$(document).ready(function () {
    $(".ZRule").each(function (index, elem) {
        $(elem).on({
            "click": Execute_ZambaRuleId
        });
    });

    $(".autoSave").each(function (index) {
        $(this).on("change", function () {
            SaveIndex(this);
        });
    });
});

function Execute_ZambaRuleId(event) {
    var scope_taskController = angular.element($("#taskController")).scope();
    scope_taskController.Execute_ZambaRule(event.target.attributes["ruleid"].value, getElementFromQueryString("docid"));
}

function traslateAction(palabra) {
    try {
        switch (palabra.toLowerCase()) {
            case "actualizar":
                return "update";
                break;
            case "cerrar":
                return "close";
                break;
            case "ejecutar regla":
                return "executerule";
                break;
            case "preguntar":
            case "consultar":
                return "doask";
                break;
            case "actualizar grilla":
            case "refrescar grilla":
                return "refreshgrid";
                break;
            case "ir a home":
            case "ir al home":
                return "gohome";
                break;

            default:
                return palabra;
                break;
        }
    } catch (e) {
        console.error(e + " - Lanzado por: " + "[" + arguments.callee.name + "]");
    }
}

function SaveIndex(_this) {
    var indexId = _this.id.split("_")[2];
    var indexValue = _this.value;

    var entityId = getElementFromQueryString("DocType");
    var parentResultId = getElementFromQueryString("docid");
    var taskId = 0;
    if (entityId === null) {
        taskId = getElementFromQueryString("taskid");
        entityId = 0;
    }
    console.log("Guardando index: " + indexId + " valor: " + indexValue);
    saveIndexValidated(indexId, entityId, parentResultId, taskId, indexValue);

    var refIndexs = $(_this).attr("refIndexs");
    console.log("refIndexs:" + refIndexs);
    if (refIndexs !== undefined) {
        var refIndexsList = refIndexs.split(',');
        $(refIndexsList).each(function (item) {
            console.log("refIndex:" + this);
            saveIndexValidated(this, entityId, parentResultId, taskId, indexValue);
        });
    }
}

function SaveIndexbyId(indexId, indexValue) {

    var entityId = getElementFromQueryString("DocType");
    var parentResultId = getElementFromQueryString("docid");
    var taskId = 0;
    if (entityId === null) {
        taskId = getElementFromQueryString("taskid");
        entityId = 0;
    }
    console.log("Guardando index: " + indexId + " valor: " + indexValue);
    saveIndexValidated(indexId, entityId, parentResultId, taskId, indexValue);

}

