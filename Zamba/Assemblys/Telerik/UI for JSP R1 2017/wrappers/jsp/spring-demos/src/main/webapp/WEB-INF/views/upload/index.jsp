<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="box">
    <h4>Information</h4>
    <p>
        The Upload can be used as a drop-in replacement
        for file input elements. This "synchronous" mode does not require
        special handling on the server.
    </p>
</div>

<form method="post" action="<c:url value='/upload/' />">
    <div class="demo-section k-content">
    
        <kendo:upload name="files" />
        
        <p style="padding-top: 1em; text-align: right">
            <input type="submit" value="Submit" class="k-button k-primary" />
        </p>
    </div>
</form>

<demo:footer />
