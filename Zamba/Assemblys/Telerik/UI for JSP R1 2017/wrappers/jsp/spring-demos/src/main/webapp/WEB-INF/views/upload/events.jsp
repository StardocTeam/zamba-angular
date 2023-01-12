<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="box">
    <h4>Information</h4>
    <p>
        This example shows how to handle events triggered by kendoUpload.
    </p>
</div>

<c:url value="/upload/events/save" var="saveUrl" />
<c:url value="/upload/events/remove" var="removeUrl" />

<div class="demo-section k-content">
    <kendo:upload name="files" select="onSelect" upload="onUpload" 
        success="onSuccess" error="onError" complete="onComplete" progress="onProgress">
        <kendo:upload-async autoUpload="true" saveUrl="${saveUrl}" removeUrl="${removeUrl}"/>
    </kendo:upload>
</div>

<div class="box">
    <h4>Console log</h4>
    <div class="console"></div>
</div>

 <script>
    function onSelect(e) {
        kendoConsole.log("Select :: " + getFileInfo(e));
    }

    function onUpload(e) {
        kendoConsole.log("Upload :: " + getFileInfo(e));
    }

    function onSuccess(e) {
        kendoConsole.log("Success (" + e.operation + ") :: " + getFileInfo(e));
    }

    function onError(e) {
        kendoConsole.log("Error (" + e.operation + ") :: " + getFileInfo(e));
    }

    function onComplete(e) {
        kendoConsole.log("Complete");
    }

    function onCancel(e) {
        kendoConsole.log("Cancel :: " + getFileInfo(e));
    }

    function onRemove(e) {
        kendoConsole.log("Remove :: " + getFileInfo(e));
    }

    function onProgress(e) {
        kendoConsole.log("Upload progress :: " + e.percentComplete + "% :: " + getFileInfo(e));
    }

    function getFileInfo(e) {
        return $.map(e.files, function(file) {
            var info = file.name;

            // File size is not available in all browsers
            if (file.size > 0) {
                info  += " (" + Math.ceil(file.size / 1024) + " KB)";
            }
            return info;
        }).join(", ");
    }
</script>
<demo:footer />
