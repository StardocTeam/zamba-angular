<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />

<div class="demo-section k-content">

<kendo:tabStrip name="tabstrip">
    <kendo:tabStrip-items>
        <kendo:tabStrip-item text="Tab 1 with long text" selected="true">
            <kendo:tabStrip-item-content>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer felis libero, lobortis ac rutrum quis, varius a velit.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Tab 2 with long text">
            <kendo:tabStrip-item-content>
                <p>Donec lacus erat, cursus sed porta quis, adipiscing et ligula. Duis volutpat, sem pharetra accumsan pharetra, mi ligula cursus felis, ac aliquet leo diam eget risus.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Tab 3 with long text">
            <kendo:tabStrip-item-content>
                <p>Integer facilisis, justo cursus venenatis vehicula, massa nisl tempor sem, in ullamcorper neque mauris in orci.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Tab 4 with long text">
            <kendo:tabStrip-item-content>
                <p>Ut orci ligula, varius ac consequat in, rhoncus in dolor. Mauris pulvinar molestie accumsan. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
    </kendo:tabStrip-items>
</kendo:tabStrip>

</div>

<demo:footer />