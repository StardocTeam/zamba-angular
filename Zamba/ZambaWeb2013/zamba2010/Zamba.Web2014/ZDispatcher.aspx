<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZDispatcher.aspx.cs" Inherits="ZDispatcher" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html >
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />
    <title></title>

    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

</head>
<body>
    <form id="form1" runat="server">
        <div><asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" /></div>
    </form>
</body>
</html>
