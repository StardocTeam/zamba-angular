<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertarDoc.aspx.cs" Inherits="InsertarDoc" 
    MasterPageFile="~/MasterPage.Master" EnableSessionState="True" ValidateRequest="false" 
    EnableEventValidation="false" %>

<%@ Register TagPrefix="WCInsert" TagName="WCInsert" Src="~/WCInsert.ascx" %>



<asp:Content ID ="contentInsertarDoc" ContentPlaceHolderID ="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="scriptManager" runat="server" />

  <asp:Panel id="pnlInsertar" runat="server"  ScrollBars="Auto"  Height="450px">
        
        <WCInsert:WCInsert runat="server" ID="WCInsert0" />
        
  </asp:Panel> 
    
</asp:Content>



