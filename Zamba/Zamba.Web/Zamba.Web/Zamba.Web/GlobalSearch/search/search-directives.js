var urlGlobalSearch = ZambaWebRestApiURL + "/Search/";
var searchResults;
var iMsgTxt = [];



var GSLoading = {
    Show: function (txt) {
        var g = $("#globalSearchModalLoading");
        g.modal({ backdrop: 'static', keyboard: false });
        g.find("#txtToShow").text(txt || "Cargando...");
        $('.modal-backdrop').appendTo(g.parent());
        $('body').removeClass("modal-open")
        $('body').css("padding-right", "");
    },
    Hide: function () {
        $("#globalSearchModalLoading").modal("hide");
        $(".modal-backdrop.fade.in").remove();
    }
};

//(function () {
//    'use strict';
//    angular.module('zamba-search', ['ngSanitize', 'ngEmbed', 'ngAnimate', 'Search'])

app.directive('zambaSearch', function ($sce) {
    return {
        restrict: 'E',
        //scope: {
        //    model: '=ngModel',
        //    parameters: '='
        //},
      //  replace: true,
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: $sce.getTrustedResourceUrl('../../scripts/app/partials/searchbox.html'),
      
    };
})


app.directive('clickChk', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                e.stopPropagation();
                var elem = browserType() == 'chrome' ? e.toElement.tagName : e.target.tagName;
                if (elem == "INPUT") {
                    var $li = $(this).parent().children("li");
                    if ($li.length == 1) return;

                    var chk = 0;
                    var nochk = 0;
                    for (var i = 0; i <= $li.length - 1; i++) {
                        if ($($li[i]).children("label").children("input").prop("checked"))
                            chk += 1;
                        else
                            nochk += 1;
                    }
                    var $l = $($li[0]).children("label");
                    if (chk == $li.length - 1) {
                        $l.children("span").text('Ninguno');
                        $l.children('input').prop("checked", true);
                    }
                    if (nochk == $li.length - 1) {
                        $l.children("span").text('Todo');
                        $l.children('input').prop("checked", false);
                    }
                }
            });
        }
    };
}])

app.directive('selButton', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {

            element.bind('mouseenter', function (e) {
                $(this).tooltip();
            });

            element.bind('click', function (e) {
                e.stopPropagation();
                advFilterSelect($(e.currentTarget));
            });
        }
    };
}])

app.directive('checkIfSearch', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                var _this = $(e.target);
                if (!_this.hasClass("doGSSearch")) {
                    //Comprueba si hizo click en icono de busqueda sino cancela
                    e.stopPropagation();
                }
            });
        }
    };
}])
//.directive('hideShowInput', [function () {
//    return {
//        restrict: 'A',
//        replace: true,
//        link: function (scope, element, attrs) {
//            element.bind('mouseenter', function (e) {
//                //e.stopPropagation();
//                $(this).children(".typeahead").fadeIn().css("display", "inline-block");
//                if ($(this).children("ul").css("display")=="none")
//                    $(this).children(".typeahead").focus();
//                $(this).tooltip();
//                var $btn = $(this).children('button');
//                if ($btn.text() == '+') {
//                    var $li = $(this).children('ul').children("li");
//                    var t = '';
//                    var c = 0;
//                    for (var i = 0; i <= $li.length - 1; i++) {
//                        if ($($li[i]).attr("class").indexOf('ng-hide') != -1) {
//                            var txt = $($li[i]).children('label').text();
//                            t += (txt ? txt : $($li[i]).text().trim()) + '<br>';
//                            c++;
//                            if (c == 10) {
//                                t += "<br>Ver mas...";
//                                break;
//                            }
//                        }
//                    }
//                    $btn.attr("data-title", t);
//                    $btn.tooltip();
//                }
//                else
//                    $btn.removeAttr("data-title");
//            });
//            element.bind('mouseleave', function (e) {
//                 e.stopPropagation();
//                if (!$(this).children(".typeahead").val().length)
//                    $(this).children(".typeahead").fadeOut();
//            });
//        }
//    };
//}])

app.directive('chkboxControl', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                e.stopPropagation();

                var elem = browserType() == 'chrome' ? e.toElement.tagName : e.target.tagName;
                //if (elem == "LABEL" || elem == "SPAN")
                //    return;

                var $chk = $(this).children().children("input");
                var $li = $(this).parent().find("li");
                for (var i = 0; i <= $li.length - 1; i++) {
                    $($li[i].children[0].children[0]).prop('checked', $chk.is(':checked'));
                }
                var $span = $(this).children().children("span");
                $span.text($span.text() == 'Ninguno' ? 'Todo' : 'Ninguno');
            });
        }
    };
}])

app.directive('wordsEntIndex', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                $('#ui-datepicker-div').css("z-index", "9999999999999  !important");
                e.stopPropagation();
            });
            element.bind('change', function (e) {
                e.stopPropagation();
                if ($(e.target).hasClass("dateValueGS") && $(e.target).val() != "") {
                    $(e.target).parent().find(".dateCompare,.dateCompareChk").css("display", "block");
                }
            });
            element.bind('keydown', function (e) {
                e.stopPropagation();
                var ew = e.which;
                var id;
                var entId;
                var $prt = $(this).parent().parent().parent();
                if ($(this).parent()[0].tagName == "LI") {
                    id = $(this).parent().attr("id");
                    entId = $prt.attr("id").replace('item', '');
                }
                else {
                    id = $(this).parent().parent().attr("id");
                    entId = $prt.parent().attr("id").replace('item', '');
                }

                if (e.which == 13) {
                    $("#item" + entId).click();
                    return;
                }
                var word = $(this).val();
                if ((e.which >= 48 && ew <= 57) || (e.which >= 65 && ew <= 90))
                    word += String.fromCharCode(e.which);

                if (word.length <= 2 || ew == 46 || ew == 8 || ew == 219 || ew == 188)
                    return;
                //Agrego selecciones anteriores
                var en = [], wo = [], at = [];
                var $li = $("#item" + entId).children("ul").children("li");
                for (var i = 0; i <= $li.length - 1; i++) {
                    if ($($li[i]).children().val() != '' && $($li[i]).children().css("display") != "none" && $($li[i]).attr("id") != id) {
                        en.push(entId);
                        at.push($($li[i]).attr("id"))
                        wo.push($($li[i]).children().val());
                    }
                }
                //Agrego nuevo, actual
                en.push(entId);
                at.push(id);
                wo.push(word);

                var data = [];
                $.ajax({
                    type: "GET",
                    traditional: true,
                    async: false,
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    data: {
                        index: at.toString(), entities: en.toString(), word: wo.toString()
                    },
                    url: ZambaWebRestApiURL + '/Search/SuggestionsList',
                    success: function (result) {
                        if (result != null && result != undefined) {
                            for (var i = 0; i <= result.length - 1; i++)
                                data.push(result[i].word);
                        }
                    },
                });

                if (data.length >= 1)
                    BuildTH($(this), data);
            });
        }
    };
}])

