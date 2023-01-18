<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplicationsPools.aspx.cs" Inherits="Zamba.AgentServer.Pages.ApplicationsPools" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>


    <asp:Label ID="Labelinfo" runat="server" Text="Label"></asp:Label>
    <telerik:RadGrid ID="RadGrid1" runat="server"  
        CellSpacing="0" GridLines="None"  
        onitemcommand="RadGrid1_ItemCommand">
<MasterTableView>
<Columns>
 <telerik:GridButtonColumn Text="Iniciar" CommandName="Iniciar">
                </telerik:GridButtonColumn>
 <telerik:GridButtonColumn Text="Parar" CommandName="Parar">
                </telerik:GridButtonColumn>
 <telerik:GridButtonColumn Text="Reciclar" CommandName="Reciclar">
                </telerik:GridButtonColumn>
</Columns>
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
    </telerik:RadGrid>


    <telerik:RadAjaxManager runat="server">
        <AjaxSettings>
            
             <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Labelinfo" />
                              <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>


</asp:Content>
