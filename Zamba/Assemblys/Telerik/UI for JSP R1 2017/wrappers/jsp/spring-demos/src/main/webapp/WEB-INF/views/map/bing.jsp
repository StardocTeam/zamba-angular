<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<demo:header />

<kendo:map name="map" center="<%= new double[] {51.505, -0.09} %>" zoom="3">
    <kendo:map-layers>
        <!-- IMPORTANT: This key is locked to demos.telerik.com/kendo-ui -->
        <!-- Please replace with your own Bing Key -->
        <kendo:map-layer type="bing" imagerySet="aerialWithLabels" key="AqaPuZWytKRUA8Nm5nqvXHWGL8BDCXvK8onCl2PkC581Zp3T_fYAQBiwIphJbRAK" />
    </kendo:map-layers>
</kendo:map>

<demo:footer />
