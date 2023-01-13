<%@ Page Language="C#" MasterPageFile="~/IntraMasterPage.Master" AutoEventWireup="true"
    CodeFile="Suggestions.aspx.cs" Inherits="IntranetMarsh.Suggestions" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            height: 324px;
        }
        .style2
        {
            width: 180px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="height: 380px" width="500px">
        <tr>
            <td valign="top" style="height: 30px">
                <table style="height: 25px" width="100%">
                    <tr>
                    
                        <td bgcolor="#17BA3E" style="border-bottom-color: Blue; border-bottom-style: inset;
                            border-bottom-width: 2px" class="style2">
                            <div id="ShowNews" runat="server" visible="true" style="position: absolute; width: 40px;
                                height: 34px; top: 87px; left: 180px; vertical-align: middle; font-family: 'Arial';
                                font-size: small; font-weight: normal; font-style: normal; font-variant: normal;
                                text-transform: none; color: #FF0000">
                                <asp:Image ID="SuggIcon" runat="server" ImageUrl="~/Images/SuggIcon.png" Width="40px"
                                    Height="34px" />
                            </div>
                            <div style="margin-left: 70px; height: 15px; width: 211px;">
                                <asp:Label ID="TxtMain" runat="server" Font-Bold="True" Text="BUZON DE SUGERENCIAS"
                                    Font-Size="Small" ForeColor="White"></asp:Label>
                            </div>
                        </td>
                        
                        <td style="background-color: #00CC66;width:5px" ></td>
                    <td style="background-color: #00CC00;width:5px"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" style="padding-top: 25px; padding-left:24px" class="style1">
                <table style="width: 586px;" bgcolor="#003366">
                    <tr>
                        <td style="width: 200px; background-color: #99CCFF; padding-left:4px">
                            <asp:Label ID="lblTSugg" runat="server" Text="Tema de la sugerencia" ForeColor="#003366" Font-Size="Small"></asp:Label>
                        </td>
                        <td style="padding-left: 5px; background-color: White">
                            <asp:TextBox ID="TxtTem" runat="server" Width="377px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; background-color: #99CCFF; padding-left:4px">
                            <asp:Label ID="Label1" runat="server" Text="Nombre y Apellido" ForeColor="#003366" Font-Size="Small"></asp:Label>
                        </td>
                        <td style="padding-left: 5px; background-color: White">
                            <asp:TextBox ID="TextBox1" runat="server" Width="377px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; background-color: #99CCFF; padding-left:4px">
                            <asp:Label ID="Label2" runat="server" Text="Interno" ForeColor="#003366" Font-Size="Small"></asp:Label>
                        </td>
                        <td style="padding-left: 5px; background-color: White">
                            <asp:TextBox ID="TextBox2" runat="server" Width="377px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; background-color: #99CCFF; padding-left:4px">
                            <asp:Label ID="Label3" runat="server" Text="Asunto" ForeColor="#003366" Font-Size="Small"></asp:Label>
                        </td>
                        <td style="padding-left: 5px; background-color: White">
                            <asp:TextBox ID="TextBox3" runat="server" Width="377px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px; background-color: #99CCFF; padding-left:4px" valign="top">
                            <asp:Label ID="Label4" runat="server" Text="Mensaje" ForeColor="#003366" Font-Size="Small"></asp:Label>
                        </td>
                        <td style="padding-left: 5px; background-color: White">
                            <asp:TextBox ID="TextBox4" runat="server" Height="178px" Width="377px" 
								TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                <br />
                <div style="text-align:center; padding-right:15px">
                            <asp:Button id="btnSend" runat="server" Text="Enviar" onclick="btnSend_Click" />
                            </div>
            
            </td>
        </tr>
    </table>
</asp:Content>
