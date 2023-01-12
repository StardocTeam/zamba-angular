<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TemplatesListSelector.ascx.cs"
    Inherits="Controls_Insert_Templates_TemplatesListSelector" %>
<asp:Panel ID="Panel1" runat="server">
</asp:Panel>
<asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="false" BackColor="White"
    BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="X-Small"
     AlternatingRowStyle-Font-Underline="false"
    HeaderStyle-Font-Underline="false" EditRowStyle-Font-Underline="false" EmptyDataRowStyle-Font-Underline="false"
    Font-Underline="false" FooterStyle-Font-Underline="false" PagerStyle-Font-Underline="false"
    RowStyle-Font-Underline="false" SelectedRowStyle-Wrap="false" AlternatingRowStyle-Wrap="false"
    EditRowStyle-Wrap="false" PagerStyle-Wrap="false" RowStyle-Wrap="false">
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" BorderStyle="None" BorderWidth="1px"
        Font-Size="X-Small" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" Font-Size="X-Small" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
        Font-Size="X-Small" />
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" Font-Size="X-Small" />
    <Columns>
        <asp:CommandField ShowSelectButton="True" />
    </Columns>
</asp:GridView>
