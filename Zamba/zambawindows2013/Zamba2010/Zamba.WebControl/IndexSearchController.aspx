<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndexSearchController.aspx.cs" Inherits="IndexSearchController" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Visualizador de Indices</title>
    <link rel="stylesheet" type="text/css" />
</head>
<%--<script language="JavaScript" type="text/javascript">
    function GetCoordenates()
    {
	    if(HiddenField1.value != "")
	    {
		    var a = document.getElementById(HiddenField1.value);
            var X = a.top;
            var Y = a.left;
            CompPanel.X = X;
            CompPanel.Y = Y;
	    }
    }
    </script>
    <body onload="GetCoordenates()">
    --%>
<body>
    <form id="form1" runat="server">
    <div>
    &nbsp;<center>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    &nbsp;<asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource1"
                        Width="468px">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                            </asp:DropDownList>
                            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="btnSus_Click" Width="100px"></asp:TextBox>&nbsp;
                            <asp:DropDownList ID="DropDownList3" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:DataList><asp:ObjectDataSource ID="ObjectDataSource1"  TypeName="SearchServices" SelectMethod="GetIndexsSchema" runat="server"></asp:ObjectDataSource>
                    &nbsp; &nbsp;&nbsp;

        <asp:Button runat="server" ID="PopButton" BackColor="white" Text="" Width="0" Height="0"/>&nbsp;

        <asp:Button runat="server" ID="CompPopButton" BackColor="white" Text="" Width="0" Height="0"/>&nbsp;

        <ajaxToolkit:ModalPopupExtender ID="CompModal" runat="server"
        TargetControlID="CompPopButton"
        PopupControlID="CompPanel"
        DropShadow="false" 
        ></ajaxToolkit:ModalPopupExtender>
        
        <asp:Panel runat="server" ID="CompPanel" BackColor="white" >
            <asp:LinkButton ID="btnEqual" runat="server" Font-Overline="False" OnClick="btnComp_Click"></asp:LinkButton><br />
            <asp:LinkButton ID="btnMayor" runat="server" Font-Overline="False" OnClick="btnComp_Click"></asp:LinkButton><br />
            <asp:LinkButton ID="btnMayorIgual" runat="server" Font-Overline="False" OnClick="btnComp_Click">>=</asp:LinkButton><br />
            <asp:LinkButton ID="btnMenor" runat="server" Font-Overline="False" OnClick="btnComp_Click"><</asp:LinkButton><br />
            <asp:LinkButton ID="btnMenorIgual" runat="server" Font-Overline="False" OnClick="btnComp_Click"><=</asp:LinkButton><br />
            <asp:LinkButton ID="btnDistinto" runat="server" Font-Overline="False" OnClick="btnComp_Click"><></asp:LinkButton><br />
            <asp:LinkButton ID="btnEntre" runat="server" Font-Overline="False" OnClick="btnComp_Click">Entre</asp:LinkButton><br />
            <asp:LinkButton ID="btnNull" runat="server" Font-Overline="False" OnClick="btnComp_Click">Es Nulo</asp:LinkButton></asp:Panel>
    </center>
        <center>
            <ajaxToolkit:ModalPopupExtender ID="TableModal" runat="server"
            TargetControlID="PopButton"
            PopupControlID="TablePanel"
            DropShadow="false" 
            Y="0"> </ajaxToolKit:ModalPopupExtender>
            <asp:Panel ID="TablePanel" runat="server" Height="50px" Width="1000px">
                <asp:Table ID="TblSus" runat="server" BorderStyle="Solid" CssClass="SearchStyle" BackColor ="LightCyan" Height="86px" Width="146px">
                </asp:Table>
                <asp:RadioButton ID="RadioButton1" runat="server" />
            </asp:Panel>




 <ajaxToolkit:ModalPopupExtender ID="DropDown" runat="server"
        TargetControlID="DataList1"
        PopupControlID="DropDownPanel"
        DropShadow="false" 
        ></ajaxToolkit:ModalPopupExtender>
        
        <asp:Panel runat="server" ID="DropDownPanel" BackColor="white" >
            <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource2">
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="SearchServices" SelectMethod="GetComboList"></asp:ObjectDataSource>

            </asp:Panel>




                </ContentTemplate>
            </asp:UpdatePanel>
            &nbsp;

        </center>
    </div>
    </form>
</body></html>
