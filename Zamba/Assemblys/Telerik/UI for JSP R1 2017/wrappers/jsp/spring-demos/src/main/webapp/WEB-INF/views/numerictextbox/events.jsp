<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />

<div class="demo-section k-content">
    <h4>Set value</h4>
	<kendo:numericTextBox name="numerictextbox" change="change" spin="spin" style="width: 100%;"></kendo:numericTextBox>
</div>

<div class="box">
    <h4>Console log</h4>
    <div class="console"></div>
</div>

<script>
    function change() {
        kendoConsole.log("Change :: " + this.value());
    }

    function spin() {
        kendoConsole.log("Spin :: " + this.value());
    }
</script>

<demo:footer />
