<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTaskGrid.ascx.cs" Inherits="ucTaskGrid" %>
<%@ Register Src="~/Views/UC/Grid/ZGridView.ascx" TagName="ZGrid" TagPrefix="ZControls" %>

<asp:Panel runat="server" ID="pnlFilters">
    <div class="wf_main_toolbar" id="FiltrosHeader" runat="server" visible="true">
        <div style="padding:5px">
            <asp:DropDownList ID="cmbDocType" runat="server" Width="200px" 
                AutoPostBack="True">
            </asp:DropDownList>
        </div>
    </div>
    <br /> 
</asp:Panel>   
<div style="text-align:center"><asp:Label runat="server" ID="lblinfo"></asp:Label></div>
<div id="TaskGridContent" class="gridContainer"  style="overflow: hidden">
    <ZControls:ZGrid ID="grvTaskGrid" PagingButtonCount="30"  runat="server"/>
</div>