<%@ Control Language="C#" AutoEventWireup="false" Inherits="Arbol" CodeBehind="Arbol.ascx.cs" %>
<script>

</script>
<div id="dialoginsertarform" style="display: none" title="Seleccione un formulario">
    <asp:DropDownList ID="ddltipodeform" runat="server" ToolTip="Seleccione el formulario deseado" CssClass="form-control" Width="350">
    </asp:DropDownList>
</div>

<nav class="navbar " id="spnToolbarTasksTree" style="margin-bottom: 0; min-height: 0">

    <a id="btnCloseSidePanelTasksList" class="btn btn-md" style="display: none;">
        <span id="imgBtnCloseSidePanel" class="glyphicon glyphicon-menu-left btnToggleSidePanel"></span>
    </a>

    <asp:LinkButton runat="server" ID="RefreshClick" OnClick="BtnRefresh_Click" CssClass="btn btn-xs">
    <span class="glyphicon glyphicon-refresh"></span>
    </asp:LinkButton>

    <button id="btnInsertar" runat="server" tooltip="Insertar" onclick="openTreeUIModal();" class="btn btn-xs">
        <span class="glyphicon glyphicon-edit"></span>
    </button>


    <input type="text" id="SearchInTree" placeholder=" Buscar.." class="form-control input-xs" />
    <style>
        .highlight {
            background: #ececec;
        }

        #SearchInTree {
            width: 210px !important;
            height: 25px;
            bottom: 22px !important;
            position: relative;
            left: 60px;
        }
    </style>

    <script type="text/javascript">

        function pageLoad(sender, args) {

            var treeTables = $('#ContentPlaceHolder_Arbol_ArbolProcesos>div>div').children().find("tr");
            var titleTables = $('#ContentPlaceHolder_Arbol_ArbolProcesos>div').children("table");

            $('#SearchInTree').on('keyup', function () {
                var val = $(this).val().toLowerCase();
                if (val) {
                    titleTables.css("display", "none");
                    treeTables.css("display", "none");
                    /* Busca en los nodos padres   */
                    for (i = 0; i < treeTables.length; i++) {
                        if ($(titleTables[i]).text().toLowerCase().trim().indexOf(val) !== -1) {
                            $(titleTables[i]).css("display", "block");
                        }
                    }
                    /* Busca en los nodos hijos   */
                    for (i = 0; i < treeTables.length; i++) {
                        if ($(treeTables[i]).text().toLowerCase().trim().indexOf(val) !== -1) {
                            $(treeTables[i]).css("display", "block");
                            $('#ContentPlaceHolder_Arbol_ArbolProcesos>div>div').css("display", "block");//se expanden
                            $($(treeTables[i]).parents("div")[0]).prev().css("display", "block");
                        }
                    }
                }
                else {
                    /* Si el buscador esta vacio restauro el arbol*/
                    treeTables.css("display", "block");
                    $('#ContentPlaceHolder_Arbol_ArbolProcesos>div>div').css("display", "none");
                    $('#ContentPlaceHolder_Arbol_ArbolProcesos>div').children("table").css("display", "block");

                }
            })

        }

        function openTreeUIModal() {
            $("#dialoginsertarform").dialog({
                bgiframe: true,
                modal: true,
                autoOpen: false,
                height: 170,
                width: 450,
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

            $(".ui-dialog-titlebar-close").addClass("btn");
            $(".ui-dialog .ui-dialog-buttonpane button").addClass("btn btn-primary")
            $(".ui-dialog").css({
                "background-color": "white",
                "border": "1px solid #b9b9b9"
            });
            $("#dialoginsertarform").dialog('open');
        }

        function RefreshGridTree() {
            $("#<%=RefreshClick.ClientID %>").click();
            document.getElementById('<%=RefreshClick.ClientID%>').click();
        }

        function AddStepCountHandler() {
            if ($("#<%=ArbolProcesos.ClientID %>n0Nodes").length > 0) {
                $("a[href^='javascript:TreeView_ToggleNode']", $("#<%=ArbolProcesos.ClientID %>n0Nodes")).each(function () {
                    this.href = this.href + ";LoadStepsCounts();";
                });
                LoadStepsCounts();
            }
        }

        //Carga todas los count de las tareas 
        function LoadStepsCounts() {
            // Obtiene todos los anchor que se encuentren en el arbol 
            if ($("#<%=ArbolProcesos.ClientID %>n0Nodes").length > 0) {
                //try {
                //    ScriptWebServices.TaskService.set_defaultSucceededCallback(CountSuccess);
                //} catch (e) { }
                GetUsedFilters(); //marcosarbol

                var anchorHref, splitedValues, stepId, uId, aId;
                uId = GetUID();
                // Se hace de esta forma, y no en una sola línea porque algunos navegadores renderizan 'display:block' y otros 'display: block' --%>
                // Si conocen como resolver las tres primeras lineas de una manera mas eficiente, modificarlo y verificar con los tres navegadores --%>
                $("#<%=ArbolProcesos.ClientID %>n0Nodes div").each(function () {
                    if ($(this).css('display') == 'block') {

                        var stepIdArray = [], nodeIdArray = [];
                        $(this).find("a[class]").each(function () {
                            var nodeName = $(this).parents("table").text().trim();
                            if (nodeName.endsWith("(...)")) {
                                aId = $(this).attr("id");
                                anchorHref = $("#" + aId).attr("href");
                                splitedValues = anchorHref.split("\\");
                                if (splitedValues.length > 0 && aId.lastIndexOf('i') != aId.length - 1) {
                                    stepId = splitedValues[splitedValues.length - 1].replace("')", "");
                                    try {
                                        if (!stepId || stepId == '')
                                            stepId = 0;
                                        else
                                            stepId = parseInt(stepId);
                                        if (!uId || uId == '')
                                            uId = 0;
                                        else
                                            uId = parseInt(uId);
                                        if (!aId) aId = '';
                                        stepIdArray.push(stepId);
                                        nodeIdArray.push(aId);
                                        //try {                                         
                                        //    $.ajax({
                                        //        url: "../../Services/TaskService.asmx/GetTaskCount",
                                        //        type: "POST",
                                        //        data: { stepId: stepId, usrId: uId, nodeId: aId },
                                        //        success: function (d) {
                                        //            CountSuccess(xml2json(d));
                                        //        },
                                        //        error: function (xhr, ajaxOptions, thrownError) {
                                        //            throw xhr;
                                        //        }
                                        //    });
                                        //} catch (e) { }
                                    }
                                    catch (e) {
                                        // alert("Error al cargar el conteo de tareas, \n error:" + e); --%>
                                    }
                                }
                            }
                        });
                        if (stepIdArray.length && nodeIdArray.length) {
                            try {                              
                                $.ajax({
                                    url: "../../Services/TaskService.asmx/GetTaskCountArray",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    data: JSON.stringify({ 'usrId': uId, 'nodeIdArray': nodeIdArray, 'stepIdArray': stepIdArray }),
                                    success: function (d) {
                                        _.each(d.d, function (a) {
                                            CountSuccess(a);
                                        })
                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                        throw xhr;
                                    }
                                });
                            } catch (e) { }
                        }
                    }
                });
            }

            // si existe el arbol le establezco alto de la pantalla menos barra botones
            if ($('#divTreeContainer').length > 0) {
                $('#divTreeContainer').css('overflow', 'auto').height($("#tabtasklist").height() - $("#spnToolbarTasksTree").outerHeight());
            }
            // A la grilla tambien
            if ($('#TaskGridContent').length) {
                $('#TaskGridContent').css('overflow', 'auto');
                var navbarFilters = $('#ContentPlaceHolder_TaskGrid_ucTaskGrid_pnlFilters');

                if (navbarFilters.length && navbarFilters.outerHeight() > 0) {
                    $('#TaskGridContent').height($("#tabtasklist").height() - navbarFilters.outerHeight());
                }
                else {
                    $('#TaskGridContent').height($('#divTreeContainer').height());
                }
            }
        }

        function CountSuccess(result) {
            if (result == undefined) return;
            // El result del WS devuelve el ID del anchor que lo llamo y el count separados por | --%>
            if (typeof (result) == "object") {
                result = result.string;
            }
            var nodeID = result.split('|')[1];
            var nodeCount = result.split('|')[0];

            // Obtiene el anchor que llamo al WS y agrega el count al texto --%>   
            $("#" + nodeID).text($("#" + nodeID).text().replace("...", nodeCount));
        }

        function GetWFTree() {
            return $("#<%=ArbolProcesos.ClientID %>");
        }

        function FixWfTree() {
            // Corrige error visual de TreeView --%>
            $('a[href$=_SkipLink]').each(function () {
                $(this).remove();
            });
        }

        function xml2json(xml) {
            try {
                var obj = {};
                //En caso de IE
                if (xml.children == undefined) return xml.childNodes[0].firstChild.data;
                if (xml.children.length > 0) {
                    for (var i = 0; i < xml.children.length; i++) {
                        var item = xml.children.item(i);
                        var nodeName = item.nodeName;

                        if (typeof (obj[nodeName]) == "undefined") {
                            obj[nodeName] = xml2json(item);
                        } else {
                            if (typeof (obj[nodeName].push) == "undefined") {
                                var old = obj[nodeName];

                                obj[nodeName] = [];
                                obj[nodeName].push(old);
                            }
                            obj[nodeName].push(xml2json(item));
                        }
                    }
                } else {
                    obj = xml.textContent;
                }
                return obj;//.string;
            } catch (e) {
                console.error(e);
            }
        }

    </script>
</nav>
<asp:HiddenField ID="StepId" runat="server" />

<div class="wf_main_toolbar" id="divTreeContainer" style="width: 100%">
    <span class="btn btn-default" style="display: none" onclick="GetUsedFilters()"></span>

    <asp:TreeView ID="ArbolProcesos" runat="server" ShowLines="false"
        NodeIndent="3"
        LineImagesFolder="~/Content/Images/TreeLineImages"
        Height="100%" BorderStyle="None"
        OnSelectedNodeChanged="ArbolProcesos_SelectedNodeChanged"
        RootNodeStyle-CssClass="RootAllKeys"
        ParentNodeStyle-CssClass="ParentAllKeys">
        <ParentNodeStyle ImageUrl="~/Content/Images/appbar.marvel.ironman2.png" />
        <RootNodeStyle ImageUrl="~/Content/Images/appbar.marvel.ironman2.png" />
        <NodeStyle ImageUrl="~/Content/Images/step.png" CssClass="StepTreeNode" />
        <SelectedNodeStyle BackColor="LightSteelBlue" ImageUrl="~/Content/Images/appbar.marvel.ironman2.png"
            Font-Bold="true" />
        <HoverNodeStyle Font-Bold="true" />
    </asp:TreeView>
</div>



