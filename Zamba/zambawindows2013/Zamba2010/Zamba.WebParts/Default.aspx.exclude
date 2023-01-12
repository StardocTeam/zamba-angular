<%@ Page Language="C#" EnableEventValidation = "true" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/UserControls/UCTaskBalances/UCTaskBalances.ascx" TagPrefix="MyUcs"
    TagName="StepBalances" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:WebPartManager ID="wpmMainManager" Personalization-ProviderName="AspNetSqlPersonalizationProvider"
                runat="server">
            </asp:WebPartManager>
        </div>
        <br />
        Estado de la pagina:
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
            <asp:ListItem Value="1">Browse</asp:ListItem>
            <asp:ListItem Value="2">Design</asp:ListItem>
            <asp:ListItem Value="3">Edit</asp:ListItem>
            <asp:ListItem Value="4">Catalog</asp:ListItem>
        </asp:DropDownList>
        <div class="Editor">
            <asp:EditorZone ID="ezMainEditor" runat="server">
                <ZoneTemplate>
                    <asp:LayoutEditorPart ID="LayoutEditorPart1" runat="server" />
                    <asp:PropertyGridEditorPart ID="PropertyGridEditorPart1" runat="server" />
                    <asp:BehaviorEditorPart ID="BehaviorEditorPart1" runat="server" />
                    <asp:AppearanceEditorPart ID="AppearanceEditorPart1" runat="server" />
                </ZoneTemplate>
            </asp:EditorZone>
        </div>
        <div class="Catalogo">
            <asp:CatalogZone ID="czMainCatalog" runat="server">
                <ZoneTemplate>
                    <asp:PageCatalogPart ID="PageCatalogPart1" runat="server" />
                </ZoneTemplate>
            </asp:CatalogZone>
        </div>
        <div class="Zonas">
            <table>
                <tr>
                    <td>
                        <asp:WebPartZone ID="Zona1" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#FFFBD6" BorderColor="#FFCC66" Font-Names="Verdana" ForeColor="#333333" />
                            <MenuLabelHoverStyle ForeColor="#FFCC66" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#FFFBD6" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#990000" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#990000" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#990000" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="Zona2" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#EFF3FB" BorderColor="#D1DDF1" Font-Names="Verdana" ForeColor="#333333" />
                            <MenuLabelHoverStyle ForeColor="#D1DDF1" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#EFF3FB" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#507CD1" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="Zona5" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:WebPartZone ID="WebPartZone1" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="WebPartZone2" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="WebPartZone3" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:WebPartZone ID="WebPartZone4" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="WebPartZone5" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                <MyUcs:StepBalances ID="StepBalances" runat="server" title="Balances de tareas segun Estado" />
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="WebPartZone6" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:WebPartZone ID="WebPartZone7" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="WebPartZone8" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="WebPartZone9" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:WebPartZone ID="WebPartZone10" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="WebPartZone11" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                    <td>
                        <asp:WebPartZone ID="WebPartZone12" runat="server" BorderColor="#CCCCCC" Font-Names="Verdana"
                            Padding="6">
                            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" />
                            <MenuLabelHoverStyle ForeColor="#E2DED6" />
                            <EmptyZoneTextStyle Font-Size="0.8em" />
                            <MenuLabelStyle ForeColor="White" />
                            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                                BorderWidth="1px" ForeColor="#333333" />
                            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" />
                            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="0.6em" />
                            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </asp:WebPartZone>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
