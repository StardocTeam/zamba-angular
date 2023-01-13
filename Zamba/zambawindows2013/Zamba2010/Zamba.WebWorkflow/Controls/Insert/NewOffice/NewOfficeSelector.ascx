<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewOfficeSelector.ascx.cs"
    Inherits="Controls_Insert_NewOffice_NewOfficeSelector" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:UpdatePanel ID="udp_WCinsert" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:Panel ID="Panel1" runat="server" Width="280px">
<table>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Tipo de Documento: " Font-Names="Arial"
                ForeColor="Navy" Font-Size="X-Small"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="DocTypesDataSource"
                DataTextField="DOC_TYPE_NAME" DataValueField="DOC_TYPE_ID">
            </asp:DropDownList>
            <asp:ObjectDataSource ID="DocTypesDataSource" runat="server" SelectMethod="GetDocTypesIdsAndNamesSorted"
                TypeName="Zamba.Core.DocTypesBusiness"></asp:ObjectDataSource>
        </td>
    </tr>
</table>
    <div style="padding: 10px;">
        <div style="padding: 5px; border: solid black thin;">
            <table cellspacing="2">
                <tr>
                    <td>
                        <asp:ImageButton ID="Image1"  runat="server"  onclick="lnkNewWord_Click" ImageUrl="~/images/word.jpg" AlternateText="Documento de Microsoft Word"  Height="25px" Width="25px" />
                    </td>
                   
                    <td>
                    <asp:Label ID="lnkNewWord" runat="server" Text="Nuevo Documento de Word" 
                             Font-Names="Arial" Font-Size="X-Small" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="Image2"  runat="server" onclick="LnkNewExcel_Click" ImageUrl="~/images/excel.jpg" AlternateText="Documento de Microsoft Excel" Height="25px" Width="25px" />
                    </td>
                    <td>
                    <asp:Label ID="LnkNewExcel" runat="server" Text="Nuevo Documento de Excel" 
                             Font-Names="Arial" Font-Size="X-Small" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="Image3"  runat="server" onclick="LnkNewPowerPoint_Click" ImageUrl="~/images/powerpoint.jpg" AlternateText="Documento de Microsoft PowerPoint" Height="25px" Width="25px" />
                    </td>
                    <td>
                    <asp:Label ID="LnkNewPowerPoint" runat="server"  
                            Text="Nuevo Documento de PowerPoint"  Font-Names="Arial" Font-Size="X-Small" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
<ajaxToolKit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BehaviorID="RoundedCornersBehavior1"
    TargetControlID="Panel1" Radius="6" Corners="All" />
    
</asp:Panel>    
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="Image1" />
<asp:PostBackTrigger ControlID="Image2" />
<asp:PostBackTrigger ControlID="Image3" />
</Triggers>
</asp:UpdatePanel>
