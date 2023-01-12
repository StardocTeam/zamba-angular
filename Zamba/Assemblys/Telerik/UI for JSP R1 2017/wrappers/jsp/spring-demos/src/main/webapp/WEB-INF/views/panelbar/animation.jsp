<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<c:url value="/resources/web/window/armchair-402.png" var="armchair" />

<demo:header />

<c:url value="/resources/web/panelbar/history.png" var="history" />
<c:url value="/resources/web/panelbar/orgFoot.png" var="orgFoot" />

<form class="box" method="Post">
    <h4>Animation Settings</h4>
    <ul class="options">
        <li>
            <input name="animation" type="radio" ${ animation == "toggle" ? "checked=\"checked\"" : "" } value="toggle" /> <label for="toggle">toggle animation</label>
        </li>
        <li>
            <input name="animation" type="radio" ${ animation != "toggle" ? "checked=\"checked\"" : "" } value="expand" /> <label for="expand">expand animation</label>
        </li> 
        <li>
            <button class="k-button">Apply</button>
        </li>
    </ul>
</form>

<div class="demo-section k-content">
    <h4>Conversation history</h4>

	<kendo:panelBar name="panelbar" animation="${ animation != \"toggle\" }">
		<kendo:panelBar-animation>		
			<kendo:panelBar-animation-expand effects="${ (animation != \"toggle\" ? \"expand:vertical\" : \" simple\") }" duration="200"/>
		</kendo:panelBar-animation>	
		<kendo:panelBar-items>			
			<kendo:panelBar-item  text="Today" expanded="true">
				<kendo:panelBar-items>					
					<kendo:panelBar-item text="Jane King"/>
					<kendo:panelBar-item text="Bob Fuller"/>
					<kendo:panelBar-item text="Lynda Kallahan"/>
					<kendo:panelBar-item text="Matt Sutnar"/>					
				</kendo:panelBar-items>
			</kendo:panelBar-item>
			<kendo:panelBar-item  text="Yesterday">
				<kendo:panelBar-items>
					<kendo:panelBar-item text="Stewart"/>
					<kendo:panelBar-item text="Jane King"/>
					<kendo:panelBar-item text="Steven"/>
					<kendo:panelBar-item text="Ken Stone"/>					
				</kendo:panelBar-items>
			</kendo:panelBar-item>	
			<kendo:panelBar-item  text="Wednesday, February 01, 2012">
				<kendo:panelBar-items>
					<kendo:panelBar-item text="Jane King"/>
					<kendo:panelBar-item text="Lynda Kallahan"/>
					<kendo:panelBar-item text="Todd"/>
					<kendo:panelBar-item text="Bob Fuller"/>					
				</kendo:panelBar-items>
			</kendo:panelBar-item>		
			<kendo:panelBar-item  text="Tuesday, January 31, 2012">
				<kendo:panelBar-items>
					<kendo:panelBar-item text="Emily Davolio"/>
					<kendo:panelBar-item text="Matt Sutnar"/>
					<kendo:panelBar-item text="Bob Fuller"/>
					<kendo:panelBar-item text="Jenn Heinlein"/>					
				</kendo:panelBar-items>
			</kendo:panelBar-item>	
			<kendo:panelBar-item  text="Monday, January 30, 2012">
				<kendo:panelBar-items>
					<kendo:panelBar-item text="Matt Sutnar"/>
					<kendo:panelBar-item text="Joshua"/>
					<kendo:panelBar-item text="Michael"/>
					<kendo:panelBar-item text="Jenn Heinlein"/>					
				</kendo:panelBar-items>
			</kendo:panelBar-item>		
		</kendo:panelBar-items>
	</kendo:panelBar>
</div>

<demo:footer />
