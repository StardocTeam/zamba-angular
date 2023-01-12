<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/resources/web/window/armchair-402.png" var="armchair" />

<demo:header />
<div id="showContainer">
    <span id="show" style="display: none;" class="k-button hidden-on-narrow">Click here to open the dialog.</span>
</div>
<kendo:dialog name="dialog" title="Software Update" modal="false" close="onClose"
              content="<p>A new version of <strong>Kendo UI</strong> is available. Would you like to download and install it now?<p>">
    <kendo:dialog-actions>
        <kendo:dialog-action text="Skip this version" />
        <kendo:dialog-action text="Remind me later" />
        <kendo:dialog-action text="Install update" primary="true" />
    </kendo:dialog-actions>
</kendo:dialog>

<div class="box wide hidden-on-narrow">
    <div class="box-col">
        <h4>Supported keys and user actions</h4>
        <ul class="keyboard-legend">
            <li>
                <span class="button-preview">
                    <span class="key-button leftAlign wider">alt</span>
                    <span class="key-button">w</span>
                </span>
                <span class="button-descr">
                    focuses dialog
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button">esc</span>
                </span>
                <span class="button-descr">
                    closes dialog
                </span>
            </li>
            <li>
                <span class="button-preview">
                    <span class="key-button leftAlign wider">enter</span> /
                    <span class="key-button">space</span>
                </span>
                <span class="button-descr">
                    triggers the focused action and the close button if focused
                </span>
            </li>
        </ul>
    </div>
</div>

<script>
    $(document).ready(function () {
        var dialog = $("#dialog");
        var show = $("#show");

        
        show.click(function () {
            dialog.data("kendoDialog").open();
            show.fadeOut();
        });

        // focus the widget's element
        $(document).on("keydown.examples", function (e) {
            if (e.altKey && e.keyCode === 87 /* w */) {
                dialog.get(0).focus();
            }
        });
    });
    
    function onClose() {
        $("#show").fadeIn();
    }

</script>
<style>
    #example .box {
        margin-top: 250px;
    }
    #showContainer {
        position: relative;
        height: 3em;
    }
    #show {
        text-align: center;
        white-space: nowrap;
        position: absolute;
        padding: 1em;
        cursor: pointer;
    }
</style>

<demo:footer />
