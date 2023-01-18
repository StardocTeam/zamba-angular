<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InsertControl.ascx.cs" Inherits="UC_Viewers_FormBrowser"  %>

<script type="text/javascript">
//    function SaveHtmlAndInsertDoc()
//    {
////        var ifrmdom = $('#<%=docViewer.ClientID %>').contents().find('body').html();
////        $('#<%=hdnIframe.ClientID %>').val(ifrmdom);
//        __doPostBack();
    //    }
    function Cerrar() {
        parent.eval('tb_remove()');
    }

    $(document).ready(function() {
        //$("#<%=btn_insertar.ClientID %>").click(function() {
        //    ShowLoadingAnimation();
        //});
        
        //Obtiene los textbox de los formularios, y les agrega en el foco la seleccion del texto.
        //Con esto se deberia solucionar el problema del "bloqueo de los textbox".
        $("input[type=text]").focus(function(){
            if(!($(this).hasClass("diabled") || $(this).hasClass("hasDatepicker") || 
                $(this).attr("disabled") || $(this).attr("readOnly")))
            {
                $(this).select();
            }
        });
    });

    
</script>

<div>
    <center>
        <asp:Literal runat="server" id="docViewer" Mode="PassThrough" ></asp:Literal>
        <div style="height:10px;white-space:nowrap;">
           <asp:Button runat="server" ID="btn_insertar" OnClick="btn_insertar_click" 
            Text="Grabar" UseSubmitBehavior="false" CssClass="insertFormButton"/>
           <% 
           if (Request["modal"] != null && bool.Parse(Request["modal"]))
           { 
           %>
            <input type="button" id="btnInsert" value="Cerrar" onclick="Cerrar();" 
            class="insertFormButton"/>
           <%
           }
           %>
        </div>          
        <asp:TextBox 
            ID="hdnIframe" runat="server" BackColor="White" BorderColor="White" 
            BorderStyle="None" ForeColor="White" Height="1px" Width="1px" />
        &nbsp;
        <div style="height:10px">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
    </center>
</div>
