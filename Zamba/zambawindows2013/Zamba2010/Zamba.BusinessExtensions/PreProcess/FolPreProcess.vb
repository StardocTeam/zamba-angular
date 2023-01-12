Imports Zamba.servers
Public Class FolPreProcess
    Inherits ZClass

#Region "PreProcess Functions"
    Public Shared Function getPreprocess(ByVal id As Integer) As ArrayList
        Dim dsp As New DataSet
        Dim PreProcessList As New ArrayList
        Try
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select ip_Id, preprocessname, preprocessparam, orderpos from ip_preprocess where ip_id=" & id.ToString & " order by orderpos")
            ds.Tables(0).TableName = "Ip_Preprocess"
            dsp.Merge(ds)
            Dim i As Integer

            For i = 0 To dsp.Tables("Ip_Preprocess").Rows.Count - 1

                Dim pre As New PreProcessOBJ

                pre.id = dsp.Tables("Ip_Preprocess")(i)("IP_ID")
                If Not IsDBNull(dsp.Tables("Ip_Preprocess")(i)("PREPROCESSNAME")) Then
                    pre.name = dsp.Tables("Ip_Preprocess")(i)("PREPROCESSNAME")
                Else
                    pre.name = String.Empty
                End If
                Try
                    If Not IsDBNull(dsp.Tables("Ip_Preprocess")(i)("PREPROCESSPARAM")) Then
                        pre.Param = dsp.Tables("Ip_Preprocess")(i)("PREPROCESSPARAM")
                Else
                        pre.Param = String.Empty
                    End If
                Catch ex As System.Data.StrongTypingException
                    pre.Param = String.Empty
                End Try

                pre.order = dsp.Tables("Ip_Preprocess")(i)("ORDERpos")

                PreProcessList.Add(pre)
            Next
        Catch ex As Exception
            Return PreProcessList
        End Try
        Return PreProcessList
    End Function
    Public Shared Sub savepreprocess(ByVal preid As Integer, ByVal preprocess As ArrayList)
        Dim pre As PreProcessOBJ
        'Server.Con.ExecuteNonQuery(CommandType.Text, "delete ip_preprocess where ip_id=" & preid.ToString)
        Server.Con.ExecuteNonQuery(CommandType.Text, "delete ip_Folder_preprocess where ip_id=" & preid.ToString)

        For Each pre In preprocess
            Dim str As New Text.StringBuilder
            str.Append("insert into ip_Folder_preprocess(ip_id,preprocessname,preprocessparam,orderpos) values(")
            str.Append(pre.id.ToString & ",")
            str.Append("'" & pre.name.Replace("'", "''") & "',")
            str.Append("'" & pre.Param.Replace("'", "''") & "',")
            str.Append(pre.order.ToString & ")")
            Server.Con.ExecuteNonQuery(CommandType.Text, str.ToString)
        Next
    End Sub
#End Region

    Public Overrides Sub Dispose()

    End Sub

End Class

