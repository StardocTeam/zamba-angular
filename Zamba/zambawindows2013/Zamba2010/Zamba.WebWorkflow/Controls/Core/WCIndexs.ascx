<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WCIndexs.ascx.cs" Inherits="Controls_Core_WCIndexs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <center><asp:Label ID="lblSelectIndex" runat="server" text="No se seleccionó ningun documento" Font-Bold="true" ForeColor="Red" Font-Size="Small" Visible="false" ></asp:Label></center>
      
        <asp:Panel ID="Panel1" runat="server" Height="100%" ScrollBars="Auto">
            <fieldset title="Listado de Indices" class="FieldSet" enableviewstate="true">
                <legend class="Legend" style="border-style: none">Listado de Indices</legend>
                <center>
                 <asp:ImageButton runat="server" ID="btnSaveChanges" ImageUrl="~/images/save.JPG" Visible="false" ToolTip="Guardar cambios realizados" />
                 </center>
                <div class="UserControlBody" style="font-size: xx-small;">
                    <asp:HiddenField runat="server" ID="hddocId" />
                    <asp:HiddenField runat="server" ID="hdDTId" />
                    <asp:Label runat="server" ID="lbTaskId" Visible="false" CssClass="Label" />
                    <asp:Panel ID="Panel2" runat="server" Height="100%" ScrollBars="Auto" EnableViewState="true">                                       
                        <asp:Table ID="tblIndices" runat="server" EnableViewState="true" />                     
                        
                  </asp:Panel>
                </div>
                <div>                
                    <asp:Panel ID="pnlPopUpModal" runat="server" Visible="false">
                        <div style="width:100%;height:100%;background-color:Silver;z-index:100;position:fixed;top:0;left:0;-moz-opacity: 0.8;opacity:.5;filter:alpha(opacity=50);" >
                        </div>
                        <div style="z-index: 1000; top: 30%; background-color: White; border: solid 1px black;
                            position: fixed; width: 250px; left: 40%;">
                            <table style="width: 94%">
                                <tr>
                                    <td>
                                        <div style="overflow: auto; height: 200px; width: 240px; text-align: center; padding-left: 1px;">
                                            <asp:GridView ID="gvSustitutionList" runat="server"  OnSelectedIndexChanged="gvSustitutionList_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnCancel" Visible="true" Text="Cancelar" OnClick="btnCancel_Onclick" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
            </fieldset>
        </asp:Panel>      
    </ContentTemplate>
</asp:UpdatePanel>
</ContentTemplate>
</asp:UpdatePanel>

