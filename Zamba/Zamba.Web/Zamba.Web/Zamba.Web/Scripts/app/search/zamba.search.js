var ZambaIndexsValuesMemory = [];
var timerResizeButtonSearch;
var LimitToSlst = 20;

/*global someFunction, app*/
/*eslint no-undef: "error"*/
app.factory('lastNodeObj', function () {
    return {
        LastNodes: '',
        UserId: 0,
    };
});


app.factory('UserOptions', function () {
    return {
        visualizerCurrentModeGS: 'none',
    };
});


app.constant("searchTypes", {
    "search": 1,
    "tasks": 2,
    "processes": 3,
})


app.factory('UserRightsFactory', function () {
    return {
        CanShowInsertPanel: false,
        CanShowAnchorSidebar: false,
        CanUsebtnFlotante: false,
        CanShowMyTasksPanel: false,
        CanShowHomePanel: false,
        CanShowSearchPanel: false,
        CanShowTreeViewPanel: false
    };
});

app.factory('SearchState', function () {
    return {
        SearchId: 0,
        OrganizationId: 0,
        DoctypesIds: [],
        Indexs: [],
        blnSearchInAllDocsType: true,
        TextSearchInAllIndexs: '',
        RaiseResults: false,
        ParentName: '',
        CaseSensitive: false,
        MaxResults: 100,
        ShowIndexOnGrid: true,
        UseVersion: false,
        UserId: 0,
        GroupsIds: [],
        StepId: 0,
        StepStateId: 0,
        TaskStateId: 0,
        WorkflowId: 0,
        NotesSearch: '',
        Textsearch: '',
        SearchResults: null,
        OrderBy: null,
        Filters: [],
        UserAssignedId: -1,
        lastFiltersByView: new Map()
    };
});

app.factory('TaskState', function () {
    return {
        SearchId: 0,
        OrganizationId: 0,
        DoctypesIds: [],
        Indexs: [],
        blnSearchInAllDocsType: true,
        TextSearchInAllIndexs: '',
        RaiseResults: false,
        ParentName: '',
        CaseSensitive: false,
        MaxResults: 100,
        ShowIndexOnGrid: true,
        UseVersion: false,
        UserId: 0,
        GroupsIds: [],
        StepId: 0,
        StepStateId: 0,
        TaskStateId: 0,
        WorkflowId: 0,
        NotesSearch: '',
        Textsearch: '',
        SearchResults: null,
        OrderBy: null,
        Filters: [],
        UserAssignedId: -1,
        lastFiltersByView: new Map()
    };
});

app.factory('ProcessState', function () {
    return {
        SearchId: 0,
        OrganizationId: 0,
        DoctypesIds: [],
        Indexs: [],
        blnSearchInAllDocsType: true,
        TextSearchInAllIndexs: '',
        RaiseResults: false,
        ParentName: '',
        CaseSensitive: false,
        MaxResults: 100,
        ShowIndexOnGrid: true,
        UseVersion: false,
        UserId: 0,
        GroupsIds: [],
        StepId: 0,
        StepStateId: 0,
        TaskStateId: 0,
        WorkflowId: 0,
        NotesSearch: '',
        Textsearch: '',
        SearchResults: null,
        OrderBy: null,
        Filters: [],
        UserAssignedId: -1,
        lastFiltersByView: new Map()
    };
});

app.factory('Search', function () {

    return {
        SearchId: 0,
        OrganizationId: 0,
        DoctypesIds: [],
        Indexs: [],
        blnSearchInAllDocsType: true,
        TextSearchInAllIndexs: '',
        RaiseResults: false,
        ParentName: '',
        CaseSensitive: false,
        MaxResults: 100,
        ShowIndexOnGrid: true,
        UseVersion: false,
        UserId: 0,
        GroupsIds: [],
        StepId: 0,
        StepStateId: 0,
        TaskStateId: 0,
        WorkflowId: 0,
        NotesSearch: '',
        Textsearch: '',
        SearchResults: null,
        OrderBy: null,
        Filters: [],
        UserAssignedId: -1,
        crdateFilters: [],
        lupdateFilters: [],
        nameFilters: [],
        originalFilenameFilters: [],
        stateFilters: [],
        UserAssignedFilter: { IsChecked: true, zFilterWebID: 0 },
        StepFilter: { IsChecked: true, zFilterWebID: 0 },
        lastFiltersByView: new Map()
    };
});

app.factory('FieldsService', function ($http) {
    var BaseURL = '';
    var fac = {};
    fac.GetAll = function (IndexId) {
        return $http.post(ZambaWebRestApiURL + '/search/FillIndex?IndexId=' + IndexId).then(function (response) {
            return response;
        },
            function (response) {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
                $scope.message = "Failed to register  due to:" + errors.join(' ');
            });
    }
    return fac;
});



