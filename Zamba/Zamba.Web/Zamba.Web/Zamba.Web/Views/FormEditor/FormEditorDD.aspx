<%@ Page Language="C#" AutoEventWireup="true" Inherits="Zamba.WebFormEditor.FormEditorDD" Codebehind="FormEditorDD.aspx.cs" %>

<%@ Import Namespace="System.Web.Optimization" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Zamba Form Editor</title>
    <style type="text/css">
        .TelerikModalOverlay {
            filter: progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=10) !important;
            background: black !important;
            opacity: .4 !important;
            -moz-opacity: .1 !important;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="Content/css/thickbox.css" />
    <link rel="Stylesheet" type="text/css" href="Content/css/jquery-ui-1.8.6.css" />
    <link rel="Stylesheet" type="text/css" href="Content/css/ZambaUIWeb.css" />
    <link rel="Stylesheet" type="text/css" href="Content/css/ZambaUIWebTables.css" />
    <link rel="stylesheet" type="text/css" href="Content/css/jq_datepicker.css" />
    <link rel="Stylesheet" type="text/css" href="Content/css/GridThemes/WhiteChromeGridView.css" />
    <link rel="stylesheet" href="Content/css/tabber.css" type="text/css" media="screen" />
    <link rel="Stylesheet" type="text/css" href="Content/css/GridThemes/GridViewGray.css" />
    <link href="Content/css/Zamba.css" type="text/css" rel="stylesheet" />
    <link href="Content/css/Zamba_table.css" type="text/css" rel="stylesheet" />

    <%--<script src="Content/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="Content/scripts/jquery-ui-1.8.6.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="Content/Scripts/jquery.quicksearch.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.validate.min.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.decimalMask.1.1.1.min.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.caret.1.02.min.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.dataTables.js"></script>
    <script type="text/javascript" src="Content/scripts/jquery.scrollTo.js"></script>--%>

    <%: Scripts.Render("~/bundles/jqueryCore") %>
    <%: Scripts.Render("~/bundles/jqueryAddIns") %>
    <%: Scripts.Render("~/bundles/jqueryval") %>
    <%: Scripts.Render("~/bundles/bootstrap") %>

    <script src="Content/scripts/jq_datepicker.js" type="text/javascript"></script>
    <script src="Content/scripts/thickbox-compressed.js" type="text/javascript"></script>
    <script type="text/javascript" src="Content/scripts/tabber.js"></script>
    <script src="Content/scripts/Zamba.js" type="text/javascript"></script>
    <script src="Content/scripts/Zamba.Fn.js" type="text/javascript"></script>
    <script src="Content/scripts/Zamba.Validations.js" type="text/javascript"></script>


