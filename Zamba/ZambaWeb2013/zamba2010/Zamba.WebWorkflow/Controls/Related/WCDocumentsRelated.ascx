<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCDocumentsRelated.ascx.cs" Inherits="Controls_Related_WCDocumentsRelated" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:Label runat="server" Text="" ID="txtRel" visible="false"></asp:Label>
 <asp:TreeView ID="TreeView1" runat="server" Height="210px" 
        style="top: 103px; left: 171px; " Width="183px" ImageSet="Simple">
     <ParentNodeStyle Font-Bold="False" />
     <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
     <SelectedNodeStyle Font-Bold="True" Font-Italic="False" Font-Underline="True" 
         ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
     <RootNodeStyle Font-Bold="False" />
     <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
         HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
    </asp:TreeView>
</ContentTemplate>
</asp:UpdatePanel>