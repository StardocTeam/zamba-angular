<%@ Control Language="C#" AutoEventWireup="false" CodeFile="Arbol.ascx.cs" Inherits="Arbol" %>

<div id="dialoginsertarform" style="display: none" title="Seleccione un formulario">
    <asp:DropDownList ID="ddltipodeform" runat="server" ToolTip="Seleccione el formulario deseado"  Width="200">

    </asp:DropDownList>
</div>

<nav class="navbar " id="spnToolbar" style="margin-bottom: 0; min-height:0">


    <asp:LinkButton runat="server" ID="RefreshClick"  OnClick="BtnRefresh_Click"  Cssclass="btn btn-xs">
    <span class="glyphicon glyphicon-refresh"></span>
    </asp:LinkButton >

    <button ID="btnInsertar" runat="server" ToolTip="Insertar" OnClick="$('#dialoginsertarform').dialog('open'); return false"   class="btn btn-xs">
     <span class="glyphicon glyphicon-edit"></span>
    </button>

 
</nav>

<div class="wf_main_toolbar" id="divTreeContainer" style=" width:100%">
    <asp:TreeView ID="ArbolProcesos" runat="server" ShowLines="true" 
        NodeIndent="3" ExpandImageUrl="~/Content/Images/toolbars/bullet_ball_blue.png"
       Height="100%" BorderStyle="None"  
               OnSelectedNodeChanged="ArbolProcesos_SelectedNodeChanged" RootNodeStyle-CssClass="RootAllKeys"
            ParentNodeStyle-CssClass="ParentAllKeys">
        <NodeStyle ImageUrl="~/Content/Images/toolbars/bullet_ball_blue.png" CssClass="StepTreeNode" />
        <SelectedNodeStyle BackColor="LightSteelBlue" ImageUrl="~/Content/Images/toolbars/bullet_ball_blue.png"
            Font-Bold="true" />
    </asp:TreeView>
</div>
   
<script type="text/javascript">

    $(document).ready(function () {
    function RefreshGridTree() {
        $("#<%=RefreshClick.ClientID %>").click();
        document.getElementById('<%=RefreshClick.ClientID%>').click();
    }

        $("#dialoginsertarform").dialog({
            bgiframe: true,
            modal: true,
            autoOpen: false,
            height: 170,
            buttons:
            {
                'Cancelar': function () {
                    $(this).dialog('close');
                }
                ,
                'Aceptar': function () {
                    $(this).dialog('close');
                    InsertForm($('#<%=ddltipodeform.ClientID%>').val(), true);
                }
            }
        });
    });



    function AddStepCountHandler() {
        if ($("#<%=ArbolProcesos.ClientID %>n0Nodes").length > 0) {
            $("a[href^='javascript:TreeView_ToggleNode']", $("#<%=ArbolProcesos.ClientID %>n0Nodes")).each(function () {
                this.href = this.href + ";LoadStepsCounts();";
            });
        }
    }

    <%-- Carga todas los count de las tareas --%>
    function LoadStepsCounts() {
        <%-- Obtiene todos los anchor que se encuentren en el arbol --%>
        if ($("#<%=ArbolProcesos.ClientID %>n0Nodes").length > 0) {
            try {
                ScriptWebServices.TasksService.set_defaultSucceededCallback(CountSuccess);
            } catch (e) { }

            var anchorHref, splitedValues, stepId, uId, aId;
            
            <%-- Se hace de esta forma, y no en una sola línea porque algunos navegadores renderizan 'display:block' y otros 'display: block' --%>
            <%-- Si conocen como resolver las tres primeras lineas de una manera mas eficiente, modificarlo y verificar con los tres navegadores --%>
            $("#<%=ArbolProcesos.ClientID %>n0Nodes div").each(function() {
                if($(this).css('display') == 'block') {
                    $(this).find("a").each(function () {
                        aId = $(this).attr("id");
                        anchorHref = $("#" + aId).attr("href");
                        splitedValues = anchorHref.split("\\");

                        <%-- Si el anchor es el que contiene el texto(si se hace split por \\ debe obtenerse la etapa y no debe finalizar con una 'i' --%>
                        if (splitedValues.length > 0 && aId.lastIndexOf('i') != aId.length - 1) {
                            stepId = splitedValues[splitedValues.length - 1].replace("')", "");

                            <%-- Deberia usar GTUID() de Zamba.js pero no puedo lograr referenciarlo correctamente. --%>
                            uId = GetUID();

                            try {
                                if (!stepId || stepId == '')
                                    stepId = 0;
                                else
                                    stepId = parseInt(stepId);

                                if (!uId || uId == '')
                                    uId = 0;
                                else
                                    uId = parseInt(uId);

                                if (!aId)
                                    aId = '';

                                try {
                                    ScriptWebServices.TasksService.GetTaskCount(stepId, uId, aId);
                                } catch (e) { }
                            }
                            catch (e) {
                                <%-- alert("Error al cargar el conteo de tareas, \n error:" + e); --%>
                            }
                        }
                    });
                }
            });
        } 
    }

    function CountSuccess(result) {
        <%-- El result del WS devuelve el ID del anchor que lo llamo y el count separados por | --%>
        var nodeID = result.split('|')[1];
        var nodeCount = result.split('|')[0];

        <%-- Obtiene el anchor que llamo al WS y agrega el count al texto --%>
        $("#" + nodeID).text($("#" + nodeID).text().replace("actualizando...", nodeCount));
    }

    function GetWFTree() {
        return $("#<%=ArbolProcesos.ClientID %>");
    }

    function FixWfTree() {
        <%-- Corrige error visual de TreeView --%>
        $('a[href$=_SkipLink]').each(function () {
            $(this).remove();
        });
    }
</script>
