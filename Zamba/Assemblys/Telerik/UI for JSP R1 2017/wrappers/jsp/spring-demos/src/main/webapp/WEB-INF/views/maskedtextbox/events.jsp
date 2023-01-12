<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />

<div class="demo-section k-content">
    <h4>Enter phone number</h4>
    <kendo:maskedTextBox name="maskedtextbox" mask="(999) 000-0000" change="change" style="width: 100%;"></kendo:maskedTextBox>
</div>

<div class="box">
    <h4>Console log</h4>
    <div class="console"></div>
</div>

<script>
    function change() {
        kendoConsole.log("Change :: " + this.value());
    }
</script>

<demo:footer />
