<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/window/content1" var="remoteUrl" />

<demo:header />

<div class="box hidden-on-narrow">
    <h4>Console log</h4>
    <div class="console"></div>
</div>
<span id="show" style="display:none" class="k-button hidden-on-narrow">Click here to open the window.</span>
<kendo:dialog name="dialog" title="Software Update" closable="false" modal="false"
              close="onClose"
              initOpen="onInitOpen"
              open="onOpen"
              show="onShow"
              hide="onHide"
              content="<p>A new version of <strong>Kendo UI</strong> is available. Would you like to download and install it now?<p>">
    <kendo:dialog-actions>
        <kendo:dialog-action text="Skip this version" />
        <kendo:dialog-action text="Remind me later" />
        <kendo:dialog-action text="Install update" primary="true" />
    </kendo:dialog-actions>
</kendo:dialog>

<div class="responsive-message"></div>

<script>
    var dialog = $("#dialog");
    var show = $("#show");

    function onInitOpen(e) {
        kendoConsole.log("event :: initOpen");
    }

    function onOpen(e) {
        kendoConsole.log("event :: open");
    }

    function onShow(e) {
        kendoConsole.log("event :: show");
    }

    function onHide(e) {
        kendoConsole.log("event :: hide");
    }

    function onClose(e) {
        show.fadeIn();
        kendoConsole.log("event :: close");
    }

    show.click(function () {
        dialog.data("kendoDialog").open();
        show.fadeOut();
    });
</script>

<style>
    #example {
        min-height: 370px;
    }

    #example .box {
        margin: 0;
    }

    #show {
        text-align: center;
        position: absolute;
        white-space: nowrap;
        padding: 1em;
        cursor: pointer;
        margin-top: 30px;
    }
</style>

<demo:footer />
