<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/window/content1" var="remoteUrl" />

<demo:header />
<div class="box wide hidden-on-narrow" style="z-index:10000">
    <div class="box-col">
        <h4>API Functions</h4>
        <ul class="options">
            <li>
                <button id="open" class="k-button">Open</button>
                <button id="close" class="k-button">Close</button>
            </li>
        </ul>
    </div>
</div>
<kendo:dialog name="dialog" title="Software Update" modal="false"
              content="<p>A new version of <strong>Kendo UI</strong> is available. Would you like to download and install it now?<p>">
    <kendo:dialog-actions>
        <kendo:dialog-action text="Skip this version" />
        <kendo:dialog-action text="Remind me later" />
        <kendo:dialog-action text="Install update" primary="true" />
    </kendo:dialog-actions>
</kendo:dialog>
<div class="responsive-message"></div>
<script>
    $(function () {
        var dialog = $("#dialog");
        $("#open").click(function (e) {
            dialog.getKendoDialog().open();
        });

        $("#close").click(function (e) {
            dialog.getKendoDialog().close();
        });
    });
</script>
<style>
    #example {
        min-height: 200px;
    }
</style>
<demo:footer />
