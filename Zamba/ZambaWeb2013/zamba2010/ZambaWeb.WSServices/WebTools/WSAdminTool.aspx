<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WSAdminTool.aspx.cs" Inherits="ZambaWeb.WSServices.WebTools.WSAdminTool" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
    <div id="dvUTool">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="RadScriptManager1" runat="Server">
        </asp:ScriptManager>
        <telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" DecoratedControls="All" DecorationZoneID="decoratedZone" />
        <telerik:RadFormDecorator runat="server" ID="RadFormDecorator2" DecoratedControls="All" DecorationZoneID="decoratedZoneFiledset" />
        <div style="background-color:White">
            <telerik:RadFileExplorer runat="server" ID="FileExplorer1" Width="100%" Height="520px">      
            </telerik:RadFileExplorer>
        </div>
        <div>
            <br /><asp:Label ID="lblErrorMsg" runat="server"></asp:Label><br />
            <asp:TextBox runat="server" style="width:800px" ID="txtfilepath"></asp:TextBox>
            <asp:Button ID="Button1" text="Upload" runat="server" onclick="btnUp_Click" style="border-style:none"/>
            <asp:Label ID="lblMsg" runat="server"></asp:Label><br /></div>
    </form>
    </div>
</body>
</html>
