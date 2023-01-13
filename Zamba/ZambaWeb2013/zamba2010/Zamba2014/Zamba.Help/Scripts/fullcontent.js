var jsonDataTree;
//https://github.com/jonmiles/bootstrap-treeview
$(document).ready(function () {
    $("button").tooltip({
        placement: 'bottom'
    });
    $("#contentContainer").split({ orientation: 'vertical', limit: 10, position: '20%' });
    $.ajax({
        type: "POST",
       // async: false,
        url: Treedataurl(),
        success: function (result) {
            ProcessJson(result.Nodes);
            SelectNode();
        }, error: function (e, r, x) {
            console.error(e);
        }
    });
    $("#searchTreeView").keydown(function (e) {
        // ProcessJson(jsonDataTree, String.fromCharCode(e.which));
        if (e.which === 13) return;
        $('#treeview').treeview('collapseAll', { silent: true });

        var txt;
        if (e.which === 8) {//borrar
            txt = $("#searchTreeView").val().substring(0, $("#searchTreeView").val().length - 1);
        }
        else {
            txt = $("#searchTreeView").val() + String.fromCharCode(e.which);
        }

        $('#treeview').treeview('search', [txt, {
            ignoreCase: true,     // case insensitive
            exactMatch: false,    // like or equals
            revealResults: true,  // reveal matching nodes
        }]);

    });
    function SelectNode() {
        var loc = window.location.href.toUpperCase();
      //  var str = "FULLCONTENT/"
        var id = loc.substring(loc.lastIndexOf('/') + 1); //loc.substring(loc.indexOf(str) + str.length);
        if (!isNaN(id)) SelectNodeById(id);
    }
    ListMenuContext();
});

function ListMenuContext() {
    $(document).on('contextmenu', ".list-group-item.node-treeview", function (event) {
        event.preventDefault();
        var $l = $(event.target);
        if (!$l.children("b").length) return;

        var txt;
        $.each($l[0].childNodes, function (i, e) {
            if (e.tagName === undefined) {
                txt = e.nodeValue.trim();
                return false;
            }
        });

        // Show contextmenu
        $(".custom-menu").data({ "selId": $l.children("b").text(), "selText": txt }).finish().toggle(100).
        // In the right position (the mouse)
        css({
            top: event.pageY + "px",
            left: event.pageX + "px"
        });
    });
    $(document).on('mousedown', ".list-group-item.node-treeview", function (e) {
        // If the clicked element is not the menu
        if (!$(e.target).parents(".custom-menu").length > 0) {
            // Hide it
            $(".custom-menu").hide(100);
        }
    });
    $(".custom-menu li").click(function (e) {
        var $menu = $(this).parent();
        // This is the triggered action name
        switch ($(this).attr("data-action")) {
            // A case for each action. Your actions here
            case "addRef": SelectRefHelp($menu.data("selId"), $menu.data("selText")); break;
                //case "second": alert("second"); break;
                //case "third": alert("third"); break;
        }
        // Hide it AFTER the action was triggered
        $(".custom-menu").hide(100);
    });
    $(document).on('click', '.list-group-item.node-treeview', function (e) {
        if (!$(e.target).hasClass("icon")) {
            var id = parseInt($(this).attr("data-nodeid"));
            var exp = !$('#treeview').treeview('getNode', id).state.expanded;
            $('#treeview').treeview(exp ? 'expandNode' : 'collapseNode', [id, { levels: 1, silent: true }]);
        }
    });
}


function SelectRefHelp(id, txt) {
    parent.AddHelpReference(id, txt);
}

function SelectNodeById(id) {
    $("#treeview").treeview("expandAll");
    var visibleNode = [];
    var $tv = $('#treeview');
    var l = $(".list-group-item.node-treeview");
    for (var i = 0; i <= l.length - 1; i++) {
        var node = $('#treeview').treeview('getNode', i);
        if (node.text.indexOf("<b>" + id + "</b>") > -1) {
            var parentNode = $tv.treeview('getParent', ($tv.treeview('getParent', ($tv.treeview('getParent', node)))));
            visibleNode.push(parentNode.nodeId);
            $tv.treeview('expandNode', [parentNode, { levels: 3, silent: true }]);
            $tv.treeview('selectNode', [node, { silent: true }]);
        }
        else if (visibleNode.indexOf(node.nodeId) === -1) {
            $tv.treeview('collapseNode', [node, { silent: true, ignoreChildren: false }]);
        }
    }
}

