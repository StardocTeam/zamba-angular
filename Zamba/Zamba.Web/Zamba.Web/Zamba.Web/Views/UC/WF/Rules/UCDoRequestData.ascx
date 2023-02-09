<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UC_WF_Rules_UCDoRequestData" CodeBehind="UCDoRequestData.ascx.cs" %>

<style>
    #ui-datepicker-div {
        z-index: 1051 !important;
        /*z-index:1002 !important;*/
    }

    span {
        white-space: nowrap;
    }
    #openModalIFUcRules{
        margin-top: 80px !important;
        position: absolute !important;
        display: block !important;
        padding-right: 12px !important;
        overflow-y: scroll !important;
        padding-left: 15% !important;
        padding-right: 15% !important;
    }
</style>

<asp:HiddenField runat="server" ID="hddocId" />
<asp:HiddenField runat="server" ID="hdDTId" />
<div class="row">
    <div class="col-xs-12">

        <asp:Panel ID="DoRequestDataIndexes" runat="server">

            <asp:Table ID="tblIndices" runat="server" CellPadding="0" EnableViewState="true"
                Style="height: 100%;" />
        </asp:Panel>

    </div>
</div>
<div class="row">
</div>
<div class="row">
    <div class="col-xs-12" style="margin-top:15px">
        <asp:Button ID="_btnok" Text="Aceptar" CssClass="btn btn-success btn-xs" runat="server" Width="97px" OnClick="_btnOk_Click" 
            UseSubmitBehavior="false" />
        <asp:Button ID="_btnCancel" Text="Cancelar" CssClass="btn btn-info btn-xs" runat="server" Width="102px" UseSubmitBehavior="false" 
            OnClick="_btnCancel_Click" />
    </div>
</div>



