<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSendMail.aspx.cs" Inherits="Zamba.Web.Test.SendMail.TestSendMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <button runat="server" style="width: 169px" onclick="Testmail" >Test Send Mail</button>
   <asp:LinkButton runat="server" Text="Insertar" ID="lnkInsertar" OnClick="Testmail"
                                    CssClass="btn btn-primary btn-sm" data-loading-text="<div style='display:inline-block' class='loader'></div><div style='display:inline-block; margin-left:10px'> Test Send Mail </div> "  />

        </div>
    </form>
</body>
</html>
