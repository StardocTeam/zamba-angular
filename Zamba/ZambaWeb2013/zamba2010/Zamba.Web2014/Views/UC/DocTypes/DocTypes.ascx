<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocTypes.ascx.cs" Inherits="DocTypes" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
                      
            <asp:DataList ID="DataList1" SkinID="skDataList1" runat="server"  OnItemDataBound="Dt_ItemDataBound">
                <ItemTemplate>
                  <span>  <asp:TextBox runat="server" Text='<%# Eval("Doc_Type_ID") %>' ID="Doc_Type_ID" Visible="false"
                        AutoPostBack="True" />
                   </span>
                     <asp:CheckBox runat="server" Text='<%# Eval("Doc_Type_Name") %>' dtid='<%# Eval("Doc_Type_ID") %>'
                        ID="DocTypeCheckBox" Name="DocTypeName" OnCheckedChanged="Check" AutoPostBack="True" />
                </ItemTemplate>
            </asp:DataList>
            
       
    </ContentTemplate>
</asp:UpdatePanel>
