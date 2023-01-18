<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TableroDOR.aspx.cs" Inherits="Views_Aysa_TableroDOR" %>

<%@ Import Namespace="System.Web.Optimization" %>

  <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

<!DOCTYPE html>

<html>
<head runat="server">

    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/thickbox.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/jquery-ui-1.8.6.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWebTables.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/Styles/jq_datepicker.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/GridThemes/WhiteChromeGridView.css" />

  

    <title>Tablero DOR</title>

    <script>
        $(window).load(function () {
            t = setTimeout("parent.hideLoading();", 500);
        });
    </script>

    <style type="text/css">
        .DataTable {
            border-collapse: collapse;
        }

            .DataTable td, table th {
                border: 1px solid black;
            }

        .NoneBorders {
            border: 0px solid black;
        }

        .BoldFont {
            font-weight: bold;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <asp:ScriptManager ID="RadScriptManager1" runat="server">
                <Scripts>
                </Scripts>
            </asp:ScriptManager>

            <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1">
                <strong>Procesando...
                    <img src="../../Content/Images/loading.gif" alt="" />
                </strong>
            </telerik:RadAjaxLoadingPanel>
        </div>
        <table>
            <tr>
                <td style="width: 20%">&nbsp;
            <iframe id="fileIF" runat="server" style="display: none"></iframe>
                </td>
                <td>
                    <table class="DataTable" style="border-collapse: collapse; font-family: Arial; font-size: 10pt; background-color: white;">
                        <tr>
                            <td colspan="2" style="text-align: left; font-weight: bold; width: 70%">Seleccione el mes y a&ntilde;o que filtrar: 
                    <telerik:RadMonthYearPicker ID="MonthYearPicker" runat="server" ZIndex="30001">
                    </telerik:RadMonthYearPicker>
                                <asp:Button ID="btnSubmitQuery" runat="server" OnClick="SubmitQuery" Text="Mostrar resultados" OnClientClick="parent.ShowLoadingAnimation();" />
                            </td>
                            <td colspan="7" style="background-color: #0E2B8D; border-right: 0px solid transparent; border-top: 0px solid transparent">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9" style="text-align: center; background-color: #ffcc99; font-weight: bold">Tablero DOR
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 15px; text-align: center">&nbsp;
                            </td>
                            <td colspan="2" style="font-weight: bold">Indicador de actividad industrial
                            </td>
                            <td style="font-weight: bold">DRCF
                            </td>
                            <td style="font-weight: bold">DRN
                            </td>
                            <td style="font-weight: bold">DRO
                            </td>
                            <td style="font-weight: bold">DRSE
                            </td>
                            <td style="font-weight: bold">DRSO
                            </td>
                            <td style="font-weight: bold">Concesión
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: center">1</td>
                            <td colspan="2">Número de establecimientos industriales con descarga industrial a colectora	</td>
                            <td>
                                <asp:Label ID="var1" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var2" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var3" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var4" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var5" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var6" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">2</td>
                            <td colspan="2">Número de establecimientos industriales sin descarga industrial a colectora	</td>
                            <td>
                                <asp:Label ID="var7" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var8" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var9" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var10" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var11" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var12" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">3</td>
                            <td colspan="2">Cantidad de inspecciones a establecimientos industriales con descarga industrial a colectora</td>
                            <td>
                                <asp:Label ID="var13" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var14" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var15" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var16" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var17" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var18" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">4</td>
                            <td colspan="2">Cantidad de inspecciones a establecimientos industriales sin descarga industrial a colectora</td>
                            <td>
                                <asp:Label ID="var19" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var20" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var21" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var22" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var23" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var24" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">5</td>
                            <td colspan="2">Cantidad de inspecciones no contempladas en items 3 y 4</td>
                            <td>
                                <asp:Label ID="var25" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var26" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var27" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var28" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var29" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var30" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td rowspan="3" style="text-align: center">6</td>
                            <td rowspan="3">Gestión administrativa de factibilidad de volcamiento
                            </td>
                            <td>ingresados</td>
                            <td>
                                <asp:Label ID="var31" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var32" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var33" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var34" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var35" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var36" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>concedidas</td>
                            <td>
                                <asp:Label ID="var37" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var38" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var39" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var40" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var41" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var42" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>denegados</td>
                            <td>
                                <asp:Label ID="var43" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var44" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var45" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var46" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var47" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var48" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="background-color: #ffff00">
                            <td style="text-align: center">7</td>
                            <td colspan="2">Intimaciones via carta documento por parámetros fuera de norma</td>
                            <td>
                                <asp:Label ID="var49" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var50" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var51" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var52" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var53" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var54" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">8</td>
                            <td colspan="2">Cantidad de cortes de servicio efectuados</td>
                            <td>
                                <asp:Label ID="var55" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var56" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var57" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var58" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var59" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var60" runat="server"></asp:Label></td>
                        </tr>

                        <tr>
                            <td style="text-align: center">9</td>
                            <td colspan="2">Cantidad de cortes de servicio rehabilitados</td>
                            <td>
                                <asp:Label ID="var61" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var62" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var63" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var64" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var65" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="var66" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <asp:Button ID="brnExport" runat="server" Text="Exportar a CSV" OnClick="Export_Click" OnClientClick="parent.ShowLoadingAnimation();" />
                </td>
                <td style="width: 20%">&nbsp;
	                
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
