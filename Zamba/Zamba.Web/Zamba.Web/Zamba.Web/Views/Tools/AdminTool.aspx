<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Tools_AdminTool" Codebehind="AdminTool.aspx.cs" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html >
<head id="Head1" runat="server">

    <title>ADMIN TOOL</title>
</head><body>
    <div id="dvUTool">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="RadScriptManager1" runat="Server" ScriptMode="Release">
        </asp:ScriptManager>
        <telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" DecoratedControls="All" DecorationZoneID="decoratedZone" />
        <telerik:RadFormDecorator runat="server" ID="RadFormDecorator2" DecoratedControls="All" DecorationZoneID="decoratedZoneFiledset" />
        <div style="background-color:White">                      
            <telerik:RadFileExplorer runat="server" ID="FileExplorer1" Width="100%" Height="520px">      
            </telerik:RadFileExplorer>
        </div>
        <div>
            <br /><asp:Label ID="lblMsg" runat="server" style="color:white"></asp:Label><br />
            <asp:TextBox runat="server" style="width:800px" ID="txtfilepath"></asp:TextBox>
            <asp:Button text="Upload" runat="server" onclick="btnUp_Click" style="border-style:none; background-color:white"/></div>
           </form></div>
</body>
</html>