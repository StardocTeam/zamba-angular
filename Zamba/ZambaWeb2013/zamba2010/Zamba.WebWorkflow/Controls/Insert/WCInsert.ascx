<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCInsert.ascx.cs" Inherits="Controls_Insert_WCInsert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register TagPrefix="ZControls" TagName="Office" Src="~/Controls/Insert/NewOffice/NewOfficeSelector.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Forms" Src="~/Controls/Insert/Forms/NewFormSelector.ascx" %>
<%@ Register TagPrefix="ZControls" TagName="Templates" Src="~/Controls/Insert/Templates/TemplatesListSelector.ascx" %>--%>

<asp:UpdatePanel ID="udp_WCinsert" runat="server" UpdateMode="Conditional">
<ContentTemplate>
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
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Seleccione un archivo: " Font-Names="Arial"
                ForeColor="#000066" Font-Size="X-Small" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:FileUpload ID="FileUpload1" runat="server" />           
            <div></div>
            <asp:Button ID="btInsertar" runat="server" Text="Visualizar Archivo" 
                ValidationGroup="Upload" />
        </td>
    </tr>
</table>
<%--<table>
    <tr>
        <td>
            <ZControls:Office runat="server" ID="OfficeSelector" />
        </td>
        <td>
            <ZControls:Forms runat="server" ID="FormsSelector" />
        </td>
    </tr>
    <tr>
        <td>
            <ZControls:Templates runat="server" ID="TemplatesSelector" />
        </td>
    </tr>
</table>--%>
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="btInsertar" />
</Triggers>
</asp:UpdatePanel>
