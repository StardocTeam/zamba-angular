<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls.Forum.Controls_Forum_WCForum" Codebehind="Forum.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="MyUC" TagName="NuevoMensaje" Src="NuevoTema.ascx" %>

<script type="text/javascript">
    //24/10/11: Como las tabs de ajax no se están renderizando correctamente, en el ready se buscan las mismas y se les agrega un height.  
    $(document).ready(function () {
        var screenHeight = $(window).innerHeight() - $("#divButtons").outerHeight(true);
        $("#divForum").height(screenHeight);
        $(".ajax__tab_tab").height(20);
    });

    //24/10/11: Como el control tiene eventos de Ajax se debe tambien refrescar las tabs.
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args) {
        $(".ajax__tab_tab").height(20);   
        t = setTimeout("parent.hideLoading();", 500);
    }

    function Refresh_Click() {
        parent.ShowLoadingAnimation();
        document.location = document.location;
    }
</script>

<asp:HiddenField ID="hdnIdMensaje" runat="server" />
<asp:HiddenField ID="hdnNuevoMensaje" runat="server" />
<asp:HiddenField ID="hdnCanDelete" runat="server" />
<asp:HiddenField ID="hdnSelectedNodeIndex" runat="server" />
<asp:HiddenField ID="hdnSelectedNodeSubjet" runat="server" />
<asp:HiddenField ID="hdnDocId" runat="server" />
<asp:HiddenField ID="hdnDocTypeId" runat="server" />
<asp:HiddenField ID="hdnUrl" runat="server" />

<asp:Panel ID="pnlErrors" Width="10" Height="15" Style="display: none" runat="server">
    <asp:TextBox ID="txtErrors" runat="server"></asp:TextBox>
</asp:Panel>

