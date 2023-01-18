<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />
    <div class="demo-section k-content k-rtl">
        <h4>Select Continents</h4>
        <kendo:multiSelect name="select" dataTextField="text" dataValueField="value">
            <kendo:dataSource data="${continents}">
            </kendo:dataSource>
        </kendo:multiSelect>
    </div>
<demo:footer />