app.directive('wordsEntSelect', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                e.stopPropagation();
            });

            element.bind('keydown', function (e) {
                e.stopPropagation();

                var id = $(this).parent().attr("id") == undefined ? ($(this).parent().parent().attr("id") == undefined ?
                    parseInt($(this).parent().parent().parent().attr("id").replace("item", "")) :
                    parseInt($(this).parent().parent().attr("id").replace("item", "")))
                    : parseInt($(this).parent().attr("id").replace("item", ""));
                if (e.which == 13) {
                    $("#item" + id).click();
                    return;
                }
                var word = $(this).val() + String.fromCharCode(e.which);
                if (word.length <= 2)
                    return;
                var data = [];
                $.ajax({
                    type: "GET",
                    traditional: true,
                    async: false,
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    data: {
                        index: 0, entities: id, word: word
                    },
                    url: ZambaWebRestApiURL + '/Search/SuggestionsByindex',
                    success: function (result) {
                        for (var i = 0; i <= result.length - 1; i++)
                            data.push(result[i].word);
                    },
                });
                if (data.length >= 1)
                    BuildTH($(this), data);
            });
        }
    };
}])

app.directive('wordsAttrSelect', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                e.stopPropagation();
                e.preventDefault();
            }),
                element.bind('change', function (e) {
                    e.stopPropagation();
                    if ($(e.target).hasClass("dateValueGS") && $(e.target).val() != "") {
                        $(e.target).parent().find(".dateCompare,.dateCompareChk").css({
                            "display": "block"
                        });
                        $(e.target).parent().find(".dateCompareChk").css("margin-left", "100px");
                    }
                });
            element.bind('keypress', function (e) {
                e.stopPropagation();
                var id = $(this).parent().attr("id") == undefined ? ($(this).parent().parent().attr("id") == undefined ?
                    parseInt($(this).parent().parent().parent().attr("id").replace("index", "")) :
                    parseInt($(this).parent().parent().attr("id").replace("index", "")))
                    : parseInt($(this).parent().attr("id").replace("index", ""));

                if (e.which == 13) {
                    $("#index" + id).click();
                    return;
                }

                var word = $(this).val();
                if ((e.which >= 48 && e.which <= 57) || (e.which >= 65 && e.which <= 90))
                    word += String.fromCharCode(e.which);
                var $tp = $(this).parent();
                var $li = $tp.children("ul").children("li");
                if ($li.length == 0)
                    $li = $tp.parent().children("ul").children("li");
                if ($li.length == 0)
                    $li = $tp.parent().parent().children("ul").children("li");

                var ent = [];
                for (var i = 0; i <= $li.length - 1; i++) {
                    if ($($li[i]).find("input").is(':checked')) {
                        ent.push(parseInt($($li[i]).find("input").attr("value")));
                    }
                }

                if (!ent.length >= 1) {
                    $("#index" + id).children(".adv").css("display", "flex");
                    return;
                }
                else
                    $("#index" + id).children(".adv").css("display", "none");

                if (word.length <= 2)
                    return;

                jQuery.ajaxSettings.traditional = true;
                var $this = $(this);
                var data = [];
                $.ajax({
                    type: "GET",
                    traditional: true,
                    async: false,
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    data: {
                        index: id, entities: ent.join(), word: word
                    },
                    url: ZambaWebRestApiURL + '/Search/SuggestionsByindex',
                    success: function (result) {
                        if (result != null) {
                            for (var i = 0; i <= result.length - 1; i++)
                                data.push(result[i].word);
                        }
                    },
                });

                if (data.length >= 1)
                    BuildTH($(this), data);
            });
        }
    };
}])

app.directive('expandItems', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                var mas = "<center><p>+ Ver mas...</p></center>";
                var menos = "<center><p>- Ver menos...</p></center>";
                e.stopPropagation();
                var show = $(this).html() == mas;
                var display = show ? "flex" : "none";
                $(this).html(show ? menos : mas);
                var $ul = $(this).parent().children("ul");
                var i = 0;
                var liN = $(this).parent().attr("id").indexOf("item") != -1 ? 6 : 8;
                for (var i = 0; i <= $ul.length - 1; i++) {
                    for (var li = liN; li <= $ul[0].children.length - 1; li++) {
                        var $cLi = $($ul[i].children[li])
                        if (display == 'none')
                            $cLi.addClass("ng-hide");
                        else
                            $cLi.removeClass("ng-hide");
                    }
                }

            });
        }
    };
}])

//Click de etapa, asignado, estado
app.directive('showAdvF', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                e.stopPropagation();
                var elem = browserType() == 'chrome' ? e.toElement : e.target;
                if (elem.tagName == "INPUT") return;
                // if (elem.tagName != "BUTTON") return;                  
                var $btn = $(elem);

                if ($btn.hasClass("advFilter")) return;

                if ($btn.hasClass("doGSSearch") || $btn.parent().hasClass("doGSSearch")) {
                    $btn.parents("li").click();
                    return;
                }

                $btn.parent().css("display", "none");
                var $divAdv = $btn.parent().parent();
                $divAdv.children("input").css("display", "flex");
                $divAdv.children("input").focus();
            });
        }
    };
}])

