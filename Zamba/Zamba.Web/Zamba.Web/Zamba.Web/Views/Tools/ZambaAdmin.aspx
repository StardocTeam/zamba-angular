<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Tools_ZambaAdmin" Codebehind="ZambaAdmin.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <title>ZAMBA ADMIN</title>

    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

</head>
<body>
    <form id="form1" runat="server">
        <div id="ZQContainer_Div">
            <div style="height: 61px">
                <asp:TextBox Width="100%" Rows="10" TextMode="MultiLine" Height="50px" ID="txtQueryWritter" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="queryResult" runat="server" Style="background-color: White"></asp:Label><br />
                <asp:GridView ID="gvResults" Width="100%" runat="server" Style="background-color: White"></asp:GridView>
            </div>
            <div>
                <asp:RadioButton ID="rdoExecuteQuery" GroupName="querytype" runat="server" /><span>Ejecutar Consulta</span><br />
                <asp:RadioButton ID="rdoReturnValues" GroupName="querytype" runat="server" /><span>Devuelve Valores</span><br />
                <br />

                <asp:Button ID="btnExecute" Text="EJECUTAR" runat="server" OnClick="btnExecute_Click" />
                <asp:Button ID="btnCleanValues" Text="LIMPIAR VALORES" runat="server" OnClick="btnCleanValues_Click" />
            </div>
        </div>
    </form>
</body>
</html>
