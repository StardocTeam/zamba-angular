<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCDocTypesl.ascx.cs" Inherits="Controls_Core_WCDocTypesl" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="Panel1"  runat="server" Height="100%" ScrollBars="Vertical">
            <asp:DataList ID="DataList1" runat="server" BorderStyle="Solid" 
                BackColor="White" HorizontalAlign="Left"
                BorderColor="#E7E7FF" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                RepeatColumns="1" OnItemDataBound="Dt_ItemDataBound" 
                >
                <ItemStyle BackColor="#EDF7FE" BorderStyle="None" BorderWidth="1px" ForeColor="Black"
                                    Font-Size="Small" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" Font-Size="Small" />
                <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                <AlternatingItemStyle BackColor="#F7F7F7" Font-Size="Small" />
                <ItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Eval("ID") %>' ID="DocTypeId" Visible="false"
                        AutoPostBack="True" />
                    <asp:CheckBox runat="server" Text='<%# Eval("Name") %>' dtid='<%# Eval("ID") %>'
                        ID="DocTypeCheckBox" Name="DocTypeName" OnCheckedChanged="Check" AutoPostBack="True" />
                </ItemTemplate>
            </asp:DataList>
            <%-- <asp:ObjectDataSource ID="DocTypesDataSource" runat="server" 
            SelectMethod="GetDocTypesIdsAndNames" TypeName="Zamba.Core.DocTypesBusiness">
        </asp:ObjectDataSource>--%></asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
