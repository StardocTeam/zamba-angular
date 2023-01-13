<%@ Control Language="C#" AutoEventWireup="true" Inherits="ZGridView" CodeBehind="ZGridView.ascx.cs" %>

<div id="gridContainer">

    <div runat="server" id="pagerZmb">
        <% if (FromTab.Text != string.Empty)
            { %>
        <div style="display: inline;">
            <asp:Label ID="FromTab" runat="server" Font-Bold="true"></asp:Label>
            -
        <asp:Label ID="ToTab" runat="server" Font-Bold="true"></asp:Label>
            de
        <asp:Label ID="TotalTab" runat="server" Font-Bold="true"></asp:Label>

            <asp:HiddenField ID="hiddenStepId" runat="server" />
            <asp:HiddenField ID="hiddenCurrentPage" runat="server" />
        </div>

        <% } %>
        <ul id="divPager">
            <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                    <li>
                        <asp:LinkButton ID="lnkPage" runat="server" Text='' CommandArgument='<%#Eval("Value") %>'
                            Enabled='<%#Eval("Enabled") %>' OnClick="Page_Changed" CssClass='<%#Eval("Text") %>'
                            Font-Bold="true" />
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>

        <%--envia zip--%>
        <span class="btn btn-primary btn-xs" onclick="openZipModal()">Enviar Zip</span>

    </div>
    <asp:GridView ID="grvGrid" runat="server" CssClass="GridViewStyle"
        GridLines="None" AutoGenerateColumns="false" OnRowDataBound="DataItemGrid_RowDataBound">
        <RowStyle CssClass="RowStyleTasks" Wrap="false" />
        <EmptyDataRowStyle CssClass="EmptyRowStyle" Wrap="false" />
        <HeaderStyle CssClass="HeaderStyle" Wrap="false" />
    </asp:GridView>
</div>


<div id="DialogSendZip" style="display: none" title="Enviar Zip">
    <form id="FormSendZip" method="POST">
        <label>Para : *</label>
        <input type="email" class="form-control" id="MailTo" name="MailTo" />

        <label>CC :</label>
        <input type="email" class="form-control" id="CC" name="CC" />

        <label>Asunto : *</label>
        <input class="form-control" id="Asunto" name="Asunto" />

        <label>Archivos Adjuntos : *</label>
        <div id="div1" contenteditable="true" ondrop="drop(event)" ondragover="allowDrop(event)" style="overflow-y: auto">
        </div>

        <label>Nombre del Zip: *</label>
        <input class="form-control" id="ZipName" name="ZipName" />

        <label>Password : *</label>
        <input type="password" class="form-control" id="ZipPassowrd" name="ZipPassowrd" />
    </form>
</div>

<style>
    #div1 {
        height: 90px;
        padding: 10px;
        border: 1px solid #aaaaaa;
        border-radius: 4px;
    }

        #div1 a {
            display: inherit;
        }

    .ui-dialog-titlebar {
        color: white;
        background-color: #337ab7 !important;
    }
</style>



<script type="text/javascript">

    function allowDrop(e) {
        e.preventDefault();
    }

    function drag(e) {
        e.dataTransfer.setData("Text", e.target.id);
    }

    function drop(e) {

        if (navigator.userAgent.match(/Trident\/7\./)) {
            var data = e.dataTransfer.getData("text");
            var link = e.dataTransfer.getData("text");
            $("#div1").append('<a href="' + link + '">' + data + '</a>');
            e.preventDefault();
        }
        else {
            var data = e.dataTransfer.getData("zambaurldesc");
            var link = e.dataTransfer.getData("zambaurl");
            $("#div1").append('<a href="' + link + '">' + data + '</a>');
            e.preventDefault();
        }
    }

    function checkIfArrayIsUnique(arr) {
        var map = {}, i, size;

        for (i = 0, size = arr.length; i < size; i++) {
            if (map[arr[i]]) {
                return false;
            }
            map[arr[i]] = true;
        }
        return true;
    }

    function openZipModal() {
        $("#div1").empty();
        $("#DialogSendZip").children("input").val("");
        $("#DialogSendZip").dialog({


            bgiframe: true,
            modal: true,
            autoOpen: false,
            height: 450,
            width: 430,
            top: -580,
            buttons:
            {
                'Enviar': function () {
                    var DocList = []
                    var AllLink = $("#div1").children("a");
                    var uniqueIds = [];
                    for (var i in AllLink) {
                        var url = AllLink[i].href;
                        if (url != null) {
                            var docid = /docid=(\d+)/.exec(url)[1];
                            DocList.push(docid);
                        }
                    }
                    //Elimino ids repetidos en el array
                    $.each(DocList, function (i, el) {
                        if ($.inArray(el, uniqueIds) === -1) uniqueIds.push(el);
                    });
                    if (DocList.length > 0) {
                        var zipdata = {};
                        zipdata.DocidList = uniqueIds;
                        zipdata.MailTo = $("#MailTo").val();
                        zipdata.CC = $("#CC").val();
                        zipdata.Asunto = $("#Asunto").val();
                        zipdata.ZipName = $("#ZipName").val();
                        zipdata.ZipPassword = $("#ZipPassowrd").val();
                        if (zipdata.MailTo != "" && zipdata.Asunto != "" && zipdata.ZipName != "" && zipdata.ZipPassword != "") {
                            $.ajax({
                                type: "POST",
                                url: "../../Services/TaskService.asmx/GetTaskDocument",
                                data: "{zipdata:" + JSON.stringify(zipdata) + "}",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    if (data.d != false) {
                                        toastr.success("El email se ha enviado con exito");
                                    }
                                    else {
                                        toastr.warning("La tarea no contiene documentos");
                                    }
                                },
                            });
                        }
                        else {
                            toastr.warning("Complete todos los campos marcados con *");
                        }
                    }
                    else {
                        toastr.warning("Deslice una tarea sobre el panel");
                    }
                },

                'Cancelar': function () {
                    $(this).dialog('close');
                }
            }
        });
        $(".ui-dialog").css({
            "background-color": "white",
            "border": "1px solid #aaaaaa"
        });


        $(".ui-dialog-titlebar-close").addClass("btn");
        $("#DialogSendZip").dialog('open'); $("div[role=dialog] button:contains('Enviar')").addClass("btn btn-primary");
        $("#DialogSendZip").dialog('open'); $("div[role=dialog] button:contains('Cancelar')").addClass("btn btn-primary");
    }
</script>
