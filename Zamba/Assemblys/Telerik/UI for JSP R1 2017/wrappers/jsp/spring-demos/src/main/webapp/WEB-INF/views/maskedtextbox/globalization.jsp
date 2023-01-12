<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>

<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<c:url value="/resources/js/cultures/kendo.culture.en-US.min.js" var="enUS"/>
<c:url value="/resources/js/cultures/kendo.culture.en-GB.min.js" var="enGB"/>
<c:url value="/resources/js/cultures/kendo.culture.de-DE.min.js" var="deDE"/>
<c:url value="/resources/js/cultures/kendo.culture.fr-FR.min.js" var="frFR"/>
<c:url value="/resources/js/cultures/kendo.culture.bg-BG.min.js" var="bgBG"/>

<script type="text/javascript" src="${enUS}"></script>
<script type="text/javascript" src="${enGB}"></script>
<script type="text/javascript" src="${deDE}"></script>
<script type="text/javascript" src="${frFR}"></script>
<script type="text/javascript" src="${bgBG}"></script>

<div id="product-view" class="demo-section k-content">
   <ul id="fieldlist">
     <li>
       <label for="culture">Choose culture:</label>
       <kendo:dropDownList name="culture" dataTextField="text" dataValueField="value" change="changeCulture" value="en-US" style="width: 100%;">
	       <kendo:dataSource data="${cultures}"></kendo:dataSource>
	   </kendo:dropDownList>  
  	</li>
  	<li>
       <label for="initial">Initial price:</label>
       <kendo:maskedTextBox name="initial" mask="9,999.99 $" value="1234.56" style="width: 100%;"></kendo:maskedTextBox>
  	</li>
   </ul>
</div>

<style>
     #fieldlist {
         margin: 0 0 -2em;
         padding: 0;
     }

     #fieldlist li {
         list-style: none;
         padding-bottom: 2em;
     }

     #fieldlist label {
         display: block;
         padding-bottom: 1em;
         font-weight: bold;
         text-transform: uppercase;
         font-size: 12px;
         color: #444;
     }

 </style>

<script>
	function changeCulture() {
	    kendo.culture(this.value());
	
	    $("#initial").data("kendoMaskedTextBox").setOptions(initial.options);
	}
</script>

<demo:footer />
