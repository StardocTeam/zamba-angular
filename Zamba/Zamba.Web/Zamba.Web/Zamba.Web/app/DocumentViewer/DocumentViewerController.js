app.controller('DocumentViewerController', function ($scope, $filter, $http, DocumentViewerServices, ZambaUserService) {

    $scope.onlyMsg = false;

    $scope.pageindex = 1;
    $scope.maxindex;
    $scope.pdfAsArray; 

    //Metodo invocable que obtiene el archivo asociado a la tarea.
    $scope.ShowViewer = function () {
        var valueParams = {
            Params: {
                "id": $("#SearchID").val(),
                "externuserid": $("#UserIDs").val()
            }
        }
        var tokenSearchId = $("#TokenSearchIDs").val();

        var RDO = DocumentViewerServices.getDocFileService(valueParams, tokenSearchId);

        if (RDO != undefined) {
            if (isParseableToJson(RDO)) {
                var JsonResult = JSON.parse(RDO);

                if (JsonResult != undefined) {
                    $scope.subject = JsonResult.subject;
                    $scope.from = JsonResult.from;
                    $scope.to = JsonResult.to;
                    $scope.date = JsonResult.date;
                    $scope.body = JsonResult.body;
                    $scope.isMsg = JsonResult.isMsg;
                    $scope.attachs = JsonResult.attachs;

                    switchToDocumentViewer("MSG");
                    console.log("Visualización de formato MSG exitosa.");
                    try {
                        $scope.$applyAsync();

                    } catch (e) {
                        console.error(e);
                    }
                } else {
                    console.log("Archivo no disponible!");
                    swal({
                        title: "Archivo no disponible!",
                        text: 'El archivo no esta disponible',
                        icon: "warning",
                        timer: 2000
                    });
                    //switchToDocumentViewer(false);   //Podria haber una pantalla de error para este o cualqueir caso.
                }
            } else if (!(navigator.userAgent.indexOf('MSIE') !== -1 ||
                navigator.appVersion.indexOf('Trident/') > 0 ||
                navigator.userAgent.toString().indexOf('Edge/') > 0)) {
                if (JsonResult.fileName.indexOf(".html") != -1) {
                    JsonResult.ContentType = "text/html";
                }
                var a = "data:" + JsonResult.ContentType + ";base64," + JsonResult.data;// "data:application/pdf;base64," + JsonResult.data;
                $("#iframeID").attr("src", a);

                switchToDocumentViewer("PDF");
            } else {
                switchToDocumentViewer("PDFForIE");

                var pdfAsDataUri = JsonResult.data;
                var pdfAsArray = convertDataURIToBinary(pdfAsDataUri);
                var url = '../../NGTemplates/PDFViewer/viewer.html?file=';

                var binaryData = [];
                binaryData.push(pdfAsArray);
                var dataPdf = window.URL.createObjectURL(new Blob(binaryData, { type: JsonResult.ContentType }))

                document.getElementById('previewDocIframe').setAttribute('src', url + encodeURIComponent(dataPdf));


            }
        } else {
            console.log("No hay un archivo para mostrar.");
            swal({
                title: "Archivo no disponible!",
                text: 'El archivo no esta disponible',
                icon: "warning",
                timer: 2000
            });
        }

        $scope.$applyAsync();

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

    //Cuando el componente termina de cargar, obtiene el archivo asociado a la tarea, .
    $scope.ShowDocument = function (url, ICON_ID) {
        try {
            var UrlParams = getUrlParameters(url);

            if (UrlParams != undefined) {
                if (UrlParams.user != undefined) {
                    $scope.userid = UrlParams.user;
                } else if (UrlParams.userid != undefined) {
                    $scope.userid = UrlParams.userid;
                } else if (UrlParams.u != undefined) {
                    $scope.userid = UrlParams.u;
                }

                $scope.docid = UrlParams.docid;

                if ($scope.entityId != undefined) {
                    $scope.doctypeid = $scope.entityId;
                } else if (UrlParams.doctypeid != undefined) {
                    $scope.doctypeid = UrlParams.doctypeid;
                } else if (UrlParams.doctype != undefined) {
                    $scope.doctypeid = UrlParams.doctype;
                } else {
                    console.log("[Error]: Fallo al obtener el ID de la entidad (DocTypeId).")
                    return;
                }

                if ($scope.doctypeid != undefined) {

                    var tokenSearchId = $("#TokenSearchIDs").val();

                    $scope.CleanIframe();

                    if (!(navigator.userAgent.indexOf('MSIE') !== -1 ||
                        navigator.appVersion.indexOf('Trident/') > 0 ||
                        navigator.userAgent.toString().indexOf('Edge/') > 0) && ICON_ID == 9) {
                        DocumentViewerServices.getDocumentService($scope.userid, $scope.doctypeid, $scope.docid, tokenSearchId, false, $scope.LoadDocument, true);

                    } else {

                        DocumentViewerServices.getDocumentService($scope.userid, $scope.doctypeid, $scope.docid, tokenSearchId, true, $scope.LoadDocument, true);
                    }
                }
            } else {
                console.error("Ocurrio un error al cargar el archivo.");
                switchToDocumentViewer("Error");
            }
        } catch (e) {
            console.error(e);
            switchToDocumentViewer("Error");
        }
    }

    $scope.ShowDocument_FromItem = function (userId, docTypeId, docId, pathFile = "") {
        if (userId != undefined && docTypeId != undefined && docId != undefined) {
            $scope.userid = userId;
            $scope.doctypeid = docTypeId;
            $scope.docid = docId;
            
            $scope.CleanIframe();
            if (!(navigator.userAgent.indexOf('MSIE') !== -1 ||
                navigator.appVersion.indexOf('Trident/') > 0 ||
                navigator.userAgent.toString().indexOf('Edge/') > 0)) {
                var tokenSearchId = "";
                DocumentViewerServices.getDocumentService($scope.userid, $scope.doctypeid, $scope.docid, tokenSearchId, false, $scope.LoadDocument, true, true, pathFile);

            } else {

                DocumentViewerServices.getDocumentService($scope.userid, $scope.doctypeid, $scope.docid, tokenSearchId, true, $scope.LoadDocument, true, true, pathFile);
            }
        }        
    }

    $scope.CleanVars = function () {
        $scope.userid = 0;
        $scope.doctypeid = 0;
        $scope.docid = 0;
    }

    $scope.CleanIframe = function () {
        $("#iframeID").attr("src", "about:blank");
        document.getElementById('previewDocIframe').setAttribute('src', 'about:blank');
    }

    $scope.LoadDocument = function (RDO) {
        
        try {
            if (RDO != undefined) {
                var JsonResult = JSON.parse(RDO);
                if (JsonResult.dataObject != null) {

                    $scope.subject = JsonResult.dataObject.subject;
                    $scope.from = JsonResult.dataObject.from;
                    $scope.to = JsonResult.dataObject.to;
                    $scope.date = JsonResult.dataObject.date;
                    $scope.body = JsonResult.dataObject.body;
                    $("#txtAreaBody")[0].srcdoc = $scope.body;
                    $scope.isMsg = JsonResult.dataObject.isMsg;
                    $scope.attachs = JsonResult.dataObject.attachs;

                    switchToDocumentViewer("MSG");
                    console.log("Visualización de formato MSG exitosa.");
                    try {
                        $scope.$applyAsync();

                    } catch (e) {
                        console.error(e);
                    }
                }
                else if (!esIE() && !JsonResult.fileName.endsWith(".pdf")) {
                    if (JsonResult.fileName.endsWith(".html") == true) {
                        JsonResult.ContentType = "text/html";
                        var a = "data:" + JsonResult.ContentType + ";base64," + JsonResult.data;// "data:application/pdf;base64," + JsonResult.data;
                        $("#iframeID").attr("src", a);
                        switchToDocumentViewer("PDF");
                    } else {
                        
                        if (JsonResult.fileName.endsWith(".mp4") == true) {
                            var a = JsonResult.data;// "data:application/pdf;base64," + JsonResult.data;
                            $("#IframeVideoSrc").attr("type", "video/mp4"); 
                            $("#IframeVideoSrc").attr("src", "data:video/mp4;base64,"+a);
                            switchToDocumentViewer("video");
                        } else {
                            var Controller = angular.element(document.getElementById("DocumentViewerFromSearch")).scope();

                            console.log("Archivo encontrado, pero no es posible mostrarlo.");

                            try {

                                if ($scope.currentModeSearch == undefined || ($scope.currentModeSearch == 'results' && ZambaUserService.VisualizerMode == 'preview')) {
                                    swal({
                                        text: 'No es posible mostrar el archivo. Desea descargarlo?',
                                        icon: "warning",
                                        buttons: true,
                                        dangerMode: true,
                                    })
                                        .then((Download) => {
                                            if (Download) {
                                                try {
                                                    Controller.DownloadFile($scope.userid, $scope.doctypeid, $scope.docid);
                                                } catch (e) {
                                                    DownloadFile();
                                                }
                                            }
                                            $scope.CleanVars();
                                        });
                                }
                            } catch (e) {
                                console.error(e);
                            }
                            switchToDocumentViewer("Error");
                        }
                    }
                }
                else {
                    switchToDocumentViewer("PDFForIE");
                    var pdfAsDataUri = JsonResult.data;
                    var pdfAsArray = convertDataURIToBinary(pdfAsDataUri);
                    ////$scope.pdfAsArray = pdfAsDataUri; 
                    var url = '../../NGTemplates/PDFViewer/viewer.html?file=';
                    $scope.showZeditorview(JsonResult.fileName);
                    var binaryData = [];
                    binaryData.push(pdfAsArray);
                    var dataPdf = window.URL.createObjectURL(new Blob(binaryData, { type: JsonResult.ContentType }))

                    document.getElementById('previewDocIframe').setAttribute('src', url + encodeURIComponent(dataPdf));

                    //document.getElementById('previewDocIframe').style.display = "none";
                    //$scope.canvasPDF($scope.pdfAsArray, $scope.pageindex);
                }
            } else {

                let currentUrl = window.location.href;
                if (currentUrl.includes("TaskViewer") && currentUrl.includes("docviewer")) {

                    console.log("No hay un archivo para mostrar.");
                    //swal({
                    //    title: "Archivo no disponible!",
                    //    text: 'El archivo no esta disponible',
                    //    icon: "warning",
                    //    timer: 2000
                    //});
                }

                
                switchToDocumentViewer("Error");
                

                
            }
        } catch (e) {
            console.error(e + "Lanzado por: $scope.LoadDocument(" + RDO + ")");
            switchToDocumentViewer("Error");
        }
    }
 

    $scope.canvasPDF = function (pdfAsArray,pagNumber) {
        
        document.getElementById('prev').addEventListener('click', onPrevPage);  
        document.getElementById('next').addEventListener('click', onNextPage);  

        var pdfData = atob(pdfAsArray);

        // Loaded via <script> tag, create shortcut to access PDF.js exports.
        var pdfjsLib = window['pdfjs-dist/build/pdf'];

        // The workerSrc property shall be specified.
        pdfjsLib.GlobalWorkerOptions.workerSrc = '../../Scripts/pdf.worker.js';

        // Using DocumentInitParameters object to load binary data.
        var loadingTask = pdfjsLib.getDocument({ data: pdfData });
        loadingTask.promise.then(function (pdf) {
            console.log('PDF loaded');

            // Fetch the first page
            var pageNumber = pagNumber;
            pdf.getPage(pageNumber).then(function (page) {
                console.log('Page loaded');

                var scale = 2;
               var viewport = page.getViewport({ scale: scale });

                // Prepare canvas using PDF page dimensions
                var canvas = document.getElementById('the-canvas');

                var style = canvas.style;
                style.marginLeft = "auto";
                style.marginRight = "auto";
                var parentStyle = canvas.parentElement.style;
                parentStyle.textAlign = "center";
                //parentStyle.width = "100%";

                var context = canvas.getContext('2d');
                canvas.height = viewport.height;
                canvas.width = viewport.width;

                document.getElementById('page_num').textContent = pagNumber;
                document.getElementById('page_count').textContent = pdf._pdfInfo.numPages;
                /*$scope.pageindex = pdf._pdfInfo.numPages;*/

                
                // Render PDF page into canvas context
                var renderContext = {
                    canvasContext: context,
                    viewport: viewport
                };
                var renderTask = page.render(renderContext);
                renderTask.promise.then(function () {
                    console.log('Page rendered');
                });
            });
        }, function (reason) {
            // PDF loading error
            console.error(reason);
        });
    }

    function onPrevPage() {
        
        if ($scope.pageindex <= 1) {
            return;
        }
        $scope.canvasPDF($scope.pdfAsArray,--$scope.pageindex);

    }

    function onNextPage() {
        if ($scope.pageindex >= $scope.maxindex) {
            return;
        }

        $scope.canvasPDF($scope.pdfAsArray,++$scope.pageindex);
    }

    $scope.showZeditorview = function (e) {

        $scope.showEditor = false;
        if (e.includes(".docx")) {
            $scope.showEditor = true;
            $scope.$applyAsync();
        }

        $scope.showBtnEditor = false;
        ////Creamos la instancia
        let urlParams = new URLSearchParams(location.search);

        ////Accedemos a los valores
        let Ed = urlParams.get('Ed');
        if (Ed != null)
            $scope.showBtnEditor = true;
    }

    $scope.ShowDocument_FromItem = function (userId, docTypeId, docid) {
        var result = true
        var success = DocumentViewerServices.getDocumentService(userId, docTypeId, docid, '', true, $scope.LoadDocument, true);
        if (!success) {
            result = false;

        }
        return result;
    }

    $scope.DownloadFile = function (userId, docTypeId, docid) {
        DocumentViewerServices.getDocumentService(userId, docTypeId, docid, '', false, $scope.DownloadResult, false);
    }

    $scope.DownloadResult = function (RDO) {
        if (RDO != undefined) {
            var JsonResult = JSON.parse(RDO);
            if (JsonResult.dataObject != null) {



                var blobdata = new Blob([JsonResult.data], { type: JsonResult.ContentType });
                var link = document.createElement("a");
                link.setAttribute("href", window.URL.createObjectURL(blobdata));
                link.setAttribute("download", JsonResult.fileName);
                document.body.appendChild(link);
                link.click();


            } else if ((navigator.userAgent.indexOf('MSIE') !== -1 ||
                navigator.appVersion.indexOf('Trident/') > 0 ||
                navigator.userAgent.toString().indexOf('Edge/') > 0)) {

                var pdfAsDataUri = JsonResult.data;
                var pdfAsArray = convertDataURIToBinary(pdfAsDataUri);

                var binaryData = [];
                binaryData.push(pdfAsArray);
                var dataBlob = new Blob(binaryData, { type: JsonResult.ContentType });

                window.navigator.msSaveOrOpenBlob(dataBlob, JsonResult.fileName);

            } else {



                var pdfAsDataUri = JsonResult.data;
                var pdfAsArray = convertDataURIToBinary(pdfAsDataUri);

                var binaryData = [];
                binaryData.push(pdfAsArray);
                var dataBlob = new Blob(binaryData, { type: JsonResult.ContentType });

                if (window.navigator.msSaveOrOpenBlob) {
                    window.navigator.msSaveBlob(dataBlob, filename);
                }
                else {
                    var elem = window.document.createElement('a');
                    elem.href = window.URL.createObjectURL(dataBlob);
                    elem.download = JsonResult.fileName;
                    document.body.appendChild(elem);
                    elem.click();
                    document.body.removeChild(elem);
                    window.URL.revokeObjectURL(elem.href);
                }

            }
        } else {
            swal('', 'No existe el archivo para descargar', 'warning');
            console.log("No hay un archivo para descargar.");
        }
    };

    //Devuelve verdadero si el item no es el ultimo del array.
    $scope.commaSeparator = function (ArrayVar, Item) {
        if (ArrayVar.indexOf(Item) < ArrayVar.length - 1)
            return true;
        else
            return false;
    }

    //Devuelve una representacion del tamaño del archivo en el formato deseado.
    $scope.fileSize_Format = function (Size, Format) {
        var ret = Size;

        try {
            switch (Format.toLowerCase()) {
                case "kb":
                    divider = 1024;
                    ret /= divider;
                    break
                case "mb":
                    divider = Math.pow(1024, 2);
                    ret /= divider;
                    break
            }
        } catch (e) {
            console.error(e + "Lanzado por: $scope.fileSize_Format(" + Size + ", " + Format + ")");
        }

        return ret.toFixed(2) + " " + Format.toLowerCase().replace(/\b\w/g, function (l) { return l.toUpperCase() });
    }

    //Realiza la descarga del archivo del MSG visualizado.
    $scope.downloadfile = function (data) {
        var elemento = document.getElementById(data.Id);
        var dataBase64 = 'data:application/octet-stream;base64,' + data.Data;

        elemento.href = dataBase64;
        elemento.click();
    }

    $scope.ResizeIframe = function() {
        var PaddingTopHeader = 5;

        var TotalHeight = window.innerHeight;
        var Toolbar = parseInt(document.getElementById("Toolbar").style.height);
        var EspacioAsignado = TotalHeight - (Toolbar + PaddingTopHeader) - 25;

        $("#IFPreview").height(EspacioAsignado);
    }

    function esIE() {
        return (navigator.userAgent.indexOf('MSIE') !== -1 ||
            navigator.appVersion.indexOf('Trident/') > 0 ||
            navigator.userAgent.toString().indexOf('Edge/') > 0)
    }

    //Devuelve verdadero si se puede parsear a JSON, caso contrario devuelve falso.
    function isParseableToJson(str) {
        try {
            if (JSON.parse(str)) {
                return true;
            }
        } catch (e) {
            console.error(e);
            return false;
        }
    }

    //Cambia la visualizacion entre DocumentViewer y Iframe para mostrar un MSG o por otro lado cualquier archivo diferente a MSG.
    function switchToDocumentViewer(value) {
        switch (value) {
            case "MSG":
                $("#MSG").css("display", "block");
                $("#PDF").css("display", "none");
                $("#PDFForIE").css("display", "none");
                $("#IframeVideoSrc").css("display", "none");
                $("#Error404").css("display", "none");
                break

            case "PDF":
                $("#MSG").css("display", "none");
                $("#PDF").css("display", "block");
                $("#PDFForIE").css("display", "none");
                $("#IframeVideoSrc").css("display", "none");
                $("#Error404").css("display", "none");
                break

            case "PDFForIE":
                $("#MSG").css("display", "none");
                $("#PDF").css("display", "none");
                $("#PDFForIE").css("display", "block");
                $("#IframeVideoSrc").css("display", "none");
                $("#Error404").css("display", "none");
                break

            case "Error":
                $("#MSG").css("display", "none");
                $("#PDF").css("display", "none");
                $("#PDFForIE").css("display", "none");
                $("#IframeVideoSrc").css("display", "none");
                $("#Error404").css("display", "block");
                break

            case "ErrorPreview":
                $("#MSG").css("display", "none");
                $("#PDF").css("display", "none");
                $("#PDFForIE").css("display", "none");
                $("#IframeVideoSrc").css("display", "none");
                $("#ErrorPreview").css("display", "block");
                break
            case "video":
                $("#MSG").css("display", "none");
                $("#PDF").css("display", "none");
                $("#PDFForIE").css("display", "none");
                $("#ErrorPreview").css("display", "none");
                $("#IframeVideoSrc").css("display", "block");
                break

            default:
        }
    }
    //Obtiene valores obtenidos de la URL.
    function getUrlParameters(url) {
        if (url != undefined) {
            var pairs = url.split("?")[1].split("&");
            var res = {}, i, pair;
            for (i = 0; i < pairs.length; i++) {
                pair = pairs[i].toLowerCase().split('=');
                if (pair[1])
                    res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
            }
            return res;
        } else {
            var pairs = window.location.search.substring(1).split(/[&?]/);
            var res = {}, i, pair;
            for (i = 0; i < pairs.length; i++) {
                pair = pairs[i].toLowerCase().split('=');
                if (pair[1])
                    res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
            }
            return res;
        }
    }    
});

app.directive('zambaDocumentViewer', function ($sce) {

    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {
            $scope.preview = attributes.preview;
            $scope.userid = attributes.userid;
            $scope.doctypeid = attributes.doctypeid;
            $scope.docid = attributes.docid;
            $scope.subject = attributes.subject;
            $scope.entityId = attributes.entityId;
            $scope.body = attributes.from;
            $scope.to = attributes.to;
            $scope.date = attributes.date;
            $scope.body = attributes.body;
            $scope.isMsg = attributes.isMsg;
            $scope.attachs = attributes.attachs;
            $scope.src = attributes.src;
            $scope.onlyMsg = attributes.onlyMsg;

            //Si la vista donde se encuentra el componente no es un DocViewer, variable flag que determina el alto del textArea (body). 
            var url = window.location.href;
            if (url.toLowerCase().indexOf("docviewer") != -1) {
                $scope.ResizeIframe();
            } else {
                if (attributes.height) {
                    $scope.height = attributes.height;
                }
            }

            if ($scope.userid != undefined && $scope.doctypeid != undefined && $scope.docid != undefined) {
                setTimeout(function () {
                    $scope.ShowDocument_FromItem($scope.userid, $scope.doctypeid, $scope.docid);
                }, 200);
            }

            if ($("#GridDocumentViewer").length > 0) {
                var ScopeDocumentViewer = angular.element($("#GridDocumentViewer")).scope();
                var FirstData = $("#zamba_grid_index_all").data().kendoGrid._data[0];

                setTimeout(function () {
                    ScopeDocumentViewer.ShowDocument_FromItem(GetUID(), FirstData.DOC_TYPE_ID, FirstData.DOC_ID);
                }, 200);
            } else if (window.location.href.split("&").length > 1) {
                setTimeout(function () {
                    $scope.ShowDocument();
                }, 200);
            }
        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/DocumentViewer/DocumentViewer.html'), //Implementar HTML en la carpeta DocumentViewer.
    }
});