app.controller('maincontroller', function ($scope, $attrs, $http, $compile, EntityFieldsService, Search, lastNodeObj, $element, $timeout, $filter, $rootScope, authService, uiService, ruleExecutionService, ZambaUserService, UserOptions, searchTypes, SearchState, TaskState, ProcessState, UserRightsFactory, ModalVisualPreferencesService, $translate, SearchObjectService, SearchFilterService, treeViewServices) {

    $scope.lastSearchEntitiesNodes;
    $scope.Search = Search;
    $scope.SearchState = SearchState;
    $scope.showSearchBtn = true;
    //#region LoadPage

    var checkAuthenticationIE = setInterval(function () {
        if (authService.authentication.isAuth == true) {
            clearInterval(checkAuthenticationIE);

            $scope.LoadUserRights();
            $scope.LoadTaskFilterRights();

            setTimeout($scope.GetEntities, 2);



            setTimeout($scope.SetPreviewDocumentBlank, 200);


            setTimeout($scope.CheckIfExecuteSearchByQueryStringOrLoadDefaultView, 50);

        }
    }, 100);

    //#endregion LoadPage



    $scope.EntitiesCheckEnable = false;
    $scope.EntityCheckTime = null
    $scope.EntityCheckDelay = 1800;

    $scope.ValidateTask = false;

    $scope.PreviewMode = "noPreview";
    $scope.PreviewerUsedBy = null;
    $scope.currentPreviewInTab = false;
    $scope.LayoutPreview = "row";
    $scope.VisualizerMode = null;

    var TmrZambaSave = null;
    //#region User Rights

    $scope.CheckUserToken = function () {

        var token = authService.fillAuthData();
        if (token == "invalid user" || token == undefined) {
            authService.logOut();
            return;
        }

    }



    $scope.LoadUserRights = function () {
        try {

            $scope.ShowAsociatedTab = $scope.NewGetUserRigths(RightsTypeEnum.Use, ObjectTypesEnum.WFStepsTree);

            $scope.CanChangePassword = $scope.NewGetUserRigths(RightsTypeEnum.ChangePassword, ObjectTypesEnum.LogIn);
            $scope.ShowSendMailByResults = $scope.NewGetUserRigths(RightsTypeEnum.EnviarPorMailWeb, ObjectTypesEnum.Documents);
            $scope.ShowDownloadFileBtnPreview = $scope.NewGetUserRigths(RightsTypeEnum.Saveas, ObjectTypesEnum.Documents);
            $scope.IsAdminUser = $scope.NewGetUserRigths(RightsTypeEnum.Delete, ObjectTypesEnum.Cache);

            $scope.ShowTreeView = $scope.NewGetUserRigths(RightsTypeEnum.ShowWFTreeView, ObjectTypesEnum.WFTask);

            UserRightsFactory.CanShowTreeViewPanel = $scope.ShowTreeView;
            UserRightsFactory.CanShowInsertPanel = $scope.NewGetUserRigths(RightsTypeEnum.View, ObjectTypesEnum.InsertWeb);

            UserRightsFactory.CanShowAnchorSidebar = $scope.NewGetUserRigths(RightsTypeEnum.View, ObjectTypesEnum.ShowAnchorSidebar);
            UserRightsFactory.CanUsebtnFlotante = $scope.NewGetUserRigths(RightsTypeEnum.View, ObjectTypesEnum.UsebtnFlotante);

            UserRightsFactory.CanShowMyTasksPanel = $scope.NewGetUserRigths(RightsTypeEnum.ShowMyTask, ObjectTypesEnum.WFTask);
            UserRightsFactory.CanShowHomePanel = $scope.NewGetUserRigths(RightsTypeEnum.ShowHome, ObjectTypesEnum.HomeWeb);
            UserRightsFactory.CanShowSearchPanel = true;
            $rootScope.$broadcast('sidebarPermissionLoaded');
            $rootScope.$broadcast('PermissionLoaded');

            var usrController = angular.element($('#usrController')).scope();
            $scope.EntitiesSelectionExclusive = usrController.getUserPreferencesSync('EntitiesSelectionExclusive', false);
            $scope.OpenTaskOnOneResult = usrController.getUserPreferencesSync('OpenTaskOnOneResult', true);

            if ($scope.EntitiesSelectionExclusive == true)
                $scope.EntityCheckDelay = 1;
            else
                $scope.EntityCheckDelay = 1500;

        } catch (e) {
            console.error(e);
        }
    }

    $scope.LoadTaskFilterRights = function () {

        try {
            var userid = GetUID();

            if (userid != null && userid != undefined && userid != 0) {
                var TaskFilterConfig = window.localStorage.getItem("TaskFilterConfig-" + GetUID());

                if (TaskFilterConfig == undefined || TaskFilterConfig == null || TaskFilterConfig == '') {

                    TaskFilterConfig = uiService.LoadTaskFiltersConfig()

                    $scope.SettasksFilters(TaskFilterConfig);


                }
                else {
                    $scope.SettasksFilters(TaskFilterConfig);
                }
            }

        } catch (e) {
            console.error(e);
        }
    };

    //#endregion User Rights

    //#region Layout

    $scope.ShowchkMyTasks = true;
    $scope.ShowchkMyTeam = true;
    $scope.ShowchkMyAllTeam = true;
    $scope.ShowchkViewAllMy = true;

    $scope.ShowAsociatedTab = false;
    $scope.ShowTreeView = false;
    $scope.ShowSendMailByResults = false;
    $scope.ShowDownloadFileBtnPreview = false;
    $scope.chkMyTasks = true;
    $scope.chkMyTeam = false;
    $scope.chkMyAllTeam = false;
    $scope.chkViewAllMy = false;


    $scope.MyTasksText = 'Mis Tareas';
    $scope.MyTeamTasksText = 'Tareas del Equipo';
    $scope.MyAllTeamTasksText = 'Todo el Equipo';
    $scope.AllTasksText = 'Mis Casos';

    $scope.IdsAllTasks = '2523';
    $scope.MydocumentTask = '';

    $scope.visualizerModeGSFn = function (_this, mode) {

        UserOptions.visualizerCurrentModeGS = mode;
        ZambaUserService.VisualizerMode = mode;
        $scope.VisualizerMode = mode;
        var KGrid = $("#Kgrid").data("kendoGrid");

        var isGlobalSearch = false;
        if (_this !== null && _this !== undefined && $(_this).parents("#Advfilter1").length) {
            isGlobalSearch = true;
        }

        $("#resultsGridSearchBox").hide();
        $("#resultsGridSearchBoxThumbs").hide();
        $("#resultsGridSearchBoxPreview").hide();
        $("#resultsGridSearchGrid").hide();
        $("#Kgrid").hide();

        $("#multipleSelectionMenu").find(".activeButtonIconBar").click();


        $("#multipleSelectionPreview").find(".activeButtonIconBar").click();
        if ($("#chkThumbGrid").hasClass("ng-not-empty")) {
            $("#chkThumbGrid").click();
        }

        $(".filterFunc").show();

        //$scope.DisableActions();
        $(".glyphicon.ngtitle.glyphicon-ok-circle").css("background-color", "#4285f4");
        DisableActions();
        $(".switch").hide()
        switch (mode) {
            case "grid":
                $scope.PreviewMode = "noPreview";
                $scope.LayoutPreview = "row";
                $("#resultsGridSearchGrid").show();
                $("#Kgrid").show();
                $(".btn-preview").removeClass("md-btn-green");
                $(".btn-thumb").removeClass("md-btn-green");
                $(".btn-grid").addClass("md-btn-green");
                ResizeResultsArea();
                $(".switch").show();
                break;
            case "preview":
                $scope.PreviewMode = "noPreview";
                $scope.LayoutPreview = "row";
                $("#resultsGridSearchGrid").show();
                $("#resultsGridSearchBoxPreview").css('display', 'inline-block');
                $(".btn-thumb").removeClass("md-btn-green");
                $(".btn-grid").removeClass("md-btn-green");
                $(".btn-preview").addClass("md-btn-green");
                if ($scope.Search.SearchResults.length > 0 && $scope.Search.LastPage == 0) {
                    var currentresult = $scope.Search.SearchResults[0];
                    $scope.previewItem(currentresult, -1);
                }
                ResizeResultsArea();
                break;
            case "list":
                $scope.PreviewMode = "noPreview";
                $scope.LayoutPreview = "row";
                $("#resultsGridSearchBox").show();
                $("#resultsGridSearchGrid").show();
                ResizeResultsArea();
                break;
            case "thumbs":
                $scope.PreviewMode = "noPreview";
                $("#resultsGridSearchBoxThumbs").css("height", "75%").show();
                $("#resultsGridSearchGrid").show();
                $(".btn-preview").removeClass("md-btn-green");
                $(".btn-grid").removeClass("md-btn-green");
                $(".btn-thumb").addClass("md-btn-green");
                ResizeResultsArea();
                break;
        }
        ////Entendiendo que siempre como primera busqueda mostrara el modo visual "Grilla", se establece la siguiente condicion.
        ////Si ninguno de los modos esta seteado, muestra el switch.
        //if (!$("#visualizerModeGS.btn-grid").hasClass("BtnGridStyle") &&
        //    !$("#visualizerModeGS.btn-thumb").hasClass("BtnGridStyle") &&
        //    !$("#visualizerModeGS.btn-preview").hasClass("BtnGridStyle")) {
        //    $(".switch").show();
        //}
        ////si ya se habia seteado posteriormente el modo grilla, mostrara el switch.
        //else if ($("#visualizerModeGS.btn-grid").hasClass("BtnGridStyle")) {
        //    //$(".switch").show();
        //}

        //AdjustGridColumns();
    }


    //#region Layout Helpers


    function setMdBtnColorActive() {
        //solo para ShowchkMyTasks.
        $("#ShowchkMyTasks").addClass('md-btn-' + "warning" + ' active');
        $("#ShowchkMyTasks").removeClass('md-btn-basic btn-disabled');
    }

    function unSetMdBtnColorActive() {
        //solo para ShowchkMyTasks.
        $("#ShowchkMyTasks").addClass('md-btn-basic btn-disabled');
        $("#ShowchkMyTasks").removeClass('md-btn-' + "warning" + ' active')
    }

    //#endregion Layout Helpers

    //#endregion Layout

    //#region SearchTreeview

    $scope.GetEntities = function () {
        $scope.LoadingEntities = true;
        try {

            if (enableGlobalSearch != undefined && enableGlobalSearch) {
                var userId = parseInt(GetUID());
                if (isNaN(userId)) {
                    //$("#Advfilter1").hide();
                    //GSRedirectToLogin(); //bootbox.alert("Usuario incorrecto");
                    $scope.LoadingEntities = false;
                }
                else {
                    if (window.localStorage) {
                        var localEntities = window.localStorage.getItem('localEntities' + userId);
                        if (localEntities != undefined && localEntities != null && localEntities != '' && localEntities.length > 0) {
                            try {
                                var data = JSON.parse(localEntities);
                                if (data == null || data.length == 0) {
                                    $scope.LoadEntitiesFromDB();
                                    console.log("localEntities is null");
                                }
                                else {
                                    $scope.availableSearchParams = data;
                                    if ($scope.availableSearchParams != undefined) {
                                        LoadSearchTreeView();
                                        $scope.LoadingEntities = false;
                                    }
                                    else {
                                        $scope.LoadingEntities = false;
                                        console.log("localEntities is null");
                                    }

                                }

                            } catch (e) {
                                console.error(e);
                                $scope.LoadEntitiesFromDB();
                            }
                        }
                        else {
                            $scope.LoadEntitiesFromDB();
                        }

                    }
                    else {
                        $scope.LoadEntitiesFromDB();
                    }

                }
            }
        }
        catch (e) {
            console.error(e);
            $scope.LoadingEntities = false;
        }
    };

    $scope.LoadEntitiesFromDB = function () {
        var userId = parseInt(GetUID());

        $http({
            method: 'GET',
            url: ZambaWebRestApiURL + "/Search/Entities",
            crossDomain: true, // enable this            
            params: { userId: userId },
            dataType: 'json',
            headers: { 'Content-Type': 'application/json' }
        }).
            then(function (data, status, headers, config) {
                if (data == null) {
                    $scope.availableSearchParams = data.data;
                    try {
                        if (window.localStorage) {
                            window.localStorage.setItem('localEntities' + userId, JSON.stringify(data.data));
                        }
                    }
                    catch (e) {
                        console.error(e);
                        if (e.message.indexOf('exceeded the quota') != -1) {
                            window.localStorage.clear();
                        }

                    }
                    if ($scope.availableSearchParams != undefined) {
                        LoadSearchTreeView();
                    }
                    else {
                        console.log("localEntities is null");
                    }
                    bootbox.alert("No se encontraron entidades asignadas");
                }
                else {
                    $scope.availableSearchParams = data.data;
                    try {
                        if (window.localStorage) {
                            window.localStorage.setItem('localEntities' + userId, JSON.stringify(data.data));
                        }
                    }
                    catch (e) {
                        console.error(e);
                        if (e.message.indexOf('exceeded the quota') != -1) {
                            window.localStorage.clear();
                        }

                    }

                    if ($scope.availableSearchParams != undefined) {
                        LoadSearchTreeView();
                    }
                    else {
                        console.log("localEntities is null");
                    }
                }
                $scope.LoadingEntities = false;

            }).
            catch(function (data, status, headers, config) {
                $scope.LoadingEntities = false;
                $scope.message = data;
                if (data.Message != undefined) {
                    console.error(data.Message);
                }
            });
    }


    //#endregion SearchTreeview

    $scope.currentMode = 'loading';

    //#region Query String Search


    $scope.CheckIfExecuteSearchByQueryStringOrLoadDefaultView = function () {
        try {
            var parameters = [];

            $scope.onSelectionMode = false;
            $scope.thumbSelectedIndexs = [];
            $scope.Search.CreatedTodayCount = 0;

            function parameter(editMode, id, name, type, operator, value, placeholder, value2) {
                this.color = "b1";
                this.editMode = editMode;
                this.groupnum = 1;
                this.id = id;
                this.maingroup = type == 0 ? true : editMode;
                this.name = name || "";
                this.type = type;//0: palabra
                this.operator = operator || "=";
                this.placeholder = placeholder || "";
                this.value = value || "";
                this.value2 = value2 || "";
            }

            function GetQSAttribute(d) {

                var parameters = [];
                var parametersIndexs = [];

                var typesURL = URLParam.Types();
                var searchsURL = URLParam.Search();
                var attributesURL = URLParam.Attr();

                var types = typesURL.split(",")
                var attributes = attributesURL.split(",")
                var searchs = searchsURL.split(",")

                var attrExists = false;


                for (var h = 0; h < types.length; h++) {

                    for (var i = 0; i < d.length; i++) {

                        var type = d[i];

                        if (types[h] == type.id) {

                            attrExists = false;


                            for (var k = 0; k < attributes.length; k++) {

                                for (var j = 0; j < type.indexes.length; j++) {

                                    var index = type.indexes[j];
                                    if (attributes[k] == index.id) {
                                        if (parametersIndexs.indexOf(attributes[k]) == -1) {

                                            parameters.push(new parameter(true, attributes[k], index.name, 2, "=", searchs[k]));
                                            parametersIndexs.push(attributes[k]);
                                            attrExists = true;
                                            break;
                                        }
                                        else {
                                            attrExists = true;
                                        }
                                    }

                                    // falta seleccionar las entidades
                                    //  $scope.setSearchEntites(types.join(','));

                                    $scope.Search.DoctypesIds = types.join(',');

                                    //Asigno a los atributos del search de las entidades
                                    for (var i in $scope.Search.Indexs) {
                                        if (attributes[k] == $scope.Search.Indexs[i].id) {
                                            $scope.Search.Indexs[i].Data = searchs[k];
                                            $scope.Search.usedFilters.push($scope.Search.Indexs[i].Name);
                                            break;
                                        }
                                    }
                                }
                            }

                            if (!attrExists) {
                                throw "No se encontraron los atributos para el type " + type.id;
                            }

                            parameters.push(new parameter(false, types[h], type.name, 1));
                            break;
                        }
                    }
                }

                return parameters;
            }

            var URLParam = {
                Types: function () {
                    return getUrlParameters().types || "";
                },
                Attr: function () {
                    return getUrlParameters().attr || "";
                },
                Search: function () {
                    return getUrlParameters().search || "";
                }
            };



            if (URLParam.Attr() == "" && URLParam.Types() == "") {
                //Busqueda solo por palabras
                var txt = URLParam.Search();
                if (txt)
                    parameters.push(new parameter(false, 0, txt, 0, "Empieza", txt));
            }
            else {
                try {
                    while ($scope.LoadingEntities == true) {

                    }
                    var parameters = GetQSAttribute($scope.availableSearchParams);
                } catch (e) {
                    console.error(e);
                    toastr.error(e, "ERROR");
                }

                if (parameters == undefined) {
                    bootbox.alert("Parametros incorrectos");
                    return;
                }
            }

            if (parameters.length) {
                $scope.DoSearchByQS(parameters);
            }
            else {
                setTimeout($scope.GetMainMenuItem, 500);
            }
        }
        catch (e) {
            console.error(e);
        }

    };

    //#endregion

    //#region GetMainMenuItem
    $scope.GetMainMenuItem = function () {
        try {
            var DV;
            if (sessionStorage.getItem('lastmainmenuitem|' + GetUID()) == undefined && sessionStorage.getItem('lastmainmenuitem|' + GetUID()) == null) {
                DV = ModalVisualPreferencesService.GetMainMenuItem();
            } else {
                DV = sessionStorage.getItem('lastmainmenuitem|' + GetUID());
            }


            if (DV == undefined) {
                DV = 'search';
            }

            if (DV == 'MyProcess' && $scope.ShowTreeView == false) {
                DV = 'search';
            }

            searchModeGSFn(null, DV);
            $("." + DV).addClass("Selected");
        } catch (e) {
            console.error(e);
            searchModeGSFn(null, 'search');
            $("." + DV).addClass("Selected");
        }
    }
    //#endregion






    $scope.ResetTasksSearch = function () {

        $("#chkMyTeam").prop('checked', false);
        $("#chkMyTasks").prop('checked', false);
        $("#chkMyAllTeam").prop('checked', false);
        $("#chkViewAllMy").prop('checked', false);

        updateDisplay($("#chkViewAllMy"))
        updateDisplay($("#chkMyTasks"))
        updateDisplay($("#chkMyTeam"))
        updateDisplay($("#chkMyAllTeam"))


        $scope.Search.AsignedTasks = true;
        $scope.Search.Filters = [];
        $scope.Search.DoctypesIds = '';

        $scope.Search.View = "MyTasks";

        $scope.Search.usedFilters = [];
        $scope.Search.filter = [];

        $scope.Search.SearchResults = null;
        $scope.Search.SearchResultsObject = null;
        $scope.Search.GroupsIds = [];

        $scope.Search.OrderBy = '';
        $scope.Search.columnFiltering = false;

        $scope.Search.LastPage = 0;
        SearchFrom = "Mytask";

        $scope.Search.StepId = 0;
        $scope.Search.stateID = 0;
        $scope.lastSelectedNode = 0;

    }

    $scope.ResetProcessSearch = function () {
        $scope.chkMyTasks = false;
        $scope.chkMyAllTeam = false;
        $scope.chkMyTeam = false;
        $scope.chkViewAllMy = true;


        $scope.Search.AsignedTasks = true;
        $scope.Search.Filters = [];
        $scope.Search.DoctypesIds = '';
        $scope.Search.Indexs = [];

        $scope.Search.View = "MyProcess";

        $scope.Search.usedFilters = [];
        $scope.Search.filter = [];

        $scope.Search.SearchResults = null;
        $scope.Search.SearchResultsObject = null;
        $scope.Search.GroupsIds = [];

        $scope.Search.OrderBy = '';
        $scope.Search.columnFiltering = false;

        $scope.Search.LastPage = 0;
        SearchFrom = "MyProcess";
    }


    //#region SET LAYOUT MODE


    var TaskLoaded = false;

    $scope.WFSideBarIsOpen = true;

    $scope.ToggleWFSideBar = function () {
        //si el panel del treeview esta abierto
        if ($scope.WFSideBarIsOpen == true) {
            $scope.CloseWFSideBar();
        }
        else {
            $scope.OpenWFSideBar();
        }
    };

    $scope.OpenWFSideBar = function () {
        try {
            $scope.WFSideBarIsOpen = true;
            document.getElementById("SidebarTree").style.width = "300px";
            document.getElementById("ResultsCtrl").style.marginLeft = "300px";
            //  $('#iconBtnOpenTreeview').removeClass('revertIcon');
        } catch (e) {
            console.error(e);
        }
    }

    $scope.CloseWFSideBar = function () {
        try {
            $scope.WFSideBarIsOpen = false;
            document.getElementById("SidebarTree").style.width = "0";
            document.getElementById("ResultsCtrl").style.marginLeft = "0";
            // $('#iconBtnOpenTreeview').addClass('revertIcon');
        } catch (e) {
            console.error(e);
        }
    }



    $scope.currentModeSearch = 'search';

    $scope.GoBackToSearch = function () {
        $scope.currentModeSearch = 'search';
        $("#tabresults").hide();
        $("#SearchControls").show();
        $("#multipleSelectionMenu").find(".activeButtonIconBar").click();
        if ($("#chkThumbGrid").hasClass("ng-not-empty")) {
            $("#chkThumbGrid").click();
        }
    }

    $scope.GoBackToSearchResults = function () {
        $scope.searchModeGSFn(null, 'search');
        $scope.currentModeSearch = 'results';
        $("#SearchControls").hide();
        $("#tabresults").show();
        if ($('#ModalSearch2').hasClass('in')) {
            $("#ModalSearch2").modal('hide');
        }

        if ($('#Kgrid').css('display') === "block") {
            setTimeout(ResizeResultsArea, 500);
            AdjustGridColumns();
        }
    }
    $scope.GoHomeForHistory = function (_this, mode) {
        $scope.searchModeGSFn(_this, mode);
        setTimeout(function () { $("md-tab-item")[0].click(); }, 1000);
    }
    $scope.searchModeGSFn = function (_this, mode) {

        $scope.PreviewMode = "noPreview";
        $scope.LayoutPreview = "row";

        if ($scope.currentMode != mode && $scope.currentMode != 'loading') {
            ModalVisualPreferencesService.SetLastMainMenuItem(mode);
        }
        console.log('SetLastMainMenuItem');

        $scope.currentMode = mode;
        $scope.Search.View = mode;

        if (angular.element($("#taskController")).scope() != undefined) {
            angular.element($("#taskController")).scope().actionRules = null;
        }

        $("#SearchControls").hide();
        $("#tabresults").hide();

        $("#tabInsert").hide();

        if ($("#chkThumbGrid").hasClass("ng-not-empty")) {
            $("#chkThumbGrid").click();
        }

        switch (mode) {
            case "MyTasks":

                if ($scope.LayoutPreview == "row") {
                    resizeGridHeight();
                } else if ($scope.LayoutPreview == "column") {
                    resizeTabHome();
                }

                TaskLoaded = false;
                $("#resultsGridSearchBox").hide();
                $("#resultsGridSearchBoxThumbs").hide();
                $("#resultsGridSearchBoxPreview").hide();

                $("#tabresults").show();
                $("#resultsGridSearchGrid").show();
                $("#Kgrid").show();
                $(".ActualizarResultados").css("display", "inline-block");


                if (TaskLoaded == false) {
                    $scope.ResetTasksSearch();
                    $scope.CleanAllInputs();
                    var LoadedFromLocal = $scope.loadSearchFromLocal();

                    var entitiesWithResults = $scope.Search.SearchResultsObject.entities.filter(function (e) { return e.ResultsCount > 0 });
                    if (entitiesWithResults.length == 1) {
                        $scope.Search.DoctypesIds = [];
                        $scope.Search.DoctypesIds.push($scope.Search.SearchResultsObject.entities[0].id)
                        $scope.sumNonIndexedFilters();
                    }

                    if ($scope.Search.View.indexOf('MyTasks') != -1) {
                        $scope.ValidateCheckMyTasks(!LoadedFromLocal);
                    } else if ($scope.Search.View.indexOf('MyTeam') != -1) {
                        $scope.ValidateCheckMyTeam(!LoadedFromLocal);
                    } else if ($scope.Search.View.indexOf('MyAllTeam') != -1) {
                        $scope.ValidateCheckMyAllTeam(!LoadedFromLocal);
                    } else if ($scope.Search.View.indexOf('ViewAllMy') != -1) {
                        $scope.ValidateCheckAllMy(!LoadedFromLocal);
                    } else {
                        $scope.ValidateCheckMyTasks(!LoadedFromLocal);
                    }


                    if ($('#ModalSearch2').hasClass('in')) {
                        $("#ModalSearch2").modal('hide');
                    }

                    $scope.WFSideBarIsOpen = false;
                }

                break;
            case "MyProcess":

                if ($scope.LayoutPreview == "row") {
                    resizeGridHeight();
                } else if ($scope.LayoutPreview == "column") {
                    resizeTabHome();
                }

                $("#resultsGridSearchBox").hide();
                $("#resultsGridSearchBoxThumbs").hide();
                $("#resultsGridSearchBoxPreview").hide();

                var LoadedFromLocal = $scope.loadSearchFromLocal();
                $scope.ValidateCheckForTreeView();

                try {
                    if (($scope.Search.lastNodeSelected = treeViewServices.GetLastWFSelected("MyProcess")) != undefined) {
                        var nodeIDs = $scope.Search.lastNodeSelected.split('-');
                        var nodeId = parseInt(nodeIDs[nodeIDs.length - 1]);
                    }
                    $scope.Search.DoctypesIds = SearchObjectService.GetStepEntities(nodeId);

                } catch (e) {

                }

                if (!LoadedFromLocal) {
                    $scope.CleanAllInputs();
                    $scope.ResetProcessSearch();
                }

                $("#tabresults").show();
                $("#resultsGridSearchGrid").show();
                $("#Kgrid").show();
                $(".ActualizarResultados").css("display", "inline-block");

                if ($('#ModalSearch2').hasClass('in')) {
                    $("#ModalSearch2").modal('hide');
                }

                TaskLoaded = true;

                break;
            case "all":
            case "search":
                $scope.currentModeSearch = 'search';
                var localSearchType;
                if (window.localStorage) {
                    localSearchType = window.localStorage.getItem("tipoBusqueda");
                }


                var LoadedFromLocal = $scope.loadSearchFromLocal();

                $scope.CleanAllInputs();;
                $scope.GoBackToSearch();

                $scope.WFSideBarIsOpen = false;
                break;
            case "global":

                $("#showatributtes").empty();
                var localSearchType;
                if (window.localStorage) {
                    localSearchType = window.localStorage.getItem("tipoBusqueda");
                }
                if ($('#Kgrid').children().length > 0 && localSearchType === "Palabras") {
                    $("#SearchControls").show();
                    $("#tabresults").show();
                    $scope.visualizerModeGSFn(_this, "grid")
                }
                else {
                    $("#SearchControls").show();
                }
                break;

            case "Home":
                break;
            case "News":
                break;
            case "insert":

                setInsertIframeUrl();
                $("#tabInsert").show();

                break;


        }

        ResizeMDDatePickers();
        try {
            if (mode != 'loading') {
                $scope.$applyAsync();
                sessionStorage.setItem('lastmainmenuitem|' + GetUID(), mode);
            }
        } catch (e) {
        }

    }

    $timeout($scope.searchModeGSFn(null, 'loading'), 0);

    //#region Search Page Entities

    $scope.setSearchEntites = function (DoctypesIds) {
        $scope.Search.DoctypesIds = DoctypesIds;
        var userId = parseInt(GetUID());

        if (DoctypesIds.length > 0) {

            if (window.localStorage) {
                var localIndexsByDTIds = window.localStorage.getItem('localIndexsByDTIds' + Array.prototype.join.call(DoctypesIds, '-') /*DoctypesIds.join("-")*/ + "|" + userId);
                if (localIndexsByDTIds != undefined && localIndexsByDTIds != null && localIndexsByDTIds.length > 0) {
                    try {
                        var data = JSON.parse(localIndexsByDTIds);
                        $scope.Search.Indexs = data; // Success

                        $scope.Search.Filters = [];
                        $scope.Search.usedFilters = [];
                        $scope.Search.OrderBy = '';
                        $scope.Search.LastPage = 0;
                    } catch (e) {
                        console.error(e);
                        $scope.LoadIndexsByDTIdsFromDB();
                    }
                }
                else {
                    $scope.LoadIndexsByDTIdsFromDB();
                }
            }
            else {
                $scope.LoadIndexsByDTIdsFromDB();
            }
        }
        else {
            $scope.Search.Indexs = [];
            $scope.Search.Filters = [];
            $scope.Search.usedFilters = [];
            $scope.Search.OrderBy = '';
            $scope.Search.LastPage = 0;

        }
    };

    $scope.updateSelectedEntities = function (DoctypesIds, lastnodes) {

        $scope.Search.DoctypesIds = DoctypesIds;
        var userId = parseInt(GetUID());
        try {


            // refresca la memoria
            for (i = 0; i < $scope.Search.Indexs.length; i++) {
                var es_nuevo = true;
                for (j = 0; j < ZambaIndexsValuesMemory.length; j++) {
                    var parte_indice = ZambaIndexsValuesMemory[j].split(';')[0];
                    if (parte_indice == $scope.Search.Indexs[i].Name) {
                        es_nuevo = false;
                        if ($scope.Search.Indexs[i].Data == null) {
                            ZambaIndexsValuesMemory[j] = parte_indice + ';'
                        }
                        else {
                            if (ZambaIndexsValuesMemory[j] != "") {

                                if ($scope.Search.Indexs[i].dataDescription == "" && isNaN($scope.Search.Indexs[i].Data) != true) {
                                    ZambaIndexsValuesMemory[j] = parte_indice + ';' + $scope.Search.Indexs[i].Data;
                                } else if ($scope.Search.Indexs[i].dataDescription != "") {
                                    ZambaIndexsValuesMemory[j] = parte_indice + ';' + $scope.Search.Indexs[i].dataDescription;
                                }
                            }

                        }

                    }
                }
                if (es_nuevo && $scope.Search.Indexs[i].Data != "" && $scope.Search.Indexs[i].Data != undefined) {
                    if ($scope.Search.Indexs[i].Data == null) {
                        ZambaIndexsValuesMemory.push($scope.Search.Indexs[i].Name + ';')
                    } else {
                        if ($scope.Search.Indexs[i].dataDescription == "") {
                            ZambaIndexsValuesMemory.push($scope.Search.Indexs[i].Name + ';' + $scope.Search.Indexs[i].Data)
                        } else {
                            ZambaIndexsValuesMemory.push($scope.Search.Indexs[i].Name + ';' + $scope.Search.Indexs[i].dataDescription)
                        }

                    }

                }
            }
        }
        catch (e) {
            console.error(e);
        }

        if (DoctypesIds.length > 0) {

            if (window.localStorage) {
                var localIndexsByDTIds = window.localStorage.getItem('localIndexsByDTIds' + Array.prototype.join.call(DoctypesIds, '-')/*DoctypesIds.join("-")*/ + "|" + userId);
                if (localIndexsByDTIds != undefined && localIndexsByDTIds != null) {
                    try {
                        var data = JSON.parse(localIndexsByDTIds);
                        // coloca los datos recordados de la busqueda
                        if (data != undefined) {
                            for (i = 0; i < data.length; i++) {
                                for (j = 0; j < ZambaIndexsValuesMemory.length; j++) {
                                    var separar_item = ZambaIndexsValuesMemory[j].split(';');
                                    var parte_indice = separar_item[0];
                                    if (separar_item.length > 1)
                                        separar_item.shift();
                                    var parte_valor = separar_item.join(';');
                                    if (data[i].Name == parte_indice) {
                                        if (!(parte_valor == null || parte_valor == undefined || parte_valor == '')) {
                                            if (data[i].Type == 0 || data[i].Type == 1 || data[i].Type == 2 || data[i].Type == 3 || data[i].Type == 5) {
                                                data[i].Data = parseFloat(parte_valor);
                                            }
                                            else
                                                data[i].Data = parte_valor;
                                        }
                                    }
                                }
                            }
                        }
                        if (data.length > 0) {
                            $scope.Search.Indexs = data; // Success
                            if ($scope.Search.Indexs != null) {
                                if ($scope.Search.Indexs[0] != null) {
                                    $('#barratop').css('display', 'block');
                                } else {
                                    $('#barratop').css('display', 'none');
                                }
                            }
                            $scope.Search.Filters = [];
                            $scope.Search.usedFilters = [];
                            $scope.Search.OrderBy = '';
                            $scope.Search.LastPage = 0;

                        }
                        else {
                            $scope.LoadIndexsByDTIdsFromDB();

                        }
                    } catch (e) {
                        console.error(e);
                        $scope.LoadIndexsByDTIdsFromDB();
                    }
                }
                else {
                    $scope.LoadIndexsByDTIdsFromDB();
                }
            }
            else {
                $scope.LoadIndexsByDTIdsFromDB();
            }
        }
        else {
            $scope.Search.Indexs = [];
            $scope.Search.Filters = [];
            $scope.Search.usedFilters = [];
            $scope.Search.OrderBy = '';
            $scope.Search.LastPage = 0;
        }

        $scope.lastNodeObj.LastNodes = lastnodes;
        $scope.lastNodeObj.UserId = GetUID();

        try {
            var midata = $scope.Search.Indexs;
            if (midata != undefined) {
                for (j = 0; j < ZambaIndexsValuesMemory.length; j++) {
                    for (i = 0; i < midata.length; i++) {
                        var separar_item = ZambaIndexsValuesMemory[j].split(';');
                        var parte_indice = separar_item[0];
                        separar_item.shift();
                        var parte_valor = separar_item.join(';');
                        if (midata[i].Name == parte_indice) {
                            $($("input[indexname='" + midata[i].Name + "']")).val(parte_valor);
                        }
                    }
                }
            }
        }
        catch (e) {
            console.error(e);
        }

        if (window.localStorage) {
            var treeView = $("#treeview").data("kendoTreeView"),
                nodeToLocalStorage = treeView.dataSource.options.data;
            var localTreeData = JSON.stringify(nodeToLocalStorage);
            try {
                if (localTreeData != undefined && localTreeData != null && localTreeData != '') {
                    //window.localStorage.setItem('localTreeData|' + GetUID(), JSON.stringify(nodeToLocalStorage));
                    $scope.$broadcast('localTreeDataLoaded', nodeToLocalStorage);
                }
            }
            catch (e) {
                console.error(e);
                if (e.message.indexOf('exceeded the quota') != -1) {
                    window.localStorage.clear();
                }

            }
        }
        setTimeout(function () {
            $scope.$applyAsync();
        }, 100);

    };

    function setNodesOnDB() {
        var data = $scope.lastNodeObj;
        $.ajax({
            type: 'POST',
            // dataType: 'json',
            url: ZambaWebRestApiURL + '/search/SetLastNodes',
            async: true,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8"
        });
    }


    function checkedNodeIdsfalse(nodes, checkedNodesfalse, DoctypesIdsfalse, DoctypesIds) {
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].checked == 'true' || nodes[i].checked == true) {
                checkedNodesfalse.push(nodes[i].NodeType + "-" + nodes[i].id);
                if (nodes[i].NodeType == "Entity") {
                    if (DoctypesIds.indexOf(nodes[i].id) == -1) {
                        DoctypesIds.push(nodes[i].id);
                    }
                }
            }
            if (nodes[i].hasChildren) {
                checkedNodeIdsfalse(nodes[i].children.view(), checkedNodesfalse, DoctypesIdsfalse, DoctypesIds);
            }
        }
    }



    $scope.LoadIndexsByDTIdsFromDB = function () {
        var userId = parseInt(GetUID());
        var result = EntityFieldsService.GetAll($scope.Search.DoctypesIds);

        //EntityFieldsService.GetAll($scope.Search.DoctypesIds).then(function (d) {
        try {
            var results = JSON.parse(result);

            var dtids = '';
            if ($scope.Search.DoctypesIds.join == undefined) {
                dtids = $scope.Search.DoctypesIds
            }
            else {
                dtids = $scope.Search.DoctypesIds.join("-");
            }
            try {
                if (window.localStorage) {
                    window.localStorage.setItem('localIndexsByDTIds' + dtids + "|" + userId, result);
                }
            }
            catch (e) {
                console.log(e);
                if (e.message.indexOf('exceeded the quota') != -1) {
                    window.localStorage.clear();
                }
            }

            $scope.Search.Indexs = results; // Success
            $scope.Search.Filters = [];
            $scope.Search.usedFilters = [];
            $scope.Search.OrderBy = '';
            $scope.Search.LastPage = 0;

        } catch (e) {
            console.log(e);
            $scope.Search.Indexs = [];
            $scope.Search.Filters = [];
            $scope.Search.OrderBy = '';
            $scope.Search.LastPage = 0;
            // alert('Error Occured !!!'); // Failed
        }

        //}, function (response) {
        //    console.log(response);
        //    $scope.Search.Indexs = [];
        //    $scope.Search.Filters = [];
        //    $scope.Search.OrderBy = '';
        //    $scope.Search.LastPage = 0;
        //    // alert('Error Occured !!!'); // Failed
        //});
    }



    //#endregion

    //#region Search Page Attributes Search


    //#region DatePicker



    $scope.subscribeDatepicker = function (id) {
        if (!($("#" + id).hasClass("TengoCalendar"))) {
            setTimeout(function () {
                $(".datepicker").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "mm-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    yearRange: "-150:+100",
                });
                $(".datepicker").datepicker().mask("99/99/9999");
                $("#" + id).focus();
                $("#" + id).addClass("TengoCalendar");
            }, 0);
        }
    };

    $scope.subscribeDatepickerBetween = function (id) {
        var elementId = '#' + id;
        $(elementId).datepicker().mask("99/99/9999");
    };

    //#endregion DatePicker

    //#region STEPS
    $scope.selectedStepStateName = '';

    $scope.StepSelected = function (item, model) {
        if ($scope.Search.StepId == null && $scope.Search.StepId == undefined) {
            $scope.Search.StepId = 0;
        }
        $scope.Search.LastPage = 0;

        $scope.DoSearch();
    }

    $scope.FillIndexFilters = function (filtersFromDB) {
        $scope.setUsedFiltersFromDB(filtersFromDB);
    }

    $scope.setUsedFiltersFromDB = function (filtersFromDB) {
        $scope.Search.usedFilters = [];
        filtersFromDB.forEach(function (filter) {

            //si es indice/si no es columna fija zamba
            if (filter.Filter.toLowerCase().startsWith('i') &&
                filter.Description != "Fecha Creacion" &&
                filter.Description != "Modificación" &&
                filter.Description != "Nombre Original" &&
                filter.Description != "Tarea" &&
                filter.Description.toLowerCase() != "estado tarea") {
                try {
                    var indexId = parseInt(filter.Filter.substring(1));
                    //setea los valores en Search.usedFilters
                    var newFilter = { ID: indexId, Name: filter.Description, IsChecked: filter.Enabled, Data: filter.Value.slice(1, -1), dataDescription: filter.DataDescription, CompareOperator: filter.Comparator, CurrentUserId: filter.UserId, StepId: 0, EntitiesIds: 0, zFilterWebID: filter.Id };

                    if ($scope.Search.usedFilters == undefined)
                        $scope.Search.usedFilters = [];

                    $scope.Search.usedFilters.push(newFilter);

                } catch (e) {
                    console.error(e);
                }
            }
        });
    }
    $scope.setUserAssignedFilter = function (filtersFromDB) {

        try {
            var filterUserAssigned = filtersFromDB.filter(f => f.Description == "Asignado");
            if (filterUserAssigned.length > 0) {
                let lastUserAssignedIndex = filterUserAssigned.length - 1;
                let userNameAssigned = filterUserAssigned[lastUserAssignedIndex].Value.slice(1, -1);

                let userIdAssigned = $scope.Search.SearchResultsObject.UsersAsigned.filter(u => u.Name.toLowerCase() == userNameAssigned.toLowerCase());

                if (userNameAssigned == "" && filterUserAssigned[lastUserAssignedIndex].Comparator.toLocaleLowerCase() == 'es nulo') {
                    userIdAssigned = $scope.Search.SearchResultsObject.UsersAsigned.filter(u => u.ID == 0);
                } else if (userNameAssigned == "" && filterUserAssigned[lastUserAssignedIndex].Comparator.toLocaleLowerCase() == 'no es nulo') {
                    userIdAssigned = $scope.Search.SearchResultsObject.UsersAsigned.filter(u => u.ID == -2);
                }


                if (userIdAssigned.length > 0) {
                    $scope.Search.UserAssignedId = userIdAssigned[0].ID;
                    if ($scope.Search.UserAssignedFilter == undefined) {
                        $scope.Search.UserAssignedFilter = { IsChecked: true, zFilterWebID: 0 };
                    }
                    $scope.Search.UserAssignedFilter.IsChecked = filterUserAssigned[lastUserAssignedIndex].Enabled;
                    $scope.Search.UserAssignedFilter.zFilterWebID = filterUserAssigned[lastUserAssignedIndex].Id;

                }

            } else {
                $scope.Search.UserAssignedFilter.IsChecked = false;
                $scope.Search.UserAssignedFilter.zFilterWebID = 0;
                $scope.Search.UserAssignedId = -1;
            }
            console.log(filtersFromDB);
        } catch (e) {
            console.error(e);
        }
    }
    $scope.setStepFilter = function (filtersFromDB) {

        try {
            var filterStep = filtersFromDB.filter(f => f.Description == "Etapa");
            if (filterStep.length > 0) {
                $scope.Search.StepId = parseInt(filterStep[0].Value.slice(1, -1));
                if ($scope.Search.StepFilter == undefined) {
                    $scope.Search.StepFilter = { IsChecked: true, zFilterWebID: 0 };
                }
                $scope.Search.StepFilter.IsChecked = filterStep[0].Enabled;
                $scope.Search.StepFilter.zFilterWebID = filterStep[0].Id;
            } else {
                $scope.ClearStepFilterSelected(false);
            }
        } catch (e) {
            console.error(e);
        }
    }
    $scope.callDoSearchFromTreeviewSelectedNode = function (stepID, stateID) {
        if ($scope.Search.StepId == null && $scope.Search.StepId == undefined) {
            $scope.Search.StepId = 0;
        }
        if ($scope.Search.StepStateId == null && $scope.Search.StepStateId == undefined) {
            $scope.Search.StepStateId = 0;
        }

        var entitieIds = SearchObjectService.GetStepEntities(stepID);

        //if (JSON.stringify(entitieIds) != JSON.stringify($scope.Search.DoctypesIds)) {
        $scope.Search.UserAssignedId = -1;
        $scope.Search.UserAssignedFilter = { IsChecked: false, zFilterWebID: 0 };
        $scope.Search.StepFilter = { IsChecked: false, zFilterWebID: 0 };
        $rootScope.$broadcast('resetFiltersDefaultZambaColumnFilters');
        $scope.Search.DoctypesIds = entitieIds;
        $scope.Search.Indexs = JSON.parse(EntityFieldsService.GetAllSync($scope.Search.DoctypesIds));
        $scope.Search.usedFilters = [];
        if (entitieIds.length == 1) {
            let docTypeIdFilter = $scope.Search.DoctypesIds[0];
            //obtengo los filtros para 1 entidad
            var filtersFromDB = SearchFilterService.GetFiltersByView(docTypeIdFilter, 'MyProcess');

            if (!($scope.Search.lastFiltersByView instanceof Map)) {
                $scope.Search.lastFiltersByView = new Map();
            }
            $scope.Search.lastFiltersByView.set("MyProcess", JSON.stringify(filtersFromDB));

            if (filtersFromDB.length > 0) {
                $scope.setUserAssignedFilter(filtersFromDB);
                $scope.setStepFilter(filtersFromDB);
                $scope.FillIndexFilters(filtersFromDB);
                $scope.$broadcast('hasFiltersFromDBEvent', filtersFromDB);
            }
        }
        // }
        //$scope.Search.UsedZambafilters = $scope.sumNonIndexedFilters();
        $scope.Search.StepId = stepID;
        $scope.Search.StepStateId = stateID
        $scope.Search.LastPage = 0;

        $scope.DoSearch();

    }



    $scope.hideTreeviewFilterButton = function () {
        var scope_TreeViewController = angular.element($("#SidebarTree")).scope();
        if (scope_TreeViewController != undefined)
            scope_TreeViewController.clearSelectedNode();
    };

    $scope.ClearStepSelected = function () {

        if (angular.element($("#taskController")).scope() != undefined) {
            angular.element($("#taskController")).scope().actionRules = null;
        }

        $scope.Search.StepId = 0;
        $scope.Search.stateID = 0;
        $scope.lastSelectedNode = 0;
        $scope.Search.LastPage = 0;

        $scope.DoSearch();
    }
    //#endregion STEPS


    //#region Preview Document

    $scope.SetPreviewDocumentBlank = function () {
        try {
            if ($("#previewDocSearch")[0] != undefined) {
                if ($("#previewDocSearch")[0].contentWindow.OpenUrl != undefined) {
                    $("#previewDocSearch")[0].contentWindow.OpenUrl("_AboutBlank", 0)
                }
                else {
                    if ($("#previewDocSearch")[0] != undefined) {
                        $($("#previewDocSearch")[0]).attr("src", "");
                    }
                }
            }
        }
        catch (e) {
            console.error(e);
        }
    }

    $scope.scrollHdlr = function ($event, $direct) {
        $direct.preventDefault();
        $direct.stopImmediatePropagation();
        var c = $direct.currentTarget;
        var scroll = c.scrollHeight - c.clientHeight - c.scrollTop;
        if (scroll === 0) {
            if ($scope.MustDoSearch()) {
                if (window.localStorage && window.localStorage.getItem("tipoBusqueda").length && window.localStorage.getItem("tipoBusqueda") === "Atributos") {
                    if ($scope.Search.SearchResultsObject.total > $scope.PageSize) {
                        setPageNumber($scope.Search.LastPage);
                        $scope.DoSearch();
                    }
                }
                else {
                    if ($scope.Search.SearchResultsObject.total > $scope.PageSize) {
                        this.isPagging = true;
                        this.page++;
                        $scope.doSearchGS();
                    }
                }
            }
        }
        thumbButtonDisplay();
        thumbPreviewButtonDisplay();
    };

    $scope.DoEndlessScroll = function (arg) {
        $scope.Search.LastPage++;
        $scope.DoSearch();
    }

    //#endregion
    $scope.saveData = function (Index, Value, Label) {
        if (Value != undefined) //Valor por seleccion de typeahead o modal
        {
            if (Index.DropDown == 1) {
                Index.dataDescription = Label;
                Index.Data = Label;
            }
            else {
                Index.dataDescription = Label;
                Index.Data = Value;
            }
            if ($('#ModalSearch').hasClass('in')) {
                $("#ModalSearch").modal("hide");
                setTimeout(function () { $('#searchWrapper').focus(); }, 600);
            }

            if ($('#ModalSearch2').hasClass('in')) {
                $("#ModalSearch2").modal("hide");
                setTimeout(function () { $('#searchWrapper').focus(); }, 600);
            }
        }
    };


    $scope.val_formatNumber = function (e) {
        var input = e.target;

        if (input.getAttributeNames().indexOf("format-number") != -1) {
            if (!isNaN(parseInt(event.key))) {
                return true;
            } else {
                event.preventDefault();
                return false;
            }
        }
    };

    $scope.CleanData = function (Index) {
        if (Index !== undefined) {
            Index.Data = "";
            Index.dataDescription = "";
        }
    }

    $scope.CleanData2 = function (Index) {
        if (Index !== undefined) {
            Index.Data2 = "";
            Index.dataDescription2 = "";
        }
    }

    $scope.saveDataBetween = function (Index, Value, Label) {
        if (Value != undefined) //Valor por seleccion de typeahead o modal
        {
            Index.dataDescription2 = Label;
            Index.Data2 = Value;
            if ($('#ModalSearch').hasClass('in'))
                $("#ModalSearch").modal("hide");
            if ($('#ModalSearch2').hasClass('in'))
                $("#ModalSearch2").modal("hide");

        }
    };

    $scope.saveOperator = function (Index, Value) {
        if (Value != undefined) {
            if (Value != "Entre") {
                Index.Data2 = "";
                Index.dataDescription2 = "";
            }
            Index.Operator = Value;
        }
    };

    $scope.loadOperator = function (Index) {
        var opTxt = [{
            Desc: 'Igual',
            Oper: '='
        }, {
            Desc: 'Nulo',
            Oper: 'Es nulo'
        }, {
            Desc: 'Alguno',
            Oper: 'Alguno'
        }, {
            Desc: 'Contiene',
            Oper: 'Contiene'
        }, {
            Desc: 'Termina',
            Oper: 'Termina'
        }, {
            Desc: 'Empieza',
            Oper: 'Empieza'
        }];
        var opNum = [{
            Desc: 'Igual',
            Oper: '='
        }, {
            Desc: 'Entre',
            Oper: 'Entre'
        }, {
            Desc: 'Distinto',
            Oper: 'Distinto'
        }, {
            Desc: 'Menor',
            Oper: '<'
        }, {
            Desc: 'Menor igual',
            Oper: '<='
        }, {
            Desc: 'Mayor',
            Oper: '>'
        }, {
            Desc: 'Mayor igual',
            Oper: '>='
        }];
        switch (Index.Type) {
            case 1: case 2: case 3: case 4: case 5: case 6:
                if (Index.DropDown == 2 || Index.DropDown == 4)
                    $scope.Operators = opTxt;
                else
                    $scope.Operators = opNum;
                break;
            case 7: case 8:
                $scope.Operators = opTxt;
                break;
        }
        ResizeMDDatePickers();
    };

    $scope.showlistFirstTime = function (index, id) {
        $($("input[id='Search.selectedIndex.ID']")[0]).val("");
        $scope.showlist(index, id, false, true);
    }

    $scope.showlist = function (index, id, moreResults, firstTime) {
        if (moreResults)
            LimitToSlst += 20;
        else
            LimitToSlst = 20;



        /*var SIndex = id != undefined ? index : Search.Indexs[id];*/

        var SIndex = index;

        $scope.selectedIndex = SIndex;
        Search.selectedIndex = SIndex;

        var valueSearch = "";
        if (!firstTime)
            valueSearch = $scope.selectedIndex.dataDescription;

        var indexData = $http.post(ZambaWebRestApiURL + '/search/ListOptions', JSON.stringify({
            IndexId: $scope.selectedIndex.ID,
            Value: valueSearch,
            LimitTo: LimitToSlst

            // Trae como tope (LimitToSlst + 1) filas, en caso que haya LimitToSlst + 1 elimina el ultimo y muestra un mensaje que hubo mas resultados

        })).then(function (response) {
            var results = JSON.parse(response.data);
            if (results.length > LimitToSlst) {
                {
                    $("#searchMoreResults").show();
                    results.pop();
                }
            }
            else {
                $("#searchMoreResults").hide();
            }

            $scope.selectedIndex.DropDownList = results;
            BtnTrashHidden();
            if (!$('#ModalSearch').hasClass('in')) {
                $("#ModalSearch").modal();
                $("#ModalSearch").draggable();
                $("#modalFormHome > div")[0].childNodes[1].value = "";
            }
            if (!$('#ModalSearch2').hasClass('in')) {
                $("#ModalSearch2").modal();
                $("#ModalSearch2").draggable();
                $("#modalFormHome > div")[0].childNodes[1].value = "";
            }

            var modal = $(".modal-backdrop")[0];

            // esto es porque los estilos del modal hacen que se rompa la vista
            $(modal).css('opacity', 0);
            $(modal).css('z-index', 99999);
            $(modal).css('display', 'contents');

        });
    };

    //Muestra la lista desplegable otra vez al borrar los Datos del input
    $scope.ShowListAfter = function () {

        var indexData = $http.post(ZambaWebRestApiURL + '/search/ListOptions', JSON.stringify({
            IndexId: $scope.selectedIndex.ID,
            Value: Search.selectedIndex.dataDescription,//.DataTemp,
            LimitTo: 10
        })).then(function (response) {
            var results = JSON.parse(response.data);

            Search.selectedIndex.DropDownList = results;
            if (!$('#ModalSearch').hasClass('in'))
                $("#ModalSearch").modal();
            if (!$('#ModalSearch2').hasClass('in'))
                $("#ModalSearch2").modal();


        });

    }

    $scope.ResetValuesAndOperatorByDafault = function () {
        onCheck();
    }
    $scope.CleanData = function (Index) {
        if (Index !== undefined) {
            Index.Data = "";
            Index.dataDescription = "";
        }
    }

    $scope.CleanData2 = function (Index) {
        if (Index !== undefined) {
            Index.Data2 = "";
            Index.dataDescription2 = "";
        }
    }

    //#endregion Search Page Attributes Search

    //#region Rules Execution
    $scope.executeCurrentRule = function (ruleName) {
        if ($scope.checkedIds != null && $scope.checkedIds.length > 0) {
            var ruleIds = getRuleIdFromdictionaryByName(ruleName);
            var resultIds = getSelectedDocids().toString();

            var scope = angular.element($("panel_ruleActions")).scope();
            scope.Execute_ZambaRule(ruleIds, resultIds);
        } else {
            swal("No se a podido ejecutar la regla", "Seleccione al menos una tarea.", "warning");
        }
    }

    $scope.getRuleName = function () {
        var names = [];
        var d = ruleExecutionService.getRuleNames($scope.ruleIds)
        var results = JSON.parse(d);
        $scope.ruleDictionary = results;
        for (var result in results) {
            if (result.indexOf("id") == -1) {
                names.push(results[result])
            }
        }
        return names;
    }

    $scope.getRules = function () {
        alert("ALGO de GETRULES()");
        return "algo";
    };

    function getRuleIdFromdictionaryByName(ruleName) {
        var ruleDictionary = $scope.ruleDictionary;
        var ruleId = null;
        for (var rule in ruleDictionary) {
            if (ruleDictionary[rule] == ruleName) {
                ruleId = rule;
            }
        }
        return ruleId;
    }
    //#endregion Rules Execution

    //#region GridSelection

    function getSelectedDocids() {
        var docIds = [];
        for (i = 0; i < attachsIds.length; i++) {
            docIds.push(attachsIds[i].Docid);
        }
        return docIds;
    }

    //#endregion GridSelection


    //#region userPersistance
    $scope.saveSearchByView = function (objSearch) {
        try {
            objSearch.currentMode = objSearch.View;
            if (objSearch.lastFiltersByView instanceof Map) {
                objSearch.lastFiltersByView = Object.fromEntries(objSearch.lastFiltersByView.entries());
            }
            var rdoSave = SearchObjectService.SaveSearch(objSearch, $scope);
            if (localStorage) {
                localStorage.setItem('localSearch-' + objSearch.currentMode + '-' + GetUID(), JSON.stringify(objSearch));
            }
        } catch (e) {
            console.error(e);
        }
    }

    $scope.loadSearchFromLocal = function () {
        var result = false;

        try {
            if (localStorage) {
                var receivedSearch = false;
                let localSearch = null;

                localSearch = localStorage.getItem('localSearch-' + $scope.currentMode + '-' + GetUID());

                if (localSearch != undefined && localSearch != null) {
                    $scope.Search = JSON.parse(localSearch);
                    try {
                        $scope.Search.lastFiltersByView = new Map(Object.entries($scope.Search.lastFiltersByView));
                    } catch (e) {
                        $scope.Search.lastFiltersByView = new Map();
                    }
                    //compara con filtros de la base por si alguien los modifico desde Desktop
                    if ($scope.Search.DoctypesIds.length == 1) {
                        if ($scope.Search.currentMode.toLowerCase() == "search")
                            SearchFilterService.SetDisabledAllFiltersByUser('Search');

                        var filtersFromDB = SearchFilterService.GetFiltersByView($scope.Search.DoctypesIds[0], $scope.Search.currentMode);

                        if (JSON.stringify(filtersFromDB) != $scope.Search.lastFiltersByView.get($scope.Search.currentMode)) {
                            if ($scope.Search.currentMode.toLowerCase() == "myprocess") {
                                $scope.callDoSearchFromTreeviewSelectedNode($scope.Search.StepId, $scope.Search.StepStateId);
                                return true;
                            } else if ($scope.Search.currentMode.toLowerCase() == "search") {
                                $scope.Search.lastFiltersByView.set($scope.Search.currentMode, JSON.stringify(filtersFromDB));
                                $scope.setUserAssignedFilter(filtersFromDB);
                                $scope.setStepFilter(filtersFromDB);
                                $scope.FillIndexFilters(filtersFromDB);
                                $scope.$broadcast('hasFiltersFromDBEvent', filtersFromDB);
                            }
                        }
                    }

                    if ($scope.Search.UserAssignedFilter == undefined) {
                        let UserAssignedFilter = { IsChecked: false, zFilterWebID: 0 };
                        $scope.Search.UserAssignedFilter = UserAssignedFilter;
                    }
                    if ($scope.Search.StepFilter == undefined) {
                        let StepFilter = { IsChecked: false, zFilterWebID: 0 };
                        $scope.Search.StepFilter = StepFilter;
                    }

                    $scope.Search.ExpirationDate = new Date($scope.Search.ExpirationDate);
                    $scope.ShowExpirationDate = $scope.GetRowFetchedDate();

                    $scope.ExecuteDoSearchFromLocal = $scope.Search.HasResults;
                    $scope.$broadcast('loadSearchFromLocalEvent', $scope.Search);
                    result = $scope.LoadLastSearchState($scope.Search);


                    return result;

                }
                else {
                    receivedSearch = SearchObjectService.GetSearch($scope.currentMode);

                    if (receivedSearch) {



                        $scope.Search.ExpirationDate = new Date(receivedSearch.ExpirationDate);
                        $scope.ShowExpirationDate = $scope.GetRowFetchedDate();

                        $scope.ExecuteDoSearchFromLocal = $scope.Search.HasResults;

                        $scope.Search = JSON.parse(receivedSearch.ObjectSearch);

                        try {
                            $scope.Search.lastFiltersByView = new Map(Object.entries($scope.Search.lastFiltersByView));
                        } catch (e) {
                            $scope.Search.lastFiltersByView = new Map();
                        }
                        if ($scope.Search.DoctypesIds.length == 1) {
                            if ($scope.Search.currentMode.toLowerCase() == "search")
                                SearchFilterService.SetDisabledAllFiltersByUser('Search');

                            var filtersFromDB = SearchFilterService.GetFiltersByView($scope.Search.DoctypesIds[0], $scope.Search.currentMode);

                            if (JSON.stringify(filtersFromDB) != $scope.Search.lastFiltersByView.get($scope.Search.currentMode)) {
                                if ($scope.Search.currentMode.toLowerCase() == "myprocess") {
                                    $scope.callDoSearchFromTreeviewSelectedNode($scope.Search.StepId, $scope.Search.StepStateId);
                                    return true;
                                } else if ($scope.Search.currentMode.toLowerCase() == "search") {
                                    $scope.Search.lastFiltersByView.set($scope.Search.currentMode, JSON.stringify(filtersFromDB));
                                    $scope.setUserAssignedFilter(filtersFromDB);
                                    $scope.setStepFilter(filtersFromDB);
                                    $scope.FillIndexFilters(filtersFromDB);
                                    $scope.$broadcast('hasFiltersFromDBEvent', filtersFromDB);
                                }
                            }
                        }

                        if ($scope.Search.UserAssignedFilter == undefined) {
                            let UserAssignedFilter = { IsChecked: false, zFilterWebID: 0 };
                            $scope.Search.UserAssignedFilter = UserAssignedFilter;
                        }
                        if ($scope.Search.StepFilter == undefined) {
                            let StepFilter = { IsChecked: false, zFilterWebID: 0 };
                            $scope.Search.StepFilter = StepFilter;
                        }
                        $scope.$broadcast('loadSearchFromLocalEvent', $scope.Search);

                        result = $scope.LoadLastSearchState($scope.Search);

                        onCheck();
                        return result;
                    }

                }

            } else {
                receivedSearch = SearchObjectService.GetSearch($scope.currentMode);

                if (receivedSearch) {

                    $scope.Search.ExpirationDate = new Date(receivedSearch.ExpirationDate);
                    $scope.ShowExpirationDate = $scope.GetRowFetchedDate();

                    $scope.ExecuteDoSearchFromLocal = $scope.Search.HasResults;

                    console.log(receivedSearch.ObjectSearch);
                    $scope.Search = JSON.parse(receivedSearch.ObjectSearch);

                    try {
                        $scope.Search.lastFiltersByView = new Map(Object.entries($scope.Search.lastFiltersByView));
                    } catch (e) {
                        $scope.Search.lastFiltersByView = new Map();
                    }

                    if ($scope.Search.DoctypesIds.length == 1) {
                        if ($scope.Search.currentMode.toLowerCase() == "search")
                            SearchFilterService.SetDisabledAllFiltersByUser('Search');

                        var filtersFromDB = SearchFilterService.GetFiltersByView($scope.Search.DoctypesIds[0], $scope.Search.currentMode);

                        if (JSON.stringify(filtersFromDB) != $scope.Search.lastFiltersByView.get($scope.Search.currentMode)) {
                            if ($scope.Search.currentMode.toLowerCase() == "myprocess") {
                                $scope.callDoSearchFromTreeviewSelectedNode($scope.Search.StepId, $scope.Search.StepStateId);
                                return true;
                            } else if ($scope.Search.currentMode.toLowerCase() == "search") {
                                $scope.Search.lastFiltersByView.set($scope.Search.currentMode, JSON.stringify(filtersFromDB));
                                $scope.setUserAssignedFilter(filtersFromDB);
                                $scope.setStepFilter(filtersFromDB);
                                $scope.FillIndexFilters(filtersFromDB);
                                $scope.$broadcast('hasFiltersFromDBEvent', filtersFromDB);
                            }
                        }
                    }

                    if ($scope.Search.UserAssignedFilter == undefined) {
                        let UserAssignedFilter = { IsChecked: false, zFilterWebID: 0 };
                        $scope.Search.UserAssignedFilter = UserAssignedFilter;
                    }
                    if ($scope.Search.StepFilter == undefined) {
                        let StepFilter = { IsChecked: false, zFilterWebID: 0 };
                        $scope.Search.StepFilter = StepFilter;
                    }
                    $scope.$broadcast('loadSearchFromLocalEvent', $scope.Search);
                    result = $scope.LoadLastSearchState($scope.Search);


                    return result;
                }

                $scope.ExecuteDoSearchFromLocal = true;
            }

            return result;
        } catch (e) {
            console.error(e);
            $rootScope.$emit('hideLoading');
            return result;
        }
        finally {
            $rootScope.$emit('hideLoading');
        }
    }

    $scope.GetRowFetchedDate = function () {
        try {


            if ($scope.Search.ExpirationDate != null) {
                var day = $scope.Search.ExpirationDate.toLocaleDateString().split('/')[0];
                var month = $scope.Search.ExpirationDate.toLocaleDateString().split('/')[1];
                var year = $scope.Search.ExpirationDate.toLocaleDateString().split('/')[2].substring(2, 4);;
                var hours = $scope.Search.ExpirationDate.toLocaleTimeString().split(':')[0];
                var minutes = $scope.Search.ExpirationDate.toLocaleTimeString().split(':')[1];

                return day + "/" + month + "/" + year + " " + hours + ":" + minutes;
            } else {
                console.error("No existe fecha en la que se obtuvieron los datos");
                return new Date().toLocaleDateString().split('/')[0] + "/" +
                    new Date().toLocaleDateString().split('/')[1] + "/" +
                    new Date().toLocaleDateString().split('/')[2].substring(2, 4) + "/" +
                    new Date().toLocaleDateString().split(':')[0] + " " +
                    new Date().toLocaleDateString().split(':')[1] + ":" + minutes;
            }
        } catch (e) {
            console.error("No existe fecha en la que se obtuvieron los datos");
            return new Date().toLocaleDateString().split('/')[0] + "/" +
                new Date().toLocaleDateString().split('/')[1] + "/" +
                new Date().toLocaleDateString().split('/')[2].substring(2, 4) + "/" +
                new Date().toLocaleDateString().split(':')[0] + " " +
                new Date().toLocaleDateString().split(':')[1] + ":" + minutes;
        }
    }

    $scope.removeSearchFromLocal = function () {
        try {
            if (localStorage) {
                let localSearch = null;
                localSearch = localStorage.getItem('localSearch-' + $scope.currentMode + '-' + GetUID());
                if (localSearch != undefined && localSearch != null)
                    localStorage.removeItem('localSearch-' + GetUID());
            }

            SearchObjectService.removeSearch(GetUID(), $scope.currentMode);

        } catch (e) {
            console.error(e);
        }
    }

    $scope.placeholder = $attrs.placeholder || 'Buscar ...';
    $scope.message = '';
    $scope.searchQuery = {};
    $scope.Result = null;
    $scope.setSearchFocus = false;
    $scope.page = 0;
    $scope.pageSize = 100;
    $scope.isPagging = false;
    $scope.isLastPage = false;


    $scope.lastNodeObj = lastNodeObj;

    $scope.Search.LastPage = 0;
    $scope.PageSize = 100;
    $scope.SearchResultsObject = null;
    $scope.Refreshing = false;


    //#endregion Properties



    //Codifo para que funcione filtros de tereas
    if (typeof (Sys) !== 'undefined') {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var elem = angular.element(document.getElementById("toolbarTabTasksList"));

            elem.replaceWith($compile(elem)($scope));
            $scope.$applyAsync();

        });
    }


    //#region Load More Results and Paging

    $scope.DoPaging = function (page) {
        $scope.Search.LastPage = page - 1;
        $scope.DoSearch();
    }

    $scope.Sorting = function (columm) {

        if (columm.sort.dir == undefined) {
            $scope.Search.OrderBy = "";
        }
        else {
            $scope.Search.OrderBy = columm.sort.field + " " + columm.sort.dir;
        }

        $scope.Search.LastPage = 0;

        $scope.Search.columnFiltering = true;
        $scope.DoSearch();
    }

    $scope.LoadMoreResults = function () {

        if (angular.element($("#taskController")).scope() != undefined) {
            angular.element($("#taskController")).scope().actionRules = null;

        }

        if ($scope.MustDoSearch()) {
            $scope.DoSearch();

        } else {
            toastr.options.timeOut = 1500;
            toastr.warning("No hay mas resultados para mostrar");
            //var input = document.querySelector('[name="dis"]');
            //input.setAttribute('disabled', true);
        }
    }

    $scope.MustDoSearch = function () {
        $scope.Search.LastPage = Math.floor($scope.Search.SearchResultsObject.data.length / $scope.PageSize);
        var desde = $scope.Search.SearchResultsObject.data.length + 1;
        return desde < $scope.Search.SearchResultsObject.total;
    }

    //#endregion Load More Results and Paging

    //#region Filters

    $scope.$on('filtersAdded', function (event, data) {
        let executeSearch = true;
        console.log('filtersAdded');
        $scope.Search.LastPage = 0;


        $scope.saveLastFiltersState();

        if (data != undefined)
            executeSearch = data;
        if (executeSearch) $scope.DoSearch();
    });

    $scope.$on('kendoGridReady', function (event, data) {
        hideLoading();
    });

    //#endregion Filters

    //#region Toolbar Dowload

    $scope.DownloadFile = function (obj) {
        var task = $scope.Search.SearchResults[obj];
        var docId = task.DOC_ID;
        var docTypeId = task.DOC_TYPE_ID;


        var scope = angular.element(document.getElementById("DocumentViewerFromSearch")).scope();
        scope.DownloadFile(GetUID(), docTypeId, docId);


        //  var url = "../../Services/GetDownloadFile.ashx?DocTypeId=" + docTypeId + "&DocId=" + docId + "&UserID=" + GetUID() + "&ConvertToPDf=false";

        //   window.open(url);
    }
    //Exporta Todo el contenido de la grilla de resultados en un Excel con formato .xlsx
    $scope.ExportResultsGrid_ToExcel = function (maxQuantity) {
        try {
            var busqueda = toastr.info("Realizando la exportacion de la grilla a excel.");
            toastr.options.timeOut = 50000;

            $scope.Search.UserId = GetUID();
            $scope.Search.GroupsIds = GetGroupsIdsByUserId($scope.Search.UserId);
            $scope.Search.Lista_ColumnasFiltradas = $scope.ColumnsAdvisor_ForResultsGrid();

            $scope.Search.LastPage = 0;
            $scope.Search.PageSize = maxQuantity;

            $.ajax({
                type: "POST",
                url: ZambaWebRestApiURL + '/search/ExportToExcel',
                contentType: 'application/json',
                async: false,
                data: JSON.stringify($scope.Search),
                success: function (response) {
                    if ($("#spinnerExportExcel") != null && $("#btnExportar") != null) {
                        $("#spinnerExportExcel").hide();
                        $("#btnExportar").show();
                    }
                    var FileName = 'Zamba - Grilla de Resultados - ' + moment().format('YYYYMMDD_HHmmss') + ".xlsx";
                    var dataBase64 = 'data:application/octet-stream;base64,' + response;

                    if (navigator.userAgent.indexOf('MSIE') !== -1 ||
                        navigator.appVersion.indexOf('Trident/') > 0 ||
                        navigator.userAgent.toString().indexOf('Edge/') > 0) {
                        //Sin Cabecera
                        DownloadExcel_ForIE11(response, FileName);
                    } else {
                        //Con Cabecera
                        DownloadExcel(dataBase64, FileName);
                    }
                    $("#errorMessageModalExcelExport").hide();
                    $("#myModal").modal('toggle');

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    if (XMLHttpRequest.responseJSON.ExceptionType.toLowerCase().trim() == "system.timeoutexception") {
                        toastr.error(XMLHttpRequest.responseJSON.Message);
                    }
                    else {
                        toastr.error("Ha ocurrido un error al intentar realizar la exportación a Excel.");
                    }
                    if ($("#spinnerExportExcel") != null && $("#btnExportar") != null) {
                        $("#spinnerExportExcel").hide();
                        $("#btnExportar").show();
                    }
                    $("#errorMessageModalExcelExport").hide();
                    $("#myModal").modal('toggle');
                }
            });

            $scope.Search.PageSize = 100;

        } catch (e) {
            console.error(e + " - Lanzado por: " + "[$scope.ExportResultsGrid_ToExcel]");
        }
    }

    //Descarga un excel, para el IE11, pasandole base64 del archivo y su nombre con el cual se guardara.
    function DownloadExcel_ForIE11(ObjBase64, Name) {
        var byteCharacters = atob(ObjBase64);

        var byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++)
            byteNumbers[i] = byteCharacters.charCodeAt(i);

        var byteArray = new Uint8Array(byteNumbers);
        var blob = new Blob([byteArray], { type: "" });

        window.navigator.msSaveOrOpenBlob(blob, Name);
    }

    //Descarga un excel pasandole base64 del archivo y su nombre con el cual se guardara.
    function DownloadExcel(ObjBase64, Name) {
        const elementA = window.document.createElement('a');
        elementA.href = ObjBase64;
        elementA.download = Name;
        document.body.appendChild(elementA);
        elementA.click();
        document.body.removeChild(elementA);
    }

    //#endregion Toolbar Dowload

    //$scope.DoSearchAttrClickButton = function () {
    //    if ($scope.Search.Indexs.length == 0) {
    //        toastr.error("No hay atributos seleccionados");
    //        hideLoading();
    //    }
    //    else {
    //        $scope.DoSearchAttr();
    //    }
    //}

    $scope.DoSearchAttr = function (OpenTaskOnOneResult) {
        if (angular.element($("#taskController")).scope() != undefined) {
            angular.element($("#taskController")).scope().actionRules = null;
        }
        var nodes = $("#treeview").data("kendoTreeView").dataSource.view()[0].items;
        var NotItemsChecked = true;
        nodes.forEach(function (item) {
            if (item.checked == true)
                NotItemsChecked = false;
        });
        if (NotItemsChecked) {
            toastr.error("Debe seleccionar al menos una entidad.");
            return;
        }
        //$rootScope.$broadcast('ClearFilters');
        $rootScope.$broadcast('resetFiltersDefaultZambaColumnFilters');
        $scope.Search.AsignedTasks = false;
        $scope.Search.View = "";

        if ($('#ModalSearch2').hasClass('in')) {
            $("#ModalSearch2").modal('hide');
        }

        $scope.Search.StepId = 0;
        $scope.Search.stateID = 0;
        $scope.Search.UserAssignedId = -1;
        $scope.Search.UserAssignedFilter = { IsChecked: false, zFilterWebID: 0 };
        $scope.Search.StepFilter = { IsChecked: false, zFilterWebID: 0 };
        $scope.lastSelectedNode = 0;
        $scope.Search.lupdateFilters = [];
        $scope.Search.crdateFilters = [];
        $scope.Search.nameFilters = [];
        $scope.Search.originalFilenameFilters = [];
        $scope.Search.stateFilters = [];
        if (!validateDatesByIndexType($scope.Search)) {
            return;
        }

        $scope.Search.LastPage = 0;
        $scope.Search.OpenTaskOnOneResult = OpenTaskOnOneResult == false ? OpenTaskOnOneResult : true;
        //set zfiltersweb disable
        SearchFilterService.SetDisabledAllFiltersByUser('Search');

        $scope.DoSearch();

        if ($scope.Search.DoctypesIds.length == 1) {
            let docTypeIdFilter = $scope.Search.DoctypesIds[0];
            //obtengo los filtros para 1 entidad
            var filtersFromDB = SearchFilterService.GetFiltersByView(docTypeIdFilter, 'search');
            console.log('GetFiltersByView' + $scope.Search.currentMode);
            if (!($scope.Search.lastFiltersByView instanceof Map)) {
                $scope.Search.lastFiltersByView = new Map();
            }
            $scope.Search.lastFiltersByView.set($scope.Search.currentMode, JSON.stringify(filtersFromDB));
            if (filtersFromDB.length > 0) {
                $scope.setUserAssignedFilter(filtersFromDB);
                $scope.setStepFilter(filtersFromDB);
                $scope.FillIndexFilters(filtersFromDB);
                $scope.$broadcast('hasFiltersFromDBEvent', filtersFromDB);
            }
        }
    };

    $scope.setDisabledCurrentFilters = function () {

        SearchFilterService.SetDisabledAllFiltersByUserViewDoctype($scope.Search.View, $scope.Search.DoctypesIds[0]);
        let docTypeIdFilter = $scope.Search.DoctypesIds[0];
        var filtersFromDB = SearchFilterService.GetFiltersByView(docTypeIdFilter, $scope.Search.View);
        if (!($scope.Search.lastFiltersByView instanceof Map)) {
            $scope.Search.lastFiltersByView = new Map();
        }
        $scope.Search.lastFiltersByView.set($scope.Search.currentMode, JSON.stringify(filtersFromDB));
        if (filtersFromDB.length > 0) {
            $scope.setUserAssignedFilter(filtersFromDB);
            $scope.setStepFilter(filtersFromDB);
            //reset index filters
            $scope.Search.usedFilters = [];
            //set index filters
            $scope.FillIndexFilters(filtersFromDB);
            //reset and set default zamba columns filters
            $scope.$broadcast('hasFiltersFromDBEvent', filtersFromDB);
        }

        $scope.DoSearch();
    }

    $scope.setEnabledCurrentFilters = function () {

        SearchFilterService.SetEnabledAllFiltersByUserViewDoctype($scope.Search.View, $scope.Search.DoctypesIds[0]);
        let docTypeIdFilter = $scope.Search.DoctypesIds[0];
        var filtersFromDB = SearchFilterService.GetFiltersByView(docTypeIdFilter, $scope.Search.View);
        if (!($scope.Search.lastFiltersByView instanceof Map)) {
            $scope.Search.lastFiltersByView = new Map();
        }
        $scope.Search.lastFiltersByView.set($scope.Search.currentMode, JSON.stringify(filtersFromDB));
        if (filtersFromDB.length > 0) {
            $scope.setUserAssignedFilter(filtersFromDB);
            $scope.setStepFilter(filtersFromDB);
            //reset index filters
            $scope.Search.usedFilters = [];
            //set index filters
            $scope.FillIndexFilters(filtersFromDB);
            //reset and set default zamba columns filters
            $scope.$broadcast('hasFiltersFromDBEvent', filtersFromDB);
        }

        $scope.DoSearch();
    }

    $scope.RefreshCurrentResults = function () {

        $scope.Refreshing = true;
        if ($scope.Search.AsignedTasks == false) {
            $scope.DoSearchAttr(false);
        }
        else {
            TaskLoaded = false;
            $scope.Search.LastPage = 0;
            $scope.DoSearch();
        }
    };


    $scope.SearchFrom = null;

    $scope.entitieCounts_ForDoSearch = [];

    //Obtiene la cantidad de registros que tiene la entidad iterada pasada por parametro.
    $scope.RowCounter = function (Entidad) {
        return $scope.entitieCounts_ForDoSearch[$scope.Search.SearchResultsObject.entities.indexOf(Entidad)];
    }

    function isValidIndexDate(validateDate, indexIdElement) {
        var rv = true;


        var dateFormatRegex = /^((0[1-9]|[12][0-9]|3[01])(\/)(0[13578]|1[02]))|((0[1-9]|[12][0-9])(\/)(02))|((0[1-9]|[12][0-9]|3[0])(\/)(0[469]|11))(\/)\d{4}$/;
        var isValid = validateDate.match(dateFormatRegex);
        if (isValid == null) {
            document.getElementById(indexIdElement).style.border = "solid 1px #FF0000";
            toastr.info("Algunas de las fechas ingresadas no son validas");
            rv = false;
        }
        else {
            var arrayDate = validateDate.split("/");
            var dateToValidate = new Date(arrayDate[2], arrayDate[1], arrayDate[0]);
            var minDateValid = new Date(1930, 01, 01);
            if (dateToValidate < minDateValid) {
                toastr.error("La fecha minima aceptada es 01/01/1930");
                rv = false;
            }
        }
        return rv;
    }
    function validateDatesByIndexType(currentSearch) {
        var isValid = true;
        for (var i in currentSearch.Indexs) {


            if (currentSearch.Indexs[i].Type == 4 && $("#" + currentSearch.Indexs[i].ID + "").val() != "") {
                if ($("#" + currentSearch.Indexs[i].ID + "").val() != currentSearch.Indexs[i].Data) {
                    currentSearch.Indexs[i].Data = $("#" + currentSearch.Indexs[i].ID + "").val();
                }
                var validateDate = currentSearch.Indexs[i].Data;
                var indexIdElement = currentSearch.Indexs[i].ID;
                if (validateDate != undefined && !isValidIndexDate(validateDate, indexIdElement)) {
                    isValid = false;
                    break;
                }

            }

            if (currentSearch.Indexs[i].Operator == "Entre") {
                if ($("#" + currentSearch.Indexs[i].ID + "-2").val() != currentSearch.Indexs[i].Data2) {
                    currentSearch.Indexs[i].Data2 = $("#" + currentSearch.Indexs[i].ID + "-2").val();
                }
                var validateDate2 = currentSearch.Indexs[i].Data2;
                var indexIdElement2 = currentSearch.Indexs[i].ID + "-2";
                if (!isValidIndexDate(validateDate2, indexIdElement2)) {
                    isValid = false;
                    break;
                }
            }
        }
        return isValid;
    }

    function columnaSinCaracteresEspeciales(columna) {
        try {
            for (i = 0; i < columna.length - 1; i++) {
                columna = columna
                    .replaceAll(" ", "_").replaceAll("-", "_").replaceAll("%", "_").replaceAll("/", "_")
                    .replaceAll("._", "_").replaceAll("*", "_").replaceAll(".", "_").replaceAll("?", "_").replaceAll("¿", "_")
                    .replaceAll("+", "_").replaceAll("/", "_").replaceAll("&", "_").replaceAll("-", "_").replaceAll("\\", "_")
                    .replaceAll("%", "_").replaceAll(")", "_").replaceAll("(", "_").replaceAll("#", "_")
                    .replaceAll("+", "_").replaceAll("°", "_").replaceAll("__", "_");
            }
            return columna;
        } catch (e) {
            console.error("ocurrio un problema convertir columna en caracteres especiales");
        }

    }

    $rootScope.$on('ExecuteSearch', function (type, Search) {
        try {
            searchModeGSFn(null, 'search');
            if (angular.element($("#taskController")).scope() != undefined) {
                angular.element($("#taskController")).scope().actionRules = null;
            }

            $rootScope.$broadcast('ClearFilters');
            $rootScope.$broadcast('resetFiltersDefaultZambaColumnFilters');
            Search.AsignedTasks = false;
            Search.View = "";

            if ($('#ModalSearch2').hasClass('in')) {
                $("#ModalSearch2").modal('hide');
            }

            Search.StepId = 0;
            Search.stateID = 0;
            $scope.Search.UserAssignedFilter = { IsChecked: false, zFilterWebID: 0 };
            $scope.Search.StepFilter = { IsChecked: false, zFilterWebID: 0 };
            $scope.lastSelectedNode = 0;
            Search.lupdateFilters = [];
            Search.crdateFilters = [];
            Search.nameFilters = [];
            Search.originalFilenameFilters = [];
            Search.stateFilters = [];
            if (!validateDatesByIndexType(Search)) {
                return;
            }

            Search.LastPage = 0;
            Search.OpenTaskOnOneResult = true;
            //set zfiltersweb disable
            SearchFilterService.SetDisabledAllFiltersByUser('Search');
            $scope.Search.UsedZambafilters = 0;

            $scope.ExecuteSearch(true, Search);



        } catch (e) {
            console.error(e);
        }
    });


    $scope.DoSearch = function (reloadResults) {
        try {
            ShowLoadingAnimationNoClose();
            $rootScope.$emit('showLoading');

            $scope.CheckUserToken();
            ResizeButtonsSearch();

            if ($('#ModalSearch').hasClass('in'))
                $("#ModalSearch").modal("hide");

            let btnRefreshGrid = document.querySelector("#btnRefreshGrid");
            if (btnRefreshGrid !== null) {
                btnRefreshGrid.disabled = true;
                btnRefreshGrid.style.opacity = "0.5";
            }

            $scope.Search.UserId = GetUID();
            $scope.Search.GroupsIds = GetGroupsIdsByUserId($scope.Search.UserId);

            if ($scope.Search.View != undefined && $scope.Search.View != null && $scope.Search.View == "") {
                if ($scope.currentMode != undefined && $scope.currentMode != null && $scope.currentMode != "") {
                    $scope.Search.View = $scope.currentMode;
                } else {
                    $scope.Search.View = "search";
                }
            }

            var busquedaValida = true;

            for (var i in $scope.Search.Indexs) {
                $scope.Search.Indexs[i].DropDownList = []
                if ($scope.Search.OrderBy.split(' ').length == 2) {
                    var nombreColumna = $scope.Search.OrderBy.split(' ')[0];
                    var ordenamiento = $scope.Search.OrderBy.split(' ')[1];
                    if (nombreColumna == columnaSinCaracteresEspeciales($scope.Search.Indexs[i].Name)) {
                        $scope.Search.OrderBy = $scope.Search.Indexs[i].Name + ' ' + ordenamiento;
                        break;
                    }
                }

                if ($scope.Search.Indexs[i].Operator == "Entre") {
                    var DataDesde;
                    var DataHasta;
                    switch ($scope.Search.Indexs[i].Type) {
                        case 1: // Numerico
                            DataDesde = parseInt($scope.Search.Indexs[i].Data);
                            DataHasta = parseInt($scope.Search.Indexs[i].Data2);
                            break;
                        case 2: // Numerico largo
                            DataDesde = parseInt($scope.Search.Indexs[i].Data);
                            DataHasta = parseInt($scope.Search.Indexs[i].Data2);
                            break;
                        case 3: // Decimales
                            DataDesde = parseFloat($scope.Search.Indexs[i].Data);
                            DataHasta = parseFloat($scope.Search.Indexs[i].Data2);
                            break;
                        case 4: // Fecha
                            DataDesde = parseDate($scope.Search.Indexs[i].Data);
                            DataHasta = parseDate($scope.Search.Indexs[i].Data2);
                            break;
                        case 5:// Fecha y hora
                            DataDesde = parseDate($scope.Search.Indexs[i].Data);
                            DataHasta = parseDate($scope.Search.Indexs[i].Data2);
                            break;
                        case 6: // Moneda
                            DataDesde = parseFloat($scope.Search.Indexs[i].Data);
                            DataHasta = parseFloat($scope.Search.Indexs[i].Data2);
                            break;
                        case 7: // Alfanumerico
                            DataDesde = $scope.Search.Indexs[i].Data;
                            DataHasta = $scope.Search.Indexs[i].Data2;
                            break;
                        case 8: //Alfanumerico_largo
                            DataDesde = $scope.Search.Indexs[i].Data;
                            DataHasta = $scope.Search.Indexs[i].Data2;
                            break;
                    }


                    if (DataDesde > DataHasta) {
                        toastr.error("El intervalo de búsqueda en el campo ' " + $scope.Search.Indexs[i].Name + "' es incorrecto. 'Desde' debe ser menor o igual que 'Hasta'");
                        busquedaValida = false;
                        hideLoading();
                    }
                }
            }

            if (!busquedaValida) {
                $rootScope.$emit('hideLoading');
                return;
            }

            $scope.Search.PageSize = 100;
            var busqueda = toastr.info("Realizando la búsqueda");
            toastr.options.timeOut = 20000;

            if ($scope.Refreshing) {
                $scope.Search.LastPage = 0;
            }
            return $scope.getResultsFromService(reloadResults, $scope.Search).then(function (response) {

                $scope.LastResponse = response;

                var data = $scope.LastResponse.data;
                data = data.replace(/&_/g, "");
                var SearchResultsObject = JSON.parse(data);

                // Si no trajo resultados
                if (SearchResultsObject == undefined || SearchResultsObject == null || SearchResultsObject.data == undefined || SearchResultsObject.data.length == 0) {

                    if ($scope.Search.AsignedTasks) {
                        toastr.options.timeOut = 5000;
                        toastr.warning("No se encontraron resultados");
                        $scope.Search.AsignedTasks = true;
                        $("#SearchControls").hide();
                        $("#tabresults").show();
                        hideLoading();
                    }
                    else {
                        $scope.Search.SearchResults = [];
                        $scope.Search.SearchResultsObject = null;
                        $scope.Search.LastPage = 0;
                        CleanKGrid();
                        $scope.FillFilters(null);
                        toastr.options.timeOut = 5000;
                        toastr.warning("Por favor intente redefiniendo sus parametros de busqueda", "No se encontro ningun resultado");
                        $scope.Search.HasResults = false;
                        hideLoading();
                        return;
                    }
                } else {
                    $scope.Search.HasResults = true;
                }

                //Asignacion de resultados al objeto Search.----------------------------------------------------------------------------/////

                if ($scope.Search.LastPage === 0 || $scope.Search.SearchResults == undefined || $scope.Search.SearchResultsObject == null || $scope.Refreshing) {

                    $scope.Search.SearchResults = SearchResultsObject.data;
                    $scope.Search.SearchResultsObject = SearchResultsObject;

                }
                else {

                    for (var i = 0; i < SearchResultsObject.data.length; i++) {
                        $scope.Search.SearchResultsObject.data.push(SearchResultsObject.data[i]);
                        $scope.Search.SearchResultsObject.total = SearchResultsObject.total;
                    }
                }

                if ($scope.Search.DoctypesIds.length == 1) {
                    let docTypeIdFilter = $scope.Search.DoctypesIds[0];
                    //obtengo los filtros para 1 entidad
                    var filtersFromDB = SearchFilterService.GetFiltersByView(docTypeIdFilter, $scope.Search.View);
                    if (filtersFromDB.length > 0) {
                        $scope.setUserAssignedFilter(filtersFromDB);
                    }
                }

                //END Asignacion de resultados al objeto Search.----------------------------------------------------------------------------/////


                try {
                    if (reloadResults != undefined && reloadResults == false && $scope.Search.entities != undefined && $scope.Search.entities != null && $scope.Search.entities.length > 0) {
                        $scope.Search.SearchResultsObject.data = $scope.Search.SearchResultsObject.data.filter(x => JSON.stringify($scope.Search.entities.filter(x => x.enabled == true).map(x => x.id)).indexOf(x.DOC_TYPE_ID) != -1);
                        $scope.Search.SearchResultsObject.entities = $scope.Search.entities;
                    }
                } catch (e) {
                    console.error(e);
                }

                try {

                    if ($scope.Search.SearchResultsObject.data.length == 0) {
                        $(".switch").hide();
                    } else {
                        $(".switch").show();
                    }
                } catch (e) {
                    console.error(e);
                }


                if ($scope.entitieCounts_ForDoSearch.length == 0) {
                    $scope.Search.SearchResultsObject.entities.forEach(function (Element) {
                        $scope.entitieCounts_ForDoSearch.push(Element.ResultsCount)
                    })
                }

                $scope.Search.FiltersResetables = false;


                ProcessResults($scope.Search);
                $scope.Search.UsedZambafilters = $scope.sumNonIndexedFilters();
                setTimeout(function () {
                    try {
                        $scope.saveSearchByView($scope.Search);
                    } catch (e) {
                        console.error(e);
                    }
                }, 0);

                if ($scope.currentMode == "search") {
                    if ($scope.Search.lastSearchEntitiesNodes != "") {
                        StoreNodesOnDB($scope.Search.lastSearchEntitiesNodes);
                    }
                    SaveChecksOnLocalStorage();
                    $scope.SearchState = $scope.Search;

                }

                //analizar la continuidad de estos metodos, que son de la interfaz vieja y algunas cosas de la nueva
                try {
                    $("#SearchControls").hide();
                    $("#tabresults").show();
                    $scope.currentModeSearch = 'results';
                } catch (e) {
                    console.error(e);
                }

                try {
                    GoToUpGlobalSearch();
                } catch (e) {
                    console.error(e);
                }

                var currentresult = $scope.Search.SearchResults[0];


                //Open First Result in Search ONLY if is just one result.
                try {
                    if ((reloadResults == undefined || reloadResults == false) && $scope.Search.SearchResults.length == 1 && $scope.OpenTaskOnOneResult == true && $scope.Search.OpenTaskOnOneResult == true) {
                        $scope.OpenTaskResult(currentresult);
                    }
                } catch (e) {
                    console.error(e);
                }
                $scope.Search.OpenTaskOnOneResult = false

                var VirtualEntitiesArray = String($scope.Search.SearchResultsObject.VirtualEntities).split(',');

                if (($scope.Search.AsignedTasks && $scope.Search.LastPage == 0) || VirtualEntitiesArray.includes(String(currentresult.DOC_TYPE_ID))) {
                    SearchFrom = "";
                }
                else {
                    TaskLoaded = false;
                }
                try {
                    setTimeout($scope.visualizerModeGSFn(null, ZambaUserService.VisualizerMode), 500);
                } catch (e) {
                    console.error(e);
                }

                try {

                    if ($scope.Search.LastPage == 0) {
                        try {

                            $scope.RowCounter($scope.Search.SearchResultsObject.entities[2]);
                        } catch (e) {
                            console.error(e);
                        }

                        setTimeout(KendoGrid($scope.Search.SearchResultsObject, $scope.Search.UserId, $scope.Search), 200);
                        setTimeout($scope.FillFilters($scope.Search.SearchResultsObject), 200);
                    }
                    else {
                        RefreshKGrid($scope.Search.SearchResultsObject, $scope.Search);
                        $scope.FillFilters($scope.Search.SearchResultsObject)
                    }
                } catch (e) {
                    console.error(e);
                }

                $scope.checkStatus = false;
                $scope.MultipleSelection(false);
                toastr.clear(busqueda);
                $scope.Refreshing = false;

            }).then(function onSuccess(data, response) {
                //hideLoading();
                $scope.EntitiesCheckEnable = false;
                ResetSwitch();
                showBtns_ForResultsGrid();
                setTimeout(AdjustGridColumns, 500);
                setTimeout(ResizeResultsArea, 500);
                //setTimeout(hideLoading, 1000);
                setTimeout(resizeGridHeight, 1000);
                console.log('------------ DoSearch Done! ----------------');
                $scope.hideRefreshGrid();
                //hideLoading();
                //$rootScope.$emit('hideLoading');

            }).catch(function (data, status, headers, config) {
                hideLoading();
                GSLoading.Hide();
                $scope.message = data.data;
                $scope.Search.SearchResultsObject = null;
                $scope.Search.SearchResults = [];
                $scope.Search.LastPage = 0;
                $scope.FillFilters(null);
                CleanKGrid();
                var r = data.data == undefined ? data.message : data.data.ExceptionMessage;
                console.error(data.message);
                toastr.options.timeOut = 5000;
                toastr.error("Ocurrio un error y no se pudo realizar la carga de resultados. Intente de nuevo");
                console.error('------------ DoSearch ERROR! ----------------');
                $scope.hideRefreshGrid();
                hideLoading();
                $rootScope.$emit('hideLoading');
            });

        } catch (e) {
            hideLoading();
            $rootScope.$emit('hideLoading');
            console.error(e);
        }


    };

    $scope.hideRefreshGrid = function () {
        try {

            let btnRefreshGrid = document.querySelector("#btnRefreshGrid");
            if (btnRefreshGrid !== null) {
                btnRefreshGrid.disabled = false;
                btnRefreshGrid.style.opacity = "1";
            }
        } catch (e) {
            console.error(e);
        }
    }


    $scope.LoadLastSearchState = function (searchState) {
        try {

            $rootScope.$emit('showLoading');

            var busqueda = null;
            if ($scope.currentMode != "search") {
                busqueda = toastr.info("actualizando...");
                toastr.options.timeOut = 20000;
            }

            $scope.Search = searchState;

            if ($scope.currentMode == 'search') {
                $scope.SearchState = searchState;
            }
            ResizeButtonsSearch();


            if ($scope.Search.SearchResultsObject.data.length == 0) {
                $(".switch").hide();
            } else {
                $(".switch").show();
            }


            if ($scope.entitieCounts_ForDoSearch.length == 0) {
                $scope.Search.SearchResultsObject.entities.forEach(function (Element) {
                    $scope.entitieCounts_ForDoSearch.push(Element.ResultsCount)
                })
            }

            $scope.Search.FiltersResetables = false;

            ProcessResults($scope.Search);

            GoToUpGlobalSearch();

            var currentresult = $scope.Search.SearchResults[0];
            var VirtualEntitiesArray = String($scope.Search.SearchResultsObject.VirtualEntities).split(',');

            if (($scope.Search.AsignedTasks && $scope.Search.LastPage == 0) || VirtualEntitiesArray.includes(String(currentresult.DOC_TYPE_ID))) {
                SearchFrom = "";
            }
            else {
                TaskLoaded = false;
            }

            try {
                setTimeout($scope.visualizerModeGSFn(null, ZambaUserService.VisualizerMode), 500);
            } catch (e) {
                console.error(e);
            }

            if ($scope.Search.LastPage == 0) {
                try {
                    $scope.RowCounter($scope.Search.SearchResultsObject.entities[2]);
                } catch (e) {

                }

                setTimeout(KendoGrid($scope.Search.SearchResultsObject, $scope.Search.UserId, $scope.Search), 200);
                setTimeout($scope.FillFilters($scope.Search.SearchResultsObject), 200);
                $scope.Search.UsedZambafilters = $scope.sumNonIndexedFilters();

            }
            else {
                RefreshKGrid($scope.Search.SearchResultsObject, $scope.Search);
                $scope.FillFilters($scope.Search.SearchResultsObject);
                $scope.Search.UsedZambafilters = $scope.sumNonIndexedFilters();
            }

            $scope.checkStatus = false;
            $scope.MultipleSelection(false);

            $scope.EntitiesCheckEnable = false;

            if (busqueda != undefined)
                toastr.clear(busqueda);

            $scope.Refreshing = false;

            // Handle success
            $scope.EntitiesCheckEnable = false;
            ResetSwitch();
            showBtns_ForResultsGrid();
            setTimeout(AdjustGridColumns, 500);
            setTimeout(ResizeResultsArea, 500);
            setTimeout(resizeGridHeight, 1500);
            console.log('------------ DoSearch Done! ----------------');
            hideLoading();
            $rootScope.$emit('hideLoading');
            return true;
        } catch (e) {
            console.error(e);
            hideLoading();
            $rootScope.$emit('hideLoading');
            GSLoading.Hide();
            $scope.Search.SearchResultsObject = null;
            $scope.Search.SearchResults = [];
            $scope.Search.LastPage = 0;
            $scope.FillFilters(null);
            CleanKGrid();
            if ($scope.currentMode != "search") {
                toastr.options.timeOut = 5000;
                toastr.error("No se encontraron resultados");
            }
            hideLoading();
            $rootScope.$emit('hideLoading');
            return false;
        }
        finally {
            hideLoading();

        }

    };


    $scope.getResultsFromService = function (reloadResults, Search) {
        if (reloadResults == undefined || reloadResults == null || reloadResults == true || $scope.LastResponse == undefined || $scope.LastResponse == null) {
            return $http.post(ZambaWebRestApiURL + '/search/DoSearch', Search);
        }
        else {
            return new Promise(function (resolve, reject) {
                setTimeout(function () {
                    resolve($scope.LastResponse);
                }, 1);
            });
        }
    }




    $scope.ExecuteSearch = function (reloadResults, currentSearch) {
        try {

            $scope.CheckUserToken();

            ShowLoadingAnimationNoClose();

            ResizeButtonsSearch();

            if ($('#ModalSearch').hasClass('in'))
                $("#ModalSearch").modal("hide");

            currentSearch.UserId = GetUID();
            currentSearch.GroupsIds = GetGroupsIdsByUserId(currentSearch.UserId);

            if (currentSearch.View != undefined && currentSearch.View != null && currentSearch.View == "") {
                if ($scope.currentMode != undefined && $scope.currentMode != null && $scope.currentMode != "") {
                    currentSearch.View = $scope.currentMode;
                } else {
                    currentSearch.View = "search";
                }
            }

            var busquedaValida = true;
            for (var i in currentSearch.Indexs) {
                currentSearch.Indexs[i].DropDownList = []
                if (currentSearch.OrderBy.split(' ').length == 2) {
                    var nombreColumna = currentSearch.OrderBy.split(' ')[0];
                    var ordenamiento = currentSearch.OrderBy.split(' ')[1];
                    if (nombreColumna == columnaSinCaracteresEspeciales(currentSearch.Indexs[i].Name)) {
                        currentSearch.OrderBy = currentSearch.Indexs[i].Name + ' ' + ordenamiento;
                        break;
                    }
                }

                if (currentSearch.Indexs[i].Operator == "Entre") {
                    var DataDesde;
                    var DataHasta;
                    switch (currentSearch.Indexs[i].Type) {
                        case 1: // Numerico
                            DataDesde = parseInt(currentSearch.Indexs[i].Data);
                            DataHasta = parseInt(currentSearch.Indexs[i].Data2);
                            break;
                        case 2: // Numerico largo
                            DataDesde = parseInt(currentSearch.Indexs[i].Data);
                            DataHasta = parseInt(currentSearch.Indexs[i].Data2);
                            break;
                        case 3: // Decimales
                            DataDesde = parseFloat(currentSearch.Indexs[i].Data);
                            DataHasta = parseFloat(currentSearch.Indexs[i].Data2);
                            break;
                        case 4: // Fecha
                            DataDesde = parseDate(currentSearch.Indexs[i].Data);
                            DataHasta = parseDate(currentSearch.Indexs[i].Data2);
                            break;
                        case 5:// Fecha y hora
                            DataDesde = parseDate(currentSearch.Indexs[i].Data);
                            DataHasta = parseDate(currentSearch.Indexs[i].Data2);
                            break;
                        case 6: // Moneda
                            DataDesde = parseFloat(currentSearch.Indexs[i].Data);
                            DataHasta = parseFloat(currentSearch.Indexs[i].Data2);
                            break;
                        case 7: // Alfanumerico
                            DataDesde = currentSearch.Indexs[i].Data;
                            DataHasta = currentSearch.Indexs[i].Data2;
                            break;
                        case 8: //Alfanumerico_largo
                            DataDesde = currentSearch.Indexs[i].Data;
                            DataHasta = currentSearch.Indexs[i].Data2;
                            break;
                    }


                    if (DataDesde > DataHasta) {
                        toastr.error("El intervalo de búsqueda en el campo ' " + currentSearch.Indexs[i].Name + "' es incorrecto. 'Desde' debe ser menor o igual que 'Hasta'");
                        busquedaValida = false;
                        hideLoading();
                    }
                }
            }

            if (!busquedaValida) {
                return;
            }

            currentSearch.PageSize = 100;
            var busqueda = toastr.info("Realizando la búsqueda");
            toastr.options.timeOut = 20000;

            if ($scope.Refreshing) {
                currentSearch.LastPage = 0;
            }

            return $scope.getResultsFromService(reloadResults, currentSearch).then(function (response) {

                $scope.LastResponse = response;

                var data = $scope.LastResponse.data;
                data = data.replace(/&_/g, "");
                var SearchResultsObject = JSON.parse(data);

                // Si no trajo resultados
                if (SearchResultsObject == undefined || SearchResultsObject == null || SearchResultsObject.data == undefined || SearchResultsObject.data.length == 0) {

                    if (currentSearch.AsignedTasks) {
                        toastr.options.timeOut = 5000;
                        toastr.warning("No se encontraron resultados");
                        currentSearch.AsignedTasks = true;
                        $("#SearchControls").hide();
                        $("#tabresults").show();
                    }
                    else {
                        currentSearch.SearchResults = [];
                        currentSearch.SearchResultsObject = null;
                        currentSearch.LastPage = 0;
                        CleanKGrid();
                        $scope.FillFilters(null);
                        toastr.options.timeOut = 5000;
                        toastr.warning("Por favor intente redefiniendo sus parametros de busqueda", "No se encontro ningun resultado");
                        currentSearch.HasResults = false;
                        return;
                    }
                } else {
                    currentSearch.HasResults = true;
                }

                //Asignacion de resultados al objeto Search.----------------------------------------------------------------------------/////

                if (currentSearch.LastPage === 0 || currentSearch.SearchResults == undefined || currentSearch.SearchResultsObject == null || $scope.Refreshing) {

                    currentSearch.SearchResults = SearchResultsObject.data;
                    currentSearch.SearchResultsObject = SearchResultsObject;
                }
                else {

                    for (var i = 0; i < SearchResultsObject.data.length; i++) {
                        currentSearch.SearchResultsObject.data.push(SearchResultsObject.data[i]);
                        currentSearch.SearchResultsObject.total = SearchResultsObject.total;
                    }
                }


                //END Asignacion de resultados al objeto Search.----------------------------------------------------------------------------/////


                try {
                    if (reloadResults != undefined && reloadResults == false && currentSearch.entities != undefined && currentSearch.entities != null && currentSearch.entities.length > 0) {
                        currentSearch.SearchResultsObject.data = currentSearch.SearchResultsObject.data.filter(x => JSON.stringify(currentSearch.entities.filter(x => x.enabled == true).map(x => x.id)).indexOf(x.DOC_TYPE_ID) != -1);
                        currentSearch.SearchResultsObject.entities = currentSearch.entities;
                    }
                } catch (e) {
                    console.error(e);
                }

                try {

                    if (currentSearch.SearchResultsObject.data.length == 0) {
                        $(".switch").hide();
                    } else {
                        $(".switch").show();
                    }
                } catch (e) {
                    console.error(e);
                }


                if ($scope.entitieCounts_ForDoSearch.length == 0) {
                    currentSearch.SearchResultsObject.entities.forEach(function (Element) {
                        $scope.entitieCounts_ForDoSearch.push(Element.ResultsCount)
                    })
                }

                currentSearch.FiltersResetables = false;


                ProcessResults(currentSearch);
                currentSearch.UsedZambafilters = $scope.sumNonIndexedFilters();
                setTimeout(function () {
                    try {
                        $scope.saveSearchByView(currentSearch);
                    } catch (e) {
                        console.error(e);
                    }
                }, 0);

                if ($scope.currentMode == "search") {
                    if (currentSearch.lastSearchEntitiesNodes != "") {
                        StoreNodesOnDB(currentSearch.lastSearchEntitiesNodes);
                    }
                    SaveChecksOnLocalStorage();
                    $scope.SearchState = currentSearch;

                }

                //analizar la continuidad de estos metodos, que son de la interfaz vieja y algunas cosas de la nueva
                try {
                    $("#SearchControls").hide();
                    $("#tabresults").show();
                    $scope.currentModeSearch = 'results';
                } catch (e) {
                    console.error(e);
                }

                try {
                    GoToUpGlobalSearch();
                } catch (e) {
                    console.error(e);
                }

                var currentresult = currentSearch.SearchResults[0];


                //Open First Result in Search ONLY if is just one result.
                try {
                    if ((reloadResults == undefined || reloadResults == false) && currentSearch.SearchResults.length == 1 && $scope.OpenTaskOnOneResult == true && currentSearch.OpenTaskOnOneResult == true) {
                        $scope.OpenTaskResult(currentresult);
                    }
                } catch (e) {
                    console.error(e);
                }
                currentSearch.OpenTaskOnOneResult = false

                var VirtualEntitiesArray = String(currentSearch.SearchResultsObject.VirtualEntities).split(',');

                if ((currentSearch.AsignedTasks && currentSearch.LastPage == 0) || VirtualEntitiesArray.includes(String(currentresult.DOC_TYPE_ID))) {
                    SearchFrom = "";
                }
                else {
                    TaskLoaded = false;
                }
                try {
                    setTimeout($scope.visualizerModeGSFn(null, ZambaUserService.VisualizerMode), 500);
                } catch (e) {
                    console.error(e);
                }

                try {

                    if (currentSearch.LastPage == 0) {
                        try {

                            $scope.RowCounter(currentSearch.SearchResultsObject.entities[2]);
                        } catch (e) {
                            console.error(e);
                        }

                        setTimeout(KendoGrid(currentSearch.SearchResultsObject, currentSearch.UserId, currentSearch), 200);
                        setTimeout($scope.FillFilters(currentSearch.SearchResultsObject), 200);
                    }
                    else {
                        RefreshKGrid(currentSearch.SearchResultsObject, currentSearch);
                        $scope.FillFilters(currentSearch.SearchResultsObject)
                    }
                } catch (e) {
                    console.error(e);
                }
                $scope.Search = currentSearch;
                if ($scope.Search.DoctypesIds.length == 1) {
                    let docTypeIdFilter = $scope.Search.DoctypesIds[0];
                    //obtengo los filtros para 1 entidad
                    var filtersFromDB = SearchFilterService.GetFiltersByView(docTypeIdFilter, 'search');
                    if (filtersFromDB.length > 0) {
                        $scope.setUserAssignedFilter(filtersFromDB);
                        $scope.setStepFilter(filtersFromDB);
                        $scope.FillIndexFilters(filtersFromDB);
                        $scope.$broadcast('hasFiltersFromDBEvent', filtersFromDB);
                    }
                }
                $scope.checkStatus = false;
                $scope.MultipleSelection(false);
                toastr.clear(busqueda);
                $scope.Refreshing = false;

            }).then(function onSuccess(data, response) {
                hideLoading();
                $scope.EntitiesCheckEnable = false;
                ResetSwitch();
                showBtns_ForResultsGrid();
                setTimeout(AdjustGridColumns, 500);
                setTimeout(ResizeResultsArea, 500);
                console.log('------------ DoSearch Done! ----------------');
            }).catch(function (data, status, headers, config) {
                hideLoading();
                GSLoading.Hide();
                $scope.message = data.data;
                currentSearch.SearchResultsObject = null;
                currentSearch.SearchResults = [];
                currentSearch.LastPage = 0;
                $scope.FillFilters(null);
                CleanKGrid();
                var r = data.data == undefined ? data.message : data.data.ExceptionMessage;
                console.error(data.message);
                toastr.options.timeOut = 5000;
                toastr.error("No se encontraron resultados");
                console.error('------------ DoSearch ERROR! ----------------');
            });

        } catch (e) {
            hideLoading();
            console.error(e);
        }


    };





    //Reestablece el switch de la grilla de resultados
    function ResetSwitch() {
        try {
            if ($("#chkThumbGrid")[0].checked == true)
                $("#chkThumbGrid")[0].checked = false
        } catch (e) {
            console.error("ERROR: " + e.messages);
        }
    }

    //Muestra una columna del KendoGrid.
    function ShowColumn_Function(Column) {
        try {
            $('#Kgrid').data("kendoGrid").showColumn(Column)
        }
        catch (e) {
            try {
                $('#Kgrid').data("kendoGrid").showColumn(Column)
            } catch (e) { }
        }
    }

    //Oculta una columna del KendoGrid.
    function hideColumn_Function(Column) {
        try {
            $('#Kgrid').data("kendoGrid").hideColumn(Column)
        }
        catch (e) {
            try {
                $('#Kgrid').data("kendoGrid").hideColumn(Column)
            } catch (e) { }
        }
    }

    //Obtiene la coleccion de columnas de DoSearch() y oculta las columnas que no existan de un lado o del otro.
    function validateGridColumns(SearchResults_DoSearch) {

        if ($("#Kgrid").css('display') === 'block') {

            var gridColumns = $('#Kgrid').data("kendoGrid").columns;

            var ColumnaExiste;
            var field;
            for (var i = 0; i < gridColumns.length; i++) {

                ColumnaExiste = false;
                field = gridColumns[i].field;

                if (field != undefined && field.toLowerCase() != 'icon') {

                    for (var j = 0; j < SearchResults_DoSearch.columns.length; j++) {

                        if (SearchResults_DoSearch.columns[j].toLowerCase() == field.toLowerCase()) {
                            ColumnaExiste = true;
                            break;
                        }
                    }

                    if (ColumnaExiste) {

                        if (gridColumns[i].hidden == undefined || gridColumns[i].hidden == false || gridColumns[i].hidden == "false") {
                            ShowColumn_Function(gridColumns[i]);
                            gridColumns[i].hidden = "false";
                        }
                        else {
                            hideColumn_Function(gridColumns[i]);
                        }
                    }
                    else {
                        hideColumn_Function(gridColumns[i]);
                        gridColumns[i].hidden = "true";
                    }

                }
            }

            setTimeout(AdjustGridColumns, 500);
            setTimeout(ResizeResultsArea, 500);
        }
    }

    $scope.searchGridText = "";


    $scope.doSearchGS = function () {

        SearchFilterService.SetDisabledAllFiltersByUser('Search');

        searchModeGSFn(null, "search");

        $scope.Search.UserId = GetUID();

        if ($scope.isPagging) {
            $scope.isPagging = false;
        }
        else {
            $scope.page = 0;
            $scope.Search.SearchResults = [];
            $scope.isLastPage = false;
        }
        if ($scope.isLastPage) return;

        var busqueda = toastr.info("Buscando...");
        toastr.options.timeOut = 20000;

        var $p = (JSON.parse(JSON.stringify($scope.model.parameters)));
        for (var i = 0; i <= $p.length - 1; i++) {
            if ($p[i] != undefined && $p[i].type == 1 && $p[i].value != '' && $p[i].value.indexOf('| Empieza: ') != 1) {
                $p[i].value = $p[i].value.replace('| Empieza: ', '');
            }
            if ($p[i] != undefined && $p[i].value2 != "") {
                $p[i].value = $p[i].value.substr(0, $p[i].value.indexOf("-")).trim();
            }
        }
        var filterCount = parseInt($("#filterCountDiv").attr("value"));
        var parameters = { Parameters: $p, UserId: parseInt(GetUID()) };
        parameters.SizePage = { LastPage: $scope.page, PageSize: $scope.pageSize };

        var param = JSON.stringify($scope.parameters);
        $scope.searchQuery = parameters;

        selectDescGSFn($p);

        $http({
            method: 'POST',
            dataType: 'json',
            url: ZambaWebRestApiURL + '/search/Results',
            data: parameters,
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
        }).
            then(function (d, status, headers, config) {
                $scope.ProcessSearch(d.data);

                if (d.data.total > 0)
                    $scope.Search.SearchResults.total = d.data.total;
                hideLoading();

            }).then(function onSuccess(data, response) {
                // Handle success
                toastr.clear(busqueda);
                var currentresult = $scope.Search.SearchResults[0];
                var VirtualEntities = ZambaUserService.getSystemPreferences('VirtualEntities');
                var VirtualEntitiesArray = String(VirtualEntities).split(',');
                if (($scope.Search.AsignedTasks && $scope.Search.LastPage == 0) || VirtualEntitiesArray.includes(currentresult.DOC_TYPE_ID)) {
                    SearchFrom = "";
                    $("#resultsGridSearchBox").hide();
                    $("#resultsGridSearchBoxThumbs").hide();
                    $("#resultsGridSearchBoxPreview").hide();
                    $("#resultsGridSearchGrid").show();
                    $("#Kgrid").show();
                }
                else {

                    $("#tabresults").show();
                    $("#resultsGridSearchBox").hide();
                    $("#resultsGridSearchBoxThumbs").hide();

                    $("#resultsGridSearchBoxPreview").css('display', 'inline-block');
                    $("#resultsGridSearchGrid").show();
                    $("#Kgrid").hide();
                    $scope.GetNextUrl(-1);
                }

                if ($("#Kgrid").css('display') === 'block') {
                    setTimeout(AdjustGridColumns, 500);
                    setTimeout(ResizeResultsArea, 500);
                }
            }).catch(function (data, status, headers, config) {
                GSLoading.Hide();
                $scope.message = data.data;
                $scope.Search.SearchResults = null;
                var r = data.data == undefined ? data.message : data.data.ExceptionMessage;
                console.log(data.message);
                if (r != null) {
                    var error = r.indexOf("\n") > -1 ? r.substring(0, r.indexOf("\n")) : r;
                    toastr.warning("No se encontraron archivos", "Error al cargar indices");
                }
            });
    };

    $scope.myFunct = function (keyEvent) {
        if (keyEvent.which === 13)
            alert('I am an alert');
    }



    $scope.NewGetUserRigths = function (RightType, ObjectId) {
        var result;
        var genericRequest = {
            UserId: parseInt(GetUID()),
            Params:
            {
                "ObjectId": ObjectId,
                "RightType": RightType
            }
        };


        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Account/NewGetUserRight',
            contentType: 'application/json',
            async: false,
            data: JSON.stringify(genericRequest),
            success: function (response) {
                result = JSON.parse(response);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                result = XMLHttpRequest;
            }
        });

        return result;
    }


    var addItem = function (item, list) {
        list.push(item);
    },
        removeItem = function (item, list) {
            for (listItem in list) {
                if (list[listItem] == item) {
                    list.splice(listItem, 1);
                }
            }
        };


    $scope.resetMultipleSelection = function () {
        var thumbsCollection = $("#resultsGridSearchBoxThumbs").find(".glyphicon-ok-sign");
        thumbsCollection.addClass("glyphicon glyphicon-ok-circle");
        thumbsCollection.parent().parent().parent().css("border", "1px solid rgb(221, 221, 221)")
        thumbsCollection.removeClass("glyphicon-ok-sign");
        $("#resultsGridSearchBoxThumbs").find(".glyphicon-info-sign").show();
        $("#resultsGridSearchBoxThumbs").find(".glyphicon-zoom-in").show();
        $("#resultsGridSearchBoxThumbs").find(".resultsGrid ").css("width", "150px");
        $scope.thumbSelectedIndexs = [];
        $("#multipleSelectionMenu").hide();
        $(".filterFunc").show();

    }

    $scope.resetMultipleSelectionPreview = function () {
        var thumbsCollection = $("#resultsGridSearchBoxPreview").find(".glyphicon-ok-sign");
        thumbsCollection.addClass("glyphicon glyphicon-ok-circle");
        thumbsCollection.removeClass("glyphicon-ok-sign");
        $("#resultsGridSearchBoxPreview").find(".glyphicon-new-window").show();
        $("#resultsGridSearchBoxPreview").find(".glyphicon-download-alt").show();
        $scope.thumbSelectedIndexs = [];
        $("#multipleSelectionPreview").hide();
        let multipleChoiseSelected = document.querySelectorAll(".selectMultipleThumbsEnable");
        multipleChoiseSelected.forEach(multipleChoise => {
            multipleChoise.style.background = "rgb(66, 133, 244)";
        })

    }

    $scope.DisableActions = function () {
        try {
            //   document.getElementById("BtnClearCheckbox").removeAttribute("disabled", "disabled");
            if (document.getElementById("BtnSendEmail") != undefined)
                document.getElementById("BtnSendEmail").removeAttribute("disabled", "disabled");
            document.getElementById("OpenAllSelected").removeAttribute("disabled", "disabled");

            if (document.getElementById("BtnSendZip") != undefined)
                document.getElementById("BtnSendZip").removeAttribute("disabled", "disabled");

            if (document.getElementById("BtnDownloadZip") != undefined)
                document.getElementById("BtnDownloadZip").removeAttribute("disabled", "disabled");

            document.getElementById("BtnDerivar").removeAttribute("disabled", "disabled");
            document.getElementById("panel_ruleActions").removeAttribute("disabled", "disabled");

            $("#Actions").css('display', 'inline');
        } catch (e) {
            console.error("ERROR: " + e.messages);
        }
    }

    $scope.EnableActions_FromListIds = function (e) {
        if ($scope.thumbSelectedIndexs.length > 0) {
            //  document.getElementById("BtnClearCheckbox").removeAttribute("disabled");
            if (document.getElementById("BtnSendEmail") != undefined)
                document.getElementById("BtnSendEmail").removeAttribute("disabled");
            document.getElementById("OpenAllSelected").removeAttribute("disabled");

            if (document.getElementById("BtnSendZip") != undefined)
                document.getElementById("BtnSendZip").removeAttribute("disabled");

            if (document.getElementById("BtnDownloadZip") != undefined)
                document.getElementById("BtnDownloadZip").removeAttribute("disabled");

            document.getElementById("BtnDerivar").removeAttribute("disabled");
            document.getElementById("panel_ruleActions").removeAttribute("disabled");

            //se fija si tiene workflows, si no tiene workflows como en el caso de RPI no muestra esos dos botones
            var scope_TreeViewController = angular.element($("#SidebarTree")).scope();
            if (scope_TreeViewController != undefined) {
                if (scope_TreeViewController.ChildsEntities.length > 0) {
                    $("#OpenAllSelected").css("display", "none");
                    $("#BtnDerivar").css("display", "none");
                }
            }

            $("#Actions").css('display', 'inline');
        } else {
            //  document.getElementById("BtnClearCheckbox").setAttribute("disabled", "disabled");
            if (document.getElementById("BtnSendEmail") != undefined)
                document.getElementById("BtnSendEmail").setAttribute("disabled", "disabled");

            document.getElementById("OpenAllSelected").setAttribute("disabled", "disabled");

            if (document.getElementById("BtnSendZip") != undefined)
                document.getElementById("BtnSendZip").setAttribute("disabled", "disabled");

            if (document.getElementById("BtnDownloadZip") != undefined)
                document.getElementById("BtnDownloadZip").setAttribute("disabled", "disabled");

            document.getElementById("BtnDerivar").setAttribute("disabled", "disabled");
            document.getElementById("panel_ruleActions").setAttribute("disabled", "disabled");

            $("#Actions").css('display', 'none');
        }
    }

    $scope.showSeletionModeByPreview = function (event, arg) {
        var previewTooltip = document.querySelector(".tooltip");
        if (previewTooltip != null || previewTooltip != "")
            previewTooltip.style.display = "none";
        var checkButton = null;
        if ($(event.target).hasClass("glyphicon-ok-sign")) {
            checkButton = $($(event.target).parents(".resultsGrid")[0]).find(".glyphicon-ok-sign");
            if (checkButton.hasClass("selectMultipleThumbsEnable")) {
                checkButton.removeClass("selectMultipleThumbsEnable");
            }
            checkButton.addClass("glyphicon glyphicon-ok-circle selectMultipleThumbsDisable");
            $(event.target).css("background-color", "#4285f4");
            checkButton.removeClass("glyphicon-ok-sign");
        } else {
            checkButton = $($(event.target).parents(".resultsGrid")[0]).find(".glyphicon-ok-circle");
            if (checkButton.hasClass("selectMultipleThumbsDisable")) {
                checkButton.removeClass("selectMultipleThumbsDisable");
            }
            checkButton.addClass("glyphicon glyphicon-ok-sign selectMultipleThumbsEnable");
            $(event.target).css("background-color", "#4ca74c");
            checkButton.removeClass("glyphicon-ok-circle");
        }

        var thumbsCollection = $("#resultsGridSearchBoxPreview").find(".glyphicon-ok-sign");

        if (thumbsCollection.length > 0) {
            $("#multipleSelectionPreview").fadeIn();
            $("#resultsGridSearchBoxPreview").find(".glyphicon-new-window").hide();
            $("#resultsGridSearchBoxPreview").find(".glyphicon-download-alt").hide();
            //$("#multipleSelectionMenu").fadeIn();
        } else {
            $("#multipleSelectionPreview").fadeOut();
            $("#resultsGridSearchBoxPreview").find(".glyphicon-new-window").show();
            $("#resultsGridSearchBoxPreview").find(".glyphicon-download-alt").show();
            //$("#multipleSelectionMenu").fadeOut();
        }

        if ($(event.target).hasClass("glyphicon-ok-sign")) {
            addItem(arg, $scope.thumbSelectedIndexs);
        } else {
            removeItem(arg, $scope.thumbSelectedIndexs);
        }

        /*se ha solicitado que no se muestren los botones de acciones. en dos de las 3 vistas. Por eso se comenta*/
        //$scope.EnableActions_FromListIds();

    }

    $scope.showSeletionModeByCkeck = function (event, arg) {
        thumbContainerResize(event.target);
        thumbButtonDisplay();

        if ($(event.target).hasClass("glyphicon-ok-sign")) {
            addItem(arg, $scope.thumbSelectedIndexs);

        } else {
            removeItem(arg, $scope.thumbSelectedIndexs);
        }

        var thumbsCollection = $("#resultsGridSearchBoxThumbs").find(".glyphicon-ok-sign");
        if (thumbsCollection.length == 0) {
            $(".filterFunc").show();
        } else {
            $(".filterFunc").hide();
        }
    }

    $scope.HideGBModal = function (event, arg) {
        showSeletionModeByimage(event.target);
        var isGlobalSearch = false;
        if ($(event.target).parents("#Advfilter1").length) isGlobalSearch = true;
        if (isGlobalSearch && zambaApplication != "ZambaSearch") {
            $('#Advfilter-modal-content').slideToggle();
            $('#Advfilter2').fadeIn().css('display', 'inline-flex');
            $('.favAdvSearch').fadeIn();
            $('#Advfilter2').children('.advancedSearchBox').css({
                display: 'block', width: 'auto'
            });
        }
        if ($(event.target).parent(".photo").find(".glyphicon-ok-sign").length == 0) {
            removeItem(arg, $scope.thumbSelectedIndexs);
        } else {
            addItem(arg, $scope.thumbSelectedIndexs);
        }

        var thumbsCollection = $("#resultsGridSearchBoxThumbs").find(".glyphicon-ok-sign");
        if (thumbsCollection.length == 0) {
            $(".filterFunc").show();
        } else {
            $(".filterFunc").hide();
        }
    }


    //****************************************************OPENED TASKS------------------------------------------------
    $scope.OpenedTasks = [];
    $scope.AUXOpenedTasks = [];


    $scope.OpenTaskResult = function (result) {

        let userToken = JSON.parse(localStorage.getItem('authorizationData'));
        let { token } = userToken;


        var url;

        if (result.Task_Id != undefined && result.Task_Id != null && result.Task_Id > 0) {
            url = (thisDomain + "/views/WF/TaskViewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&taskid=" + result.Task_Id + "&mode=s" + "&s=" + 0 + "&userId=" + GetUID() + "&t=" + token);
        }
        else {
            url = (thisDomain + "/views/search/docviewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&mode=s" + "&userId=" + GetUID() + "&t=" + token);
        }

        window.open(url, "R" + result.DOC_ID);


        if (!result.IsRead) {
            SetRead(result);
        }

    };

    //#region Open Task
    $scope.Opentask = function (arg) {
        var thumbsCollection = $("#resultsGridSearchBoxThumbs").find(".glyphicon-ok-sign");
        $scope.thumbsCheckedCount = thumbsCollection.length;

        if (thumbsCollection.length == 0) {
            var result = $scope.Search.SearchResults[arg];
            var userid = GetUID();

            var stepid = result.STEP_ID;
            if (stepid == undefined) stepid = result.Step_Id;

            var taskId = result.TASK_ID;
            if (taskId == undefined) taskId = result.Task_Id;

            var docId = result.DOC_ID;
            if (docId == undefined) docId = result.Doc_Id;

            let userToken = JSON.parse(localStorage.getItem('authorizationData'));
            let { token } = userToken;

            const TypeRightUse = 19;

            var esTaskViewer = false;
            if (stepid != null && stepid != undefined && stepid != 0) {
                esTaskViewer = validateUserRight(stepid, TypeRightUse)
            }

            var Url = $scope.GetTaskUrl(esTaskViewer, result, taskId, stepid, userid, token);

            //valido si esta el preview activo y si hay cambios en la tarea
            if ($scope.PreviewMode != "noPreview") {
                var PreviewTaskChanged = localStorage.getItem("PreviewTaskChanged");
                if (PreviewTaskChanged == "true") {
                    localStorage.removeItem("PreviewTaskChanged");
                    swal({
                        title: "Hay modificaciones en la tarea actual.",
                        text: "Desea guardar los cambios realizados?",
                        icon: "warning",
                        allowClickOutSide: false,
                        buttons: ["No", "Si"],
                        dangerMode: true,
                    })
                        .then((willSave) => {
                            if (willSave) {
                                console.log("Guardando Cambios");
                                var ElementPreview = document.getElementById("IFPreview");
                                $($(ElementPreview.contentDocument).find("#zamba_save")).click();
                                $scope.SwitchZambaAplication(result, Url, userid, docId);
                            } else {
                                $scope.SwitchZambaAplication(result, Url, userid, docId);
                            }
                        });
                } else {
                    $scope.SwitchZambaAplication(result, Url, userid, docId);
                }
            } else {
                $scope.SwitchZambaAplication(result, Url, userid, docId);
            }

            //-----------------------------------REFACTOR TO TASK SERVICE-------------------------------------
            $scope.NotifyDocumentReading(result, userid);
            //-----------------------------------REFACTOR TO TASK SERVICE-------------------------------------

        } else {
            $scope.onSelectionMode = true;
        }
    };

    $scope.SwitchZambaAplication = function (result, Url, userid, docId) {
        switch (zambaApplication) {
            case "ZambaWeb":
                OpenDocTask3(result.TASK_ID, result.DOC_ID, result.DOC_TYPE_ID, false, "Reemplazar", Url, userid, 0);
                $('#Advfilter1').modal("hide");
                break;
            case "ZambaWindows":
            case "ZambaHomeWidget":
            case "ZambaQuickSearch":
                winFormJSCall.openTask(result.DOC_TYPE_ID, result.DOC_ID, result.TASK_ID, result.STEP);
                $('#Advfilter1').modal("hide");
                break;
            case "Zamba":
                window.open(Url, '_blank');
                break;
            case "ZambaSearch":
                OpenTaskOnBrowser(Url, docId);
                break;
        }
    }

    function OpenTaskOnBrowser(Url, docId) {
        var OpenedTasksFlag = true;

        //$scope.currentPreviewInTab = true;
        try {
            //Valida si hay tareas.
            if ($scope.OpenedTasks && $scope.OpenedTasks != undefined && $scope.OpenedTasks.length > 0) {
                $scope.OpenedTasks.forEach(function (elem, index) {
                    //Valida si la tareaa esta abierta.
                    if ($scope.OpenedTasks[index] != undefined && $scope.OpenedTasks[index] != null && $scope.OpenedTasks[index].name != "" && $scope.OpenedTasks[index].GetDOCID() == docId && $scope.OpenedTasks[index].GetUnLoaded() == false) {
                        $scope.OpenedTasks[index].focus();

                        if ($scope.PreviewMode != "noPreview") {
                            $scope.PreviewMode = "noPreview";
                            $scope.LayoutPreview = "row";
                        }

                        $scope.currentPreviewInTab = true;
                        document.getElementById('IFThisTaskIsOpen').setAttribute('src', thisDomain + "/Scripts/app/partials/ThisTaskIsOpen.html");

                        if (OpenedTasksFlag) {
                            OpenedTasksFlag = false;
                        }
                    }
                });

                if (OpenedTasksFlag) {
                    if (!$scope.checkStatus) {
                        $scope.OpenedTasks.push(window.open(Url, "R" + docId));
                    } else {
                        $scope.AUXOpenedTasks.push(window.open(Url, "R" + docId));
                    }

                    if ($scope.PreviewMode != "noPreview") {
                        $scope.PreviewMode = "noPreview";
                        $scope.LayoutPreview = "row";
                    }
                }
            }
            else {
                //Puede que el preview este activo.
                if (!$scope.checkStatus) {
                    $scope.OpenedTasks.push(window.open(Url, "R" + docId));
                } else {
                    $scope.AUXOpenedTasks.push(window.open(Url, "R" + docId));
                }

                if ($scope.PreviewMode != "noPreview") {
                    $scope.PreviewMode = "noPreview";
                    $scope.LayoutPreview = "row";
                }
            }
        }
        catch (e) {
            if (!$scope.checkStatus) {
                $scope.OpenedTasks.push(window.open(Url, "R" + docId));
            } else {
                $scope.AUXOpenedTasks.push(window.open(Url, "R" + docId));
            }

            if ($scope.PreviewMode != "noPreview") {
                //document.getElementById("IFPreview").style["display"] = "none";
                $scope.PreviewMode = "noPreview";
                $scope.LayoutPreview = "row";
            }
        }
    }

    $scope.ScopeOpenTaskOnBrowser = function () {
        try {
            var Url = $scope.PreviewerUsedBy.URL;
            var docId = $scope.PreviewerUsedBy.DocID;

            //valido si esta el preview activo y si hay cambios en la tarea
            if ($scope.PreviewMode != "noPreview") {
                $scope.PreviewMode = "noPreview";
                $scope.LayoutPreview = "row";

                var PreviewTaskChanged = localStorage.getItem("PreviewTaskChanged");
                if (PreviewTaskChanged == "true") {
                    localStorage.removeItem("PreviewTaskChanged");
                    swal({
                        title: "Hay modificaciones en la tarea actual.",
                        text: "Desea guardar los cambios realizados?",
                        icon: "warning",
                        allowClickOutSide: false,
                        buttons: ["No", "Si"],
                        dangerMode: true,
                    })
                        .then((willSave) => {
                            if (willSave) {
                                console.log("Guardando Cambios");

                                var ElementPreview = document.getElementById("IFPreview");
                                $($(ElementPreview.contentDocument).find("#zamba_save")).click();

                                TmrZambaSave = setInterval(function () {
                                    var ZambaSaveResult = localStorage.getItem("ZambaSaveResult");

                                    if (ZambaSaveResult) {
                                        clearInterval(TmrZambaSave);

                                        OpenTaskOnBrowser(Url, docId);
                                        localStorage.removeItem("ZambaSaveResult");
                                    }
                                }, 1000);

                            } else {
                                OpenTaskOnBrowser(Url, docId);
                            }
                        });
                } else {
                    OpenTaskOnBrowser(Url, docId);
                }
            }
            else {
                OpenTaskOnBrowser(Url, docId);
            }

            $scope.LayoutPreview = "row";

            if ($scope.LayoutPreview == "row") {
                resizeGridHeight();
            } else if ($scope.LayoutPreview == "column") {
                resizeTabHome();
            }

        } catch (e) {
            console.error(e);
        }
    }

    $scope.SaveTaskBeforeClosePreview = function () {
        try {
            //valido si esta el preview activo y si hay cambios en la tarea
            if ($scope.PreviewMode != "noPreview") {
                var PreviewTaskChanged = localStorage.getItem("PreviewTaskChanged");
                if (PreviewTaskChanged == "true") {
                    localStorage.removeItem("PreviewTaskChanged");

                    swal({
                        title: "Hay modificaciones en la tarea actual.",
                        text: "Desea guardar los cambios realizados?",
                        icon: "warning",
                        allowClickOutSide: false,
                        buttons: ["No", "Si"],
                        dangerMode: true,
                    })
                        .then((willSave) => {
                            if (willSave) {
                                console.log("Guardando Cambios");
                                var ElementPreview = document.getElementById("IFPreview");
                                $($(ElementPreview.contentDocument).find("#zamba_save")).click();
                                $scope.LayoutPreview = "row";
                            }
                        });
                } else {
                    $scope.TogglePreview('noPreview');
                }
            } else {
                $scope.TogglePreview('noPreview');
            }
        } catch (e) {
            console.error(e);
        }
    }

    $scope.OpenTaskInPreview = function (arg) {
        var thumbsCollection = $("#resultsGridSearchBoxThumbs").find(".glyphicon-ok-sign");
        $scope.thumbsCheckedCount = thumbsCollection.length;

        if (thumbsCollection.length == 0) {
            var result = $scope.Search.SearchResults[arg];
            var userid = GetUID();

            var stepid = result.STEP_ID;
            if (stepid == undefined) stepid = result.Step_Id;

            var taskId = result.TASK_ID;
            if (taskId == undefined) taskId = result.Task_Id;

            var docId = result.DOC_ID;
            if (docId == undefined) docId = result.Doc_Id;

            let userToken = JSON.parse(localStorage.getItem('authorizationData'));
            let { token } = userToken;

            const TypeRightUse = 19;

            var esTaskViewer = false;
            if (stepid != null && stepid != undefined && stepid != 0) {
                esTaskViewer = validateUserRight(stepid, TypeRightUse)
            }
            var Url = $scope.GetTaskUrl(esTaskViewer, result, taskId, stepid, userid, token);
            Url.replace("mode=s", "mode=c");

            $scope.ShowTaskInPreview(Url, docId);

            //-----------------------------------REFACTOR TO TASK SERVICE-------------------------------------
            $scope.NotifyDocumentReading(result, userid);
            //-----------------------------------REFACTOR TO TASK SERVICE-------------------------------------


        } else {
            $scope.onSelectionMode = true;
        }
    };


    $scope.NotifyDocumentReading = function (result, userid) {
        try {
            if (result.ShowUnread == true && result.ShowUnread != 'false') {
                var url = ZambaWebRestApiURL + "/search/NotifyDocumentRead?" + jQuery.param({ UserId: userid, DocTypeId: result.DOC_TYPE_ID, DocId: result.DOC_ID });
                $.post(url, function myfunction() { }).done(function () { });

                // Actualiza estado de leido en thumbs y preview
                result.ShowUnread = false;
            }
        }
        catch (e) {
            console.error(e);
        }
    }

    $scope.GetTaskUrl = function (esTaskViewer, result, taskId, stepid, userid, token) {
        if (esTaskViewer) {
            var Url = (thisDomain + "/views/WF/TaskViewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&taskid=" + taskId + "&mode=s"
                + "&s=" + stepid + "&user=" + userid + "&t=" + token);
        }
        else {
            var Url = (thisDomain + "/views/search/docviewer.aspx?DocType=" + result.DOC_TYPE_ID + "&docid=" + result.DOC_ID + "&mode=s"
                + "&user=" + userid + "&t=" + token);
        }
        return Url;
    }

    $scope.ShowTaskInPreview = function (Url, docId) {
        var OpenedTasksFlag = true;
        var previewmode = "&previewmode=true";

        $scope.currentPreviewInTab = false;
        if ($scope.OpenedTasks && $scope.OpenedTasks != undefined && $scope.OpenedTasks.length > 0) {

            $scope.OpenedTasks.forEach(function (elem, index) {
                if ($scope.OpenedTasks[index] != undefined && $scope.OpenedTasks[index] != null && $scope.OpenedTasks[index].name != "" && $scope.OpenedTasks[index].GetDOCID() == docId && $scope.OpenedTasks[index].GetUnLoaded() == false) {

                    //$scope.OpenedTasks[index].focus();

                    $scope.currentPreviewInTab = true;
                    document.getElementById('IFThisTaskIsOpen').setAttribute('src', thisDomain + "/Scripts/app/partials/ThisTaskIsOpen.html");
                    $scope.PreviewerUsedBy = { URL: Url, DocID: docId };

                    if (OpenedTasksFlag) {
                        OpenedTasksFlag = false;
                    }
                }
            });

            if (OpenedTasksFlag) {
                document.getElementById('IFPreview').setAttribute('src', Url + previewmode);
                $scope.PreviewerUsedBy = { URL: Url, DocID: docId };
            }
        }
        else {
            //Puede que el preview este activo
            document.getElementById('IFPreview').setAttribute('src', Url + previewmode);
            $scope.PreviewerUsedBy = { URL: Url, DocID: docId };
        }
    }

    function GetDOCID() {
        var docid = 0;
        docid = getUrlParameters().docid;
        if (docid > 0) return docid;
        docid = getUrlParameters().did;
        if (docid > 0) return docid;
        docid = getUrlParameters().doc_id;
        if (docid > 0) return docid;
        docid = Number($("[id$=Hiddendocid]").val());
        if (docid > 0) return docid;
        docid = currentDOCID;
        if (docid > 0) return docid;
        return 0;
    }

    function getUrlParameters() {
        try {

            console.log('window.location: ', window.location);
            console.log('window.location.search: ', window.location.search);
            var pairs = window.location.search.substring(1).split(/[&?]/);
            var res = {}, i, pair;
            for (i = 0; i < pairs.length; i++) {
                pair = pairs[i].toLowerCase().split('=');
                if (pair[1])
                    res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
            }
            return res;

        } catch (e) {
            console.error(e);
            return 0;
        }
    }

    //****************************************************OPENED TASKS------------------------------------------------
    $scope.OpenMultipleTask = function () {
        OpenSelectedRows();
    };

    $scope.MultipleSelection = function (obj) {
        try {
            if (obj == true) {
                var grid = $('#Kgrid').data('kendoGrid');
                var columns = grid.columns;
                columns[1].width = 65;
                window.localStorage.setItem("MultiSelectionIsActive", true);
                $scope.ClearSelectedChecksBox();
                CleanSelectedRows();
                var FakeColumn = { template: "&nbsp;" }
                columns.push(FakeColumn);
                grid.setOptions({ columns: columns });

                if ($scope.LayoutPreview == "row") {
                    resizeGridHeight();
                } else if ($scope.LayoutPreview == "column") {
                    resizeTabHome();
                }

                setEventsGrid(grid);
                $(".k-checkbox-label").parent().show();

            } else {

                window.localStorage.setItem("deseleccion", true);
                var grid = $("#Kgrid").data("kendoGrid");
                var myKGrid = $("#Kgrid");
                var filas = myKGrid.find(".k-checkbox");
                var columns = grid.columns;
                columns = columns.filter(x => x.template != '&nbsp;');
                grid.setOptions({ columns: columns });

                if ($scope.LayoutPreview == "row") {
                    resizeGridHeight();
                } else if ($scope.LayoutPreview == "column") {
                    resizeTabHome();
                }

                if (filas !== undefined && filas !== null && filas.length > 0) {
                    filas.splice(0, 1)[0].setAttribute("aria-checked", "false");
                    setEventsGrid(grid);
                    window.localStorage.setItem("MultiSelectionIsActive", false);
                    attachsIds = [];
                    CleanSelectedRows();

                    $scope.ClearSelectedChecksBox();
                    $(".k-checkbox-label").parent().hide();
                }
            }
            showBtns_ForResultsGrid();
        } catch (e) {
            console.error(e);
        }

    };

    //#endregion Open Task








    //Obtiene la lista de columnas de la grilla de resultados y coteja cuales tienen el atributo "hidden" en "true", "false" o incluso si esta indefinido.
    $scope.ColumnsAdvisor_ForResultsGrid = function () {
        var _hiddenColumns = [];
        var _visibleColumns = [];

        $($('#Kgrid').data("kendoGrid").columns).each(function (index, columnaGrilla) {
            if (columnaGrilla.field != undefined && columnaGrilla.field != "Icon") {
                if (columnaGrilla.hidden == true) {
                    _hiddenColumns.push(columnaGrilla.field);
                } else if (columnaGrilla.hidden == false || columnaGrilla.hidden == undefined) {
                    _visibleColumns.push(columnaGrilla.field);
                }
            }
        });

        console.log("Columnas Visibles por 'ZUserConfig':");
        console.log(_visibleColumns);
        console.log("Columnas Ocultadas por 'ZUserConfig':");
        console.log(_hiddenColumns);

        //Retornar variable con columnas deseadas
        return _hiddenColumns;
    }

    $scope.RestrictionsViews = [];
    $scope.RestrictionsViewClick = function (view) {
    };

    $scope.RestrictionsViews.push({
        Name: 'test1',
        Id: 1
    });
    $scope.RestrictionsViews.push({
        Name: 'test2',
        Id: 2
    });

    //-------------------------------------------------------------------------------------------------------



    $scope.updateCheckDisplay = function () {

        $('.button-checkbox-entities').each(function (i, e) {
            $(e).hide();
        });

        setTimeout(function () {
            $('.button-checkbox-entities').each(function () {
                var $widget = $(this),
                    $button = $widget.find('button'),
                    $checkbox = $widget.find('input:checkbox'),

                    color = $button.data('color'),
                    settings = {
                        on: {
                            icon: 'fa fa-check'
                        },
                        off: {
                            icon: 'fa fa-square-o'
                        }
                    };

                //$button.on('click', function () {
                //    $checkbox.prop('checked', !$checkbox.is(':checked'));
                //    $checkbox.triggerHandler('change');
                //    updateDisplayEntitiesEntities($checkbox);
                //});

                $checkbox.on('change', function () {
                    updateDisplayEntities($checkbox);
                });

                function updateDisplayEntities() {

                    var isChecked = $checkbox.is(':checked');

                    // Update the button's color
                    if (isChecked) {
                        $button
                            .removeClass('md-btn-basic btn-disabled')
                            .addClass('md-btn-' + color + ' active');
                    }
                    else {
                        $button
                            .removeClass('md-btn-' + color + ' active')
                            .addClass('md-btn-basic btn-disabled');
                    }

                    // Set the button's state
                    $button.data('state', (isChecked) ? "on" : "off");

                    // Set the button's icon
                    $button.find('.state-icon')
                        .removeClass()
                        .addClass('state-icon ' + settings[$button.data('state')].icon);
                }
                function init() {
                    // Inject the icon if applicable
                    updateDisplayEntities();
                    if ($button.find('.state-icon').length == 0) {
                        $button.prepend('<i class="state-icon ' + settings[$button.data('state')].icon + '"></i> ');
                    }

                }
                init();
            });

            if (ResizeResultsArea)
                ResizeResultsArea();

        }, 500);
    };




    function updateDisplay(checkbox) {
        try {
            $parent = $(checkbox).parent();
            $button = $parent.find('button');
            $checkbox = checkbox;
            color = $button.data('color');
            settings = {
                on: {
                    icon: 'fa fa-check'
                },
                off: {
                    icon: 'fa fa-square-o'
                }
            };

            var isChecked = checkbox.is(':checked');

            // Update the button's color
            if (isChecked) {
                $button
                    .removeClass('md-btn-basic')
                    .addClass('md-btn-' + color + ' active');
            }
            else {
                $button
                    .removeClass('md-btn-' + color + ' active')
                    .addClass('md-btn-basic');
            }

            // Set the button's state
            $button.data('state', (isChecked) ? "on" : "off");

            // Set the button's icon
            $button.find('.state-icon')
                .removeClass()
                .addClass('state-icon ' + settings[$button.data('state')].icon);

        } catch (e) {
            console.error(e);
        }

    }



    function TypeaheadCtrl($scope, $http) {
        $scope.selected = undefined;
        $scope.Customer =
            [
                { CustomerID: 1, CustomerCode: 'C001', CustomerName: 'John Papa', City: 'RedMond' },

                { CustomerID: 5, CustomerCode: ' C008', CustomerName: 'Bill Gates', City: 'Bangalore' },
                { CustomerID: 6, CustomerCode: ' C009', CustomerName: 'Satya Nadella', City: 'Hyderabad' }
            ];
    }
    $scope.formatInput = function ($model) {
        var inputLabel = '';
        angular.forEach($scope.Customer, function (Customer) {
            if ($model === Customer.id) {
                inputLabel = Customer.CustomerID + "-" + Customer.CustomerName;
            }
        });
        return inputLabel;
    }

    $scope.onSelect = function ($item, $model, $label) {
        $scope.$item = $item;
        $scope.$model = $model;
        $scope.$label = $label;
        // Implement other logics
    };
    $scope.ClearSelectedChecksBox = function () {
        CleanSelectedRows();
        attachsIds = [];
    };




    var getCheckedDocumentId = function () {
        var docIds = [];
        $("#Kgrid").find("input:checked").each(function () {
            var index = $(this).closest("tr").index();
            docIds.push(($("#Kgrid").data("kendoGrid").dataSource._data[index].DOC_ID).toString());
        });
        return removeDuplicatesElementFromList(docIds);
    },
        removeDuplicatesElementFromList = function (list) {
            var result = [];
            $.each(list, function (index, element) {
                if ($.inArray(element, result) == -1) result.push(element);
            });
            return result;
        }


    var executeRegularExpression = function (regularExpression, wordToValidate) {
        return regularExpression.exec(wordToValidate);
    }

    var mailFormat = function (mailColection) {
        //Validate: ejemplo@unEjemplo.com
        var regEx1 = /([a-zA-Z0-9\_])+\@([a-zA-Z0-9]{3,})+\.([\w])+/;
        //Validate: ejemplo@unEjemplo.com.ar
        var regEx2 = /([a-zA-Z0-9\_])+\@([a-zA-Z0-9]{3,})+\.([\w])+\.([a-zA-Z0-9]{2,})/;
        var isCorrectMailFormat = true;

        for (var mail in mailColection) {
            var regexExecuted1 = executeRegularExpression(regEx1, mailColection[mail]),
                regexExecuted2 = executeRegularExpression(regEx2, mailColection[mail]);
            if (regexExecuted1 == null) {
                if (regexExecuted2 == null) {
                    isCorrectMailFormat = false;
                }
            }
        }
        return isCorrectMailFormat;
    }


    var mailFormatFromAmail = function (mail) {
        //Validate: ejemplo@unEjemplo.com
        var regEx1 = /([a-zA-Z0-9\_])+\@([a-zA-Z0-9]{3,})+\.([\w])+/;
        //Validate: ejemplo@unEjemplo.com.ar
        var regEx2 = /([a-zA-Z0-9\_])+\@([a-zA-Z0-9]{3,})+\.([\w])+\.([a-zA-Z0-9]{2,})/;
        var isCorrectMailFormat = true;
        var regexExecuted1 = executeRegularExpression(regEx1, mail),
            regexExecuted2 = executeRegularExpression(regEx2, mail);
        if (regexExecuted1 == null) {
            if (regexExecuted2 == null) {
                isCorrectMailFormat = false;
            }
        }
        return isCorrectMailFormat;
    }

    $scope.ValidateDeriveRightByUserAndStep = function () {
        var steps = [];
        let checkedIds = countTaskIdSelected();
        for (var i = 0; i < checkedIds.length; i++) {
            steps.push(checkedIds[i].stepId)
        }
        var distinct = function (value, index, self) {
            return self.indexOf(value) === index;
        }
        var uniqueSteps = steps.filter(distinct);
        var hasPermission = false;
        for (var i = 0; i < uniqueSteps.length; i++) {
            var hasPermission = validateUserRight(uniqueSteps[i], 29);
            if (!hasPermission)
                break;
        }
        return hasPermission;
    }

    $scope.ShowDeriveModal = function () {
        var hasPermission = $scope.ValidateDeriveRightByUserAndStep();
        if (hasPermission) {
            $("#ModalDerivar").modal();
        }
        else {
            toastr.error("No tiene permiso para derivar alguna de las tareas seleccionadas.");
        }
    }

    $scope.TogglePreview = function (value) {
        try {
            switch (value) {
                case "previewV":
                    $scope.LayoutPreview = "row";

                    if ($scope.PreviewMode == value) {
                        $scope.PreviewMode = "noPreview";
                        $scope.LayoutPreview = "row";
                    } else {
                        if ($scope.Search.SearchResults.length > 0 && $scope.Search.LastPage == 0)
                            if (document.querySelector(".k-state-selected") == null) {
                                $scope.OpenTaskInPreview(0);
                            } else {
                                var taskSelected = document.querySelector(".k-state-selected");
                                $scope.OpenTaskInPreview(taskSelected.rowIndex);
                            }
                        $scope.PreviewMode = value;
                    }

                    if ($scope.LayoutPreview == "row") {
                        resizeGridHeight();
                    } else if ($scope.LayoutPreview == "column") {
                        resizeTabHome();
                    }
                    break;

                case "previewH":
                    $scope.LayoutPreview = "column";

                    if ($scope.PreviewMode == value) {
                        $scope.PreviewMode = "noPreview";
                        $scope.LayoutPreview = "row";
                    } else {
                        if ($scope.Search.SearchResults.length > 0 && $scope.Search.LastPage == 0)
                            if (document.querySelector(".k-state-selected") == null) {
                                $scope.OpenTaskInPreview(0);
                            } else {
                                var taskSelected = document.querySelector(".k-state-selected");
                                $scope.OpenTaskInPreview(taskSelected.rowIndex);
                            }
                        $scope.PreviewMode = value;
                    }

                    if ($scope.LayoutPreview == "row") {
                        resizeGridHeight();
                    } else if ($scope.LayoutPreview == "column") {
                        resizeTabHome();
                    }
                    break;

                case "noPreview":
                    document.getElementById('IFPreview').setAttribute('src', "about:blank");
                    resizeGridHeight();
                    $scope.PreviewMode = value;
                    $scope.LayoutPreview = "row";
                    break;
                default:
            }
        } catch (e) {
            console.log(e);
            $scope.PreviewMode = "noPreview";
            $scope.LayoutPreview = "row";

        }
    }

    $(window).on("resize", function () {
        if ($scope.LayoutPreview == "row") {
            resizeGridHeight();
        } else if ($scope.LayoutPreview == "column") {
            resizeTabHome();
        }
    })

    function validateUserRight(StepId, Right) {
        var permission = false;
        var UserId = GetUID();
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Tasks/GetUsersWFStepsRights?' + jQuery.param({ stepId: StepId, right: Right, userid: UserId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (response) {
                permission = response;
            },
            error: function (response) {
            }
        });
        return permission;
    }


    $scope.DeriveTasks = function () {
        var userid = window.localStorage.getItem('TaskFilterConfig-' + GetUID());
        var userid = JSON.parse(userid);
        var userid = userid.UserId;
        var userId = $(".selectedUser").attr("data-userId");
        var docIds = GetDocIdFromList(attachsIds);
        var docIds = JSON.stringify(docIds);
        var _isUser = false;
        if ($(".selectedUser").parent().hasClass("Users")) {
            _isUser = true;
        }

        var Url = window.location.href;
        Comments = $("#ModalDerivar").find("#deriveMessage").val();

        $("#ModalDerivar").find(".derive").attr("disabled", true);
        $("#ModalDerivar").find(".closeModal").attr("disabled", true);
        $(".loadersmall").css("display", "block");



        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Tasks/DeriveTasks?' + jQuery.param({ docIds: docIds, userIDToAsign: userId, currentUserID: userid, isUser: _isUser, url: Url, comments: Comments }),
            contentType: "application/json; charset=utf-8;",
            async: false,
            success: function (data) {
                console.log(data);
                $("#liDerivar").css("display", "none");
                //location.reload();
                $('#ModalDerivar').modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                swal("", "La tarea se derivo correctamente", "success");
                $("#liDerivar").css("display", "none");
                $("#ModalDerivar").find(".derive").removeAttr("disabled");
                $("#ModalDerivar").find(".closeModal").removeAttr("disabled");
                $(".loadersmall").css("display", "none");
                $(".selectedUser").removeClass("selectedUser");
                $('#lblDerivar')[0].textContent = "Derivar";
                $("#SearchUsers")[0].value = "";
                $("#SearchGoups")[0].value = "";
                $("#deriveMessage")[0].value = "";
                RefreshResultsGridFromChildWindow();
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                console.log(err.Message);
                swal("", err.Message, "error");
                $("#ModalDerivar").find(".derive").removeAttr("disabled");
                $("#ModalDerivar").find(".closeModal").removeAttr("disabled");
                $(".loadersmall").css("display", "none");
                $(".selectedUser").removeClass("selectedUser");
                $('#lblDerivar')[0].textContent = "Derivar";
                $("#SearchUsers")[0].value = "";
                $("#SearchGoups")[0].value = "";
                $("#deriveMessage")[0].value = "";
            }
        });

    }


    attachsIds = [];
    $scope.GetTaskDocument = function (arg) {
        for (i = 0; i < arg.length; i++) {
            var result = $scope.Search.SearchResults[arg[i]];
            if (result != undefined) {
                if (checkValue(result.DOC_ID, attachsIds, "attach") != true) {
                    var IdInfo = {};
                    IdInfo.stepId = parseInt(result.STEP_ID);
                    IdInfo.Docid = parseInt(result.DOC_ID);
                    IdInfo.DocTypeid = parseInt(result.DOC_TYPE_ID);
                    attachsIds.push(IdInfo);
                }
            }
        }
    }


    $scope.RemoveAttach = function (arg) {
        var IdToRemove = $scope.Search.SearchResults[arg];

        if (IdToRemove != undefined) {
            for (i = 0; i < attachsIds.length; i++) {
                if (attachsIds[i].Docid == IdToRemove.DOC_ID) {
                    attachsIds.splice(i, 1);
                }
            }
        }
    }

    $scope.optionClick = function () {
        var display = $("#multipleSelectionMenu").find(".glyphicon-envelope").css("display");
        console.log(display);
        if (display == "none") {
            $("#multipleSelectionMenu").find(".glyphicon-envelope").fadeIn();
        } else {
            $("#multipleSelectionMenu").find(".glyphicon-envelope").fadeOut();
        }
    }

    $scope.SendEmailFromThumbsGrid = function () {
        $scope.GetTaskDocument($scope.thumbSelectedIndexs);
    }

    $scope.openModalSendMail = function () {
        $("#ModalSendZip").modal("show");
        $("#file_upload").css("display", "none");
    }

    var ValMessage;
    $scope.SendEmail = function (obj) {
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
        var MailValidation = true;

        ValMessage = "";

        //TODO: Hacer lo mismo para Envio de ZIP y DocToolbar
        var formMailTo = angular.element($("#formMailDestinatario")).scope();
        var formMailCc = angular.element($("#formMailCc")).scope();
        var formMailCco = angular.element($("#formMailCco")).scope();

        var MailTo = formMailTo.Value.replaceAll(';', ',');
        var MailCc = formMailCc.Value.replaceAll(';', ',');
        var MailCco = formMailCco.Value.replaceAll(';', ',');

        MailValidation = ValEmails(MailTo, reg, MailValidation, formMailTo.attribute);
        MailValidation = ValEmails(MailCc, reg, MailValidation, formMailCc.attribute);
        MailValidation = ValEmails(MailCco, reg, MailValidation, formMailCco.attribute);

        if (MailValidation == false) {
            swal("", "Error: Corrija las advertencias. \n\n" + ValMessage, "error");
        } else {
            $(".loadersmall").css("display", "inline-block");
            $(".loadersmall").css("position", "static");
            $("#btnMailZipSubmit").hide();
            $("#btnMailZipMailClose").hide();
            var emaildata = {};

            if (attachsIds.length == 0)
                $scope.GetTaskDocument($scope.thumbSelectedIndexs);

            var arrayEmailData = [];

            //Array de Docids y DocTypesIds para guardar historial en cada uno de las tareas
            if (DocIdschecked.length > 0 && DocTypesIdschecked.length > 0) {
                DocIdschecked.forEach(function (elem, index) {
                    var IdInfo = {};
                    IdInfo.Docid = DocIdschecked[index];
                    IdInfo.DocTypeId = DocTypesIdschecked[index];

                    arrayEmailData.push(IdInfo);
                })
            } else if (attachsIds.length > 0) {

                attachsIds.forEach(function (elem, index) {
                    var IdInfo = {};
                    IdInfo.DocTypeId = attachsIds[index].DocTypeid;
                    IdInfo.Docid = attachsIds[index].Docid;
                    IdInfo.stepId = attachsIds[index].stepId;


                    arrayEmailData.push(IdInfo);
                })
            }

            emaildata.Idinfo = arrayEmailData;

            emaildata.MailTo = MailTo;
            emaildata.CC = MailCc;
            emaildata.CCO = MailCco;
            emaildata.Subject = $('input[name="subject"]').val() == undefined ? "" : $('input[name="subject"]').val();
            emaildata.MessageBody = document.getElementById("cke_1_contents").children[0].contentDocument.children[0].childNodes[1].innerHTML
            emaildata.Base64StringArray = CollectionFiles;

            emaildata.UserId = GetUID();

            var hasFile = $scope.getIFAnyTaskHasFile(emaildata);
            var addLinks = $('input[name="addListLinks"]').prop("checked");
            emaildata.AddLink = addLinks;
            var addLinks = $("#addListLinks").val()

            //if (hasFile || addLinks) {
            $http({
                method: 'POST',
                dataType: 'json',
                url: location.origin.trim() + getValueFromWebConfig("RestApiUrl") + '/Email/SendEmailForSearch',
                data: emaildata,
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
            }).then(function (data, status, headers, config) {
                ModalView(data);

                CollectionFiles = [];
                emaildata.MessageBody = "";
                document.getElementById("cke_1_contents").children[0].contentDocument.children[0].childNodes[1].innerHTML = "";
                emaildata.Subject = "";
                $('#file_upload').val("");

            }).catch(function (error) {
                ModalView_Error(error);
            });
        }
    }

    function ValEmails(List_Destinatarios, reg, Validado, field) {
        if (List_Destinatarios != undefined) {
            if (List_Destinatarios != "") {
                List_Destinatarios.split(",").forEach(function (elem, index) {
                    if (Validado) {
                        if (elem.trim() != "") {
                            if (reg.test(elem.trim()) == false) {
                                console.log("Iteracion erronea: " + elem.trim());

                                if (field.search("_Zip" != 1)) {
                                    ValMessage += "• " + field + ": Hay caracteres o correo no valido.\n";
                                } else {
                                    ValMessage += "• " + field.substr(0, field.lastIndexOf("_Zip")) + ": Hay caracteres o correo no valido.\n";
                                }

                                Validado = false;
                            }
                        } else {

                            if (field.search("_Zip" != 1)) {
                                ValMessage += "• " + field + ": Ha escrito doble punto y coma o un punto y coma al final.\n";
                            } else {
                                ValMessage += "• " + field.substr(0, field.lastIndexOf("_Zip")) + ": Ha escrito doble punto y coma o un punto y coma al final.\n";
                            }

                            Validado = false;
                        }
                    }
                });
            }
        }
        return Validado;
    }



    //Oculta el modal para enviar un correo asi como tambien resetea las tareas seleccionadas.
    function ModalView(data) {
        console.log(data);
        if (data == false) {
            swal("", "Error al enviar Email", "error");
            $(".loadersmall").css("display", "none")
            $("#btnMailZipMailClose").show();
            $("#btnMailZipSubmit").show();
            $("#ModalSendZip").modal('toggle');
        } else {
            swal("", "Email enviado con exito", "success");
            $(".loadersmall").css("display", "none");
            $("#btnMailZipMailClose").show();
            $("#btnMailZipSubmit").show();
            $("#ModalSendZip").modal('toggle');
            $(".EmailInput").val("");

            //No cambiar FALSE a TRUE
            ResetSwitch();

            $scope.checkStatus = false;
            $scope.MultipleSelection(false);

            CleanSelectedRows();
            attachsIds = [];
            $scope.inputs = null;
        }
    }

    //Muestra un mensaje de error y deja el modal abierto para reparametrizar.
    function ModalView_Error(error) {
        console.log(error);
        NotifyError(error)

        $(".loadersmall").css("display", "none");
        $("#btnMailZipMailClose").show();
        $("#btnMailZipSubmit").show();
    }

    //Muestra un mensaje de error y deja el modal abierto para reparametrizar.
    function ModalView_NoFile() {
        //TO DO: Eliminar Metodo

        //toastr.warning("Ninguna de las tareas posee archivo asociado.", "No se ha enviado el mail");
        //$(".loadersmall").css("display", "none");
        //$("#btnMailZipMailClose").show();
        //$("#btnMailZipSubmit").show();
    }


    function getListOfLinks(docIds) {
        var allPathList = [];
        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + "/Email/getListOfLinks",
            data: JSON.stringify(docIds),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    allPathList = data;
                }
        });
        return allPathList;
    }

    function addAllCompletepath(pathList) {

        for (var path in pathList) {
            pathList[path] = thisDomain + pathList[path] + "<br>";
        }
    }

    $scope.sendIsLoading = false;

    $scope.DocInfo = function () {

        let checkedIds = countTaskIdSelected();
        if (checkedIds.length > 1) {
            swal("", "Seleccione solo una tarea", "info");
        } else {

            $('#informationModal').modal({ show: true });

            $scope.ViewInfo = $scope.Search;

            let ViewInfoResult = $scope.ViewInfo.SearchResults.filter(x => { return x.DOC_ID == checkedIds[0]['Docid'] });

            if (ViewInfoResult != "" && ViewInfoResult != null && ViewInfoResult != undefined) {

                $scope.DocInfoDocId = ViewInfoResult[0]?.DOC_ID != null ? ViewInfoResult[0].DOC_ID : "";
                $scope.DocInfoDocTypeID = ViewInfoResult[0]?.DOC_TYPE_ID != null ? ViewInfoResult[0].DOC_TYPE_ID : "";
                $scope.DocInfoEntidad = ViewInfoResult[0]?.ENTIDAD != null ? ViewInfoResult[0].ENTIDAD : "";
                $scope.DocInfoTarea = ViewInfoResult[0]?.Tarea != null ? ViewInfoResult[0].Tarea : "";
                $scope.DocInfoCreado = ViewInfoResult[0]?.CREADO != null ? moment(ViewInfoResult[0].CREADO).format("DD/MM/YYYY") : "";
            }

        }


    }

    $scope.DownLoadZip = function () {
        toastr.info("Comprimiendo archivos... aguarde por favor.");
        $scope.BtnZipDisable = true;
        var zip = {};
        let checkedIds = countTaskIdSelected();


        zip.Idinfo = checkedIds;
        //var anyTaskHasFile = $scope.getIFAnyTaskHasFile(zip);
        //anyTaskHasFile = true;
        $http({
            method: 'POST',
            dataType: 'json',
            url: location.origin.trim() + getValueFromWebConfig("RestApiUrl") + '/Email/DownloadZip',
            data: zip,
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
        }).then(function (data, status, headers, config) {
            console.log(JSON.parse(data.data));
            var jsonObjectData = JSON.parse(data.data);
            console.log("missingAttachment = " + jsonObjectData.missingAttachment);
            if (jsonObjectData.missingAttachment == true) {
                toastr.warning("Descarga finalizada. Algunas de las tareas seleccionadas no poseen archivos asociados");
                $scope.BtnZipDisable = false;
            }
            else {
                toastr.success("Descarga finalizada");
                $scope.BtnZipDisable = false;
            }

            var a = document.createElement("a"); //Create <a>
            a.href = "data:image/png;base64," + jsonObjectData.data; //Image Base64 Goes here
            a.download = jsonObjectData.fileName; //File name Here
            a.click();
        }).catch(function (error) {
            swal("", "Ninguna de las tareas posee un archivo asociado.", "warning");
            $scope.BtnZipDisable = false;
        });
    }
    $scope.SendZip = function (obj) {
        var MailValidation = true;
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

        ValMessage = "";

        //TODO: Hacer lo mismo para Envio de ZIP y DocToolbar
        var formMailTo = angular.element($("#formMailZipMailTo")).scope();
        var formMailCc = angular.element($("#formMailZipCc")).scope();
        var formMailCco = angular.element($("#formMailZipCco")).scope();

        var MailTo = formMailTo.Value.replaceAll(';', ',');
        var MailCc = formMailCc.Value.replaceAll(';', ',');
        var MailCco = formMailCco.Value.replaceAll(';', ',');

        MailValidation = ValEmails(MailTo, reg, MailValidation, formMailTo.attribute);
        MailValidation = ValEmails(MailCc, reg, MailValidation, formMailCc.attribute);
        MailValidation = ValEmails(MailCco, reg, MailValidation, formMailCco.attribute);


        if (MailValidation == false) {
            swal("", "Error: Corrija las advertencias.\n\n" + ValMessage, "error");
        } else {
            $("#btnZipMailSubmit").prop('disabled', true);
            $("#btnZipMailClose").prop('disabled', true);
            $scope.sendIsLoading = true;
            var zip = {};

            var arrayEmailData = [];

            //Array de Docids y DocTypesIds para guardar historial en cada uno de las tareas
            if (DocIdschecked.length > 0 && DocTypesIdschecked.length > 0) {
                DocIdschecked.forEach(function (elem, index) {
                    var IdInfo = {};
                    IdInfo.Docid = DocIdschecked[index];
                    IdInfo.DocTypeId = DocTypesIdschecked[index];

                    arrayEmailData.push(IdInfo);
                })
            }

            zip.Idinfo = arrayEmailData;

            zip.UserId = GetUID();
            zip.MailTo = MailTo;
            zip.CC = MailCc;
            zip.CCO = MailCco;
            zip.Subject = obj.subject || "Te han enviado archivo/s.";
            zip.ZipName = obj.zipName || "Archivo";
            zip.ZipPassword = obj.zipPasswd || "";
            zip.MessageBody = obj.MessageBody || "<br >";
            zip.MessageBody = document.getElementById("cke_2_contents").children[0].contentDocument.children[0].childNodes[1].innerHTML || "<br >";

            var anyTaskHasFile = $scope.getIFAnyTaskHasFile(zip);

            if (anyTaskHasFile) {
                $http({
                    method: 'POST',
                    dataType: 'json',
                    url: location.origin.trim() + getValueFromWebConfig("RestApiUrl") + '/Email/SendZipMail',
                    data: zip,
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                }).then(function (data, status, headers, config) {
                    ModalSendZipView(data);

                }).catch(function (error) {
                    ModalSendZipView_Error(error);
                });
            } else {
                ModalSendZipView_NoFile();
            }
        }

    }

    //Oculta el modal para enviar archivos zips como un correo asi como tambien resetea las tareas seleccionadas.
    function ModalSendZipView(data) {
        $scope.sendIsLoading = false;
        $("#btnZipMailSubmit").show();
        $("#btnZipMailClose").show();
        $("#btnZipMailSubmit").prop('disabled', false);
        $("#btnZipMailClose").prop('disabled', false);
        $("#btn btn-default cancelMailZipButton").prop('disabled', false);
        if (data.data == false) {
            swal("", "Error al enviar email", "error");

            $("#btnZipMailSubmit").show();
            $("#btnZipMailClose").show();
            $("#btnZipMailClose").removeAttr("disabled");
            $("#btnZipMailSubmit").removeAttr("disabled");
            $(".zipinput").val("");
            $(".loadersmall").css("display", "none");
        } else {
            swal("", "Email enviado con exito", "success");
            document.getElementById("cke_2_contents").children[0].contentDocument.children[0].childNodes[1].innerHTML = "";
            $scope.zipInputs = null;
            $(".loadersmall").css("display", "none");
            $("#btnMailZipSubmit").show();
            $("#btnZipMailClose").show();
            $(".zipinput").val("");
            $("#ModalSendMailZip").modal('toggle');
        }
    }

    //Muestra un mensaje de error y deja el modal abierto para reparametrizar.
    function ModalSendZipView_Error(error) {
        $scope.sendIsLoading = false;
        $(".loadersmall").css("display", "none");
        $("#btnZipMailSubmit").show();
        $("#btnZipMailClose").show();
        $("#btnZipMailClose").removeAttr("disabled");
        $("#btnZipMailSubmit").removeAttr("disabled");

        NotifyError(error);

        $("#btnMailZipSubmit").show();
    }

    //Valida y notifica la existencia de un error.
    function NotifyError(error) {
        if (error.data != undefined) {
            if (error.data.InnerException != undefined) {
                swal("", error.data.ExceptionMessage + ": " + error.data.InnerException.ExceptionMessage, "error");
            } else {
                swal("", error.data.ExceptionMessage, "error");
            }
        }
        else if (error.responseJSON != undefined) {
            if (error.responseJSON.InnerException != undefined) {
                swal("", error.responseJSON.ExceptionMessage + ": " + error.responseJSON.InnerException.ExceptionMessage, "error");
            } else {
                swal("", error.responseJSON.ExceptionMessage, "error");
            }
        }
        else {
            swal("", "Error no capturado al enviar mensaje.", "error");
        }
    }

    //Muestra un mensaje de error y deja el modal abierto para reparametrizar.
    function ModalSendZipView_NoFile() {
        $scope.sendIsLoading = false;
        $(".loadersmall").css("display", "none");
        $("#btnMailZipSubmit").show();
        $("#btnZipMailClose").show();
        $("#btnMailZipSubmit").show();
        $("#btnZipMailClose").removeAttr("disabled");
        $("#btnZipMailSubmit").removeAttr("disabled");

        swal("", "Ninguna de las tareas posee un archivo asociado.", "warning");
    }

    $scope.getIFAnyTaskHasFile = function (mailData) {
        var hasFile = false;
        $.ajax({
            "async": false,
            "url": location.origin.trim() + getValueFromWebConfig("RestApiUrl") + "/Email/getIFAnyTaskHasFile",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "data": JSON.stringify(mailData),
            "success":
                function (data, status, headers, config) {
                    hasFile = data;
                },
            "error":
                function (data, status, headers, config) {
                    console.log(data);
                }
        });
        return hasFile;
    }

    //elimina todos los valores de los inputs
    $scope.CleanAllInputs = function () {

        try {
            if ($scope.Search.Indexs != undefined && $scope.Search.Indexs != null) {
                for (var i = 0; i <= $scope.Search.Indexs.length - 1; i++) {
                    $scope.Search.Indexs[i].Data = "";
                    $scope.Search.Indexs[i].dataDescription = "";
                    $scope.Search.Indexs[i].Data2 = "";
                    $scope.Search.Indexs[i].dataDescription2 = "";
                    $('.activeIn').css({
                        'border': '',
                    });
                    $(".activeIn2").css('border-right', "none");
                }
                $scope.$broadcast('resetFiltersDefaultZambaColumnFilters');
            }
        } catch (e) {
            console.error(e);
        }

    }

    $scope.selectedIndex = null;
    $scope.ListItems = [];

    function constructMap(data, map) {
        var objects = [];
        $.each(data, function (i, object) {
            map[object.Value] = object;
            objects.push(object.Value);
        });
        return objects;
    }

    $scope.selectMatch = function (index) {
        $scope.selectedIndex.Data = $scope.ListItems[index].Code;
        $scope.selectedIndex.dataDescription = $scope.ListItems[index].Value;
    };

    $scope.getList = function (index, val) {
        $scope.selectedIndex = index;

        return $http.post(ZambaWebRestApiURL + '/search/ListOptions', JSON.stringify({
            IndexId: $scope.selectedIndex.ID,
            Value: val,
            LimitTo: 10
        })).then(function (response) {
            var results = JSON.parse(response.data);
            map = {};
            $scope.ListItems = constructMap(results, map);
            return results;
        });
    };

    $scope.dateFields = [];

    $scope.generateGrid = function (gridData) {

        var model = $scope.generateModel(gridData[0]);

        var parseFunction;
        parseFunction = function (response) {
            for (var i = 0; i < response.length; i++) {
                for (var fieldIndex = 0; fieldIndex < $scope.dateFields.length; fieldIndex++) {
                    var record = response[i];
                    record[$scope.dateFields[fieldIndex]] = kendo.parseDate(record[$scope.dateFields[fieldIndex]]);
                }
            }
            return response;
        };
    }

    $scope.generateModel = function (gridData) {
        var model = {};
        model.id = "ID";
        var fields = {};
        for (var property in gridData) {
            var propType = typeof gridData[property];

            if (propType == "number") {
                fields[property] = {
                    type: "number",
                    validation: {
                        required: true
                    }
                };
            } else if (propType == "boolean") {
                fields[property] = {
                    type: "boolean",
                    validation: {
                        required: true
                    }
                };
            } else if (propType == "string") {
                var parsedDate = kendo.parseDate(gridData[property]);
                if (parsedDate) {
                    fields[property] = {
                        type: "date",
                        validation: {
                            required: true
                        }
                    };
                    $scope.dateFields.push(property);
                } else {
                    fields[property] = {
                        validation: {
                            required: true
                        }
                    };
                }
            } else {
                fields[property] = {
                    validation: {
                        required: true
                    }
                };
            }
        }
        model.fields = fields;
        return model;
    }




    $scope.ProcessSearch = function (results) {

        var data = results.data;
        // Si hay datos
        if (data != null && data != undefined && data.length) {

            // Si es la primer pagina
            if ($scope.page === 0) {
                $scope.Search.SearchResults = data;
            }
            else {
                //Bug Zamba cuando busca paginado y no hay mas registros trae el ultimo                                            
                var ld = data[data.length - 1];
                var l = $scope.Search.SearchResults === undefined ? null : $scope.Search.SearchResults.length;
                if ($scope.Search.SearchResults != null && l &&
                    ld.DOC_ID === $scope.Search.SearchResults[l - 1].DOC_ID &&
                    ld.DOC_TYPE_ID === $scope.Search.SearchResults[l - 1].DOC_TYPE_ID &&
                    ld.Task_Id === $scope.Search.SearchResults[l - 1].Task_Id) {
                    NoData();
                    return;
                }

                function NoData() {
                    hideLoading();

                    $scope.isLastPage = true;
                    toastr.info("No se encontraron resultados", "Zamba");
                }
                if ($scope.Search.SearchResults == undefined) {
                    $scope.Search.SearchResults = data;
                }
                else {
                    for (var i = 0; i < data.length; i++) {
                        $scope.Search.SearchResults.push(data[i]);
                    }
                }
            }


            if ($scope.Search.SearchResults == null || !$scope.Search.SearchResults.length) {
                noResultsMsg();
                return;
            }

            ProcessResults($scope.Search)

            GoToUpGlobalSearch();

            hideLoading();

            //Para llamar a QuickSearch informando los resultados obtenidos
            if (typeof (OpenResultsQuickSearch) == "function") {
                SQqtity = data.length;
                OpenResultsQuickSearch();
            }
        }
        else {
            $scope.Search.SearchResults = null;
            hideLoading();
            noResultsMsg();
        }

        // Usa esto para generar el esquema de la grilla, usa el response y vuelve a hacerle el mismo laburo a algunas columnas
        for (var a = 0; a <= results.data.length - 1; a++) {
            if (results.data[a].AsignedTo == " ") {
                results.data[a].AsignedTo = "(Sin Asignar)";
            }
            results.data[a].Icon = GetImgIcon(results.data[a].ICON_ID);
            results.data[a].FULLPATH = '';
        }

        if ($scope.Search.LastPage === undefined || $scope.Search.LastPage === 0) {
            KendoGrid(results, $scope.Search.UserId, $scope.Search);
        }
        else {
            RefreshKGrid(results, $scope.Search);
        }



        $("#Kgrid").find('th').eq(0).children().hide();


        $scope.FillFilters($scope.Search.SearchResultsObject);

    };

    $scope.enterEditMode = function (index) {
        if (index === undefined)
            return;

        var searchParam = $scope.model.parameters[index];
        if (searchParam.type == 2) {
            searchParam.editMode = true;
        }
    };

    $scope.leaveEditMode = function (index) {
        if (index === undefined)
            return;

        var searchParam = $scope.model.parameters[index];
        if (searchParam.editMode == false)
            return;

        searchParam.editMode = false;

        if (!searchParam.value && searchParam.parent)
            $scope.removeSearchParam(index);
        //Al cambiar el valor del input que actualice la grilla de datos
        $scope.doSearchGS();
    };

    $scope.currentPreviewIndex = -2;
    $scope.previewItem = function (result, index, event) {
        $scope.currentPreviewIndex = index;

        //Workaround JQuery desde Search.html no cambia valor NGSelectedRow en vista si desde NG
        var $items = $("#resultsGridSearchBoxPreview>.previewListItems");
        var $active = $items.children(".resultsGridActive").index();
        $items.children().removeClass("resultsGridActive");
        if (event != undefined) {
            var item = this.Search.SearchResults[event == undefined ? index - 1 : index];

            if (this.Search.SearchResults != undefined) {
                this.Search.SearchResults = this.Search.SearchResults.map(function (itemResult) {
                    itemResult.NGSelectedRow = false;
                    return itemResult;
                });
            }
            item.NGSelectedRow = true;
            if (event.type == "click" && event.target.tagName == "IMG") {
                $(event.target).parents(".resultsGrid").addClass("resultsGridActive");
            }
        }
        else {
            if (index === -1) {
                $active = -1;
            }
            var scrollPosition = $(".previewListItems").scrollTop() + ($active > -1 ? 223 : 0);
            setTimeout(function () {
                $($items.children()[$active + 1]).addClass("resultsGridActive");
                $(".previewListItems").animate({
                    scrollTop: scrollPosition
                }, 300);
            }, 100);

        }

        var currentresult = result;
        var url = "../../Services/GetDocFile.ashx?DocTypeId=" + currentresult.DOC_TYPE_ID + "&DocId=" + currentresult.DOC_ID + "&UserID=" + GetUID() + "&ConvertToPDf=true";

        if (currentresult.Original != null) {
            if ((currentresult.Original.toString().indexOf(".msg") != -1) || currentresult.ICON_ID == 6) {
                $("#previewDocSearchPanel").hide();
                $("#DocumentViewerFromSearch").show();
                var scope = angular.element($("#DocumentViewerFromSearch")).scope();
                scope.ShowDocument(url);
            }
            else {
                $("#previewDocSearchPanel").hide();
                $("#DocumentViewerFromSearch").show();
                var scope = angular.element($("#DocumentViewerFromSearch")).scope();
                scope.ShowDocument(url, currentresult.ICON_ID);
            }
        }
        else {
            try {
                $("#previewDocSearchPanel").show();
                $("#DocumentViewerFromSearch").hide();
                if ($("#previewDocSearch")[0] != undefined) {
                    $("#previewDocSearch")[0].contentWindow.OpenUrl(url, index);
                }
            }
            catch (error) {
            }
        }
        setTimeout(
            function () {

                var docIframe = document.getElementById("previewDocIframe");
                var doc = docIframe.contentWindow.document;
                var btnImprimir = doc.getElementById("print")
                btnImprimir.style.display = 'none';
            }, 300);


        return url;
    }

    $scope.previewItem_ForDocumentViewer = function (result, index, event) {
        angular.element(document.getElementById("DocumentViewerFromSearch")).scope().ShowDocument_FromItem(GetUID(), result.DOC_TYPE_ID, result.DOC_ID);
    }

    $scope.GetNextUrl = function (index) {
        if ($scope.Search == undefined || $scope.Search.SearchResults == undefined) return;
        index++;
        var currentresult = $scope.Search.SearchResults[index];
        if (currentresult != undefined)
            $scope.previewItem(currentresult, index)
    };
    $rootScope.$on('GetNextUrl', function (type, data) {
        $scope.GetNextUrl(data);
    });

    $scope.DoSearchByQS = function (d) {
        $scope.model.parameters = d;
        $scope.doSearchGS();
    };
    $rootScope.$on('DoSearchByQS', function (type, data) {
        $scope.DoSearchByQS(data);
    });

    $scope.GetTokenInfo = function () {
        return RestApiToken().getTokenInfo();
    }

    $scope.ShowResult = function (result, index, e) {
        // var currentresult = result; 
        var currentresult = typeof (result) == "number" ? $scope.Search.SearchResults[result] : result;
        $scope.Result = currentresult;
        $scope.Result.UserId = GetUID();
        var taskId = $scope.Result.Task_Id;

        var docId = $scope.Result.DOC_ID;
        var docTypeId = $scope.Result.DOC_TYPE_ID;
        var stepId = $scope.Result.STEP_ID;
        if (stepId == undefined) {
            stepId = $scope.Result.Step_Id;
        }
        if (taskId || stepId == null) {
            taskId = 0;
            stepId = 0;
        }

        $scope.Result.Url = "/views/WF/TaskSelector.ashx?DocTypeId=" + docTypeId + "&docid=" + docId + "&taskid=" + taskId + "&wfstepid=" + stepId + "&userId=" + GetUID();
        switch (zambaApplication) {
            case "ZambaWeb":
                OpenDocTask3(taskId, docId, docTypeId, false, $scope.Result.Tarea, $scope.Result.Url, $scope.Result.UserId, 0);
                $('#Advfilter1').modal("hide");
                break;
            case "ZambaWindows": case "ZambaHomeWidget": case "ZambaQuickSearch":
                winFormJSCall.openTask(docTypeId, docId, taskId, stepId);
                $('#Advfilter1').modal("hide");
                break;
            case "Zamba":
                //var token = $scope.GetTokenInfo().token;
                window.open(Url, '_blank');
                break;
            case "ZambaSearch":
                //var token = $scope.GetTokenInfo().token;
                window.open(Url, '_blank');
                break;
        }
    }

    $scope.ShowIndexs = function (result, index) {
        event.stopPropagation();
        event.preventDefault();
        //A veces viene result como int(indice)
        if (typeof (result) == "number")
            result = $scope.Search.SearchResults[result];

        var currentresult = result;
        $scope.Result = currentresult;
        $scope.Result.UserId = GetUID();
        try {
            $http.post(ZambaWebRestApiURL + '/search/GetIndexData', $scope.Result).then(function (response) {
                $scope.Result.Indexs = JSON.parse(response.data);
                if ($scope.Result.Indexs.length) {
                    if (!$scope.Result.Indexs.filter(function (x) {
                        return x.Data != ""
                    }).length) {
                        $scope.Result.NoIndex = "No hay indices para mostrar";
                    }
                    $("#searchResultModalGS").modal();
                }
            })
                .catch(function (r) {
                    var r = r.data.ExceptionMessage;
                    var error = r.indexOf("\n") > -1 ? r.substring(0, r.indexOf("\n")) : r;
                    toastr.error(error, "Error al cargar indices");
                });
        }
        catch (ex) {
            console.log(ex);
        }
    }

    $scope.typeaheadOnSelect = function (item, model, label, event, e) {
        $("#searchboxControl").find(".search-parameter-input").attr("placeholder", "Agregar nueva busqueda...");
        var cn = $("body").data("colorNum");
        if (cn == undefined || cn == 10)
            cn = 1;
        else
            cn += 1;
        $("body").data("colorNum", cn);
        var color = 'b' + cn;

        var groupnum = $("body").data("colorGroup");
        $("body").data("colorGroup", groupnum == undefined ? 1 : groupnum + 1);
        groupnum = $("body").data("colorGroup");
        item.color = color;
        item.groupnum = groupnum;

        switch (item.type) {
            case 1:
                //seleccion de entidad
                var $item = $("#item" + item.id);
                var $filter = $item.children(".twitter-typeahead").children('.typeahead.thMain.tt-input');
                if (!$filter.length) $filter = $item.children(".typeahead");

                if ($filter.length && $filter.val().length >= 1) {
                    item.maingroup = true;
                    $scope.addSearchParam(item, "| Empieza: " + $filter.val());
                }
                else {
                    item.maingroup = true;
                    $scope.addSearchParam(item);
                }

                var $li = $item.children("ul").children("li");
                for (var i = 0; i <= $li.length - 1; i++) {
                    var $input = $($li[i]).children("input");
                    if (!$input.length) $input = $($li[i]).children("span").children(".typeahead.th.tt-input");
                    if ($input.css("display") != 'none' && $input.val().length >= 1) {
                        var abort = false;
                        for (var j = 0; j <= item.indexes.length - 1 && !abort; j++) {
                            var $liItem = $($input.parents("li")[0]);
                            if ($liItem.attr("id") == item.indexes[j].id) {
                                item.indexes[j].color = color;
                                item.indexes[j].groupnum = groupnum;
                                item.indexes[j].maingroup = false;
                                //Fecha, puede contenter "entre"
                                switch (item.indexes[j].index_type) {
                                    case 4: {
                                        var $dC = $liItem.find(".dateCompare");
                                        if ($liItem.find(".dateCompareChk input").is(":checked") && $dC.val() != "" && isDate($dC.val())) {
                                            item.indexes[j].operator = "Entre";
                                            item.indexes[j].value2 = $dC.val();
                                            $input.val($input.val() + " - " + $dC.val());
                                        }
                                        else {
                                            item.indexes[j].operator = "=";
                                            item.indexes[j].value2 = "";
                                        }
                                        break;
                                    }
                                    case 9: {//Checkbox
                                        if ($input.is(":checked"))
                                            $input.val(1);
                                        else
                                            continue;
                                    }
                                }
                                $scope.addSearchParam(item.indexes[j], $input.val());
                                abort = true;
                            }
                        }
                    }
                }
                break;
            case 2:
                //seleccion de atributo-indice
                var $val = $("#index" + item.id).children(".typeahead").length != 0 ? $("#index" + item.id).children(".typeahead") :
                    $("#index" + item.id).children(".twitter-typeahead").children(".typeahead.thMain.tt-input");
                var txt = $val.val();
                if (txt == undefined || txt == '')
                    iMsgTxt.push("Por favor ingrese un valor al atributo: " + item.name);

                item.maingroup = true;
                //Fecha, puede contenter "entre"
                if (item.index_type == 4) {
                    var $dC = $val.parent().find(".dateCompare");
                    if ($val.parent().find(".dateCompareChk input").is(":checked") && $dC.val() != "" && isDate($dC.val())) {
                        item.operator = "Entre";
                        item.value2 = $dC.val();
                        $val.val($val.val() + " - " + $dC.val());
                        txt = $val.val();
                    }
                    else {
                        item.operator = "=";
                        item.value2 = "";
                    }
                }
                $scope.addSearchParam(item, txt.length ? txt : null);

                if (item.parentArray.length) {
                    var $li = $("#index" + item.id).children("ul").children("li");
                    for (var i in $scope.parameters) {
                        for (var j = 0; j <= $li.length - 1; j++) {
                            var $chk = $($li[j].children[0].children[0]);
                            if ($chk.attr("value") == $scope.parameters[i].id && $chk.is(':checked')) {
                                $scope.parameters[i].color = color;
                                $scope.parameters[i].groupnum = groupnum;
                                $scope.parameters[i].maingroup = false;
                                $scope.addSearchParam($scope.parameters[i]);
                                break;
                            }
                        }
                    }
                }
                break;
            case 0:
                //seleccion de palabra   
                item.maingroup = true;
                $scope.addSearchParam(item, item.name);
                break;
        }
        //Armo Advanced filter: etapa, asignado, estado
        if (item.type == 1 || item.type == 2) {
            var $a = item.type == 1 ? $("#item" + item.id) : $("#index" + item.id);
            var $div = $a.children("ul").children(".advFilter").children("div");
            for (var i = 0; i <= $div.length - 1; i++) {
                var $inp = $($div[i]).children('input');
                if (!$inp.length) $inp = $($div[i]).children("span").children(".tt-input");

                if ($inp.css("display") != 'none' && $inp.val() != '') {
                    var w = $inp.data("word");
                    if (w != undefined && w.length) {
                        for (var j = 0; j <= w.length - 1; j++) {
                            if ($inp.val().toUpperCase() == w[j].toUpperCase()) {
                                var name = $($div[i]).children('button').attr("id");
                                var p = new FilterObj($inp.data("ids")[j], (name.charAt(0).toUpperCase() + name.slice(1)), $inp.val(), name, groupnum, false);
                                p.color = color;
                                $scope.addSearchParam(p, $inp.val());
                                break;
                            }
                        }
                    }
                    else
                        iMsgTxt.push("Por favor verifique el valor ingresado en: " + $($div[i]).children("button").attr("id"));
                }
            }
        }
        //Busca por palabra suelta
        if (item.type == undefined) {
            var inputTxt = inputGSTxt();

            if (inputTxt == "") return;

            var words = GetGSSuggestionWords();
            if (words.length > 0) {
                for (var i = 0; i <= words.length - 1; i++) {
                    var w = words[i];

                    if (w.word.indexOf(inputTxt) > -1) {
                        item.Word = inputTxt;
                        item.name = inputTxt;
                        item.type = 0;
                        item.DTID = w.dtid;
                        item.IndexId = w.indexid;
                        item.WordId = w.wordid;
                        item.maingroup = true;
                        $scope.addSearchParam(item, inputTxt);
                        break;
                    }
                }

                infoMsg(iMsgTxt);
                $scope.model.query = '';
                $scope.doSearchGS();
            }
            else {
                toastr.warning("Sin resultados");
            }
        }
    };

    $scope.addSearchParam = function (searchParam, value, enterEditMode) {
        //Establesco si es editable
        if (enterEditMode === undefined)
            //Solo indice, no palabra ni entidad
            if (searchParam.parent && !searchParam.parentindex)
                enterEditMode = true;
            else
                enterEditMode = false;

        if (searchParam.placeholder === undefined)
            searchParam.placeholder = '';

        $scope.model.parameters.push({
            id: searchParam.id,
            name: searchParam.name,
            placeholder: searchParam.placeholder,
            value: value || '',
            value2: searchParam.value2 || '',
            operator: searchParam.operator || 'Empieza',
            editMode: enterEditMode,
            type: searchParam.type,
            color: searchParam.color,
            groupnum: searchParam.groupnum,
            maingroup: searchParam.maingroup
        });
    };

    $scope.removeSearchParam = function (index) {
        if (index === undefined)
            return;
        $scope.Search.SearchResults = null;
        $scope.page = 0;
        $scope.isPagging = false;
        $("#resultsGridSearchBox").css("height", "50px");
        //$('#Advfilter1').find(".modal-dialog").css("width", "600px");
        RemSP($scope.model.parameters, index, false);

        if (!$scope.model.parameters.length) {
            $("#searchboxControl").find(".search-parameter-input").attr("placeholder", "Buscar...");
            $("#selectDescGS").data("desc", "");
            $("#SearchControls").show();
            $("#tabresults").hide();
        }
        else {
            $scope.doSearchGS();
        }
    };

    //Arma y guarda parametros para despues ser visualizados nuevamente en la lista desplegable
    $scope.editgroup = function (i) {
        if (i === undefined)
            return;
        event.stopPropagation();
        var p = $scope.model.parameters;
        var filters = [];
        var filtersVal = [];
        var $div = $(".search-parameter." + p[i].color);

        var typeTxt;
        switch (p[i].type) {
            case 0:
                typeTxt = "words";
                var f = {
                    name: p[i].name, value: p[i].value
                };
                break;
            case 1:
                typeTxt = "entity";
                for (var j = 0; j <= $div.length - 1; j++) {
                    if ($($div[j]).attr("colorgroup") == p[i].groupnum && $($div[j]).attr("class").indexOf(typeTxt) == -1) {
                        filters.push($($div[j]).attr("id").replace("sel", ''));
                        filtersVal.push($($div[j]).children(".value").text().trim());
                    }
                }
                var value = p[i].value.replace("| Empieza: ", "");
                var f = {
                    id: p[i].id, name: p[i].name, value: value, type: p[i].type, filter: filters, filtersVal: filtersVal
                };
                break;
            case 2:
                typeTxt = "index";
                for (var j = 0; j <= $div.length - 1; j++) {
                    if ($($div[j]).attr("colorgroup") == p[i].groupnum) {
                        filters.push($($div[j]).attr("id").replace("sel", ''));
                    }
                }
                var f = {
                    id: p[i].id, name: p[i].name, value: p[i].value, type: p[i].type, filter: filters
                };
                break;
        }

        for (var j = 0; j <= p.length - 1; j++) {
            switch (p[j].type) {
                case 5:
                    f.etapa = p[j].value;
                    f.etapaId = p[j].id;
                    break;
                case 6:
                    f.estado = p[j].value;
                    f.estadoId = p[j].id;
                    break;
                case 7:
                    f.asignado = p[j].value;
                    f.asignadoId = p[j].id;
                    break;
            }
        }
        $("body").data("filterBtn", typeTxt);
        $("body").data("filters", f);
        //Remueve los search parameters, los guarda en body-data para mostrar las mismas sugerencias
        RemSP(p, i, true);
    };

    $scope.removeAll = function () {
        if ($scope.Search !== undefined) $scope.Search.SearchResults = null;
        $("#resultsGridSearchBox").css("height", "50px");
        //$("#SearchControls").show();
        //$("#tabresults").hide();
        // $('#Advfilter1').find(".modal-dialog").css("width", "600px");
        $scope.model.parameters.length = 0;
        $("#selectDescGS").data("desc", "");
        $scope.model.query = '';
        // CleanKGrid();
    };

    $scope.editPrevious = function (currentindex) {
        var i;
        if (currentindex !== undefined) {
            $scope.leaveEditMode(currentindex);
            $scope.setSearchFocus = true;
        }

        //TODO: check if index == 0 -> what then?
        if (currentindex > 0) {
            $scope.enterEditMode(currentindex - 1);
        } else if ($scope.model.parameters.length > 0) {
            $scope.enterEditMode($scope.model.parameters.length - 1);
        }
    };

    $scope.editNext = function (currentindex) {
        if (currentindex === undefined)
            return;

        $scope.leaveEditMode(currentindex);

        //TODO: check if index == array length - 1 -> what then?
        if (currentindex < $scope.model.parameters.length - 1) {
            $scope.enterEditMode(currentindex + 1);
        } else {
            $scope.setSearchFocus = true;
        }
    };

    $scope.keydown = function (e, searchParamindex) {
        if ($(e.target).val().length < 2) {
            $("body").data('filterBtn', "all");
            advFilterSelect($("#lblGSFilAll"));
        }
        var handledKeys = [8, 9, 13, 37, 39];
        if (handledKeys.indexOf(e.which) === -1)
            return;



        var cursorPosition = getCurrentCaretPosition(e.target);

        if (e.which == 8) { // backspace
            //if (cursorPosition === 0) {
            //    if (searchParamindex !== undefined && $scope.model.parameters[searchParamindex].value.length === 0) {
            //        e.preventDefault();
            //        $scope.model.parameters.splice(searchParamindex, 1);
            //        $scope.setSearchFocus = true;
            //    } else if ($scope.model.query.length === 0) {
            //        $scope.model.parameters.pop();
            //    }
            //}

        } else if (e.which == 9) { // tab
            if (e.shiftKey) {
                e.preventDefault();
                $scope.editPrevious(searchParamindex);
            } else {
                e.preventDefault();
                $scope.editNext(searchParamindex);
            }

        } else if (e.which == 13) { // enter
            if (searchParamindex !== undefined)
                $scope.editNext(searchParamindex);

        } else if (e.which == 37) { // left
            if (cursorPosition === 0)
                $scope.editPrevious(searchParamindex);

        } else if (e.which == 39) { // right
            if (cursorPosition === e.target.value.length)
                $scope.editNext(searchParamindex);
        }

    };

    if ($scope.model === undefined) {
        $scope.model = {
        };
        $scope.model.parameters = [];
    }

    function getCurrentCaretPosition(input) {
        if (!input)
            return 0;

        // Firefox & co
        if (typeof input.selectionStart === 'number') {
            return input.selectionDirection === 'backward' ? input.selectionStart : input.selectionEnd;

        } else if (document.selection) { // IE
            input.focus();
            var selection = document.selection.createRange();
            var selectionLength = document.selection.createRange().text.length;
            selection.moveStart('character', -input.value.length);
            return selection.text.length - selectionLength;
        }

        return 0;
    }


    $scope.Result = {
        Id: 0,
        EntityId: 0,
        Name: '',
        UserId: 0,
        Url: '',
        Indexs: [],
        TaskId: '',
        StepId: ''
    };

    $scope.options = {
        link: true,      //convert links into anchor tags 
        linkTarget: '_self',   //_blank|_self|_parent|_top|framename 
        pdf: {
            embed: true                 //to show pdf viewer. 
        },
        image: {
            embed: false                //to allow showing image after the text gif|jpg|jpeg|tiff|png|svg|webp. 
        },
        audio: {
            embed: true                 //to allow embedding audio player if link to 
        },
        code: {
            highlight: false,        //to allow code highlighting of code written in markdown 
            //requires highligh.js (https://highlightjs.org/) as dependency. 
            lineNumbers: false        //to show line numbers 
        },
        basicVideo: false,     //to allow embedding of mp4/ogg format videos 
        gdevAuth: 'xxxxxxxx', // Google developer auth key for youtube data api 
        video: {
            embed: false,    //to allow youtube/vimeo video embedding 
            width: null,     //width of embedded player 
            height: null,     //height of embedded player 
            ytTheme: 'dark',   //youtube player theme (light/dark) 
            details: false,    //to show video details (like title, description etc.) 
        },
        tweetEmbed: false,
        tweetOptions: {
            //The maximum width of a rendered Tweet in whole pixels. Must be between 220 and 550 inclusive. 
            maxWidth: 550,
            //When set to true or 1 links in a Tweet are not expanded to photo, video, or link previews. 
            hideMedia: false,
            //When set to true or 1 a collapsed version of the previous Tweet in a conversation thread 
            //will not be displayed when the requested Tweet is in reply to another Tweet. 
            hideThread: false,
            //Specifies whether the embedded Tweet should be floated left, right, or center in 
            //the page relative to the parent element.Valid values are left, right, center, and none. 
            //Defaults to none, meaning no alignment styles are specified for the Tweet. 
            align: 'none',
            //Request returned HTML and a rendered Tweet in the specified. 
            //Supported Languages listed here (https://dev.twitter.com/web/overview/languages) 
            lang: 'en'
        },
        twitchtvEmbed: true,
        dailymotionEmbed: true,
        tedEmbed: true,
        dotsubEmbed: true,
        liveleakEmbed: true,
        soundCloudEmbed: true,
        soundCloudOptions: {
            height: 160, themeColor: 'f50000',   //Hex Code of the player theme color 
            autoPlay: false,
            hideRelated: false,
            showComments: true,
            showUser: true,
            showReposts: false,
            visual: false,         //Show/hide the big preview image 
            download: false          //Show/Hide download buttons 
        },
        spotifyEmbed: true,
        codepenEmbed: true,        //set to true to embed codepen 
        codepenHeight: 300,
        jsfiddleEmbed: true,        //set to true to embed jsfiddle 
        jsfiddleHeight: 300,
        jsbinEmbed: true,        //set to true to embed jsbin 
        jsbinHeight: 300,
        plunkerEmbed: true,        //set to true to embed plunker 
        githubgistEmbed: true,
        ideoneEmbed: true,        //set to true to embed ideone 
        ideoneHeight: 300
    };

    $scope.setselected = function (id, entityid, name) {
        $scope.Result.Id = id;
        $scope.Result.EntityId = entityid;
        $scope.Result.Name = name;
        $scope.Result.UserId = GetUID();
        let userToken = JSON.parse(localStorage.getItem('authorizationData'));
        let { token } = userToken
        $scope.Result.Url = (thisDomain + "/views/WF/TaskSelector.ashx?DocTypeId=" + entityid + "&docid=" + id + "&taskid=" + $scope.Result.TaskId + "&wfstepid=" + $scope.Result.StepId + "&userId=" + $scope.Result.UserId + "'," + $scope.Result.TaskId + ",'" + name + "'" + "&t=" + token);//$sce.trustAsResourceUrl
        OpenDocTask3($scope.Result.TaskId, id, entityid, false, name, $scope.Result.Url, $scope.Result.UserId, 0);
    };

    $scope.currentViewerUrl = null;



    $scope.ShowResult = function (result) {
        var currentresult = (typeof (result) == "number") ? $scope.Search.SearchResults[result] : result;
        $scope.Result = currentresult;
        $scope.Result.UserId = GetUID();
        var taskId = $scope.Result.TASK_ID;
        if (taskId == undefined) {
            taskId = $scope.Result.Task_Id;
        }
        var docId = $scope.Result.DOC_ID;
        var docTypeId = $scope.Result.DOC_TYPE_ID;
        var stepId = $scope.Result.STEP_ID;

        if (stepId == undefined) {
            stepId = $scope.Result.Step_Id;
        }
        if (stepId === undefined) {
            stepId = 0;
        }

        let userToken = JSON.parse(localStorage.getItem('authorizationData'));
        let { token } = userToken;
        $scope.Result.Url = thisDomain + "/views/WF/TaskSelector.ashx?DocTypeId=" + docTypeId + "&docid=" + docId + "&taskid=" + taskId + "&wfstepid=" + stepId + "&userId=" + GetUID() + "&t=" + token;

        //No borrar, esto se usa en la Web
        //OpenDocTask3(taskId, docId, docTypeId, false, $scope.Result.Name, $scope.Result.Url, $scope.Result.UserId,0);

        if (!OpenDocTask3()) {
            window.open($scope.Result.Url, '_blank');
        }
        //$('#Advfilter1').modal("hide");

    }

    $scope.ShowInsertBtn = false;



    $scope.ShowIndexs = function (index) {
        event.preventDefault();
        event.stopPropagation();
        var currentresult = $scope.Search.SearchResults[index];
        $scope.Result = currentresult;
        $scope.Result.UserId = GetUID();

        $http.post(ZambaWebRestApiURL + '/search/GetIndexData', $scope.Result).then(function (response) {
            $scope.Result.Indexs = JSON.parse(response.data);
            if ($scope.Result.Indexs.length)
                $("#searchResultModalGS").modal();
        });
    }

    $scope.HideListIcon = "glyphicon glyphicon-chevron-left";
    $scope.IsListHidden = false;
    $scope.HideList = function () {
        $scope.IsListHidden = !$scope.IsListHidden;
        if ($scope.IsListHidden == true) { $scope.HideListIcon = "glyphicon glyphicon-chevron-right" } else { $scope.HideListIcon = "glyphicon glyphicon-chevron-left" };
    };






    //#region Search

    //al presionar enter realiza la busqueda
    $scope.TriggerSearch = function (keyEvent) {
        if (keyEvent.which === 13) {
            $scope.Search.AsignedTasks = false;
            $scope.Search.LastPage = 0;

            $scope.DoSearchAttr();
            event.preventDefault();
        }
    }

    //#endregion Search




    //#region Filters
    $scope.Filter = function (filter) {
        var filters;
        if (filter["filter"] != null) {
            filters = filter["filter"].filters;

            $(filters).each(function (key, item) {
                var isIndex = false;
                for (var i in $scope.Search.Indexs) {
                    if (item.field == $scope.Search.Indexs[i].Name.replace(/ /g, "_")) {
                        $scope.Search.Indexs[i].Data = item.value;
                        $scope.Search.usedFilters.push(item.field);
                        isIndex = true;
                        break;
                    }
                }
                if (isIndex == false) {
                    $scope.Search.Filters.push({ Field: item.field, Operator: item.operator, Value: item.value });
                    $scope.Search.usedFilters.push(item.field);
                }
            });

        }
        else {
            $scope.Search.Filters = [];
            $scope.Search.usedFilters = [];
        }
        $scope.Search.LastPage = 0;

        $scope.DoSearch();
    }
    $scope.ResetFilters = function () {

        if (angular.element($("#taskController")).scope() != undefined) {
            angular.element($("#taskController")).scope().actionRules = null;

        }

        if ($scope.Search.SearchResultsObject != undefined) {
            $scope.Search.FiltersResetables = true;

            $scope.Search.SearchResultsObject.entities.forEach(function (element, index) {
                element.ResultsCount = 0;
            });
        }
    }


    //$scope.FilterGrid = function () {
    //    FilterKGrid($scope.searchGridText);
    //};

    $scope.FilterIndexs = [];

    //Devuelve la suma de filtros por atrobutos mas otros filtros adicionales.
    $scope.sumNonIndexedFilters = function () {
        Count = 0;

        if ($scope.Search.View == "search" || $scope.Search.View == "MyTasks") {
            if ($scope.Search.StepId > 0 && $scope.Search.StepFilter.IsChecked) {
                Count++;
            }
        }

        if ($scope.Search.UserAssignedId >= -2) {
            if ($scope.Search.UserAssignedFilter != undefined) {
                if ($scope.Search.UserAssignedFilter.IsChecked)
                    Count++;
            }
        }

        if ($scope.Search.crdateFilters) {
            if ($scope.Search.crdateFilters.length > 0) {
                let crdateFiltersChecked = $scope.Search.crdateFilters.filter(function (f) {
                    if (f.Enabled)
                        return f;
                });
                if (crdateFiltersChecked.length > 0) {
                    Count += crdateFiltersChecked.length;
                }
            }
        }
        if ($scope.Search.lupdateFilters) {
            if ($scope.Search.lupdateFilters.length > 0) {
                let lupdateFiltersChecked = $scope.Search.lupdateFilters.filter(function (f) {
                    if (f.Enabled)
                        return f;
                });
                if (lupdateFiltersChecked.length > 0) {
                    Count += lupdateFiltersChecked.length;
                }
            }
        }
        if ($scope.Search.nameFilters) {
            if ($scope.Search.nameFilters.length > 0) {
                let nameFiltersChecked = $scope.Search.nameFilters.filter(function (f) {
                    if (f.Enabled)
                        return f;
                });
                if (nameFiltersChecked.length > 0) {
                    Count += nameFiltersChecked.length;
                }
            }

        }

        if ($scope.Search.originalFilenameFilters) {
            if ($scope.Search.originalFilenameFilters.length > 0) {
                let originalFilenameFiltersChecked = $scope.Search.originalFilenameFilters.filter(function (f) {
                    if (f.Enabled)
                        return f;
                });
                if (originalFilenameFiltersChecked.length > 0) {
                    Count += originalFilenameFiltersChecked.length;
                }
            }
        }
        if ($scope.Search.stateFilters) {
            if ($scope.Search.stateFilters.length > 0) {
                let stateFiltersChecked = $scope.Search.stateFilters.filter(function (f) {
                    if (f.Enabled)
                        return f;
                });
                if (stateFiltersChecked.length > 0) {
                    Count += stateFiltersChecked.length;
                }
            }
        }
        if ($scope.Search.usedFilters == undefined)
            $scope.Search.usedFilters = [];
        let usedFiltersChecked = $scope.Search.usedFilters.filter(function (f) {
            if (f.IsChecked)
                return f;
        });
        return usedFiltersChecked.length + Count;
    }

    $scope.FillFilters = function (response) {
        if (response == null) {
            $scope.FilterIndexs = [];
            $scope.Filters = [];
        }
        else {
            $scope.FilterIndexs = response.filterIndexs;
        }
    };

    $scope.SettasksFilters = function (TaskFilterConfig) {
        try {

            if (TaskFilterConfig != null && TaskFilterConfig != undefined) {
                var filters = JSON.parse(TaskFilterConfig);
                //Obtengo los parametros que hacen visible a los checks por cada usuario, ver si hay que hacer algun casteo a bool
                if (filters.Params['ShowMyTasks'].toLowerCase() == "true") {
                    $scope.ShowchkMyTasks = true;
                    if (filters.Params['MyTasksText'] == "") {
                        $scope.MyTasksText = 'Mis Tareas';
                    } else {
                        $scope.MyTasksText = filters.Params['MyTasksText'];
                    }
                    if (filters.Params['ShowMyTeamTasks'].toLowerCase() != "true" && filters.Params['ShowMyAllTeamTasks'].toLowerCase() != "true" && filters.Params['ShowAllTasks'].toLowerCase() != "true") {
                        $scope.ValidateTask = true;
                    }

                } else {
                    $scope.ShowchkMyTasks = false;
                    $scope.MyTasksText = "";
                }


                if (filters.Params['ShowMyTeamTasks'].toLowerCase() == "true") {
                    $scope.ShowchkMyTeam = true;
                    if (filters.Params['MyTeamTasksText'] == "") {
                        $scope.MyTeamTasksText = 'Tareas del Equipo';
                    } else {
                        $scope.MyTeamTasksText = filters.Params['MyTeamTasksText'];
                    }

                } else {
                    $scope.ShowchkMyTeam = false;
                    $scope.MyTeamTasksText = "";
                }

                if (filters.Params['ShowMyAllTeamTasks'].toLowerCase() == "true") {
                    $scope.ShowchkMyAllTeam = true;
                    if (filters.Params['MyAllTeamTasksText'] == "") {
                        $scope.MyAllTeamTasksText = 'Todo el Equipo';
                    } else {
                        $scope.MyAllTeamTasksText = filters.Params['MyAllTeamTasksText'];
                    }

                } else {
                    $scope.ShowchkMyAllTeam = false;
                    $scope.MyAllTeamTasksText = "";
                }

                if (filters.Params['ShowAllTasks'].toLowerCase() == "true") {
                    $scope.ShowchkViewAllMy = true;
                    $scope.IdsAllTasks = filters.Params['IdsAllTasks'];

                    if (filters.Params['AllTasksText'] == "") {
                        $scope.AllTasksText = 'Mis Casos';
                    } else {
                        $scope.AllTasksText = filters.Params['AllTasksText'];
                    }

                } else {
                    $scope.ShowchkViewAllMy = false;
                    $scope.AllTasksText = "";
                }

            }
        } catch (e) {
            console.error(e);
        }

    }


    $scope.ValidateCheckMyTasks = function (ExecuteDoSearch) {

        if ($("#chkMyTasks").is(":checked") == false) {
            $checkbox_ResultsGrid = $($("input#chkMyTasks")[0]);
            $checkbox_ResultsGrid.prop('checked', !$checkbox_ResultsGrid.is(':checked'));
            $checkbox_ResultsGrid.triggerHandler('change');

            $checkbox_Filters = $($("input#chkMyTasks")[1]);
            $checkbox_Filters.prop('checked', $checkbox_ResultsGrid.prop('checked'));
            $checkbox_Filters.triggerHandler('change');

            $("input#chkMyTasks").each(function (i, e) {
                $checkbox = $(e);
                updateDisplay($checkbox);
            });

            $("input#chkViewAllMy").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $("input#chkMyTeam").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $("input#chkMyAllTeam").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $scope.Search.View = "MyTasks";
            $scope.Search.AsignedTasks = true;
            $scope.Search.LastPage = 0;
            $scope.Search.OrderBy = "";

            if (ExecuteDoSearch == true) {
                $scope.DoSearch();
            }
        }
    };


    $scope.ValidateCheckMyTeam = function (ExecuteDoSearch) {
        if ($("#chkMyTeam").is(":checked") == false) {
            $("input#chkMyTeam").each(function (i, e) {
                $checkbox = $(e);
                $checkbox.prop('checked', !$checkbox.is(':checked'));
                $checkbox.triggerHandler('change');
                updateDisplay($checkbox);
            });

            $("input#chkViewAllMy").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $("input#chkMyTasks").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $("input#chkMyAllTeam").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $scope.Search.View = "MyTeam";
            $scope.Search.AsignedTasks = true;
            $scope.Search.LastPage = 0;
            $scope.Search.OrderBy = "";
            if (ExecuteDoSearch == true) {
                $scope.DoSearch();
            }
        }
    };

    $scope.ValidateCheckMyAllTeam = function (ExecuteDoSearch) {
        if ($("#chkMyAllTeam").is(":checked") == false) {
            $("input#chkMyAllTeam").each(function (i, e) {
                $checkbox = $(e);
                $checkbox.prop('checked', !$checkbox.is(':checked'));
                $checkbox.triggerHandler('change');
                updateDisplay($checkbox);
            });

            $("input#chkViewAllMy").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $("input#chkMyTasks").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $("input#chkMyTeam").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $scope.Search.View = "MyAllTeam";
            $scope.Search.AsignedTasks = true;
            $scope.Search.LastPage = 0;
            $scope.Search.OrderBy = "";
            if (ExecuteDoSearch == true) {
                $scope.DoSearch();
            }
        }
    };


    $scope.ValidateCheckAllMy = function (ExecuteDoSearch) {
        if ($("#chkViewAllMy").is(":checked") == false) {
            $("input#chkViewAllMy").each(function (i, e) {
                $checkbox = $(e);
                $checkbox.prop('checked', !$checkbox.is(':checked'));
                $checkbox.triggerHandler('change');
                updateDisplay($checkbox);
            });

            $("input#chkMyTasks").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $("input#chkMyTeam").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $("input#chkMyAllTeam").each(function (i, e) {
                $(e).prop('checked', false);
                updateDisplay($(e));
            });

            $scope.Search.View = "ViewAllMy";
            $scope.Search.AsignedTasks = true;
            $scope.Search.LastPage = 0;
            $scope.Search.OrderBy = "";
            if (ExecuteDoSearch == true) {
                $scope.DoSearch();
            }
        }
    };

    $scope.ValidateCheckForTreeView = function () {
        $("#chkMyTasks").prop('checked', false);
        $("#chkMyTeam").prop('checked', false);
        $("#chkMyAllTeam").prop('checked', false);

        $checkbox = $("#chkViewAllMy");
        $checkbox.prop('checked', !$checkbox.is(':checked'));
        $checkbox.triggerHandler('change');

        $scope.Search.View = "MyProcess";
        $scope.Search.AsignedTasks = true;
        $scope.Search.LastPage = 0;
        $scope.Search.OrderBy = "";
    };

    function ShowHideFilters(sender) {
        if ($("#FiltersPanel").attr("display") == "none")
            $("#FiltersPanel").show();
        else
            $("#FiltersPanel").hide();
    };


    $scope.ShowHideFilters = function (sender) {
        if ($("#FiltersPanel").attr("display") == "none")
            $("#FiltersPanel").show();
        else
            $("#FiltersPanel").hide();
    };

    $scope.clearFilterShow = function () {
        $("select[placeholder='Selecciona un filtro']").prop("selectedIndex", 0);
        $("#DropVal").prop("selectedIndex", 0);

        if ($(".InputFilter > input")[0] != undefined)
            $(".InputFilter > input")[0].placeholder = "";

        $(".InputFilter :first-child").val("");
        $scope.Filter.Data = "";
        $scope.Filter.Name = "";
        $scope.Filter.dataDescription = "";
        $scope.Filter.CompareOperator = "";
    };

    $scope.filterPanelOpened = false;
    $scope.selectedFilterButton = "Attributes";
    $scope.toogleFilterPanel = function () {
        $scope.filterPanelOpened = !$scope.filterPanelOpened;

        setTimeout(function () {
            setTabSearchSize();
            ResizeResultsArea();

            if ($scope.LayoutPreview == "row") {
                resizeGridHeight();
            } else if ($scope.LayoutPreview == "column") {
                resizeTabHome();
            }

        }, 100);

    }

    $scope.showFilterControl = function (controlName) {
        $scope.selectedFilterButton = controlName;
    }
    //END ASIGNED
    //#endregion Filters

    $scope.handleViewCheck = function (sender) {

        $("#chkMyTeam").prop('checked', false);
        $("#chkMyTasks").prop('checked', false);
        $("#chkMyAllTeam").prop('checked', false);
        $("#chkViewAllMy").prop('checked', false);

        updateDisplay($("#chkViewAllMy"))
        updateDisplay($("#chkMyTasks"))
        updateDisplay($("#chkMyTeam"))
        updateDisplay($("#chkMyAllTeam"))


        var newValue = sender.name;

        try {
            //todo minuscula
            newValue = (!newValue) ? '' : newValue.toLowerCase();
            //espacios por guion medio
            newValue = (!newValue) ? '' : newValue.replace(/ /g, '-');
        } catch (e) {
            newValue = value;
        }

        ViewCheckTime = undefined;

        $checkbox = $('#chkv-' + sender.id);

        if ($checkbox.is(":checked") == false) {

            $checkbox.prop('checked', !$checkbox.is(':checked'));
            $checkbox.triggerHandler('change');
            updateDisplay($checkbox);
            sender.enabled = true;
            if (ViewCheckTime != undefined) clearTimeout(ViewCheckTime);
            ViewCheckTime = setTimeout(function () {
                $scope.ViewsCheckEnable = true;
                $scope.Search.View = 'reportid' + sender.reportId;
                $scope.Search.LastPage = 0;

                $scope.DoSearch();
            }, $scope.EntityCheckDelay);

        } else {
            $checkbox.prop('checked', !$checkbox.is(':checked'));
            $checkbox.triggerHandler('change');
            updateDisplay($checkbox)
            sender.enabled = false;
            if (ViewCheckTime != undefined) clearTimeout(ViewCheckTime);
            ViewCheckTime = setTimeout(function () {
                $scope.ViewsCheckEnable = true;
                $scope.Search.View = 'reportid' + sender.reportId;
                $scope.Search.LastPage = 0;

                $scope.DoSearch();
            }, $scope.EntityCheckDelay);
        }

    }

    $scope.handleViewCheckMultipleId = function (viewId, reportId) {

        $("#chkMyTeam").prop('checked', false);
        $("#chkMyTasks").prop('checked', false);
        $("#chkMyAllTeam").prop('checked', false);
        $("#chkViewAllMy").prop('checked', false);

        updateDisplay($("#chkViewAllMy"))
        updateDisplay($("#chkMyTasks"))
        updateDisplay($("#chkMyTeam"))
        updateDisplay($("#chkMyAllTeam"))

        ViewCheckTime = undefined;
        $checkbox = $('#chkv-' + viewId);
        $checkbox.prop('checked', !$checkbox.is(':checked'));
        $checkbox.triggerHandler('change');
        //updateDisplay($checkbox);
        //sender.enabled = true;
        $scope.ViewsCheckEnable = true;
        $scope.Search.View = 'reportid' + reportId;
        $scope.Search.LastPage = 0;

        $scope.DoSearch();

    }

    //#region Views

    //#region Entities Checks

    $scope.handleEntityCheck = function (sender) {
        if (angular.element($("#taskController")).scope() != undefined) {
            angular.element($("#taskController")).scope().actionRules = null;
        }

        var newValue = sender.name;

        try {
            //todo minuscula
            newValue = (!newValue) ? '' : newValue.toLowerCase();
            //espacios por guion medio
            newValue = (!newValue) ? '' : newValue.replace(/ /g, '-');
        } catch (e) {
            newValue = value;
        }

        if ($scope.EntitiesSelectionExclusive == true) {
            $('[id^=chke-]').each(function (i, e) {
                $(e).prop('checked', false);
                $(e).triggerHandler('change');
                updateDisplay($(e));
                $(e).enabled = false;

            });
        }

        $checkbox = $('#chke-' + newValue);
        if ($checkbox.is(":checked") == false) {

            $checkbox.prop('checked', !$checkbox.is(':checked'));
            $checkbox.triggerHandler('change');
            updateDisplay($checkbox);
            sender.enabled = true;

            if ($scope.EntityCheckTime != undefined)
                clearTimeout($scope.EntityCheckTime);

            $scope.EntityCheckTime = setTimeout(function () {
                $scope.EntitiesCheckEnable = true;
                $scope.Search.entities = $scope.Search.SearchResultsObject.entities;
                $scope.Search.LastPage = 0;

                $scope.DoSearch(true);

            }, $scope.EntityCheckDelay);

        } else {
            $checkbox.prop('checked', !$checkbox.is(':checked'));
            $checkbox.triggerHandler('change');
            updateDisplay($checkbox)
            sender.enabled = false;

            if ($scope.EntityCheckTime != undefined)
                clearTimeout($scope.EntityCheckTime);

            $scope.EntityCheckTime = setTimeout(function () {
                $scope.EntitiesCheckEnable = true;
                $scope.Search.entities = $scope.Search.SearchResultsObject.entities;
                $scope.Search.LastPage = 0;

                $scope.DoSearch(true);

            }, $scope.EntityCheckDelay);
        }

    }

    //#endregion Entities




    //#region FILTERS

    $scope.clearAllFiltersInPanel = function () {

        if (angular.element($("#taskController")).scope() != undefined) {
            angular.element($("#taskController")).scope().actionRules = null;
        }

        if ($scope.WFSideBarIsOpen == false) {
            $scope.Search.StepId = 0;
            $scope.Search.stateID = 0;
            $scope.lastSelectedNode = 0;
        }
        $scope.Search.LastPage = 0;

        $scope.$broadcast('removeAllDefaultZambaColumnFilter', false);

        //Este debe ser el ultimo, ya que tiene el evento dosearch
        var scope_filterController = angular.element($("#filterController")).scope();
        scope_filterController.ClearFiltersAndSearch(true);
    }


    //#endregion FILTERS

    //#region ASIGNED
    $scope.AsignedSelected = function (item, model) {

        //limpiar el usuario asignado anterior
        SearchFilterService.DeleteUserAssignedFilter($scope.Search.DoctypesIds[0], $scope.Search.currentMode);

        let filterValue = item.Name;
        $scope.Search.UserAssignedId = model;

        if (filterValue == '(Sin Asignar)')
            filterValue = "";

        if (filterValue == '- Solo asignadas -')
            filterValue = "";

        //cuando la seleccion es 'Todos' pidieron que el estado sea unchecked
        var setCheckedState = ($scope.Search.UserAssignedId != -1);
        $scope.Search.UserAssignedFilter.IsChecked = setCheckedState;

        if ($scope.Search.UserAssignedId != -1) {
            let comparator = "=";
            if ($scope.Search.UserAssignedId == 0)
                comparator = "Es Nulo";
            if ($scope.Search.UserAssignedId == -2)
                comparator = "No es Nulo";

            var zFilterWebItem = { indexId: 0, attribute: "uag.NAME", dataType: "7", comparator: comparator, filterValue: filterValue, docTypeId: $scope.Search.DoctypesIds[0], description: "Asignado", additionalType: "10", dataDescription: "", filterType: $scope.Search.View };

            let filterResult = SearchFilterService.AddFilter(zFilterWebItem);
            $scope.Search.UserAssignedFilter.zFilterWebID = filterResult.Id;
        }
        else {
            if ($scope.Search.UserAssignedFilter != undefined) {
                $scope.Search.UserAssignedFilter.zFilterWebID = 0;
                $scope.Search.UserAssignedFilter.IsChecked = false;
            }
        }
        $scope.saveLastFiltersState();
        $scope.DoSearch();
    }
    $scope.saveLastFiltersState = function () {
        console.log('saveLastFiltersState');

        var filtersFromDB = SearchFilterService.GetFiltersByView($scope.Search.DoctypesIds[0], $scope.Search.currentMode);
        if (!($scope.Search.lastFiltersByView instanceof Map)) {
            $scope.Search.lastFiltersByView = new Map();
        }
        $scope.Search.lastFiltersByView.set($scope.Search.currentMode, JSON.stringify(filtersFromDB));
        console.log($scope.Search.lastFiltersByView.get($scope.Search.currentMode));
        console.log("items: " + filtersFromDB.length);
    }

    $scope.StepFilterSelected = function (item, model) {
        //limpiar la etapa anterior
        SearchFilterService.DeleteStepFilter($scope.Search.DoctypesIds[0], $scope.Search.currentMode);

        var setCheckEnabledState = (model != -1);
        $scope.Search.StepFilter.IsChecked = setCheckEnabledState;

        if (model != undefined) {
            var zFilterWebItem = { indexId: 0, attribute: "STEPID", dataType: "7", comparator: "=", filterValue: model, docTypeId: $scope.Search.DoctypesIds[0], description: "Etapa", additionalType: "10", dataDescription: "", filterType: $scope.Search.View };

            let filterResult = SearchFilterService.AddFilter(zFilterWebItem);
            $scope.Search.StepFilter.zFilterWebID = filterResult.Id;
        }
        $scope.saveLastFiltersState();
        $scope.DoSearch();
    }


    $scope.ClearAsignedSelected = function (executeDoSearch) {
        $scope.Search.UserAssignedId = -1;
        if ($scope.Search.UserAssignedFilter != undefined) {
            if ($scope.Search.UserAssignedFilter.zFilterWebID != 0)
                SearchFilterService.RemoveFilterById($scope.Search.UserAssignedFilter.zFilterWebID);
            $scope.Search.UserAssignedFilter.zFilterWebID = 0;
            $scope.Search.UserAssignedFilter.IsChecked = false;
        }
        $scope.saveLastFiltersState();
        if (executeDoSearch) {
            $scope.DoSearch();
        }
    }
    $scope.DeleteFilter = function (filter) {

        for (var f in $scope.Search.usedFilters) {
            if ($scope.Search.usedFilters[f].ID == filter.ID) {
                $scope.Search.usedFilters.splice(f, 1);
                break;
            }
        }

        var isIndex = false;
        for (var i in $scope.Search.Indexs) {
            if (filter.ID == $scope.Search.Indexs[i].ID) {
                $scope.Search.Indexs[i].Data = '';
                $scope.Search.Indexs[i].dataDescription = '';
                isIndex = true;
                break;
            }
        }
        if (isIndex == false) {
            for (var f in $scope.Search.Filters) {
                if ($scope.Search.Filters[f].ID == filter.ID) {
                    $scope.Search.Filters.splice(f, 1);
                    break;
                }
            }
        }
        if ($scope.Search.Filters.length = !0) {
            $scope.Search.Filters = [];
        }

        if (filter.zFilterWebID != undefined) {
            SearchFilterService.RemoveFilterById(filter.zFilterWebID);
        }

        $rootScope.$broadcast('filtersAdded');
    };
    $scope.ClearStepFilterSelected = function (executeDoSearch) {
        if ($scope.Search.View != 'MyProcess')
            $scope.Search.StepId = 0;

        if ($scope.Search.StepFilter != undefined) {

            if ($scope.Search.StepFilter.zFilterWebID != 0)
                SearchFilterService.RemoveFilterById($scope.Search.StepFilter.zFilterWebID);

            $scope.Search.StepFilter.zFilterWebID = 0;
            $scope.Search.StepFilter.IsChecked = false;
        }
        if (executeDoSearch) {
            $scope.DoSearch();
        }
    }

    $scope.changeEnableStateFilter = function (filterChecked) {
        if (filterChecked.zFilterWebID != undefined) {
            SearchFilterService.SetEnabledFilterById(filterChecked.zFilterWebID, filterChecked.IsChecked);
        }
        $rootScope.$broadcast('filtersAdded');
    }
    //#endregion ASIGNED

    $scope.clear = function () {
        $scope.option1 = [];
    };

    $scope.randomSelect = function () {
        $scope.clear();
        var arrSelected = [$scope.option1, $scope.option2, $scope.option3, $scope.option4, $scope.option5, $scope.option6, $scope.option7];
        var arrOptions = [$scope.options1, $scope.options2, $scope.options2, $scope.options1, $scope.options1, $scope.options1, $scope.options2];
        var arrIsSingle = [false, false, false, true, false, false, false];
        var arrIsSimple = [true, true, false, false, true, true, true];

        for (var i = 0; i < arrSelected.length; i++) {
            var selected = arrSelected[i];
            var options = arrOptions[i];
            var isSingle = arrIsSingle[i];
            var isSimple = arrIsSimple[i];
            var min = 0;
            var max = options.length - 1;
            if (isSingle) {
                var randIndex = getRandomInt(min, max);
                if (isSimple) {
                    selected.push(options[randIndex].key);
                } else {
                    selected.push(options[randIndex]);
                }
            }
            else {
                var toSelectIndexes = [];
                var numItems = getRandomInt(0, options.length) + 1;
                for (var j = 0; j < getRandomInt(1, numItems); j++) {
                    var randIndex = getRandomInt(min, max);
                    var arrIndex = toSelectIndexes.indexOf(randIndex);
                    if (arrIndex == -1) {
                        toSelectIndexes.push(randIndex);
                        if (isSimple) {
                            selected.push(options[randIndex].key);
                        } else {
                            selected.push(options[randIndex]);
                        }
                    }
                }
            }
        }
    }

    $scope.$on('LoadStep', function (event, args) {
        $scope.selectedStepStateName = args.StepStateName;
    });

    $scope.GetMailUsers = function () {
        var params = DocIdschecked;
        var EmailController = angular.element($("#EmailController")).scope();
        EmailController.GetMails(params);
    }

});



