'use strict';

var app = angular.module('app', ['ngAnimate', 'ngStorage', 'ngMaterial']);

app.run(['$http', '$q', '$rootScope', function ($http, $q, $rootScope) {
}]);


var ZambaWebRestApiURL = window.location.origin + window.location.pathname.substring(0, window.location.pathname.indexOf('/', 2)).replace('.', '') + ".restapi/api";
var ZambaUrl = window.location.origin + window.location.pathname.substring(0, window.location.pathname.indexOf('/', 2));
var _appOrigin = "Mobile";
var _mobile_userId = "";


//Config for Cross Domain
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]);


app.controller('RequestController', function ($scope, $filter, $http, RequestServices, DocumentViewerServices, $localStorage, $sessionStorage, $mdDialog) {

    $scope.$storage = $localStorage;

    $scope.currentView = '';
    $scope.Titulo = "Procesando";
    $scope.Texto = "La operacion se esta llevando a cabo, por favor espere...";
    $scope.ComentarioResumen = "";
    $scope.alreadyAuth = false;
    $scope.username = "";
    $scope.pass = "";
    $scope.userid = "";
    $scope.loteid = "";
    $scope.accionid = "0";
    $scope.acciontypeid = "0";
    $scope.ListFacturas = [];
    $scope.loginFail = false;
    $scope.iframeControls = [];

    $scope.MS_Modal_Title = "";
    $scope.MS_Modal_Body = "";

    $scope.loginFailText = '';
    $scope.BtnsAccionesError = false;
    $scope.BtnsAcciones = false;

    //Variables de directiva ManagerialSummary
    $scope.selectedSummary = null;
    $scope.showPreviewSummary = null;
    $scope.SummaryList = [];

    $scope.selectedInvoices = [];
    $scope.selectedInvoicesLate = [];
    $scope.seeLaterList = [];
    $scope.totalInvoicesPendingCount = 0;
    $scope.totalInvoicesLateCount = 0;

    $scope.showPendingTab = true;
    //Reglas:
    //Regla Aprobar Todos(pagos / facturas11546636) variable IDAprobar
    //Regla Aprobar Todos(pagos / facturas11547097) variable IDUsuario
    //Regla Aprobar 1223308
    //Regla Rechazar 1021324 variable motivo La regla cambio por: 11546880

    //Regla Conformar Todos MAIL(11546640) variable IDConformar
    //Regla Conformar Todos APP (11547091) variable IDUsuario
    //Regla Conformar 1021731
    //Regla Rechazar 1022138 variable motivo

    //ID reportes
    //ID reporte Aprobacion 11535116
    //ID reporte Conformacion 11535117

    $scope.reportId = 11535116;
    $scope.AprobarId = 1223308;
    $scope.AprobarTodosId = 11546636;
    $scope.RechazarId = 11546880;
    $scope.AprobarTodosForMailId = 11546636;

    $scope.LoadResults = function () {
        var d = RequestServices.getResults($scope.userid, $scope.reportId);
        //alert(d);
        if (d != null && d != "") {

            let invoicesList = JSON.parse(d);
            invoicesList.forEach(function (invoice) {
                invoice.checked = false;
            });

           
            let pendingInvoiceList = [];
            if (localStorage.getItem($scope.currentView +'-'+ $scope.userid) != null) {
                var cacheList = JSON.parse(localStorage.getItem($scope.currentView + '-' + $scope.userid));
                $scope.seeLaterList = cacheList;
            }
            //comprueba que la factura NO este en el listado de Ver Despues
            if ($scope.seeLaterList.length > 0)
                pendingInvoiceList = invoicesList.filter(item1 => !$scope.seeLaterList.some(item2 => item2.ID1 === item1.ID1));
            else
                pendingInvoiceList = invoicesList;

            $scope.ListFacturas = pendingInvoiceList;
            $scope.totalInvoicesPendingCount = $scope.ListFacturas.length;
            $scope.totalInvoicesLateCount = $scope.seeLaterList.length;

            $scope.refreshIframe($scope.ListFacturas);
            $scope.refreshIframe($scope.seeLaterList);
        }
    };

    $scope.refreshIframe = function (list) {
        setTimeout(function () {
            $scope.iframeControls = [];
            list.forEach(function (element) {

                console.log(element.ID);
                //$scope.iframeID = element.ID;
                $scope.GetFacturaArchive(element.EID, element.ID1, element.ID);

                $("a[id=" + element.ID + "]").attr("href", ZambaUrl + "/views/WF/TaskViewer?DocType=" + element.EID + "&docid=" + element.ID1 + "&taskid=" + element.TID + "&mode=s&s=13&user=" + $scope.userid + "#Zamba/");

            });

        }, 1000);
    }


    $scope.LoadResultsForManagerialSummary = function () {
       
        var data = RequestServices.getResults($scope.userid, $scope.reportId);

        if (data != null && data != "") {

            $scope.SummaryList = JSON.parse(data);
        }
    };

    //TODO: hay que validar de quitar y agregar a la lista dependiendo del estado de checked
    $scope.sentToMultiSelect = function (item) {
        item.checked = !item.checked;
        

      
            if (item.checked) {
                if ($scope.showPendingTab) {
                    let filteredItem = $scope.ListFacturas.filter(element => { return element.ID === item.ID && element.ID1 === item.ID1 });
                    if (filteredItem.length > 0)
                        $scope.selectedInvoices.push(filteredItem[0]);
                } else {
                    let filteredItem = $scope.seeLaterList.filter(element => { return element.ID === item.ID && element.ID1 === item.ID1 });
                    if (filteredItem.length > 0)
                        $scope.selectedInvoicesLate.push(filteredItem[0]);

                }
               
            } else {
                if ($scope.showPendingTab) {
                    $scope.removeInvoiceFromPendingList($scope.selectedInvoices, item.ID, item.ID1)
                } else {
                    $scope.removeInvoiceFromPendingList($scope.selectedInvoicesLate, item.ID, item.ID1)
                }
               
            }          
        
    }

    $scope.sendToseeLaterList = function (item) {
        try {
            var filteredItem = $scope.ListFacturas.filter(element => { return element.ID === item.ID && element.ID1 === item.ID1 });
            if (filteredItem != undefined) {
                $scope.seeLaterList.push(filteredItem[0]);
                $scope.removeInvoiceFromPendingList($scope.ListFacturas, item.ID, item.ID1);

                localStorage.setItem($scope.currentView + '-' + $scope.userid, JSON.stringify($scope.seeLaterList));
                console.log($scope.currentView + '-' + $scope.userid);
                $scope.totalInvoicesPendingCount = $scope.ListFacturas.length;
                $scope.totalInvoicesLateCount = $scope.seeLaterList.length;
            }


        } catch (e) {
            console.error(e);
        }
        
    }

    $scope.removeInvoiceFromPendingList = function (list,ID,ID1) {
        var itemIndex = list.findIndex(function (item) {
            return item.ID === ID && item.ID1 === ID1;
        });
        list.splice(itemIndex, 1);
    }

    $scope.FormsVariables = '[]'

    $scope.SignOut = function () {
        $scope.alreadyAuth = false;
        window.close();
    }

    $scope.Aprobar = function (docId, idElemento) {
        showLoading();
        RequestServices.executeTaskRule($scope.userid, $scope.AprobarId, docId, null)
            .then(function (result) {
                result = JsonValidator(result);
                ResponseNotification(result, idElemento, docId);
                $scope.LoadResults();
                hideLoading();
            });
    }

    $scope.ApproveOnlySelectedInvoices = function () {
        try {
            showLoading();
            if ($scope.showPendingTab)
                $scope.selectedInvoicesAux = [...$scope.selectedInvoices];
            else
                $scope.selectedInvoicesAux = [...$scope.selectedInvoicesLate];

            $scope.selectedInvoicesAux.forEach(function (item, i) {
               

                var resultIds = [{
                    Docid: item.ID1,
                    DocTypeid: item.EID
                }]
                RequestServices.executeTaskRule($scope.userid, $scope.AprobarId, JSON.stringify(resultIds), null)
                    .then(function (result) {

                        if (!$scope.showPendingTab) {
                            if (localStorage.getItem($scope.currentView + '-' + $scope.userid) != null) {

                                var cacheList = JSON.parse(localStorage.getItem($scope.currentView + '-' + $scope.userid));

                                $scope.removeInvoiceFromPendingList(cacheList, item.ID, item.ID1);
                                localStorage.setItem($scope.currentView + '-' + $scope.userid, JSON.stringify(cacheList));
                            }
                        }
                        result = JsonValidator(result);

                        ResponseNotificationForSelectedInvoices(result, item.ID, item.ID1);
                        if (($scope.selectedInvoicesAux.length - 1) == i) {
                            hideLoading();
                            Swal.fire({
                                icon: 'success',
                                title: "Operacion exitosa",
                                timer: 4000,
                                showConfirmButton: false,
                                text: ""
                            })
                            $scope.LoadResults();
                            if ($scope.showPendingTab)
                                $scope.selectedInvoices = [];
                            else
                                $scope.selectedInvoicesLate = [];
                        }
                    });
            });
        } catch (e) {
            hideLoading();
            Swal.fire({
                icon: 'error',
                title: "ERROR ",
                timer: 4000,
                text: "Ha ocurrido un error,vuelva a intentarlo mas tarde."
            })
        }
    }

    $scope.ApproveOnlySelectedInvoicesAll = function () {
        try {
            showLoading();
            if ($scope.showPendingTab)
                $scope.selectedInvoicesAux = [...$scope.ListFacturas];
            else
                $scope.selectedInvoicesAux = [...$scope.seeLaterList];

            $scope.selectedInvoicesAux.forEach(function (item, i) {


                var resultIds = [{
                    Docid: item.ID1,
                    DocTypeid: item.EID
                }]
                RequestServices.executeTaskRule($scope.userid, $scope.AprobarId, JSON.stringify(resultIds), null)
                    .then(function (result) {

                        if (!$scope.showPendingTab) {
                            if (localStorage.getItem($scope.currentView + '-' + $scope.userid) != null) {

                                var cacheList = JSON.parse(localStorage.getItem($scope.currentView + '-' + $scope.userid));

                                $scope.removeInvoiceFromPendingList(cacheList, item.ID, item.ID1);
                                localStorage.setItem($scope.currentView + '-' + $scope.userid, JSON.stringify(cacheList));
                            }
                        }

                        result = JsonValidator(result);

                        ResponseNotificationForSelectedInvoices(result, item.ID, item.ID1);


                        if (($scope.selectedInvoicesAux.length - 1) == i) {
                            hideLoading();
                            Swal.fire({
                                icon: 'success',
                                title: "Operacion exitosa",
                                timer: 4000,
                                showConfirmButton: false,
                                text: ""
                            })
                            $scope.LoadResults();
                            if ($scope.showPendingTab)
                                $scope.ListFacturas = [];
                            else
                                $scope.seeLaterList = [];
                        }
                    });
            });
        } catch (e) {
            hideLoading();
            Swal.fire({
                icon: 'error',
                title: "ERROR ",
                timer: 4000,
                text: "Ha ocurrido un error,vuelva a intentarlo mas tarde."
            })
        }
    }



    $scope.AprobarResumenGerencial = function (docId, idElemento) {
        Swal.fire({
            text: 'Agregue un comentario de aprobacion.',
            input: 'textarea',
            cancelButtonText: 'cerrar',
            showCancelButton: true
        }).then(function (result) {
            showLoading();

            if (result.value != undefined) {
                if (result.value != null) {
                    $scope.FormsVariables = '[{"name":"ObsAprob", "value":"' + result.value + '"}]'
                    RequestServices.executeTaskRule($scope.userid, $scope.Aprobar_ResumenGerencialId, docId, $scope.FormsVariables).then(function (result) {
                        result = JsonValidator(result);
                        ResponseNotification(result, idElemento, docId);

                        $scope.selectedSummary = null;
                        $scope.showPreviewSummary = null;
                        $scope.LoadResultsForManagerialSummary();
                    });
                } else {
                    Swal.fire({
                        icon: 'warning',
                        title: "Campo vacio",
                        timer: 4000,
                        text: "Debe escribir algo en el campo para enviar un comentario."
                    });
                }
            }

            hideLoading();
        })
    }


    $scope.DevolverResumenGerencial = function (docId, idElemento) {
        Swal.fire({
            text: 'Agregue un comentario por la devolucion.',
            input: 'textarea',
            cancelButtonText: 'cerrar',
            showCancelButton: true
        }).then(function (result) {
            showLoading();

            if (result.value != undefined) {
                if (result.value != null) {


                    if (result.value != "") {
                        
                    $scope.FormsVariables = '[{"name":"ObsAprob", "value":"' + result.value + '"}]'
                    RequestServices.executeTaskRule($scope.userid, $scope.Devolver_ResumenGerencialId, docId, $scope.FormsVariables).then(function (result) {
                        result = JsonValidator(result);
                        ResponseNotification_DevolverResumenGerencial(result, idElemento, docId);  

                        $scope.selectedSummary = null;
                        $scope.showPreviewSummary = null;
                        $scope.LoadResultsForManagerialSummary();
                    });
                    }
                    else{
                        Swal.fire({
                            icon: 'warning',
                            title: "Campo vacio",
                            timer: 4000,
                            text: "Debe escribir algo en el campo para enviar un comentario."
                        });
                    }    


                } else {
                    Swal.fire({
                        icon: 'warning',
                        title: "Campo vacio",
                        timer: 4000,
                        text: "Debe escribir algo en el campo para enviar un comentario."
                    });
                }
            }

            hideLoading();
        })

        // $scope.FormsVariables = '[{"name":"ObsAprob", "value":""}]';
        // RequestServices.executeTaskRule($scope.userid, $scope.Devolver_ResumenGerencialId, docId, $scope.FormsVariables).then(function (result) {
            
            
        //     result = JsonValidator(result);
        //     ResponseNotification(result, idElemento, docId);

        //     Swal.fire({
        //             text: 'Agregue un comentario por la devolucion.',
        //             input: 'textarea',
        //             cancelButtonText: 'cerrar',
        //             showCancelButton: true
        //         }).then(function(value){
        //         });

        //     $scope.selectedSummary = null;
        //     $scope.LoadResultsForManagerialSummary();
        // });
    }

    $scope.DevolverResumenGerencialAnterior = function (docId, e) {
        var d = RequestServices.getResults(docId, $scope.Devolver_RG_Anterior);

        if (d != null && d != "" && d != "[]") {
            $scope.setSelectedPreviewSummary(d, e);
            $scope.showPreviewSummary = null;
        } else {
            e.stopPropagation();
        }

        
    }

    $scope.ComentarResumenGerencial = function (docId, idElemento, Texto) {
        showLoading();
        if (Texto != "") {
            $scope.FormsVariables = '[{"name":"ObsAprob", "value":"' + Texto + '"}]'
            RequestServices.executeTaskRule($scope.userid, $scope.Comentar_ResumenGerencialId, docId, $scope.FormsVariables).then(function (result) {
                result = JsonValidator(result);

                ResponseNotification(result, idElemento, docId);
                $("#ComentarioResumenId").val("");
                $("#btnActualizar").click();
            });
        } else {
            Swal.fire({
                icon: 'warning',
                title: "Campo vacio",
                timer: 4000,
                text: "Debe escribir algo en el campo para enviar un comentario."
            });
        }

        hideLoading();
    }


    $scope.ValSwitch = function (obj) {
        if (obj != null) {
            if (obj.toString().toLowerCase() == "si") {
                setTimeout(function () { $("#SwitchRecupero").attr("checked", ""); }, 1000);
            } else if (obj.toString().toLowerCase() == "no") {
                setTimeout(function () { $("#SwitchRecupero").removeAttr("checked"); }, 1000);
            }
        } else {
            obj = "no";
        }

        return obj;
    }


    $scope.Formatter = function (num) {
        if (num == null) {
            return numeral(0).format('0,0.00');
        } else {
            var rdo = num.replaceAll('.', '').replace(',', '.');
            return numeral(parseFloat(rdo)).format('0,0.00');
        }
    }

    $scope.FormatterDate = function (date) {
        if (date == null) 
            return "";
         else 
            return moment(date).format("DD/MM/YYYY");
    }

    $scope.sumaTotal = function (obj) {
        var NumTOTAL_CAPITAL = (obj.TOTAL_CAPITAL != null) ? parseFloat(obj.TOTAL_CAPITAL.replaceAll('.', '').replace(',', '.')) : 0;
        var NumCOSTAS = (obj.COSTAS != null) ? parseFloat(obj.COSTAS.replaceAll('.', '').replace(',', '.')) : 0;

        var rdo = NumTOTAL_CAPITAL + NumCOSTAS;

        return numeral(rdo).format('0,0.00');
    }

    $scope.AprobarTodos = function () {
        showLoading();
        $scope.FormsVariables = '[{"name":"IDAprobar", "value":"' + $scope.loteid.toString() + '"},{"name":"IDUsuario", "value":"' + $scope.userid.toString() + '"}]'
        RequestServices.executeTaskRule($scope.userid, $scope.AprobarTodosId, 0, $scope.FormsVariables, $scope.AprobarTodosResult)
            .then(function (result) {
                result = JsonValidator(result);
                ResponseNotification_ForAprobarTodos(result);
                $scope.LoadResults();
                hideLoading();
            });
    }
    

    $scope.MenuAncla = function () {
        if ($("#MenuOpcionesAncla")[0].style.display == "none") {
            $("#MenuOpcionesAncla")[0].style.display = "block"
        } else {
            $("#MenuOpcionesAncla")[0].style.display = "none"
        }
    }

    $scope.CerrarMenuAncla = function () {
        $("#MenuOpcionesAncla")[0].style.display = "none"
    }

    $scope.alert = function (asd) {
        alert(asd);
    };

    $scope.Rechazar = function (docId, idElemento) {

        var a = false
        Swal.fire({
            title: 'Motivo del rechazo',
            input: 'textarea',
            cancelButtonText: 'Cerrar',
            showCancelButton: true
        }).then(function (result) {
            showLoading();
            if (result.value) {
                $scope.FormsVariables = '[{"name":"motivo", "value":"' + result.value + '"}]'
                RequestServices.executeTaskRule($scope.userid, $scope.RechazarId, docId, $scope.FormsVariables).then(function (result) {
                    result = JsonValidator(result);
                    ResponseNotification(result, idElemento, docId);


                    $scope.LoadResults();
                    hideLoading();
                });
            }
            else if (result.isDismissed != true) {
                hideLoading();
                Swal.fire({
                    title: 'Ingresa el motivo de rechazo'
                });
            } else {
                hideLoading();

            }
        })



    }

    $scope.AprobarTodosForMail = function () {
        showLoading();
        console.log('Loading ON');
        $scope.FormsVariables = '[{"name":"IDAprobar", "value":"' + $scope.loteid.toString() + '"},{"name":"IDUsuario", "value":"' + $scope.userid.toString() + '"}]'
        RequestServices.executeTaskRule($scope.userid, $scope.AprobarTodosForMailId, 0, $scope.FormsVariables).then(function (result) {
            console.log('Async Service Response');

            result = JsonValidator(result);
            hideLoading();
            console.log('Loading OFF');
            ResponseNotification_ForAprobarTodos_ForRequestAction(result);
            console.log('Notification OFF');
        });
        console.log('App Methond End');

    }


    function ResponseNotification_DevolverResumenGerencial(result, idElemento, docId) {
        if (result.Vars.error == "") {
            Swal.fire({
                icon: 'success',
                title: "Devolucion completada",
                timer: 4000,
                text: "Se Realizo la devolucion del resumen gerencial al referente interno"
            });
        }
    }

    function ResponseNotificationForSelectedInvoices(result, idElemento, docId) {
        $("#" + idElemento + "-" + docId).addClass("animate__animated animate__backOutLeft");
        setTimeout(function () { $("#" + idElemento + "-" + docId).css("display", "none"); }, 500);

    }


    function ResponseNotification(result, idElemento, docId) {
        if (result.executionResult != 1 && result.executionResult != 2 && result.executionResult != 3) {
            Swal.fire({
                icon: 'error',
                title: "ERROR ",
                timer: 4000,
                text: "Ha ocurrido un error,vuelva a intentarlo mas tarde."
            })

        } else {
            $("#" + idElemento + "-" + docId).addClass("animate__animated animate__backOutLeft");
            setTimeout(function () { $("#" + idElemento + "-" + docId).css("display", "none"); }, 1000);

            Swal.fire({
                icon: 'success',
                title: "Operacion exitosa",
                timer: 4000,
                showConfirmButton: false,
                text: ""
            })
        }
    }

    function ResponseNotification_ForAprobarTodos_ForRequestAction(result) {

        if (result.executionResult != 1 && result.executionResult != 3) {

            //Cambiar color de header del card en success
            $scope.BtnsAccionesError = false;
            $scope.BtnsAcciones = false;
            $scope.Titulo = "Error";
            $scope.Texto = result.Vars.error;
            $("#ResultCard").removeAttr("class");
            $("#ResultCard").attr("class", "card-header bg-danger text-light");

            $scope.actionid = "0";

            $scope.RevisarFactura();

            Swal.fire({
                icon: 'error',
                title: "ERROR ",
                timer: 6000,
                text: "Ha ocurrido un error,vuelva a intentarlo mas tarde."
            })

        } else {
            $scope.BtnsAccionesError = false;
            $scope.BtnsAcciones = false;
            $scope.Titulo = "Operacion Exitosa";
            $scope.Texto = "Se han aprobado todas las tareas.";
            $("#ResultCard").removeAttr("class");
            $("#ResultCard").attr("class", "card-header bg-success text-light");

            $scope.RevisarFactura();

            Swal.fire({
                icon: 'success',
                title: "Operacion exitosa",
                timer: 6000,
                showConfirmButton: false,
                text: ""
            })

        }


    }


    function ResponseNotification_ForAprobarTodos(result) {
        if (result.executionResult != 1 && result.executionResult != 3) {

            //Cambiar color de header del card en success
            $scope.BtnsAccionesError = true;
            $scope.BtnsAcciones = false;
            $scope.Titulo = "Error";
            $scope.Texto = result.Vars.error;
            $("#ResultCard").removeAttr("class");
            $("#ResultCard").attr("class", "card-header bg-danger text-light");

            $scope.actionid = "0";

            Swal.fire({
                icon: 'error',
                title: "ERROR ",
                timer: 4000,
                text: "Ha ocurrido un error,vuelva a intentarlo mas tarde."
            })

        } else {
            $scope.Titulo = "Operacion Exitosa";
            $scope.Texto = "Se han aprobado todas las tareas.";
            $("#ResultCard").removeAttr("class");
            $("#ResultCard").attr("class", "card-header bg-success text-light");

            $scope.BtnsAcciones = true;

            $scope.ListFacturas = [];


            Swal.fire({
                icon: 'success',
                title: "Operacion exitosa",
                timer: 4000,
                text: ""
            })
        }


    }




    function JsonValidator(Obj) {
        try {
            return JSON.parse(Obj);;
        } catch (e) {
            console.error(e);
            return Obj;
        }
    }


    $scope.CierreMenu = function (inputs) {

        $("#MenuOpciones")[0].style.display = "none"
    }

    $scope.ChangePassword = function (inputs) {

        var CurrentPassword = inputs.CurrentPassword || "",
            NewPassword = inputs.NewPassword || "",
            NewPassword2 = inputs.NewPassword2 || "";


        //Evaluacion de politicas de Password

        var PasswordOk = true;
        var PassTextInfo = "";

        if (NewPassword.includes($scope.username) || NewPassword2.includes($scope.username)) {
            PasswordOk = false;
            PassTextInfo = "La clave no puede llevar el nombre del usurario"

        } else if (NewPassword.length <= 5 || NewPassword2.length <= 5) {
            PasswordOk = false;
            PassTextInfo = "La longitud tiene que ser mayor a 6 caracteres"

        } else if (contarMayusculas(NewPassword) < 2) {
            PasswordOk = false;
            PassTextInfo = "La contraseña debe de tener mas de 3 mayusculas"

        } else if (contarMinusculas(NewPassword) < 2) {
            PasswordOk = false;
            PassTextInfo = "La contraseña debe de tener mas de 3 minusculas"

        } else if (contarNumeros(NewPassword) < 2) {
            PasswordOk = false;
            PassTextInfo = "La contraseña debe de tener mas de 3 Numeros"

        } else if (contarCaracteres(NewPassword) < 2) {
            PasswordOk = false;
            PassTextInfo = "La contraseña debe de tener mas de 3 caracteres especiales"

        }


        if (PasswordOk == true) {
            RequestServices.ChangePass($scope.userid, inputs)
        } else {
            Swal.fire("", PassTextInfo, "error");
        }




    }

    function contarMayusculas(cadena) {
        var contar = 0;
        var mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (var i = 0; i < mayusculas.length; i++) {
            for (var x = 0; x < cadena.length; x++) {
                if (cadena[x] == mayusculas[i]) {
                    contar += 1;
                }
            }
        }
        return contar;
    }

    function contarMinusculas(cadena) {
        var contar = 0;
        var mayusculas = "abcdefghijklmnopqrstuvwxyz";
        for (var i = 0; i < mayusculas.length; i++) {
            for (var x = 0; x < cadena.length; x++) {
                if (cadena[x] == mayusculas[i]) {
                    contar += 1;
                }
            }
        }
        return contar;
    }

    function contarNumeros(cadena) {
        var contar = 0;
        var mayusculas = "0123456789";
        for (var i = 0; i < mayusculas.length; i++) {
            for (var x = 0; x < cadena.length; x++) {
                if (cadena[x] == mayusculas[i]) {
                    contar += 1;
                }
            }
        }
        return contar;
    }

    function contarCaracteres(cadena) {
        var contar = 0;
        var mayusculas = "!$#%.";
        for (var i = 0; i < mayusculas.length; i++) {
            for (var x = 0; x < cadena.length; x++) {
                if (cadena[x] == mayusculas[i]) {
                    contar += 1;
                }
            }
        }
        return contar;
    }



    $scope.Openopcionmenu = function (inputs) {

        if ($("#MenuOpciones")[0].style.display == "none") {
            $("#MenuOpciones")[0].style.display = "block"
        } else {
            $("#MenuOpciones")[0].style.display = "none"
        }


    }

    $scope.OpenopcionmenuDos = function (inputs) {
        if ($("#Resumen_Menu")[0].style.display == "none") {
            $("#Resumen_Menu")[0].style.display = "block"
        } else {
            $("#Resumen_Menu")[0].style.display = "none"
        }
        alert("OpenopcionmenuDos");
    }

    $scope.CierreMenuDos = function (inputs) {
        $("#MenuOpciones")[0].style.display = "none"
        alert("CierreMenuDos");
    }



    $scope.GetUsername = function () {

        var urlParams = $scope.getUrlParameters();

        $scope.setUrlParameters(urlParams);

        var valores = window.location.search;
        var urlParamsNew = new URLSearchParams(valores);
        var userid = urlParamsNew.get('u');


        var username = RequestServices.getUsername(userid);
        if (username != undefined && username != "") {
            $scope.username = username;
            $("#Pass").focus();
        }

        var storageToken = $scope.$storage.token;
        var storageUserName = $scope.$storage.username;
        var storageUserId = $scope.$storage.userid;

        if (username == storageUserName && storageToken != null) {
            //mismo usuario
            $scope.loginFail = false;
            $scope.alreadyAuth = true;
            $scope.Token = storageToken;
            $scope.loginFailText = '';
            $scope.userid = storageUserId;
        }
        else {
            $scope.$storage.thumphoto = null;
        }

        $scope.ThumbsPathHome();

    };

    $scope.Login = function () {
        showLoading();
        var pass = document.querySelector("#Pass").value
        var username = document.querySelector("#User").value
        var validateUser = RequestServices.getValidateUser(username);

        if (validateUser != null) {
            validateUser = JSON.parse(validateUser)
            $scope.userid = parseInt(validateUser[0]);
            var loginResult = RequestServices.getLoginResult(username, pass);


            if (loginResult.token != undefined) {
                $scope.loginFail = false;
                $scope.alreadyAuth = true;
                $scope.Token = loginResult.token;
                $scope.loginFailText = '';

                $scope.$storage.token = loginResult.token;
                $scope.$storage.username = username;
                $scope.$storage.userid = $scope.userid;

            }
            else {
                $scope.loginFail = true;
                if (loginResult != undefined && loginResult != null && loginResult.responseText != undefined && loginResult.responseText != null && loginResult.responseText.indexOf('not available') > 0) {
                    $scope.loginFailText = 'El sistema no esta disponible por el momento, intente mas tarde';
                }
                else
                    $scope.loginFailText = 'La clave ingresada es incorrecta';
            }
        } else {
            $scope.loginFail = true;
            $scope.loginFailText = 'El usuario ingresado es incorrecto';
        }

        
        
        hideLoading();
    };

    $scope.ThumbsPathHome = function () {
        var valores = window.location.search;
        var urlParams = new URLSearchParams(valores);
        var userid = urlParams.get('u');

        try {
            if ($scope.$storage.thumphoto != null)
                $scope.thumphoto = $scope.$storage.thumphoto;
            else {
                var response = RequestServices.LoadUserPhotoFromDB(userid);
                $scope.thumphoto = response;
                $scope.$storage.thumphoto = response;
            }

        } catch (e) {
            console.error(e);
        }
    };

    $scope.getUrlParameters = function () {
        var pairs = window.location.search.substring(1).split(/[&?]/);
        var res = {}, i, pair;
        for (i = 0; i < pairs.length; i++) {
            pair = pairs[i].toLowerCase().split('=');
            if (pair[1])
                res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
        }
        return res;
    }

    $scope.setUrlParameters = function (urlParams) {
        $scope.accionid = null;
    }

    $scope.getReportCounts = function () {

        let repotsId = [11535116, 11535117, 11535126];
        $scope.resumenGerencial;
        $scope.facturasYpagos;
        $scope.facturasPendientes;


        repotsId.forEach((element) => {
            var d = RequestServices.getResults($scope.userid, element);
            let invoicesList = JSON.parse(d);
            if (element == 11535126)
                $scope.resumenGerencial = invoicesList.length;
            if (element == 11535116)
                $scope.facturasYpagos = invoicesList.length;
            if (element == 11535117)
                $scope.facturasPendientes = invoicesList.length;
        })

    }
       
    

    $scope.setUrlRyRt = function (r, rt) {

        $scope.showPendingTab = true;
        $scope.selectedInvoices = [];
        $scope.selectedInvoicesLate = [];
        $scope.ListFacturas = [];
        $scope.seeLaterList = [];

        if (r != undefined)
            $scope.accionid = r;
        if (rt != undefined)
            $scope.actiontypeid = rt;

        if ($scope.actiontypeid == "0") {
            $scope.reportId = 11535116;
            $scope.AprobarId = 1223308;
            $scope.AprobarTodosId = 11547097;
            $scope.RechazarId = 11546880;
            $scope.AprobarTodosForMailId = 11546636;
            $scope.currentView = "Pendientes";
        }
        else if ($scope.actiontypeid == "1") {
            $scope.reportId = 11535117;
            $scope.AprobarId = 1021731;
            $scope.AprobarTodosId = 11547091;
            $scope.RechazarId = 11546880;
            $scope.AprobarTodosForMailId = 11546640;
            $scope.currentView = "Conformacion"
        }
        else if ($scope.actiontypeid == "2") {
            $scope.reportId = 11535126;
            $scope.Comentar_ResumenGerencialId = 11547418;
            $scope.Aprobar_ResumenGerencialId = 11547820;
            $scope.Devolver_ResumenGerencialId = 11547815;
            $scope.Devolver_RG_Anterior = 11535147;
            $scope.currentView = "ResumenGerencial";
        }
        console.log($scope.currentView);
    }

    $scope.btnRefresh = function () {
        $scope.LoadResults();
    }

    $scope.RevisarFactura = function () {
        $scope.accionid = "0";
    }

    $scope.GetFacturaArchive = function (docTypeId, docId, iframeID) {

        var userId = parseInt($scope.userid);
        //var userId = 3;
        var tokenSearchId = $scope.Token;

        DocumentViewerServices.getDocumentServiceAsync(userId, docTypeId, docId, tokenSearchId, true, true, iframeID).then(function (result) {
            //            result = JsonValidator(result);
            $scope.LoadDocument(result);
        });
    }

    $scope.LoadDocument = function (RDO) {
        if (RDO != undefined) {
            var JsonResult = JSON.parse(RDO);
            if (!esIE() && !JsonResult.fileName.endsWith(".pdf")) {
                if (JsonResult.fileName.endsWith(".html") == true) {
                    JsonResult.ContentType = "text/html";
                }
                var a = "data:" + JsonResult.ContentType + ";base64," + JsonResult.data;// "data:application/pdf;base64," + JsonResult.data;
                // $("#"+JsonResult.iframeID).attr("src", a);
                $("iframe[id=" + JsonResult.iframeID + "]").attr("src", a);
                // switchToDocumentViewer("PDF");
            }
            else {
                // switchToDocumentViewer("PDFForIE");
                var pdfAsDataUri = JsonResult.data;
                var pdfAsArray = convertDataURIToBinary(pdfAsDataUri);
                var url = '../../../NGTemplates/PDFViewer/viewer.html?file=';

                var binaryData = [];
                binaryData.push(pdfAsArray);
                var dataPdf = window.URL.createObjectURL(new Blob(binaryData, { type: JsonResult.ContentType }))

                document.querySelector('iframe[id="' + JsonResult.iframeID + '"]').setAttribute('src', url + encodeURIComponent(dataPdf));

                //para var el pdf en una pestaña nueva
                $scope.iframeControls.push(JsonResult.iframeID + " - " + JsonResult.data);

            }
        } else {
            $("iframe[id=" + JsonResult.iframeID + "]").css("display", "none");
            $("img[id=" + JsonResult.iframeID + "]").css("display", "block");
            $("img[id=" + JsonResult.iframeID + "]").attr("src", "../Utilities/images/404.jpg");
            $("img[id=" + JsonResult.iframeID + "]").css("margin-left", "20%");
            $("img[id=" + JsonResult.iframeID + "]").css("width", "54%");


            // console.log("No hay un archivo para mostrar.");
            // swal({
            //     title: "Archivo no disponible!",
            //     text: 'El archivo no esta disponible',
            //     icon: "warning",
            //     timer: 2000
            // });
            //TO DO: Podria haber una pantalla de error para este o cualqueir caso.
        }
    }

    $scope.showPDF = function () {

        $scope.iframeControls.forEach(function (item) {
            //data[0] = id del iframe , data[1] pdf en string base64
            var data = item.split(" - ");
            var buttonNewWindow = $('iframe[id="' + data[0] + '"]').contents().find("#openFileNewWindow");

            buttonNewWindow.css("display", "block");

            buttonNewWindow.click(function () {
                var byteCharacters = atob(data[1]);
                var byteNumbers = new Array(byteCharacters.length);
                for (var i = 0; i < byteCharacters.length; i++) {
                    byteNumbers[i] = byteCharacters.charCodeAt(i);
                }
                var byteArray = new Uint8Array(byteNumbers);
                var file = new Blob([byteArray], { type: 'application/pdf;base64' });
                var fileURL = URL.createObjectURL(file);
                window.open(fileURL);
            });
        });
    };

    $scope.setSelectedSummary = function (item, e) {
        e.stopPropagation();
        $scope.selectedSummary = item;
        $scope.showPreviewSummary = item;
        item.RECUPERO = $scope.ValSwitch(item.RECUPERO);
        
    }

    $scope.setSelectedPreviewSummary = function (item, e) {
        e.stopPropagation();
        item = JSON.parse(item);
        $scope.selectedSummary = item[0];
        item[0].RECUPERO = $scope.ValSwitch(item[0].RECUPERO);

    }

    $scope.goToBack = function (item) {
        $scope.selectedSummary = null;
        $scope.showPreviewSummary = null;
    }

    $scope.OpenReadingMode = function (Text, Title) {
        $scope.MS_Modal_Title = Title;
        $scope.MS_Modal_Body = Text;
    }

    
    $scope.CloseReadingMode = function () {
        $scope.MS_Modal_Title = "";
        $scope.MS_Modal_Body = "";
    }


    var isLoadingShown = false;

    function showLoading() {

        isLoadingShown = true;
        $mdDialog.show({
            template: '<md-dialog style="background-color:transparent;box-shadow:none">' +
                '<div layout="row" layout-sm="column" layout-align="center center" aria-label="wait" style="overflow: hidden">' +
                '<md-progress-circular md-mode="indeterminate" ></md-progress-circular>' +
                '</div>' +
                '</md-dialog>',
            parent: angular.element(document.body),
            clickOutsideToClose: false,
            fullscreen: false,
            escapeToClose: false
        });
    }

    function hideLoading() {
        $mdDialog.cancel();
        isLoadingShown = false;
    }


    function convertDataURIToBinary(base64) {
        var raw = window.atob(base64);
        var rawLength = raw.length;
        var array = new Uint8Array(new ArrayBuffer(rawLength));

        for (var i = 0; i < rawLength; i++) {
            array[i] = raw.charCodeAt(i);
        }
        return array;
    }

    function esIE() {
        return (navigator.userAgent.indexOf('MSIE') !== -1 ||
            navigator.appVersion.indexOf('Trident/') > 0 ||
            navigator.userAgent.toString().indexOf('Edge/') > 0)
    }

    $scope.animateTabRippleEffect = function () {
        const tabButton = document.querySelectorAll('.tab-button');


        tabButton.forEach(btn => {
            btn.addEventListener('click', (e) => {
                let ripple = document.createElement('span');

                const buttonRect = btn.getBoundingClientRect();
                const buttonCenterX = buttonRect.left + (buttonRect.width / 2);
                const buttonCenterY = buttonRect.top + (buttonRect.height / 2);

                ripple.style.left = buttonCenterX + 'px';
                ripple.style.top = buttonCenterY + 'px';
                ripple.style.position = 'absolute';
                btn.appendChild(ripple);

                setTimeout(() => {
                    ripple.remove();
                }, 500)
            })
        });
    }

    $scope.addSwipeEventListener = function () {
        try {
            var elem = document.getElementById('z-main-panel');
            var start;

            elem.addEventListener('touchstart', function (event) {
                start = event.changedTouches[0].clientX;
            }, false);

            elem.addEventListener('touchmove', function (event) {
                event.preventDefault(); // Para que no se desplace la pantalla
            }, false);

            // Detectar el final del toque
            elem.addEventListener('touchend', function (event) {
                var end = event.changedTouches[0].clientX;
                var distance = end - start;

                //izquierda
                if (distance < 0) {
                    console.log("distancia recorrida del dedo:" + distance);
                     //NC:cambiando este valor se puede ajustar la sensibilidad
                    if (distance < -80) {
                        $scope.showPendingTab = false;
                        $scope.tabButtonOnClick("SeeLater");
                    }
                }
                //derecha
                if (distance > 0) {
                    //NC:cambiando este valor se puede ajustar la sensibilidad
                    if (distance > 80) {
                        console.log("distancia recorrida del dedo:" + distance);
                        $scope.showPendingTab = true;
                        $scope.tabButtonOnClick("Pending");
                    }

                }
            }, false);
        } catch (e) {
            console.error("Error al ejecutar 'addSwipeEventListener'");
        }

    }

    $scope.tabButtonOnClick = function (buttonName) {
        $scope.showPendingTab = buttonName == "Pending";
        console.log(buttonName + " was clicked");

        if ($scope.showPendingTab) {
            console.log("Objeto Lista Pendientes:");
            $scope.refreshIframe($scope.ListFacturas);
        } else {
            console.log("Objeto Lista Ver Despues:");
            $scope.refreshIframe($scope.seeLaterList);
        }
        try {
            $scope.$apply();
        } catch (e) {

        }
        
    }

    String.prototype.replaceAll = function (search, replacement) {
        return this.split(search).join(replacement);
    };
});




app.directive('zambaRequest', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {

            $scope.LoadResults();
            $scope.addSwipeEventListener();
            $scope.animateTabRippleEffect();
            setTimeout(function () {
                $scope.showPDF();
            }, 20000);
        },
        templateUrl: $sce.getTrustedResourceUrl('RequestActionView.html?v=248'),

    }
});



app.directive('zambaRequestAction', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {

            $scope.AprobarTodosForMail($scope.loteid);
        },
        templateUrl: $sce.getTrustedResourceUrl('RequestActionFromMail.html?v=248'),

    }
});

app.directive('managerialSummary', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.LoadResultsForManagerialSummary();
        },
        templateUrl: $sce.getTrustedResourceUrl('ManagerialSummary.html'),

    }
});

app.directive('zambaDefault', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.LoadResultsForManagerialSummary();
            $scope.getReportCounts();
        },
        templateUrl: $sce.getTrustedResourceUrl('zambaDefault.html'),

    }
});
