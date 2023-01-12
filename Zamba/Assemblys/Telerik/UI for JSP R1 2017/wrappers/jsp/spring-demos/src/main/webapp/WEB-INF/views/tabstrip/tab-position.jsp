<%@page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@taglib prefix="kendo" uri="http://www.kendoui.com/jsp/tags"%>
<%@taglib prefix="demo" tagdir="/WEB-INF/tags"%>

<demo:header />

 <div class="demo-section k-content">
	<h4>Left</h4>

<kendo:tabStrip name="tabstrip-left" tabPosition="left">
    <kendo:tabStrip-animation>
        <kendo:tabStrip-animation-open effects="fadeIn" />
    </kendo:tabStrip-animation>
    <kendo:tabStrip-items>
        <kendo:tabStrip-item text="One" selected="true">
            <kendo:tabStrip-item-content>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer felis libero, lobortis ac rutrum quis, varius a velit. Donec lacus erat, cursus sed porta quis, adipiscing et ligula. Duis volutpat, sem pharetra accumsan pharetra, mi ligula cursus felis, ac aliquet leo diam eget risus. Integer facilisis, justo cursus venenatis vehicula, massa nisl tempor sem, in ullamcorper neque mauris in orci.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Two">
            <kendo:tabStrip-item-content>
                <p>Ut orci ligula, varius ac consequat in, rhoncus in dolor. Mauris pulvinar molestie accumsan. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Aenean velit ligula, pharetra quis aliquam sed, scelerisque sed sapien. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam dui mi, vulputate vitae pulvinar ac, condimentum sed eros.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Three">
            <kendo:tabStrip-item-content>
                <p>Aliquam at nisl quis est adipiscing bibendum. Nam malesuada eros facilisis arcu vulputate at aliquam nunc tempor. In commodo scelerisque enim, eget sodales lorem condimentum rutrum. Phasellus sem metus, ultricies at commodo in, tristique non est. Morbi vel mauris eget mauris commodo elementum. Nam eget libero lacus, ut sollicitudin ante. Nam odio quam, suscipit a fringilla eget, dignissim nec arcu. Donec tristique arcu ut sapien elementum pellentesque.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
    </kendo:tabStrip-items>
</kendo:tabStrip>

<h4>Right</h4>

<kendo:tabStrip name="tabstrip-right" tabPosition="right">
    <kendo:tabStrip-animation>
        <kendo:tabStrip-animation-open effects="fadeIn" />
    </kendo:tabStrip-animation>
    <kendo:tabStrip-items>
        <kendo:tabStrip-item text="One" selected="true">
            <kendo:tabStrip-item-content>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer felis libero, lobortis ac rutrum quis, varius a velit. Donec lacus erat, cursus sed porta quis, adipiscing et ligula. Duis volutpat, sem pharetra accumsan pharetra, mi ligula cursus felis, ac aliquet leo diam eget risus. Integer facilisis, justo cursus venenatis vehicula, massa nisl tempor sem, in ullamcorper neque mauris in orci.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Two">
            <kendo:tabStrip-item-content>
                <p>Ut orci ligula, varius ac consequat in, rhoncus in dolor. Mauris pulvinar molestie accumsan. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Aenean velit ligula, pharetra quis aliquam sed, scelerisque sed sapien. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam dui mi, vulputate vitae pulvinar ac, condimentum sed eros.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Three">
            <kendo:tabStrip-item-content>
                <p>Aliquam at nisl quis est adipiscing bibendum. Nam malesuada eros facilisis arcu vulputate at aliquam nunc tempor. In commodo scelerisque enim, eget sodales lorem condimentum rutrum. Phasellus sem metus, ultricies at commodo in, tristique non est. Morbi vel mauris eget mauris commodo elementum. Nam eget libero lacus, ut sollicitudin ante. Nam odio quam, suscipit a fringilla eget, dignissim nec arcu. Donec tristique arcu ut sapien elementum pellentesque.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
    </kendo:tabStrip-items>
</kendo:tabStrip>

 <h4>Bottom</h4>

<kendo:tabStrip name="tabstrip-bottom" tabPosition="bottom">
    <kendo:tabStrip-animation>
        <kendo:tabStrip-animation-open effects="fadeIn" />
    </kendo:tabStrip-animation>
    <kendo:tabStrip-items>
        <kendo:tabStrip-item text="One" selected="true">
            <kendo:tabStrip-item-content>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer felis libero, lobortis ac rutrum quis, varius a velit. Donec lacus erat, cursus sed porta quis, adipiscing et ligula. Duis volutpat, sem pharetra accumsan pharetra, mi ligula cursus felis, ac aliquet leo diam eget risus. Integer facilisis, justo cursus venenatis vehicula, massa nisl tempor sem, in ullamcorper neque mauris in orci.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Two">
            <kendo:tabStrip-item-content>
                <p>Ut orci ligula, varius ac consequat in, rhoncus in dolor. Mauris pulvinar molestie accumsan. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Aenean velit ligula, pharetra quis aliquam sed, scelerisque sed sapien. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Aliquam dui mi, vulputate vitae pulvinar ac, condimentum sed eros.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
        <kendo:tabStrip-item text="Three">
            <kendo:tabStrip-item-content>
                <p>Aliquam at nisl quis est adipiscing bibendum. Nam malesuada eros facilisis arcu vulputate at aliquam nunc tempor. In commodo scelerisque enim, eget sodales lorem condimentum rutrum. Phasellus sem metus, ultricies at commodo in, tristique non est. Morbi vel mauris eget mauris commodo elementum. Nam eget libero lacus, ut sollicitudin ante. Nam odio quam, suscipit a fringilla eget, dignissim nec arcu. Donec tristique arcu ut sapien elementum pellentesque.</p>
            </kendo:tabStrip-item-content>
        </kendo:tabStrip-item>
    </kendo:tabStrip-items>
</kendo:tabStrip>
</div>
<demo:footer />