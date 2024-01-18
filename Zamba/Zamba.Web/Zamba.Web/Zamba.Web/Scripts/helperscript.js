﻿function ChangeEditorView(el, type) {
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

function ReplaceUnderscore(items, b) {
    if (items == undefined || items == "") return "";
    if (typeof (items) == "string")
        return b ? items.replace("_", " ") : items.replace(" ", "_");

    for (var i = 0; i <= items.length - 1; i++) {
        if (b) {
            items[i] = items[i].replace("_", " ");
        }
        else {
            items[i] = items[i].replace(" ", "_");
        }
    }
    return items;
}


function GetTinyMCEContent(editor) {
    return tinyMCE.editors[editor].getContent();
}
function SetTinyMCEContent(editor, content) {
    $(document).ready(function () {
        setTimeout(function () {
            tinyMCE.editors[editor].setContent(content, { format: 'raw' });
        }, 500);
    });
}

jQuery(function ($) {    
//$(document).ready(function () {
    var zh = $('[zamba-help]');

    for (var i = 0; i <= zh.length - 1; i++) {
        var zhAttr = $(zh[i]).attr("zamba-help");
        var zhAttrId = zhAttr.substring(0, zhAttr.indexOf(",")) || zhAttr;

        var iframe = '<iframe height=300 width= 350 src="' + GetURLHelper() + '/shortcontent/' + zhAttrId + '"></iframe>';
        var tooltipPosition = "bottom";

        var $hSpan = $("<span/>").addClass("glyphicon glyphicon-question-sign");
        var attrs = zhAttr.substring(zhAttr.indexOf(",") + 1).replace(":", "=").split(",")

        for (var j = 0; j <= attrs.length - 1; j++) {
            var att = attrs[j].split("=")[0];
            var val = attrs[j].split("=")[1];
            if (att == "position") {
                tooltipPosition = val;
                continue;
            }
            if (att) $hSpan.css(att, val);
        }

        $(zh[i]).after($hSpan);

        $hSpan.attr({
            "data-toggle": "tooltip",
            "data-trigger": "hover",
            "data-placement": tooltipPosition,
            "data-html": "true",
            "zamba-help": zhAttr,
            "data-title": iframe,
          //  "title": iframe,
        }).click(function (e) {
            var id = $(this).attr("zamba-help").substring(0, zhAttr.indexOf(",")) || $(this).attr("zamba-help");
            window.open(GetURLHelper() + '/fullcontent/' + id, '_blank');
        });

        $hSpan.tooltip({
            //effect: 'slide',
            //content:"sdfdssf",
            hide: {
                effect: 'explode'// added for visibility
            },
            content: iframe,
            open: function (event, ui) {
                ui.tooltip.css("max-width", "600px");
                $(ui.tooltip).appendTo(this);
            }
        });

    }
});