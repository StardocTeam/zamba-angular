﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Controls/TaskInformation.ascx" TagPrefix="MyUcs" TagName="TaskInformation" %>
<%@ Register Src="~/Controls/TasksList.ascx" TagPrefix="MyUcs" TagName="TasksList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Request Action</title>
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="smMain" runat="server" />
        <div>
            <asp:Label ID="lbSuccess" runat="server" />
            <asp:Label ID="lbError" runat="server" />
            <asp:TextBox ID="tbSuccess" runat="server" TextMode="MultiLine" Width="100%" />
            <table id="tblInformation" runat="server" width="100%">
                <tr>
                    <td colspan="2">
                        <MyUcs:TasksList ID="UcTasksList" runat="server" OnSelectedTaskChanged="UcTasksList_SelectedTaskChanged" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <MyUcs:TaskInformation ID="UcTaskInformation" runat="server" />
                    </td>
                </tr>
            </table>
            <table id="tblNeededValues" runat="server" />
            <asp:Button ID="btExectuteRule" runat="server" Text="Ejecutar" OnClick="btExectuteRule_Click" />
        </div>
    </form>
</body>
</html>