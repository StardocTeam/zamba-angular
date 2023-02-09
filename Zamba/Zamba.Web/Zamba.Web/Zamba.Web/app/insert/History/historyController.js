app.controller('BCHistoryController', function ($scope, BCHistoryService) {

    $scope.BCHistoryData;
    $scope.btnActive;
    $scope.DataCount;

    const BCHistorySearch = {
        UNREAD: 'unread',
        READ: 'read',
        ALL: 'all',
    }

    $scope.init = function () {
        $scope.btnActive = BCHistorySearch.UNREAD;
        BCHistoryService.getBCHistory(parseInt(GetUID()), BCHistorySearch.UNREAD, GetBCHistoryOnSucces, GetBCHistoryOnError);
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

        BCHistoryService.getBCHistory(parseInt(GetUID()), $scope.btnActive, GetBCHistoryOnSucces, GetBCHistoryOnError);
    }

    function GetBCHistoryOnSucces(response) {
        $scope.BCHistoryData = response.data;

        if (response.data != undefined && response.data != null) {
            $scope.DataCount = response.data.length;
        }
        else {
            $scope.DataCount = 0;
        }

        toastr.clear();
    }

    function GetBCHistoryOnError(response) {
        console.error(response);
        toastr.clear();
    }

    $scope.OpenTask = function (result) {

        BCHistoryService.getTaskId(result.DocId, result.DocTypeId).then(function (response) {

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

        BCHistoryService.setRead(result.DocId, result.DocTypeId).then(function (response) {

            if (response.status === 200) {
                result.IsRead = true;
            }
            else {
                console.log("No se ha podido establecer la novedad como leida");
            }

        });
    }


    $scope.showHistory = function () {
        var path = '../../app/insert/history/BCHistoryTemplate.html';
        $scope.showModalWithGridData(result, path, "Historial");
    };


    //modal

    $scope.openModal = function (result, path) {
        $uibModal.open({
            templateUrl: path,
            windowClass: 'modalStyle',
            controller: function ($scope, $uibModalInstance) {
                $scope.resultDetails = result;
                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };
            }
        })
    };

    $scope.showModalWithGridData = function (result, path, mode) {
        if (mode == "zoom") {
            $scope.openModal(result, path);
        } else if (mode == "moreInfo") {
            $scope.setModalData(result);
            $scope.openModal($scope.setModalData(result), path);
        }

    }

    $scope.setModalData = function (result) {
        var newResult = null;
        if (result != null) {
            newResult = mappingResultToModal(result);
        }
        return newResult;
    }

    function mappingResultToModal(result) {
        var newResultDictionary = {};
        for (var attribute in result) {
            if (!validateHiddenAttr(attribute)) {
                newResultDictionary[String(attribute)] = result[attribute] == null ? "" : String(result[attribute]);
            }
        }
        return newResultDictionary;
    }

    function validateHiddenAttr(name) {
        var isHiddenAttr = false;
        if (name == "DOC_ID" || name == "DOC_TYPE_ID" || name == "STR_ENTIDAD" || name == "THUMB" || name == "FULLPATH" || name == "ICON_ID"
            || name == "USER_ASIGNED" || name == "EXECUTION" || name == "DO_STATE_ID" || name == "TASK_ID" || name == "RN" || name == "LEIDO"
            || name == "WORKFLOW" || name == "Proceso" || name == "Estado" || name == "DISK_GROUP_ID"
            || name == "DISK_GROUP_ID" || name == "VOL_ID" || name == "OFFSET" || name == "PLATTER_ID" || name == "SHARED" || name == "VER_PARENT_ID"
            || name == "ROOTID" || name == "DISK_VOL_PATH" || name == "STEP_ID" || name == "WORK_ID" || name == "DISK_VOL_ID" || name == "VERSION"
            || name == "NUMEROVERSION" || name == "DOC_FILE" || name == "ORIGINAL" || name == "AsignedTo" || name == "Step"
            || name == "ThumbImg" || name == "Tarea" || name == "ShowUnread" || name == "Icon"
            || name.toLowerCase().startsWith("i") && isNumeric(name.slice(1, name.length - 1))) {
            isHiddenAttr = true;
        }
        return isHiddenAttr;
    }



});