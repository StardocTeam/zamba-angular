
//var urlGlobalSearch = 'http://localhost/GlobalSearch/api/Search/';
var urlGlobalSearch = 'http://localhost:49864/api/Search/';

var searchResults;
var iMsgTxt = [];

(function () {

    'use strict';

    angular.module('zamba-search', []).directive('zambaSearch', function () {
        return {
            restrict: 'E',
            scope: {
                model: '=ngModel',
                parameters: '='
            },
            replace: true,
            templateUrl: '../../GlobalSearch/search/searchbox.html',
            controller: [
                '$scope', '$http', '$attrs', '$element', '$timeout', '$filter',
                function ($scope, $http, $attrs, $element, $timeout, $filter) {

                    $scope.placeholder = $attrs.placeholder || 'Search ...';

                    $scope.searchQuery = '';

                    $scope.message = '';

                    $scope.setSearchFocus = false;

                    $scope.doSearch = function () {
                        //var parameterObj;
                        var $p = $scope.model.parameters;
                        for (var i = 0; i <= $p.length - 1; i++) {
                            if ($p[i] != undefined && $p[i].type == 1 && $p[i].value != '' && $p[i].value.indexOf('| Contiene: ') != 1) {
                                $p[i].value = $p[i].value.replace('| Contiene: ', '');
                            }
                        }
                        $http({
                            method: 'POST',
                            url: urlGlobalSearch + 'Results',
                            crossDomain: true, // enable this
                            dataType: 'jsonp',
                            // headers: { 'Content-Type': 'application/json', 'Access-Control-Allow-Origin:': '*' },
                            data: $p// parameterObj
                        }).
                        success(function (data, status, headers, config) {
                            if (data != null && data != undefined)
                                window.searchResults(data);
                        }).
                        error(function (data, status, headers, config) {
                            $scope.message = data;
                        });
                    };

                    $scope.enterEditMode = function (index) {                       
                        if (index === undefined)
                            return;

                        var searchParam = $scope.model.parameters[index];
                        if (searchParam.type == 2) {
                            searchParam.editMode = true;
                        }
                    };

                    $scope.leaveEditMode = function (index) {
                        if (index === undefined)
                            return;

                        var searchParam = $scope.model.parameters[index];
                        searchParam.editMode = false;

                        if (!searchParam.value && searchParam.parent)
                            $scope.removeSearchParam(index);
                        //Al cambiar el valor del input que actualice la grilla de datos
                        $scope.doSearch();
                    };

                    $scope.typeaheadOnSelect = function (item, model, label, event) {
                        var cn = $("body").data("colorNum");
                        if (cn == undefined || cn == 10)
                            cn = 1;
                        else
                            cn += 1;
                        $("body").data("colorNum", cn);
                        var color = 'b' + cn;

                        var groupnum = $("body").data("colorGroup");
                        $("body").data("colorGroup", groupnum == undefined ? 1 : groupnum + 1);
                        groupnum = $("body").data("colorGroup");
                        item.color = color;
                        item.groupnum = groupnum;

                        switch (item.type) {
                            case 1:
                                //seleccion de entidad
                                var $item = $("#item" + item.id);
                                var $filter = $item.children(".twitter-typeahead").children('.typeahead.thMain.tt-input');
                                if (!$filter.length) $filter = $item.children(".typeahead");

                                if ($filter.length && $filter.val().length >= 1) {
                                    item.maingroup = true;
                                    $scope.addSearchParam(item, "| Contiene: " + $filter.val());
                                }
                                else {
                                    item.maingroup = true;
                                    $scope.addSearchParam(item);
                                }

                                var $li = $item.children("ul").children("li");
                                for (var i = 0; i <= $li.length - 1; i++) {
                                    var $input = $($li[i]).children("input");
                                    if (!$input.length) $input = $($li[i]).children("span").children(".typeahead.th.tt-input");
                                    if ($input.css("display") != 'none' && $input.val().length >= 1) {
                                        for (var j = 0; j <= item.indexes.length - 1; j++) {
                                            if ($input.parent().attr("id") == item.indexes[j].id) {
                                                item.indexes[j].color = color;
                                                item.indexes[j].groupnum = groupnum;
                                                item.indexes[j].maingroup = false;
                                                $scope.addSearchParam(item.indexes[j], $input.val());
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2:
                                //seleccion de atributo-indice
                                var txt = $("#index" + item.id).children(".typeahead").length != 0 ? $("#index" + item.id).children(".typeahead").val() :
                                    $("#index" + item.id).children(".twitter-typeahead").children(".typeahead.thMain.tt-input").val();
                                if (txt == undefined || txt == '')
                                    iMsgTxt.push("Por favor ingrese un valor al atributo: " + item.name);

                                item.maingroup = true;
                                $scope.addSearchParam(item, txt.length ? txt : null);

                                if (item.parentArray.length) {
                                    var $li = $("#index" + item.id).children("ul").children("li");
                                    for (var i in $scope.parameters) {
                                        for (var j = 1; j <= $li.length - 1; j++) {
                                            var $chk = $($li[j].children[0].children[0]);
                                            if ($chk.attr("value") == $scope.parameters[i].id && $chk.is(':checked')) {
                                                $scope.parameters[i].color = color;
                                                $scope.parameters[i].groupnum = groupnum;
                                                $scope.parameters[i].maingroup = false;
                                                $scope.addSearchParam($scope.parameters[i]);
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            case 0:
                                //seleccion de palabra   
                                item.maingroup = true;
                                $scope.addSearchParam(item, item.name);
                                break;
                        }
                        //Armo Advanced filter: etapa, asignado, estado
                        if (item.type == 1 || item.type == 2) {
                            var $a = item.type == 1 ? $("#item" + item.id) : $("#index" + item.id);
                            var $div = $a.children("ul").children(".advFilter").children("div");
                            for (var i = 0; i <= $div.length - 1; i++) {
                                var $inp = $($div[i]).children('input');
                                if (!$inp.length) $inp = $($div[i]).children("span").children(".tt-input");

                                if ($inp.css("display") != 'none' && $inp.val() != '') {
                                    var w = $inp.data("word");
                                    if (w != undefined && w.length) {
                                        for (var j = 0; j <= w.length - 1; j++) {
                                            if ($inp.val().toUpperCase() == w[j].toUpperCase()) {
                                                var name = $($div[i]).children('button').attr("id");
                                                var p = new FilterObj($inp.data("ids")[j], (name.charAt(0).toUpperCase() + name.slice(1)), $inp.val(), name, groupnum, false);
                                                p.color = color;
                                                $scope.addSearchParam(p, $inp.val());
                                                break;
                                            }
                                        }
                                    }
                                    else
                                        iMsgTxt.push("Por favor verifique el valor ingresado en: " + $($div[i]).children("button").attr("id"));                                        
                                }
                            }
                        }
                        infoMsg(iMsgTxt);
                        $scope.model.query = '';
                        $scope.doSearch();
                    };

                    $scope.addSearchParam = function (searchParam, value, enterEditMode) {
                        //Establesco si es editable
                        if (enterEditMode === undefined)
                            //Solo indice, no palabra ni entidad
                            if (searchParam.parent && !searchParam.parentindex)
                                enterEditMode = true;
                            else
                                enterEditMode = false;

                        if (searchParam.placeholder === undefined)
                            searchParam.placeholder = '';

                        $scope.model.parameters.push({
                            id: searchParam.id,
                            name: searchParam.name,
                            placeholder: searchParam.placeholder,
                            value: value || '',
                            editMode: enterEditMode,
                            type: searchParam.type,
                            color: searchParam.color,
                            groupnum: searchParam.groupnum,
                            maingroup: searchParam.maingroup
                        });
                    };

                    $scope.removeSearchParam = function (index) {
                        if (index === undefined)
                            return;
                        RemSP($scope.model.parameters, index);
                    };
                    //Arma y guarda parametros para despues ser visualizados nuevamente en la lista desplegable
                    $scope.editgroup = function (i) {
                        if (i === undefined)
                            return;
                        event.stopPropagation();
                        var p = $scope.model.parameters;
                        var filters = [];
                        var filtersVal = [];
                        var $div = $(".search-parameter." + p[i].color);

                        var typeTxt;
                        switch (p[i].type) {
                            case 0:
                                typeTxt = "words";
                                var f = { name: p[i].name, value: p[i].value };
                                break;
                            case 1:
                                typeTxt = "entity";
                                for (var j = 0; j <= $div.length - 1; j++) {
                                    if ($($div[j]).attr("colorgroup") == p[i].groupnum && $($div[j]).attr("class").indexOf(typeTxt) == -1) {
                                        filters.push($($div[j]).attr("id").replace("sel", ''));
                                        filtersVal.push($($div[j]).children(".value").text().trim());
                                    }
                                }
                                var value = p[i].value.replace("| Contiene: ", "");
                                var f = { id: p[i].id, name: p[i].name, value: value, type: p[i].type, filter: filters, filtersVal: filtersVal };
                                break;
                            case 2:
                                typeTxt = "index";
                                for (var j = 0; j <= $div.length - 1; j++) {
                                    if ($($div[j]).attr("colorgroup") == p[i].groupnum) {
                                        filters.push($($div[j]).attr("id").replace("sel", ''));
                                    }
                                }
                                var f = { id: p[i].id, name: p[i].name, value: p[i].value, type: p[i].type, filter: filters };
                                break;
                        }

                        for (var j = 0; j <= p.length - 1; j++) {
                            switch (p[j].type) {
                                case 5:
                                    f.etapa = p[j].value;
                                    f.etapaId = p[j].id;
                                    break;
                                case 6:
                                    f.estado = p[j].value;
                                    f.estadoId = p[j].id;
                                    break;
                                case 7:
                                    f.asignado = p[j].value;
                                    f.asignadoId = p[j].id;
                                    break;
                            }

                        }
                        $("body").data("filterBtn", typeTxt);
                        $("body").data("filters", f);
                        //Remueve los search parameters, los guarda en body-data para mostrar las mismas sugerencias
                        RemSP(p, i);
                    };

                    $scope.removeAll = function () {
                        $scope.model.parameters.length = 0;
                        $scope.model.query = '';
                    };
                    $scope.showHelp = function () {
                        showHelp();
                    };

                    $scope.editPrevious = function (currentindex) {
                        var i;
                        if (currentindex !== undefined) {
                            $scope.leaveEditMode(currentindex);
                            $scope.setSearchFocus = true;
                        }

                        //TODO: check if index == 0 -> what then?
                        if (currentindex > 0) {
                            $scope.enterEditMode(currentindex - 1);
                        } else if ($scope.model.parameters.length > 0) {
                            $scope.enterEditMode($scope.model.parameters.length - 1);
                        }
                    };

                    $scope.editNext = function (currentindex) {
                        if (currentindex === undefined)
                            return;

                        $scope.leaveEditMode(currentindex);

                        //TODO: check if index == array length - 1 -> what then?
                        if (currentindex < $scope.model.parameters.length - 1) {
                            $scope.enterEditMode(currentindex + 1);
                        } else {
                            $scope.setSearchFocus = true;
                        }
                    };

                    $scope.keydown = function (e, searchParamindex) {
                        var handledKeys = [8, 9, 13, 37, 39];
                        if (handledKeys.indexOf(e.which) === -1)
                            return;

                        var cursorPosition = getCurrentCaretPosition(e.target);

                        if (e.which == 8) { // backspace
                            if (cursorPosition === 0) {
                                if (searchParamindex !== undefined && $scope.model.parameters[searchParamindex].value.length === 0) {
                                    e.preventDefault();
                                    $scope.model.parameters.splice(searchParamindex, 1);
                                    $scope.setSearchFocus = true;
                                } else if ($scope.model.query.length === 0) {
                                    $scope.model.parameters.pop();
                                }
                            }

                        } else if (e.which == 9) { // tab
                            if (e.shiftKey) {
                                e.preventDefault();
                                $scope.editPrevious(searchParamindex);
                            } else {
                                e.preventDefault();
                                $scope.editNext(searchParamindex);
                            }

                        } else if (e.which == 13) { // enter
                            if (searchParamindex !== undefined)
                                $scope.editNext(searchParamindex);

                        } else if (e.which == 37) { // left
                            if (cursorPosition === 0)
                                $scope.editPrevious(searchParamindex);

                        } else if (e.which == 39) { // right
                            if (cursorPosition === e.target.value.length)
                                $scope.editNext(searchParamindex);
                        }
                    };

                    if ($scope.model === undefined) {
                        $scope.model = {};
                        $scope.model.parameters = [];
                    }

                    function getCurrentCaretPosition(input) {
                        if (!input)
                            return 0;

                        // Firefox & co
                        if (typeof input.selectionStart === 'number') {
                            return input.selectionDirection === 'backward' ? input.selectionStart : input.selectionEnd;

                        } else if (document.selection) { // IE
                            input.focus();
                            var selection = document.selection.createRange();
                            var selectionLength = document.selection.createRange().text.length;
                            selection.moveStart('character', -input.value.length);
                            return selection.text.length - selectionLength;
                        }

                        return 0;
                    }
                }
            ]
        };
    })

    .directive('clickitem', [function () {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
                element.bind('click', function (e) {
                    e.stopPropagation();
                    var $th = $(this).children(".typeahead");
                    if ($th.css("display") == 'none') {
                        $th.css("display", 'flex');
                        $th.focus();
                        var id = $(this).attr('id');
                        $(this).attr('idselected', id);

                    }
                });
            }
        };
    }])

         .directive('clickchk', [function () {
             return {
                 restrict: 'A',
                 replace: true,
                 link: function (scope, element, attrs) {
                     element.bind('click', function (e) {
                         e.stopPropagation();
                         var elem = browserType() == 'chrome' ? e.toElement.tagName : e.target.tagName;
                         if (elem == "INPUT") {
                             var $li = $(this).parent().children("li");
                             var chk = 0;
                             var nochk = 0;
                             for (var i = 1; i <= $li.length - 1; i++) {
                                 if ($($li[i]).children("label").children("input").prop("checked"))
                                     chk += 1;
                                 else
                                     nochk += 1;
                             }
                             var $l = $($li[0]).children("label");
                             if (chk == $li.length - 1) {
                                 $l.children("span").text('Deseleccionar todo');
                                 $l.children('input').prop("checked", true);
                             }
                             if (nochk == $li.length - 1) {
                                 $l.children("span").text('Seleccionar todo');
                                 $l.children('input').prop("checked", false);
                             }
                         }
                     });
                 }
             };
         }])

    .directive('selbutton', [function () {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {

                element.bind('mouseenter', function (e) {
                    $(this).tooltip();
                });

                element.bind('click', function (e) {
                    e.stopPropagation();
                    if ($(e.currentTarget).attr('class') == "btn btn-default") {// active btnSelected         
                        //window.wait();
                        var $btn = $($(".btn.btn-default.active.btnSelected")[0]);
                        $btn.removeClass("btn btn-default active btnSelected");
                        $btn.addClass("btn btn-default");

                        $(this).removeClass("btn btn-default");
                        $(this).addClass("btn btn-default active btnSelected");

                        $("body").data("filterBtn", $(this).children('input').attr("value").toLowerCase());

                        var $input = $(".search-parameter-input").eq(0);
                        var inputTxt = $input.val();
                        $input.val('').trigger("input");
                        $input.eq(0).val(inputTxt).trigger("input");
                        //window.ready();
                    }
                });
            }
        };
    }])

    .directive('hideshowinput', [function () {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
                element.bind('mouseenter', function (e) {
                    //e.stopPropagation();
                    $(this).children(".typeahead").fadeIn().css("display", "inline-block");
                    $(this).children(".typeahead").focus();
                    $(this).tooltip();
                    var $btn = $(this).children('button');
                    if ($btn.text() == '+') {
                        var $li = $(this).children('ul').children("li");
                        var t = '';
                        var c = 0;
                        for (var i = 0; i <= $li.length - 1; i++) {
                            if ($($li[i]).attr("class").indexOf('ng-hide') != -1) {
                                var txt = $($li[i]).children('label').text();
                                t += (txt ? txt : $($li[i]).text().trim()) + '<br>';
                                c++;
                                if (c == 10) {
                                    t += "<br>Ver mas...";
                                    break;
                                }
                            }
                        }
                        $btn.attr("data-title", t);
                        $btn.tooltip();
                    }
                    else
                        $btn.removeAttr("data-title");
                });
                element.bind('mouseleave', function (e) {
                    // e.stopPropagation();
                    $(this).children(".typeahead").fadeOut();
                });
            }
        };
    }])

    .directive('chkboxcontrol', [function () {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
                element.bind('click', function (e) {
                    e.stopPropagation();

                    var elem = browserType() == 'chrome' ? e.toElement.tagName : e.target.tagName;
                    if (elem == "LABEL" || elem == "SPAN")
                        return;

                    var $chk = $(this).children().children("input");
                    var $li = $(this).parent().children("li");
                    for (var i = 0; i <= $li.length - 1; i++) {
                        if (i != 0)
                            $($li[i].children[0].children[0]).prop('checked', $chk.is(':checked'));
                    }
                    var $span = $(this).children().children("span");
                    $span.text($span.text() == 'Deseleccionar todo' ? 'Seleccionar todo' : "Deseleccionar todo");
                });
            }
        };
    }])

    .directive('wordsentindex', [function () {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
                element.bind('click', function (e) {
                    e.stopPropagation();
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
                        data: { index: at.toString(), entities: en.toString(), word: wo.toString() },
                        url: urlGlobalSearch + 'SuggestionsList',
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

    .directive('wordsentselect', [function () {
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
                        data: { index: 0, entities: id, word: word },
                        url: urlGlobalSearch + 'SuggestionsByindex',
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

    .directive('wordsattrselect', [function () {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
                element.bind('click', function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                }),

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
                    for (var i = 1; i <= $li.length - 1; i++) {
                        if ($($li[i].children[0].children[0]).is(':checked')) {
                            ent.push(parseInt($($li[i].children[0].children[0]).attr("value")));
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
                        data: { index: id, entities: ent.join(), word: word },
                        url: urlGlobalSearch + 'SuggestionsByindex',
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

    //.directive('wordsattribute', [function () {
    //    return {
    //        restrict: 'A',
    //        replace: true,
    //        link: function (scope, element, attrs) {
    //            element.bind('click', function (e) {
    //                e.stopPropagation();
    //                var $th = $(this).parent().children(".typeahead");
    //                if ($th.css("display") == 'none') {
    //                    $th.css("display", 'block');

    //                    var data = [];
    //                    var id = $(this).parent().parent().attr("id").replace('index', '');

    //                    $(this).remove();
    //                    $.ajax({
    //                        type: "GET",
    //                        async: false,
    //                        data: { entity: 0, index: id },
    //                        url: 'http://localhost:49864/api/search/Getindexwords',
    //                        success: function (result) {
    //                            for (var i = 0; i <= result.length - 1; i++)
    //                                data.push(result[i].word);
    //                        },
    //                    });
    //                    var bh = new Bloodhound({
    //                        local: data,
    //                        queryTokenizer: Bloodhound.tokenizers.whitespace,
    //                        datumTokenizer: Bloodhound.tokenizers.whitespace
    //                    });
    //                    $th.typeahead({
    //                        hint: true,
    //                        highlight: true,
    //                        minLength: 1
    //                    },
    //                    {
    //                        source: bh
    //                    });
    //                }
    //            });
    //        }
    //    };
    //}])

    .directive('expanditems', [function () {
        return {
            restrict: 'A',
            replace: true,
            link: function (scope, element, attrs) {
                element.bind('click', function (e) {
                    e.stopPropagation();
                    var show = $(this).html() == '+';
                    var display = show ? "flex" : "none";
                    $(this).html(show ? "-" : "+");
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

           .directive('showadvf', [function () {
               return {
                   restrict: 'A',
                   replace: true,
                   link: function (scope, element, attrs) {
                       element.bind('click', function (e) {
                           e.stopPropagation();
                           var elem = browserType() == 'chrome' ? e.toElement : e.target;
                           if (elem.tagName != "BUTTON") return;
                           var $btn = $(elem);
                           $btn.css("margin-bottom", "5px");
                           $btn.parent().children("input").css("display", "flex");
                           $btn.parent().children("input").focus();
                       });
                   }
               };
           }])

           .directive('advfilter', [function () {
               return {
                   restrict: 'A',
                   replace: true,
                   link: function (scope, element, attrs) {

                       element.bind('keydown', function (e) {
                           var nEvents = [46, 40, 30,8];
                           if (nEvents.indexOf(e.which) !=-1)
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
                               objData = { entity: $a.attr("id").replace('item', ''), entword: entw, index: at.toString(), word: wo.toString(), filter: btn, filterword: word };
                           }
                           else {
                               //ATRIBUTO
                               method = 'SuggestionsAdvInd';
                               for (var i = 1; i <= $li.length - 1; i++) {
                                   var $inp = $($li[i]).children("label").children("input");
                                   if ($inp.prop("checked"))
                                       en.push($inp.attr("value"));
                               }
                               objData = { index: $a.attr("id").replace('index', ''), indword: entw, entities: en.toString(), filter: btn, filterword: word };
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

    .directive('nitSetFocus', [
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

    .directive('nitAutoSizeInput', [
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

    .directive('ngEnter', function () {
        return function (scope, element, attrs) {
            replace: true,
            element.bind("keydown", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.ngEnter);
                    });

                    event.preventDefault();
                }
            });
        };
    })

        //Directiva para rearmar el grupo de búsqueda
            .directive('editgroup', function () {
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
                                        $in.children("input").val(f.filtersVal[cont]);
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
                                for (var i = 1; i <= $li.length - 1; i++) {
                                    var $in = $($li[i]).children().children("input");
                                    if ($.inArray($in.attr("value"), f.filter) == -1)
                                        $in.prop("checked", false);
                                }
                                if (f.value != undefined && f.value != '') {
                                    $("#index" + f.id).children("input").css("display", "inline");
                                    $("#index" + f.id).children("input").val(f.value);
                                }
                                break;
                        }

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

    .filter('uniqueName', function () {
        return function (items) {
            var filtered = [];
            var filteredLookUp = {};
            var item;
            //Para que coloque primero la barra de botones de sugerencias.
            var entObj = {};
            entObj.button = true;
            entObj.buttonVal = $("body").data('filterBtn') == undefined ? 'all' : $("body").data('filterBtn');
            filtered.push(entObj);

            for (item in items) {
                if (!filteredLookUp[items[item].name]) {
                    filtered.push(items[item]);
                    filteredLookUp[items[item].name] = items[item];
                }
                else {
                    //Atributo - Indice
                    if (items[item].type == 2) {
                        var add = true;
                        for (var i = 0; i <= filteredLookUp[items[item].name].parentName.length - 1; i++) {
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
                var inputTxt = $(".search-parameter-input").val();
                if (inputTxt.length >= 3) {
                    $.ajax({
                        type: "GET",
                        async: false,
                        data: { text: inputTxt },
                        url: urlGlobalSearch + 'Suggestions',
                        success: function (result) {
                            if (result != undefined) {
                                for (var i = 0; i <= result.length - 1; i++) {
                                    result[i].type = 0;
                                    result[i].name = result[i].Word;
                                    filtered.push(result[i]);
                                }
                            }
                        },
                    });
                }
            }
            if (filtered.length == 1) {
                filtered[0].noresults = true;
            }
            return filtered;
        }
    })

    .filter('showChilds', function () {
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
                            var entObj = {};
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
        angular.bootstrap(document.getElementById("Advfilter1"), ["zamba-search"]);
        angular.bootstrap(document.getElementById("Advfilter2"), ["zamba-search"]);
        angular.bootstrap(document.getElementById("Advfilter3"), ["zamba-search"]);

        //var todoRootNode = jQuery('[ng-controller=appController]');
        // angular.bootstrap(todoRootNode, ['Advfilter1']);
        // angular.bootstrap(todoRootNode, ['Advfilter2']);
        // angular.bootstrap(todoRootNode, ['Advfilter3']);
    });
})();



window.load = function () {
    waitingDialog.show('Cargando registros');
}
window.wait = function () {
    waitingDialog.show('Procesando su busqueda'); 
}
window.ready = function () {
    waitingDialog.hide();
}

function RemSP(p,index) {
    var groupnum = $("#sel" + p[index].id).attr("colorgroup");
    var del = 0;
    var pl = p.length;
    for (var i = 0; i <= pl - 1; i++) {
        var $item = $("#sel" + p[i + del].id + '.' + p[i + del].color);
        if ($item.attr("colorgroup") == groupnum) {
            p.splice(i + del, 1);
            del--;
        }
    }
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
    this.groupnum=groupnum;
    this.maingroup = maingroup;
}

function advIdtoStr(id) {
    switch (id) {
        case 5:
          return "Etapa";
            break;
        case 6:
            return "Estado";
            break;
        case 7:
            return "Asignado";
            break;
    }
}

function BuildTH(th,data) {
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
    var strLength = th.val().length * 2;
    th.focus();
    th[0].setSelectionRange(strLength, strLength);
}
function showHelp() {
    bootbox.dialog({
        message: "Instrucciones...",
        title: "Zamba Advanced Search",
        buttons: {
            success: {
                label: "Aceptar",
                className: "btn-success",
                callback: function () {
                   // Example.show("great success");
                }
            }        
        }
    });
}
function infoMsg(msg) {
    var text = '';
    for (var i = 0; i <= msg.length - 1; i++)    
        text += msg[i] + '<hr style="margin:5px">';
    
    iMsgTxt = [];
    $(".infoMsg").html(text).show().delay(5000).fadeOut();
}

function browserType(){
    var BrowserDetect = {
        init: function () {
            this.browser = this.searchString(this.dataBrowser) || "Other";
            this.version = this.searchVersion(navigator.userAgent) || this.searchVersion(navigator.appVersion) || "Unknown";
        },
        searchString: function (data) {
            for (var i = 0; i < data.length; i++) {
                var dataString = data[i].string;
                this.versionSearchString = data[i].subString;

                if (dataString.indexOf(data[i].subString) !== -1) {
                    return data[i].identity;
                }
            }
        },
        searchVersion: function (dataString) {
            var index = dataString.indexOf(this.versionSearchString);
            if (index === -1) {
                return;
            }
            var rv = dataString.indexOf("rv:");
            if (this.versionSearchString === "Trident" && rv !== -1) {
                return parseFloat(dataString.substring(rv + 3));
            } else {
                return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
            }
        },
        dataBrowser: [
            { string: navigator.userAgent, subString: "Edge", identity: "MS Edge" },
            { string: navigator.userAgent, subString: "MSIE", identity: "Explorer" },
            { string: navigator.userAgent, subString: "Trident", identity: "Explorer" },
            { string: navigator.userAgent, subString: "Firefox", identity: "Firefox" },
            { string: navigator.userAgent, subString: "Opera", identity: "Opera" },
            { string: navigator.userAgent, subString: "OPR", identity: "Opera" },
            { string: navigator.userAgent, subString: "Chrome", identity: "Chrome" },
            { string: navigator.userAgent, subString: "Safari", identity: "Safari" }
        ]
    };
    BrowserDetect.init();
    return BrowserDetect.browser.toLowerCase();
}

