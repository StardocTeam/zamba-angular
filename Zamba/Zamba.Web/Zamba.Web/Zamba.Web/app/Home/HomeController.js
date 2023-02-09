app.controller('HomeController', function ($scope, HomeService) {

    $scope.Tabs = ['Acciones'];

    $scope.init = function () {
        try {

      
        HomeService._getTabs().then(function (response) {

            if (response.data === null) {
                $scope.Tabs = [];
            } else {
                $scope.Tabs = response.data;
                $scope.Tabs.push('Diagramas');
            }
        });


            var d = HomeService.getResults();

            if (d == "") {
                console.log("No se pudo obtener los Tabs de la Home");
                $scope.Tabs = [];
               
            }
            else {
                $scope.Tabs = d;
                $scope.Tabs.push('Diagramas');
            }
      
        } catch (e) {

        }

    };
});

function setHomePage(_this, mode) {
    $("#tabhomeMain").hide();
    $("#tabhomeReports").hide();
    $("#tabhomeCalendar").hide();
    $("#tabhomeNews").hide();
    $("#tabhomeDiagrams").hide();

    switch (mode) {
        case "HomeMain":
            $("#tabhomeMain").show();
            break;
        case "HomeReports":
            $("#tabhomeReports").show();
            break;
        case "HomeCalendar":
            $("#tabhomeCalendar").show();
            break;
        case "HomeNews":
            $("#tabhomeNews").show();
            break;
        case "HomeDiagrams":
            $("#tabhomeDiagrams").show();
            break;
    }

};

function setHomeIframeUrl() {

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
}


function setDiagramsIframeUrl() {
    try {

   
    $('#diagramsFrame').attr('src', "../../../Zamba.Diagram/public/index.html");

    var url = "../../../Zamba.Diagram/public/index.html";

    if (window.frames['diagramsFrame'] !== undefined) {

        $('#diagramsFrame').attr('src', url);
    }
    else {
        jQuery.ajax({
            type: 'GET',
            url: url,
            success: function () {
                window.frames['diagramsFrame'].src = url;
            }
        });
        }
    } catch (e) {

    }
}
