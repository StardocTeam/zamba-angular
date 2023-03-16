var RefreshParentDataFromChildWindowAsync = (NextRulesIds) =>
    new Promise((resolve, reject) => {
        try {
            var parentWindow = window.opener;
            if (parentWindow == undefined || parentWindow == null)
                parentWindow = window.parent;

            if (NextRulesIds != undefined && NextRulesIds != null && NextRulesIds != '') {
                var scope = angular.element($("#taskController")).scope();
                debugger;

                return scope.Execute_ZambaRuleAsync(NextRulesIds, [{ Docid: GetDOCID(), DocTypeid: GetDocTypeId() }]).then((res) => {
                    NextRulesIds = null;
                    debugger;

                    if (parentWindow.isSearchHtml())
                        parentWindow.RefreshResultsGridFromChildWindow();
                    else if (parentWindow.isTaskviewer())
                        parentWindow.RefreshParent(NextRulesIds);
                    else
                        parentWindow.RefreshParent(NextRulesIds);
                    resolve();
                });
            } else {
                debugger;

                if (parentWindow.isSearchHtml())
                    parentWindow.RefreshResultsGridFromChildWindow();
                else if (parentWindow.isTaskviewer())
                    parentWindow.RefreshParent(NextRulesIds);
                else
                    parentWindow.RefreshParent(NextRulesIds);
                resolve();
            }
        }
        catch (e) {
            debugger;

            console.error(e);
            reject(e);
        }
    });

function RefreshParentDataFromChildWindow(NextRulesIds) {
    try {
        var parentWindow = window.opener;

        if (parentWindow == undefined || parentWindow == null)
            parentWindow = window.parent;

        //var ucDocTypes_DocTypes = document.getElementById('ucDocTypes_DocTypes');
        //if (ucDocTypes_DocTypes != undefined) {
        //    var selectedDocTypeIDInsert = ucDocTypes_DocTypes.value;
        //    gridTarget = selectedDocTypeIDInsert;
        //}

        //if (gridTarget != undefined && gridTarget != '')
        //    parentWindow.LoadGrillaForm(gridTarget);
        // TODO: SM
        if (NextRulesIds != undefined && NextRulesIds != null && NextRulesIds != '') {
            var scope = angular.element($("#taskController")).scope();
            scope.Execute_ZambaRule(NextRulesIds, GetDOCID())
            NextRulesIds = null;
        }

        if (parentWindow.isSearchHtml())
            parentWindow.RefreshResultsGridFromChildWindow();
        else if (parentWindow.isTaskviewer())
            parentWindow.RefreshParent(NextRulesIds);
        else
            parentWindow.RefreshParent(NextRulesIds);

    } catch (e) {
        console.error(e);
    }
}

function OpenTaskOnBrowser() {
    angular.element($('#EntitiesCtrl')).scope().ScopeOpenTaskOnBrowser();
}

function GetDataKendoGrid(index) {
    Data = $(document.querySelector("#zamba_grid_index_all")).data().kendoGrid._data[index]; 
    return Data;
}


function ClosePreview() {
    angular.element($('#EntitiesCtrl')).scope().TogglePreview('noPreview');
}


function TaskUpdated(task) {
    try {


        var parentWindow = window.opener;

        if (parentWindow != null && parentWindow.RefreshRowTaskOnGrid)
            parentWindow.RefreshRowTaskOnGrid(task);

    } catch (e) {
        console.error(e);
    }
    return;
}


//----------------------------------------ASYNC MSG METHODS------------------------------------------
var PageHasToRefresh = false;
function RefreshFullPageAsync() {
    try {

        if (PageHasToRefresh !== undefined && PageHasToRefresh !== null && PageHasToRefresh === true) {
            PageHasToRefresh = false;
            window.location.reload();
        }
    } catch (e) {
        console.error(e);
    }
    return;
}

function RefreshParenTask() {
    try {
        var parentWindow = window.opener;
        if (parentWindow != null)
            parentWindow.PageHasToRefresh = true;
    } catch (e) {
        console.error(e);
    }
    return;
}


