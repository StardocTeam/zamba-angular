$(document).ready(function () {

    tinymce.init({
        language: "es",
        selector: "textarea",
        //  elements: "@ViewData.TemplateInfo.GetFullHtmlFieldName(string.Empty)",
        theme: "modern",
        theme_advanced_buttons1: "fullscreen,code",
        theme_advanced_buttons2: "",

        paste_data_images: true,
        //images_upload_url: '/Circular/UploadImage',
        //images_upload_base_path: '/some/basepath',
        relative_urls: false,
        remove_script_host: true,
        convert_urls: true,
        //setup: function (editor) {
        //    editor.addButton('helpReference', {
        //        text: 'Referenciar',
        //        icon: 'insert',
        //        onclick: function (e) {
        //            $("#helpReferenceModal").modal().draggable();                   
        //        }
        //    });
        //},
        // content_css: "http://localhost/Circulares/Scripts/tinymce/external_fonts.css",    
        //http://stackoverflow.com/questions/16582910/tinymce-4-theme-advanced-fonts
        fontsize_formats: "8pt 9pt 10pt 11pt 12pt 13pt 14pt 15pt 16pt 17pt 18pt 19pt 20pt 21pt 22pt 23pt 24pt 36pt",
        //theme_advanced_fonts:
        font_formats:
             "Andale Mono=andale mono,times;" +
               "Arial=arial,helvetica,sans-serif;" +
               "Arial Black=arial black,avant garde;" +
               "Book Antiqua=book antiqua,palatino;" +

               "Calibrí=calibri;" +
               "Comic Sans MS=comic sans ms,sans-serif;" +
               "Courier New=courier new,courier;" +
               "Georgia=georgia,palatino;" +
               "Helvetica=helvetica;" +
               "Impact=impact,chicago;" +
               "Symbol=symbol;" +
               "Tahoma=tahoma,arial,helvetica,sans-serif;" +
               "Terminal=terminal,monaco;" +
               "Times New Roman=times new roman,times;" +
               "Trebuchet MS=trebuchet ms,geneva;" +
               "Verdana=verdana,geneva;" +
               "Webdings=webdings;" +
               "Wingdings=wingdings,zapf dingbats",
        /*Excel copy-paste Utility :Starts*/
        paste_retain_style_properties: "all",
        paste_strip_class_attributes: "none",
        //paste_remove_spans : true,  
        /*Excel copy-paste Utility :Ends*/
        //               height: '100% !important',
        height: "300px",
        // autoresize_min_height: 400,
        force_br_newlines: false,
        force_p_newlines: false,
        // remove_linebreaks: false,
        forced_root_block: '',

        //Para pegar con word  http://www.tinymce.com/wiki.php/Plugin3x:paste
        paste_auto_cleanup_on_paste: true,
        plugins: [
            "advlist autolink lists link image charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars code fullscreen",
            "insertdatetime media nonbreaking save table contextmenu directionality",
            "emoticons paste textcolor colorpicker textpattern imagetools fullscreen template preview code"
        ],
        toolbar1: "undo redo | newdocument | fontselect  | fontsizeselect | styleselect  | bold italic forecolor backcolor | bullist numlist outdent indent | alignleft aligncenter alignright alignjustify | image | preview | fullscreen | helpReference ",

        image_advtab: true,

        oninit: function () {  // Once initialized, tell the editor to go fullscreen
            //    tinyMCE.get('description').execCommand('mceFullScreen');
        },
        file_browser_callback: function (field_name, url, type, win) {
            if (type == 'image') $('#my_form input').click();
        },
    });
});


function UploadIMG(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            var img64 = e.target.result;
            var $thisInp = top.$('.mce-btn.mce-open').parent();
            $thisInp.parent().parent().parent().find(".mce-textbox.mce-last").val(img64);
            $thisInp.find('.mce-textbox').val(img64).closest('.mce-window').find('.mce-primary').click();
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function HelpReferenceContent(e) {
    var s = e;
}