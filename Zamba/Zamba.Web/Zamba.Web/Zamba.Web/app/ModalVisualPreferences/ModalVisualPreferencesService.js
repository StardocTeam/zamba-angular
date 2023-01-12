
'use strict';
var serviceBase = ZambaWebRestApiURL;

app.service('ModalVisualPreferencesService', function ($http) {

    var ModalVisualPreferencesService = {};

    var _SetDefaultMainMenuItem = function (userid, DefaultView) {
        try {
            if (window.localStorage) {
                window.localStorage.setItem("DefaultView|" + userid, DefaultView);
            }
            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: ZambaWebRestApiURL + '/Account/SetDefaultMainMenuItem?' + jQuery.param({ UserId: userid, View: DefaultView }),
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    console.log("vista agregada");
                    console.log(response);
                },
            });
        } catch (e) {
            console.error(e);
        }
    }

    var _GetDefaultMainMenuItem = function () {

        var DefaultView = null;
        if (window.localStorage) {
            DefaultView = window.localStorage.getItem("DefaultView|" + parseInt(GetUID()));
        }
        if (DefaultView == undefined || DefaultView == "undefined"  || DefaultView == null || DefaultView == '' || DefaultView == 'null' || DefaultView == "None") {

            $.ajax({
                type: 'POST',
                dataType: 'json',
                url: ZambaWebRestApiURL + '/Account/GetDefaultMainMenuItem?' + jQuery.param({ UserId: parseInt(GetUID()) }),
                contentType: "application/json; charset=utf-8",
                async: false,
                success: function (response) {
                    if (response != "" && response != undefined) {
                        //busco la vista configurada por el usuario,si la encuentra la seteo
                        DefaultView = response;
                        if (window.localStorage) {
                            window.localStorage.setItem("DefaultView|" + parseInt(GetUID()), DefaultView);
                        }
                    }

                    return DefaultView;
                },
            });
            return DefaultView;
        }
        else { return DefaultView; }
    };

    var _LoadLastMainMenuItem = function () {

        var userid = parseInt(GetUID());
        var DV = null;

        if (window.localStorage) {
            DV = window.localStorage.getItem("DV|" + userid);
        }

        if (DV != null && DV != '' && DV != "undefined" && DV != "None") {
            return DV;
        }

        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: ZambaWebRestApiURL + '/Account/GetLastMainMenuItem?' + jQuery.param({ UserId: userid }),
            contentType: "application/json; charset=utf-8",
            async: false,
            success: function (response) {
                if (response != "" && response != undefined) {
                    //busco la vista configurada por el usuario,si la encuentra la seteo
                    DV = response;
                    if (window.localStorage) {
                        window.localStorage.setItem("DV|" + userid, DV);
                    }
                    return DV;

                }
                else {
                    //seteo la pantalla de inicio por defecto
                    DV = 'search';
                    return DV;
                }
            },
        });
        return DV;

    }

    var _GetMainMenuItem = function () {
        var DV = null;
        
        var refreshFromChild = 'false';
        try {
            if (localStorage)
                refreshFromChild = localStorage.getItem('refreshFromChild');
        } catch (e) {
            console.error(e);
        }

        if (refreshFromChild == undefined || refreshFromChild == null || refreshFromChild != 'true') {
            if (localStorage)
                localStorage.setItem('refreshFromChild', 'false');

            DV = _GetDefaultMainMenuItem();
            if (DV != null && DV != '' && DV != 'null' && DV != "undefined" && DV != "None") {
                return DV;
            }
        }

        DV = _LoadLastMainMenuItem();
        if (DV != null && DV != '' && DV != 'null' && DV != "undefined" && DV != "None") {
            return DV;
        }

        return 'search';
    }

    var _SetLastMainMenuItem = function (view) {
        try {

            window.localStorage.setItem("DV|" + parseInt(GetUID()), view);
            var userid = parseInt(GetUID());
            $.ajax({
                type: 'POST',
                dataType: 'json',
                async: true,
                url: ZambaWebRestApiURL + '/Account/SetLastMainMenuItem?' + jQuery.param({ UserId: userid, View: view }),
                contentType: "application/json; charset=utf-8",
            });
        } catch (e) {
            console.error(e);
        }
    }
    ModalVisualPreferencesService.SetDefaultMainMenuItem = _SetDefaultMainMenuItem;
    ModalVisualPreferencesService.GetDefaultMainMenuItem = _GetDefaultMainMenuItem;

    ModalVisualPreferencesService.LoadLastMainMenuItem = _LoadLastMainMenuItem;
    ModalVisualPreferencesService.SetLastMainMenuItem = _SetLastMainMenuItem;

    ModalVisualPreferencesService.GetMainMenuItem = _GetMainMenuItem;

    return ModalVisualPreferencesService;

});