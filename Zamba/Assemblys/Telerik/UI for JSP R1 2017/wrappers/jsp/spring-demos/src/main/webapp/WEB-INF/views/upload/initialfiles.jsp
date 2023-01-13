<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="box">
    <h4>Information</h4>
    <p>
        This example show how to persist the successfully uploaded files
        in the list and display them again when the page is reloaded. 
        Please upload some files and refresh the page.
    </p>
</div>

<c:url value="/upload/initialfiles/saveAndPersist" var="saveAndPersistUrl" />
<c:url value="/upload/initialfiles/removeAndPersist" var="removeAndPersistUrl" />

<div class="demo-section k-content">
	    <kendo:upload name="files" files="${initialFiles}">
	        <kendo:upload-async autoUpload="true"
	        					saveUrl="${saveAndPersistUrl}" 
	        					removeUrl="${removeAndPersistUrl}"/>
	    </kendo:upload>
</div>

<demo:footer />