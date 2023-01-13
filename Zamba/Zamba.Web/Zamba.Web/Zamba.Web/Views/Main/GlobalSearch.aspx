<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GlobalSearch.aspx.cs" Inherits="Zamba.Web.Views.Main.GlobalSearch" %>


                                                    <%--SOLO PARA WINDOWS--%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <script>
        enableGlobalSearch = "true";
    </script>

    <script>
        if (typeof (winFormJSCall) != "undefined") {
            var conf = winFormJSCall.confVal();
            conf.then(function (confVal) {
                confVal = JSON.parse(confVal);
                thisDomain = confVal.GlobalSearchURL;
                ZambaWebRestApiURL = confVal.ZambaWebRestApiURL;
                userIdGS = confVal.ZLuserId;
            });
        }
        else {
            //SOLO PARA TEST, DEBE USAR winFormJSCall
            //thisDomain = "http://localhost/Zamba.Web/";
            //ZambaWebRestApiURL = "http://localhost/ZambaWeb.RestApi/api/";
            userIdGS = 2;
        }

  //demosiniestros 50356
    </script>

    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/themes/base/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/bootstrap-theme.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/HomeWidget.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/toastr.css" />

    <script src="../../Scripts/jquery-2.2.2.min.js"></script>
    <script src="../../Scripts/jquery-ui-1.11.4.min.js"></script>
    <script src="../../Scripts/angular.min.js"></script>
    <script src="../../Scripts/angular-sanitize.min.js"></script>
    <script src="../../Scripts/angular-animate.min.js"></script>
    <script src="../../Scripts/ng-embed/ng-embed.min.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/ui-bootstrap-tpls-0.12.0.min.js"></script>
    <script src="../../Scripts/bootstrap-waitingfor.js"></script>
    <script src="../../Scripts/modernizr-custom.js"></script>
    <script src="../../Scripts/bootbox.js"></script>
    <script src="../../Scripts/toastr.js"></script>
    <script src="../../Scripts/Zamba.Fn.js"></script>

    <title>Zamba Home</title>
    <meta charset="utf-8" />
</head>

<body data-ng-app="app">
    <script>
        function GetUID() {
            return userIdGS;
        }
        initaltab = "";
        zambaApplication = "ZambaHomeWidget";
        var URLServer = ZambaWebRestApiURL;

    </script>

    <div class="container HWContainer">
        <div class="row HWRowTitle">
            <img src="../../Content/Images/pwbyzamba.png" class="HWTitleImg" />
            <div class="HWTitle"></div>
            <button style="float: right;" class="btn btn-default btn-xs" onclick="location.reload();">
                <span class="glyphicon glyphicon-refresh"></span>
            </button>
        </div>
        <div class="row HRGlobalSearch">
            <div class="col-xs-4 col-xs-offset-4">
<%--                <zhwgs:UCHWGlobalSearch runat="server" ID="GlobalSearch" />--%>
            </div>
        </div>
    </div>
</body>
</html>

<script>
        // angular.element(".search-parameter-input").scope().doSearch();
        var zambaApplication = "ZambaQuickSearch";
        var SQword = "";
        var SQqtity = "";
        $(document).ready(function () {
            //quitar para debbug
            $(".HWRowTitle, #Advfilter2").remove();

            $('#Advfilter-modal-content').modal();
            $("#advSearchClose, #advSearchSave").remove();
            $("#Advfilter-modal-content").children(".modal-dialog").css({ "top": 0, "left": 0, "width": "600px", "padding": 0 });
        });
        function DoQuickSearch(w) {
            $("#advSearchRemAll").click()
            $(".search-parameter-input").val(w).trigger("input");
            SQword = w;
            setTimeout(function () { TiggerOpenResult(w); }, 2000);
        }

        function TiggerOpenResult(word) {
            var results = $(".rowWord").not(".ng-hide");
            var noResultDiv = $("div[ng-show='match.model.noresults']").not(".ng-hide");
            if (results.length > 0 && !noResultDiv.length) {
                $($(".rowWord").not(".ng-hide")[0]).click();
                // setTimeout(function () { OpenResults(word); }, 2000);
            }
            //  winFormJSCall.showMessage("Zamba Quick Search", "Se encontraron " + results.length + " palabras con " + word);
        }

        function OpenResultsQuickSearch() {
            if (SQword != "" && SQqtity > 0) {
                winFormJSCall.showMessage("Zamba Quick Search", SQword + ": " + SQqtity + " resultados");
                SQword = "";
                SQqtity = 0;
            }
        }
</script>