<asp:Panel runat="server" ID="Foro" CssClass="PanelPrincipal-Frm-Forum">

        <div id="divButtons" style="padding: 5px">
        <div class="row">
            <div class="col-md-12">
                <button id="btnRefresh" type="button" class="btn btn-default btn-xs" onclick="Refresh_Click();">
                    <span class="glyphicon glyphicon-refresh"></span> Refrescar                                   
                </button>

                <asp:LinkButton runat="server" title="Crear tema" ID="btnNewThreadTop" class="btn btn-primary btn-xs"
                    OnClick="btnNewThread_Click" OnClientClick="parent.ShowLoadingAnimation();">
                    <span class="glyphicon glyphicon-pencil"></span> Crear tema
                </asp:LinkButton>

                <asp:LinkButton runat="server" title="Responder" ID="btnReplyTop" class="btn btn-primary btn-xs"
                    OnClick="btnResponderForo_Click" OnClientClick="parent.ShowLoadingAnimation();">
                    <span class="glyphicon glyphicon-comment"></span> Responder
                </asp:LinkButton>

                <asp:LinkButton runat="server" title="Eliminar" ID="btnDeleteTop" class="btn btn-danger btn-xs"
                    OnClick="btnEliminarForo_Click" OnClientClick="parent.ShowLoadingAnimation();">
                    <span class="glyphicon glyphicon-trash"></span> Eliminar
                </asp:LinkButton>
            </div>
        </div>
    </div>

        <div id="divForum" style="padding: 0px; width:100%">
        <div class="row">
            <div class="col-md-6">
                <div id="divMessages" style="overflow: auto;">
                    <asp:TreeView ID="tvwMensajesForo" runat="server" ImageSet="Arrows" LineImagesFolder="~/TreeLineImages"
                        SkipLinkText="" OnSelectedNodeChanged="tvwMensajesForo_SelectedNodeChanged">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" />
                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </div>
            </div>
           <!-- css -->
           <style type="text/css">
              .tabStyle .ajax__tab_header
                    {
                        font-family: "Helvetica Neue" , Arial, Sans-Serif;
                        font-size: 14px;
                        font-weight:bold;
                        height:8%;
                        width: 506px;

                    }
                    .tabStyle .ajax__tab_header .ajax__tab_outer
                    {
                        border-color: #222;
                        color: #222;
                        padding-left: 10px;
                        margin-right: 3px;
                        border:solid 1px #d7d7d7;                        
                            
                        
                    }
                    .tabStyle .ajax__tab_header .ajax__tab_inner
                    {
                        border-color: #666;
                        color: #666;
                        padding: 3px 10px 2px 0px;                        
                        
                    }
                    .tabStyle .ajax__tab_hover .ajax__tab_outer
                    {
                        background-color:#6E6E6E;
                    }
                    .tabStyle .ajax__tab_hover .ajax__tab_inner
                    {
                        color: #fff;
                    }
                    .tabStyle .ajax__tab_active .ajax__tab_outer
                    {
                        border-bottom-color: #ffffff;
                        background-color: #d7d7d7;
                    }
                    .tabStyle .ajax__tab_active .ajax__tab_inner
                    {
                        color: #000;
                        border-color: #333;
                    }
                    .tabStyle .ajax__tab_body
                    {
                        font-family: verdana,tahoma,helvetica;
                        font-size: 10pt;
                        background-color: #fff;
                        border-top-width: 0;
                        /*border: solid 1px #d7d7d7;*/
                        border-top-color: #ffffff; 
                        width: 506px;
                    }
                  </style>
            <!--Fin css-->


            
                

                    <asp:UpdatePanel ID="updatePanelDetalles" UpdateMode="Conditional" runat="server"  RenderMode="Block">

                    <ContentTemplate>
                                    <div class="col-md-4" style="margin-right:16%;">
                
                <asp:Panel runat="server" ID="pnlTabs">
                    <!--  Comienzo de presentación de mesajes  -->
                    <ajaxToolkit:TabContainer ID="TabForo" runat="server" cssClass="tabStyle" AutoPostBack="true"   OnActiveTabChanged="TabForo_ActiveTabChanged" >
                        
                        <!--Tab Mensajes-->
                        <asp:TabPanel ID="TabMensajes" runat="server" HeaderText="Mensaje">
                            <ContentTemplate>
                                <asp:Panel ID="pnlMensaje" Visible="false" runat="server">
                                    <div  style="padding-bottom:5px; visibility:hidden;">
                                       <asp:LinkButton runat="server" Visible="false" title="Responder" ID="btnReplyBottom" class="btn btn-primary btn-xs"
                                           OnClick="btnResponderForo_Click">
                                            <span class="glyphicon glyphicon-comment"></span> Responder
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" Visible="false" title="Eliminar" ID="btnDeleteBottom" class="btn btn-danger btn-xs"
                                            OnClick="btnEliminarForo_Click">
                                            <span class="glyphicon glyphicon-trash"></span> Eliminar
                                        </asp:LinkButton>
                                    </div>
                                    <asp:TextBox ID="txtMensaje" runat="server"  ReadOnly="True" TextMode="MultiLine"
                                        Width="100%" Height="250px" class="form-control" rows="20"></asp:TextBox>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>

                                                
                        <!--Tab Adjuntos-->
                        <asp:TabPanel ID="TabAdjuntos" runat="server" Visible="False"  HeaderText="Adjuntos">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server" style="padding: 15px">
                                    <asp:UpdatePanel ID="idUpdatePanelADjuntos" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                    <asp:GridView ID="grdAdjuntos" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Underline="False"
                                        OnSelectedIndexChanged="grdAdjuntos_SelectedIndexChanged" Width="100%">
                                        <AlternatingRowStyle BackColor="#EAEAEA" Font-Names="Verdana" Font-Underline="False"
                                            Wrap="True" />
                                        <EditRowStyle Font-Underline="False" Wrap="True" />
                                        <EmptyDataRowStyle Font-Underline="False" />
                                        <FooterStyle BackColor="#CCCCCC" Font-Underline="False" ForeColor="Black" />
                                        <HeaderStyle BackColor="#DDDDDD" Font-Names="Verdana" Font-Underline="False" ForeColor="#595959"
                                            HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#DEDEDE" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left"
                                            Wrap="False" />
                                        <RowStyle BackColor="#F2F2F2" BorderStyle="None" BorderWidth="1px" Font-Names="Verdana"
                                            Font-Underline="False" ForeColor="Black" VerticalAlign="Top" Wrap="True" />
                                        <SelectedRowStyle Wrap="True" />
                                    </asp:GridView>
                                    <asp:Label ID="lblAdjuntos" runat="server" Visible="False" Text="El mensaje no contiene adjuntos."></asp:Label>
                          <%--            <iframe id="formBrowser" runat="server" frameborder="0" height="500px" style="border: 1px solid black; overflow-y:auto"
                                            width="100%" scrolling="yes"></iframe>--%>
                                    <asp:Label ID="lblAttachError" runat="server" Text="Error al visualizar el adjunto"
                                        Visible="False" ForeColor="Red"></asp:Label>
                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                      </asp:Panel>
                            </ContentTemplate>
                        </asp:TabPanel>

                        <!--Tab Participantes-->
                        <asp:TabPanel ID="TabParticipantes" runat="server" HeaderText="Participantes" >
                            <ContentTemplate>
                                
                                <div style="padding: 15px; overflow: auto;">
                                    <asp:GridView ID="grdParticipantes" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Underline="False"
                                        Width="100%">
                                        <AlternatingRowStyle BackColor="#EAEAEA" Font-Names="Verdana" Font-Underline="False"
                                            Wrap="True" />
                                        <EditRowStyle Font-Underline="False" Wrap="True" />
                                        <EmptyDataRowStyle Font-Underline="False" />
                                        <FooterStyle BackColor="#CCCCCC" Font-Underline="False" ForeColor="Black" />
                                        <HeaderStyle BackColor="#DDDDDD" Font-Names="Verdana" Font-Underline="False" ForeColor="#595959"
                                            HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#DEDEDE" Font-Underline="False" ForeColor="Black" HorizontalAlign="Left"
                                            Wrap="False" />
                                        <RowStyle BackColor="#F2F2F2" BorderStyle="None" BorderWidth="1px" Font-Names="Verdana"
                                            Font-Underline="False" ForeColor="Black" VerticalAlign="Top" Wrap="True" />
                                        <SelectedRowStyle Wrap="True" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>

                        <!--Tab Información-->
                        <asp:TabPanel ID="TabInformacion" runat="server" HeaderText="Información">
                            <ContentTemplate>
                                <div style="padding:15px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>Usuario Creador</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:TextBox ID="UsuarioCreador" runat="server" Enabled="False" Width="200px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:20px">
                                        <div class="col-md-12">
                                            <label>Fecha de Creación</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:TextBox ID="FechaCreacion" runat="server" Enabled="False" Width="200px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>

                    </ajaxToolkit:TabContainer>
                       </asp:Panel>
    </div>
                          </ContentTemplate>

                        </asp:UpdatePanel>
                    <!--  Fin de presentación de mesajes  -->
             

            </div>
              
                
        </div>

</asp:Panel>

<asp:Panel ID="pnlRespuesta" runat="server" >
    <MyUC:NuevoMensaje ID="popUpMensaje" runat="server" />
</asp:Panel>