<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="History" %>

<%@ Register TagPrefix="MyUcs" TagName="Error" Src="~/RequestAction/Controls/ErrorHandler.ascx" %>
<fieldset title="Historial" class="FieldSet">
    <legend class="Legend">Historial</legend>
    <MyUcs:Error id="UcError" runat="server" />        
    <div class="UserControlBody" style="text-align: center; font-size:xx-small;" runat="server" id="pnlHistory" >
        <asp:HiddenField runat="server" ID="hfTaskId" />
        <asp:Label ID="lbNoHistory" runat="server" CssClass="Label" />
        <asp:Panel runat="server" ScrollBars="auto" Height="200 px">
            <asp:GridView ID="gvHistory" runat="server" AllowPaging="True" OnPageIndexChanging="gvHistory_PageIndexChanging"  Font-Size="xx-small"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                CellPadding="3">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#006699" ForeColor="White" Font-Bold="True" />
            </asp:GridView>
        </asp:Panel>       
    </div>
</fieldset>
