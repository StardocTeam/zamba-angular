<%@ Page Language="C#" MasterPageFile="~/ZambaMaster.master" AutoEventWireup="true" CodeFile="MainTasks.aspx.cs" Inherits="Tasks_MainTasks" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table>
  <tr>
  <td>
    <asp:Panel ID="Panel1" runat="server">
        <a href="~/WFMonitor/Controls/TaskSelector/StepsList.ascx">~/WFMonitor/Controls/TaskSelector/StepsList.ascx</a>
    </asp:Panel>
  </td>
  <td>
    <asp:Panel ID="Panel2" runat="server">
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <a href="~/WFMonitor/Controls/TaskSelector/TasksList.ascx">~/WFMonitor/Controls/TaskSelector/TasksList.ascx</a>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
</td>
  </tr>
  </table>
  
</asp:Content>

