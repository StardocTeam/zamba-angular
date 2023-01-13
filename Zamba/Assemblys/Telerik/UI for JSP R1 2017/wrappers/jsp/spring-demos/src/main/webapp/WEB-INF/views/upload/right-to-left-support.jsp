<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<c:url value="/upload/async/save" var="saveUrl" />
<c:url value="/upload/async/remove" var="removeUrl" />

<div class="demo-section k-content k-rtl">
    <kendo:upload name="files">
        <kendo:upload-async autoUpload="true" saveUrl="${saveUrl}" removeUrl="${removeUrl}"/>
    </kendo:upload>
</div>

<demo:footer />
