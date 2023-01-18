<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Tools_Default" Codebehind="AdminCleaner.aspx.cs" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <title>Admin Cleaner</title>
</head>
<body  style="background-color:White">
    <form id="form1" runat="server">
        <div>        
            <asp:CheckBox ID="chkCleanWF" runat="server" Text="Borrar cache de WorkFlows" /><br />
            <asp:CheckBox ID="chkCleanVolumes" runat="server" Text="Borrar cache de Volumenes" /><br />
            <asp:CheckBox ID="chkCleanForms" runat="server" Text="Borrar cache de Forms" /><br />
            <asp:CheckBox ID="chkCleanIndexs" runat="server" Text="Borrar cache de Atributos" /><br />
            <asp:CheckBox ID="chkCleanUsers" runat="server" Text="Borrar cache de Usuarios y Permisos" /><br />
            <asp:CheckBox ID="chkCleanDocTypes" runat="server" Text="Borrar cache de Entidades" /><br />
        </div>
        <br /><asp:Button ID="btnClean" runat="server" Text="Limpiar" onclick="btnClean_Click" />
        <asp:Button ID="btnCleanAll" runat="server" Text="Limpiar todo" onclick="btnCleanAll_Click" />
    </form>
</body>
</html>
