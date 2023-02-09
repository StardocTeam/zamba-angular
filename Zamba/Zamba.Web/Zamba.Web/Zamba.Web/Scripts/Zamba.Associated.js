function RefreshParentDataFromChildWindow(NextRulesIds) {
    try {

        var parentWindow = window.opener;
        if (parentWindow == undefined || parentWindow == null) {
            parentWindow = window.parent;
        }
        if (parentWindow != undefined && parentWindow != null) {
            //Intenta tomar el DocTypeId de la URL, si no lo consigue lo busca en el hdn del formulario
            //var gridTarget = GetDocTypeId();
            //if (gridTarget == undefined) {
            //    if ($("[id$=hdnDocTypeId]") != undefined) {
            //        gridTarget = $("[id$=hdnDocTypeId]").val();
            //    }
            //}

            //var ucDocTypes_DocTypes = document.getElementById('ucDocTypes_DocTypes');
            //if (ucDocTypes_DocTypes != undefined) {
            //    var selectedDocTypeIDInsert = ucDocTypes_DocTypes.value;
            //    gridTarget = selectedDocTypeIDInsert;
            //}

            //if (gridTarget != undefined && gridTarget != '')
            //    parentWindow.LoadGrillaForm(gridTarget);
            // TODO: SM
            if (parentWindow.isSearchHtml())
                parentWindow.RefreshResultsGridFromChildWindow();
            else
                parentWindow.RefreshParent(NextRulesIds);
        }
    } catch (e) {
        console.error(e);
    }
}

function TaskUpdated(task) {
    console.time('TaskUpdated');

    var parentWindow = window.opener;

    if (parentWindow != null)
        parentWindow.RefreshRowTaskOnGrid(task);

    console.timeEnd('TaskUpdated');
    return;
}


function RefreshRowTaskOnGrid(task) {
    try {

        console.time('RefreshRowTaskOnGrid');
        var scope_entitiesCtrl = angular.element($("#EntitiesCtrl")).scope();
        if (scope_entitiesCtrl != null && scope_entitiesCtrl.Search != null) {
            var searchResults = scope_entitiesCtrl.Search.SearchResults;
            if (searchResults != undefined && searchResults != null) {
                var indexRow = 0;
                for (i = 0; i <= searchResults.length - 1; i++) {
                    if (searchResults[i].Task_Id == task._taskID) {
                        indexRow = i;
                        break;
                    }
                }

                scope_entitiesCtrl.Search.SearchResults[indexRow].Asignado = task._usernameAsigned;
                scope_entitiesCtrl.Search.SearchResults[indexRow].AsignedTo = task._usernameAsigned;
                scope_entitiesCtrl.Search.SearchResultsObject.data[indexRow].Asignado = task._usernameAsigned;
                scope_entitiesCtrl.Search.SearchResultsObject.data[indexRow].AsignedTo = task._usernameAsigned
            }
            console.timeEnd('RefreshRowTaskOnGrid');

            console.time('saveSearchByView');

            scope_entitiesCtrl.saveSearchByView(scope_entitiesCtrl.Search);
            console.timeEnd('saveSearchByView');
            console.time('RefreshKGrid');
            RefreshKGrid(scope_entitiesCtrl.Search.SearchResultsObject);
            console.timeEnd('RefreshKGrid');
        }

    } catch (e) {
        console.error(e);
    }
    return;
}

//function LoadGrillaForm(elemId) {
//    var grilla = document.getElementsByTagName("zamba-associated");
//    if (grilla != undefined) {
//        CountGrilla = document.getElementsByTagName("zamba-associated").length;
//        for (var i = 0; i < CountGrilla; i++) {
//            grilla = document.getElementsByTagName("zamba-associated")[i].attributes["entities"].nodeValue;
//            if (grilla == elemId && document.getElementsByTagName("zamba-associated")[i].querySelector(".BtnRefresh") != null) {
//                document.getElementsByTagName("zamba-associated")[i].querySelector(".BtnRefresh").click();
//            }
//        }
//    }
//}

function RefreshResultsGridFromChildWindow() {
    try {

        var ResultsCtrlScope = angular.element($("#EntitiesCtrl")).scope();
        if (ResultsCtrlScope != undefined && ResultsCtrlScope.currentMode.toLowerCase() != 'home')
            ResultsCtrlScope.RefreshCurrentResults();
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
        if (localStorage)
            localStorage.setItem('refreshFromChild', 'true');

        RefreshParentDataFromChildWindow();
        if (NextRulesIds != undefined && NextRulesIds != null && NextRulesIds != '') {
            var scope = angular.element($("#taskController")).scope();
            scope.Execute_ZambaRule(NextRulesIds, GetDOCID())
        } else {
            document.location.reload(true);
        }
    } catch (e) {
        console.error(e);

    }
}