app.directive('advfilter', [function () {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, element, attrs) {

            element.bind('keydown', function (e) {
                var nEvents = [46, 40, 30, 8];
                if (nEvents.indexOf(e.which) != -1)
                    return;
                e.stopPropagation();
                var $a = $(this).parents('a');
                var isEntity = ($a.attr("id").indexOf('item') != -1);

                if (e.which == 13) {
                    $a.click();
                    return;
                }

                var word = $(this).val();
                //if ((e.which >= 48 && e.which <= 57) || (e.which >= 65 && e.which <= 90))
                word += String.fromCharCode(e.which);

                if (word.length <= 2)
                    return;
                var $tp = $(this).parent();
                var btn = $tp.children("button").attr("id") != undefined ? $tp.children("button").attr("id") :
                    $tp.parent().children("button").attr("id");

                var word = $(this).val();

                var $li = $a.children("ul").children("li");

                var en = [], wo = [], at = [];
                var method;
                var objData;

                var entw = '';
                var $ew = $a.children(".typeahead");
                if ($ew.css("display") != 'none' && $ew.val() != '')
                    entw = $ew.val();
                if (isEntity) {
                    //ENTIDAD
                    method = 'SuggestionsAdvEnt';
                    for (var i = 0; i <= $li.length - 1; i++) {
                        if ($($li[i]).children().val() != '' && $($li[i]).children().css("display") != "none") {
                            at.push($($li[i]).attr("id"))
                            wo.push($($li[i]).children().val());
                        }
                    }
                    objData = {
                        entity: $a.attr("id").replace('item', ''), entword: entw, index: at.toString(), word: wo.toString(), filter: btn, filterword: word
                    };
                }
                else {
                    //ATRIBUTO
                    method = 'SuggestionsAdvInd';
                    for (var i = 1; i <= $li.length - 1; i++) {
                        var $inp = $($li[i]).children("label").children("input");
                        if ($inp.prop("checked"))
                            en.push($inp.attr("value"));
                    }
                    objData = {
                        index: $a.attr("id").replace('index', ''), indword: entw, entities: en.toString(), filter: btn, filterword: word
                    };
                }

                jQuery.ajaxSettings.traditional = true;
                var $this = $(this);
                var data = [];
                var dataId = [];
                $.ajax({
                    type: "GET",
                    traditional: true,
                    async: false,
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    data: objData,
                    url: urlGlobalSearch + method,
                    success: function (result) {
                        if (result != null) {
                            for (var i = 0; i <= result.length - 1; i++) {
                                data.push(result[i].word);
                                dataId.push(result[i].Id);
                            }
                        }
                    },
                });
                if (data.length >= 1) {
                    //Guardo datos traidos de AJAX para validarlos con seleccion de usuario.
                    $this.data('word', data);
                    $this.data('ids', dataId);

                    BuildTH($this, data);
                }
            });
        }
    };
}])

app.directive('nitSetFocus', [
    '$timeout', '$parse',
    function ($timeout, $parse) {
        return {
            restrict: 'A',
            replace: true,
            link: function ($scope, $element, $attrs) {
                var model = $parse($attrs.nitSetFocus);
                $scope.$watch(model, function (value) {
                    if (value === true) {
                        $timeout(function () {
                            $element[0].focus();
                            $element[0].click();
                        });
                    }
                });
                $element.bind('blur', function () {
                    $scope.$apply(model.assign($scope, false));
                });
            }
        };
    }
])

app.directive('nitAutoSizeInput', [
    function () {
        return {
            restrict: 'A',
            replace: true,
            scope: {
                model: '=ngModel'
            },
            link: function ($scope, $element, $attrs) {
                var container = angular.element('<div style="position: fixed; top: -9999px; left: 0px;"></div>');
                var shadow = angular.element('<span style="white-space:pre;"></span>');

                var maxWidth = $element.css('maxWidth') === 'none' ? $element.parent().innerWidth() : $element.css('maxWidth');
                $element.css('maxWidth', maxWidth);

                angular.forEach([
                    'fontSize', 'fontFamily', 'fontWeight', 'fontStyle',
                    'letterSpacing', 'textTransform', 'wordspacing', 'textIndent',
                    'boxSizing', 'borderLeftWidth', 'borderRightWidth', 'borderLeftStyle', 'borderRightStyle',
                    'paddingLeft', 'paddingRight', 'marginLeft', 'marginRight'
                ], function (css) {
                    shadow.css(css, $element.css(css));
                });

                angular.element('body').append(container.append(shadow));

                function resize() {
                    shadow.text($element.val() || $element.attr('placeholder'));
                    $element.css('width', shadow.outerWidth() + 10);
                }

                resize();

                if ($scope.model) {
                    $scope.$watch('model', function () { resize(); });
                } else {
                    $element.on('keypress keyup keydown focus input propertychange change', function () { resize(); });
                }
            }
        };
    }
])

app.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        replace: true,
            element.bind("keydown", function (event) {
                if (event.which === 13) {

                    $(".remove-all-icon.fa.fa-trash-o.fa-2x").click();
                    scope.$apply(function () {
                        //cancelo la busqueda por ng-enter qeu busque
                        //solo al presionar click en item, sino hace busqueda dos veces
                        // scope.$eval(attrs.ngEnter);
                    });

                    event.preventDefault();
                }
            });
    };
})

