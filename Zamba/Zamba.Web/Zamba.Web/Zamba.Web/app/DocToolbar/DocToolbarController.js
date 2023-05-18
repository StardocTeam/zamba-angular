app.controller('DocToolbarController', function ($scope, $filter, $http, DocToolbarService, ZambaAssociatedService) {

    $scope.HasPermissionToDownloadFile = false;
    $scope.HasPermissionToSendMail = false;

    $scope.LoadUserRights = function () {
        try {
            $scope.HasPermissionToDownloadFile = DocToolbarService.GetUserRights(10, 6);
            $scope.HasPermissionToSendMail = DocToolbarService.GetUserRights(164, 6);
            $scope.HasPermissionToPrint = DocToolbarService.GetUserRights(9, 6);; // DocToolbarService.GetUserRights(164, 6);
        } catch (e) {
            console.error(e);
        }
        
    };
    $scope.LoadUserRights();


    $scope.LoadCountAssociated = function () {
        let parentTaskId = 0;

        let parentResultId = GetDOCID();

        let partentEntityId = GetDocTypeId();

        if (partentEntityId == null) {
            parentTaskId = GetTASKID();
        }

        ZambaAssociatedService.getResults(parentResultId, partentEntityId, '', parentTaskId, false).then(function (result) {
            try {
                if (result != undefined) {
                    $scope.AssociatedCountButton = JSON.parse(result.data).data.length;
                } else {
                    $scope.AssociatedCountButton = 0;
                }

            } catch (e) {
                console.error('Error en obtener cantidad asociados');
                $scope.loading = false;
            }
        })
    };

    $scope.LoadCountAssociated();
});