var TasksToRefresh = [];
function RefreshRowTaskOnGrid(task) {
    try {
        TasksToRefresh.push(task);
    } catch (e) {
        console.error(e);
    }
    return;
}
function RefreshRowTaskOnGridAsync() {
    try {
        let task = TasksToRefresh.pop();
        if (task != undefined && task != null) {
            var scope_entitiesCtrl = angular.element($("#EntitiesCtrl")).scope();
            if (scope_entitiesCtrl != null && scope_entitiesCtrl.Search != null) {
                var searchResults = scope_entitiesCtrl.Search.SearchResults;
                if (searchResults != undefined && searchResults != null) {
                    var indexRow = null;
                    for (i = 0; i <= searchResults.length - 1; i++) {
                        if (searchResults[i].Task_Id == task._taskID) {
                            indexRow = i;
                            break;
                        }
                    }
                    if (indexRow !== null) {
                        searchResults[indexRow].Asignado = task._usernameAsigned;
                        searchResults[indexRow].AsignedTo = task._usernameAsigned;
                        if (scope_entitiesCtrl.Search.SearchResultsObject != undefined && scope_entitiesCtrl.Search.SearchResultsObject != null) {
                            scope_entitiesCtrl.Search.SearchResultsObject.data[indexRow].Asignado = task._usernameAsigned;
                            scope_entitiesCtrl.Search.SearchResultsObject.data[indexRow].AsignedTo = task._usernameAsigned;

                            setTimeout(function () {
                                //                                scope_entitiesCtrl.saveSearchByView(scope_entitiesCtrl.Search);
                                RefreshKGrid(scope_entitiesCtrl.Search.SearchResultsObject, scope_entitiesCtrl.Search);
                            }, 10)


                        }
                    }
                }

            }
        }
    } catch (e) {
        console.error(e);
    }
    return;
}


var GridHasToRefresh = false;
function RefreshResultsGridFromChildWindowAsync() {
    try {
        if (GridHasToRefresh !== undefined && GridHasToRefresh !== null && GridHasToRefresh === true) {
            GridHasToRefresh = false;
            let ResultsCtrlScope = angular.element($("#EntitiesCtrl")).scope();
            if (ResultsCtrlScope != undefined && ResultsCtrlScope.currentMode.toLowerCase() != 'home')
                ResultsCtrlScope.RefreshCurrentResults();
        }
    } catch (e) {
        console.error(e);
    }
}

function RefreshResultsGridFromChildWindow() {
    try {
        GridHasToRefresh = true;
        return;
    } catch (e) {
        console.error(e);
    }
}

function RefreshService() {
    setInterval(RefreshRowTaskOnGridAsync, 20000);
    setInterval(RefreshResultsGridFromChildWindowAsync, 30000);
    setInterval(RefreshFullPageAsync, 40000);
}

RefreshService();


//----------------------------------------END ASYNC MSG METHODS------------------------------------------


function LoadGrillaForm(elemId) {
    try {

        var grilla = document.getElementsByTagName("zamba-associated");
        if (grilla != undefined) {
            CountGrilla = document.getElementsByTagName("zamba-associated").length;
            for (var i = 0; i < CountGrilla; i++) {
                grilla = document.getElementsByTagName("zamba-associated")[i].attributes["entities"].nodeValue;
                var gridAssocEntities = grilla.split(',')
                for (var ii = 0; ii < gridAssocEntities.length; ii++) {
                    if (gridAssocEntities[ii] == elemId && document.getElementsByTagName("zamba-associated")[i].querySelector(".BtnRefresh") != null ||
                        (gridAssocEntities[ii] == '' && document.getElementsByTagName("zamba-associated")[i].querySelector(".BtnRefresh"))) {
                        document.getElementsByTagName("zamba-associated")[i].querySelector(".BtnRefresh").click();
                    }
                }
            }
        }
    } catch (e) {
        console.error(e);
    }
}


function RefreshParent(NextRulesIds) {
    try {
        setTimeout(RefreshParentFromChildEvent, 500, NextRulesIds);
    } catch (e) {
        console.error(e);
    }
}

function RefreshParentFromChildEvent(NextRulesIds) {

    try {
        if (window.opener == null) {
            var ElementPreview = document.getElementsByClassName("ActualizarResultados");
            $(ElementPreview).click();
        } else {
            if (localStorage)
                localStorage.setItem('refreshFromChild', 'true');

            RefreshParentDataFromChildWindow();
            if (NextRulesIds != undefined && NextRulesIds != null && NextRulesIds != '') {
                var scope = angular.element($("#taskController")).scope();
                scope.Execute_ZambaRule(NextRulesIds, GetDOCID())
            } else {
                document.location.reload(true);
            }
        }
    } catch (e) {
        console.error(e);
    }
}
