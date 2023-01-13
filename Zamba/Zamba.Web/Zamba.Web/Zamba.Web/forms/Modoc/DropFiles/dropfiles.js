var appblank = angular.module('appblank', [])
.directive('zambaDrop', function ($sce) {
    return {
        restrict: 'E',
        scope: {
            model: '=ngModel',
            parameters: '='
        },
        link: function (scope, element, attributes) {
            angular.element(element.find("#dZFUpload"))
                .attr({ "elemId": attributes.id, "id": "dZFUpload" + attributes.id });
            SetDZ(attributes.id);
        },
        //replace: true,
        templateUrl: $sce.getTrustedResourceUrl('../../DropFiles/dropfiletmp.html')
    };
})
    .controller('appController', ['$http', '$scope', function ($http, $scope) {
}])
;

function SetDZ(id) {
    //$(document).ready(function () {
        var entityId = $("#dZFUpload" + id).parents("zamba-drop").attr("EntityId");
        var docId = $("#dZFUpload" + id).parents("zamba-drop").attr("DocId");
        var elemId = $("#dZFUpload" + id).attr("elemId");
        
        $("#dZFUpload" + id).dropzone({
            maxFiles: 1,
            //removedfile: function (file) {
            //    bootbox.confirm({
            //        message: "Esta seguro de eliminar: " + file.name + "?",
            //        buttons: {
            //            confirm: {
            //                label: 'Si',
            //                className: 'btn-default'
            //            },
            //            cancel: {
            //                label: 'No',
            //                className: 'btn-primary'
            //            }
            //        },
            //        callback: function (result) {
            //            if (result) DeleteFileDZ();
            //        }
            //    });
            //    function DeleteFileDZ() {
            //        $.ajax({
            //            type: "POST",
            //            url: "../../Services/FileUpload.ashx?remove=true&file=" + file.name,
            //            contentType: "application/json; charset=utf-8",
            //            dataType: "json",
            //        });
            //        var _ref;
            //        return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            //    }
            //},
            addRemoveLinks: true,
            //accept: function (file, done) {
            //    console.log("uploaded");
            //    done();
            //},
            init: function () {
                this.on("maxfilesexceeded", function (file) {
                    this.removeFile(file);
                });
            },
            success: function (file, responseText) {
                if (responseText != "") {
                    bootbox.alert("Se ha producido un error al procesar el archivo.<hr/> Detalles:<br/>" + responseText);
                    this.removeFile(file);
                }
                else{
                    toastr.success("Archivo procesado exitosamente");
                    return file.previewElement.classList.add("dz-success");
                }
            },  
            sending: function (file, xhr, formData) {
                formData.append('isNGform', true);
                formData.append('entityId', entityId);
                formData.append('docId', docId);
                formData.append('newEntityId', elemId);
                console.log(formData)
            },
             error: function (file, errorMessage) {
              console.log(errorMessage);
            },
            queuecomplete: function () {
              //  if (errors) console.log("There were errors! dropfiles.js");               
            },
         
            dictDefaultMessage: "Arrastre y suelte aqui los archivos, o haga click y seleccionelo desde una ubicación.",
            dictFallbackMessage: "El explorador no soporta drap and drop.",
            dictFallbackText: "Please use the fallback form below to upload your files like in the olden days.",
            dictFileTooBig: "El archivo que intenta subir es muy grande ({{filesize}}MiB). Tamaño maximo: {{maxFilesize}}MiB.",
            dictInvalidFileType: "No se pueden subir archivos de este tipo.",
            dictResponseError: "Se produjo el siguiente error en el servidor: {{statusCode}}.",
            dictCancelUpload: "Cancelar",
            dictCancelUploadConfirmation: "Estas seguro de cancelar?",
            dictRemoveFile: "Eliminar",
            dictMaxFilesExceeded: "No se pueden subir mas archivos.",
        });
    //});
}