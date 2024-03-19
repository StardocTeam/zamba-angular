<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginRRHH.aspx.cs" Inherits="Zamba.Web.Views.Security.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnAuthorizationData" runat="server" />
        <div>

        </div>
    </form>
    <script>
        var auth = document.getElementById('<%=hdnAuthorizationData.ClientID%>').value;
        localStorage.setItem("authorizationData", auth);
        localStorage.setItem("authorizationData", JSON.parse(auth).UserId);
    </script>
</body>
    
</html>