<script>

    $(document).ready(function () {
      //  SetListFilters();
    });

    function SetListFilters() {
        //Obtiene todos los tags select
        $('select:not(:disabled)').each(function () {
            //Verifica que lo que encontro sea un índice
            if ($(this).attr('id') != undefined && $(this).attr('id').toLowerCase().indexOf('showdorequestdata_') > -1 && !$(this).hasClass("readonly") && $(this).css('display') != 'none' ) {
                //Agrega la lupa para filtrar y buscar valores
                $(this)//.attr('id').onclick(CreateTable(this, false));
                    .click(function () { CreateTable(this, false) });
                AddFilter($(this).attr('id'), false);
            }
        });
    }

    function CreateTable(obj, codecolumn) {
        //parent.ShowLoadingAnimation();
        var winHeight = $(window).height();
        //Creando el nombre del select
        var arrayObj = obj.id.toString().split('_');
        var cBox = obj.id;
        $('#dynamic_filter').data("indexid", cBox);

        var table = LoadTable3(cBox, codecolumn);
        //Limpiando la tabla
        $('#dynamic_filter').html("");

        var initModal = '<div class="modal fade" id="openModalSearch" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">' +
            '<div class="modal-dialog">' +
            ' <div class="modal-content" id="openModalIFSearch">' +
            ' <div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>' +
            '<h4 class="modal-title" id="modalFormTitleSearch"></h4>' +
            '</div>' +
            '<div class="modal-body" id="">' +
            '<div id="modalDivBodySearch">';
        var endModal = '</div> </div> </div> </div>';

        //Agregando los botones
        var html = initModal + table;

        html += '<center><input id="btnAceptDynamic" type="button" class="btn btn-success" value="Aceptar" onclick="Accept()" />';
        html += '<input id="btnCancelDynamic" type="button" value="Cancelar" class="btn btn-info" style=" margin-left:10px;" onclick="Cancel()" /></center>';
        html += endModal;

        //Agregando la tabla creada dinamicamente
        $('#dynamic_filter').html(html);

        $("#example tbody").click(function (e) { $(oTable.fnSettings().aoData).each(function () { $(this.nTr).removeClass('row_selected'); }); $(e.target.parentNode).addClass('row_selected'); });

        //Agregando el evento click a cada fila
        $('#example tr').click(function () {
            if ($(this).hasClass('row_selected'))
                $(this).removeClass('row_selected');
            else
                $(this).addClass('row_selected');
        });

        /* Add a click handler for the delete row */
        $('#delete').click(function () {
            var anSelected = fnGetSelected(oTable);
            oTable.fnDeleteRow(anSelected[0]);
        });

        /* Init the table */
        var oTable = $('#example').dataTable({ "bPaginate": false, "bLengthChange": false, "bInfo": false, "bAutoWidth": false, "bSort": false });

        //Calculando heights
        var popupHeight = selectHeight >= winHeight ? winHeight - 45 : selectHeight + 45;
        var tableHeight = selectHeight > popupHeight - 70 ? popupHeight - 70 : selectHeight;

        if (tableHeight <= 0) {
            tableHeight = 30;
        }
        selectHeight = selectHeight > 1400 ? selectHeight / 10 : selectHeight;


        if (popupHeight > $(window).width() - 600) {
            $("#dynamic_filter").css('height', selectHeight - 77 + 'px');
            $("#exampleContainer").css('height', selectHeight - 160 + 'px');
            popupHeight = tableHeight - 130;
        }
        else {
            popupHeight = tableHeight - 130;
        }

        //Abriendo el  modal
        $("#example_wrapper").height(tableHeight);
        if (selectHeight > winHeight - 70) {
            tableHeight = winHeight - 100;

            $("#exampleContainer").height(tableHeight);
        }
        $("#dynamic_filter").css('height', '');
        $("#openModalSearch").modal();
        $("#openModalSearch").draggable();

        setTimeout("parent.hideLoading();", 500);
    }
    function Cancel() {
        $("#openModalSearch").modal('hide');
    }

    function Accept() {
        var rows = $('#example tbody tr');
        var value = "";
        for (var i = 0; i < rows.length; i++) {
            if ($(rows[i]).hasClass('row_selected')) {
                value = rows[i];
                value = $(value).find("td")[0].innerHTML;
            }
        }
        if (value == "") {
            toastr.error("Por favor seleccione una fila");
        }
        else {
            $('#' + $('#dynamic_filter').data("indexid") + ' option').each(
                function () {
                    if ($(this).val().toString().trim() == value.toString().trim()) {
                        $(this).attr("selected", "selected");
                        $(this).parent().change();
                        $(this).parent().valid();
                    }
                });
            Cancel();
        }
    }
    /*
    * I don't actually use this here, but it is provided as it might be useful and demonstrates
    * getting the TR nodes from DataTables
    */
    function fnGetSelected(oTableLocal) {
        var aReturn = new Array();
        var aTrs = oTableLocal.fnGetNodes();

        for (var i = 0; i < aTrs.length; i++) {
            if ($(aTrs[i]).hasClass('row_selected')) {
                aReturn.push(aTrs[i]);
            }
        }
        return aReturn;
    }

    var selectHeight;

    function LoadTable3(cbox, codecolumn) {
        selectHeight = 0;
        var t = "";
        var select = $("#" + cbox + " option");
        t = '<div id="exampleContainer" style="overflow:auto;height:340px !important"><table cellpadding="0" cellspacing="0" border="0" class="display" id="example" style="height:60px;">';

        //Cabecera
        t += "<thead>";
        t += "<tr>";
        if (codecolumn)
            t += "<th>Codigo</th>";
        else
            t += '<th style="display:none;">Codigo</th>';
        t += "<th>Descripcion</th>";
        t += "</tr>";
        t += "</thead>";

        //Cuerpo
        t += "<tbody>";
        for (var i = 0; i < select.length; i++) {
            t += '<tr class="gradeA">';
            if (codecolumn) {
                t += "<td>";
            } else {
                selectHeight = selectHeight + 20;
                t += '<td style="display:none;">';
                t += select[i].value;
                t += "</td>";
                t += "<td>";
                t += select[i].text;
                t += "</td>";
                t += "</tr>";
            }
        }
        t += "</tbody>";

        //Final
        t += "<tfoot>";
        t += "<tr>";
        t += "</tr>";
        t += "</tfoot>";
        t += "</table></div>";

        return t;
    }
</script>