<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
<div class="demo-section k-content">
<h4>Buttons</h4>
<kendo:button name="textButton" type="button" click="onClick">
    <kendo:button-content>
        Text button
    </kendo:button-content>
</kendo:button>

<kendo:button name="refreshButton" tag="a" icon="refresh" click="onClick">
    <kendo:button-content>
        Refresh button
    </kendo:button-content>
</kendo:button>

<kendo:button name="disabledButton" tag="span" enable="false" click="onClick">
    <kendo:button-content>
        Disabled button
    </kendo:button-content>
</kendo:button>

<p class="demo-hint">(The disabled button will not fire click events)</p>
</div>
<div class="box">                
    <h4>Console log</h4>
    <div class="console"></div>
</div>

<style>
    .demo-section {
        line-height: 4em;
    }
    .demo-section .k-button {
        margin-right: 10px;
    }
</style>

<script>
    function onClick(e) {
        kendoConsole.log("event :: click (" + $(e.event.target).closest(".k-button").attr("id") + ")");
    }
</script>

<demo:footer />