<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

 <div class="demo-section k-content">
        <h4>Treeview One</h4>
        <kendo:treeView name="treeview-left" dragAndDrop="true">
            <kendo:treeView-items>
                <kendo:treeView-item text="Furniture" expanded="true">
                    <kendo:treeView-items>
                        <kendo:treeView-item text="Tables & Chairs" />
                        <kendo:treeView-item text="Sofas" />
                        <kendo:treeView-item text="Occasional Furniture" />
                    </kendo:treeView-items>
                </kendo:treeView-item>
                <kendo:treeView-item text="Decor">
                    <kendo:treeView-items>
                        <kendo:treeView-item text="Bed Linen" />
                        <kendo:treeView-item text="Curtains & Blinds" />
                        <kendo:treeView-item text="Carpets" />
                    </kendo:treeView-items>
                </kendo:treeView-item>
            </kendo:treeView-items>
        </kendo:treeView>
	
	<h4 style="padding-top: 2em;">Treeview Two</h4>
	
        <kendo:treeView name="treeview-right" dragAndDrop="true">
            <kendo:treeView-items>
                <kendo:treeView-item text="Storage" expanded="true">
                    <kendo:treeView-items>
                        <kendo:treeView-item text="Wall Shelving" />
                        <kendo:treeView-item text="Floor Shelving" />
                        <kendo:treeView-item text="Kids Storage" />
                    </kendo:treeView-items>
                </kendo:treeView-item>
                <kendo:treeView-item text="Lights">
                    <kendo:treeView-items>
                        <kendo:treeView-item text="Ceiling" />
                        <kendo:treeView-item text="Table" />
                        <kendo:treeView-item text="Floor" />
                    </kendo:treeView-items>
                </kendo:treeView-item>
            </kendo:treeView-items>
        </kendo:treeView>
    </div>

    <style>
       #treeview-left,
       #treeview-right
       {
           overflow: visible;
       }
   	</style>
<demo:footer />
