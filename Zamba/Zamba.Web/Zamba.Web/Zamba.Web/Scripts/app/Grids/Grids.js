
app.factory('dataFactory', ['$http', function ($http) {
    var url = ZambaWebRestApiURL;    
    var urlBase = url +  '/Tasks/GetBCHistorySummary?userid=';
    var dataFactory = {};

    dataFactory.GetGridData = function (userid)
    {
        return $http.get(urlBase + userid);
    };

    return dataFactory;

    }]);

app.controller('GridListController', ['$scope', 'dataFactory',
        function ($scope, dataFactory) {
            $scope.GridData;
            getGrid();
            function getGrid()
            {
                dataFactory.GetGridData(GetUID())
                    .then(function (response)
                    {
                        $scope.GridData = response.data;
                    }, function (error) {
                        $scope.status = 'Unable to load customer data: ' + error.message;
                    });
            }   

            $scope.OpenTask = function (obj) {
                var url = "../WF/TaskSelector.ashx?" + 'doctype=' + obj.g.DocTypeId + '&docid=' + obj.g.DocId + '&taskid=' + 0 + '&wfstepid=' + 0  + "&userId=" + GetUID();
                OpenDocTask3(0, obj.g.DocId, obj.g.DocTypeId, false, obj.g.Name, url, GetUID(),0);   
            };

}]);

app.directive('gridBCHistory', function ($sce) {

    return {
        restrict: 'E',
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: $sce.getTrustedResourceUrl('../../scripts/app/partials/PartialGrid.html')
    };
});









