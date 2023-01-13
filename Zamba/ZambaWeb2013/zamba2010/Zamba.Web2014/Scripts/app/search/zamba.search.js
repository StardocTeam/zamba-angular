function GetTree(currentuserid, treeview) {

        $.ajax({
            url: "../../Services/IndexsService.asmx/GetTree",
            type: "POST",
            dataType: "json",
            cache: true,
            data: "{token:''," + "currentuserid:" + GetUID() + "}",
            contentType: "application/json; charset=utf-8",
            success: GetComplete,
            error: GetError
        });
    

    function GetComplete(data) {
        var kdata = $.parseJSON(data.d);
        $("#treeview").kendoTreeView({
            checkboxes: {
                checkChildren: true
            },
            check: onCheck,
            dataSource: kdata
        });
        onCheck();
    }

    function GetError(e) {
        console.log("Error: " + request.responseText + request.status + error);
    }



/* function that gathers IDs of checked nodes*/
function checkedNodeIds(nodes, checkedNodes, EntitiesIds) {
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].checked) {
                checkedNodes.push(nodes[i].NodeType + "-" + nodes[i].id);
                if (nodes[i].NodeType == "Entity") EntitiesIds.push(nodes[i].id);
            }

            if (nodes[i].hasChildren) {
                checkedNodeIds(nodes[i].children.view(), checkedNodes, EntitiesIds);
            }
        }
    }

    /* show checked node IDs on datasource change */
    function onCheck() {
        var checkedNodes = [],
            treeView = $("#treeview").data("kendoTreeView"),
            message;
        var EntitiesIds = [];

        var nodes = treeView.dataSource.view();

        checkedNodeIds(nodes, checkedNodes, EntitiesIds);

        if (checkedNodes.length > 0) {
            SetLastNodes(checkedNodes.join(","), EntitiesIds);
            message = "IDs of checked nodes: " + checkedNodes.join(",");
        } else {
            SetLastNodes('', EntitiesIds);
            message = "No nodes checked.";
        }


        $("#result").html(message);
    }

    function SetLastNodes(lastnodes, EntitiesIds) {

        var currentuserid = GetUID();

        $.ajax({
            url: "../../Services/IndexsService.asmx/GetLastNodes",
            type: "POST",
            dataType: "json",
            cache: true,
            data: "{LastNodes:'" + lastnodes + "',currentuserid:" + currentuserid + "}",
            contentType: "application/json; charset=utf-8",
            error: GetError
        });

        var scope = angular.element(document.getElementById("EntitiesController")).scope();
       // scope.$apply(function () {
        //NO ESTA DEFINIDO EN NINGUN LADO Y ARROJA ERROR REVISAR, SE COMENTA
        // scope.updateSelectedEntities(EntitiesIds.join(","));
      //  });


        function GetError(e) {
            console.log("Error: " + request.responseText + request.status + error);
        }
        
    }
    }


