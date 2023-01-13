<%@ Control Language="C#" AutoEventWireup="true" Inherits="ucTaskGrid" CodeBehind="ucTaskGrid.ascx.cs" %>
<%@ Register Src="~/Views/UC/Grid/ZGridView.ascx" TagName="ZGrid" TagPrefix="ZControls" %>
<%@ Register Src="../Grid/CustomFilterControl.ascx" TagName="ucTaskGridFilter" TagPrefix="uc1" %>


<nav runat="server" id="pnlFilters" class="navbar">
    <asp:HiddenField runat="server" ID="Userid" />
    <asp:HiddenField runat="server" ID="DocTypeIdDropHidden" />
    <div class="navbar-header">
        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#toolbarTabTasksList">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>
    </div>
    <a id="btnSidePanelTasksList" class="navbar-brand" href="#">
        <span id="imgBtnSidePanelTasksList" class="glyphicon glyphicon-menu-left btnToggleSidePanel"></span>
    </a>

   

    <div id="toolbarTabTasksList" class="collapse navbar-collapse">
        <div class="col-1 col-sm-4 col-lg-3" id="FiltrosHeader" runat="server" visible="true" style="padding-left:1px;">
            <div>
                <asp:DropDownList ID="cmbDocType" runat="server" Height="30px" Visible="true"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="CmbDocType_SelectedIndexChanged"
                    onchange="SelectedIndexChanged(this)" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <uc1:ucTaskGridFilter ID="ucTaskGridFilter" runat="server" />
        <%--    <asp:Panel ID="panelFilters" runat="server"></asp:Panel>--%>
    </div>




</nav>
<div style="text-align: center">
    <asp:Image ID="NotFound" runat="server" ImageUrl="../../../Content/Images/fullpix.png" Style="height: 150px; margin-top: 160px;" />
    <asp:Label runat="server" ID="NotFoundText" Style="margin-top: 60px;" Visible="false"><h4> No se encontraron Resultados.. </h4></asp:Label>

</div>

<div>
    <div id="TaskGridContent" class="gridContainer">
        <ZControls:ZGrid ID="grvTaskGrid" PagingButtonCount="1" runat="server" />
    </div>
</div>
<%--<div>
 <asp:dropdownlist id ="ddlComputedColumns" runat ="server"></ asp:dropdownlist >
</div>--%>

<script type="text/javascript">


    function SelectedIndexChanged(obj) {
        var id = obj.value;
        $("[id$=DocTypeIdDropHidden]").val(id);
    }


</script>
