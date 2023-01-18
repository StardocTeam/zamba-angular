app.controller('HomeController', function ($scope, HomeService) {

    $scope.currentTabHome = 'HomeDash';
    $scope.Tabs = ['Acciones'];
    $scope.HomeTabsInitializing = true;
    $scope.init = function () {
        try {
            var d = HomeService.getResults();

            if (d == "") {
                console.log("No se pudo obtener los Tabs de la Home");
                $scope.Tabs = [];
            }
            else {
                $scope.Tabs = d;
            }

            $scope.HomeTabsInitializing = false;
            $scope.GetDefaultTabsHome();


        } catch (e) {
            console.error(e);
        }

    };


    //#region HOME TABS
    $scope.SetDefaultTabsHome = function (mode) {
        var userid = parseInt(GetUID());
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: ZambaWebRestApiURL + '/Account/SetDefaultTabsHome?' + jQuery.param({ UserId: userid, Mode: mode }),
            contentType: "application/json; charset=utf-8",
        });
    }


    $scope.GetDefaultTabsHome = function () {
        var userid = parseInt(GetUID());
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: ZambaWebRestApiURL + '/Account/GetDefaultTabsHome?' + jQuery.param({ UserId: userid }),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response != "") {
                    if (response == undefined || response == '' || (response != 'HomeMain' && response != 'HomeDash'))
                        response = 'HomeDash';

                    $scope.currentTabHome = response;
                    switch (response) {
                        case "HomeMain":
                            $("md-tab-item:contains('GESTION')").click();


                            break;
                        case "HomeDash":
                            $("md-tab-item:contains('INICIO')").click();
                            break;
                        //    case "HomeReports":
                        //        $("md-tab-item:contains('Reportes')").click();
                        //        break;
                        //    case "HomeCalendar":
                        //        $("md-tab-item:contains('Agenda')").click();
                        //        break;
                        //    case "HomeDiagrams":
                        //        $("md-tab-item:contains('Diagramas')").click();
                        //        try {
                        //            $scope.setDiagramsIframeUrl();
                        //        } catch (e) {

                        //        }
                        //        break;
                        default:
                            $("md-tab-item:contains('INICIO')").click();
                            break;
                    }
                    setTimeout(function () { $scope.timer_acomodar_Inicio(); }, 500)
                }
            },
        });
    }


    $scope.timer_acomodar_Inicio = function () {
        var InicioInterval = setInterval(function () {
            var inicio = $([name = 'TABGESTION'])[0]
            if (!(inicio == undefined)) {
                clearInterval(InicioInterval);
                $(inicio).removeClass('md-tab');
                $(inicio).css('padding-top', '12px');
                $scope.setHomeIframeUrl();
            }
        }, 500);
    };


    $scope.setHomeIframeUrl = function () {
        try {

            $('#homePageFrame').attr('src', "../../content/Images/loading.gif");

            var url = "../../Views/wf/HomePage.aspx?userid=" + GetUID();

            if (window.frames['homePageFrame'] !== undefined) {

                $('#homePageFrame').attr('src', url);
            }
            else {
                jQuery.ajax({
                    type: 'GET',
                    url: url,
                    success: function () {
                        window.frames['homePageFrame'].src = url;
                    }
                });
            }

            $scope.ResizeCurrentTabHome();

        } catch (e) {
            console.error(e);
        }

    }

    //Da tamaño al tab pasado por parametro.
    $scope.ResizeCurrentTabHome = function (item) {
        let tabsHomeWrapper = $('#tabsHomeWrapper').outerHeight(false);
        $('#contentTabhomeMain').css("height", (window.innerHeight - tabsHomeWrapper + 10) + "px");
        $('#tabhomeMain').css("height", (window.innerHeight - tabsHomeWrapper) + "px");
        $('#tabhomeDash').css("height", (window.innerHeight - tabsHomeWrapper) + "px");
        //$('#homePageFrame').css("height", (window.innerHeight - tabsHomeWrapper) + "px");
    }

    //$scope.setDiagramsIframeUrl = function () {
    //    try {


    //        $('#diagramsFrame').attr('src', "../../../Zamba.Diagram/public/index.html");

    //        var url = "../../../Zamba.Diagram/public/index.html";

    //        if (window.frames['diagramsFrame'] !== undefined) {

    //            $('#diagramsFrame').attr('src', url);
    //        }
    //        else {
    //            jQuery.ajax({
    //                type: 'GET',
    //                url: url,
    //                success: function () {
    //                    window.frames['diagramsFrame'].src = url;
    //                }
    //            });
    //        }
    //    } catch (e) {

    //    }
    //}


});