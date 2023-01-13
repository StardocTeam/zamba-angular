<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskToExpire.aspx.cs" Inherits="UserControls_UCTaskToExpire_TaskToExpire" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/WfReports/UserControls/UCTaskToExpire/UCTaskToExpire.ascx" TagPrefix="MyUcs"
    TagName="UCTaskToExpire" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body style="margin:0px 0px 0px 0px">
    <form id="form1" runat="server" style="margin:0px 0px 0px 0px">
    <div style="margin:0px 0px 0px 0px">
    <MyUcs:UCTaskToExpire runat="server" ID="UCTaskToExpire" />
    </div>
    </form>
</body>
</html>