//Directiva para rearmar el grupo de búsqueda
app.directive('editGroup', function () {
    return function (scope, element, attrs) {
        replace: true,
            element.bind("mouseenter", function (event) {
                $(this).tooltip();
            });
        element.bind("click", function (event) {
            event.stopPropagation();
            event.preventDefault();

            var f = $("body").data("filters");
            var $input = $(".search-parameter-input").eq(0);
            var inputTxt = $input.val();
            $input.val('').trigger("input");
            $input.eq(0).val(f.name).trigger("input");
            $input.parent().children("ul").css("left", ($(event.toElement).position().left) - 20).css("top", ($(event.toElement).position().top) + 25);
            var $a;
            switch (f.type) {
                case 1://Entidad
                    var $it = $("#item" + f.id);
                    $a = $it;
                    var $li = $it.children("ul").children("li");
                    var cont = 0;
                    for (var i = 0; i <= $li.length - 1; i++) {
                        var $in = $($li[i]);
                        if ($.inArray($in.attr("id"), f.filter) != -1) {
                            $in.children("input").css("display", "inline");
                            //Fecha con "entre"
                            if ($in.children("input").hasClass("dateValueGS") &&
                                f.filtersVal[cont].indexOf("-") > 0 && f.filtersVal[cont].length >= 14) {
                                $in.children("input").val(f.filtersVal[cont].substr(0, f.filtersVal[cont].indexOf("-")).trim());
                                var val = f.filtersVal[cont];
                                var val1 = val.substr(0, f.filtersVal[cont].indexOf("-")).trim();
                                var val2 = val.substr(f.filtersVal[cont].indexOf("-") + 1).trim();
                                $in.children().css({
                                    "display": "block", "visibility": "visible"
                                });
                                $in.find(".dateCompareChk>input").attr('checked', true);
                                $in.children(".dateValueGS").val(val1);
                                $in.children(".dateCompare").val(val2);
                                showCompareDateGS($in.find(".dateCompareChk"), 'entity', null);
                            }
                            else {
                                $in.children("input").val(f.filtersVal[cont]);
                            }
                            cont++;
                        }
                    }
                    if (f.value != '') {
                        $it.children("input").val(f.value);
                        $it.children("input").css("display", 'inline')
                    }
                    break;

                case 2://Indice -atributo
                    $a = $("#index" + f.id);
                    var $li = $a.children("ul").children("li");
                    for (var i = 0; i <= $li.length - 1; i++) {
                        var $in = $($li[i]).children().children("input");
                        if ($.inArray($in.attr("value"), f.filter) == -1)
                            $in.prop("checked", false);
                    }
                    //Fecha con "entre"
                    if (f.value != undefined && f.value != '') {
                        if ($a.children("input").hasClass("dateValueGS") &&
                            f.value.indexOf("-") > 0 && f.value.length >= 14) {
                            var val = f.value;
                            $a.children("input").val(val.substr(0, val.indexOf("-")).trim());
                            var val1 = val.substr(0, val.indexOf("-")).trim();
                            var val2 = val.substr(val.indexOf("-") + 1).trim();
                            $a.children(".dateValueGS").css({
                                "display": "inline-block", "visibility": "visible"
                            });
                            $a.children(".dateCompare,.dateCompareChk").css({
                                "display": "block", "visibility": "visible"
                            });
                            $a.find(".dateCompareChk>input").attr('checked', true);
                            $a.children(".dateValueGS").val(val1);
                            $a.children(".dateCompare").val(val2);
                            showCompareDateGS($a.find(".dateCompareChk"), 'index', null);
                        }
                        else {
                            $("#index" + f.id).children("input").val(f.value);
                        }
                        $("#index" + f.id).children("input").css("display", "inline");
                    }
                    break;
            }
            if ($a == undefined) return;
            var $advFDiv = $a.children('ul').children('.advFilter').children('div');
            for (var j = 0; j <= $advFDiv.length - 1; j++) {
                if (f.etapa != undefined && $($advFDiv[j]).children("button").attr("id") == 'etapa') {
                    var $inp = $($advFDiv[j]).children("input");
                    $inp.css("display", "inline");
                    $inp.val(f.etapa);
                    $inp.data("word", f.etapa);
                    $inp.data("ids", f.etapaId);
                }
                if (f.asignado != undefined && $($advFDiv[j]).children("button").attr("id") == 'asignado') {
                    var $inp = $($advFDiv[j]).children("input");
                    $inp.css("display", "inline");
                    $inp.val(f.asignado);
                    $inp.data("word", f.asignado);
                    $inp.data("ids", f.asignadoId);
                }
                if (f.estado != undefined && $($advFDiv[j]).children("button").attr("id") == 'estado') {
                    var $inp = $($advFDiv[j]).children("input");
                    $inp.css("display", "inline");
                    $inp.val(f.estado);
                    $inp.data("word", f.estado);
                    $inp.data("ids", f.estadoId);
                }
            }
        });
    };
})

app.directive('searchIndexslist', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: $sce.getTrustedResourceUrl('../../scripts/app/partials/partialSearchIndexs.html')
    };
})

app.directive('filterIndex', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: $sce.getTrustedResourceUrl('../../scripts/app/partials/PartialIndexType.html')
    };
})


app.directive('filtersGrid', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: $sce.getTrustedResourceUrl('../../scripts/app/partials/filters.html')
    };
})

app.directive('smartDatepicker', function () {
    return {
        restrict: 'A',
        scope: {
            options: '='
        },
        link: function (scope, element, attributes) {

            var onSelectCallbacks = [];
            if (attributes.minRestrict) {
                onSelectCallbacks.push(function (selectedDate) {
                    $(attributes.minRestrict).datepicker('option', 'minDate', selectedDate);
                });
            }
            if (attributes.maxRestrict) {
                onSelectCallbacks.push(function (selectedDate) {
                    $(attributes.maxRestrict).datepicker('option', 'maxDate', selectedDate);
                });
            }

            //Let others know about changes to the data field
            onSelectCallbacks.push(function (selectedDate) {
                //CVB - 07/14/2015 - Update the scope with the selected value
                element.triggerHandler("change");

                //CVB - 07/17/2015 - Update Bootstrap Validator
                var form = element.closest('form');

                if (typeof form.bootstrapValidator == 'function')
                    form.bootstrapValidator('revalidateField', element.attr('name'));
            });

            var options = _.extend({
                prevText: '<i class="fa fa-chevron-left"></i>',
                nextText: '<i class="fa fa-chevron-right"></i>',
                onSelect: function (selectedDate) {
                    angular.forEach(onSelectCallbacks, function (callback) {
                        callback.call(this, selectedDate)
                    })
                }
            }, scope.options || {});


            if (attributes.numberOfMonths) options.numberOfMonths = parseInt(attributes.numberOfMonths);

            if (attributes.dateFormat) options.dateFormat = attributes.dateFormat;

            if (attributes.defaultDate) options.defaultDate = attributes.defaultDate;

            if (attributes.changeMonth) options.changeMonth = attributes.changeMonth == "true";

            element.datepicker(options)
        }
    }
})