// Defining angularjs module
var app = angular.module('searchApp', ['ngRoute', 'ngResource', 'ngCookies'])
.directive("formatNumber", function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attr, modelCtrl) {
            modelCtrl.$formatters.push(function (modelValue) {
                return parseInt(modelValue);
            })
        .done(function (data) {
            // __doPostBack('ctl00$ContentPlaceHolder$UpdatePanel2', '');
            __doPostBack('ctl00$ContentPlaceHolder$btnClearIndexs', '');
        });
        }
    }

    $scope.currentEditField = null;
    // Edit Field details
    $scope.edit = function (data) {
        $scope.currentEditField = data;
        }

    // Cancel Field details
    $scope.cancel = function () {
        $scope.clear();
    }
     
    // Update Field details
    $scope.update = function () {
        if ($scope.Field.UserId != "") {
            $http({
                method: 'PUT',
                url: 'api/Fields/PutField/' + $scope.Field.UserId,
                data: $scope.Field
            }).then(function successCallback(response) {
                $scope.FieldsData = response.data;
                $scope.clear();
                alert("Field Updated Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    $scope.getField = function (propertyName, propertyValue, collection) {
        var found = $filter('getFieldsByProperty')(propertyName, propertyValue, collection);
        return found;
    }
});

app.factory('FieldsTypesService', function ($http) {
    var BaseURL = '';
    var fac = {};
    fac.GetAll = function () {
        return $http.get(thisDomain + 'api/FieldTypesApi');
    }
    return fac;
});
app.factory('EntityFieldsService', function ($http) {
    var BaseURL = '';
    var fac = {};
    fac.GetAll = function (SelectedEntitiesIds) {
        return $http.get(thisDomain + 'api/search/Indexs', {params: {token: '', SelectedEntitiesIds: SelectedEntitiesIds}});
    }
    return fac;
});

app.filter('getFieldsByProperty', function () {
    return function (propertyName, propertyValue, collection) {
        var i = 0, len = collection.length;
        for (; i < len; i++) {
            if (collection[i][propertyName] == +propertyValue) {
                return collection[i];
            }
        }
        return null;
    }
});


app.directive('searchindexslist', function () {
    return {
        restrict: 'E',
        transclude: true,
        link: function (scope, element, attributes) {
        },
        templateUrl: thisDomain + 'scripts/app/partials/partialSearchIndexs.html'
    };
});


//---------------------------------------------- END FieldS -------------------------------------------------------

//If Not Request.QueryString("mode") Is Nothing AndAlso Request.QueryString("mode") = "ajax" Then
//'Saving the variables in session. Variables are posted by ajax.

//If Not Request.Params("SelectedsDocTypesIds") Is Nothing Then
//Dim SelectedsDocTypesIds As New List(Of Int64)
//                For Each Id As String In Request.Params("SelectedsDocTypesIds").Split(",")
//                    If IsNumeric(Id) Then SelectedsDocTypesIds.Add(Int64.Parse(Id))
//                Next
//                Session("SelectedsDocTypesIds") = SelectedsDocTypesIds
//                ShowIndexs(SelectedsDocTypesIds, WebModuleMode.Search)
//                Me.UpdatePanel2.Update()
//            End If
//        End If




//    Protected Sub btnClearIndexs_Click(sender As Object, e As EventArgs)
//        If Not Session Is Nothing AndAlso Not Session("SelectedsDocTypesIds") Is Nothing AndAlso Session("SelectedsDocTypesIds").Count > 0 Then   'Se crean de vuelta los atributos
//ShowIndexs(Session("SelectedsDocTypesIds"), WebModuleMode.Search)
//'Se Actualizan los valores de los controles del UpdatePanel
//UpdatePanel2.Update()
//End If
//TxtTextSearch.Text = [String].Empty

//' Si se encuentra visible el mensaje "No se encontraron resultados"
//If NoResults.Visible Then
//' Entonces se oculta
//NoResults.Visible = False
//End If
//End Sub

//Private Sub ShowIndexs(DocTypesIds As List(Of Int64), Mode As WebModuleMode)
//    If DocTypesIds Is Nothing OrElse DocTypesIds.Count = 0 Then
//        Dim ind As Index() = New Index(-1) {}
//DocTypesIndexs.Clear()
//lblErrorIndex.Text = "No hay elementos seleccionados para realizar la busqueda"
//lblErrorIndex.Visible = True
//Return
//End If





//Dim Rights As New SRights()
//Try

//Dim indexList = New List(Of IIndex)()

//'Si seleccionó algún documento
//If DocTypesIds.Count > 0 Then
//Dim indexs As IEnumerable(Of Zamba.Core.Index) = GetindexSchemaNew(DocTypesIds)

//Dim viewSpecifiedIndex As Boolean = True
//Dim docTypesIds64 = New List(Of Int64)()

//For Each id As Int32 In DocTypesIds
//    docTypesIds64.Add(id)

//    'Si se hace una busqueda combinada, si algun doctype tiene permiso para no filtrar indices
//    'Bastaria para aplicar ese permiso a todos
//    Dim permisosFiltrarIndices As Boolean = Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, id)

//    If permisosFiltrarIndices = False Then
//        viewSpecifiedIndex = False
//    End If
//Next

//If viewSpecifiedIndex Then
//    Dim user As IUser = Session("User")
//    Dim iri As Hashtable = Rights.GetIndexsRights(docTypesIds64, user.ID, True)

//    For Each currentIndex As Zamba.Core.Index In indexs
//        If DirectCast(iri(currentIndex.ID), Zamba.Core.IndexsRightsInfo).Search Then
//            indexList.Add(currentIndex)
//        End If
//    Next
//Else
//    indexList.AddRange(indexs)
//End If

//'If Not DocTypesControl.GotSelectedIndexs() Then
//'    indexList = New List(Of IIndex)()
//'    Session("SelectedsDocTypesIds") = New List(Of Int64)()
//'End If

//DocTypesIndexs.DtId = DocTypesIds(0)
//End If

//DocTypesIndexs.ShowIndexs(indexList, Mode)
//lblErrorIndex.Visible = False
//Catch ex As Exception
//Zamba.AppBlock.ZException.Log(ex)
//End Try

//End Sub

//Private Function GetindexSchemaNew(docTypesIds As List(Of Int64)) As IEnumerable(Of Zamba.Core.Index)

//Dim indexs As List(Of IIndex)

//'IndexsBusiness IndexBusinessObj = new IndexsBusiness(user);

//Dim ar = New ArrayList()
//ar.AddRange(docTypesIds)
//indexs = ZCore.GetInstance().FilterSearchIndex(ar)

//Dim clonedIndexs = New Zamba.Core.Index(indexs.Count - 1) {}
//Dim contador As Int32 = 0

//For Each ind As Zamba.Core.Index In indexs
//Dim newIndex = ind
//clonedIndexs(contador) = newIndex
//contador += 1
//Next

//Return clonedIndexs
//End Function