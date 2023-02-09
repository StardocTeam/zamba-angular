//var appblank = angular.module('appblank', [])
app.directive('zambaDrop', function ($sce) {
    return {
        restrict: 'E',

        link: function (scope, element, attributes) {
            angular.element(element.find("#dZFUpload"))
                .attr({ "elemId": attributes.id, "id": "dZFUpload" + attributes.id });
            maxFiles: 1;
            scope.SetDZ(attributes.id);
        },
        //replace: true,
        templateUrl: $sce.getTrustedResourceUrl('../../DropFiles/dropfiletmp.html')
        // template: '<div class="UserControlBody"><form action="../../Services/FileUpload.ashx" style="position:absolute" class="dropzone margenInput" id="dZFUpload">      <div class="fallback">            <div class="dz-default dz-message center-div-element" />       <input type="file" name="file" />        </div>    </form></div>'
    };
})
app.controller('appController', ['$http', '$scope', function ($http, $scope, FieldsService) {

    $scope.model = '=ngModel';
    $scope.parameters = '=';

    $scope.SetDZ = function (id) {
        var Array_docIds = [];
        var entityId = $("#dZFUpload" + id).parents("zamba-drop").attr("EntityId");
        var indexIds = $("#dZFUpload" + id).parents("zamba-drop").attr("IndexIds");
        var ruleId = $("#dZFUpload" + id).parents("zamba-drop").attr("ruleId");
        var docId = $("#dZFUpload" + id).parents("zamba-drop").attr("docid");
        var elemId = $("#dZFUpload" + id).attr("elemId");
        var FilesCount = 1;
        if (entityId > 0) FilesCount = 99;

        $("#dZFUpload" + id).dropzone({
            maxFiles: FilesCount,
            acceptedFiles: "application/*,audio/*,image/*,video/*,.psd,.pdf,.docx,.doc,.xlsx,.xls,.ppt,.pptx,.txt,.pps,.ppsx,.zip,.rar,.7zip,.msg,.eml",

            addRemoveLinks: false,

            init: function () {
                this.on("maxfilesexceeded", function (file) {
                    this.removeFile(file);
                });
            },

            success: function (file, responseText) {
                var result = JSON.parse(responseText);
                if ((result.errorMessage != undefined && result.errorMessage != '') || (result["0"].insertResults != undefined && result["0"].insertResults == 'ErrorInsertar')) {
                    if (result.errorMessage == undefined) {
                        result.errorMessage = '';
                    }

                    window.swal({
                        title: "Error al subir archivo!",
                        type: "warning",
                        text: result.errorMessage,
                        showConfirmButton: false,
                        timer: 2000
                    });

                    this.removeFile(file);
                } else {

                    window.swal({
                        title: "Archivo subido!",
                        type: "success",
                        showConfirmButton: false,
                        timer: 1000
                    });

                    //Mifuncion(Array_docIds, responseText);

                    if (sessionStorage.getItem('Lista_Archivos') == undefined ||
                        sessionStorage.getItem('Lista_Archivos') == null ||
                        sessionStorage.getItem('Lista_Archivos') == "") {
                        Array_docIds = [];
                    }

                    Array_docIds.push(JSON.parse(responseText)[0].DocIds);

                    //Esto es para que se limpie el sessionStorage y pueda recibir el array que se va cargando por iteracion.
                    if (sessionStorage.key('Lista_Archivos'))
                        sessionStorage.removeItem('Lista_Archivos');

                    sessionStorage.setItem('Lista_Archivos', JSON.stringify(Array_docIds));

                    if (indexIds != undefined && indexIds != "0") {
                        var newDocId = result["0"].DocIds;
                        $scope.FuncionDropzone(indexIds, elemId, newDocId, ruleId, docId);
                        LoadIframe();
                        toastr.success("Archivo ingresado exitosamente");
                        return file.previewElement.classList.add("dz-success");
                    }
                    else {
                        if ($('.dz-complete') != "") {
                            $('.dropzone').css('pointer-events', 'none');
                        } else {
                            $('.dropzone').css('pointer-events', 'auto');
                        }
                        RemoveStyleMultiDropzone();
                        LoadIframe();
                        LoadGrillaForm(elemId);
                        toastr.success("Archivo ingresado exitosamente");
                        localStorage.setItem("newDocIds", result.newDocIds);
                        return file.previewElement.classList.add("dz-success");
                    }

                }
            },
            sending: function (file, xhr, formData) {
                window.swal({
                    title: "Subiendo archivo...",
                    text: "Espere",
                    showCancelButton: false,
                    showConfirmButton: false,
                    buttons: false
                });
                formData.append('isNGform', true);
                formData.append('UploadParentEntityId', entityId);
                formData.append('UploadParentDocId', docId);
                formData.append('UploadNewEntityId', elemId);
                //formData.append('indexIds', indexIds);
                console.log(formData)
            },
            error: function (file, errorMessage) {
                window.swal({
                    title: "Error al subir el archivo!",
                    showConfirmButton: false,
                    timer: 1000
                });
                console.log(errorMessage);
                console.log(errorMessage);
            },
            queuecomplete: function () {
                //  if (errors) console.log("There were errors! dropfiles.js");               
            },

            dictDefaultMessage: "Arrastra y solta aqui los archivos, o haga click y seleccionelo desde una ubicación. * Imagenes, Audio, Video, Office, Zip, ...",
            dictFallbackMessage: "El explorador no soporta drap and drop.",
            dictFallbackText: "Por favor usar el formulario para subir archivos.",
            dictFileTooBig: "El archivo que intenta subir es muy grande ({{filesize}}MiB). Tamaño maximo: {{maxFilesize}}MiB.",
            dictInvalidFileType: "No se pueden subir archivos de este tipo.",
            dictResponseError: "Se produjo el siguiente error en el servidor: {{statusCode}}.",
            dictCancelUpload: "Cancelar",
            dictCancelUploadConfirmation: "Estas seguro de cancelar?",
            dictRemoveFile: "Eliminar",
            dictMaxFilesExceeded: "No se pueden subir mas archivos.",
        });
    }




    $scope.FuncionDropzone = function (indexIds, entityId, newDocIds) {

        localStorage.setItem("NewAssociatedIndexIds", indexIds);
        localStorage.setItem("NewAssociatedEntityId", entityId);
        localStorage.setItem("NewAssociatedDocIds", newDocIds);
        if ($('#listaID')[0].childElementCount == 0) {

            // ajax pedir los datos
            var a = '';
            $.ajax({
                type: "POST",
                url: ZambaWebRestApiURL + '/Tasks/GetAsociatedIndexsDropzon?' + jQuery.param({ indexId: indexIds, entityId: entityId }),
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    a = data;
                    var ElementoJson = JSON.parse(a);
                    // console.log(ElementoJson.length);
                    $("#ModalSearchDropzone").modal();
                    if ($(".modal-backdrop").hasClass("in") == true) {
                        $(".modal-backdrop").css("position", "relative");
                    }

                    for (var i = 0; i < ElementoJson.length; i++) {
                        var name = ElementoJson[i].Value,
                            id = ElementoJson[i].$id,
                            Code = ElementoJson[i].Code;
                        $('#listaID').append('<li class="ModalClassUl" id="' + Code + '" name="' + name.toString() + '"  onclick="AssociatedIndexsModalselectBtn(this);">' + Code + ' - ' + name.toString() + '</li>');
                    }
                }, error: function (error) {
                    console.log(error);
                }

            });

        }
        else {
            $("#ModalSearchDropzone").modal();
            if ($(".modal-backdrop").hasClass("in") == true) {
                $(".modal-backdrop").css("position", "relative");
            }
        }

    }



    $scope.FuncionDropzone = function (indexIds, entityId, newDocIds, ruleId, docId) {
        localStorage.setItem("NewAssociatedIndexIds", indexIds);
        localStorage.setItem("NewAssociatedEntityId", entityId);
        localStorage.setItem("NewAssociatedDocIds", newDocIds);

        localStorage.setItem("NewAssociatedruleId", ruleId);
        localStorage.setItem("NewAssociateddocId", docId);
        var NewAssociatedIds = localStorage.getItem("NewAssociatedEntityId") + localStorage.getItem("NewAssociatedIndexIds");
        if (localStorage.getItem("ItemCache") == null) {
            $scope.dropzoneCallAjax(indexIds, entityId);
        } else if (localStorage.getItem("ItemCache") != NewAssociatedIds) {
            $scope.dropzoneCallAjax(indexIds, entityId);
        } else if ($("ul#listaID")[0].childElementCount <= 0) {
            $scope.dropzoneCallAjax(indexIds, entityId);
        }
        else {
            $("#ModalSearchDropzone").modal();
            if ($(".modal-backdrop").hasClass("in") == true) {
                $(".modal-backdrop").css("position", "relative");
            }
        }


    }

    $scope.dropzoneCallAjax = function (indexIds, entityId) {
        // ajax pedir los datos
        var a = '';
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Tasks/GetAsociatedIndexsDropzon?' + jQuery.param({ indexId: indexIds, entityId: entityId }),
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                a = data;
                var ElementoJson = JSON.parse(a);
                // console.log(ElementoJson.length);
                $("#ModalSearchDropzone").modal();
                if ($(".modal-backdrop").hasClass("in") == true) {
                    $(".modal-backdrop").css("position", "relative");
                }

                for (var i = 0; i < ElementoJson.length; i++) {
                    var name = ElementoJson[i].Value,
                        id = ElementoJson[i].$id,
                        Code = ElementoJson[i].Code;
                    if (Code != 'PF') {
                        $('#listaID').append('<li class="ModalClassUl" id="' + Code + '" name="' + name.toString() + '"  onclick="AssociatedIndexsModalselectBtn(this);">' + Code + ' - ' + name.toString() + '</li>');
                    }
                }
                var NewAssociated = localStorage.getItem("NewAssociatedEntityId") + localStorage.getItem("NewAssociatedIndexIds");
                localStorage.setItem("ItemCache", NewAssociated);
            }, error: function (error) {
                console.log(error);
            }

        });
        ////// ajax pedir los datos
        ////var indexsList = [];
        ////if (indexIds.indexOf(",") != -1) {
        ////    indexsList = indexIds.split(",");
        ////}
        ////else {
        ////    indexsList.push(indexIds);
        ////}

        ////$(indexsList).each(function (item) {

        ////    FieldsService.GetAll(item).then(function (d) {
        ////        var Index = JSON.parse(d.data);

        ////        $scope.LoadIndexList(item, entityId);
        ////    });



        ////});
    }


    $scope.LoadIndexList = function (indexId, entityId) {

        var a = '';
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Tasks/GetAsociatedIndexsDropzon?' + jQuery.param({ indexId: indexId, entityId: entityId }),
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                a = data;
                var ElementoJson = JSON.parse(a);
                // console.log(ElementoJson.length);
                $("#ModalSearchDropzone").modal();
                if ($(".modal-backdrop").hasClass("in") == true) {
                    $(".modal-backdrop").css("position", "relative");
                }

                for (var i = 0; i < ElementoJson.length; i++) {
                    var name = ElementoJson[i].Value,
                        id = ElementoJson[i].$id,
                        Code = ElementoJson[i].Code;
                    if (Code != 'PF') {
                        $('#listaID').append('<li class="ModalClassUl" id="' + Code + '" name="' + name.toString() + '"  onclick="AssociatedIndexsModalselectBtn(this);">' + Code + ' - ' + name.toString() + '</li>');
                    }
                }
                var NewAssociated = localStorage.getItem("NewAssociatedEntityId") + localStorage.getItem("NewAssociatedIndexIds");
                localStorage.setItem("ItemCache", NewAssociated);
            }, error: function (error) {
                console.log(error);
            }
        });
    }




    $scope.AssociatedIndexsModalselectBtn = function(sender) {
        var IndexListSelectedValue = sender.id;
        var NewAssociatedEntityId = localStorage.getItem("NewAssociatedEntityId");
        var NewAssociatedDocIds = localStorage.getItem("NewAssociatedDocIds");
        var NewAssociatedIndexIds = localStorage.getItem("NewAssociatedIndexIds");
        var ruleId = localStorage.getItem("NewAssociatedruleId");
        var docId = localStorage.getItem("NewAssociateddocId");

        var List_FilesIds = JSON.parse(sessionStorage.getItem('Lista_Archivos'));

        if (typeof NewAssociatedDocIds === 'string') {
            NewAssociatedDocIds = [NewAssociatedDocIds];
        }

        //for (i = 0; i < NewAssociatedDocIds.length; i++) {
        for (i = 0; i < List_FilesIds.length; i++) {
            $.ajax({
                type: "POST",
                url: ZambaWebRestApiURL + '/Tasks/UpdateTaskIndex?' + jQuery.param({ DoCTypeId: NewAssociatedEntityId, DocId: List_FilesIds[i], IndexId: NewAssociatedIndexIds, Data: IndexListSelectedValue }),
                //url: ZambaWebRestApiURL + '/Tasks/UpdateTaskIndex?' + jQuery.param({ DoCTypeId: NewAssociatedEntityId, DocId: NewAssociatedDocIds[i], IndexId: NewAssociatedIndexIds, Data: IndexListSelectedValue }),
                //url: ZambaWebRestApiURL + '/Tasks/Call_UpdateTaskIndex?' + jQuery.param({ DoCTypeId: NewAssociatedEntityId, DocId: emi[i], IndexId: NewAssociatedIndexIds, Data: IndexListSelectedValue }),
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (data) {
                    toastr.success("Archivo actualizado exitosamente");
                    $("#ModalSearchDropzone").modal('hide');
                    try {
                        var NewAssociated = localStorage.getItem("NewAssociatedEntityId");
                        LoadGrillaForm(NewAssociated);

                    } catch (e) {

                    }
                },
                error: function (error) {
                    console.log(error);
                    toastr.success("Archivo actualizado exitosamente");
                    $("#ModalSearchDropzone").modal('hide');
                }
            });

            if (ruleId != "undefined" && ruleId != null) {
                ExecuteRuleIdDropzone(ruleId, List_FilesIds[i]);
            }
            sessionStorage.removeItem('Lista_Archivos');
        }
    }
    $scope.ExecuteRuleIdDropzone = function (ruleId, resultIds) {
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params: {
                "ruleId": ruleId,
                "resultIds": resultIds
            }
        };

        $.ajax({
            "async": false,
            "url": ZambaWebRestApiURL + '/tasks/ExecuteTaskRule',
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "success": function () {
                toastr.success('Se ha ejecutado la acción');

            },
            "error": function () {
                toastr.error('Error al ejecutar acción');
            },
            "data": JSON.stringify(genericRequest)
        });
    }

    $scope.RemoveStyleMultiDropzone = function () {
        var CountDropzone = $("zamba-drop");
        for (var i = 0; i < CountDropzone.length; i++) {
            var dropzamba = $("zamba-drop")[i].id;
            if (dropzamba > 0) {
                var eventoStyle = $($("zamba-drop")[i]).children(2)[2].children["0"].style[0];
                if (eventoStyle == "pointer-events") {
                    $($("zamba-drop")[i]).children(2)[2].children["0"].removeAttribute("style");
                }
            }
        }
    }




}]);