app.directive('searchResults', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: $sce.getTrustedResourceUrl('../../scripts/app/partials/resultsgrid.html?v=168')
    };
})
app.directive('resultsGrid', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: $sce.getTrustedResourceUrl('../../scripts/app/partials/resultsgrid.html?v=168')
    };
})

//.directive('scrollPagging', function ($document) {
//    return {
//        restrict: 'A',
//        link: function (scope, element, attrs) {

//            $document.bind('scroll', function () {
//                scope.$apply(attrs.scrolly);
//            });
//            $document.bind('wheel', function () {
//                scope.$apply(attrs.scrolly);
//            });
//        }
//    };
//})

app.filter('uniqueName', function () {
    return function (items) {
        var filtered = [];
        var filteredLookUp = {
        };
        var item;
        //Para que coloque primero la barra de botones de sugerencias.
        var entObj = {};
        entObj.button = true;
        entObj.buttonVal = $("body").data('filterBtn') == undefined ? 'all' : $("body").data('filterBtn');
        filtered.push(entObj);
        //Ordeno primero los Atributos despues los Indices
        var i = 0;
        var j = 0;
        while (j <= items.length - 1) {
            if (items[i].type == 2) {
                var index = jQuery.extend(true, {
                }, items[i]);//Clone
                items.splice(i, 1);
                items.push(index);
            }
            else
                i++;
            j++;
        }

        for (item in items) {
            if (!filteredLookUp[items[item].name]) {
                filtered.push(items[item]);
                filteredLookUp[items[item].name] = items[item];
            }
            else {
                //Atributo - Indice
                if (items[item].type == 2) {
                    var add = true;
                    var iToFor = (typeof filteredLookUp[items[item].name].parentName === "object") ? filteredLookUp[items[item].name].parentName.length : 1;
                    for (var i = 0; i <= iToFor - 1; i++) {
                        if (filteredLookUp[items[item].name].parentName == undefined)
                            add = false;
                        else
                            if (filteredLookUp[items[item].name].parentName[i] == items[item].name)
                                add = false;
                    }
                    if (add) {
                        var entObj = {};
                        entObj.id = items[item].parent;
                        entObj.name = items[item].parentName;
                        filteredLookUp[items[item].name].parentArray.push(entObj);
                    }
                }
            }
        }

        var fBtn = $("body").data('filterBtn');
        if (fBtn != 'entity' && fBtn != 'index') {
            var inputTxt = inputGSTxt();
            if (inputTxt.length >= 3) {
                var result = GetGSSuggestionWords();
                if (result != undefined) {
                    for (var i = 0; i <= result.length - 1; i++) {
                        result[i].type = 0;
                        result[i].name = result[i].word;
                        filtered.push(result[i]);
                    }
                }
            }
        }
        if (filtered.length == 1) {
            filtered[0].noresults = true;
        }
        filtered[0].total = filtered.length - 1;
        return filtered;
    }
})

app.filter('showChilds', function () {
    //filtro que agrega los indices de cada entidad seleccionada a la lista
    return function (items, parameters) {
        // window.wait();
        var filtered = [];
        var fBtn = $("body").data('filterBtn');
        for (var item in items) {
            var entity = items[item];
            //Agrego Entidad
            var addE = '';
            for (var p in parameters) {
                if (parameters[p].type == 1 && parameters[p].id == items[item].id) {
                    addE = items[item].id;
                    break;
                }
            }
            //si es numero no agrega - si ya esta seleccionado la entidad
            if (addE == '' && fBtn != 'words' && fBtn != 'index')
                filtered.push(entity);

            if (fBtn != 'words' && fBtn != 'entity') {
                for (var index in entity.indexes) {
                    //Agrego atributos
                    var ent = entity.indexes[index];
                    var addA = true;
                    for (var p in parameters) {
                        if (parameters[p].type == 2 && parameters[p].id == ent.id) {
                            addA = false;
                        }
                    }
                    if (addA) {
                        var entObj = {
                        };
                        entObj.id = entity.id;
                        entObj.name = entity.name;

                        ent.parentArray = [];
                        ent.parentArray.push(entObj);
                        ent.parentName = entity.name;
                        filtered.push(ent);
                    }
                }
            }
        }
        // window.ready();           
        return filtered;
    }
});

angular.element(document).ready(function () {
    //angular.bootstrap(document.getElementById("Advfilter1"), ["zamba-search"]);
    //angular.bootstrap(document.getElementById("Advfilter2"), ["zamba-search"]);
    //angular.bootstrap(document.getElementById("Advfilter3"), ["zamba-search"]);

    //var todoRootNode = jQuery('[ng-controller=appController]');
    // angular.bootstrap(todoRootNode, ['Advfilter1']);
    // angular.bootstrap(todoRootNode, ['Advfilter2']);
    // angular.bootstrap(todoRootNode, ['Advfilter3']);
});

//})();

window.load = function () {
    if ($('#loadingModZmb').hasClass('in')) return;

    //  waitingDialog.show('Cargando');
    //GSLoading.Show('Cargando...');
    //bottom: 0;
    //position: fixed;
    //width: 500px;
    //right: 0;
    //left: auto!important;
}

window.wait = function () {
    if ($('#loadingModZmb').hasClass('in')) return;
    GSLoading.Show('Procesando su busqueda');
    //waitingDialog.show('Procesando su busqueda');
}

window.ready = function () {
    //  e.stopPropagation(); //This line would take care of it
    if (typeof (zambaApplication) != "undefined" && zambaApplication == "ZambaQuickSearch" && $("#advSearchSave").length)
        $("#advSearchSave").remove();
    GSLoading.Hide();
    //waitingDialog.hide();
    var $m = $('#loadingModZmb');
    if ($m.hasClass('in')) {
        $m.removeClass("in");
        $(".modal-backdrop").hide();
        $m.hide();
    }
}

