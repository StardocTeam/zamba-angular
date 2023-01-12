//var webadminApp = angular.module('app', ['ngTouch', 'ui.grid', 'ui.grid.edit', 'ui.grid.rowEdit', 'ui.grid.cellNav', 'addressFormatter']);


webadminApp.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(false);


}]);

webadminApp.controller('ZMachineConfig', ['$scope', '$http', '$q', '$interval', function ($scope, $http, $q, $interval) {
    $scope.gridOptions = {
        enableFiltering: true,

    };
    //var url = ThisDomain + "/api/Abm/";



    $scope.gridOptions.columnDefs = [

      { name: 'Nombre', field: 'name', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit : false },
      { name: 'Valor', field: 'value', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: true },
      { name: 'Seccion', field: 'nombre', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
     
    ];

    $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
        if (col.filters[0].term) {
            return 'header-filtered';
        } else {
            return '';
        }
    };

    $scope.saveRow = function (rowEntity) {
        
        // create a fake promise - normally you'd use the promise returned by $http or $resource
        var promise = $q.defer();
        $scope.gridApi.rowEdit.setSavePromise(rowEntity, promise.promise);
        var Data = {
            "DefaultValue": rowEntity.machineName,
            "Name": rowEntity.name,
            "Value": rowEntity.value,
            "section": rowEntity.nombre,
        }
        $http.post('http://localhost/Zamba.WebAdmin/api/abm/EditZmachine', Data).
             then(function (data, status, headers, config) {
                 promise.resolve();
             })
    };

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.gridApi = gridApi;
        gridApi.rowEdit.on.saveRow($scope, $scope.saveRow);


    };

    $scope.create = function () {
        var Data = {
            "DefaultValue":$scope.NamePc,
            "Name": $scope.Name,
            "Value": $scope.Value,
            "section": $scope.section,
        }
        $http.post('http://localhost/Zamba.WebAdmin/api/abm/InsertZmachine', Data).
            then(function (data, status, headers, config) {
                 $scope.gridOptions.data.push({
                     "name": $scope.Name,
                     "value": $scope.Value,
                     "nombre": $scope.section,
                 });
             }).
             error(function (data, status, headers, config) {
                 return "";
             });
   };
   
    angular.element(document).ready(function () {
        var rowCount = 0;
        $scope.loading = true;
        $scope.myData = [];
        var i = 0; 
        //$http.get("../../api/Abm/ZmachineData")
        $http.get("http://localhost/Zamba.WebAdmin/api/abm/ZmachineData")
        .then(function (d) {
           
            $scope.gridOptions.data = d.data;
            
        }),
        (function (error) {
            $scope.loading = false;
            console.log("Error retrieving data.");
        });
    });
    
 
}]);



