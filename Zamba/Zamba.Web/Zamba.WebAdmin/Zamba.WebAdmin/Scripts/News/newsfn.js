function ChangeEditorView(el, type) {
    $(el).parent().children().removeClass("active");
    $(el).addClass("active");

    if (type == "full") {
        $("#ShortContent").parent().hide();
        $("#FullContent").parent().show();
    }
    else {
        $("#FullContent").parent().hide();
        $("#ShortContent").parent().show();
    }
}

function GetTinyMCEContent(editor) {
    return tinyMCE.editors[editor].getContent();
}

function SetTinyMCEContent(editor, content) {
    $(document).ready(function () {
        //jQuery(document).on('tinymce-editor-init', function (event, editor) {
        //    // Blah.
        //});
        setTimeout(function () {
            tinyMCE.editors[editor].setContent(content, { format: 'raw' });
        }, 2500);
    });
}

//$(document).ready(function () {
//    $("#editOptionsModal").draggable({
//        handle: ".modal-header"
//    });
//});

//function AddHelpReference(id,text) {
//    var url = GetURLHelper() + '/fullcontent/' + id;
//    //var icon = '<div class="glyphicon glyphicon-question-sign"></div>';
//    var href='<a href="'+ url + '" target="_blank">' + text+'</a>';
//    tinymce.activeEditor.execCommand('mceInsertContent', false, href);
//}