app.controller('appFilterController', function ($scope, $http, $rootScope, FieldsService, SearchFilterService) {
    $scope.showFilterIndex = true;

    $scope.searchFiltersByEnter = function (e) {
        if (e.charCode == 13) {
            $scope.Filter.Data = e.target.value;
            $scope.Filter.CompareOperator = $("#DropVal").val();
            $scope.Filter.Data = e.target.value;
            $scope.AddFilter();
        }
    }


    $scope.FiltersIndexSelected = function () {

        if ($scope.Filter.Index != null) {
            if ($scope.Filter.ID == 0 && $scope.Filter.Index.ID != null) {

                $scope.Filter.ID = $scope.Filter.Index.ID;

            } else if ($scope.Filter.ID != $scope.Filter.Index.ID && $scope.Filter.Index.ID != null) {

                $scope.Filter.ID = $scope.Filter.Index.ID;

            }

            if ($scope.Filter.Name == "" && $scope.Filter.Index.Name != null) {

                $scope.Filter.Name = $scope.Filter.Index.Name;
            }
            else if ($scope.Filter.Name != $scope.Filter.Index.Name && $scope.Filter.Index.Name != null) {

                $scope.Filter.Name = $scope.Filter.Index.Name;

            }



            if ($scope.Filter.ID != null) {
                FieldsService.GetAll($scope.Filter.ID).then(function (d) {
                    var Index = JSON.parse(d.data);
                    if (Index != null) {
                        $scope.FilterIsShowing = false;

                        $scope.Filter.Name = Index.Name;
                        $scope.Filter.Type = Index.Type;

                        //todo: ML
                        //Si el operador previamente seleccionado no es compatible con el tipo de indice, se debe rastaurar al por defecto de ese tipo.  $scope.Filter.CompareOperator = Index.Operator; //ver que esta linea borra el operador, lo setea pero no se ve como seleccionado en el combo.
                        $scope.Filter.DropDown = Index.DropDown;
                        $scope.Filter.DropDownList = Index.DropDownList;
                        $scope.FilterIsShowing = true;

                    }

                    //if ($scope.Filter.Type =! $scope.Filter.Index.Type) {
                    //    $scope.Filter.Type == $scope.Filter.Index.Type
                    //}






                }, function (e) {
                    console.error(e);
                });
            }
            else {
                $scope.FilterIsShowing = false;
                $scope.Filter.Name = '';
                $scope.Filter.Type = 0;
                $scope.Filter.DropDown = 0;
                $scope.Filter.DropDownList = [];
                $scope.Filter.CompareOperator = "=";
            }
        }
    };

    $scope.FiltersOperatorSelected = function () {
        $scope.showFilterIndex = !($scope.Filter.CompareOperator.toLowerCase() == 'es nulo' || $scope.Filter.CompareOperator.toLowerCase() == 'no es nulo');
        console.log("Operador seleccionado: " + $scope.Filter.CompareOperator);
    };

    $scope.Filter = {
        ID: 0,
        Name: '',
        CompareOperator: '=',
        ValueString: '',
        Value: '',
        IsChecked: true,
        CurrentUserId: 0,
        StepId: 0,
        EntitiesIds: '',
        DropDown: 0,
        Type: 1,
        DropDownList: [],
        Index: null
    };

    $scope.TasksFilters = [{
        column: 'assignedto',
        checked: true,
        name: "Asignadas a mi",
        value: "Zamba.Me"
    }, {
        column: 'assignedto',
        checked: true,
        name: "Asignadas a mi Equipo",
        value: "Zamba.MyTeam"
    }, {
        column: 'assignedto',
        checked: true,
        name: "Asignadas a",
        value: "Zamba.Ask"
    }];

    $scope.TasksViews = [{
        column: 'assignedto',
        checked: true,
        name: "Asignadas a mi",
        value: "Zamba.Me"
    }, {
        column: 'assignedto',
        checked: true,
        name: "Asignadas a mi Equipo",
        value: "Zamba.MyTeam"
    }, {
        column: 'assignedto',
        checked: true,
        name: "Asignadas a",
        value: "Zamba.Ask"
    }];


    $scope.handleRadioClick = function (radius) {
        alert(radius.price);
    };

    $scope.$on('zambaFilterOnChangeEvent', function (event, data) {

        let executeSearch = true;

        if (data.crdateFilters != undefined)
            $scope.Search.crdateFilters = data.crdateFilters;
        if (data.lupdateFilters != undefined)
            $scope.Search.lupdateFilters = data.lupdateFilters;
        if (data.nameFilters != undefined)
            $scope.Search.nameFilters = data.nameFilters;
        if (data.originalFilenameFilters != undefined)
            $scope.Search.originalFilenameFilters = data.originalFilenameFilters;
        if (data.stateFilters != undefined)
            $scope.Search.stateFilters = data.stateFilters;

        if (data.executeSearch != undefined)
            executeSearch = data.executeSearch;

        $rootScope.$broadcast('filtersAdded', executeSearch);
    });

    $scope.$on('enabledStateChangeZFiltersWebEvent', function (event, zFilterWebItem) {
        if (zFilterWebItem != undefined) {
            zFilterWebItem.docTypeId = $scope.Search.DoctypesIds[0];
            SearchFilterService.SetEnabledFilter(zFilterWebItem)
        }
    });


    $scope.$on('removeOtherZFiltersWebEvent', function (event, zFilterWebItem) {
        if (zFilterWebItem != undefined) {
            zFilterWebItem.docTypeId = $scope.Search.DoctypesIds[0];
            SearchFilterService.RemoveOtherFilters(zFilterWebItem, $scope.Search.View);
        }
    });

    $scope.validateUsedFilter = function () {
        if ($scope.Search.usedFilters == undefined) {
            $scope.Search.usedFilters = [];
        }
    };
    $scope.AddFilter = function () {

        $scope.validateUsedFilter();
        var matchedFilters = $scope.Search.usedFilters.filter(function (f) {
            return (f.ID == $scope.Filter.ID && f.CompareOperator == $scope.Filter.CompareOperator && f.dataDescription == $scope.Filter.dataDescription && f.Data.toString() == $scope.Filter.Data.toString());
        });

        if (matchedFilters.length > 0)
            return;

        if (angular.element($("#taskController")).scope() != undefined) {
            angular.element($("#taskController")).scope().actionRules = null;

        }
        $scope.Filter.CurrentUserId = GetUID();

        var dtids = '';
        if ($scope.Search.DoctypesIds.join == undefined) {
            dtids = $scope.Search.DoctypesIds
        }
        else {
            dtids = $scope.Search.DoctypesIds.join(",");
        }
        $scope.Filter.EntitiesIds = dtids;

        var saveFilterByDoctypeId = false;
        if ($scope.Search.DoctypesIds.length == 1)
            saveFilterByDoctypeId = true;

        if ($('#ModalSearch2').hasClass('in'))
            $("#ModalSearch2").modal("hide");

        if ($scope.Filter.CompareOperator != $("#DropVal").val()) {
            $scope.Filter.CompareOperator = $("#DropVal").val();
        }

        if (($scope.Filter.Data == undefined || $scope.Filter.Data == "") && $scope.Filter.dataDescription != undefined) {
            $scope.Filter.Data = $scope.Filter.dataDescription;
        }
        if ($scope.Filter.CompareOperator.toLowerCase() == "es nulo" || $scope.Filter.CompareOperator.toLowerCase() == "no es nulo") {
            $scope.Filter.Data = "";
            $scope.Filter.dataDescription = "";
        }
        if (($scope.Filter.ID != 0 && $scope.Filter.Data != undefined && $scope.Filter.Name != "") || ($scope.Filter.CompareOperator.toLowerCase() == "es nulo" && $scope.Filter.Name != "") || ($scope.Filter.CompareOperator.toLowerCase() == "no es nulo" && $scope.Filter.Name != "")) {
            var isSlst = false;
            let filterValue = "";
            for (var i in $scope.Search.Indexs) {
                if ($scope.Filter.ID == $scope.Search.Indexs[i].ID) {
                    if (($scope.Filter.DropDown == 1 || $scope.Filter.DropDown == 2 || $scope.Filter.DropDown == 3 || $scope.Filter.DropDown == 4)) {
                        isSlst = true;
                    }
                    break;
                }
            }
            if (isSlst)
                filterValue = $scope.Filter.dataDescription;
            else
                filterValue = $scope.Filter.Data;

            var newFilter = { ID: $scope.Filter.ID, Name: $scope.Filter.Name, IsChecked: $scope.Filter.IsChecked, Data: filterValue, dataDescription: $scope.Filter.dataDescription, CompareOperator: $scope.Filter.CompareOperator, CurrentUserId: $scope.Filter.CurrentUserId, StepId: $scope.Filter.StepId, EntitiesIds: $scope.Filter.EntitiesIds };

            if (saveFilterByDoctypeId) {
                var zfilterWeb = {
                    "indexId": $scope.Filter.ID,
                    "attribute": $scope.Filter.ID.toString(),
                    "dataType": $scope.Filter.Type,
                    "comparator": $scope.Filter.CompareOperator,
                    "filterValue": filterValue,
                    "docTypeId": $scope.Search.DoctypesIds[0],
                    "description": $scope.Filter.Name,
                    "additionalType": $scope.Filter.Index.TypeIndex,
                    "dataDescription": ($scope.Filter.dataDescription == undefined ? "" : $scope.Filter.dataDescription),
                    "filterType": $scope.Search.View
                };
                let filterResult = SearchFilterService.AddFilter(zfilterWeb);

                if (filterResult.Id == null || filterResult.Id == undefined) {
                    toastr.error('Error, intente nuevamente');
                    return;
                }
                newFilter.zFilterWebID = filterResult.Id;
            }
            $scope.Search.usedFilters.push(newFilter);

            $scope.Search.OrderBy = "";
            $rootScope.$broadcast('filtersAdded');


            setTimeout(function () {
                $("select[placeholder='Selecciona un filtro']").prop("selectedIndex", 0);
                $("#DropVal").prop("selectedIndex", 0);
                $(".InputFilter :first-child").val("");
                $scope.Filter.Data = "";
                $scope.Filter.Name = "";
                $scope.Filter.dataDescription = "";
                $scope.showFilterIndex = true;
            }, 1000)



        }
        else {
            toastr.error('Complete todos los campos');
        }

    };

    $scope.CleanFilterInputs = function () {


        $("select[placeholder='Selecciona un filtro']").prop("selectedIndex", 0);
        $("#DropVal").prop("selectedIndex", 0);
        $(".InputFilter :first-child").val("");
        $scope.Filter.Data = "";
        $scope.Filter.Name = "";
        $scope.Filter.dataDescription = "";

    }

    $scope.changeEnableState = function (filterChecked) {
        if (filterChecked.zFilterWebID != undefined) {
            SearchFilterService.SetEnabledFilterById(filterChecked.zFilterWebID, filterChecked.IsChecked);
        }
        $rootScope.$broadcast('filtersAdded');
    }

    $scope.DeleteFilter = function (filter) {

        for (var f in $scope.Search.usedFilters) {
            if ($scope.Search.usedFilters[f].ID == filter.ID) {
                $scope.Search.usedFilters.splice(f, 1);
                break;
            }
        }

        var isIndex = false;
        for (var i in $scope.Search.Indexs) {
            if (filter.ID == $scope.Search.Indexs[i].ID) {
                $scope.Search.Indexs[i].Data = '';
                $scope.Search.Indexs[i].dataDescription = '';
                isIndex = true;
                break;
            }
        }
        if (isIndex == false) {
            for (var f in $scope.Search.Filters) {
                if ($scope.Search.Filters[f].ID == filter.ID) {
                    $scope.Search.Filters.splice(f, 1);
                    break;
                }
            }
        }
        if ($scope.Search.Filters.length = !0) {
            $scope.Search.Filters = [];
        }

        if (filter.zFilterWebID != undefined) {
            SearchFilterService.RemoveFilterById(filter.zFilterWebID);
        }

        $rootScope.$broadcast('filtersAdded');
    };

    $scope.DeleteFilters = function () {

        var isIndex = false;
        for (var u in $scope.Search.usedFilters) {

            for (var i in $scope.Search.Indexs) {
                if ($scope.Search.usedFilters[u].ID == $scope.Search.Indexs[i].ID) {
                    $scope.Search.Indexs[i].Data = '';
                    $scope.Search.Indexs[i].dataDescription = '';
                    isIndex = true;
                    break;
                }
            }
            if (isIndex == false) {
                for (var f in $scope.Search.Filters) {
                    if ($scope.Search.Filters[f].ID == $scope.Search.usedFilters[u].ID) {
                        $scope.Search.Filters.splice(f, 1);
                        break;
                    }
                }
            }
        }
        $scope.Search.usedFilters = [];
        $scope.Filter.dataDescription = '';
        $scope.Filter.Data = '';
    };

    function ClearEntities() {
        $scope.Search.entities.forEach(function (elem, i) {
            $(elem).prop('checked', true);
            $(elem).triggerHandler('change');
            updateDisplay($(e));
            $(elem).enabled = true;
        });
    }

    $scope.ClearEntities = function () {
        $scope.Search.entities.forEach(function (elem, i) {
            $(elem).prop('checked', true);
            $(elem).triggerHandler('change');
            updateDisplay($(e));
            $(elem).enabled = true;
        });
    }

    $scope.ClearFiltersAndSearch = function (clearAll) {
        if (clearAll) {
            $scope.Search.UserAssignedId = -1;
            let executeDoSearch = false;
            $scope.ClearAsignedSelected(executeDoSearch);
            if ($scope.Search.View != 'MyProcess')
                $scope.ClearStepFilterSelected(executeDoSearch);
        }
        SearchFilterService.RemoveAllIndexFilters($scope.Search.DoctypesIds[0], $scope.Search.currentMode);
        $scope.Search.usedFilters = [];
        $scope.Filter.dataDescription = '';
        $scope.Filter.Data = '';
        $scope.saveLastFiltersState();
        $scope.DoSearch();
    };

    $scope.$on('ClearFilters', function (event, data) {
        console.log('ClearFilters');
        $scope.DeleteFilters();
    });

    $scope.FilterDataChanged = function () {

    };

    $scope.subscribeItem_datepicker = function () {
        if (!($(".fechaInput").hasClass("TengoCalendar"))) {
            setTimeout(function () {
                $(".datepicker").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "mm-yyyy",
                    viewMode: "months",
                    minViewMode: "months"
                });
                $(".datepicker").datepicker().mask("99/99/9999");
                $(".fechaInput").focus();
                $(".fechaInput").addClass("TengoCalendar");
            }, 0);
        }
    };





    //obtengo los filtros aplicados y los muestro en el dropdown
    $scope.GetUsedFilters = function () {
        var usedfilters = [];
        usedfilters.CurrentUserId = GetUID();
        usedfilters.StepId = 0;
        //var SubListName = sessionStorage.getItem('SubListName');

        $.ajax({
            type: "POST",
            url: "../../Services/TaskService.asmx/getUsedFilters",
            data: "{usedfilters:" + JSON.stringify(usedfilters) + "}",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $scope.usedFilters = data.d;
            },
        });
    };


    $scope.CleanData = function (Index) {
        if (Index !== undefined) {
            $scope.Index.Data = "";
            $scope.Index.dataDescription = "";
        }
    }

    $scope.CleanData2 = function (Index) {
        if (Index !== undefined) {
            Index.Data2 = "";
            Index.dataDescription2 = "";
        }
    }

    $scope.saveDataBetween = function (Index, Value, Label) {
        if (Value != undefined) //Valor por seleccion de typeahead o modal
        {
            Index.dataDescription2 = Label;
            Index.Data2 = Value;
            if ($('#ModalSearch2').hasClass('in'))
                $("#ModalSearch2").modal("hide");
        }
    };

    //$scope.saveOperator = function (Index, Value) {
    //    if (Value != undefined) {
    //        if (Value != "Entre") {
    //            Index.Data2 = "";
    //           Index.dataDescription2 = "";
    //        }
    //        Index.Operator = Value;
    //    }
    //};

    //Trae el modal con los datos
    $scope.showlist = function (Filter) {

        var indexData = $http.post(ZambaWebRestApiURL + '/search/ListOptions', JSON.stringify({
            IndexId: Filter.ID,
            Value: "",//.DataTemp,
            LimitTo: 10
        })).then(function (response) {
            var results = JSON.parse(response.data);
            Filter.DropDownList = results;
            BtnTrashHidden();
            if (!$('#ModalSearch2').hasClass('in')) {
                $('#ModalSearch2').appendTo("#resultsGridSearchGrid");
                $("#ModalSearch2").modal();
            }
        });
    };

    $scope.showListFirstTime2 = function (Filter, moreResults) {
        LimitToSlst = 20;
        $($("input[id='Filter.ID']")[1]).val("");
        $scope.showlistFilter(Filter, moreResults, true);
    }
    $scope.showlistFilter = function (Filter, moreResults, firstTime) {

        if (moreResults)
            LimitToSlst += 20;
        else
            LimitToSlst = 20;
        var valueSearch = "";
        if (!firstTime)
            valueSearch = Filter.dataDescription;

        var indexData = $http.post(ZambaWebRestApiURL + '/search/ListOptions', JSON.stringify({
            IndexId: Filter.ID,
            Value: valueSearch,//.DataTemp,
            LimitTo: LimitToSlst
        })).then(function (response) {
            //var results = JSON.parse(response.data);

            //BtnTrashHidden();
            //if (!$('#ModalSearch2').hasClass('in')) {
            //    $('#ModalSearch2').appendTo("#resultsGridSearchGrid");
            //    $("#ModalSearch2").modal();
            //}
            var results = JSON.parse(response.data);

            if (results.length > LimitToSlst) {
                {
                    $("#searchMoreResultsFilter").show();
                    results.pop();
                }
            }
            else {
                $("#searchMoreResultsFilter").hide();
            }
            Filter.DropDownList = results;
            //Search.selectedIndex.DropDownList = Filter.dataDescription;

            BtnTrashHidden();

            //if (!$('#ModalSearch').hasClass('in')) {
            //    $("#ModalSearch").modal();
            //    $("#ModalSearch").draggable();
            //    $("#modalFormHome > div")[0].childNodes[1].value = "";
            //}
            //if (!$('#ModalSearch2').hasClass('in')) {
            //    $("#ModalSearch2").modal();
            //    $("#ModalSearch2").draggable();
            //    $("#modalFormHome > div")[0].childNodes[1].value = "";
            //}
            if (!$('#ModalSearch2').hasClass('in')) {
                $('#ModalSearch2').appendTo("#resultsGridSearchGrid");
                $("#resultsGridSearchGrid").css('display', 'block');
                $("#ModalSearch2").modal();

            }


        });
    };

    //Muestra la lista desplegable otra vez al borrar los Datos del input
    //$scope.ShowListAfter = function () {
    //    var indexData = $http.post(ZambaWebRestApiURL + '/search/ListOptions', JSON.stringify({
    //        IndexId: Index.ID,
    //        Value: Index.dataDescription,//.DataTemp,
    //        LimitTo: 10
    //    })).then(function (response) {
    //        var results = JSON.parse(response.data);
    //        Search.selectedIndex.DropDownList = results;
    //        if (!$('#ModalSearch2').hasClass('in'))
    //            $("#ModalSearch2").modal();
    //    });
    //}


    $scope.CleanData = function (Index) {
        if (Index !== undefined) {
            $scope.Index.Data = "";
            $scope.Index.dataDescription = "";
        }
    }

    $scope.CleanData2 = function (Index) {
        if (Index !== undefined) {
            Index.Data2 = "";
            Index.dataDescription2 = "";
        }
    }


    $scope.selectedIndex = null;
    $scope.ListItems = [];

    function constructMap(data, map) {
        var objects = [];
        $.each(data, function (i, object) {
            map[object.Value] = object;
            objects.push(object.Value);
        });
        return objects;
    }

    $scope.selectMatch = function (Index) {
        $scope.selectedIndex.Data = $scope.ListItems[Index].Code;
        $scope.selectedIndex.dataDescription = $scope.ListItems[Index].Value;
    };

    $scope.dateFields = [];

    $scope.generateGrid = function (gridData) {
        var model = $scope.generateModel(gridData[0]);
        var parseFunction;
        parseFunction = function (response) {
            for (var i = 0; i < response.length; i++) {
                for (var fieldIndex = 0; fieldIndex < $scope.dateFields.length; fieldIndex++) {
                    var record = response[i];
                    record[$scope.dateFields[fieldIndex]] = kendo.parseDate(record[$scope.dateFields[fieldIndex]]);
                }
            }
            return response;
        };
    }

    $scope.generateModel = function (gridData) {
        var model = {};
        model.id = "ID";
        var fields = {};
        for (var property in gridData) {
            var propType = typeof gridData[property];

            if (propType == "number") {
                fields[property] = {
                    type: "number",
                    validation: {
                        required: true
                    }
                };
            } else if (propType == "boolean") {
                fields[property] = {
                    type: "boolean",
                    validation: {
                        required: true
                    }
                };
            } else if (propType == "string") {
                var parsedDate = kendo.parseDate(gridData[property]);
                if (parsedDate) {
                    fields[property] = {
                        type: "date",
                        validation: {
                            required: true
                        }
                    };
                    $scope.dateFields.push(property);
                } else {
                    fields[property] = {
                        validation: {
                            required: true
                        }
                    };
                }
            } else {
                fields[property] = {
                    validation: {
                        required: true
                    }
                };
            }
        }
        model.fields = fields;
        return model;
    }





});

