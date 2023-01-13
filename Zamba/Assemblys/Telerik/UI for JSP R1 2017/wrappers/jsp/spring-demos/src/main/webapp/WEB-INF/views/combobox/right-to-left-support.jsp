<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />
<div class="demo-section k-content">
    <div class="k-rtl">
        <h4>RTL ComboBox</h4>
        <kendo:comboBox name="combobox" dataTextField="text" dataValueField="value" style="width: 100%;">
            <kendo:dataSource data="${items}">
            </kendo:dataSource>
        </kendo:comboBox>
    </div>
</div>
<demo:footer />
