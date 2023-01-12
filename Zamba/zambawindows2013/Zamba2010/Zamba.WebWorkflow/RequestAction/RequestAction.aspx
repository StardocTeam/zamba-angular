<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true" CodeFile="RequestAction.aspx.cs" Inherits="ExecuteRequestAction" Title="Zamba Web - Acciones Pendientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/RequestAction/Controls/TaskInformation.ascx" TagPrefix="MyUcs"
    TagName="TaskInformation" %>
<%@ Register Src="~/RequestAction/Controls/ListadoTareas.ascx" TagPrefix="MyUcs"
    TagName="ListadoTareas" %>
<%@ Register Src="~/RequestAction/Controls/ListadoTareasTerminadas.ascx" TagPrefix="MyUcs"
    TagName="ListadoTareasTerminadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



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
                                <MyUcs:ListadoTareas ID="UcTasksList" runat="server" OnSelectedTaskChanged="UcTasksList_SelectedTaskChanged" />
                                <MyUcs:TaskInformation ID="UcTasksInformation" runat="server" />
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <fieldset title="Listado de Reglas" class="FieldSet" runat="server" id="fsButtons"
                        style="text-align: right;">
                        <legend class="Legend">Listado de Reglas</legend>
                        <asp:Repeater runat="server" ID="rpRules" Visible="true">
                            <ItemTemplate>
                                <asp:Button OnClick="btExecuteRule_Click" ID="btExecuteRule" CommandName='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                    runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                    CssClass="Button" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </fieldset>
                </td>
                <td style="width: 50%; vertical-align: top">
                    <asp:UpdatePanel ID="upFinishedTasks" runat="server">
                        <ContentTemplate>
                            <fieldset title="Información de las tareas realizadas" class="FieldSet">
                                <legend class="Legend">Información de las tareas realizadas</legend>
                                <MyUcs:ListadoTareasTerminadas ID="UcFinishedTasks" runat="server" OnSelectedTaskChanged="UcFinishedTasks_SelectedTaskChanged" />
                                <MyUcs:TaskInformation ID="UcFinishedTasksInformation" runat="server" />
                                <asp:LinkButton ID="lnkViewHistory" runat="server" OnClick="lnkViewHistory_Click"
                                    Text="Ver Historial Completo" />
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
    
    </asp:Content>

