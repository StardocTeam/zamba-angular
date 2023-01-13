<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCForum.ascx.cs" Inherits="Controls_Forum_WCForum" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register TagPrefix="MyUC" TagName="NuevoMensaje" Src="~/Controls/Forum/NuevoTema.ascx" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <p>
            <asp:Button ID="btnNuevoTema" runat="server" OnClick="btnNuevoTema_Click" 
                Text="Nuevo Tema" Enabled="False" />
            <asp:HiddenField ID="hdnResultId2" runat="server" OnValueChanged="hdnResultId2_ValueChanged" />
            <asp:HiddenField ID="hdnIdMensaje" runat="server" />
            <asp:HiddenField ID="hdnNuevoMensaje" runat="server" />
            <asp:Panel ID="FakeControl" Width="10" Height="15" Style="display: none" runat="server">
            </asp:Panel>
            <asp:Panel runat="server" id="TreeViewPanel" >
            <div style="height:365px;overflow:auto;margin-top:10px; width: 309px;">
            <asp:TreeView ID="tvwMensajesForo" runat="server" ImageSet="Inbox" LineImagesFolder="~/TreeLineImages"
                OnLoad="PopUpDialog_OnUnload" SkipLinkText="" 
                 OnSelectedNodeChanged ="tvwMensajesForo_SelectedNodeChanged" >
                <ParentNodeStyle Font-Bold="False" />
                <HoverNodeStyle Font-Underline="True" />
                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px"
                    VerticalPadding="0px" />
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView> 
            </div>    
            </asp:Panel>
            
           <asp:Panel ID="pnlPopUpModal" runat="server" Visible="false">                        
               <%-- <div style="background-color:Silver;top:0;left:0;width:100%;height:100%;position:fixed;z-index:100;filter:alpha(opacity=50);"></div>--%>
               <div style=" background-color:Activeborder;border:solid 1px black;position:absolute;width:30%; top:17%; left:26%; height: 254px; text-align:center">
                    <asp:TextBox ID="txtMensaje" ReadOnly="true" TextMode="MultiLine" Width="95%" Height="200px" 
                        runat="server" ontextchanged="txtMensaje_TextChanged" ></asp:TextBox>
                    <div style="text-align:center;padding-top:5px;background-color:ActiveBorder ">
                    <table>
                    <tr>
                    <td>
                                <asp:Button ID="btnResponderForo1" runat="server" Text="Responder" Enabled="False" OnClick="btnResponderForo_Click" />
                            </td>
                            <td>
                            <asp:Button ID="btnEliminarForo" runat="server" Text="Eliminar" Enabled="false" 
                                    onclick="btnEliminarForo_Click" />
                            </td>
                            <td>
                                                    <asp:Button  ID="btnCerrarPopUpModal" runat="server"  Text="Cerrar"
                            onclick="btnCerrarPopUpModal_Click"   />
                            </td>
                            </tr>
                            </table>
                    </div>
                </div>                         
            </asp:Panel>  
     
                    
    </ContentTemplate>
</asp:UpdatePanel>


               
