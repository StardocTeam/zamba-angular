app.controller('LastSearchController', function ($scope, LastSearchService) {

    $scope.LastSearchData;
    $scope.DataCount = 0;
    $scope.emptyMessage = '';


    $scope.init = function () {
         LastSearchService.getLastSearch(parseInt(GetUID()), GetLastSearchOnSucces, GetLastSearchOnError);
      
    }

    $scope.Reload = function (elem) {

        toastr.options.timeOut = 3000;
        toastr.options.extendedTimeOut = 0;
        toastr.info("Cargando...");

         LastSearchService.getLastSearch(parseInt(GetUID()), GetLastSearchOnSucces, GetLastSearchOnError);
       
    }

    $scope.clearSearch = function () {
        $scope.searchCardText = "";
    }


    function CountLastSearchData(LastSearchData) {
        if (LastSearchData != undefined && LastSearchData != null ) {
            $scope.DataCount = LastSearchData.length;
        }
        else {
            $scope.DataCount = 0;
        }

    }

    function GetLastSearchOnSucces(response) {
        $scope.LastSearchData = response.data;

        CountLastSearchData($scope.LastSearchData);

        $scope.$applyAsync();
    }

    function GetLastSearchOnError(error) {
        console.error(error);
        $scope.$applyAsync();
    }

    $scope.ExeccuteSearch = function (LastSearch) {
        try {
            $scope.$emit('ExecuteSearch', JSON.parse(LastSearch.ObjectSearch));    

        } catch (e) {
            console.error(e);
        }


    };

    function SetRead(result) {

        //LastSearchService.setRead(result.doc_Id, result.DOC_TYPE_ID).then(function (response) {

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