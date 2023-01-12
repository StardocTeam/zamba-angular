<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />
        <c:url value="/autocomplete/template/read" var="readUrl" />

        <%
        String headerTemplate = "<div class=\"dropdown-header k-widget k-header\">" +
                "<span>Photo</span>" +
                "<span>Contact info</span>" +
            "</div>";

        String template = "<span class=\"k-state-default\" style=\"background-image: url('../resources/web/Customers/#:data.customerId#.jpg')\" ></span>" +
                          "<span class=\"k-state-default\"><h3>#: data.contactName #</h3><p>#: data.companyName #</p></span>";
        %>

        <div class="demo-section k-content">
    		<h4>Customers</h4>
            <kendo:autoComplete name="customers" dataTextField="contactName" 
                headerTemplate="<%=headerTemplate%>" template="<%=template%>" height="370"
                minLength="1" style="width:100%;">
                <kendo:dataSource serverFiltering="true">
                    <kendo:dataSource-transport>
                       <kendo:dataSource-transport-read url="${readUrl}" type="POST" contentType="application/json"/>
                       <kendo:dataSource-transport-parameterMap>
                            <script>
                                function parameterMap(options) {
                                    return JSON.stringify(options);
                                }
                            </script>
                        </kendo:dataSource-transport-parameterMap>
                    </kendo:dataSource-transport>
                    <kendo:dataSource-schema data="data" total="total">
                    </kendo:dataSource-schema>
                </kendo:dataSource>
            </kendo:autoComplete>
            <p class="demo-hint">
		        Start typing to find a customer. E.g. "Ann"
		    </p>
        </div>

        <style>
            .dropdown-header {
		        border-width: 0 0 1px 0;
		        text-transform: uppercase;
		    }
		
		    .dropdown-header > span {
		        display: inline-block;
		        padding: 10px;
		    }
		
		    .dropdown-header > span:first-child {
		        width: 50px;
		    }
		
		    #customers-list .k-item {
		        line-height: 1em;
		        min-width: 300px;
		    }
		    
		    /* Material Theme padding adjustment*/
		    
		    .k-material #customers-list .k-item,
		    .k-material #customers-list .k-item.k-state-hover,
		    .k-materialblack #customers-list .k-item,
		    .k-materialblack #customers-list .k-item.k-state-hover {
		        padding-left: 5px;
		        border-left: 0;
		    }
		
		    #customers-list .k-item > span {
		        -webkit-box-sizing: border-box;
		        -moz-box-sizing: border-box;
		        box-sizing: border-box;
		        display: inline-block;
		        vertical-align: top;
		        margin: 20px 10px 10px 5px;
		    }
		
		    #customers-list .k-item > span:first-child {
		        -moz-box-shadow: inset 0 0 30px rgba(0,0,0,.3);
		        -webkit-box-shadow: inset 0 0 30px rgba(0,0,0,.3);
		        box-shadow: inset 0 0 30px rgba(0,0,0,.3);
		        margin: 10px;
		        width: 50px;
		        height: 50px;
		        border-radius: 50%;
		        background-size: 100%;
		        background-repeat: no-repeat;
		    }
		
		    #customers-list h3 {
		        font-size: 1.2em;
		        font-weight: normal;
		        margin: 0 0 1px 0;
		        padding: 0;
		    }
		
		    #customers-list p {
		        margin: 0;
		        padding: 0;
		        font-size: .8em;
		    }
        </style>

<demo:footer />
