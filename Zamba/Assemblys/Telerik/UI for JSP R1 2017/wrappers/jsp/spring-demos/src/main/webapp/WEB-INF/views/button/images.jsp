<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<c:url value="/resources/shared/icons/sports/snowboarding.png" var="snowboarding"/>

<style>
    .demo-section {
        line-height: 4em;
    }
    .demo-section .k-button {
        margin-right: 5px;
    }
    .k-button .k-image {
        height: 16px;
    }
    .netherlandsFlag {
        background-image: url(<c:url value="/resources/shared/styles/flags.png" />);
        background-position: 0 -64px;
    }
</style>

<div class="demo-section k-content">
<h4>Kendo UI Button with icons</h4>
<kendo:button name="iconButton" type="button" spriteCssClass="k-icon netherlandsFlag">
    <kendo:button-content>Sprite icon</kendo:button-content>
</kendo:button>

<kendo:button name="kendoIconButton" type="button" icon="funnel">
    <kendo:button-content>Kendo UI sprite icon</kendo:button-content>
</kendo:button>

<kendo:button name="imageButton" type="button" imageUrl="${snowboarding}">
    <kendo:button-content>Image icon</kendo:button-content>
</kendo:button>
</div>
<demo:footer />