function ProcessJson(data, filter) {
    jsonDataTree = data;
    var apps = [];
    for (var app = 0; app <= data.length - 1; app++) {
        var mods = [];
        var appItems = data[app].ChildItems;
        for (var mod = 0; mod <= appItems.length - 1; mod++) {
            var modItems = appItems[mod].ChildItems;
            var functions = [];
            for (var fun = 0; fun <= modItems.length - 1; fun++) {
                var funItems = modItems[fun].HelpItems;
                var items = [];
                for (var item = 0; item <= funItems.length - 1; item++) {
                    //var text = "<b>" + funItems[item].Id + "</b> " + funItems[item].Title + ", " + funItems[item].Name;
                    var text = funItems[item].Title + ", " + funItems[item].Name;
                    if (filter === undefined || text.toUpperCase().indexOf(filter) > -1) {
                        items.push({
                            href: "#SearchItem" + funItems[item].Id,
                            text: text,
                            icon: "glyphicon glyphicon-chevron-right",
                            selectedIcon: "glyphicon glyphicon-ok",
                            nodes: "",
                        });
                    }
                    //if (text.toUpperCase().indexOf(filter) > -1) $('#treeview').treeview("expandAll");
                }
                functions.push({
                    href: "#FunctionItem" + modItems[fun].Name,
                    text: modItems[fun].Name,
                    nodes: items,
                });
            }
            var modItems = appItems[mod].HelpItems;
            var items = [];
            for (var item = 0; item <= modItems.length - 1; item++) {
                //var text = "<b>" + modItems[item].Id + "</b> " + modItems[item].Title + ", " + modItems[item].Name;
                var text = modItems[item].Title + ", " + modItems[item].Name;
                if (filter === undefined || text.toUpperCase().indexOf(filter) > -1) {
                    functions.push({
                        href: "#SearchItem" + modItems[item].Id,
                        text: text,
                        icon: "glyphicon glyphicon-chevron-right",
                        selectedIcon: "glyphicon glyphicon-ok",
                        nodes: "",
                    });
                }
                //if (text.toUpperCase().indexOf(filter) > -1) $('#treeview').treeview("expandAll");
            }
            mods.push({
                href: "#ModItem" + appItems[mod].Name,
                text: appItems[mod].Name,
                nodes: functions,
            });
        }
        var appItems = data[app].HelpItems;
        var items = [];
        for (var item = 0; item <= appItems.length - 1; item++) {
            //var text = "<b>" + appItems[item].Id + "</b> " + appItems[item].Title + ", " + appItems[item].Name;
            var text = appItems[item].Title + ", " + appItems[item].Name;
            if (filter === undefined || text.toUpperCase().indexOf(filter) > -1) {
                mods.push({
                    href: "#SearchItem" + appItems[item].Id,
                    text: text,
                    icon: "glyphicon glyphicon-chevron-right",
                    selectedIcon: "glyphicon glyphicon-ok",
                    nodes: "",
                });
            }
            //if (text.toUpperCase().indexOf(filter) > -1) $('#treeview').treeview("expandAll");
        }
        apps.push({
            href: "#AppItem" + data[app].Name,
            text: data[app].Name,
            nodes: mods,
        });
    }
    FillTree(apps, filter);
}

function RemoveSearch() {
    $("#searchTreeView").val("");
    $('#treeview').treeview('clearSearch');
    $('#treeview').treeview('collapseAll', { silent: true });
}

function ExpandSearch() {
    $('#treeview').treeview('expandAll', { levels: 3, silent: true });
}

function CollapseSearch() {
    $('#treeview').treeview('collapseAll', { silent: true });
}

function FillTree(data, collapse) {
    var state = collapse ? "expandAll" : "collapseAll";
    $('#treeview').treeview({
        color: "#428bca",
        showBorder: false,
        data: data
    }).on('nodeSelected', function (event, data) {
        if (data.nodes === "") {
            $("#defaultContent").css("display", "none");
            $("#navigateContent").css("display", "block");
            $("#contentDiv").css("overflow", "hidden");
            var id = data.href.replace("#SearchItem", "");
            var url = LocalhostURL() + "/viewer/fullcontenthtml/" + id;
            if (!isNaN(id))
                $("#navigateContent").children("iframe").attr("src", url);
        }
    }).treeview(state, { silent: true })
    .treeview('search', ['Parent', {
        ignoreCase: true,     // case insensitive
        exactMatch: false,    // like or equals
        revealResults: true,  // reveal matching nodes
    }]);
}

function toogleTree(_this) {
    var collapse = $("#treeDiv").data("collapse") == true || false;
    if (collapse === false) {
        $("#contentDiv").data("width", $("#contentDiv").css("width")).css("width", "100%");
        $(".vsplitter").hide();
        $("#toogleTree").css("right", "inherit")
            .children("span").removeClass("glyphicon glyphicon-chevron-left")
            .addClass("glyphicon glyphicon-chevron-right");
        $("#treeDiv").children().not("#toogleTree").hide();
        $(_this).css("left", "0");
    }
    else {
        $("#contentDiv").css("width", $("#contentDiv").data("width"));
        $(".vsplitter").show();
        $("#toogleTree").css("right", "0")
           .children("span").removeClass("glyphicon glyphicon-chevron-right")
           .addClass("glyphicon glyphicon-chevron-left");
        $("#treeDiv").children().not("#toogleTree").show();
        $(_this).css("left", "auto");
    }
    $("#treeDiv").data("collapse", !collapse);
}
