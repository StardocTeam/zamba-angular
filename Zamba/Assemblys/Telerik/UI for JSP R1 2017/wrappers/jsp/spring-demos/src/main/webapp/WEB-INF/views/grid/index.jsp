<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<c:url value="/resources/web/grid/clientsDb.png" var="backgroundUrl" />
<c:url value="/grid/customers/" var="transportReadUrl" />

<demo:header />

<kendo:grid name="grid" groupable="true" sortable="true" style="height:550px;">
	<kendo:grid-pageable refresh="true" pageSizes="true" buttonCount="5">
	</kendo:grid-pageable>
    <kendo:grid-columns>
        <kendo:grid-column title="Contact Name" field="contactName" width="240" 
        	template="<div class='customer-photo' style='background-image: url(../resources/web/Customers/#:data.customerId#.jpg);'></div><div class='customer-name'>#: contactName #</div>">            	
        </kendo:grid-column>
        <kendo:grid-column title="Contact Title" field="contactTitle" />
        <kendo:grid-column title="Company Name" field="companyName" />
        <kendo:grid-column title="Country" field="country" width="150" />
    </kendo:grid-columns>
    <kendo:dataSource pageSize="10">
         <kendo:dataSource-schema>
            <kendo:dataSource-schema-model>
                <kendo:dataSource-schema-model-fields>
                    <kendo:dataSource-schema-model-field name="contactName" type="string" />
                    <kendo:dataSource-schema-model-field name="contactTitle" type="string" />
                    <kendo:dataSource-schema-model-field name="companyName" type="string" />
                    <kendo:dataSource-schema-model-field name="country" type="string" />
                </kendo:dataSource-schema-model-fields>
            </kendo:dataSource-schema-model>
        </kendo:dataSource-schema>
        <kendo:dataSource-transport>
            <kendo:dataSource-transport-read url="${transportReadUrl}"/>
        </kendo:dataSource-transport>
    </kendo:dataSource>
</kendo:grid>


 <style type="text/css">
    .customer-photo {
        display: inline-block;
        width: 32px;
        height: 32px;
        border-radius: 50%;
        background-size: 32px 35px;
        background-position: center center;
        vertical-align: middle;
        line-height: 32px;
        box-shadow: inset 0 0 1px #999, inset 0 0 10px rgba(0,0,0,.2);
        margin-left: 5px;
    }

    .customer-name {
        display: inline-block;
        vertical-align: middle;
        line-height: 32px;
        padding-left: 3px;
    }
</style>  
<demo:footer />