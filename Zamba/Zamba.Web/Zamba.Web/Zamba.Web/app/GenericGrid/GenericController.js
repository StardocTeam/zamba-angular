app.controller('GenericController', function ($scope, GenericService) {

    $scope.GenericData;
    $scope.btnActive;
    $scope.DataCount = 0;
    $scope.emptyMessage = '';

    const GenericSearch = {
        UNREAD: 'unread',
        READ: 'read',
        ALL: 'all',
    }

    $scope.init = function () {
        $scope.btnActive = GenericSearch.UNREAD;
         GenericService.getGeneric(parseInt(GetUID()), GenericSearch.UNREAD, $scope.gridType, GetGenericOnSucces, GetGenericOnError);
      
    }

    $scope.Reload = function (elem) {
        
        if (elem !== null && elem.name === $scope.btnActive) {
            // Si no apreto refresh y apreto el boton que esta seleccionado actualmente
            return;
        }

        toastr.options.timeOut = 3000;
        toastr.options.extendedTimeOut = 0;
        toastr.info("Cargando...");

        if (elem != undefined && elem != null) {
            $scope.btnActive = elem.name;
        }

         GenericService.getGeneric(parseInt(GetUID()), $scope.btnActive, $scope.gridType, GetGenericOnSucces, GetGenericOnError);
       
    }
    $scope.clearSearch = function () {
        $scope.searchCardText = "";
    }


    function CountGenericData(GenericData) {
        if (GenericData != undefined && GenericData != null ) {
            $scope.DataCount = GenericData.length;
        }
        else {
            $scope.DataCount = 0;
        }

    }

    function GetGenericOnSucces(response) {
        $scope.GenericData = response.data;

        CountGenericData($scope.GenericData);

        toastr.clear();
        $scope.$applyAsync();
    }

    function GetGenericOnError(response) {
        console.error(response);
        toastr.clear();
        $scope.$applyAsync();
    }

    $scope.OpenTask = function (result) {

        let userToken = JSON.parse(localStorage.getItem('authorizationData'));
        let { token } = userToken;

        GenericService.getTaskId(result.doc_id, result.DOC_TYPE_ID).then(function (response) {

            var url;
            var taskId = response.data;

            if (taskId > 0) {
                url = (thisDomain + "/views/WF/TaskViewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.doc_id + "&taskid=" + taskId + "&mode=s" + "&s=" + 0 + "&userId=" + GetUID() + "&t=" + token);
            }
            else {
                url = (thisDomain + "/views/search/docviewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.doc_id + "&mode=s" + "&userId=" + GetUID() + "&t=" + token);
            }

            window.open(url, "R" + result.doc_id);

            if (!result.IsRead) {
                SetRead(result);
            }

        });
    };

    function SetRead(result) {

        //GenericService.setRead(result.doc_Id, result.DOC_TYPE_ID).then(function (response) {

        //    if (response.status === 200) {
        //        result.IsRead = true;
        //    }
        //    else {
        //        console.log("No se ha podido establecer la novedad como leida");
        //    }

        //});
    }
    $scope.FormatDateTimeDDMMYYYYHHMMSS = function(date){
        var ret = moment(date).format("DD/MM/YYYY HH:mm:ss");
        return ret
    }
    $scope.FormatDateTimeDDMMYYYYHHMM = function (date) {
        var ret = moment(date).format("DD/MM/YYYY HH:mm");
        return ret
    }
    $scope.FormatDateTimeDDMMYYYY = function (date) {
        var ret = moment(date).format("DD/MM/YYYY");
        return ret
    }
    $scope.FormatDateTimeDDMMYY = function (date) {
        var ret = moment(date).format("DD/MM/YY");
        return ret
    }

});