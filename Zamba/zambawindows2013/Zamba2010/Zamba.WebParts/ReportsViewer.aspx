<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportsViewer.aspx.cs" Inherits="ReportsViewer"
    Title="Zamba WorkFlow Informes" MasterPageFile="~/ZambaMaster.master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="margin: 0px 0px 0px 0px">
                <table cellpadding="0" cellspacing="0">
                    <tr style="margin: 0px 0px 0px 0px">
                        <td valign="top" style="width: 20px">

                        </td>
                        <td valign="top" style="margin: 0px 0px 0px 0px">
                                                    <ajaxToolKit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250" AutoSize="None"
                                RequireOpenedPane="false" SuppressHeaderPostbacks="true" >
                                <Panes>
                                    <ajaxToolKit:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>
                                            <a href="">Seleccione...</a></Header>
                                        <Content>
                                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" NodeIndent="10"
                                                ShowCheckBoxes="All" Width="150px" ShowExpandCollapse="False" NodeWrap="True"
                                                OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                                <ParentNodeStyle Font-Bold="False" />
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" Font-Size="X-Small" />
                                                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                                    VerticalPadding="0px" />
                                                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="1px"
                                                    NodeSpacing="0px" VerticalPadding="5px" />
                                                <LeafNodeStyle Font-Size="XX-Small" />
                                            </asp:TreeView>
                                            <br />
                                            <div>
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Aplicar selección"
                                                    Width="123px" />&nbsp;</div>
                                            <div>
                                                <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Actualizar"
                                                    Width="123px" />&nbsp;</div>
                                        </Content>
                                    </ajaxToolKit:AccordionPane>
                                </Panes>
                            </ajaxToolKit:Accordion>
                            
                            <asp:Table runat="server" ID="tblUcs" CellPadding="0" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top" Height="50px" ID="td1" RowSpan="0">
                                        <iframe style="margin: 0px 0px 0px 0px" id="iframeAverageTime" frameborder="no" height="380"
                                            width="310" runat="server" src="UserControls/UCAverageTimeInSteps/AverageTimeInSteps.aspx" />
                                        <iframe style="margin: 0px 0px 0px 0px" id="iframeTasksBalances" frameborder="no"
                                            height="380" width="310" runat="server" src="UserControls/UCTaskBalances/TaskBalances.aspx" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="top" Height="50px" ID="td3">
                                        <iframe style="margin: 0px 0px 0px 0px" id="iframeAsignedTasks" frameborder="no"
                                            height="410" width="310" runat="server" src="UserControls/UCAsignedTasksCount/AsignedTasksCount.aspx" />
                                        <iframe style="margin: 0px 0px 0px 0px" id="iframeTasksToExpire" frameborder="no"
                                            height="410" width="310" runat="server" src="UserControls/UCTaskToExpire/TaskToExpire.aspx" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
