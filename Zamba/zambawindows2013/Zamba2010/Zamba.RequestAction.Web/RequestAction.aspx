<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestAction.aspx.cs" Inherits="ExecuteRequestAction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Controls/TaskInformation.ascx" TagPrefix="MyUcs" TagName="TaskInformation" %>
<%@ Register Src="~/Controls/TasksList.ascx" TagPrefix="MyUcs" TagName="TasksList" %>
<%@ Register Src="~/Controls/FinishedTasksList.ascx" TagPrefix="MyUcs" TagName="FinishedTaskList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Request Action</title>
</head>
<body>
    <form id="frmMain" runat="server">
        <asp:ScriptManager ID="smMain" runat="server" />
        <div class="Tasks">
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lbSuccess" runat="server" CssClass="Message" />
                        <div class="Error">
                            <asp:Label ID="lbError" runat="server" Visible="false" CssClass="ErrorMessage" />
                            <asp:BulletedList runat="server" ID="blErrors" CssClass="ErrorMessage" Visible="false" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="Request">
                            <fieldset title="Información del pedido" class="FieldSet">
                                <legend class="Legend">Información del pedido</legend>
                                <table id="tblRequestInformation" runat="server" class="Table" />
                            </fieldset>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <asp:UpdatePanel ID="upTasks" runat="server">
                            <ContentTemplate>
                                <fieldset title="Información de las tareas" class="FieldSet" style="height: 50%">
                                    <legend class="Legend">Información de las tareas </legend>
                                    <MyUcs:TasksList ID="UcTasksList" runat="server" OnSelectedTaskChanged="UcTasksList_SelectedTaskChanged" />
                                    <MyUcs:TaskInformation ID="UcTasksInformation" runat="server" />
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <fieldset title="Listado de Reglas" class="FieldSet" runat="server" id="fsButtons"
                            style="text-align: right;">
                            <legend class="Legend">Listado de Reglas</legend>
                            <asp:Repeater runat="server" ID="rpRules">
                                <ItemTemplate>
                                    <asp:Button OnClick="btExecuteRule_Click" ID="btExecuteRule" CommandName='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                        runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                        CssClass="Button" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </fieldset>
                    </td>
                    <td style="width: 50%;vertical-align: top ">
                        <asp:UpdatePanel ID="upFinishedTasks" runat="server">
                            <ContentTemplate>
                                <fieldset title="Información de las tareas realizadas" class="FieldSet">
                                    <legend class="Legend">Información de las tareas realizadas</legend>
                                    <MyUcs:FinishedTaskList ID="UcFinishedTasks" runat="server" OnSelectedTaskChanged="UcFinishedTasks_SelectedTaskChanged" />
                                    <MyUcs:TaskInformation ID="UcFinishedTasksInformation" runat="server" />
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div class="NeededValues">
            <table id="tblNeededValues" runat="server" class="Table" />
        </div>
    </form>
</body>
</html>
