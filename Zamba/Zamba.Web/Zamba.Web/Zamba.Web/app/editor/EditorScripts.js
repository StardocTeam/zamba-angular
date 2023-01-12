function getParameterByName(name) {
    var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}

var ValorUrl = "ZambaLeg.RestApi";

function LoadResultHtml(docid, doctypeid) {

   
    var content = '';
    // Llamada Ajax para para obtener documento de result
     $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL+'/Tasks/GetResultDocumentContent?' + jQuery.param({ docid: docid, doctypeid: doctypeid}),
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (r) {
                content = r;
            },
        });
    return content;
}

function SaveEditorContent(_docid, _doctypeid, _content) {
    
    var _obj = JSON.stringify({docid: _docid, doctypeid: _doctypeid, content: _content});

    $.ajax({
        type: "POST",
        url: ZambaWebRestApiURL + '/Tasks/SaveResultDocContent',
        contentType: "application/json; charset=utf-8",
        async: false,
        data: _obj,
        success: function (r) {
            
        },
    });
}


function SetEditorHeigth() {
    $('.k-editor').css("height",  $(document).outerHeight(false));
}

function SetSaveBtnColor() {
    $(".k-i-Guardar").parent('a').css("background-color", "#337ab7");
}