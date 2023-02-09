<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Views_UC_Grid_CustomFilterControl" CodeBehind="CustomFilterControl.ascx.cs" %>
<%@ Register Src="~/Views/UC/Grid/ZGridView.ascx" TagName="ZGrid" TagPrefix="ZControls" %>

<%--<asp:HiddenField ID="StepId" runat="server" />--%>




<script type="text/javascript">

    function ClearFilters() {
        $(".filtersfield").val("");
        return true;
    }
</script>

<style>
    .TaskInput {
        width: 270px !important;
        height: 30px;
    }

    #filterz {
        list-style-type: none;
        position: fixed;
        border-radius: 5px;
        background-color: white;
    }

        #filterz li a {
            margin-right: 15px;
            text-decoration: none;
        }
</style>

  
<div data-ng-controller="appFilterController" class="col-12 col-sm-6 col-lg-8">
    <asp:DropDownList CssClass="form-control col-xs-3" ID="IndexsDropdown" runat="server" Width="150px" Style="height: 30px" onchange="GetSelectedTextValue(this) " ng-change="FiltersEntitySelected()" ng-model="SelectedEntityId">
    </asp:DropDownList>

    <select id="DropVal" class="col-xs-1 form-control" style="width: 100px; height: 30px" onchange="getTextValue()">
        <option value="=">=</option>
        <option value=">">></option>
        <option value="<"><</option>
        <option value=">=">>=</option>
        <option value="<="><=</option>
        <option value="<>"><></option>
        <option value="No contiene">No contiene</option>
        <option value="contiene">Contiene</option>
    </select>



    <input id="CompareOpe" style="display: none" />
    <%--guardo el valor del comparador para despues tomarlo con jquery--%>
    <input id="IndexValue" style="display: none; height: 30px" />

    <input id="IdSubList" style="display: none; height: 30px" />
    <input id="SubListName" style="display: none; height: 30px" />
    <div class="col-xs-4 " style="height: 30px; display: none" id="InputData">
        <filter-index id="angularTemplate"></filter-index>
    </div>


    <%--Input del usuario--%>

    <button id="BtnAgregar" class="btn btn-default btn-sm" onclick="NewFilter()">Filtrar</button>

    <%--   boton muestra los filtros--%>

    <%--<div class="row">--%>

  <span id="ShowFilters" onclick="showfilters()" class="btn btn-default btn-sm">Mostrar Filtros <a href="#" id="counter"></a></span>
    <%--<span id="ShowFilters" onclick="showfilters()" class="btn btn-default btn-sm">Mostrar Filtros <a href="#" id="counter"></a></span>--%>
    <ul id="filterz" style="display: none"></ul>

   <%-- </div>--%>
    <%--  guardo los filtros--%>
</div>




<script src="../../Scripts/app/search/zamba.search.js?v=168"></script>


