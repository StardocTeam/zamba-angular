app.controller('ZambaFiltersController', function ($scope, $rootScope, SearchFilterService) {

    $scope.inputValues = {
        dateValue: "",
        alphaNumericInputValue: ""
    };

    $scope.operatorsAlphanumericToChoose = [
        {
            value: '=',
            Description: '='
        },
        {
            value: '>',
            Description: '>'
        },
        {
            value: '<',
            Description: '<'
        },
        {
            value: '>=',
            Description: '>='
        },
        {
            value: '<=',
            Description: '<='
        },
        {
            value: '<>',
            Description: '<>'
        },
        {
            value: 'Es nulo',
            Description: 'Es nulo'
        },
        {
            value: 'No es Nulo',
            Description: 'No es Nulo'
        },
        {
            value: 'empieza',
            Description: 'Inicia'
        },
        {
            value: 'termina',
            Description: 'Termina'
        },
        {
            value: 'No Contiene',
            Description: 'No Contiene'
        },
        {
            value: 'Contiene',
            Description: 'Contiene'
        },
    ];
    //Opciones de operadores para campos fecha
    $scope.operatorsCalendarToChoose = [
        {
            value: '=',
            Description: '='
        },
        {
            value: '>',
            Description: '>'
        },
        {
            value: '<',
            Description: '<'
        },
        {
            value: '>=',
            Description: '>='
        },
        {
            value: '<=',
            Description: '<='
        },
        {
            value: '<>',
            Description: '<>'
        },
        {
            value: 'Es Nulo',
            Description: 'Es nulo'
        },
        {
            value: 'No es Nulo',
            Description: 'No es Nulo'
        },
    ];
    //Opciones para el combo/select
    $scope.optionsToChoose = [
        {
            ID: -1,
            Name: '- Seleccionar otros filtros -',
            disabled: true,
            isDate: false
        },
        {
            ID: 1,
            Name: 'Tarea',
            isDate: false,
            disabled: false,
        },
        {
            ID: 2,
            Name: 'Fecha Creacion',
            isDate: true,
            disabled: false,
        },
        {
            ID: 3,
            Name: 'Modificación',
            isDate: true,
            disabled: false,
        },
        {
            ID: 4,
            Name: 'Nombre Original',
            isDate: false,
            disabled: false,
        },
        {
            ID: 5,
            Name: 'Estado Tarea',
            isDate: false,
            disabled: false,
        },
    ];

    //objeto que acumula condiciones para una misma columna en un array
    $scope.zambaFiltersForDefaultColumns = { lupdateFilters: [], crdateFilters: [], originalFilenameFilters: [], nameFilters: [], stateFilters: [] };

    $scope.currentSelectedOption = $scope.optionsToChoose[0];

    //Opciones de operadores para campos alfanumericos
    $scope.operatorsToChoose = $scope.operatorsAlphanumericToChoose;

    $scope.currentSelectedOperator = $scope.operatorsToChoose[0];

    $scope.zambaFilterOnChange = function (executeSearch) {
        
        var lupdateFilters = $scope.removeAngularHasKey($scope.zambaFiltersForDefaultColumns.lupdateFilters);
        var crdateFilters = $scope.removeAngularHasKey($scope.zambaFiltersForDefaultColumns.crdateFilters);
        var originalFilenameFilters = $scope.removeAngularHasKey($scope.zambaFiltersForDefaultColumns.originalFilenameFilters);
        var nameFilters = $scope.removeAngularHasKey($scope.zambaFiltersForDefaultColumns.nameFilters);
        var stateFilters = $scope.removeAngularHasKey($scope.zambaFiltersForDefaultColumns.stateFilters);

        var selectedOptionsForEventParams = { lupdateFilters: lupdateFilters, crdateFilters: crdateFilters, originalFilenameFilters: originalFilenameFilters, nameFilters: nameFilters, stateFilters: stateFilters, executeSearch: executeSearch};
        //uso $rootScope porque el handler se encuentra en un controller al mismo nivel que el ZambaFiltersController
        $rootScope.$broadcast('zambaFilterOnChangeEvent', selectedOptionsForEventParams);

    }


    $scope.selectedOptionHasChanged = function () {

        if ($scope.currentSelectedOption.isDate) {
            if ($scope.operatorsToChoose == $scope.operatorsAlphanumericToChoose) {
                $scope.operatorsToChoose = $scope.operatorsCalendarToChoose;
                $scope.currentSelectedOperator = $scope.operatorsToChoose[0];
            }
        } else {
            if ($scope.operatorsToChoose == $scope.operatorsCalendarToChoose) {
                $scope.operatorsToChoose = $scope.operatorsAlphanumericToChoose
                $scope.currentSelectedOperator = $scope.operatorsToChoose[0];
            }
        }
    }

    $scope.selectedOperatorHasChanged = function () {
        console.log('filtros', 'selectedOperatorHasChanged');
        if ($scope.currentSelectedOperator.value == 'Es nulo' || $scope.currentSelectedOperator.value == 'No es Nulo') {
            $scope.inputValues.dateValue = "";
            $scope.inputValues.alphaNumericInputValue = "";
        }
    }

    //remove al $$hashKey agregado por angularjs luego que una coleccion es usada en un ng-repeat
    $scope.removeAngularHasKey = function (collection) {

        return collection.map(function (item) {
            return { "Field": item.Field, "Operator": item.Operator, "Value": item.Value, "DataBaseColumn": "-", "Enabled": item.Enabled, "FilterID": item.FilterID  };
        });
    }

    $scope.changeEnableState = function (filter) {
        var executeSearch = true;
        SearchFilterService.SetEnabledFilterById(filter.FilterID,filter.Enabled);
        $scope.zambaFilterOnChange(executeSearch);
    }

    $scope.searchDefaultFiltersByEnter = function (e) {
        
        if (e.keyCode == 13) {
            $scope.inputValues.dateValue = e.target.value;
            $scope.addDefaultZambaColumnFilter();
        }
    }

    //function que agrega items a la coleccion de filtros
    $scope.addDefaultZambaColumnFilter = function () {
       
        try {
            var zFilterWebItem = undefined;
            var newItem = undefined;

            if ($scope.currentSelectedOption.ID != -1) {
                var canThrowEventToSearch = false;
                if ($scope.currentSelectedOption.isDate) {
                    if ($scope.inputValues.dateValue != undefined && $scope.inputValues.dateValue != '' || $scope.currentSelectedOperator.value == "Es nulo" || $scope.currentSelectedOperator.value == "No es Nulo") {
                        if ($scope.validateDateFilterIsValid($scope.inputValues.dateValue) || $scope.currentSelectedOperator.value == "Es nulo" || $scope.currentSelectedOperator.value == "No es Nulo") {
                            newItem = { "Field": $scope.currentSelectedOption.Name, "Operator": $scope.currentSelectedOperator.value, "Value": $scope.inputValues.dateValue, "Enabled": true };
                            switch ($scope.currentSelectedOption.Name) {
                                case "Fecha Creacion":
                                    if (!$scope.newFilterAlreadyExist($scope.zambaFiltersForDefaultColumns.crdateFilters, newItem)) {
                                        $scope.zambaFiltersForDefaultColumns.crdateFilters.push(newItem);
                                        canThrowEventToSearch = true;

                                        zFilterWebItem = { indexId: 0, attribute: "Creado", dataType: "5", comparator: $scope.currentSelectedOperator.value, filterValue: newItem.Value, docTypeId: "", description: "Fecha Creacion", additionalType: "10" };
                                    }
                                    break;
                                case "Modificación":
                                    if (!$scope.newFilterAlreadyExist($scope.zambaFiltersForDefaultColumns.lupdateFilters, newItem)) {
                                        $scope.zambaFiltersForDefaultColumns.lupdateFilters.push(newItem);
                                        canThrowEventToSearch = true;

                                        zFilterWebItem = { indexId: 0, attribute: "Modificado", dataType: "5", comparator: $scope.currentSelectedOperator.value, filterValue: newItem.Value, docTypeId: "", description: "Modificación", additionalType: "10" };
                                    }
                                    break;
                                default:
                            }
                        }
                    }
                } else {
                    if ($scope.inputValues.alphaNumericInputValue != undefined && $scope.inputValues.alphaNumericInputValue != '' || $scope.currentSelectedOperator.value == "Es nulo" || $scope.currentSelectedOperator.value == "No es Nulo") {
                        newItem = {
                            "Field": $scope.currentSelectedOption.Name, "Operator": $scope.currentSelectedOperator.value, "Value": $scope.inputValues.alphaNumericInputValue, "Enabled": true
                        };
                        switch ($scope.currentSelectedOption.Name) {
                            case "Nombre Original":
                                if (!$scope.newFilterAlreadyExist($scope.zambaFiltersForDefaultColumns.originalFilenameFilters, newItem)) {
                                    $scope.zambaFiltersForDefaultColumns.originalFilenameFilters.push(newItem);
                                    canThrowEventToSearch = true;
                                    zFilterWebItem = { indexId: 0, attribute: "Original", dataType: "7", comparator: $scope.currentSelectedOperator.value, filterValue: newItem.Value, docTypeId: "", description: "Nombre Original", additionalType: "10" };
                                }
                                break;
                            case "Tarea":
                                if (!$scope.newFilterAlreadyExist($scope.zambaFiltersForDefaultColumns.nameFilters, newItem)) {
                                    $scope.zambaFiltersForDefaultColumns.nameFilters.push(newItem);
                                    canThrowEventToSearch = true;

                                    zFilterWebItem = { indexId: 0, attribute: "Tarea", dataType: "7", comparator: $scope.currentSelectedOperator.value, filterValue: newItem.Value, docTypeId: "", description: "Tarea", additionalType: "10" };
                                }
                                break;
                            case "Estado Tarea":
                                if (!$scope.newFilterAlreadyExist($scope.zambaFiltersForDefaultColumns.stateFilters, newItem)) {
                                    $scope.zambaFiltersForDefaultColumns.stateFilters.push(newItem);
                                    canThrowEventToSearch = true;

                                    zFilterWebItem = { indexId: 0, attribute: "Estado Tarea", dataType: "7", comparator: $scope.currentSelectedOperator.value, filterValue: newItem.Value, docTypeId: "", description: "Estado Tarea", additionalType: "10" };
                                }
                                break;
                            default:
                        }
                    }
                }
                //Busca solo si se ha agregado un nuevo filtro
                if (canThrowEventToSearch) {
                    if ($scope.Search.DoctypesIds.length == 1) {
                        var filterType = "";
                        if ($scope.Search.View == "MyProcess")
                            filterType = "Task";
                        else
                            filterType = $scope.Search.View;

                        zFilterWebItem.docTypeId = $scope.Search.DoctypesIds[0];
                        zFilterWebItem.filterType = filterType;
                        zFilterWebItem.dataDescription = "";
                        let filterResult = SearchFilterService.AddFilter(zFilterWebItem);
                        newItem.FilterID = filterResult.Id;
                    }
                    $scope.zambaFilterOnChange();
                }

            }
        } catch (e) {
            console.error(e);
        }
        
    }

    $scope.validateDateFilterIsValid = function (dateToValidate) {
        try {
            var arrayDate = dateToValidate.split('/');
            arrayDate.reverse();
            var dateFormatted = '';
            for (var i = 0; i < arrayDate.length; i++) {
                if (i != arrayDate.length - 1)
                    dateFormatted += arrayDate[i] + '/';
                else
                    dateFormatted += arrayDate[i];
            }
            return moment(dateFormatted).isValid();
        } catch (e) {
            return false;
        }

    }

    $scope.newFilterAlreadyExist = function (arrayToValidate, newItem) {
        var alreadyExist = arrayToValidate.filter(function (item) {
            return ('' + item.Field + item.Operator + item.Value == '' + newItem.Field + newItem.Operator + newItem.Value);
        });
        return (alreadyExist.length > 0);
    }
    //quita el filtro de la coleccion, segun en que array de filtros se encuentre
    $scope.removeDefaultZambaColumnFilter = function (childItem, parenName) {

        SearchFilterService.RemoveFilterById(childItem.FilterID);

        switch (parenName) {
            case 'originalFilenameFilters':
                $scope.zambaFiltersForDefaultColumns.originalFilenameFilters = $scope.zambaFiltersForDefaultColumns.originalFilenameFilters.filter(function (item) {
                    return ('' + item.Field + item.Operator + item.Value != ''+ childItem.Field + childItem.Operator + childItem.Value);
                });
                break;
            case 'nameFilters':
                $scope.zambaFiltersForDefaultColumns.nameFilters = $scope.zambaFiltersForDefaultColumns.nameFilters.filter(function (item) {
                    return ('' + item.Field + item.Operator + item.Value != '' + childItem.Field + childItem.Operator + childItem.Value);
                });
                break;
            case 'crdateFilters':
                $scope.zambaFiltersForDefaultColumns.crdateFilters = $scope.zambaFiltersForDefaultColumns.crdateFilters.filter(function (item) {
                    return ('' + item.Field + item.Operator + item.Value != '' + childItem.Field + childItem.Operator + childItem.Value);
                });
                break;
            case 'lupdateFilters':
                $scope.zambaFiltersForDefaultColumns.lupdateFilters = $scope.zambaFiltersForDefaultColumns.lupdateFilters.filter(function (item) {
                    return ('' + item.Field + item.Operator + item.Value != '' + childItem.Field + childItem.Operator + childItem.Value);
                });
                break;
            case 'stateFilters':
                $scope.zambaFiltersForDefaultColumns.stateFilters = $scope.zambaFiltersForDefaultColumns.stateFilters.filter(function (item) {
                    return ('' + item.Field + item.Operator + item.Value != '' + childItem.Field + childItem.Operator + childItem.Value);
                });
                break;
            default:
        }
        $scope.zambaFilterOnChange();
    }

    $scope.SetEnabledFilterToDB = function (childItem) {
        var zfilterwebToUpdate = {
            "indexId": 0,
            "enabled": childItem.Enabled,
            "attribute": childItem.Field,
            "comparator": childItem.Operator,
            "filterValue": childItem.Value,
            "docTypeId": ""
        };
        $rootScope.$broadcast('enabledStateChangeZFiltersWebEvent', zfilterwebToUpdate);

    }

    $scope.RemoveOtherFiltersFromDB = function () {
        var zfilterwebToDelete = {
            "docTypeId": ""
        };
        $rootScope.$broadcast('removeOtherZFiltersWebEvent', zfilterwebToDelete);
    }

    $scope.removeAllDefaultZambaColumnFilter = function (executeSearch) {
        $scope.zambaFiltersForDefaultColumns = { lupdateFilters: [], crdateFilters: [], originalFilenameFilters: [], nameFilters: [], stateFilters: [] };
        $scope.currentSelectedOption = $scope.optionsToChoose[0];
        $scope.RemoveOtherFiltersFromDB();
        $scope.zambaFilterOnChange(executeSearch);
    }
    $scope.$on('removeAllDefaultZambaColumnFilter', function (event, data) {
        $scope.removeAllDefaultZambaColumnFilter(data);
    });

    $scope.$on('loadSearchFromLocalEvent', function (event, data) {
        if (data.crdateFilters)
            $scope.zambaFiltersForDefaultColumns.crdateFilters = data.crdateFilters;
        if (data.lupdateFilters)
            $scope.zambaFiltersForDefaultColumns.lupdateFilters = data.lupdateFilters;
        if (data.nameFilters)
            $scope.zambaFiltersForDefaultColumns.nameFilters = data.nameFilters;
        if (data.originalFilenameFilters)
            $scope.zambaFiltersForDefaultColumns.originalFilenameFilters = data.originalFilenameFilters;
        if (data.stateFilters)
            $scope.zambaFiltersForDefaultColumns.stateFilters = data.stateFilters;


        $scope.operatorsToChoose = $scope.operatorsAlphanumericToChoose;
        $scope.currentSelectedOption = $scope.optionsToChoose[0];
    });

    $scope.$on('hasFiltersFromDBEvent', function (event, data) {
        var nameFilters = [];
        var originalFilenameFilters = [];
        var lupdateFilters = [];
        var crdateFilters = [];
        var stateFilters = [];

        nameFilters = $scope.getFiltersByDescription(data, "Tarea");
        originalFilenameFilters = $scope.getFiltersByDescription(data, "Nombre Original");
        lupdateFilters = $scope.getFiltersByDescription(data, "Modificación");
        crdateFilters = $scope.getFiltersByDescription(data, "Fecha Creacion");
        stateFilters = $scope.getFiltersByDescription(data, "Estado Tarea");

        if (nameFilters.length > 0) {
            $scope.zambaFiltersForDefaultColumns.nameFilters = nameFilters;
            $scope.Search.nameFilters = nameFilters;
        }
            
        if (originalFilenameFilters.length > 0) {
            $scope.zambaFiltersForDefaultColumns.originalFilenameFilters = originalFilenameFilters;
            $scope.Search.originalFilenameFilters = originalFilenameFilters;
        }
            
        if (lupdateFilters.length > 0) {
            $scope.zambaFiltersForDefaultColumns.lupdateFilters = lupdateFilters;
            $scope.Search.lupdateFilters = lupdateFilters;
        }
            
        if (crdateFilters.length > 0) {
            $scope.zambaFiltersForDefaultColumns.crdateFilters = crdateFilters;
            $scope.Search.crdateFilters = crdateFilters;
        }
        if (stateFilters.length > 0) {
            $scope.zambaFiltersForDefaultColumns.stateFilters = stateFilters;
            $scope.Search.stateFilters = stateFilters;
        }
        

        $scope.operatorsToChoose = $scope.operatorsAlphanumericToChoose;
        $scope.currentSelectedOption = $scope.optionsToChoose[0];
    });


    $scope.getFiltersByDescription = function (data, Description) {

        var zFiltersWebByDescription = [];
        zFiltersWebByDescription = data.filter(filter => filter.Description == Description);
        zFiltersWebByDescription = zFiltersWebByDescription.map(function (filter) {
            var filterValue = "";
            if (filter.FormatValue != undefined) {
                filterValue = filter.FormatValue.slice(1, -1);
            }
            var newItem = {
                Field: filter.Description,
                Operator: filter.Comparator,
                Value: filterValue,
                Enabled: filter.Enabled,
                FilterID: filter.Id
            };
            return newItem;
        });
        return zFiltersWebByDescription;
    }

    $scope.$on('resetFiltersDefaultZambaColumnFilters', function (event, data) {
        $scope.resetDefaultZambaFilters();
    });

    $scope.resetDefaultZambaFilters = function () {
        $scope.zambaFiltersForDefaultColumns = { lupdateFilters: [], crdateFilters: [], originalFilenameFilters: [], nameFilters: [], stateFilters: [] };
        $scope.Search.lupdateFilters = [];
        $scope.Search.crdateFilters = [];
        $scope.Search.originalFilenameFilters = [];
        $scope.Search.nameFilters = [];
        $scope.Search.stateFilters = [];
        $scope.operatorsToChoose = $scope.operatorsAlphanumericToChoose;
        $scope.currentSelectedOption = $scope.optionsToChoose[0];
    };
});

app.directive('zambaFilters', function ($sce) {
    return {
        restrict: 'E',
        transclude: true,
        link: function ($scope, element, attributes) {

        },
        templateUrl: $sce.getTrustedResourceUrl('../../app/ZambaFilters/ZambaFiltersTemplate.html')
    }
});


