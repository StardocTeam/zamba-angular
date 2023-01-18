<%@ Page Language="C#" MasterPageFile="~/MasterBlankPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="WebPages_News" Title="Untitled Page" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <br /> 
    <center>
        <asp:UpdatePanel runat="server" ID="UpdGrid" UpdateMode="conditional">
            <ContentTemplate>
                <div id="Div1" class="gridContainer">
                    <asp:GridView ID="grdNews" runat="server" AllowPaging="False" AllowSorting="False"
                        AutoGenerateColumns="False" CssClass="GridViewStyle" GridLines="None">
                        <RowStyle CssClass="RowStylenews" Wrap="false" />
                        <EmptyDataRowStyle CssClass="EmptyRowStylenews" Wrap="false" />
                        <PagerStyle CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStylenews" Wrap="false"/>
                        <HeaderStyle CssClass="HeaderStyle" Wrap="false"/>
                        <EditRowStyle CssClass="EditRowStylenews" Wrap="false"/>
                        <AlternatingRowStyle CssClass="AltRowStylenews" Wrap="false"/>
                    </asp:GridView>
                    <asp:Label runat="server" ID="lblZeroNews" Text="No hay novedades disponibles" Visible="false" Font-Size="Medium" ></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
 
