<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="java.util.ArrayList"%>
<%@page import="com.kendoui.spring.models.DropDownListItem"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-content">

	<kendo:menu name="menu" orientation="${orientation}">
	    <kendo:menu-items>
	        <kendo:menu-item text="Men's">
	            <kendo:menu-items>
	                <kendo:menu-item text="Footwear">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="Leisure Trainers"></kendo:menu-item>
	                        <kendo:menu-item text="Running Shoes"></kendo:menu-item>
	                        <kendo:menu-item text="Outdoor Footwear"></kendo:menu-item>
	                        <kendo:menu-item text="Sandals/Flip Flops"></kendo:menu-item>
	                        <kendo:menu-item text="Footwear Accessories"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>
	                <kendo:menu-item text="Leisure Clothing">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="T-Shirts"></kendo:menu-item>
	                        <kendo:menu-item text="Hoodies & Sweatshirts"></kendo:menu-item>
	                        <kendo:menu-item text="Jackets"></kendo:menu-item>
	                        <kendo:menu-item text="Pants"></kendo:menu-item>
	                        <kendo:menu-item text="Shorts"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>               
	                <kendo:menu-item text="Sports Clothing">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="Football"></kendo:menu-item>
	                        <kendo:menu-item text="Basketball"></kendo:menu-item>
	                        <kendo:menu-item text="Golf"></kendo:menu-item>
	                        <kendo:menu-item text="Tennis"></kendo:menu-item>
	                        <kendo:menu-item text="Swimwear"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>
	                <kendo:menu-item text="Accessories"></kendo:menu-item>
	            </kendo:menu-items>        
	        </kendo:menu-item>
	        <kendo:menu-item text="Women's">
	            <kendo:menu-items>
	                <kendo:menu-item text="Footwear">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="Leisure Trainers"></kendo:menu-item>
	                        <kendo:menu-item text="Running Shoes"></kendo:menu-item>
	                        <kendo:menu-item text="Outdoor Footwear"></kendo:menu-item>
	                        <kendo:menu-item text="Sandals/Flip Flops"></kendo:menu-item>
	                        <kendo:menu-item text="Footwear Accessories"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>
	                <kendo:menu-item text="Leisure Clothing">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="T-Shirts"></kendo:menu-item>
	                        <kendo:menu-item text="Jackets"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>               
	                <kendo:menu-item text="Sports Clothing">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="Basketball"></kendo:menu-item>
	                        <kendo:menu-item text="Golf"></kendo:menu-item>
	                        <kendo:menu-item text="Tennis"></kendo:menu-item>
	                        <kendo:menu-item text="Swimwear"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>
	                <kendo:menu-item text="Accessories"></kendo:menu-item>
	            </kendo:menu-items>  
	        </kendo:menu-item>
	        <kendo:menu-item text="Boy's">
	            <kendo:menu-items>
	                <kendo:menu-item text="Footwear">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="Leisure Trainers"></kendo:menu-item>
	                        <kendo:menu-item text="Running Shoes"></kendo:menu-item>
	                        <kendo:menu-item text="Outdoor Footwear"></kendo:menu-item>
	                        <kendo:menu-item text="Sandals/Flip Flops"></kendo:menu-item>
	                        <kendo:menu-item text="Footwear Accessories"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>
	                <kendo:menu-item text="Leisure Clothing">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="T-Shirts"></kendo:menu-item>
	                        <kendo:menu-item text="Hoodies & Sweatshirts"></kendo:menu-item>
	                        <kendo:menu-item text="Jackets"></kendo:menu-item>
	                        <kendo:menu-item text="Pants"></kendo:menu-item>
	                        <kendo:menu-item text="Shorts"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>               
	                <kendo:menu-item text="Sports Clothing">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="Football"></kendo:menu-item>
	                        <kendo:menu-item text="Basketball"></kendo:menu-item>
	                        <kendo:menu-item text="Golf"></kendo:menu-item>
	                        <kendo:menu-item text="Tennis"></kendo:menu-item>
	                        <kendo:menu-item text="Swimwear"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>
	                <kendo:menu-item text="Accessories"></kendo:menu-item>
	            </kendo:menu-items>  
	        </kendo:menu-item>
	        <kendo:menu-item text="Girl's">
	            <kendo:menu-items>
	                <kendo:menu-item text="Footwear">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="Leisure Trainers"></kendo:menu-item>
	                        <kendo:menu-item text="Running Shoes"></kendo:menu-item>
	                        <kendo:menu-item text="Outdoor Footwear"></kendo:menu-item>
	                        <kendo:menu-item text="Sandals/Flip Flops"></kendo:menu-item>
	                        <kendo:menu-item text="Footwear Accessories"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>
	                <kendo:menu-item text="Leisure Clothing">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="T-Shirts"></kendo:menu-item>
	                        <kendo:menu-item text="Hoodies & Sweatshirts"></kendo:menu-item>
	                        <kendo:menu-item text="Jackets"></kendo:menu-item>
	                        <kendo:menu-item text="Pants"></kendo:menu-item>
	                        <kendo:menu-item text="Shorts"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>               
	                <kendo:menu-item text="Sports Clothing">
	                    <kendo:menu-items>
	                        <kendo:menu-item text="Basketball"></kendo:menu-item>
	                        <kendo:menu-item text="Tennis"></kendo:menu-item>
	                        <kendo:menu-item text="Swimwear"></kendo:menu-item>
	                    </kendo:menu-items>
	                </kendo:menu-item>
	                <kendo:menu-item text="Accessories"></kendo:menu-item>
	            </kendo:menu-items>  
	        </kendo:menu-item>
	    </kendo:menu-items>
	</kendo:menu>
</div>
<form method="post">
     <div class="box">
                <h4>Orientation Settings</h4>
                <ul class="options">
                    <li>      
                        <label for="orientation">Orientation:</label> 
                <kendo:dropDownList name="orientation" value="${orientation}" dataTextField="text" dataValueField="value">
                    <kendo:dataSource data="${orientations}"></kendo:dataSource>
                </kendo:dropDownList>
            </li>
        </ul>
        <button class="k-button" style="width: 80px;">Apply</button>
    </div>
</form>

<demo:footer />
