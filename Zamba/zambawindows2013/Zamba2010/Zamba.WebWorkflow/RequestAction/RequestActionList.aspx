<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestActionList.aspx.cs"
    Inherits="RequestActionList" MasterPageFile="~/ZambaMaster.master" EnableEventValidation="false"  %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<asp:Content ContentPlaceHolderID ="ContentPlaceHolder1" runat="server" >
    <%-- <html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>Listado de pedidos</title>
</head>
<body>--%>
    <%--<form id="frmMain" >--%>
    <div align="center">
        <asp:Label ID="lbNoTasks" runat="server" CssClass="Label" />
    </div>
    <div align="right">
        <asp:Label ID="lblFullName" runat="server" Font-Bold="True" Font-Italic="True" 
            Font-Size="Medium"  />
    </div>
    <div align="left">
        <asp:Label ID="lblPendingTasks" runat="server" Font-Size="Smaller"  />
    </div>
    <fieldset title="" class="FieldSet" runat="server" id="fsTaskList">
        
        <div class="UserControlBody" style="vertical-align:top; text-align: left">
            <asp:GridView ID="gvRequests" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                ForeColor="Navy" CellPadding="4" GridLines="None" Font-Size="xx-Small" 
                OnPageIndexChanging="gvRequests_PageIndexChanging" PageSize="15">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Wrap="False" />
                <EmptyDataRowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRequest" runat="server" ToolTip="Ir al pedido" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Requerido por" />
                    <asp:BoundField HeaderText="Fecha del pedido" />
                    <asp:BoundField HeaderText="Titulo" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" Wrap="True" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" HorizontalAlign="Left"
                    VerticalAlign="Middle" Wrap="False" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Wrap="True" />
                <HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" Wrap="True" />
                <AlternatingRowStyle BackColor="White" Wrap="True" />
            </asp:GridView>
        </div>
    </fieldset>
    <%--</div>--%>
<%--    </form>--%>
<%--</body>
</html>--%>
</asp:Content>