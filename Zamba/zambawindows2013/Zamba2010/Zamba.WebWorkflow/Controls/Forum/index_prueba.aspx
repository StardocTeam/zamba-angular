<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index_prueba.aspx.cs" Inherits="Controls_Forum_index_prueba" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="MyUC" TagName="ForumMessages" Src="~/Controls/Forum/WCForum.ascx" %>
<%@ Register TagPrefix="MyUC" TagName="NuevoTema" Src="~/Controls/Forum/WCForum.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <MyUC:ForumMessages runat="server" ID="ForumMessages"/>
    
    </div>
    </form>
</body>
</html>
