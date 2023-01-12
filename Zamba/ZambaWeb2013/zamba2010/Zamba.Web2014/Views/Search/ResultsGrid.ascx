<%@ Control Language="C#"  AutoEventWireup="false" CodeFile="ResultsGrid.ascx.cs" Inherits="ResultsGrid" %>
<%@ Register Assembly="ExtExtenders" Namespace="ExtExtenders" TagPrefix="cc1" %>

<div class="main_toolbar">
    <table>
        <tr>
            <td style="padding-left:10px; padding-right:15px;">
                <asp:ImageButton ID="imgbtnimprimir" runat="server" 
                    ImageUrl="~/Content/Images/Toolbars/print.png" 
                    Height="20px" Width="25px"  ToolTip="Imprimir" />
            </td>
            <td style="padding-left:10px; padding-right:15px;">
                <asp:ImageButton ID="imgbtnincdoc" runat="server"
                    ImageUrl="~/Content/Images/Toolbars/folder_add_32.png"
                    Height="20px" Width="25px"   ToolTip="Incorporar Documento"/>
            </td>
            <td style="padding-left:10px; padding-right:15px;">
                <asp:ImageButton ID="imgtareas" runat="server"
                    ImageUrl="~/Content/Images/Toolbars/note_pinned.png"
                    Height="20px" Width="25px"   ToolTip="Ver tarea asociada al documento" />
            </td>
        </tr>
    </table>
</div>

 <cc1:YuiGrid runat="server" ClicksToEdit="2" 
    ID="YuiGrid1"  
    AutoHeight="False" 
    Height="300px"
    Width="900px" 
    SelectMultiple="false" 
    AutoGenerateColumns="true"    
    AutoPostBack="true"      
    EnableColumnHide="false">
</cc1:YuiGrid>