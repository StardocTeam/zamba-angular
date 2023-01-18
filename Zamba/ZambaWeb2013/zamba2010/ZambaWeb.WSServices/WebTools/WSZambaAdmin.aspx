<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WSZambaAdmin.aspx.cs" Inherits="ZambaWeb.WSServices.WebTools.WSZambaAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ZAMBA ADMIN</title>
    <script src="../../scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery-ui-1.8.6.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery.layout-latest.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ZQContainer_Div">            
        <div style="height: 61px">
            <asp:TextBox Width="100%" Rows="10" TextMode="MultiLine" Height="50px" id="txtQueryWritter" runat="server"></asp:TextBox>            
        </div>        
        <div style="border-style:solid; border: 1px;">            
            <asp:Label id="queryResult" runat="server"></asp:Label><br />
            <asp:GridView ID="gvResults" Width="100%" runat="server"> </asp:GridView>         
        </div>       
        <div>            
            <asp:RadioButton id="rdoExecuteQuery" groupname="querytype" runat="server"/><span>Ejecutar Consulta</span><br />
            <asp:RadioButton id="rdoReturnValues" groupname="querytype" runat="server"/><span>Devuelve Valores</span><br /><br />
                    
            <asp:Button ID="btnExecute" Text="EJECUTAR" runat="server" onclick="btnExecute_Click" />
            <asp:Button ID="btnCleanValues" Text="LIMPIAR VALORES" runat="server" onclick="btnCleanValues_Click" />
        </div>            
    </div>
    </form>
</body>
</html>