app.factory('FieldsTypesService', function ($http) {
    var BaseURL = '';
    var fac = {};
    fac.GetAll = function () {
        return $http.get(ZambaWebRestApiURL + '/FieldTypesApi');
    }
    return fac;
});

app.factory('userConfigService', function ($http) {
    var BaseURL = '';
    var fac = {};
    fac.GetAll = function () {
        return $http.get(ZambaWebRestApiURL + '/FieldTypesApi');
    }
    return fac;
});


var ztoken = '';
app.factory('EntityFieldsService', function ($http) {
    var BaseURL = '';
    var fac = {};
    fac.GetAll = function (SelectedDoctypesIds) {

        var response;

        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/Search/Indexs',
            data: JSON.stringify(SelectedDoctypesIds),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    response = data;
                },
            error: function (err, status) {
                console.log(err);
            }
        });
        return response;
        //return $http.post(ZambaWebRestApiURL + '/search/Indexs', SelectedDoctypesIds).then(function (response) {
        //    return response;
        //});
    }
    //Traigo indices para las entidades seleccionadas, de manera sync
    fac.GetAllSync = function (SelectedDoctypesIds) {

        var result = false;

        $.ajax({
            type: "POST",
            url: ZambaWebRestApiURL + '/search/Indexs',
            data: JSON.stringify(SelectedDoctypesIds),
            contentType: "application/json; charset=utf-8",
            async: false,
            success:
                function (data, status, headers, config) {
                    result = data;
                },
            error:
                function (error) {
                    result = error;
                }

        });
        return result;
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

var searchtreeview;

function LoadSearchTreeView() {

    try {
        if (window.localStorage) {
            var localTreeData = window.localStorage.getItem('localTreeData|' + GetUID());
            if (localTreeData != undefined && localTreeData != null && localTreeData != '') {
                try {
                    var treeData = JSON.parse(localTreeData);
                    LoadTree(treeData);
                } catch (e) {
                    console.error(e);
                    LoadTreeFromDB();
                }
            }
            else {
                LoadTreeFromDB();
            }
        }
        else {
            LoadTreeFromDB();
        }
    }
    catch (e) {
        console.error(e);
    }
}

function LoadTreeFromDB() {
    var genericRequest = {
        UserId: parseInt(GetUID())
    }

    $.ajax({
        type: "POST",
        url: serviceBase + '/Search/GetEntitiesTree',
        data: JSON.stringify(genericRequest),
        contentType: "application/json; charset=utf-8",
        async: false,
        success:
            function (data, status, headers, config) {
                if (data != undefined) {

                    try {
                        if (data === "") {
                            //$("#SearchControl").css('display', 'none');
                            var msj = $('<p/>', { text: "No hay datos para mostrar" }).appendTo("#tabsearch");
                            msj.css({
                                'width': '50%',
                                'text-align': 'center',
                                'margin': 'auto',
                                'padding': '50px',
                                'font-size': '20px',
                                'font-weight': 'bold'
                            });
                            return;
                        }
                        var kdata = $.parseJSON(data);
                        LoadTree(kdata);
                        //try {
                        //    if (window.localStorage) {
                        //        window.localStorage.setItem('localTreeData|' + GetUID(), data);
                        //    }
                        //}
                        //catch (e) {
                        //    console.error(e);
                        //    if (e.message.indexOf('exceeded the quota') != -1) {
                        //        window.localStorage.clear();
                        //    }

                        //}
                    }
                    catch (e) {
                        console.error(e);
                    }
                }
            },
        error: function (err, status) {
            console.log(err);

        }
    });


};

function GetComplete(data) {
    if (data.d === "") {
        //$("#SearchControl").css('display', 'none');
        var msj = $('<p/>', { text: "No hay datos para mostrar" }).appendTo("#tabsearch");
        msj.css({
            'width': '50%',
            'text-align': 'center',
            'margin': 'auto',
            'padding': '50px',
            'font-size': '20px',
            'font-weight': 'bold'
        });
        return;
    }
    var kdata = $.parseJSON(data.d);
    LoadTree(kdata);
    //try {
    //    if (window.localStorage) {
    //        window.localStorage.setItem('localTreeData|' + GetUID(), data.d);
    //    }
    //}
    //catch (e) {
    //    console.error(e);
    //    if (e.message.indexOf('exceeded the quota') != -1) {
    //        window.localStorage.clear();
    //    }

    //}
}

function resizeGrid() {
    var gridElement = $("#Kgrid");
    var newHeight = $('#Kgrid').outerHeight(true) - $('#KendoGridButtons').outerHeight(true);
    var otherElements = gridElement.children().not(".k-grid-content");
    var otherElementsHeight = 0;

    otherElements.each(function () {
        otherElementsHeight += $(this).outerHeight();
    });
    var currentHeight = newHeight - otherElementsHeight;
    if (currentHeight < 100)
        currentHeight = 100;
    gridElement.children(".k-grid-content").height(currentHeight);

}

function LoadTree(kdata) {
    for (i = 0; i < kdata[0].items.length; i++) {
        var valor = kdata[0].items[i].checked;
        var tipo = typeof (valor);
        if (tipo == "string") {
            if (valor == "true") {
                kdata[0].items[i].checked = true;
            }
            else {
                kdata[0].items[i].checked = false;
            }
        }
        else {
            if (valor == false) {
                kdata[0].items[i].checked = false;
            }
            else {
                kdata[0].items[i].checked = true;
            }
        }
    }

    searchtreeview = $("#treeview").kendoTreeView({
        check: onCheck,
        checkboxes: {
            template: "<input type='checkbox' name='#= item.id #' value='#= item.id #' #if(item.checked == true){# checked class='expanded' #}#/>",
            checkChildren: true
        },
        //dataBound: onDataBound,
        dataSource: kdata,
        scrollable: true,
    });
    // Default to expanded.
    var searchtreeview = $("#treeview").data("kendoTreeView");
    searchtreeview.expand(".expanded");
    searchtreeview.expand(".k-item");
    searchtreeview.expand(".k-first");
    onCheck();
}

function GetError(e) {
    console.log("Error: " + e.responseText + e.status + e.error);
}

function SaveChecksOnLocalStorage() {

    if (window.localStorage) {
        var treeView = $("#treeview").data("kendoTreeView"),
            nodeToLocalStorage = treeView.dataSource.options.data;
        var localTreeData = JSON.stringify(nodeToLocalStorage);
        try {
            if (localTreeData != undefined && localTreeData != null && localTreeData != '') {
                var nodes = $("#treeview").data("kendoTreeView").dataSource.view();
                for (i = 0; i < nodes[0].items.length; i++) {
                    var tipo = typeof (nodes[0].items[i].checked);
                    if (tipo == "string") {
                        if (nodes[0].items[i].checked == "true") {
                            nodeToLocalStorage[0].items[i].checked = true;
                        }
                        else {
                            nodeToLocalStorage[0].items[i].checked = false;
                        }
                    }
                    else {
                        if (nodes[0].items[i].checked == false) {
                            nodeToLocalStorage[0].items[i].checked = false;
                        }
                        else {
                            nodeToLocalStorage[0].items[i].checked = true;
                        }
                    }
                }
                window.localStorage.setItem('localTreeData|' + GetUID(), JSON.stringify(nodeToLocalStorage));
            }
        }
        catch (e) {
            console.error(e);
            if (e.message.indexOf('exceeded the quota') != -1) {
                window.localStorage.clear();
            }
        }
    }
}

function BroadcastlocalTreeDataLoaded() {
    if (window.localStorage) {
        var treeView = $("#treeview").data("kendoTreeView"),
            nodeToLocalStorage = treeView.dataSource.options.data;
        var localTreeData = JSON.stringify(nodeToLocalStorage);
        try {
            if (localTreeData != undefined && localTreeData != null && localTreeData != '') {
                var nodes = $("#treeview").data("kendoTreeView").dataSource.view();
                for (i = 0; i < nodes[0].items.length; i++) {
                    var tipo = typeof (nodes[0].items[i].checked);
                    if (tipo == "string") {
                        if (nodes[0].items[i].checked == "true") {
                            nodeToLocalStorage[0].items[i].checked = true;
                        }
                        else {
                            nodeToLocalStorage[0].items[i].checked = false;
                        }
                    }
                    else {
                        if (nodes[0].items[i].checked == false) {
                            nodeToLocalStorage[0].items[i].checked = false;
                        }
                        else {
                            nodeToLocalStorage[0].items[i].checked = true;
                        }
                    }
                }
                var scope = angular.element(document.getElementById("EntitiesCtrl")).scope();
                if (scope != undefined) {
                    scope.$broadcast('localTreeDataLoaded', nodeToLocalStorage);
                }
            }
        }
        catch (e) {
            console.error(e);
            if (e.message.indexOf('exceeded the quota') != -1) {
                window.localStorage.clear();
            }
        }
    }
}
/* show checked node IDs on datasource change */
function onCheck() {

    var checkedNodes = [];
    var DoctypesIds = [];
    var lastNodes = "";
    var hasGlobalSearchPermission = false;
    var mainController = angular.element($("#EntitiesCtrl")).scope();

    var nodes = $("#treeview").data("kendoTreeView").dataSource.view();
    checkedNodeIds(nodes, checkedNodes, DoctypesIds);
    //Si no encuentra checked true, oculta barra busqueda
    mainController.showSearchBtn = !(nodes[0].items
        .filter(item => item.checked).length == 0
    );

    for (var i = 0; i < DoctypesIds.length; i++) {
        //ObjectTypes.DocTypes = 2 , RightsType.GlobalSearch = 190
        hasGlobalSearchPermission = userHasRight(2, 190, DoctypesIds[i]);
        if (!hasGlobalSearchPermission)
            break;
    }

    if (hasGlobalSearchPermission) {
        $('.advancedSearchBox').show();
    }
    else {
        $('.advancedSearchBox').hide();
    }

    if (checkedNodes.length > 0) {
        lastNodes = checkedNodes.join(",");
    }

    mainController.Search.lastSearchEntitiesNodes = lastNodes;

    SetLastNodes(lastNodes, DoctypesIds);

    BroadcastlocalTreeDataLoaded();

    ResizeMDDatePickers();
}

function userHasRight(ObjectType, RightType, docTypeId) {
    var permission = false;
    var UserId = GetUID();
    $.ajax({
        type: "POST",

        url: ZambaWebRestApiURL + '/Tasks/UserHasRight?' + jQuery.param({ userid: UserId, objectType: ObjectType, right: RightType, aditionalParam: docTypeId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            permission = response;
        },
        error: function (response) {
        }
    });
    return permission;
}

/* function that gathers IDs of checked nodes*/
function checkedNodeIds(nodes, checkedNodes, DoctypesIds) {
    //return;

    for (var i = 0; i < nodes.length; i++) {

        var node = nodes[i];

        if (node.checked == 'true' || node.checked == true) {

            checkedNodes.push(node.NodeType + "-" + node.id);

            if (node.NodeType == "Entity" && DoctypesIds.indexOf(node.id) == -1) {
                DoctypesIds.push(nodes[i].id);
            }
        }

        if (node.hasChildren) {
            checkedNodeIds(node.children.view(), checkedNodes, DoctypesIds);
        }
    }
}

function SetLastNodes(lastnodes, DoctypesIds) {
    var scope = angular.element(document.getElementById("EntitiesCtrl")).scope();
    if (scope != undefined) {
        scope.updateSelectedEntities(DoctypesIds, lastnodes);
    }
}

function StoreNodesOnDB(lastNodes) {
    if (lastNodes == undefined) {
        return;
    }

    var data = {
        UserId: parseInt(GetUID()),
        Params:
        {
            "LastNodes": lastNodes
        }
    };
    $.ajax({
        type: 'POST',
        // dataType: 'json',
        url: ZambaWebRestApiURL + '/search/SetLastNodes',
        async: true,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8"
    });

}

function selectNodes(option) {
    var $in = $(".k-in").children();
    for (var i = 0; i <= $in.length; i++) {
        var $first = $($(".k-in")[i]).parents("li:first");
        if ($first.length > 0)
            $("#treeview").data("kendoTreeView").dataSource.getByUid($first.attr("data-uid")).set("checked", option);
    }

    onCheck();

    var $li = $first.find("li");
    for (var i = 0; i <= $li.length - 1; i++) {
        var uId = $($li[i]).attr("data-uid");
        $("#treeview").data("kendoTreeView").dataSource.getByUid(uId).set("checked", option);
    }
}

function expandAllNodes() {
    if (searchtreeview == undefined) searchtreeview = $("#treeview").data("kendoTreeView");
    if (searchtreeview == undefined) return;
    searchtreeview.expand(".k-item");
}

function collapseAllNodes() {
    if (searchtreeview == undefined) searchtreeview = $("#treeview").data("kendoTreeView");
    if (searchtreeview == undefined) return;

    for (var i = 0; i <= $(".k-item").length - 1; i++) {

        var item = $($(".k-item")[i]);
        if (item.attr("data-uid") == $(".k-item:first").attr("data-uid")) {
            $("#treeview").data("kendoTreeView").collapse($("#treeview").data("kendoTreeView").findByUid(item.attr("data-uid")));
        }

    }
}

function collapseNonSelectNodes() {
    //collapseAllNodes();
    var $inp = $(".k-checkbox").children();
    for (var i = 0; i <= $inp.length; i++) {
        var $thisInp = $($inp[i]);
        if ($thisInp.is(':checked')) {
            var p = $($thisInp.parents(".k-item")[1]).attr("data-uid");
            $("#treeview").data("kendoTreeView").expand($("#treeview").data("kendoTreeView").findByUid(p));
        }
    }
}

function formatDate(d) {
    return moment(d).format('DD MMM YYYY');
}

function onChange(arg) {
    var selected = $.map(this.select(), function (item) {
        var scope = angular.element(document.getElementById("ResultsCtrl")).scope();
        scope.$apply(function () {
            scope.setselected($(item).attr('data-uid'), $(item).attr('data-tid'), $(item).attr('data-name'));
        });
        return $(item);
    });

    console.log("Selected: " + selected.length + " item(s)");
}

//on dataBound event restore previous selected rows:
function onDataBound(e) {
    var view = this.dataSource.view();
    CheckIsSet(view, this);
}

function CheckIsSet(view, tree) {
    if (view.IsSet != undefined && view.IsSet == "true") {
        if (tree.tbody != undefined)
            tree.tbody.find("tr[data-uid='" + view.uid + "']")
                .addClass("k-state-selected")
                .find(".checkbox")
                .attr("checked", "checked");
    }

    for (var i = 0; i < view.length; i++) {
        var currentview = view[i];
        if (currentview.IsSet == "true") {
            tree.tbody.find("tr[data-uid='" + currentview.uid + "']")
                .addClass("k-state-selected")
                .find(".checkbox")
                .attr("checked", "checked");
        }
        for (var x = 0; x < currentview.children._view.length; x++) {
            var newview = currentview.children._view[x];
            CheckIsSet(newview, tree);
        }
    }
}

function IframeautoResize(id) {
    try {

        var newheight;
        var newwidth;

        if (document.getElementById) {
            newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
            newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
        }

        document.getElementById(id).height = (newheight) + "px";
        document.getElementById(id).width = (newwidth) + "px";

    } catch (e) {
        console.error(e);
    }

}

try {

    $(".affix").affix({ offset: { top: $("resultsDiv").outerHeight(true) } });
} catch (e) {
    console.error(e);
}


function ResizeButtonsSearch() {
    var ButtonSearch = $("#btnbusqueda");

    var GoBackToSearchResultsBtn = $("#GoBackToSearchResultsBtn");
    var ancho = document.documentElement.clientWidth;
    if (GoBackToSearchResultsBtn.css('display') == "inline-block") {
        ButtonSearch.css('width', ((380 * ancho) / 1908).toString() + "px");
    }
    else {
        ButtonSearch.css('width', ((420 * ancho) / 1908).toString() + "px");
    }
}
//Configuracion sobre seleccion de nodos
$(document).ready(function () {
    ZambaVersion = getValueFromWebConfig("ZambaVersion");
    $("#ZambaVersionSearch")[0].innerText = ZambaVersion;
    $(window).resize(function (e) {
        ResizeButtonsSearch();
        //  ResizeCurrentTab($("#contentTabhomeMain"));
        //ResizeMDDatePickers();
    });


    $("#OpenAllSelected").prop("disabled", true);




    $(".k-in").off();
    $(".k-checkbox").off();
    //al seleccionar texto que seleccione tambien checkbox
    $("body").on("click", ".k-in", function (evt) {
        $(this).parent().find("input").click();
        ResizeMDDatePickers();
        evt.stopImmediatePropagation();
        //$(this).parent().find("input").prop("checked");
    });
    //Si selecciona el padre todos los hijos quedan seleccionados
    $("body").on("click", ".k-checkbox", function () {
        var dS = $("#treeview").data("kendoTreeView").dataSource;
        if ($(this).parent().children(".k-icon").length) {
            var chk = $(this).children().prop("checked");
            var $li = $(this).parents("li:first").find("li");
            for (var i = 0; i <= $li.length - 1; i++) {
                var uId = $($li[i]).attr("data-uid");
                dS.getByUid(uId).set("checked", chk);
            }
            //Muestra/oculta listas hijas
            $(this).parent().parent().children("ul").css("display", chk ? "block" : "none");
        }
        //Si selecciono un hijo que el padre tambien se seleccione
        var thisVal = $(this).children().prop("checked");
        var thisParent = $($(this).parents("li")[1]);
        var firstItem = $($("#treeview").find("li")[0]);
        if (thisVal) {
            if (thisParent.length) dS.getByUid(thisParent.attr("data-uid")).set("checked", true);//Marco el padre
            dS.getByUid(firstItem.attr("data-uid")).set("checked", true);//Marco el nodo principal
        }
        else
            //if (!thisParent.children("ul").find("input").prop("checked")) //Pregunto si todos los hijos estan destildados
            //    if (dS.getByUid(thisParent.attr("data-uid")) != undefined) dS.getByUid(thisParent.attr("data-uid")).set("checked", false);//Desmarco el padre
            //Si no hay ninguno seleccionado que desmarque el principal
            if (!firstItem.children("ul").find("input").is(':checked')) {
                dS.getByUid(firstItem.attr("data-uid")).set("checked", false);
                if (!thisParent.children("ul").find("input").prop("checked"))
                    if (dS.getByUid(thisParent.attr("data-uid")) != undefined) dS.getByUid(thisParent.attr("data-uid")).set("checked", false);
            }
    });

    $("body").on("keyup", "#filterText", function () {
        var filterText = $("#filterText").val();
        var treeViewId = "#treeview";
        var tv = $(treeViewId).data('kendoTreeView');
        $(treeViewId + ' li.k-item').show();

        $('span.k-in > span.highlight').each(function () {
            var $icon = $(this).parent().children(".glyphicon");
            var $tP = $(this).parent();
            var txt = $tP.text();
            $tP.html("");
            $icon.appendTo($tP);
            $tP.html($tP.html() + txt);

        });

        // ignore if no search term
        if ($.trim($(this).val()) === '') {
            return;
        }

        var term = this.value.toUpperCase();
        var tlen = term.length;

        $(treeViewId + ' span.k-in').each(function (index) {
            var text = $(this).text();
            var $span = $(this).children("span");
            var html = '';
            var q = 0;
            var p;

            while ((p = text.toUpperCase().indexOf(term, q)) >= 0) {
                html += text.substring(q, p) + '<span class="highlight">' + text.substr(p, tlen) + '</span>';
                q = p + tlen;
            }

            if (q > 0) {
                html += text.substring(q);
                $(this).html("");
                $span.appendTo($(this));
                $(this).html($(this).html() + html);

                $(this).parentsUntil('.k-treeview').filter('.k-item').each(function (index, element) {
                    tv.expand($(this));
                    $(this).data('SearchTerm', term);
                });
            }
        });
        $(treeViewId + ' li.k-item:not(:has(".highlight"))').hide()
    });

    //reloadBootstrap();

    $("body").on("click", "#btnTabSearch", function (event) {
        collapseAllNodes();
        event.preventDefault();
        event.stopImmediatePropagation();
        //collapseNonSelectNodes();
        $("#showSelectNodes").click();
        $(".btn.btn-xs.btnBlue").tooltip();
    });

    $("#expandAllNodes").click(function (e) {
        expandAllNodes();
        //e.preventDefault();
        //e.stopImmediatePropagation();
    });

    $("#collapseAllNodes").click(function (e) {
        //e.preventDefault();
        //e.stopImmediatePropagation();
        collapseAllNodes();
    });

    $("#selectAllNodes").click(function (e) {
        //e.preventDefault();
        //e.stopImmediatePropagation();
        selectNodes(true);
        if ($('.smart-form.ng-pristine.ng-valid').children().children().length == 0) {
            $('#barratop').css('display', 'none');
            ;
        }
    });
    $("#unSelectNodes").click(function (e) {
        //e.preventDefault();
        //e.stopImmediatePropagation();
        var searchtreeview = $("#treeview").data("kendoTreeView");
        selectNodes(false);
    });

    $("#showSelectNodes").click(function (e) {
        //e.preventDefault();
        //e.stopImmediatePropagation();
        collapseNonSelectNodes();
    });



    collapseNonSelectNodes();

    $('body').tooltip({ selector: '.ngtitle' });

    setTabSearchSize();
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
    $(document).ready(function () {
        timerResizeButtonSearch = setInterval(function () {
            clearInterval(timerResizeButtonSearch);
            ResizeButtonsSearch();
        }, 500);
    });
});

function backToTopFn() {
    if ($('#back-to-top').length) {
        var scrollTrigger = 100, // px
            backToTop = function () {
                var scrollTop = $("#tabresults").scrollTop();
                if (scrollTop > scrollTrigger) {
                    $('#back-to-top').addClass('show');
                    $('#back-to-top').tooltip();
                } else {
                    $('#back-to-top').removeClass('show');
                }
            };
        backToTop();
        $("#tabresults").on('scroll', function () {
            backToTop();
            $('#back-to-top').tooltip();
        });
        $('body').on('click', '#back-to-top', function (e) {
            e.preventDefault();
            $("#tabresults").animate({
                scrollTop: 0
            }, 500);
        });
    }
}

$(function () {//Para que cambie el texto del dropdown al seleccionarse
    $('body').on('click', '.dropdown-menu li a', function () {
        if ($(this).parents("div:first").attr("changeText") == "true") {
            $(this).parents("div:first").find(".btn:first-child").text($(this).text());
            $(this).parents("div:first").find(".btn:first-child").val($(this).text());
        }
    });
});

function GoToUpGlobalSearch() {
    $('body').tooltip({ selector: '.ngtitle' });
    if ($('#back-to-top').length) {
        var scrollTrigger = 100, // px
            backToTop = function () {
                var scrollTop = $("#tabresults").scrollTop();
                if (scrollTop > scrollTrigger) {
                    $('#back-to-top').addClass('show');
                    $('#back-to-top').tooltip();
                } else {
                    $('#back-to-top').removeClass('show');
                }
            };
        backToTop();
        $("#tabresults").on('scroll', function () {
            backToTop();
            $('#back-to-top').tooltip();
        });
        $('body').on('click', '#back-to-top', function (e) {
            e.preventDefault();
            $("#tabresults").animate({
                scrollTop: 0
            }, 500);
        });
    }
}




var enableGlobalSearch = "true", zambaApplication = "ZambaSearch";



var SearchConfig = {
    IsSearchConfig: function () {
        return true;
    },
    UserId: function () {
        return GetUID();
    }
};



$(document).ready(function () {
    window.localStorage.setItem("MultiSelectionIsActive", false);
    window.addEventListener('resize', function (event) {

        var prevButton = $("md-prev-button")[0];
        var nextButton = $("md-next-button")[0];
        if (prevButton == undefined)
            return;
        prevButton = prevButton.childNodes[1].childNodes[0].childNodes[0].childNodes[0];
        if (prevButton) {
            prevButton.parentNode.removeChild(prevButton);
            nextButton = nextButton.childNodes[1].childNodes[0].childNodes[0].childNodes[0];
            nextButton.parentNode.removeChild(nextButton);
        }
    }, true);
});


function LoadMyTasksCount(element) {

    var scope = angular.element(element).scope();

    if (scope !== null && scope != undefined) {
        $.ajax({
            type: 'GET',
            url: ZambaWebRestApiURL + '/search/GetMyUnreadTasksCount?currentUserId=' + GetUID(),
            async: true,
            //data: { currentUserId: GetUID() },
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //                scope.$apply();
                if (scope.MyUnreadTasks != data && data != 0) {
                    scope.MyUnreadTasks = data;
                    //setInterval(function () {
                    var actualizada = toastr.info("Se ha agregado una nueva tarea");
                    toastr.options.timeOut = 3000;
                    // }, 300000);
                }
            }
        });
    }

}

function GetGroupsIdsByUserId(id) {

    var groups = [];
    try {
        if (window.localStorage) {
            var localGroups = window.localStorage.getItem("localGroups" + GetUID());
            if (localGroups != undefined && localGroups != null && localGroups.length > 0) {
                groups = localGroups;
                return groups;
            }
        }
    } catch (e) {
        console.error(e);
    }

    if (groups.length == 0) {


        $.ajax({
            type: 'GET',
            async: false,
            url: ZambaWebRestApiURL + '/search/GetGroupsByUserIds',
            data: { usrID: id },
            success: function (data) {
                if (data != undefined && data != null) {
                    groups = data;
                    try {
                        if (window.localStorage) {
                            window.localStorage.setItem("localGroups" + GetUID(), data);
                        }
                    } catch (e) {
                        console.error(e);
                    }
                }

            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    }

    return groups;
}








function updateDisplay(checkbox) {
    try {
        $parent = $(checkbox).parent();
        $button = $parent.find('button');
        $checkbox = checkbox;
        color = $button.data('color');
        settings = {
            on: {
                icon: 'fa fa-check'
            },
            off: {
                icon: 'fa fa-square-o'
            }
        };

        var isChecked = checkbox.is(':checked');

        // Update the button's color
        if (isChecked) {
            $button
                .removeClass('md-btn-basic')
                .addClass('md-btn-' + color + ' active');
        }
        else {
            $button
                .removeClass('md-btn-' + color + ' active')
                .addClass('md-btn-basic');
        }

        // Set the button's state
        $button.data('state', (isChecked) ? "on" : "off");

        // Set the button's icon
        $button.find('.state-icon')
            .removeClass()
            .addClass('state-icon ' + settings[$button.data('state')].icon);

    } catch (e) {
        console.error(e);
    }

}


function UpdateInputsForProcess() {

    $("#chkMyTasks").prop('checked', false);
    $("#chkMyTeam").prop('checked', false);
    $("#chkMyAllTeam").prop('checked', false);

    $checkbox = $("#chkViewAllMy");
    $checkbox.prop('checked', !$checkbox.is(':checked'));
    $checkbox.triggerHandler('change');

    $checkbox = $("#chkMyTasks");
    $checkbox.prop('checked', false);
    $checkbox.triggerHandler('change');

    $checkbox = $("#chkMyTeam");
    $checkbox.prop('checked', false);
    $checkbox.triggerHandler('change');

    $checkbox = $("#chkMyAllTeam");
    $checkbox.prop('checked', false);
    $checkbox.triggerHandler('change');
    updateDisplay($checkbox)



};



function CleanCache() {
    if (window.localStorage) {
        window.localStorage.clear();
    }
    //window.reload(true);
    window.location.reload(true);

}
function CleanAllCache() {
    if (window.localStorage) {
        window.localStorage.clear();
    }


    window.location.reload(true);

}


function setInsertIframeUrl() {
    $('#insertIframe').attr('src', "../../content/Images/loading.gif");
    $('#insertIframe').attr('src', "../../Views/Insert/Insert.aspx?userid=" + GetUID() + "&embedded='true'&InsertView=Main");
}




// Funcion para el elemento ayuda (Menu superior derecho)
function AyudaSearch() {
    window.open('../../forms/Boston/manuales/Manual_Usuario_Zamba_Web.pdf', '_blank');

}

//#region Thumb buttons - Start

function thumbContainerResize(_this) {
    var thumbContainer = $(_this).parents(".resultsGrid");
    var thumb = $(_this).parent().parent().children(".document-photo-thumbs");
    var $detailsButton = $($(_this).parents(".resultsGrid")[0]).find(".glyphicon-info-sign");
    var $zoomButton = $($(_this).parents(".resultsGrid")[0]).find(".glyphicon-zoom-in");
    var changeSizeButton;


    if (thumbContainer.css("border-color") == "rgb(51, 122, 183)") {
        $detailsButton.show();
        $zoomButton.show();
        changeSizeButton = $($(_this).parents(".resultsGrid")[0]).find(".glyphicon-ok-sign");
        changeSizeButton.addClass("glyphicon glyphicon-ok-circle");
        changeSizeButton.removeClass("glyphicon-ok-sign");
        //thumbContainer.css("width", "150px");
        thumbContainer.css("border", "1px solid #ddd");
    } else {
        $detailsButton.hide();
        $zoomButton.hide();
        // $("#resultsGridSearchBoxThumbs").find(".document-photo-thumbs").attr("onclick", "showSeletionMode(this)");
        changeSizeButton = $($(_this).parents(".resultsGrid")[0]).find(".glyphicon-ok-circle");
        changeSizeButton.addClass("glyphicon glyphicon-ok-sign");
        changeSizeButton.removeClass("glyphicon-ok-circle");
        //thumbContainer.css("width", "130px");
        thumbContainer.css("border", "4px solid #337ab7");
    }
}

function thumbButtonDisplay() {
    var thumbsCollection = $("#resultsGridSearchBoxThumbs").find(".glyphicon-ok-sign");

    if (thumbsCollection.length > 0) {
        $("#resultsGridSearchBoxThumbs").find(".glyphicon-info-sign").hide();
        $("#resultsGridSearchBoxThumbs").find(".glyphicon-zoom-in").hide();
        $("#multipleSelectionMenu").fadeIn();
    } else {
        $("#resultsGridSearchBoxThumbs").find(".glyphicon-info-sign").show();
        $("#resultsGridSearchBoxThumbs").find(".glyphicon-zoom-in").show();
        $("#multipleSelectionMenu").fadeOut();
    }
}

function thumbPreviewButtonDisplay() {
    var thumbsCollection = $("#resultsGridSearchBoxPreview").find(".glyphicon-ok-sign");

    if (thumbsCollection.length > 0) {
        $("#resultsGridSearchBoxPreview").find(".glyphicon-download-alt").hide();
        $("#resultsGridSearchBoxPreview").find(".glyphicon-new-window").hide();
        $("#multipleSelectionPreview").fadeIn();
    } else {
        $("#resultsGridSearchBoxPreview").find(".glyphicon-download-alt").show();
        $("#resultsGridSearchBoxPreview").find(".glyphicon-new-window").show();
        $("#multipleSelectionPreview").fadeOut();
    }
}


function showSeletionModeByimage(_this) {
    var thumbsCollection = $("#resultsGridSearchBoxThumbs").find(".glyphicon-ok-sign");
    if (thumbsCollection.length > 0) {
        thumbContainerResize(_this);
        thumbButtonDisplay();
    }
}

function thumbZoom(t) {
    var rg = $(t).parents(".resultsGrid");
    var dpt = $(t).parent().parent().children(".document-photo-thumbs");
    var $detailsButton = $($(t).parents(".resultsGrid")[0]).find(".glyphicon-info-sign");
    var $selectionButton = $($(t).parents(".resultsGrid")[0]).find(".glyphicon-ok-circle");
    if ($(t).attr("mode") === "normal") {

        $detailsButton.hide();
        $selectionButton.hide();
        $(t).parent().css("top", "-12px");
        $(t).attr("class", "glyphicon glyphicon glyphicon-remove");
        $(".resultsGrid.ng-scope").hide();
        $(t).attr({ "mode": "zoom", "src": "../../GlobalSearch/Images/close.png" });

        //se agrego ya que al tener una resolucion chica aparece un scroll que no debe aparecer
        $("#resultsGridSearchBoxThumbs").css("overflow-y", "hidden")

        rg.css("max-width", "350px").show();
        dpt.css("max-height", "480px").show();
        rg.animate({ width: "280px" }, 300);
        dpt.animate({ width: "450px" }, 300);
    }
    else {
        $detailsButton.show();
        $selectionButton.show();
        $(t).parent().css("top", "auto");
        $(t).attr("class", "glyphicon glyphicon-zoom-in");
        $(".resultsGrid.ng-scope").show();
        $(t).attr({ "mode": "normal", "src": "../../GlobalSearch/Images/word.png" });
        rg.css("max-width", "8.5%");
        dpt.css("max-height", "225px");
        dpt.animate({ width: "100%" }, 300);

        $("#resultsGridSearchBoxThumbs").css("overflow-y", "visible");

    }
}

function ShowThumbInfoGS(_this) {
    var $zoomButton = $($(_this).parents(".resultsGrid")[0]).find(".glyphicon-zoom-in");
    var $selectionButton = $($(_this).parents(".resultsGrid")[0]).find(".glyphicon-ok-circle");
    var $showDiv = $($(_this).parents(".resultsGrid")[0]).find("#ShowThumbDetails");
    var iconSpan = _this;

    if ($showDiv.css("display") == "none") {
        $zoomButton.hide();
        $selectionButton.hide();

        if ($(_this).hasClass('glyphicon-info-sign') === false) {
            iconSpan = $(_this).find('.glyphicon-info-sign');
        }
        $(iconSpan).attr("class", "glyphicon glyphicon glyphicon-remove");
        $showDiv.fadeIn().css("display", "table");

        if ($(_this).find('.document-name-preview').length) {
            $(_this).find('.document-name-preview').hide();
        }
    }
    else {
        $showDiv.fadeOut();
        if ($(_this).hasClass('glyphicon-remove') === false) {
            iconSpan = $(_this).find('.glyphicon-remove');
        }
        $(iconSpan).attr("class", "glyphicon glyphicon-info-sign");
        $zoomButton.show();
        $selectionButton.show();

        if ($(_this).find('.document-name-preview').length) {
            $(_this).find('.document-name-preview').show();
        }
    }
}

//#endregion THUMBS



function GetDocIdFromList(list) {
    var docIds = [];
    let checkedIds = countTaskIdSelected();
    for (var item in checkedIds) {
        if (checkedIds[item].Docid != undefined)
            docIds.push(checkedIds[item].Docid);
    }
    return docIds;
}

//Functions del modal para la exportación a Excel
function enableQuantityField() {
    $("#errorMessageModalExcelExport").hide();
    $("#quantityToExport").show();
}

function disableQuantityField() {
    $("#errorMessageModalExcelExport").hide();
    $("#quantityToExport").val("");
    $("#quantityToExport").hide();
}

function toogleMyModal() {
    $("#errorMessageModalExcelExport").hide();
    $("#myModal").modal('toggle');
}
function showErrorMessageForModalExcelExport() {
    if ($("#spinnerExportExcel") != null && $("#btnExportar") != null) {
        $("#spinnerExportExcel").hide();
        $("#btnExportar").show();
    }
    $("#errorMessageModalExcelExport").show();
}
function validateModalInputData() {
    try {
        $("#errorMessageModalExcelExport").hide();
        var value = $("#quantityToExport").val();
        if (value != undefined && value != "") {
            parsedValue = parseInt(value);
            if (parsedValue == undefined || parsedValue == "") {
                showErrorMessageForModalExcelExport();
            }
            else {
                if (parsedValue != 0) {
                    var scopeCtrl = angular.element(document.getElementById("EntitiesCtrl")).scope();
                    scopeCtrl.ExportResultsGrid_ToExcel(parsedValue);
                }
            }
        }
        else {
            showErrorMessageForModalExcelExport();
        }
    } catch (e) {
        console.error(e);
        showErrorMessageForModalExcelExport();
    }
}

function executeExportToExcelBySelectedOption() {
    if ($("#spinnerExportExcel") != null && $("#btnExportar") != null) {
        $("#spinnerExportExcel").show();
        $("#btnExportar").hide();
    }
    setTimeout(exportToExcelBySelectedOption, 3000);

}

function exportToExcelBySelectedOption() {
    $("#errorMessageModalExcelExport").hide();

    if ($("#radio-elegircantidad").is(":checked")) {
        validateModalInputData();
    }
    else {
        var scopeCtrl = angular.element(document.getElementById("EntitiesCtrl")).scope();
        scopeCtrl.ExportResultsGrid_ToExcel(scopeCtrl.Search.SearchResultsObject.total);
    }
}


app.filter('toid', function () {
    return function (value) {

        var newValue = value;

        try {
            //todo minuscula
            newValue = (!newValue) ? '' : newValue.toLowerCase();
            //espacios por guion medio
            newValue = (!newValue) ? '' : newValue.replace(/ /g, '-');
        } catch (e) {
            console.error(e);
            newValue = value;
        }

        return newValue;
    };

});


function ViewUpdatDisplayJS(id, reportId) {

    var scope = angular.element(document.getElementById("EntitiesCtrl")).scope();
    scope.handleViewCheckMultipleId(id, reportId);

}



function searchModeGSFn(_this, mode) {
    var ResultsCtrlScope = angular.element($("#EntitiesCtrl")).scope();

    ResultsCtrlScope.searchModeGSFn(_this, mode);
}


function ExecutePostLoginActions() {
    var ResultsCtrlScope = angular.element($("#EntitiesCtrl")).scope();
    ResultsCtrlScope.currentModeSearch = 'search';
}
