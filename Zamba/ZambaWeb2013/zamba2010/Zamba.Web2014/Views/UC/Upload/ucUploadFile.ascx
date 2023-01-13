<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUploadFile.ascx.cs" Inherits="Views_UC_Upload_ucUploadFile" %>

<asp:Panel ID="pnlListadoIndices" runat="server" width="550px" ScrollBars="Auto">
    <fieldset title="" class="Fielset-controles-UC" enableviewstate="true" style="padding:5px;">
        <h5>Paso 3. Seleccione el archivo que desea subir</h5>
        <div class="UserControlBody" >
            <form name="form1" method="post" enctype="multipart/form-data">
                <asp:FileUpload ID="FileUpload" runat="server" style="width:500px"  />
            </form>
        </div>
    </fieldset>
</asp:Panel>

