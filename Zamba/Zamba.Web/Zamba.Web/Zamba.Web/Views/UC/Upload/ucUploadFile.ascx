<%@ Control Language="C#" AutoEventWireup="true" Inherits="Views_UC_Upload_ucUploadFile" CodeBehind="ucUploadFile.ascx.cs" %>

<style>
    .officeImgUpload {
        height: 50px;
        width: 50px;
    }

    #sourcesUploadFile {
        padding-left: 50px;
        height: 40%;
        overflow: auto;
        display: none !important;
    }

    .importantRule {
        display: block !important;
    }

    .divOfficeUpload {
        display: inline;
    }

    .spanOfficeUpload {
        cursor: pointer;
        color: blue;
        text-decoration: underline;
    }

        .spanOfficeUpload:hover {
            text-decoration: none;
        }

    .modal-content {
        margin-top: 50%
    }
</style>



<asp:Panel ID="pnlListadoIndices" runat="server" Width="550px" ScrollBars="Auto"  class="noprint">
    <fieldset id="InsertOption" title="" class="Fielset-controles-UC" enableviewstate="true" style="padding: 5px;">
        <h4><strong>Agrega aca los archivos</strong></h4>
        <div id="sourcesUploadFile" style="display: none !important">
            <strong style="margin-left: -22px" id="TempTitle">Templates</strong>
            <%--       <img class="officeImgUpload" title="Nuevo documento Word" src="../../Content/Images/office/word.png" onclick="OpenNewFile('word');" />
            <img class="officeImgUpload" title="Nuevo documento Excel" src="../../Content/Images/office/excel.png" onclick="OpenNewFile('excel');" />
            <img class="officeImgUpload" title="Nuevo documento PowerPoint" src="../../Content/Images/office/ppt.png" onclick="OpenNewFile('ppt');" />
            <img class="officeImgUpload" title="Nuevo documento Outlook" src="../../Content/Images/office/outlook.png" onclick="OpenNewFile('outlook');" />--%>
        </div>
        <div class="UserControlBody">
            <form name="form1" method="post" enctype="multipart/form-data" >
            </form>
            <form action="../../Services/FileUpload.ashx" style="max-height: 1000px" class="dropzone" id="dZUpload">
                <div class="fallback">
                    <div class="dz-default dz-message" />
                    <input type="file" name="file" multiple />
                </div>

            </form>

            <iframe src="" style="border: none; width: 300px; height: 400px" id="previewItemIframe"></iframe>
        </div>
    </fieldset>

   


</asp:Panel>


<script type="text/javascript">

   


    $(document).ready(function () {
        if (typeof (thisDomain) == "undefined") thisDomain = parent.thisDomain;
        $(".officeImgUpload").tooltip();
        //GetInsertTemplates();
  
    Dropzone.options.dZUpload = {
        maxFilesize: 45,
        //maxFiles: 1,
        addRemoveLinks: true,
        init: function () {
        },
        success: function (file, args) {
            try {
                if (args != undefined && args != null && args != '') {
                    var files = JSON.parse(args);
                    $(files).each(function (item) {
                        var fileURL = item;
                        $("#previewItemIframe").attr('src', fileURL);
                        EnableInsert();
                    });
                }
            } catch (e) {

            }

        },
        removedfile: function (file) {
            bootbox.confirm({
                message: "Esta seguro de eliminar: " + file.name + "?",
                buttons: {
                    confirm: {
                        label: 'Si',
                        className: 'btn-default'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-primary'
                    }
                },
                callback: function (result) {
                    if (result) DeleteFileDZ();
                }
            });

            function DeleteFileDZ() {
                $.ajax({
                    type: "POST",
                    url: "../../Services/FileUpload.ashx?remove=true&file=" + file.name,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                });
                var _ref;
                return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
            }
        },
        dictDefaultMessage: "Arrastre y suelte aqui los archivos, o haga click y seleccionelo desde una ubicación. * Imagenes, Audio, Video, Office, Zip, ...",
        dictFallbackMessage: "El explorador no soporta drap and drop.",
        dictFallbackText: "Por favor usar el formulario para subir archivos.",
        // dictFileTooBig: "El archivo que intenta subir es muy grande {{filesize}}MiB. Tamaño maximo: {{maxFilesize}}MiB.",
        dictFileTooBig: "El archivo que intenta subir es muy grande.",
        dictInvalidFileType: "No se pueden subir archivos de este tipo.",
        dictResponseError: "Se produjo el siguiente error en el servidor: {{statusCode}}.",
        dictCancelUpload: "Cancelar",
        dictCancelUploadConfirmation: "Estas seguro de cancelar?",
        dictRemoveFile: "Eliminar",
        dictMaxFilesExceeded: "No se pueden subir mas archivos.",
        };


    });
    //($("iframe#ContentPlaceHolder_insertIframe").contents().find("#dZUpload"))
    //document.getElementById("ContentPlaceHolder_insertIframe").contentWindow.addDZ()
    function addDZ(file) {
        var dz = Dropzone.forElement("#dZUpload");
        dz.addFile(file);
        dz.processQueue();
    }
    function getDZ() {
        return Dropzone.forElement("#dZUpload");
    }

    function GetInsertTemplates() {
        $.ajax({
            type: "GET",
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            url: '../../api/WebTemplates/GetTemplates',
        }).done(function (d) {
            if (d != undefined && d.Table != undefined) {
                var t = d.Table;
                if (t.length > 0) {
                    //$("#sourcesUploadFile").attr("style", 'display: block !important');
                }
                for (var i = 0; i < t.length; i++) {
                    var $div = $("<div/>").addClass("row divOfficeUpload").appendTo("#sourcesUploadFile")
                        .attr({ "id": t[i].Id, "path": t[i].PAth, "title": t[i].Description }).tooltip()
                        .click(function () { downloadOfficeTemplate(this) });
                    $("<img/>").addClass("officeImgUpload").appendTo($div).attr("src", GetIcon(t[i].PAth));
                    $("<span/>").addClass("spanOfficeUpload").text(t[i].Name).appendTo($div);

                }
            }

            function GetIcon(f) {
                var file = "";
                var ext = f.split('.').pop();
                switch (ext) {
                    case "doc": case "docx":
                        file = "word";
                        break;
                    case "xls": case "xlsx":
                        file = "excel";
                        break;
                    case "ppt": case "pptx":
                        file = "ppt"
                        break;
                    default:
                        file = "file";
                        break;
                }
                return "../../Content/Images/office/" + file + ".png"
            }
            function downloadOfficeTemplate(_this) {
                var id = $(_this).attr("id");
                var path = $(_this).attr("path");
                var uri = thisDomain + "/api/WebTemplates/DownloadTemplate?path=" + path;
                var downloadLink = document.createElement("a");
                downloadLink.href = uri;
                document.body.appendChild(downloadLink);
                downloadLink.click();
                document.body.removeChild(downloadLink);
            }
        });
    }

    $("button[data-bb-handler='cancel']").click(function () {
       // alert("hola")
    })
</script>