function RemSP(p, index, all) {
    //if (all || p[index].maingroup || typeof (p[index].id) == "undefined") { //Busqueda por palabras sin Id             
    //    var groupnum = $("#sel" + p[index].id).attr("colorgroup");
    //    var del = 0;
    //    var pl = p.length;
    //    for (var i = 0; i <= pl - 1; i++) {
    //        var $item = $("#sel" + p[i + del].id + '.' + p[i + del].color);
    //        if ($item.attr("colorgroup") == groupnum) {
    //            p.splice(i + del, 1);
    //            del--;
    //        }
    //    }
    //}
    //else {
    p.splice(index, 1);
    //}
}

//ViewMODEL
function FilterObj(id, name, value, type, groupnum, maingroup) {
    this.id = id;
    this.name = name;
    this.value = value;
    var t = '';
    switch (type) {
        case "etapa":
            t = 5;
            break;
        case "estado":
            t = 6;
            break;
        case "asignado":
            t = 7;
            break;
    }
    this.type = t;
    this.groupnum = groupnum;
    this.maingroup = maingroup;
}

function advIdtoStr(id) {
    switch (id) {
        case 5:
            return "Etapa";
        case 6:
            return "Estado";
        case 7:
            return "Asignado";
    }
}

function BuildTH(th, data) {
    th.typeahead("destroy");
    var bh = new Bloodhound({
        local: data,
        datumTokenizer: function (d) {
            var test = Bloodhound.tokenizers.whitespace(d);
            $.each(test, function (k, v) {
                i = 0;
                while ((i + 1) < v.length) {
                    test.push(v.substr(i, v.length));
                    i++;
                }
            })
            return test;
        },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
    });

    bh.initialize();
    th.typeahead({
        hint: true,
        highlight: true,
        minLength: 1
    },
        {
            source: bh
        });
    th.parent().attr("style", "")
        .children("input").attr("style", "");
    th.parent().children("div.tt-menu")
        .css({
            "left": "350px", "top": "auto", "margin-top": "18px"
        });

    var strLength = th.val().length * 2;
    th.focus();
    th[0].setSelectionRange(strLength, strLength);
}

function showGSHelp() {
    bootbox.dialog({
        message:
        "<b>Caracteristicas</b><hr/>" +
        "<i>  Zamba Global Search</i> permite realizar busquedas de toda la informacion de su, sin importar su origen o ubicacion, permitiendo indexar la informacion del usuario y de la organizacion, haciendola accesible desde un unico lugar." +
        " Su interfaz simple y atractiva consta de una caja de texto inteligente programada para arrojar resultados a medida que  se va introduciendo texto en ella. En la misma se podra buscar cualquier informacion o dato particular contenido en cualquier documento, atributo o tarea, entre otros, dentro de Zamba o de todas las aplicaciones que  se hayan vinculado al motor de indexacion.<hr/>" +
        "<b>Modo de uso</b><hr/>" +
        "  Ingrese su busqueda dentro de la caja de texto inteligente, ya sea un indice, entidad, palabra, etc. El buscador arrojara el resultado cargando una grilla debajo de la caja de texto si es que encontro algun resultado.<br/> " +
        "Si desea continuar filtrando los resultados, puede continuar colocando filtros al ya aplicado volviendo a introducir un nuevo parametro a buscar, esto hara que se ejecute una nueva busqueda y se actualizaran los datos en la grilla. Tambien puede combinar y agrupar por indices, atributos y palabras en comun." +
        "<hr/><i><span class='glyphicon glyphicon-book'></span> Para mas informacion <a href='http://www.zambabpm.com.ar/Zamba/Zamba%20Global%20Search%20v1.docx'>Zamba Global Search Manual</a> <i/>",
        title: "<span class='glyphicon glyphicon-filter'> </span> Zamba Advanced Search",
        draggable: true,
        buttons: {
            success: {
                label: "Aceptar",
                className: "btn-success",
            }
        }
    });
    $(".bootbox.modal").draggable();
}

function infoMsg(msg) {
    for (var i = 0; i <= msg.length - 1; i++)
        toastr.warning(msg);
    iMsgTxt = [];
}

function noResultsMsg() {
    // hideLoading();
    $("#resultsGridSearchBox").css("height", "50px");
    toastr.warning("Por favor intente redefiniendo sus parametros de busqueda", "No se encontro ningun resultado");
}

function advFilterSelect(filSel) {
    if ($(filSel).attr('class') == "btn btn-default") {// active btnSelected         
        //window.wait();
        var $btn = $($(".btn.btn-default.active.btnSelected")[0]);
        $btn.removeClass("btn btn-default active btnSelected");
        $btn.addClass("btn btn-default");

        $(filSel).removeClass("btn btn-default");
        $(filSel).addClass("btn btn-default active btnSelected");

        $("body").data("filterBtn", $(filSel).children('input').attr("value").toLowerCase());

        var $input = $(".search-parameter-input").eq(0);
        var inputTxt = $input.val();
        $input.val('').trigger("input");
        $input.eq(0).val(inputTxt).trigger("input");
        //window.ready();
    }
}

function openGSAtr(_this, e, type) {
    e.stopPropagation();
    e.preventDefault();
    var $cont;
    var $ul;
    if (type == 'entity') {
        $cont = $(_this).parents(".rowEntity").children(".btnMore,.ulAttr,.footerTxt,.entULFilter,.searchEntInsideUL");
        $ul = $(_this).parents(".rowEntity").children(".ulAttr");
    }
    else {
        $cont = $(_this).parents(".rowIndex").children(".btnMore,.ulAttr,.footerTxt,.chkAllNone");
        $ul = $(_this).parents(".rowIndex").children(".ulAttr");
    }
    var $childrenLi = $ul.children("li");
    $childrenLi.css({
        width: "150px"
    });
    var liHeight = ($childrenLi.length * 20) + 95;
    if (liHeight > 180) liHeight = 206;
    var display = $ul.css("display") == "none" ? "block" : "none";

    //Reestablezco tamaños
    $(_this).parents(".dropdown-menu").find(".btnMore,.ulAttr,.footerTxt,#entULFilter,.entULFilter,.searchEntInsideUL").css("display", "none");
    $(_this).parents(".dropdown-menu").children("li:not(:first-child)").css({
        "height": "75px", "overflow": "hidden"
    });

    $cont.css("display", display);

    if (display == "block") {
        $(_this).parents("li").css({ height: liHeight + "px", "overflow-y": "overlay" });
        if (type == 'entity') $(_this).parents(".rowEntity").children(".entULFilter,.searchEntInsideUL").css("display", "inline");
    }
    else {
        $(_this).parents("li").css({
            height: "75px", "overflow-y": "hidden"
        });
    }
}

