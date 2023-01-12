
////The Module Declaration
//var appListGrid = angular.module('ngmodule', []);

////Declaring Service
//appListGrid.service('ngservice', function ($http) {
//    //The function to read all Orders
//    this.getOrders = function () {
//        var res = $http.get("http://localhost/Zamba.Web/Orders");
//        return res;
//    };
//    //The function to read Orders based on filter and its value
//    //The function reads all records if value is not entered
//    //else based on the filter and its value, the Orders will be returned
//    this.getfilteredData = function (filter, value) {
//        var res;
//        if (value.length == 0) {
//            res = $http.get("http://localhost/Zamba.Web/Orders");
//            return res;
//        } else {
//            res = $http.get("http://localhost/Zamba.Web/Orders/" + filter + "/" + value);
//            return res;
//        }

//    };
//});

////Declaring the Controller
//appListGrid.controller('ngcontroller', function ($scope, ngservice) {
//    $scope.Selectors = ["CustomerName", "SalesAgentName", "MobileNo"];
//    $scope.SelectedCriteria = ""; //The Object used for selecting value from <option>
//    $scope.filterValue = ""; //The object used to read value entered into textbox for filtering information from table

//    loadOrders();

//    //Function  to load all Orders
//    function loadOrders() {
//        var promise = ngservice.getOrders();
//        promise.then(function (resp) {
//            $scope.Orders = resp.data;
//            $scope.Message = "Call is Completed Successfully";
//        }, function (err) {
//            $scope.Message = "Call Failed " + err.status;
//        });
//    };

//    //Function to load orders based on a criteria
//    $scope.getFilteredData = function () {
//        var promise = ngservice.getfilteredData($scope.SelectedCriteria, $scope.filterValue);
//        promise.then(function (resp) {
//            $scope.Orders = resp.data;
//            $scope.Message = "Call is Completed Successfully";
//        }, function (err) {
//            $scope.Message = "Call Failed " + err.status;
//        });
//    };
//});