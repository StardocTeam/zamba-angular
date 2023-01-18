<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Views_UC_WF_Rules_UCDoShowTable" CodeBehind="UCDoShowTable.ascx.cs" %>
<%@ Import Namespace="System.Web.Optimization" %>



<link rel="Stylesheet" type="text/css" href="../../Content/Styles/GridThemes/GridViewGray.css?v=249" />
<script type="text/javascript" src="../../Scripts/jquery.quicksearch.min.js?v=248"></script>

<%--<script type="text/javascript" src="https://code.jquery.com/jquery-migrate-3.0.0.js"></script>--%>
<%--<script type="text/javascript" src="https://code.jquery.com/jquery-migrate-3.0.0.min.js"></script>--%>


<script src="../../Scripts/jquery-migrate-3.0.0.js"></script>


<script type="text/javascript">

    var doubleclickevent = false;
    var EnterEvent = false;
    var pos = 0;

    $(document).ready(function (e) {


        //var ModalIF = document.getElementById("openModalIFContentUcRules");

        //ModalIF.parentElement.style.minHeight = "562px";
        //ModalIF.parentElement.style.maxHeight = "662px";
        //ModalIF.parentElement.style.minWidth = "1000px";
        //ModalIF.parentElement.style.maxWidth = "1200px";

        $(".Body-Master-Blanck").css("overflow", "hidden");

        var tblControl = document.getElementById("<%=dgValue.ClientID %>")
        var lstControls = tblControl.getElementsByTagName("input");

        $('.mGrid tr').single_double_click(function (event) {
            if (event.target.type !== 'checkbox') {
                $(':checkbox', this).trigger('click');

                $(':checkbox', this).focus();
            }

        }, function (event) {
            if (event.target.type !== 'checkbox') {
                doubleclickevent = true;
                $(':checkbox', this).trigger('click');

            }
        });

        InitializeTable();
        $('input#search_dgValue').quicksearch('.mGrid.notFixed tbody tr ');
        FormatTable();

        $('#openModalIFContentUcRules').dialog({ height: 'auto', width: '72%' });
        document.getElementById("openModalIFContentUcRules").style.minWidth = "1000px";
        document.getElementById("openModalIFContentUcRules").style.maxWidth = "1350px";
        document.getElementById("openModalIFContentUcRules").style.minHeight = "500px";
        document.getElementById("openModalIFContentUcRules").style.maxHeight = "662px";
        document.getElementById("openModalIFContentUcRules").parentElement.style.minWidth = "1000px";
        document.getElementById("openModalIFContentUcRules").parentElement.style.maxWidth = "1350px";
        document.getElementById("openModalIFContentUcRules").parentElement.style.minHeight = "530px";
        document.getElementById("openModalIFContentUcRules").parentElement.style.maxHeight = "687px";
        document.getElementById("openModalIFContentUcRules").parentElement.style.left = "12%";
        document.getElementById("openModalIFContentUcRules").parentElement.style.top = "20px";
        document.getElementById("openModalIFContentUcRules").style.overflow = "hidden";

        $('.ui-dialog.ui-corner-all.ui-widget.ui-widget-content.ui-front.ui-draggable.ui-resizable').css('z-index', '9999');
        $('.ui-dialog.ui-corner-all.ui-widget.ui-widget-content.ui-front.ui-draggable.ui-resizable').css('top', '38px');

        $('.modal-backdrop.fade.in').css('display', 'none');

        $('#ui-id-1.ui-dialog-title').html($('#ctl00_ContentPlaceHolder_UC_WFExecution_pnlUcRules').attr('title'));
        $('.ui-dialog-titlebar-close').css("display", "none");

        var justShow = $("#<%=hdnJustShow.ClientID %>").val();
        //Si la tabla es de solo consulta se habilita el botón OK
        if (justShow == 'True') {
            $('#<%=_btnok.ClientID %>').removeAttr("disabled");
        }


    });



    jQuery.fn.single_double_click = function (single_click_callback, double_click_callback, timeout) {
        return this.each(function () {
            var clicks = 0, self = this;
            jQuery(this).click(function (event) {
                clicks++;
                if (clicks == 1) {
                    setTimeout(function () {
                        if (clicks == 1) {
                            single_click_callback.call(self, event);
                        } else {
                            double_click_callback.call(self, event);
                        }
                        clicks = 0;
                    }, timeout || 400);
                }
            });
        });
    }


    function InitializeTable() {


        var grid = $("#ContentPlaceHolder_UC_WFExecution_ShowDoShowTable_dgValue");

        //Por cada GridView que se encuentre modificar el código HTML generado para agregar el THEAD.
        if (grid.find("tbody > tr > th").length > 0) {
            grid.find("tbody").before("<thead><tr></tr></thead>");
            grid.find("thead:first tr").append(grid.find("th"));
            //grid.find("tbody tr:first").remove();
        }

        //Cuando no hay datos se acondiciona la apariencia
        if (grid.find("tr").length == 1) {
            $("#GridContainer").css("overflow", "hidden");
            $("#GridContainer").css("height", "50px");
            $('#<%=_btnok.ClientID %>').attr("disabled", "disabled");
        }

        var justShow = $("#<%=hdnJustShow.ClientID %>").val();
        //Si la tabla es de solo consulta se habilita el botón OK
        if (justShow == 'True') {
            $('#<%=_btnok.ClientID %>').removeAttr("disabled");
        }
    }

    function UnCheckAll() {
        //Obtenemos la tabla del gridView
        var tblControl = document.getElementById("<%=dgValue.ClientID %>")
        //Extraemos todos los elementos del tag input.
        var lstControls = tblControl.getElementsByTagName("input");

        //Descheckeamos todos los checkbox 
        for (var i = 0; i < lstControls.length; i++) {
            if (lstControls[i].checked) {
                lstControls[i].checked = false;
                $(lstControls[i]).parent().parent().removeClass("backColor");
            }
        }
    }

    function CheckFuncionality(chkObject) {

        //Al seleccionar un item se habilita el botón OK
        $('#<%=_btnok.ClientID %>').removeAttr("disabled");
        var isMultipleCheck = $("#<%=hdnMultipleCheck.ClientID %>").val().toLowerCase();
        var selectedChecksIndexes = "";

        var anyChecked = false;

        if (isMultipleCheck == "false") {
            //Obtenemos la tabla del gridView
            var tblControl = document.getElementById("<%=dgValue.ClientID %>")
            //Extraemos todos los elementos del tag input.
            var lstControls = tblControl.getElementsByTagName("input");

            //Descheckeamos todos los checkbox 
            for (var i = 0; i < lstControls.length; i++) {
                if (lstControls[i].checked) {
                    if (lstControls[i].id == chkObject.id) {
                        selectedChecksIndexes = i;
                        pos = i;
                    }

                }
                lstControls[i].checked = false;
                $(lstControls[i]).parent().parent().removeClass("backColor");

            }

            //Checkeamos el control clickeado
            chkObject.checked = true;
            $(chkObject).parent().parent().addClass("backColor")
            anyChecked = true;
        }
        else {
            //Obtenemos la tabla del gridView    
            var max = lstControls.length;
            var i = 0;
            //Agregamos los indices de los checks checkeados en el hdnChecks
            for (i = 0; i < max; i++) {
                if (lstControls[i].id.indexOf("chkSelected", 0) != -1) {
                    if (lstControls[i].checked) {
                        anyChecked = true;
                        selectedChecksIndexes += (selectedChecksIndexes !== "" ? "," : "") + i;
                    }
                }
            }

        }
        var justShow = $("#<%=hdnJustShow.ClientID %>").val();

        if (anyChecked || justShow == 'True') {
            $('#<%=_btnok.ClientID %>').removeAttr("disabled");
        }
        else {
            $('#<%=_btnok.ClientID %>').attr("disabled", "disabled");
        }

        getHdnChecks().val(selectedChecksIndexes);

        if (doubleclickevent) {
            chkObject.checked = false;
            $('#<%=_btnok.ClientID %>').trigger('click');
        }

        return true;
    }



    $(document).keydown(function (e) {
        var tblControl = document.getElementById("<%=dgValue.ClientID %>")
        var lstControls = tblControl.getElementsByTagName("input");

        var keyCode = e.keyCode || e.which;
        var arrow = { left: 37, up: 38, right: 39, down: 40 };
        switch (keyCode) {
            case arrow.left:
                break;
            case arrow.up:

                if (pos > 0)
                    pos -= 1;

                lstControls[pos].checked = true;
                $(lstControls[pos]).focus();
                CheckFuncionality(lstControls[pos])
                break;

            case arrow.down:
                if (pos < lstControls.length - 1)
                    pos += 1;

                lstControls[pos].checked = true;
                $(lstControls[pos]).focus();
                CheckFuncionality(lstControls[pos]);
                break;
        }

        //when user press enter submit the selected checkbox
        if (e.which == 13) {
            $('#ctl00_ContentPlaceHolder_UC_WFExecution_ShowDoShowTable__btnok').trigger('click');

        }

        //When user press ESC emulate cancel button
        if (e.which == 27) {
            $('#<%=_btnCancel.ClientID %>').trigger('click');
        }
    });

    function FormatTable() {
        var div = $("#<%=GridContainer.ClientID %> div:first");
        var table = $("#<%=GridContainer.ClientID %> table:first");
        table.appendTo("#<%=GridContainer.ClientID %>");
        div.remove();
    }


    function RealoadDoShowTable() {
        location.reload();
    }