function selectDescGSFn($p) {
    var entN = 0;
    var indN = 0;
    var str = "";//= "<u>Se buscara:</u> "
    for (var p = 0; p <= $p.length - 1; p++) {
        if ($p[p].type == 1) {
            entN++;
            if (entN == 1)
                //    str += " En la entidad <b>'" + $p[p].name + "'</b>,";//
                //else
                str += " <b>'" + $p[p].name + "'</b>";
            if ($p[p].value2 != "" && isDate($p[p].value2)) {
                str += " con fecha entre: <b>'" + $p[p].value + "'</b> y <b>'" + $p[p].value2 + "'</b>";// el texto
            }
            else {
                if ($p[p].value != "") {
                    str += " = <b>'" + $p[p].value + "'</b>";//conteniendo el texto:
                }
            }
        }
    }
    for (var p = 0; p <= $p.length - 1; p++) {
        if ($p[p].type == 2) {
            indN++;
            str += " Con indice <b>'" + $p[p].name + "'</b>,";
            if ($p[p].value2 != "" && isDate($p[p].value2)) {
                str += " con fecha entre: <b>'" + $p[p].value + "'</b> y <b>'" + $p[p].value2 + "'</b>";// el texto
            }
            else {
                if ($p[p].value != "") {
                    str += " que contenga: <b>'" + $p[p].value + "'</b>";// el texto
                }
            }
        }
    }
    if (indN == 0) str += " dentro de todos los indices";
    for (var p = 0; p <= $p.length - 1; p++) {
        if ($p[p].type == 0) {
            if ($p[p].value != "") {
                str += ". Incluyendo: <b>'" + $p[p].value + "'</b>";// la palabra
            }
        }
    }
    $("#selectDescGS").data("desc", str);
    $("#Advfilter-modal-content").find(".GSTxtInModalHeader").html(str);
}

function showGSConfig() {
    var $confDiv = $(".filterCount.dropdown");
    if ($confDiv.css("display") == "none") {
        $confDiv.css("display", "inline");
    }
    else {
        $confDiv.css("display", "none");
    }
}

function showDescGsFn() {
    var msg = $("#selectDescGS").data("desc");
    if (msg != undefined && msg.length) {
        bootbox.dialog({
            message: msg,
            title: "<span class='glyphicon glyphicon-eye-open'> </span> Busqueda actual",
            draggable: true,
            buttons: {
                success: {
                    label: "Aceptar",
                    className: "btn-success",
                }
            }
        });
        $(".bootbox.modal").draggable();
    }
}



function showCompareDateGS(_this, type, e) {
    if (e != undefined)
        e.stopPropagation();
    var $dC = $(_this).parent().children(".dateCompare");
    if (type == "entity") {
        if ($(_this).find("input").is(':checked')) {
            $dC.css({
                "visibility": "visible", "margin-left": "-15px"
            });
            $(_this).parent().css("margin-left", "-30px");
            $(_this).parent().find("input").css("width", "70px");
            $(_this).parent().find(".dateCompareChk>input").css("margin-left", "-15px");
        }
        else
            $dC.css("visibility", "hidden");
    }
    else {//Index
        if ($(_this).find("input").is(':checked')) {
            $dC.css({
                "visibility": "visible", "display": "block", "margin-top": "-10px"
            });
            $(_this).parent().find(".dateCompareChk>input").css("margin-right", "10px");
        }
        else
            $dC.css("visibility", "hidden");
    }
}

function inputGSTxt() {
    return $(".search-parameter-input").val();
}

function GetGSSuggestionWords() {
    var words = null;
    var inputTxt = inputGSTxt();
    $.ajax({
        type: "GET",
        async: false,
        data: {
            text: inputTxt
        },
        url: ZambaWebRestApiURL + '/Search/Suggestions',
        success: function (result) {
            if (result != undefined) {
                words = removeDuplicates(result, "word");
            }
        },
    });
    return words;
}

function removeDuplicates(originalArray, prop) {
    var newArray = [];
    var lookupObject = {
    };

    for (var i in originalArray) {
        lookupObject[originalArray[i][prop]] = originalArray[i];
    }

    for (i in lookupObject) {
        newArray.push(lookupObject[i]);
    }
    return newArray;
}

function isDate(val) {
    if (!Date.ddmm)//Esta en español la fecha
        val = esToEnDate(val);
    var d = new Date(val);
    return !isNaN(d.valueOf());
}

function esToEnDate(d) {
    var s = (d.indexOf("/")) ? "/" : "-";
    var day = d.substring(0, d.indexOf(s));
    var m = d.substring(0, d.lastIndexOf(s)).substring(day.length + 1);
    var y = d.substring(d.length - 2);
    return m + s + day + s + y;
}

Date.ddmm = (function () {
    return Date.parse('2/6/2009') > Date.parse('6/2/2009');
})()

    //function HideGBModal(_this) {
    //    showSeletionModeByimage(_this);
    //    var isGlobalSearch = false;
    //    if ($(_this).parents("#Advfilter1").length) isGlobalSearch = true;

    //    if (isGlobalSearch && zambaApplication != "ZambaSearch") {
    //        $('#Advfilter-modal-content').slideToggle();
    //        $('#Advfilter2').fadeIn().css('display', 'inline-flex');
    //        $('.favAdvSearch').fadeIn();
    //        $('#Advfilter2').children('.advancedSearchBox').css({
    //            display: 'block', width: 'auto'
    //        });
    //    }

    //}

    //Scroll directive
    ; (function (window, _, $, angular, undefined) {
        var module = angular.module("app");
        module.directive("onScroll", [function () {
            var previousScroll = 0;
            var link = function ($scope, $element, attrs) {
                $element.bind('scroll', function (evt) {
                    var currentScroll = $element.scrollTop();
                    $scope.$eval(attrs["onScroll"], {
                        $event: evt, $direct: currentScroll > previousScroll ? 1 : -1
                    });
                    previousScroll = currentScroll;
                });
            };
            return {
                restrict: "A",
                link: link
            };
        }]);
    })(window, _, jQuery, angular);

