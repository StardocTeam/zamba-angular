<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WFFilters.ascx.cs" Inherits="Web.WFFilters" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

 Proceso: <asp:DropDownList ID="DDWF" 
            runat="server" AutoPostBack="True" DataSourceID="WorkflowsSQL" 
            DataTextField="Name" DataValueField="work_id">
        </asp:DropDownList>
    
        <asp:SqlDataSource ID="WorkflowsSQL" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
            SelectCommand="SELECT [work_id], [Name] FROM [WFWorkflow] ORDER BY [Name]">
           
        </asp:SqlDataSource>

<asp:SqlDataSource ID="WFSQL" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
           SelectCommand="SELECT [step_Id], [Name] FROM [WFStep] where work_id = @work_id  ORDER BY [Name]">
 <SelectParameters>
                <asp:ControlParameter ControlID="DDWF" DefaultValue="0" Name="work_id" 
                    PropertyName="SelectedValue" Type="Decimal" />
</SelectParameters>           
 </asp:SqlDataSource>

 <telerik:RadComboBox ID="CHKLSTEPS" runat="server" AutoPostBack="True" 
    CheckBoxes="True" DataSourceID="WFSQL" DataTextField="Name" 
    DataValueField="step_id" Skin="Windows7" 
    onselectedindexchanged="CHKLSTEPS_SelectedIndexChanged1">
</telerik:RadComboBox>

<telerik:RadComboBox ID="CHKLSTATES" runat="server" AutoPostBack="True" 
    CheckBoxes="True" DataSourceID="StatesSQL" DataTextField="Name" 
    DataValueField="Doc_State_ID" Skin="Windows7">
</telerik:RadComboBox>


<input type="hidden" id="hdnstepsids" runat="server" />
<input type="hidden" id="hdnstatesids" runat="server" />

<asp:SqlDataSource ID="StatesSQL" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ZambaConnectionString %>" 
    
    SelectCommand="SELECT [Doc_State_ID], [Name] FROM [WFStepStates] WHERE ([Step_Id] in (@Step_Id)) ORDER BY [Name]">
    <SelectParameters>
        <asp:ControlParameter ControlID="CHKLSTEPS" Name="Step_Id" 
            PropertyName="SelectedValue" Type="Decimal" />
    </SelectParameters>
 
</asp:SqlDataSource>