</script>

<style type="text/css">
    .notFixed {
        table-layout: auto !important;
        font-size: 11px;
    }

    .mGrid > tbody > tr:hover {
        background-color: #dae7f5;
        color: white;
        cursor: pointer;
    }

    #GridContainer {
        height: 460px;
        width: 100%;
        overflow: auto;
    }
/*
    .ui-dialog.ui-corner-all.ui-widget.ui-widget-content.ui-front.ui-draggable.ui-resizable {
        width:38% !important;
        min-width:730px;
    }*/

    .ui-dialog.ui-corner-all.ui-widget.ui-widget-content.ui-front.ui-draggable.ui-resizable {
        min-width:730px;
    }
    #search_dgValue {
        border-color: rgb(162, 162, 162) !important;
    }

    .mGrid th {
        height: 35px;
        text-align:center;
    }

    #modalFormHomeUcRules{
        min-width: 1000px !important;
        min-height: 500px !important;
        max-width: 1340px !important;
        max-height: 562px !important;
    }


</style>

<div class="container-fluid">
    <div class="row">
        <asp:HiddenField ID="hdnMultipleCheck" runat="server" />
        <asp:HiddenField ID="hdnJustShow" runat="server" Value="false" />
        <div class="col-xs-1 " style="text-align: left; font-size: 15px; margin-left: 1px; margin-top: 6px;">
            Buscar
        </div>
        <div class="col-xs-10" style="text-align: left; border-color: rgb(162, 162, 162) !important; margin-left: -22px;">
            <input type="text" id="search_dgValue" class="form-control" style="width: 32%" />
        </div>
    </div>
    <div class="row" style="padding-top: 10px;">
        <div class="col-xs-12">
            <div id="GridContainer" runat="server" style="height: 380px; width: 100%; overflow: scroll">
                <asp:GridView ID="dgValue" runat="server"
                    AllowSorting="true" GridLines="Both"
                    EmptyDataText="No hay registros para visualizar"
                    CssClass="mGrid notFixed doShowTableGridView"
                    PagerStyle-CssClass="pgr"
                    AlternatingRowStyle-CssClass="alt"
                    Style="margin-top: 0px;"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center"  />

                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelected" OnClick="CheckFuncionality(this)" Enabled="true" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div style="text-align: center; margin-top: 20px;" class="col-xs-12">
            <asp:Button ID="_btnok" Text="Aceptar" BackColor="#42bd3e" ForeColor="White" runat="server" UseSubmitBehavior="false" OnClick="_btnOk_Click" OnClientClick="" Width="102px" Height="35px" disabled="disabled" CssClass="btn btn-primary btn-xs" />
            <asp:Button ID="_btnCancel" Text="Cancel" runat="server" Width="102px" UseSubmitBehavior="false" CssClass="btn btn-primary btn-xs"
                OnClick="_btnCancel_Click" Style="display: none" />
            <button type="button" class="btn btn-primary btn-xs" style="width: 102px;height: 35px;" onclick="RealoadDoShowTable()">Cancelar </button>

        </div>
    </div>
</div>