</head>
<body class="BODY">
    <form id="form1" runat="server">
        <input id="hdnSelectedControl" type="hidden" />
        <telerik:RadScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
        <br />
        <table width="97%" summary="Resource Browser">
            <tr>
                <td style="padding-left: 20px; font-size: 12px; border-bottom-color: Gray; border-bottom-style: solid; border-bottom-width: 1px" valign="top">

                    <strong>Entidad:  <%=GetDocTypeName() %></strong>
                    Tipo:   &nbsp;        
                    <asp:DropDownList runat="server" ID="DropDownListFormTypes" Enabled="true">
                    </asp:DropDownList>
                </td>
                <td style="padding-left: 20px; border-bottom-color: Gray; border-bottom-style: solid; border-bottom-width: 1px" valign="top">
                    <asp:Button runat="server" ID="BtnSave" Text="Guardar" OnClick="BtnSave_Click" />
                    <asp:Button runat="server" ID="BtnGoBack" Text="Salir" OnClick="BtnGoBack_Click" />
                    <asp:Button runat="server" ID="BtnTest" Enabled="false" Text="Previsualizar en Internet Explorer" OnClick="BtnTestForm_Click" />
                    <br />
                    <asp:Label runat="server" ID="lblerror" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" style="width: 300px;" class="module">
                    <telerik:RadTreeView ID="RadTreeView1" runat="server" Height="90%" EnableDragAndDrop="True"
                        OnClientNodeDragStart="OnClientNodeDragStart" OnClientNodeDragging="OnClientNodeDragging"
                        OnClientNodeDropping="OnClientNodeDropping" OnClientNodeClicking="BeforeClick"
                        Width="300px" Skin="Windows7" Font-Size="10px" ShowLineImages="true">
                    </telerik:RadTreeView>
                </td>
                <td style="padding-left: 20px; width: 100%; border-bottom-color: Gray; border-bottom-style: solid; border-bottom-width: 1px" valign="top">
                    <telerik:RadEditor OnClientLoad="OnClientLoad" SkinID="BasicSetOfTools" Width="100%"
                        Height="650" ID="RadEditor1" runat="server" AllowScripts="True" Skin="Windows7"
                        OnClientSelectionChange="OnClientSelectionChange" ContentFilters="None">
                        <Modules>
                            <telerik:EditorModule Name="RadEditorDomInspector" Visible="true" Enabled="true" />
                            <telerik:EditorModule Name="RadEditorNodeInspector" Visible="true" Enabled="true" />
                            <telerik:EditorModule Name="MyModule" Enabled="true" Visible="true" />
                        </Modules>
                        <Content>
	 <html>                
	 <head>                
	
  

	<link rel="stylesheet" type="text/css" href="Content/css/Zamba.css"/>
	<link rel="stylesheet" type="text/css" href="Content/css/Zamba_table.css"/>

	<script type="text/javascript" src="../../scripts/jquery.quicksearch.js"></script>
	<script type="text/javascript" src="../../scripts/jquery.decimalMask.1.1.1.min.js"></script>
	<script type="text/javascript" src="../../scripts/jquery.dataTables.js"></script>
	<script type="text/javascript" src="../../scripts/jquery.scrollTo.js"></script>
  
	</head>                
	 <body>                
	 <form>

	 <div style="border-style:solid;border-color:black;border-width:1px">
	 
	

	<div ><strong>Titulo</strong></div>


<br />
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
  
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<input type="submit" class="btn btn-primary" id="zamba_save"  name="zamba_save" value="Guardar"></input>
<input type="submit" class="btn btn-primary" id="zamba_cancel"  name="zamba_cancel" value="Cancelar"></input>
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />&nbsp;&nbsp;
<br />


 </div>


