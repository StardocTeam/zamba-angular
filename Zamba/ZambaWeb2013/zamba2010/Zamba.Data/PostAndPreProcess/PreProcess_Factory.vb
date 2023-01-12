Imports Zamba.Servers
Imports Zamba.Core

Public Class PreProcess_Factory
    Public Shared Function getSlst_s45(ByVal CodeCompany As String) As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "Select Codigo,replace(Descripcion,'''',' ' ) from slst_s45 where item=" & CodeCompany)
    End Function

    Public Shared Sub savePreprocess(ByVal preid As Integer, ByVal preprocess As ArrayList)
        Dim pre As PreProcessOBJ
        Server.Con.ExecuteNonQuery(CommandType.Text, "Delete from ip_folder_preprocess where ip_id=" & preid.ToString)
     
        For Each pre In preprocess
            Dim str As New System.Text.StringBuilder
            str.Append("insert into ip_folder_preprocess(ip_id,preprocessname,preprocessparam,orderpos) values(")
            str.Append(pre.id.ToString & ",")
            str.Append("'" & pre.name.Replace("'", "''") & "',")
            str.Append("'" & pre.Param.Replace("'", "''") & "',")
            str.Append(pre.order.ToString & ")")
            Server.Con.ExecuteNonQuery(CommandType.Text, str.ToString)

        Next
    End Sub

    Public Shared Function GetPreProcessById(ByVal Id As Int32) As Core.dsIPPreprocess
        Dim dsp As New Core.dsIPPreprocess
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select ip_Id, preprocessname, preprocessparam, orderpos from ip_Folder_preprocess where ip_id=" & Id.ToString & " order by orderpos")
        ds.Tables(0).TableName = "Ip_Preprocess"
        dsp.Merge(ds)
        Return dsp
    End Function

End Class
