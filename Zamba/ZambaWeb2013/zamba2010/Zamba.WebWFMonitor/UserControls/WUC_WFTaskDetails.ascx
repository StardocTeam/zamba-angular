<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WUC_WFTaskDetails.ascx.vb" Inherits="WUC_WFTaskDetails" %>
<asp:FormView ID="FormView1" runat="server" DataSourceID="odsTaskDetails" Font-Names="Verdana" Font-Size="X-Small" Width="408px" Height="80px">
    <EditItemTemplate>
        ISVIRTUAL:
        <asp:CheckBox ID="ISVIRTUALCheckBox" runat="server" Checked='<%# Bind("ISVIRTUAL") %>' /><br />
        IsPowerpoint:
        <asp:CheckBox ID="IsPowerpointCheckBox" runat="server" Checked='<%# Bind("IsPowerpoint") %>' /><br />
        File_Format_ID:
        <asp:TextBox ID="File_Format_IDTextBox" runat="server" Text='<%# Bind("File_Format_ID") %>'>
        </asp:TextBox><br />
        Picture:
        <asp:TextBox ID="PictureTextBox" runat="server" Text='<%# Bind("Picture") %>'>
        </asp:TextBox><br />
        IsOffice2:
        <asp:CheckBox ID="IsOffice2CheckBox" runat="server" Checked='<%# Bind("IsOffice2") %>' /><br />
        Id:
        <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>'>
        </asp:TextBox><br />
        IsOffice:
        <asp:CheckBox ID="IsOfficeCheckBox" runat="server" Checked='<%# Bind("IsOffice") %>' /><br />
        Format:
        <asp:TextBox ID="FormatTextBox" runat="server" Text='<%# Bind("Format") %>'>
        </asp:TextBox><br />
        CreateDate:
        <asp:TextBox ID="CreateDateTextBox" runat="server" Text='<%# Bind("CreateDate") %>'>
        </asp:TextBox><br />
        Doc_File:
        <asp:TextBox ID="Doc_FileTextBox" runat="server" Text='<%# Bind("Doc_File") %>'>
        </asp:TextBox><br />
        CheckIn:
        <asp:TextBox ID="CheckInTextBox" runat="server" Text='<%# Bind("CheckIn") %>'>
        </asp:TextBox><br />
        AutoName:
        <asp:TextBox ID="AutoNameTextBox" runat="server" Text='<%# Bind("AutoName") %>'>
        </asp:TextBox><br />
        Index:
        <asp:TextBox ID="IndexTextBox" runat="server" Text='<%# Bind("Index") %>'>
        </asp:TextBox><br />
        IsMAG:
        <asp:CheckBox ID="IsMAGCheckBox" runat="server" Checked='<%# Bind("IsMAG") %>' /><br />
        IsOpen:
        <asp:CheckBox ID="IsOpenCheckBox" runat="server" Checked='<%# Bind("IsOpen") %>' /><br />
        DocumentalId:
        <asp:TextBox ID="DocumentalIdTextBox" runat="server" Text='<%# Bind("DocumentalId") %>'>
        </asp:TextBox><br />
        ObjecttypeId:
        <asp:TextBox ID="ObjecttypeIdTextBox" runat="server" Text='<%# Bind("ObjecttypeId") %>'>
        </asp:TextBox><br />
        OffSet:
        <asp:TextBox ID="OffSetTextBox" runat="server" Text='<%# Bind("OffSet") %>'>
        </asp:TextBox><br />
        Childs:
        <asp:TextBox ID="ChildsTextBox" runat="server" Text='<%# Bind("Childs") %>'>
        </asp:TextBox><br />
        IsExpired:
        <asp:CheckBox ID="IsExpiredCheckBox" runat="server" Checked='<%# Bind("IsExpired") %>' /><br />
        ExpireDate:
        <asp:TextBox ID="ExpireDateTextBox" runat="server" Text='<%# Bind("ExpireDate") %>'>
        </asp:TextBox><br />
        IsText:
        <asp:CheckBox ID="IsTextCheckBox" runat="server" Checked='<%# Bind("IsText") %>' /><br />
        State:
        <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Bind("State") %>'>
        </asp:TextBox><br />
        Disk_Group_Id:
        <asp:TextBox ID="Disk_Group_IdTextBox" runat="server" Text='<%# Bind("Disk_Group_Id") %>'>
        </asp:TextBox><br />
        Indexs:
        <asp:TextBox ID="IndexsTextBox" runat="server" Text='<%# Bind("Indexs") %>'>
        </asp:TextBox><br />
        EditDate:
        <asp:TextBox ID="EditDateTextBox" runat="server" Text='<%# Bind("EditDate") %>'>
        </asp:TextBox><br />
        IsImage:
        <asp:CheckBox ID="IsImageCheckBox" runat="server" Checked='<%# Bind("IsImage") %>' /><br />
        TaskState:
        <asp:TextBox ID="TaskStateTextBox" runat="server" Text='<%# Bind("TaskState") %>'>
        </asp:TextBox><br />
        FullPath:
        <asp:TextBox ID="FullPathTextBox" runat="server" Text='<%# Bind("FullPath") %>'>
        </asp:TextBox><br />
        Platter_Id:
        <asp:TextBox ID="Platter_IdTextBox" runat="server" Text='<%# Bind("Platter_Id") %>'>
        </asp:TextBox><br />
        Thumbnails:
        <asp:TextBox ID="ThumbnailsTextBox" runat="server" Text='<%# Bind("Thumbnails") %>'>
        </asp:TextBox><br />
        DocType:
        <asp:TextBox ID="DocTypeTextBox" runat="server" Text='<%# Bind("DocType") %>'>
        </asp:TextBox><br />
        PrintName:
        <asp:TextBox ID="PrintNameTextBox" runat="server" Text='<%# Bind("PrintName") %>'>
        </asp:TextBox><br />
        WfStep:
        <asp:TextBox ID="WfStepTextBox" runat="server" Text='<%# Bind("WfStep") %>'>
        </asp:TextBox><br />
        Parent:
        <asp:TextBox ID="ParentTextBox" runat="server" Text='<%# Bind("Parent") %>'>
        </asp:TextBox><br />
        DISK_VOL_PATH:
        <asp:TextBox ID="DISK_VOL_PATHTextBox" runat="server" Text='<%# Bind("DISK_VOL_PATH") %>'>
        </asp:TextBox><br />
        IconId:
        <asp:TextBox ID="IconIdTextBox" runat="server" Text='<%# Bind("IconId") %>'>
        </asp:TextBox><br />
        PrintPicture:
        <asp:TextBox ID="PrintPictureTextBox" runat="server" Text='<%# Bind("PrintPicture") %>'>
        </asp:TextBox><br />
        FolderId:
        <asp:TextBox ID="FolderIdTextBox" runat="server" Text='<%# Bind("FolderId") %>'>
        </asp:TextBox><br />
        Object_Type_Id:
        <asp:TextBox ID="Object_Type_IdTextBox" runat="server" Text='<%# Bind("Object_Type_Id") %>'>
        </asp:TextBox><br />
        Name:
        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>'>
        </asp:TextBox><br />
        File:
        <asp:TextBox ID="FileTextBox" runat="server" Text='<%# Bind("File") %>'>
        </asp:TextBox><br />
        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
            Text="Update">
        </asp:LinkButton>
        <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
            Text="Cancel">
        </asp:LinkButton>
    </EditItemTemplate>
    <InsertItemTemplate>
        ISVIRTUAL:
        <asp:CheckBox ID="ISVIRTUALCheckBox" runat="server" Checked='<%# Bind("ISVIRTUAL") %>' /><br />
        IsPowerpoint:
        <asp:CheckBox ID="IsPowerpointCheckBox" runat="server" Checked='<%# Bind("IsPowerpoint") %>' /><br />
        File_Format_ID:
        <asp:TextBox ID="File_Format_IDTextBox" runat="server" Text='<%# Bind("File_Format_ID") %>'>
        </asp:TextBox><br />
        Picture:
        <asp:TextBox ID="PictureTextBox" runat="server" Text='<%# Bind("Picture") %>'>
        </asp:TextBox><br />
        IsOffice2:
        <asp:CheckBox ID="IsOffice2CheckBox" runat="server" Checked='<%# Bind("IsOffice2") %>' /><br />
        Id:
        <asp:TextBox ID="IdTextBox" runat="server" Text='<%# Bind("Id") %>'>
        </asp:TextBox><br />
        IsOffice:
        <asp:CheckBox ID="IsOfficeCheckBox" runat="server" Checked='<%# Bind("IsOffice") %>' /><br />
        Format:
        <asp:TextBox ID="FormatTextBox" runat="server" Text='<%# Bind("Format") %>'>
        </asp:TextBox><br />
        CreateDate:
        <asp:TextBox ID="CreateDateTextBox" runat="server" Text='<%# Bind("CreateDate") %>'>
        </asp:TextBox><br />
        Doc_File:
        <asp:TextBox ID="Doc_FileTextBox" runat="server" Text='<%# Bind("Doc_File") %>'>
        </asp:TextBox><br />
        CheckIn:
        <asp:TextBox ID="CheckInTextBox" runat="server" Text='<%# Bind("CheckIn") %>'>
        </asp:TextBox><br />
        AutoName:
        <asp:TextBox ID="AutoNameTextBox" runat="server" Text='<%# Bind("AutoName") %>'>
        </asp:TextBox><br />
        Index:
        <asp:TextBox ID="IndexTextBox" runat="server" Text='<%# Bind("Index") %>'>
        </asp:TextBox><br />
        IsMAG:
        <asp:CheckBox ID="IsMAGCheckBox" runat="server" Checked='<%# Bind("IsMAG") %>' /><br />
        IsOpen:
        <asp:CheckBox ID="IsOpenCheckBox" runat="server" Checked='<%# Bind("IsOpen") %>' /><br />
        DocumentalId:
        <asp:TextBox ID="DocumentalIdTextBox" runat="server" Text='<%# Bind("DocumentalId") %>'>
        </asp:TextBox><br />
        ObjecttypeId:
        <asp:TextBox ID="ObjecttypeIdTextBox" runat="server" Text='<%# Bind("ObjecttypeId") %>'>
        </asp:TextBox><br />
        OffSet:
        <asp:TextBox ID="OffSetTextBox" runat="server" Text='<%# Bind("OffSet") %>'>
        </asp:TextBox><br />
        Childs:
        <asp:TextBox ID="ChildsTextBox" runat="server" Text='<%# Bind("Childs") %>'>
        </asp:TextBox><br />
        IsExpired:
        <asp:CheckBox ID="IsExpiredCheckBox" runat="server" Checked='<%# Bind("IsExpired") %>' /><br />
        ExpireDate:
        <asp:TextBox ID="ExpireDateTextBox" runat="server" Text='<%# Bind("ExpireDate") %>'>
        </asp:TextBox><br />
        IsText:
        <asp:CheckBox ID="IsTextCheckBox" runat="server" Checked='<%# Bind("IsText") %>' /><br />
        State:
        <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Bind("State") %>'>
        </asp:TextBox><br />
        Disk_Group_Id:
        <asp:TextBox ID="Disk_Group_IdTextBox" runat="server" Text='<%# Bind("Disk_Group_Id") %>'>
        </asp:TextBox><br />
        Indexs:
        <asp:TextBox ID="IndexsTextBox" runat="server" Text='<%# Bind("Indexs") %>'>
        </asp:TextBox><br />
        EditDate:
        <asp:TextBox ID="EditDateTextBox" runat="server" Text='<%# Bind("EditDate") %>'>
        </asp:TextBox><br />
        IsImage:
        <asp:CheckBox ID="IsImageCheckBox" runat="server" Checked='<%# Bind("IsImage") %>' /><br />
        TaskState:
        <asp:TextBox ID="TaskStateTextBox" runat="server" Text='<%# Bind("TaskState") %>'>
        </asp:TextBox><br />
        FullPath:
        <asp:TextBox ID="FullPathTextBox" runat="server" Text='<%# Bind("FullPath") %>'>
        </asp:TextBox><br />
        Platter_Id:
        <asp:TextBox ID="Platter_IdTextBox" runat="server" Text='<%# Bind("Platter_Id") %>'>
        </asp:TextBox><br />
        Thumbnails:
        <asp:TextBox ID="ThumbnailsTextBox" runat="server" Text='<%# Bind("Thumbnails") %>'>
        </asp:TextBox><br />
        DocType:
        <asp:TextBox ID="DocTypeTextBox" runat="server" Text='<%# Bind("DocType") %>'>
        </asp:TextBox><br />
        PrintName:
        <asp:TextBox ID="PrintNameTextBox" runat="server" Text='<%# Bind("PrintName") %>'>
        </asp:TextBox><br />
        WfStep:
        <asp:TextBox ID="WfStepTextBox" runat="server" Text='<%# Bind("WfStep") %>'>
        </asp:TextBox><br />
        Parent:
        <asp:TextBox ID="ParentTextBox" runat="server" Text='<%# Bind("Parent") %>'>
        </asp:TextBox><br />
        DISK_VOL_PATH:
        <asp:TextBox ID="DISK_VOL_PATHTextBox" runat="server" Text='<%# Bind("DISK_VOL_PATH") %>'>
        </asp:TextBox><br />
        IconId:
        <asp:TextBox ID="IconIdTextBox" runat="server" Text='<%# Bind("IconId") %>'>
        </asp:TextBox><br />
        PrintPicture:
        <asp:TextBox ID="PrintPictureTextBox" runat="server" Text='<%# Bind("PrintPicture") %>'>
        </asp:TextBox><br />
        FolderId:
        <asp:TextBox ID="FolderIdTextBox" runat="server" Text='<%# Bind("FolderId") %>'>
        </asp:TextBox><br />
        Object_Type_Id:
        <asp:TextBox ID="Object_Type_IdTextBox" runat="server" Text='<%# Bind("Object_Type_Id") %>'>
        </asp:TextBox><br />
        Name:
        <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>'>
        </asp:TextBox><br />
        File:
        <asp:TextBox ID="FileTextBox" runat="server" Text='<%# Bind("File") %>'>
        </asp:TextBox><br />
        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
            Text="Insert">
        </asp:LinkButton>
        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
            Text="Cancel">
        </asp:LinkButton>
    </InsertItemTemplate>
    <ItemTemplate>
        Fecha de Creacion:
        <asp:Label ID="CreateDateLabel" runat="server" Text='<%# Bind("CreateDate") %>'>
        </asp:Label><br />
        Nombre:
        <asp:Label ID="AutoNameLabel" runat="server" Text='<%# Bind("AutoName") %>'></asp:Label><br />
        Fecha De Expiracion:
        <asp:Label ID="ExpireDateLabel" runat="server" Text='<%# Bind("ExpireDate") %>'>
        </asp:Label><br />
        Estado:
        <asp:Label ID="StateLabel" runat="server" Text='<%# Bind("State") %>'></asp:Label><br />
        Asignacion:
        <asp:Label ID="TaskStateLabel" runat="server" Text='<%# Bind("TaskState") %>'></asp:Label><br />
        <br />
    </ItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="odsTaskDetails" runat="server" SelectMethod="GetTaskByTaskId"
    TypeName="Zamba.WFBusiness.WFTaskBussines">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="TaskId" SessionField="TaskId" Type="Int32" />
        <asp:SessionParameter DefaultValue="0" Name="WfId" SessionField="WfId" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
