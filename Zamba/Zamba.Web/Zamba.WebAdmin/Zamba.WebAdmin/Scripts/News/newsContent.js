$(document).ready(function () {
    $(".newItem").tooltip();
    $("#filterNewsInp").on('search', function () {
        $("a.newItem").fadeIn();
    });

    $("#filterNewsInp").keydown(function (e) {
        $("a.newItem").hide();
        var c = $(e.target).val();
        var l = String.fromCharCode(e.which);
        var txt = e.which != 8? c+l:  c.substring(0, c.length - 1);     
        $("a.newItem:contains(" + txt + ")").fadeIn();
    });    
    
    $("#orderNewsBy").on('change', function () {
        $("a.newItem").show();
        var sel = (this.value);
        switch (sel) {
            case "all":
                $("a.newItem").fadeIn();
                break;
            case "noread":
                $("a.newItem.readed").fadeOut();
                break;
            case "recent":
                $aN=$('.allNews');
                $aN.find('a.newItem').sort(function (a, b) {
                    return new Date($(b).attr("created")) - new Date($(a).attr("created"));
                })
                .appendTo($aN);
                $aN.children(".col-md-12").appendTo($aN);
                break;
            case "last":
                $aN = $('.allNews');
                $aN.find('a.newItem').sort(function (a, b) {
                    return new Date($(a).attr("created")) - new Date($(b).attr("created"));
                })
                .appendTo($aN);
                $aN.children(".col-md-12").appendTo($aN);
                break;
        }
    })
});

function showMoreNews(e) {
    $("a.newItem:hidden:lt(3)").fadeIn();
    if (!$("a.newItem:hidden:lt(3)").length)
        $(e).fadeOut();

    $(".allNews").animate({ scrollTop: $(document).height() }, 1000);
}

$.expr[":"].contains = $.expr.createPseudo(function (arg) {
    return function (elem) {
        return $(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
    };
});
