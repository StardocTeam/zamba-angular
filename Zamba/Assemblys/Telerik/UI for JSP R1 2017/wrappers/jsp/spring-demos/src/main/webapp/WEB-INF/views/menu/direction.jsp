<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@page import="java.util.ArrayList"%>
<%@page import="com.kendoui.spring.models.DropDownListItem"%>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>

<demo:header />

<div class="demo-section k-content">
	<kendo:menu name="menu">
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

 <div class="box">
    <h4>Animation Settings</h4>
    <ul class="options">
        <li>
            <input id="default" name="direction" type="radio" checked="checked" /> <label for="default">default / bottom</label>
        </li>
        <li>
            <input id="left" name="direction" type="radio" /> <label for="left">left</label>
        </li>
        <li>
            <input id="right" name="direction" type="radio" /> <label for="right">right</label>
        </li>
        <li>
            <input id="top" name="direction" type="radio" /> <label for="top">top</label>
        </li>
        <li>
            <input id="custom" name="direction" type="radio" /> <label for="custom">custom:</label>
            <input id="customValue" type="text" value="top left" class="k-textbox customValue" />
        </li>
    </ul>
    <br />
    <a class="k-button" id="apply" href="#" style="width: 80px;">Apply</a>
</div>
<script>
    $(document).ready(function() {
        var original = $("#menu").clone(true);

        original.find(".k-state-active").removeClass("k-state-active");

        $("#apply").click( function(e) {
            e.preventDefault();
            var menu = $("#menu"),
                clone = original.clone(true);

            menu.data("kendoMenu").close($("#menu .k-link"));

            menu.replaceWith(clone);

            initMenu();
        });
        var getDirection = function () {
            var checked = $("input[type=radio]:checked")[0].id;
            return /custom|customValue/.test(checked) ? $("#customValue").val() : checked;
        };

        var initMenu = function () {
            $("#menu").kendoMenu({
                direction: getDirection()
            });
        };
    });
</script>
<demo:footer />
