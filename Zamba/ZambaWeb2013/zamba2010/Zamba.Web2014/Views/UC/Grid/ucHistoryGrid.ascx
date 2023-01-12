<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHistoryGrid.ascx.cs" Inherits="Views_UC_Grid_ucHistoryGrid" %>

<asp:HiddenField ID="hdnAction" runat="server" />
<asp:HiddenField ID="hdnTarget" runat="server" />
<asp:Label runat="server" ID="lblMessage" font-size="14pt" Visible="false" ForeColor="Black"></asp:Label>
<asp:GridView ID="grdHistory" runat="server"
	AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None" AllowPaging="true" OnPageIndexChanging="pageChangeEvent">
	<RowStyle CssClass="RowStyle" Wrap="false" />
	<EmptyDataRowStyle CssClass="EmptyRowStyle" Wrap="false" />
	<PagerStyle CssClass="PagerStyle" />
	<SelectedRowStyle CssClass="SelectedRowStyle" Wrap="false"/>
	<HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
	<EditRowStyle CssClass="EditRowStyle" Wrap="false"/>
	<AlternatingRowStyle CssClass="AltRowStyle" Wrap="false"/>
</asp:GridView>
<div  id="divIframe" runat="server" style="padding-top:10px" >
<iframe id="formBrowser" runat="server" frameborder="1" style="border: 1px; overflow-y:auto;display:none;height:400px;width:98%"
	    scrolling="yes"></iframe>
<asp:Label ID="lblAttachError" runat="server" Text="Error al visualizar el adjunto"
	Visible="False" ForeColor="Red"></asp:Label>
</div>