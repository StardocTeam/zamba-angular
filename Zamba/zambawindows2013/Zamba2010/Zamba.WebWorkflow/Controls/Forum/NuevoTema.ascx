<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NuevoTema.ascx.cs" Inherits="Controls_Forum_WebUserControl" %>
<div style="width: 257px; margin-right: 0px;" align="left">
    <asp:HiddenField ID="hdnResultId" runat="server" OnValueChanged="hdnResultId_ValueChanged" />
    <asp:HiddenField ID="hdnNuevoMensaje2" runat="server" OnValueChanged="hdnNuevoMensaje2_ValueChanged" />
    <asp:HiddenField ID="hdnIdMensaje2" runat="server" OnValueChanged="hdnIdMensaje2_ValueChanged" />
    <div style="text-align: center; margin-bottom: 10px;">
        <asp:Label ID="lblMensajeNuevo" runat="server" Text="Asunto" Font-Underline="False"
            ForeColor="White" />
        <br />
        <div style="text-align:center " runat="server" >
        <asp:TextBox ID="txtAsunto" runat="server" Height="25px" Width="80%" 
            OnTextChanged="txtAsunto_TextChanged" MaxLength="32"  />
            </div>
        <br />
        <div id="Div2" style="text-align:center " runat="server" >
        <asp:Label ID="lblMensajeNuevo0" runat="server" Text="Mensaje" ForeColor="White" />
        </div>
        <br />
        <div id="Div1" style="text-align:center " runat="server" >
        <asp:TextBox ID="txtMensaje" runat="server" Height="150px" Width="85%" 
            TextMode="MultiLine" />
            </div> 
        <br />
        <div id="Div3" style="text-align:center " runat="server" >
         <asp:Button ID="btnGuardarMensaje" runat="server" OnClick="btnGuardarMensaje_Click"
            Text="Guardar" Style="height: 26px" Font-Size="Small" Height="16px" 
            Width="65px" />
       
        &nbsp;<asp:Button ID="btnNotificarGuardar" runat="server" 
            Text="Notificar y Guardar" />
         <asp:Button  ID="Button2" runat="server" Text="Cancelar" 
                        Style="height: 26px" Font-Size="Small" Height="16px"/> 
                        </div>                     
    </div>
</div>
