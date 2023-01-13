<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProcList.ascx.cs" Inherits="IntranetMarsh.Controls.ProcList" %>

<table>
<tr>
<td>
<div style="top:auto;width:130px;height:330px;overflow:auto" >
<asp:TreeView ID="ProcTreeView" runat="server" 
        onselectednodechanged="ProcTreeView_SelectedNodeChanged1" 
		Font-Size="Smaller" ForeColor="White" ImageSet="Msdn" NodeIndent="5" 
		NodeWrap="True">
	<ParentNodeStyle Font-Bold="False" />
	<HoverNodeStyle BackColor="#CCCCCC" BorderColor="#888888" BorderStyle="Solid" 
		Font-Underline="True" />
	<SelectedNodeStyle BorderColor="#888888" BorderStyle="Solid" 
		BorderWidth="1px" Font-Underline="False" HorizontalPadding="3px" 
		VerticalPadding="1px" />
	<NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="White" 
		HorizontalPadding="5px" NodeSpacing="1px" VerticalPadding="2px" />
	</asp:TreeView>
</div >
</td>
</tr>
</table>