app.controller('NewsController', function ($scope, NewsService) {

    $scope.NewsData;
    $scope.btnActive;
    $scope.DataCount;

    const newsSearch = {
        UNREAD: 'unread',
        READ: 'read',
        ALL: 'all',
    }

    $scope.init = function () {
        $scope.btnActive = newsSearch.UNREAD;
        NewsService.getNews(parseInt(GetUID()), newsSearch.UNREAD, GetNewsOnSucces, GetNewsOnError);
    }

    $scope.Reload = function (elem) {

        if (elem !== null && elem.name === $scope.btnActive) {
            // Si no apreto refresh y apreto el boton que esta seleccionado actualmente
            return;
        }

        toastr.options.timeOut = 0;
        toastr.options.extendedTimeOut = 0;
        toastr.info("Cargando novedades");

        if (elem != undefined && elem != null) {
            $scope.btnActive = elem.name;
        }

        NewsService.getNews(parseInt(GetUID()), $scope.btnActive, GetNewsOnSucces, GetNewsOnError);
    }

    function GetNewsOnSucces(response) {
        $scope.NewsData = response.data;

        if (response.data != undefined && response.data != null) {
            $scope.DataCount = response.data.length;
        }
        else {
            $scope.DataCount = 0;
        }

        toastr.clear();
    }

    function GetNewsOnError(response) {
        console.error(response);
        toastr.clear();
    }

    $scope.OpenTask = function (result) {

        NewsService.getTaskId(result.DocId, result.DocTypeId).then(function (response) {

            var url;
            var taskId = response.data;

            if (taskId > 0) {
                url = (thisDomain + "/views/WF/TaskViewer.aspx?DocType=" + result.DocTypeId + "&docid=" + result.DocId + "&taskid=" + taskId + "&mode=s" + "&s=" + 0 + "&userId=" + GetUID());
            }
            else {
                url = (thisDomain + "/views/search/docviewer.aspx?DocType=" + result.DocTypeId + "&docid=" + result.DocId + "&mode=s" + "&userId=" + GetUID());
            }

            window.open(url, '_blank');

            if (!result.IsRead) {
                SetRead(result);
            }

        });
    };

    function SetRead(result) {

        NewsService.setRead(result.DocId, result.DocTypeId).then(function (response) {

            if (response.status === 200) {
                result.IsRead = true;
            }
            else {
                console.log("No se ha podido establecer la novedad como leida");
            }

        });
    }

});