<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocViewer.aspx.cs" Inherits="DocViewer"
    Title="Zamba Web Visualizador de Documentos" %>

<%@ Register Src="../../Controls/Core/WCIndexs.ascx" TagName="WCIndexs" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Visualizar Documento</title>
</head>
<body>

  <form id="form1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />    
    <div style="float: left;">
        <uc1:WCIndexs ID="WCIndexs1" runat="server" />
    </div>       
    
    <iframe id="WebBrowser" runat="server" 
        style="float:right;padding-left:5px;overflow:auto;width:72%; height: 633px;" 
        frameborder="0" />         
    
   </form>
   

   
</body>
</html>
