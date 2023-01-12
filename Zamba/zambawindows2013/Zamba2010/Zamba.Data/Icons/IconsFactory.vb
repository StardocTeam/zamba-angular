Imports Zamba.Core
'Imports Zamba.Icons.Core
Public NotInheritable Class IconsFactory
    Inherits ZClass
    Public Overrides Sub Dispose()

    End Sub


    ' Devuelve el path de un icono por su IconType(Clave)
    ' previamente guardado en zamba...
    Public Shared Function GetIconsPath(ByVal IconsType As IconsType) As String
        Try
            Dim Strselect As String = _
            "Select Value from Zopt where Item = 'Icons" _
            & IconsType.ToString & "'"
            Return Server.Con.ExecuteScalar(CommandType.Text, Strselect)
        Catch ex As Exception
            raiseerror(ex)
            Return ""
        End Try
    End Function

    Public Shared Function GetIconsPathString(ByVal iconKey As String) As String
        Try
            Dim Strselect As String = _
            "Select Value from Zopt where Item = 'Icons" & iconKey & "'"
            Return Server.Con.ExecuteScalar(CommandType.Text, Strselect)
        Catch ex As Exception
            raiseerror(ex)
            Return ""
        End Try
    End Function
    ' Devuelve los paths de todos los iconos del servidor de imagenes..
    Public Shared Function GetServerImages() As DsImageServer
        Dim iSvr As New DsImageServer
        Dim ds As DataSet = _
        Server.Con.ExecuteDataset(CommandType.Text, _
        "SELECT * from imageserver")
        iSvr.Tables(0).TableName = ds.Tables(0).TableName
        iSvr.Merge(ds)
        Return iSvr
    End Function



    Public Enum IconsType
        WorkFlow
    End Enum



    Public Shared Sub SetIconspath(ByVal Type As String, ByVal Path As String)
        Try
            Dim Strinsert As String = _
            "insert into Zopt (item,value) Values ('Icons" & Type & "','" & Path & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub



    Public Shared Sub UpdateIconspath(ByVal Type As String, ByVal Path As String)
        Try
            Dim Strupdate As String = _
            "Update Zopt set Value = '" & Path & "'" & " where item = 'Icons" & Type & "'"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetPicturesPath(ByVal Type As String) As String
        Try
            Dim Strselect As String = _
            "Select Value from Zopt where Item = 'Pictures" & Type & "'"
            Return Server.Con.ExecuteScalar(CommandType.Text, Strselect)
        Catch ex As Exception
            raiseerror(ex)
            Return ""
        End Try
    End Function
    Public Shared Function GetServerImagesPath() As String
        Dim Strselect As String = _
        "Select Value from Zopt where Item = 'PicturesUSERPICTURES'"
        Return Server.Con.ExecuteScalar(CommandType.Text, Strselect)
    End Function
    Public Shared Sub SetPicturespath(ByVal Type As String, ByVal Path As String)
        Try
            Dim Strinsert As String = _
             "insert into Zopt (item,value) Values ('Pictures" _
             & Type & "','" & Path & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strinsert)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub UpdatePicturespath(ByVal Type As String, ByVal Path As String)
        Try
            Dim Strupdate As String = "Update Zopt set Value = '" & Path & "'" & _
                                      " where item = 'Pictures" & Type & "'"
            Server.Con.ExecuteNonQuery(CommandType.Text, Strupdate)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

End Class
