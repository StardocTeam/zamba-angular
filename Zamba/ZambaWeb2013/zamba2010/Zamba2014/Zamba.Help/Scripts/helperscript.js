
jQuery(function ($) {
    HelperScript();
});



$(document).on('webkitAnimationEnd animationend MSAnimationEnd oanimationend DOMNodeInserted', function (e) {
    var eTarget = e.target;
    HelperScript();
  ///  console.log(eTarget.tagName.toLowerCase() + ' added to ' + eTarget.parentNode.tagName.toLowerCase());
    //$(eTarget).draggable(); // or whatever other method you'd prefer
});


function HelperScript() {
    var zh = $('[zamba-help]');
    //zh.attr({ "title": "Click para ver ayuda", "cursor":"pointer" }).tooltip();
    var width = 600;
    var height = 500;
    for (var i = 0; i <= zh.length - 1; i++) {
        var zhAttr = $(zh[i]).attr("zamba-help");
        var zhAttrId = zhAttr.substring(0, zhAttr.indexOf(",")) || zhAttr;

        if ($("#zHelpIcon" + zhAttrId).length) continue;

        var tooltipPosition = "top";
        var $hSpan = $("<span/>").addClass("glyphicon glyphicon-question-sign").attr("id", "zHelpIcon" + zhAttrId)
        .attr({ "title": "Click para ver ayuda" }).css("cursor","pointer");
        var attrs = zhAttr.substring(zhAttr.indexOf(",") + 1).replace(":", "=").split(",");

        for (var j = 0; j <= attrs.length - 1; j++) {
            var att = attrs[j].split("=")[0];
            var val = attrs[j].split("=")[1];
            switch (att) {
                case "position":
                    tooltipPosition = val;
                    break;
                case "width":
                    width = val;
                    break;
                case "height":
                    height = val;
                    break;
                default:
                    if (att) $hSpan.css(att, val);
            }
        }

        var iframe = '<iframe height=' + height + ' width= ' + width + ' src="' + LocalhostURL() + '/shortcontent/' + zhAttrId + '"></iframe>';

        $hSpan.attr("data-placement", tooltipPosition).tooltip();

        $(zh[i]).after($hSpan);
        $("#zHelpIcon" + zhAttrId).data({ "iframe": iframe, "id": zhAttrId }).click(function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            e.stopPropagation();
            width += "px";
            height += "px";
            var id = $(this).data("id");
            var modal = !($("#ShortHelpModal" + id).length) ?
            ' <div class="modal-fade" id="ShortHelpModal' + id + '" role="dialog">' +
                 ' <div class="modal-dialog">' +
                  '    <div class="modal-content" id="modalShortHelper" style="width: ' + width + '; heigth: ' + height + ';">' +
                   '       <div class="modal-header"> ' +
                    '          <button type="button" class="close" data-dismiss="modal">&times;</button>' +
                     '         <h4 class="modal-title">Zamba Helper <a target="_blank" style="float:right;margin-right:10px;" href=' +
                LocalhostURL() + '/fullcontent/' + id + '> Ir a ayuda </a> </h4>' +
                      '    </div>' +
                       '   <div class="">' +//modal-body
                       $(this).data("iframe") +
                        '  </div>' +
                     ' </div>' +
                 ' </div>' +
             ' </div>' : $("#ShortHelpModal" + id);

            $(modal).modal({ backdrop: 'static', keyboard: false })
            .css({ "position": "absolute", "top": "10%", "left": "10%" });
         
            $("#modalShortHelper").css({ "width": width });

            $(".modal-backdrop.fade.in").css("display", "none");
            $(".modal-backdrop").css("opacity", "0 !important").css("background-color", "transparent !important");
            $(".modal-backdrop.in").remove();
            $(function (e) {
                setTimeout(function () {
                    $("#ShortHelpModal"+ id).draggable();
                }, 1000);//"#modalShortHelper"
            });
        });
    }    
}



