<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageTemplate.ascx.cs"
    Inherits="ImageTemplate" %>
<asp:HiddenField ID="hdImageID" runat="server" />
<div style="width: 125px; height: 100px">
    <a id="imageAnchor" runat="server" rel="lightbox">
        <img alt="" id="image" runat="server" width="90"  />
    </a>
    <div id="imageInfo" style="">
        <asp:Label ID='lbName' runat="server" />
        (<asp:Label ID="lbDimensions" runat="server" />)
    </div>
    <asp:CheckBox ID="chbSelected" runat="server" class="selection" />
</div>
