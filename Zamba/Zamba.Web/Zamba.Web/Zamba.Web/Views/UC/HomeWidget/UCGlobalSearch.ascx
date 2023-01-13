<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGlobalSearch.ascx.cs" Inherits="Zamba.Web.Views.UC.HomeWidget.UCGlobalSearch" %>

<link rel="stylesheet" type="text/css" href="../../Scripts/ng-embed/ng-embed.min.css" />
<link rel="stylesheet" type="text/css" href="../../GlobalSearch/search/searchbox.css" />
<link rel="stylesheet" type="text/css" href="../../Content/partialSearchIndexs.css" />

<script src="../../Scripts/typeahead.bundle.js"></script>
<script src="../../Scripts/handlebars-v2.0.0.js"></script>
<script src="../../Scripts/underscore.min.js"></script>
<script src="../../Scripts/lodash.js"></script>
<script src="../../GlobalSearch/services/angular-local-storage.min.js"></script>
<script src="../../GlobalSearch/services/authInterceptorService.js"></script>
<script src="../../GlobalSearch/search/searchbox-loader.js"></script>
<script src="../../GlobalSearch/services/authService.js"></script>
<script src="../../GlobalSearch/search/searchbox.js"></script>
<script src="../../Scripts/app/_common/forms/directives/input/smartDatepicker.js"></script>
<script src="../../Scripts/app/search/zamba.search.js"></script>
<script src="../../GlobalSearch/scripts/zambasearch.js"></script>



<ul class="nav navbar-nav" style="width: 100%; padding-bottom: 10px;">
    <li id="Advfilter2" class="adv2" style="display: flex; width: 100%;">
        <span class="glyphicon glyphicon-filter favAdvSearch" title="Abrir busqueda guardada" style="display: none;"></span>
        <span class="glyphicon glyphicon-search" style="padding: 17px;"></span>
        <input class="advancedSearchBox form-control input-xs hidden-sm" style="height: 30px; float: right; width: 100% !important; margin-left: 0;" placeholder="Buscar..." />
        <a id="LupaSearch" class="visible-sm hidden-md" style="background: none;">
            <i class="fa fa-search fa-1x" style="color: white; position: relative" aria-hidden="true"></i>
        </a>
    </li>
</ul>

<div id="Advfilter1">
    <div id="Advfilter-modal-content" class="modal" style="display: none !important; overflow: hidden;" tabindex="-1" role="dialog">
        <div class="modal-dialog" style="width: 80%; margin: 0; padding: 10px; left: 110px; top: 40px;">

            <div data-ng-controller="appController" id="appController" style="float: inherit;">
                <div class="modal-content" style="min-height: 100px;">
                    <div class="modal-header" style="padding: 2px; background-color: white;">
                        <div class="GSTxtInModalHeader">
                        </div>
                        <div style="float: right;">
                            <a href="#" data-toggle="tooltip" style="margin-right: 5px; text-decoration: none" data-placement="bottom" class="glyphicon glyphicon-wrench remove-all-icon " id="advSearchConfig" onclick="showGSConfig();" title="Configuracion"></a>
                            <a href="#" data-toggle="tooltip" style="margin-right: 5px; text-decoration: none" data-placement="bottom" class="glyphicon glyphicon-question-sign remove-all-icon " id="advSearchHelp" onclick="showGSHelp();" title="Ayuda"></a>
                            <a href="#" data-toggle="tooltip" style="margin-right: 5px; text-decoration: none" data-placement="bottom" class="glyphicon glyphicon-remove " data-dismiss="modal" id="advSearchClose" title="Cerrar busqueda"></a>
                        </div>
                        <div class="filterCount dropdown" style="display: none;">
                            <button class="btn  btn-xs dropdown-toggle" style="color: white" type="button" id="filterCountDiv" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" value="25">
                                Traer <b>25</b> registros
                                    <span class="caret"></span>
                            </button>
                            <ul class="dropdown-menu" aria-labelledby="dropdownMenu1" style="z-index: 99999; font-size: 12px;">
                                <li><a href="#" data-value="10">Traer <b>10</b> registros</a></li>
                                <li><a href="#" data-value="25 action">Traer <b>25</b> registros</a></li>
                                <li><a href="#" data-value="50">Traer <b>50</b> registros</a></li>
                                <li><a href="#" data-value="100">Traer <b>100</b> registros</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="modal-body" style="padding: 0;">
                        <zamba-search id="zambasearchcontrol" ng-model="searchParams"
                            parameters="availableSearchParams"
                            placeholder="Buscar...">
                            </zamba-search>
                        <script>
                            function GetNextUrl() {
                                return angular.element($('#zambasearchcontrol')).scope().PreviewItem(0);
                            }

                        </script>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>
