<%@ Page Language="C#" AutoEventWireup="true" Inherits="Views_Security_Default" Codebehind="UsrPsswrd.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title title="Zamba Web"></title>
    <meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7; IE=EDGE,chrome=1" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/ZambaUIWebTables.css" />
    <%--    <link rel="Stylesheet" type="text/css" href="../../Content/Styles/jquery-ui-1.8.6.css" />--%>

    <%--<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">

    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>--%>


    <script src="../../scripts/jq_datepicker.js" type="text/javascript"></script>
    <script src="../../scripts/Zamba.Validations.js" type="text/javascript"></script>
    <script src="../../scripts/zamba.js" type="text/javascript"></script>
    <script src="../../scripts/Zamba.Fn.js" type="text/javascript"></script>
    <script src="../../scripts/thickbox-compressed.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CloseChangePassDlg() {
            parent.CloseChangePassDlg();
        }
    </script>


    <asp:PlaceHolder runat="server">

        <%: Styles.Render("~/bundles/Styles/jquery")%>
        <%: Styles.Render("~/bundles/Styles/bootstrap")%>
        <%: Styles.Render("~/Content/bootstrap-theme.css")%>
        <link href="../../Content/site.css" rel="stylesheet" />

        <%: Styles.Render("~/Content/bootstrap.css")%>
    </asp:PlaceHolder>


</head>

<body>
      

    <form id="form" runat="server" style="padding: 5px;">
           
            <table id='tblform' style="width: 100%;">

              <tr>

                  <td>

                         <div class="modal-header" style="background-color:#25557d; color:white; text-align:center;">
                <h4 class="modal-title" id="modalFormTitleTimeout1">Zamba Software - Cambiar Contraseña</h4>

            </div>
                      <br />
                      <br />
                      <br />


                  </td>

              </tr>


                <tr>

                    <td>
                        <div>
              
                            <asp:TextBox runat="server" TextMode="Password" ID='CurrentPassword' class="form-control" style=" width: 380px; position:relative; bottom:25px; padding-left: 10px;left:90px" placeholder="Clave Actual" />

                            <%--<input type="password" id="timeOutPassTxt" style="width: 380px; position:relative; bottom:25px; padding-left: 10px;left:60px" class="form-control" placeholder="Clave Actual">--%>

                        </div>
                        <br />
                        
                        <div>

                            <asp:TextBox runat="server" TextMode="Password" ID='NewPassword' class="form-control" style="width: 380px; position:relative; bottom:25px; padding-left: 10px;left:90px" placeholder="Nueva Clave" />

                            <%--<asp:TextBox runat="server" ID="NewPassword" TextMode="Password" class="NewPassword form-control" style="margin-bottom: 15px ;  margin-left: 13px;" />--%>
                        </div>
                        <br />
                     
                        <div>

                             <asp:TextBox runat="server" TextMode="Password" ID='NewPassword2' class="form-control" style="width: 380px; position:relative; bottom:25px; padding-left: 10px;left:90px" placeholder="Confirme Nueva Clave" />

                            <%--<asp:TextBox runat="server" ID="NewPassword2" TextMode="Password" class="NewPassword2 form-control" Style="margin-left: 13px;" />--%>


                        </div>

                        <br />
                       
    
  
                    </td>
                </tr>


                <tr>
                    <td colspan="2" style="text-align: center">

                 <div id="divError" style="display: none; padding-bottom: 5px" runat="server">
                            <asp:Label ID="lblMsj" runat="server" Visible="true" CssClass="error" Style="padding: 0px 0px 10px 0px"></asp:Label>
                        </div>


                    </td>
                </tr>


            </table>


         <asp:LinkButton runat="server" title="Guardar" ID="lnkGuardar" OnClick="lnkSave_clic"
                                            class="btn btn-primary" Style="float: left; margin-left: 90px; margin-top:auto">Guardar</asp:LinkButton>




    </form>
</body>

</html>