</form>
	 </body>                

	 </html>                


                        </Content>
                    </telerik:RadEditor>
                </td>
                <td></td>
            </tr>
        </table>
        <script type="text/javascript">
            //<![CDATA[

            //------------------------MODULO DE EDICION DE ATRIBUTOS DEL INDICE------------------------

            //Declaracion e instanciacion del modulo de edicion de atributos de indices
            MyModule = function (element) {
                MyModule.initializeBase(this, [element]);
            }
            MyModule.prototype = {
                initialize: function () {
                    MyModule.callBaseMethod(this, 'initialize');

                    var element = this.get_element();
                    var html = "<div id=\"divIndexAttr\" class=\"reModule\"><table>";

                    //Fila que contiene el valor por defecto, tipo de dato y longitud
                    html += "<tr><td class=\"reModuleLabel\"><label>Valor por defecto</label></td>";
                    html += "<td><input id=\"txtDefaultValue\" style=\"width:100px;\" /></td>";
                    html += "<td><label class=\"reModuleLabel\">Tipo de dato</label></td>";
                    html += "<td><select id=\"txtDataType\" style=\"width:100px;\">";
                    html += "<option></option><option>numeric</option><option>date</option><option>decimal_2_16</option></select></td>";
                    html += "<td class=\"reModuleLabel\"><label>Longitud</label></td>";
                    html += "<td><input id=\"txtLength\" style=\"width:100px;\" /></td>";
                    html += "<td rowSpan=\"2\"><input id=\"btnUpdateAttr\" type=\"submit\" value=\"  Guardar  \" onclick=\"UpdateAttr();return false;\" /></td></tr>";

                    //Fila que contiene la configuración de requerido, solo lectura y deshabilitado
                    html += "<tr><td class=\"reModuleLabel\"><label>Requerido</label></td>";
                    html += "<td><input id=\"chkRequired\" type=\"checkbox\"/></td>";
                    html += "<td class=\"reModuleLabel\"><label>Solo lectura</label></td>";
                    html += "<td><input id=\"chkReadOnly\" type=\"checkbox\"/></td>";
                    html += "<td class=\"reModuleLabel\"><label>Deshabilitado</label></td>";
                    html += "<td><input id=\"chkDisabled\" type=\"checkbox\"/></td></tr></table></div>";
                    element.innerHTML = html;
                }
            };
            MyModule.registerClass('MyModule', Telerik.Web.UI.Editor.Modules.ModuleBase);

            //Carga los atributos del elemento seleccionado del RadEditor
            function OnClientSelectionChange(editor, args) {
                try {
                    var html = editor.getSelectionHtml();
                    var element = GetFirstIndexElement(html);

                    if (element != null) {
                        $("#divIndexAttr").style.display = "block";

                        //Se obtienen los atributos del elemento.
                        var valDefault = element.getAttribute("DefaultValue");
                        var valRequired = element.className.indexOf("isRequired");
                        var valReadOnly = element.getAttribute("readonly");
                        var valDisabled = element.getAttribute("disabled");
                        var valLength = element.getAttribute("length");
                        var valDatatyype = element.getAttribute("dataType");

                        //Se completan los atributos del índice.
                        if (valRequired != -1) {
                            $("#chkRequired").checked = true;
                        } else {
                            $("#chkRequired").checked = false;
                        }
                        if (valReadOnly == "readonly") {
                            $("#chkReadOnly").checked = true;
                        } else {
                            $("#chkReadOnly").checked = false;
                        }
                        if (valDisabled == "disabled") {
                            $("#chkDisabled").checked = true;
                        } else {
                            $("#chkDisabled").checked = false;
                        }
                        if (valDefault) {
                            $("#txtDefaultValue").val(valDefault);
                        } else {
                            $("#txtDefaultValue").val('');
                        }
                        if (valLength) {
                            $("#txtLength").val(valLength);
                        } else {
                            $("#txtLength").val('');
                        }
                        if (valDatatyype) {
                            $("#txtDataType").val(valDatatyype);
                        } else {
                            $("#txtDataType").val('');
                        }
                    }
                    else {

                        $("#divIndexAttr").style.display = "none";
                    }
                } catch (e) {

                    $("#divIndexAttr").style.display = "none";
                }
            }

            //Actualiza los atributos de un elemento del RadEditor
            function UpdateAttr() {

                try {
                    //Obtiene el elemento sin modificar
                    var html = $("#hdnSelectedControl").val();
                    var element = GetFirstIndexElement(html);

                    if (element) {
                        //Seteo de variables
                        var elemClass = '';
                        var valDefault = $("#txtDefaultValue").val()
                        var valLength = $("#txtLength").val();
                        var valData = $("#txtDataType").val();

                        //Se asignan las clases y atributos
                        if ($("#chkRequired").checked) {
                            elemClass += 'isRequired ';
                        }
                        if ($("#chkReadOnly").checked) {
                            element.setAttribute("readonly", "readonly");
                        } else {
                            element.removeAttribute("readonly");
                        }
                        if ($("#chkDisabled").checked) {
                            element.setAttribute("disabled", "disabled");
                        } else {
                            element.removeAttribute("disabled");
                        }
                        if (valDefault != null && valDefault != '') {
                            elemClass += 'haveDefaultValue ';
                            element.setAttribute("DefaultValue", valDefault);
                        } else {
                            element.removeAttribute("DefaultValue");
                        }
                        if (valLength != null && valLength != '') {
                            elemClass += 'length ';
                            element.setAttribute("length", valLength);
                        } else {
                            element.removeAttribute("length");
                        }
                        if (valData != null && valData != '') {
                            elemClass += 'dataType ';
                            element.setAttribute("dataType", valData);
                        } else {
                            element.removeAttribute("dataType");
                        }
                        element.className = elemClass;

                        //Se convierte el elemento a codigo html
                        var aux = document.createElement('html');
                        aux.appendChild(element.cloneNode(true));
                        var newElement = aux.innerHTML;

                        //Se obtiene el html original
                        aux = $find("<%=RadEditor1.ClientID%>").get_html(true);

                        //Se quita el elemento antiguo
                        var idTagPosition = aux.indexOf(element.id.toString());
                        var from = aux.substring(0, idTagPosition);
                        from = from.lastIndexOf("<");
                        var to = aux.substr(from);
                        to = from + to.indexOf(">") + 1;

                        //Se inyecta el elemento nuevo
                        aux = aux.substring(0, from) + newElement + aux.substring(to);

                        //Se actualiza el contenido del RadEditor con el elemento modificado
                        $find("<%=RadEditor1.ClientID%>").set_html(aux);

                } else {
                    alert("Ha ocurrido un error al modificar las propiedades del atributo");
                }
            } catch (e) {
                alert("Ha ocurrido un error al modificar las propiedades del atributo");
            }
        }

        //Obtiene el primer elemento de tipo índice que encuentre dentro de código html. 
        //Si no encuentra nada o genera error devuelve null.
        function GetFirstIndexElement(html) {
            if (html) {
                try {
                    //Se obtiene el primer control seleccionado
                    html = html.substring(html.indexOf("<"), html.indexOf(">") + 1).trim();

                    //Se genera un div para inyectarle el código html obtenido
                    var div = document.createElement('div');
                    div.innerHTML = html;

                    //Se guarda el control sin modificar.
                    //Se utiliza para luego ubicarlo en el codigo y realizar los reemplazos.
                    $("#hdnSelectedControl").val(html);

                    //Se retorna el elemento seleccionado en formato control
                    var indexElem = div.firstChild;

                    //Se valida que sea un índice
                    if (indexElem.id.toLowerCase().indexOf("zamba_index_") != -1) {
                        //En caso de ser de tipo select, se ocultan campos no configurables
                        if (indexElem.tagName == "SELECT") {
                            $("#txtLength").disabled = true;
                            $("#txtDataType").disabled = true;
                            $("#chkReadOnly").disabled = true;
                        } else {
                            $("#txtLength").disabled = false;
                            $("#txtDataType").disabled = false;
                            $("#chkReadOnly").disabled = false;
                        }
                    } else {
                        return null;
                    }

                    //Se devuelve el índice válido
                    return indexElem;

                } catch (e) {
                    return null;
                }
            } else {
                return null;
            }
        }

        /* ================== Utility events needed for the Drag/Drop ===============================*/
        function OnClientLoad(editor) {
            var tree = $find("<%= RadTreeView1.ClientID %>");
            makeUnselectable(tree.get_element());

            //Oculta la barra de edicion de atributos de índices hasta seleccionar algo sobre el formulario
            if ($("#divIndexAttr") != null)
                $("#divIndexAttr").style.display = "none";
        }

        function OnClientNodeDragStart() {
            setOverlayVisible(true);
        }

        //ML: Evento que se dispara al soltar el nodo sobre el editor.
        //Hay que agregar las categorias nuevas que se generen en los nodos.
        function OnClientNodeDropping(sender, args) {
            var editor = $find("<%=RadEditor1.ClientID%>");
		    var event = args.get_domEvent();
		    var tagSrc;

		    document.body.style.cursor = "default";

		    var result = isMouseOverEditor(editor, event);

		    if (result) {
		        var category = args.get_sourceNode()._element._item._attributes._data.Category;

		        if (category == "Image") {
		            var imageSrc = args.get_sourceNode().get_value();
		            if (imageSrc && (imageSrc.indexOf(".gif") != -1 || imageSrc.indexOf(".jpg") != -1 || imageSrc.indexOf(".png") != -1)) {
		                var imageSrc = "<img src='" + imageSrc + "'>";
		                editor.setFocus();
		                editor.pasteHtml(imageSrc);
		            }
		        }

		        if (category == "Index" || category == "DocAsociated" || category == "DocAsociatedIndex" || category == "Control") {
		            tagSrc = args.get_sourceNode()._element._item._attributes._data.Tag;
		            if (tagSrc) {
		                editor.setFocus();
		                editor.pasteHtml(tagSrc);
		            }
		        }

		        //ML: Hay que agregar un popup que liste en un arbol, los WF, Etapas y Reglas existentes, que permita elejir una regla y agregar el ID de Regla al Tag.
		        if (category == "BtnExecuteRule") {
		            tagSrc = args.get_sourceNode()._element._item._attributes._data.Tag;
		            if (tagSrc) {
		                editor.setFocus();
		                editor.pasteHtml(tagSrc);
		            }
		        }


		        //ML: Hay que agregar un popup con un editor que permita editar los js y css.
		        if (category == "Script") {
		            tagSrc = args.get_sourceNode()._element._item._attributes._data.Tag;
		            if (tagSrc) {
		                window.open(tagsrc, _blank);
		            }
		        }

		    }

		    setOverlayVisible(false);
		}

		function OnClientNodeDragging(sender, args) {
		    var editor = $find("<%=RadEditor1.ClientID%>");
		    var event = args.get_domEvent();

		    if (isMouseOverEditor(editor, event)) {
		        document.body.style.cursor = "hand";
		    }
		    else {
		        document.body.style.cursor = "no-drop";
		    }
		}

		/* ================== Utility methods needed for the Drag/Drop ===============================*/

		//Make all treeview nodes unselectable to prevent selection in editor being lost
		function makeUnselectable(element) {
		    var nodes = element.getElementsByTagName("*");
		    for (var index = 0; index < nodes.length; index++) {
		        var elem = nodes[index];
		        elem.setAttribute("unselectable", "on");
		    }
		}

		//Create and display an overlay to prevent the editor content area from capturing mouse events
		var shimId = null;
		function setOverlayVisible(toShow) {
		    if (!shimId) {
		        var div = document.createElement("DIV");
		        document.body.appendChild(div);
		        shimId = new Telerik.Web.UI.ModalExtender(div);
		    }

		    if (toShow) shimId.show();
		    else shimId.hide();
		}

		//Check if the image is over the editor or not
		function isMouseOverEditor(editor, events) {
		    var editorFrame = editor.get_contentAreaElement();
		    var editorRect = $telerik.getBounds(editorFrame);

		    var mouseX = events.clientX;
		    var mouseY = events.clientY;

		    if (mouseX < (editorRect.x + editorRect.width) &&
			 mouseX > editorRect.x &&
				mouseY < (editorRect.y + editorRect.height) &&
			 mouseY > editorRect.y) {
		        return true;
		    }
		    return false;
		}

		/* ================== These two methods are not related to the drag/drop functionality, but to the preview functionality =======*/

		function Scale(img, width, height) {
		    var hRatio = img.height / height;
		    var wRatio = img.width / width;

		    if (img.width > width && img.height > height) {
		        var ratio = (hRatio >= wRatio ? hRatio : wRatio);
		        img.width = (img.width / ratio);
		        img.height = (img.height / ratio);
		    }
		    else {
		        if (img.width > width) {
		            img.width = (img.width / wRatio);
		            img.height = (img.height / wRatio);
		        }
		        else {
		            if (img.height > height) {
		                img.width = (img.width / hRatio);
		                img.height = (img.height / hRatio);
		            }
		        }
		    }
		}

		function BeforeClick(sender, args) {
		    var node = args.get_node();
		    var object = document.createElement("IMG");
		    object.src = node.get_value();
		    if (node.get_attributes().getAttribute("Category") == "Folder") {
		        return;
		    }
		}

		//]]>
        </script>
    </form>
</body>
</html>
