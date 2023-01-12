<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocViewer.aspx.cs" Inherits="DocViewer"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Browser de Documentos</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" Text="" ID="lblDoc"></asp:Label>
    </div>
    <div id="formBrowser" runat="server" height="90%" width="100%"></div>
    </form>
</body>
</html>
