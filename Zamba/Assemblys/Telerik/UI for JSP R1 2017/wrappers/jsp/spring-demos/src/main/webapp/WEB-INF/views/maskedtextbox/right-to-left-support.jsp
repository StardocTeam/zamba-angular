<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />

<div class="demo-section k-content k-rtl">
    <h4>Set Value</h4>
    <kendo:maskedTextBox name="maskedtextbox" mask="(999) 000-0000" style="width: 100%;"></kendo:maskedTextBox>
</div>

<demo:footer />
