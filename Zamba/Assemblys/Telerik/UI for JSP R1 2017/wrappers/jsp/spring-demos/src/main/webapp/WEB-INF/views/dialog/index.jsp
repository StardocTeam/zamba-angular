<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/resources/web/dialog/armchair-402.png" var="armchair" />

<demo:header />

<kendo:dialog name="dialog" title="Software Update" closable="false" modal="false" close="onClose"
              content="<p>A new version of <strong>Kendo UI</strong> is available. Would you like to download and install it now?<p>">
    <kendo:dialog-actions>
        <kendo:dialog-action text="Skip this version" />
        <kendo:dialog-action text="Remind me later" />
        <kendo:dialog-action text="Install update" primary="true" />
    </kendo:dialog-actions>
</kendo:dialog>

<span id="undo" style="display:none" class="k-button hide-on-narrow">Click here to open the dialog</span>
<style>
    #example {
        min-height: 350px;
    }
</style>
<script>
    $(document).ready(function () {
        var dialog = $('#dialog'),
                undo = $("#undo");

        undo.click(function () {
            dialog.data("kendoDialog").open();
            undo.fadeOut();
        });
    });
    
    function onClose() {
        $("#undo").fadeIn();
    }
</script>

<demo:footer />
