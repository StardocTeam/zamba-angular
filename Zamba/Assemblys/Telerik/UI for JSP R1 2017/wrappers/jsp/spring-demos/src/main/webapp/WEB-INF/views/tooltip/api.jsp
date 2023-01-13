<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />   
   <kendo:tooltip name="#tooltip" position="bottom" autoHide="false" showOn="click" filter="div" width="200">
   		<kendo:tooltip-content content="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."/>   		
   </kendo:tooltip>   
   
 <div class="box wide">
           <ul class="options">
               <li>
	           Show at  
	           <kendo:dropDownList name="selector"  
	           		dataTextField="text" dataValueField="id" change="change">
	           		<kendo:dataSource data="${targets}"></kendo:dataSource>
	           </kendo:dropDownList>
	        </li>
	    </ul>
	</div>
	
	<div id="tooltip">
	    <div id="target1" class="k-group">Target 1</div>
	    <div id="target2" class="k-group">Target 2</div>
	</div>
            
     <script>
         function change(e) {
        	$("#tooltip").data("kendoTooltip").show($("#target" + this.value())); 
         }
     </script>
    
 <style>
    #target1, #target2 {
         display: block;
         margin: 20px auto;
         text-align: center;
         width: 200px;
         white-space: nowrap;
         border-width: 1px;
         border-style: solid;
         padding: 2em;
     }
</style>                
    
<demo:footer />