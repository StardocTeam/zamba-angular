<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintDocument.aspx.cs" Inherits="PrintDocument"
    Title="Zamba Web Visualizador de Documentos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Imprimir Documento</title>
</head>

<body onload="window.print();"> 
    <form id="form1" runat="server">
    <iframe id="WebBrowser" runat="server" style="float: left; padding-left: 5px; overflow: auto;
        width: 72%; height: 633px;" frameborder="0" />
    </form>
</body>
</html>
