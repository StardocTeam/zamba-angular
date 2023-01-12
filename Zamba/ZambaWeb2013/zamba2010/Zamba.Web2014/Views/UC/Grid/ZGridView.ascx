<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ZGridView.ascx.cs" Inherits="ZGridView" %>
<div id="gridContainer" style="overflow: auto;"  >
    <asp:GridView ID="grvGrid" runat="server" CssClass="GridViewStyle"  
        GridLines="None" AutoGenerateColumns="false" OnRowDataBound="DataItemGrid_RowDataBound">
        <RowStyle CssClass="RowStyleTasks" Wrap="false" />
        <EmptyDataRowStyle CssClass="EmptyRowStyle" Wrap="false" />
        <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
    </asp:GridView>
</div>
<div id="divPager">
    <asp:Repeater ID="rptPager" runat="server" >
        <ItemTemplate>
            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>'
                Enabled='<%#Eval("Enabled") %>' OnClick="Page_Changed" CssClass='<%#Eval("Selected","PageSelected_{0}") %>'
                Font-Bold="true" />
        </ItemTemplate>
    </asp:Repeater>
</div>