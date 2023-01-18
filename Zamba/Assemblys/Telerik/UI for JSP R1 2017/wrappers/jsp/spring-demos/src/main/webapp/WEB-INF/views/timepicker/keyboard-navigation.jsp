<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<demo:header />
    <div class="demo-section k-content">
        <h4>Select time:</h4>
        <kendo:timePicker name="timepicker" style="width: 100%" accesskey="w"></kendo:timePicker>
    </div>

            
	 <div class="box">
	     <div class="box-col">
	        <h4>Focus</h4>
	         <ul class="keyboard-legend">
	             <li>
	                 <span class="button-preview">
	                     <span class="key-button leftAlign wider"><a target="_blank" href="http://en.wikipedia.org/wiki/Access_key">Access key</a></span>
	                     +
	                     <span class="key-button">w</span>
	                 </span>
	                 <span class="button-descr">
	                     focuses the widget
	                 </span>
	             </li>
	         </ul>
	     </div>
	
	     <div class="box-col">
	         <h4>Closed popup</h4>
	         <ul class="keyboard-legend">
	             <li>
	                 <span class="button-preview">
	                     <span class="key-button wider rightAlign">enter</span>
	                 </span>
	                 <span class="button-descr">
	                     triggers change event
	                 </span>
	             </li>
	             <li>
	                 <span class="button-preview">
	                     <span class="key-button">esc</span>
	                 </span>
	                 <span class="button-descr">
	                     closes the popup
	                 </span>
	             </li>
	             <li>
	                 <span class="button-preview">
	                     <span class="key-button">alt</span>
	                     <span class="key-button wider leftAlign">down arrow</span>
	                 </span>
	                 <span class="button-descr">
	                     opens the popup
	                 </span>
	             </li>
	             <li>
	                 <span class="button-preview">
	                     <span class="key-button">alt</span>
	                     <span class="key-button wider leftAlign">up arrow</span>
	                 </span>
	                 <span class="button-descr">
	                     closes the popup
	                 </span>
	             </li>
	         </ul>
	     </div>
	
	
	     <div class="box-col">
	         <h4>Opened popup (time view)</h4>
	         <ul class="keyboard-legend">
	             <li>
	                 <span class="button-preview">
	                     <span class="key-button wide leftAlign">up arrow</span>
	                 </span>
	                 <span class="button-descr">
	                     selects previous available time
	                 </span>
	             </li>
	             <li>
	                 <span class="button-preview">
	                     <span class="key-button wider leftAlign">down arrow</span>
	                 </span>
	                 <span class="button-descr">
	                     selects next available time
	                 </span>
	             </li>
	         </ul>
	     </div>
	
	 </div>

    
<demo:footer />