webadminApp.controller('ZMachineConfig2', ['$scope', '$http', '$q', '$interval', function ($scope, $http, $q, $interval) {
    $scope.gridOptions = {

        enableFiltering: true,
    };
    //var url = ThisDomain + "/api/Abm/";
    $scope.gridOptions.columnDefs = [
      { name: 'Nombre De Pc', field: 'machineName', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
      { name: 'Nombre', field: 'name', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
      { name: 'Valor', field: 'value', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: true },
      { name: 'Seccion', field: 'nombre', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
    
    ];

    $scope.saveRow = function (rowEntity) {

        // create a fake promise - normally you'd use the promise returned by $http or $resource
        var promise = $q.defer();
        $scope.gridApi.rowEdit.setSavePromise(rowEntity, promise.promise);
        var Data = {
            "DefaultValue": rowEntity.machineName,
            "Name": rowEntity.name,
            "Value": rowEntity.value,
            "section": rowEntity.nombre,
        }
        $http.post('http://localhost/Zamba.WebAdmin/api/abm/EditZmachine', Data).
             then(function (data, status, headers, config) {
                 toastr.succes("Insercion exitosa");
                 promise.resolve();

             })
    };

    $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
        if (col.filters[0].term) {
            return 'header-filtered';
        } else {
            return '';
        }
    };

    $scope.gridOptions.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.gridApi = gridApi;
        gridApi.rowEdit.on.saveRow($scope, $scope.saveRow);
    };

    angular.element(document).ready(function () {
        var rowCount = 0;
        $scope.loading = true;
        $scope.myData = [];
        var i = 0; 
        //$http.get("../../api/Abm/ZmachineData2")
        $http.get("http://localhost/Zamba.WebAdmin/api/abm/ZmachineData2")
        .then(function (d) {
            $scope.gridOptions.data = d.data;

        },function (error){

       });
    });


}]);

webadminApp.controller('ZuserConfig', ['$scope', '$http', '$q', '$interval', function ($scope, $http, $q, $interval) {
    $scope.gridOptions = {
        enableFiltering: true,
    };
    //var url = ThisDomain + "/api/Abm/";
    $scope.gridOptions.columnDefs = [
    
      { name: 'Nombre', field: 'name', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
      { name: 'Valor', field: 'value', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: true },
      { name: 'Seccion', field: 'nombre', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
  
    ];

    $scope.saveRow = function (rowEntity) {

        // create a fake promise - normally you'd use the promise returned by $http or $resource
        var promise = $q.defer();
        $scope.gridApi.rowEdit.setSavePromise(rowEntity, promise.promise);
        var Data = {
            "DefaultValue": rowEntity.userId,
            "Name": rowEntity.name,
            "Value": rowEntity.value,
            "section": rowEntity.nombre,
        }
        $http.post('http://localhost/Zamba.WebAdmin/api/abm/EditZuserconfig', Data).
             then(function (data, status, headers, config) {
                 
                 promise.resolve();

             })
        // fake a delay of 3 seconds whilst the save occurs, return error if gender is "male"

        //promise.resolve();

    };

    $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
        if (col.filters[0].term) {
            return 'header-filtered';
        } else {
            return '';
        }
    };


    $scope.gridOptions.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.gridApi = gridApi;
        gridApi.rowEdit.on.saveRow($scope, $scope.saveRow);
    };



    $scope.create = function () {

        var Data = {
            "DefaultValue": $scope.UserName,
            "Name": $scope.Name,
            "Value": $scope.Value,
            "section": $scope.section,
        }
        $http.post('http://localhost/Zamba.WebAdmin/api/abm/InsertZuserconfig', Data).
             then(function (data, status, headers, config) {
                 toastr.succes("Insercion exitosa");
                 $scope.gridOptions.data.push({
                     "name": $scope.Name,
                     "value": $scope.Value,
                     "nombre": $scope.section
                 });

             }).
             error(function (data, status, headers, config) {
                 return "";
             });
    };

    angular.element(document).ready(function () {
        var rowCount = 0;
        $scope.loading = true;
        $scope.myData = [];
        var i = 0; 
        //$http.get("../../api/Abm/ZuserConfig") 
        $http.get("http://localhost/Zamba.WebAdmin/api/abm/ZuserConfig")
        .then(function (d) {
            $scope.gridOptions.data = d.data;

        })
        .error(function () {
            $scope.loading = false;
            console.log("Error retrieving data.");
        });
    });

 
}]);

webadminApp.controller('ZuserConfig2', ['$scope', '$http', '$q', '$interval', function ($scope, $http, $q, $interval) {
    $scope.gridOptions = {
        enableFiltering: true,
    };
    
    //var url = ThisDomain + "/api/Abm/";
    $scope.gridOptions.columnDefs = [
      { name: 'Usuario', field: 'name', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
      { name: 'Nombre', field: 'name1', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
      { name: 'Valor', field: 'value', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: true },
      { name: 'Seccion', field: 'nombre', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
   
    ];

    $scope.saveRow = function (rowEntity) {

        // create a fake promise - normally you'd use the promise returned by $http or $resource
        var promise = $q.defer();
        $scope.gridApi.rowEdit.setSavePromise(rowEntity, promise.promise);
        var Data = {
            "DefaultValue": rowEntity.userId,
            "Name": rowEntity.name,
            "Value": rowEntity.value,
            "section": rowEntity.nombre,
        }
        $http.post('http://localhost/Zamba.WebAdmin/api/abm/EditZuserconfig', Data).
             then(function (data, status, headers, config) {
                 toastr.succes("Insercion exitosa");
                 promise.resolve();

             })
        // fake a delay of 3 seconds whilst the save occurs, return error if gender is "male"

        //promise.resolve();

    };
    $scope.gridOptions.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.gridApi = gridApi;
        gridApi.rowEdit.on.saveRow($scope, $scope.saveRow);
    };

    $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
        if (col.filters[0].term) {
            return 'header-filtered';
        } else {
            return '';
        }
    };


    angular.element(document).ready(function () {
        var rowCount = 0;
        $scope.loading = true;
        $scope.myData = [];
        var i = 0; 
        //$http.get("../../api/Abm/ZuserConfig2")
        $http.get("http://localhost/Zamba.WebAdmin/api/abm/ZuserConfig")
        .then(function (d) {
            $scope.gridOptions.data = d.data;

        })
        .error(function () {
            $scope.loading = false;
            console.log("Error retrieving data.");
        });
    });

   
}]);

webadminApp.controller('Zopt', ['$scope', '$http', '$q', '$interval', function ($scope, $http, $q, $interval) {
    $scope.gridOptions = {
        enableFiltering: true,
    };
    //var url = ThisDomain + "/api/Abm/";
    $scope.gridOptions.columnDefs = [
      //{ name: 'Usuario', field: 'name', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
      { name: 'Nombre', field: 'item', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },
      { name: 'Valor', field: 'value', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: true },
      //{ name: 'Seccion', field: 'nombre', headerCellClass: $scope.highlightFilteredHeader, enableCellEdit: false },

    ];

    $scope.saveRow = function (rowEntity) {

        // create a fake promise - normally you'd use the promise returned by $http or $resource
        var promise = $q.defer();
        $scope.gridApi.rowEdit.setSavePromise(rowEntity, promise.promise);
        var Data = {
            //"DefaultValue": rowEntity.userId,
            "Name": rowEntity.item,
            "Value": rowEntity.value,
            //"section": rowEntity.nombre,
        }
        $http.post('http://localhost/Zamba.WebAdmin/api/abm/EditZopt', Data).
             then(function (data, status, headers, config) {
                 //toastr.succes("Insercion exitosa");
                 promise.resolve();

             })
        // fake a delay of 3 seconds whilst the save occurs, return error if gender is "male"

        //promise.resolve();

    };
    $scope.gridOptions.onRegisterApi = function (gridApi) {
        //set gridApi on scope
        $scope.gridApi = gridApi;
        gridApi.rowEdit.on.saveRow($scope, $scope.saveRow);
    };

    $scope.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
        if (col.filters[0].term) {
            return 'header-filtered';
        } else {
            return '';
        }
    };

    $scope.create = function () {
        var Data = {
            "Name": $scope.Name,
            "Value": $scope.Value,
  
        }
        $http.post('http://localhost/Zamba.WebAdmin/api/abm/InsertZopt', Data).
             then(function (data, status, headers, config) {
                 //toastr.succes("Insercion exitosa");
                 $scope.gridOptions.data.push({
                     "item": $scope.Name,
                     "value": $scope.Value,
                 
                 });

             }).
             error(function (data, status, headers, config) {
                 return "";
             });
    };

    angular.element(document).ready(function () {
        var rowCount = 0;
        $scope.loading = true;
        $scope.myData = [];
        var i = 0; 
        //$http.get("../../api/Abm/ZoptData") //marcos/ se medifica ya que de esta forma no funciona en IE
        $http.get("http://localhost/Zamba.WebAdmin/api/abm/ZoptData")
        .then(function (d) {
            $scope.gridOptions.data = d.data;

        })
        .error(function () {
            $scope.loading = false;
            console.log("Error retrieving data.");
        });
    });


}]);
function ShowZmachine() {

        $(".ZuserConfigGrids").css("display", "none");
        $(".ZMachineConfigGrids").css("display", "block");
  
        $(".ZoptGrid").css("display", "none");
}

function ShowZUserConfig() {


    $(".ZuserConfigGrids").css("display", "block");
    $(".ZMachineConfigGrids").css("display", "none");
    $(".ZoptGrid").css("display", "none");
}

function ShowZopt() {

    $(".ZoptGrid").css("display", "block");
    $(".ZuserConfigGrids").css("display", "none");
    $(".ZMachineConfigGrids").css("display", "none");
}