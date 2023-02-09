'use strict';
var app = angular.module('app', ['ngAnimate', 'ngStorage', 'ngMaterial']);

app.run(['$http', '$q', '$rootScope', function ($http, $q, $rootScope) {
}]);

//var ZambaWebRestApiURL = "https://bpm.provinciaseguros.com.ar/zambadesa.restapi/api";
//var ZambaUrl = "https://bpm.provinciaseguros.com.ar/zamba.desa";

var ZambaWebRestApiURL = window.location.origin +  window.location.pathname.substring(0,window.location.pathname.indexOf('/',2)).replace('.','') + ".restapi/api";
var ZambaUrl = window.location.origin + window.location.pathname.substring(0, window.location.pathname.indexOf('/', 2));



//Config for Cross Domain
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]);


app.controller('RequestController', function ($scope, $filter, $http, RequestServices, DocumentViewerServices, $localStorage, $sessionStorage, $mdDialog) {

    $scope.$storage = $localStorage;

    $scope.Titulo = "Procesando";
    $scope.Texto = "La operacion se esta llevando a cabo, por favor espere...";
    $scope.alreadyAuth = false;
    $scope.username = "";
    $scope.pass = "";
    $scope.userid = "";
    $scope.loteid = "";
    $scope.accionid = "0";
    $scope.acciontypeid = "0";
    $scope.loginFail = false;
    $scope.ListFacturas = [];
    $scope.iframeControls = [];

    $scope.loginFailText = '';
    $scope.BtnsAccionesError = false;
    $scope.BtnsAcciones = false;

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

            $scope.ListFacturas = JSON.parse(d);


            setTimeout(function () {
                $scope.iframeControls = [];
                $scope.ListFacturas.forEach(function (element) {

                    console.log(element.ID);
                    //$scope.iframeID = element.ID;
                    $scope.GetFacturaArchive(element.EID, element.ID1, element.ID);

                    
                    $("a[id=" + element.ID + "]").attr("href", ZambaUrl + "/views/WF/TaskViewer?DocType=" + element.EID + "&docid=" + element.ID1 + "&taskid=" + element.TID + "&mode=s&s=13&user=" + $scope.userid + "#Zamba/");

                });



            }, 2000);


        }
    };

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


    $scope.Rechazar = function (docId, idElemento) {

        var a = false
        Swal.fire({
            title: 'Motivo del rechazo ? ',
            input: 'textarea',
            cancelButtonText: 'cerrar',
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


    function ResponseNotification(result, idElemento, docId) {
        if (result.executionResult != 1 && result.executionResult != 3) {

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

        }else if (NewPassword.length <= 5 || NewPassword2.length <= 5) {
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

        }else if (contarCaracteres(NewPassword) < 2) {
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

    


    $scope.GetUsername = function () {

        var urlParams = $scope.getUrlParameters();

        $scope.setUrlParameters(urlParams);

        var username = RequestServices.getUsername($scope.userid);
        if (username != undefined && username != "") {
            $scope.username = username;
            $("#Pass").focus();
        }

        var storageToken = $scope.$storage.token;
        var storageUserName = $scope.$storage.username;

        if (username == storageUserName && storageToken != null) {
            //mismo usuario
            $scope.loginFail = false;
            $scope.alreadyAuth = true;
            $scope.Token = storageToken;
            $scope.loginFailText = '';
        }
        else {
            $scope.$storage.thumphoto = null;
        }

        $scope.ThumbsPathHome();

    };

    $scope.Login = function () {
        showLoading();
        var pass = $("#Pass").val();
        var username = $scope.username;

        var loginResult = RequestServices.getLoginResult(username, pass);

        if (loginResult.token != undefined) {
            $scope.loginFail = false;
            $scope.alreadyAuth = true;
            $scope.Token = loginResult.token;
            $scope.loginFailText = '';

            $scope.$storage.token = loginResult.token;
            $scope.$storage.username = username;

        }
        else {
            $scope.loginFail = true;
            if (loginResult != undefined && loginResult != null && loginResult.responseText != undefined && loginResult.responseText != null && loginResult.responseText.indexOf('not available') > 0) {
                $scope.loginFailText = 'El sistema no esta disponible por el momento, intente mas tarde';
            }
            else
                $scope.loginFailText = 'La clave ingresada es incorrecta';
        }
        hideLoading();
    };

    $scope.ThumbsPathHome = function () {
        var userid = $scope.userid;

        try {
            if ($scope.$storage.thumphoto != null)
                $scope.thumphoto = $scope.$storage.thumphoto;
            else {
                var response = RequestServices.LoadUserPhotoFromDB(userid);
                $scope.thumphoto = response;
                $scope.$storage.thumphoto = response;
            }

        } catch (e) {
            console.log(e);
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
        if (urlParams.u != undefined)
            $scope.userid = urlParams.u
        if (urlParams.id != undefined)
            $scope.loteid = urlParams.id;
        if (urlParams.r != undefined)
            $scope.accionid = urlParams.r;
        if (urlParams.rt != undefined)
            $scope.actiontypeid = urlParams.rt;

        if ($scope.actiontypeid == "0") {
            $scope.reportId = 11535116;
            $scope.AprobarId = 1223308;
            $scope.AprobarTodosId = 11547097;
            $scope.RechazarId = 11546880;
            $scope.AprobarTodosForMailId = 11546636;
        }
        else {
            $scope.reportId = 11535117;
            $scope.AprobarId = 1021731;
            $scope.AprobarTodosId = 11547091;
            
            $scope.RechazarId = 11546880;
            $scope.AprobarTodosForMailId = 11546640;
        }            
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
                $scope.iframeControls.push(JsonResult.iframeID + " - "+ JsonResult.data);
                
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

    $scope.showPDF = function (){
        
        $scope.iframeControls.forEach(function(item){
            //data[0] = id del iframe , data[1] pdf en string base64
            var data = item.split(" - ");
            var buttonNewWindow = $('iframe[id="' + data[0] + '"]').contents().find("#openFileNewWindow");
            
            buttonNewWindow.css("display","block");

            buttonNewWindow.click(function(){
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
});




app.directive('zambaRequest', function ($sce) {
    return {
        restrict: 'E',
        scope: false,
        replace: true,
        transclude: true,
        link: function ($scope, element, attributes) {
          
            $scope.LoadResults();
            setTimeout(function(){
                $scope.showPDF();
            }, 20000);
        },
        templateUrl: $sce.getTrustedResourceUrl('RequestActionView.html?v=168'),

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
        templateUrl: $sce.getTrustedResourceUrl('RequestActionFromMail.html?v=168'),

    }
});