// Evento cuando se cambia tamaño de la ventana.
$(window).on("resize", function () {
    setTabSearchSize();
    ResizeResultsArea();
});

// Muestra u oculta la grilla correspondiente.
function visualizerModeGSFn(_this, mode) {
    var isGlobalSearch = false;
    if ($(_this).parents("#Advfilter1").length) isGlobalSearch = true;
        
    $("#resultsGridSearchBox").hide();
    $("#resultsGridSearchBoxThumbs").hide();
    $("#resultsGridSearchBoxPreview").hide();
    $("#resultsGridSearchGrid").hide(); 
    $("#Kgrid").hide(); 
    $("#multipleSelectionMenu").find(".activeButtonIconBar").click();
    $("#multipleSelectionPreview").find(".activeButtonIconBar").click(); 
    if ($("#chkThumbGrid").hasClass("ng-not-empty")) {
        $("#chkThumbGrid").click();
    }
    $(".filterFunc").show();

    $(".glyphicon.ngtitle.glyphicon-ok-circle").css("background-color", "#4285f4");   
    DisableActions();
    $(".switch").hide();
    switch (mode) {
        case "grid":
            $("#resultsGridSearchGrid").show();
            $("#Kgrid").show(); 
            $(".btn-preview").removeClass("BtnGridStyle");
            $(".btn-thumb").removeClass("BtnGridStyle");
            $(".btn-grid").addClass("BtnGridStyle");
            ResizeResultsArea();
            $(".switch").show();
            break;
        case "preview":
            $("#resultsGridSearchGrid").show();
            $("#resultsGridSearchBoxPreview").show();
            $(".btn-thumb").removeClass("BtnGridStyle");
            $(".btn-grid").removeClass("BtnGridStyle");
            $(".btn-preview").addClass("BtnGridStyle");
            ResizeResultsArea();
            break;
        case "list":
            $("#resultsGridSearchGrid").show();
            $("#resultsGridSearchBox").show();
            ResizeResultsArea();
            break;
        case "thumbs":
            $("#resultsGridSearchGrid").show();
            $("#resultsGridSearchBoxThumbs").show();
            $(".btn-preview").removeClass("BtnGridStyle");
            $(".btn-grid").removeClass("BtnGridStyle");
            $(".btn-thumb").addClass("BtnGridStyle");
            ResizeResultsArea();
            break;
    }
        AdjustGridColumns();
}

function DisableActions() {
    try {
      //  document.getElementById("BtnClearCheckbox").setAttribute("disabled", "disabled");
        document.getElementById("BtnSendEmail").setAttribute("disabled", "disabled");
        document.getElementById("OpenAllSelected").setAttribute("disabled", "disabled");
        document.getElementById("BtnSendZip").setAttribute("disabled", "disabled");
        document.getElementById("BtnDerivar").setAttribute("disabled", "disabled");
        document.getElementById("panel_ruleActions").setAttribute("disabled", "disabled");

        $("#Actions").css('display', 'none');
    } catch (e) {
        console.log("ERROR: " + e.messages);
    }
}

// Da tamaño al area de busqueda por atributos.
function setTabSearchSize() {

    var MasterHeaderHeight = $('#MasterHeader').outerHeight(true);
    var sbcHeight = $('#GlobalSearchNavBar').length ? $('#Advfilter1').outerHeight(false) : 90;
    $('#SearchControl').css("height", (window.innerHeight - MasterHeaderHeight - sbcHeight) + "px");
    setIndexsPanelSize();
}

// Da tamaño al panel de indices en busqueda por atributos.
function setIndexsPanelSize() {
    var SearchControl = $('#SearchControl').outerHeight(false);
    var topButtons = $('#barratop').outerHeight(true);
    $('#dvDocTypesIndexs').css("height", (SearchControl - topButtons) + "px");
}

// Da tamaño al area de resultados.
function ResizeResultsArea() {
    var MasterHeaderHeight = $('#MasterHeader').outerHeight(false);
    var sbcHeight = $('#GlobalSearchNavBar').outerHeight(true);
    $('results-grid').css("height", (window.innerHeight - MasterHeaderHeight - sbcHeight) + "px");
    AdjustResultHeight();
}

// Ajusta el alto de los elementos dentro del area de resultados.
function AdjustResultHeight() {
    var resultAreaHeight = $('results-grid').outerHeight(false);
    var visualButtonsBarHeight = $('#ToolbarResults').outerHeight(false);
    var searchinfoHeight = $('#searchinfo').outerHeight(false);
    var resultsGridActionsHeight = $('#resultsGridActions').outerHeight(true);
    //var KendoGridButtons = $('#KendoGridButtons').outerHeight(true);
    var newHeight = (resultAreaHeight - visualButtonsBarHeight - searchinfoHeight - resultsGridActionsHeight) + "px"
    $("#Kgrid").css("height", newHeight);
    $("#resultsGridSearchBoxPreview").css("height", newHeight);
    $("#resultsGridSearchBox").css("height", newHeight);
    $("#resultsGridSearchBoxThumbs").css("height", newHeight);
  // toastr.info(newHeight);
    resizeGrid();
}


//References
// <img class="imgFlt fimgword" ng-show="searchParam.type == 0">
//<img class="imgFlt fimgent" ng-show="searchParam.type == 1">
//<img class="imgFlt fimgind" ng-show="searchParam.type == 2">
//<img class="imgFlt fimgeta" ng-show="searchParam.type == 5">
//<img class="imgFlt fimgest" ng-show="searchParam.type == 6">
//<img class="imgFlt fimgasi" ng-show="searchParam.type == 7">

//<!--Numerico (1.2)-->
//          <!--Moneda y Decimal (3.6)-->
//          <!--Fecha - Fecha y Hora (4.5)-->
//          <!--Alfanumerico (7.8)-->
// 9 Checkbox true-false











