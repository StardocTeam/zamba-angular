<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>

    <script language="javascript" type="text/javascript">
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr align="center">
                    <td align="center" style="height: 375px; width: 875px;">
                        &nbsp;<table style="width: 819px; height: 268px; position: relative;" border="0"
                            cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" colspan="2" style="width: 398px; height: 31px">
                                    <asp:Label ID="lbTitulo" runat="server" Text="Zamba Web Uploader" Height="26px" Width="337px"
                                        Style="z-index: 1; left: -98px; top: -12px; font-weight: bold; font-size: x-large;
                                        text-transform: uppercase; color: #3366cc; font-family: Verdana;" BackColor="White"
                                        Font-Bold="True" Font-Names="Arial Narrow" /></td>
                                <td align="center" colspan="1" style="width: 204px; height: 31px">
                                    <img src="imagenes/zamba modo cliente 64X64 ejemplo b.JPG" /></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="height: 15px">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="1" style="width: 397px; height: 94px">
                                    <fieldset style="left: 15px; top: -345px; width: 340px; height: 60px;">
                                        <div>
                                            &nbsp;<asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo de Documento" Height="16px"
                                                Width="172px" Style="z-index: 1; left: -80px; position: relative; top: -11px;
                                                color: #3366cc; font-family: Verdana;" BackColor="White" Font-Bold="True" Font-Names="Arial Narrow" /></div>
                                        &nbsp;<asp:DropDownList ID="cboDocType" runat="server" Style="left: -3px; top: -1px;
                                            position: relative;" Width="284px" DataSourceID="ObjectDataSource1" DataTextField="Doc_Type_Name"
                                            DataValueField="Doc_Type_Id" AutoPostBack="True">
                                        </asp:DropDownList></fieldset>
                                </td>
                                <td align="center" colspan="4" style="height: 94px">
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4" style="height: 80px">
                                    <asp:Label ID="lbIndices" runat="server" Text="Indices" Height="13px" Width="87px" Style="z-index: 1;
                                        left: -295px; position: relative; top: 48px; color: #3366cc; font-family: Verdana;"
                                        BackColor="White" Font-Bold="True" Font-Names="Arial Narrow" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4" style="height: 101px;">
                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                    <fieldset style="left: -8px; vertical-align: middle; width: 749px; position: relative;
                                        top: 1px; height: 72px; text-align: center">
                                        <asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource2" Width="723px"
                                            RepeatColumns="1" Height="1px" Style="left: 0px; position: relative; top: 13px">
                                            <ItemTemplate>
                                                <table style="width: 455px">
                                                    <tr>
                                                        <td style="height: 19px; width: 371px;">
                                                            <asp:Label ID="Index_Name" runat="server" Text='<%# Eval("INDEX_NAME", "{0:d}") %>'
                                                                Style="position: static; text-align: right;" Width="241px"></asp:Label></td>
                                                        <td style="width: 307px; height: 19px">
                                                            <asp:TextBox ID="Index_Value" runat="server" Width="263px" EnableViewState="False"></asp:TextBox>
                                                            <td style="width: 982px; height: 19px">
                                                                <asp:HiddenField ID="Index_Id" runat="server" Value='<%# Eval("INDEX_ID")  %>' />
                                                                <asp:HiddenField ID="Index_Type" runat="server" Value='<%# Eval("INDEX_TYPE")  %>' />
                                                                <asp:HiddenField ID="Index_Len" runat="server" Value='<%# Eval("INDEX_LEN")  %>' />
                                                                <%--<asp:HiddenField ID="Index_DropDown" runat="server" Value='<%# Eval("DROPDOWN")  %>'/>--%>
                                                            </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="height: 111px">
                                    <div>
                                        <asp:Label ID="lbSubirArchivo" runat="server" Text="Subir Archivo" Height="16px" Width="117px"
                                            Style="z-index: 1; left: -74px; position: relative; top: 11px; font-weight: bold;
                                            color: #3366cc; font-family: Verdana;" BackColor="White" Font-Bold="True" Font-Names="Arial Narrow" />
                                    </div>
                                    <fieldset style="left: 262px; width: 281px; position: static; top: -177px; height: 73px;"
                                        id="FIELDSET1" onclick="return FIELDSET1_onclick()">
                                        <div>
                                            &nbsp;<br />
                                            <div>
                                                <asp:Button ID="cmdUpload" runat="server" OnClick="Button1_Click" Text="Upload" Style="position: relative;
                                                    left: 0px; top: 29px;" Height="23px" Width="77px" />
                                                <asp:FileUpload ID="Upload" runat="server" Height="21px" Style="left: 0px; position: relative;
                                                    top: -23px" BorderStyle="None" />
                                            </div>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="height: 31px" align="center">
                                    <asp:Label ID="lbStatus" runat="server" Height="21px" Width="261px" Style="left: -261px;
                                        top: -202px; font-family: Verdana;"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp;
        </div>
        <span></span>&nbsp; &nbsp;&nbsp;&nbsp;
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllDocTypes"
            TypeName="Zamba.Core.DocTypesFactory"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="getIndexByDocTypeId"
            TypeName="Zamba.Core.DocTypesFactory">
            <SelectParameters>
                <asp:ControlParameter ControlID="cboDocType" DefaultValue="" Name="DocTypeId" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
