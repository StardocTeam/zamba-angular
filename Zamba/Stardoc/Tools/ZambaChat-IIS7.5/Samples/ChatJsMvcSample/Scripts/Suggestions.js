$(document).ready(function () {
    if(!$(".userContDiv").length) return;

    if (typeof (winFormJSCall) != "undefined") {
        var sMProm = winFormJSCall.enableSuggestions();
    }
 
    toastr.options = {      
        "positionClass": "toast-top-left",
    }

    $(".titleDiv").click(function (e) {
        $(".contentTitleDiv").hide();
        $(".titleDiv").css("padding", "5px");
        $("#searchPeople").fadeIn().focus();
    });
    
    $("#searchPeople").keydown(function (e) {
        $(".userContDiv").hide();
        var c = $(e.target).val();
        var l = String.fromCharCode(e.which);
        var txt = e.which != 8 ? c + l : c.substring(0, c.length - 1);
        $(".userContDiv:contains(" + txt + ")").fadeIn();
    }).on('search', function () {
        $("#searchPeople").hide();
        $(".userContDiv, .contentTitleDiv").fadeIn();        
        $(".titleDiv").css("padding", "10px");
    });    
});

function addContact(_this) {
    var $div = $(_this).parent(".userContDiv");
    var img = $div.find(".avatarImg").attr("src");

    var user = {
        id: $div.attr("userid"),
        email: $div.attr("email"),
        name:$div.attr("username"),
        company: $div.attr("usercompany"),
        avatar: img.substring(img.indexOf(",") + 1),
    };
    sendUserInvitation(user, $.getUrlVars().userId);
    toastr.success("Se envio invitacion a '" + $div.attr("userName") + "'", "Usuario añadido");
    $div.fadeOut(500,function () {
        $(this).remove();
    });
}

function sendUserInvitation(user, id) {
    $.ajax({
        type: "POST",
        url: zCollServer + '/suggestedpeople/sendUserInvitation',//URLBase +
        data: { InvitedUser: user, Id: id },
        success: function (result) {
        }
    });
}

function fadeCompUsers(_this,c) {
    var $t = $(_this);
    var mode = $t.attr("mode") || "show";
    if (mode == "show") {
        $t.children("#rowExpColl").removeClass("glyphicon glyphicon-chevron-down").addClass("glyphicon glyphicon-chevron-right");
        $(".userContDiv[usercompany='"+c+"']").fadeOut();
        $t.attr("mode", "hide");
    }
    else {
        $t.children("#rowExpColl").removeClass("glyphicon glyphicon-chevron-right").addClass("glyphicon glyphicon-chevron-down");
        $(".userContDiv[usercompany='" +c +"']").fadeIn();
        $t.attr("mode", "show");
    }

}

$.expr[":"].contains = $.expr.createPseudo(function (arg) {
    return function (elem) {
        return $(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
    };
});

$.extend({
    getUrlVars: function(){
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for(var i = 0; i < hashes.length; i++)
        {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function(name){
        return $.getUrlVars()[name];
    }
});
