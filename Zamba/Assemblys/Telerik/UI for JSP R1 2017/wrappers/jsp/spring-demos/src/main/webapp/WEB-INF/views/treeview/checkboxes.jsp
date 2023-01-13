<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-content">
    <div>
        <h4>Check nodes</h4>
        <kendo:treeView name="treeview" check="onCheck">
    	<kendo:treeView-checkboxes checkChildren="true" />
        <kendo:treeView-items>
            <kendo:treeView-item id="1" text="My Documents" spriteCssClass="rootfolder" expanded="true">
                <kendo:treeView-items>
                    <kendo:treeView-item id="2" text="Kendo UI Project" spriteCssClass="folder" expanded="true">
                        <kendo:treeView-items>
                            <kendo:treeView-item id="3" text="about.html" spriteCssClass="html" />
                            <kendo:treeView-item id="4" text="index.html" spriteCssClass="html" />
                            <kendo:treeView-item id="5" text="logo.png" spriteCssClass="image" />
                        </kendo:treeView-items>
                    </kendo:treeView-item>
                    <kendo:treeView-item id="6" text="New Web Site" spriteCssClass="folder" expanded="true">
                        <kendo:treeView-items>
                            <kendo:treeView-item id="7" text="mockup.jpg" spriteCssClass="image" />
                            <kendo:treeView-item id="8" text="Research.pdf" spriteCssClass="pdf" />
                        </kendo:treeView-items>
                    </kendo:treeView-item>
                   <kendo:treeView-item id="9" text="Reports" spriteCssClass="folder" expanded="true">
                        <kendo:treeView-items>
                            <kendo:treeView-item id="10" text="February.pdf" spriteCssClass="pdf" />
                            <kendo:treeView-item id="11" text="March.pdf" spriteCssClass="pdf" />
                            <kendo:treeView-item id="12" text="April.pdf" spriteCssClass="pdf" />
                        </kendo:treeView-items>
                    </kendo:treeView-item>
                </kendo:treeView-items>
            </kendo:treeView-item>
        </kendo:treeView-items>
    </kendo:treeView>

   </div>
    <div style="padding-top: 2em;">
        <h4>Status</h4>
        <p id="result">No nodes checked.</p>
    </div>
</div>
<script>

    // function that gathers IDs of checked nodes
    function checkedNodeIds(nodes, checkedNodes) {
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].checked) {
                checkedNodes.push(nodes[i].id);
            }

            if (nodes[i].hasChildren) {
                checkedNodeIds(nodes[i].children.view(), checkedNodes);
            }
        }
    }

    // show checked node IDs on datasource change
    function onCheck() {
    	
        var checkedNodes = [],
            treeView = $("#treeview").data("kendoTreeView"),
            message;

        checkedNodeIds(treeView.dataSource.view(), checkedNodes);

        if (checkedNodes.length > 0) {
            message = "IDs of checked nodes: " + checkedNodes.join(",");
        } else {
            message = "No nodes checked.";
        }

        $("#result").html(message);
    }
</script>
<style>
    #treeview .k-sprite {
        background-image: url("../resources/web/treeview/coloricons-sprite.png");
    }

    .rootfolder { background-position: 0 0; }
    .folder     { background-position: 0 -16px; }
    .pdf        { background-position: 0 -32px; }
    .html       { background-position: 0 -48px; }
    .image      { background-position: 0 -64px; }
</style>
<demo:footer />
